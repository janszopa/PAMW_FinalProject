using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain;
using TaskManagement.Domain.DTO;
using TaskManagement.API.Services;
using TaskManagement.Domain.Services;

namespace TaskManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _categoryService.GetAllCategoriesAsync();
        if (!response.Success)
            return BadRequest(response.Message);

        return Ok(response.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _categoryService.GetCategoryByIdAsync(id);
        if (!response.Success)
            return NotFound(response.Message);

        return Ok(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto categoryDto)
    {
        var response = await _categoryService.CreateCategoryAsync(categoryDto);
        if (!response.Success)
            return BadRequest(response.Message);

        return CreatedAtAction(nameof(GetById), new { id = response.Data }, categoryDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto categoryDto)
    {
        var response = await _categoryService.UpdateCategoryAsync(id, categoryDto);
        if (!response.Success)
            return NotFound(response.Message);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _categoryService.DeleteCategoryAsync(id);
        if (!response.Success)
            return NotFound(response.Message);

        return NoContent();
    }
}
