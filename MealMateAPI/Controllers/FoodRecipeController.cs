using MenuMasterAPI.Application.DTOs;
using MenuMasterAPI.Application.Interfaces;
using MenuMasterAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieAPI.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodRecipeController : Controller
{
    private readonly IFoodRecipeService _foodRecipeService;
    public FoodRecipeController(IFoodRecipeService foodRecipeService)
    {
        _foodRecipeService = foodRecipeService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Create")]
    public async Task<IActionResult> CreateAsync(FoodRecipeContract request)
    {
        await _foodRecipeService.AddAsync(request);
        return Ok("Successfully added");
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> UpdateAsync(FoodRecipeContract request)
    {
        await _foodRecipeService.UpdateFoodRecipeAsync(request);
        return Ok("Updated");
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("FoodRecipes")]
    public async Task<IActionResult> GetAllAsync()
    {
        var responseModel = await _foodRecipeService.GetAllAsync();
        return Ok(responseModel);
    }
    [Authorize(Roles = "Admin, User")]
    [HttpGet("FoodRecipeByMealType")]
    public async Task<IActionResult> GetFoodRecipeByMealTypeAsync(string mealType)
    {
        var responseModel = await _foodRecipeService.GetFoodRecipeWithMealTypeAsync(mealType);
        return Ok(responseModel);
    }
    [Authorize(Roles = "Admin, User")]
    [HttpGet("FoodRecipeNamesByMealType")]
    // Will return 4 entries for given meal type with id for homepage
    public async Task<IActionResult> GetFoodRecipeNamesAsync(string mealType)
    {
        var responseModel = await _foodRecipeService.GetFoodRecipeNamesWithMealTypeAsync(mealType);
        return Ok(responseModel);

    }
    [Authorize(Roles = "Admin, User")]
    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _foodRecipeService.GetByIdAsync(id);
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _foodRecipeService.RemoveByIdAsync(id);
        return Ok("Delete successful");
    }
}

