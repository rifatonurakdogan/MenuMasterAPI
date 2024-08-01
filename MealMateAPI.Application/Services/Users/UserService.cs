using AutoMapper;
using MenuMasterAPI.Application.Auth;
using MenuMasterAPI.Application.DTOs;
using MenuMasterAPI.Application.Interfaces;
using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;
using MenuMasterAPI.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
namespace MenuMasterAPI.Application.Services;

public class UserService : BaseService<User, UserContract>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly OpenAIService _openAIService;
    private readonly IFoodRecipeService _foodRecipeService;
    public UserService(IGenericRepository<User, UserContract> repository, IUserRepository userRepository, IMapper mapper, IAuthService authService, IPasswordHasher<User> passwordHasher, OpenAIService openAIService, IFoodRecipeService foodRecipeService)
           : base(repository, mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _authService = authService;
        _passwordHasher = passwordHasher;
        _openAIService = openAIService;
        _foodRecipeService = foodRecipeService;
    }

    public async Task<UserGetContract> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserGetContract>(user);

    }

    public async Task<List<UserGetContract>> GetUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<List<UserGetContract>>(users);
    }

    public async Task<string> LoginUserAsync(UserLoginContract requestModel)
    {
        // Get user from database if user is registered before
        var user = await _userRepository.GetUserIfExistsAsync(requestModel.Email);
        // Validate hashed password with inputted password
        bool passwordValidationCheck = ValidatePassword(user, requestModel.Password);
        if (passwordValidationCheck)
        {
            await SetUserLoginTime(user);
            return _authService.CreateToken(user);
        }
        else
        {
            throw new UserPasswordIncorrectException(requestModel.Email);
        }
    }

    public async Task RegisterUserAsync(UserRegisterContract requestModel)
    {
        var userExistenceCheck = await _userRepository.CheckIfUserExistsAsync(requestModel.Email);
        // Check if there is a user with same email already registered
        if (!userExistenceCheck)
        {
            var user = _mapper.Map<User>(requestModel);
            user.Password = _passwordHasher.HashPassword(user, requestModel.Password);
            await _userRepository.AddAsync(user);
        }
        else
        {
            throw new UserAlreadyExistsException(requestModel.Email);
        }
    }

    public async Task UpdateUserAsync(UserUpdateContract requestModel)
    {
        var currentUser = await _userRepository.GetByIdAsync(requestModel.Id);
        if (currentUser == null || currentUser.IsDeleted)
        {
            throw new UserNotFoundException(requestModel.Email);
        }
        bool currentPasswordValidationCheck = ValidatePassword(currentUser, requestModel.CurrentPassword);
        if (currentPasswordValidationCheck)
        {
            currentUser.Password = _passwordHasher.HashPassword(currentUser, requestModel.NewPassword);
            await _userRepository.UpdateUserPasswordAsync(currentUser);
        }
        else
        {
            throw new UserPasswordIncorrectException(currentUser.Email);
        }
        var mappedContract = _mapper.Map<UserUpdateContractDomain>(requestModel);
        await _userRepository.UpdateAsync(mappedContract);
    }
    public async Task<List<FoodRecipeContract>> GetMealRecipes(UserGetMealRecipeContract requestModel)
     {
        // Prepare user for prompt
        var user = await _userRepository.GetByIdAsync(requestModel.Id);
        var userInformationForPrompt = PrepareUserInformation(requestModel, user);
        // Get response from API
        var response = await _openAIService.SendRequestAsync(userInformationForPrompt);
        var responseList = JsonSerializer.Deserialize<Root>(response) ?? throw new BaseException("Formatting error for response from OpenAI API.");
        var returnList = new List<FoodRecipe>();
        foreach(FoodRecipeContract frc in responseList.recipes)
        {
            var recipeExistenceCheck = await _foodRecipeService.GetFoodRecipeByName(frc.Name);
            // If food recipe with same name already exists, map the recipe to user and add to list
            if(recipeExistenceCheck != null)
            {
                // If user has no foodrecipes, initialize the list and add to list
                (user.FoodRecipes ??= new List<FoodRecipe>()).Add(recipeExistenceCheck);
                //Add existing recipe to return list
                returnList.Add(recipeExistenceCheck);
            }
            else
            {
                await _foodRecipeService.AddAsync(frc);
                var foodRecipeCreated = await _foodRecipeService.GetFoodRecipeByName(frc.Name);
                // If user has no foodrecipes, initialize the list and add to list
                (user.FoodRecipes ??= new List<FoodRecipe>()).Add(foodRecipeCreated);
                //Add created recipe to return list
                returnList.Add(foodRecipeCreated);
            }
        }
        // Save changes made to user
        await _userRepository.SaveAsync();
        return _mapper.Map<List<FoodRecipeContract>>(returnList);
    }

    public async Task<List<FoodRecipeContract>> GetSavedMealRecipes(int id)
    {
        var savedRecipes = await _foodRecipeService.GetUsersFoodRecipes(id);
        return _mapper.Map<List<FoodRecipeContract>>(savedRecipes);
    }
    public bool ValidatePassword(User user, string password)
    {
        var passwordValidation = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        return passwordValidation == PasswordVerificationResult.Success;
    }

    public async Task<List<FoodRecipeContract>> GetMealRecipesFromImage(string imagePath, int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        var response = await _openAIService.SendImageRequestAsync(imagePath);
        var returnList = new List<FoodRecipe>();
        var foodRecipe = JsonSerializer.Deserialize<FoodRecipeContract>(response) ?? throw new BaseException("Formatting error for response from OpenAI API.");
        await _foodRecipeService.AddAsync(foodRecipe);
        var foodRecipeCreated = await _foodRecipeService.GetFoodRecipeByName(foodRecipe.Name);
        (user.FoodRecipes ??= new List<FoodRecipe>()).Add(foodRecipeCreated);
        returnList.Add(foodRecipeCreated);
        await _userRepository.SaveAsync();
        return _mapper.Map<List<FoodRecipeContract>>(returnList);
    }
    public async Task SetUserLoginTime(User user)
    {
        user.LastLogin = DateTime.UtcNow;
        await _userRepository.SaveAsync();
    }
    private UserSendContract PrepareUserInformation(UserGetMealRecipeContract requestModel,User user)
    {
        var userInformationForPrompt = _mapper.Map<UserSendContract>(user);
        userInformationForPrompt.MealTypes = requestModel.MealTypes;
        return userInformationForPrompt;
    }
    private class Root()
    {
        public List<FoodRecipeContract> recipes { get; set; }
    }
}
