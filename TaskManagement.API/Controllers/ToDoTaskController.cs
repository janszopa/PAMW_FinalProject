using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain;
using TaskManagement.Domain.DTO;
using TaskManagement.API.Services;
using TaskManagement.Domain.Services;

namespace TaskManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoTaskController : ControllerBase
{
    private readonly IToDoTaskService _toDoTaskService;

    public ToDoTaskController(IToDoTaskService taskService)
    {
        _toDoTaskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _toDoTaskService.GetAllTasksAsync();
        if (!response.Success)
            return BadRequest(response.Message);

        return Ok(response.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _toDoTaskService.GetTaskByIdAsync(id);
        if (!response.Success)
            return NotFound(response.Message);

        return Ok(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateToDoTaskDto toDoTaskDto)
    {
        var response = await _toDoTaskService.CreateTaskAsync(toDoTaskDto);
        if (!response.Success)
            return BadRequest(response.Message);

        return CreatedAtAction(nameof(GetById), new { id = response.Data }, toDoTaskDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateToDoTaskDto toDoTaskDto)
    {
        var response = await _toDoTaskService.UpdateTaskAsync(id, toDoTaskDto);
        if (!response.Success)
            return NotFound(response.Message);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _toDoTaskService.DeleteTaskAsync(id);
        if (!response.Success)
            return NotFound(response.Message);

        return NoContent();
    }
}
