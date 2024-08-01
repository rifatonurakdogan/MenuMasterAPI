using MenuMasterAPI.Application.DTOs;
using MenuMasterAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuMasterAPI.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService service)
    {
        _userService = service;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(UserRegisterContract request)
    {
        await _userService.RegisterUserAsync(request);
        var loginContract = new UserLoginContract
        {
            Email = request.Email,
            Password = request.Password
        };
        return await LoginAsync(loginContract);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(UserLoginContract request)
    {
        var response = await _userService.LoginUserAsync(request);
        var customResponse = new
        {
            accessToken = response
        };
        return Ok(customResponse);
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPut("Update")]
    public async Task<IActionResult> UpdateAsync(UserUpdateContract request)
    {
        await _userService.UpdateUserAsync(request);
        return Ok("Updated");
    }
 
    [Authorize(Roles = "Admin, User")]
    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _userService.GetUserByIdAsync(id);
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPost("PostRecipes")]
    public async Task<IActionResult> GetMealRecipes(UserGetMealRecipeContract requestModel)
    {
        var responseModel = await _userService.GetMealRecipes(requestModel);
        return Ok(responseModel);
    }
    [Authorize(Roles = "Admin, User")]
    [HttpGet("GetRecipesFromImage")]
    public async Task<IActionResult> GetMealRecipesFromImage(string imagePath, int userId)
    {
        var responseModel = await _userService.GetMealRecipesFromImage(imagePath,userId);
        return Ok(responseModel);
    }
    [Authorize(Roles = "Admin, User")]
    [HttpGet("GetSavedRecipes")]
    public async Task<IActionResult> GetSavedMealRecipes(int id)
    {
        var responseModel = await _userService.GetSavedMealRecipes(id);
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("Users")]
    public async Task<IActionResult> GetAllAsync()
    {
        var responseModel = await _userService.GetAllAsync();
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _userService.RemoveByIdAsync(id);
        return Ok("Delete successful.");
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("PagedUsers")]
    public async Task<IActionResult> GetAllPagedAsync([FromQuery] PaginationFilter filter)
    {
        PaginationFilter paginationFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var responseModel = await _userService.GetPagedDataAsync(paginationFilter.PageNumber, paginationFilter.PageSize);
        return Ok(responseModel);
    }
}
