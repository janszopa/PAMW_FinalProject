using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain;
using TaskManagement.Domain.DTO;
using TaskManagement.API.Services;
using TaskManagement.Domain.Services;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _personService.GetAllPersonsAsync();
        if (!response.Success)
            return BadRequest(response.Message);

        return Ok(response.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _personService.GetPersonByIdAsync(id);
        if (!response.Success)
            return NotFound(response.Message);

        return Ok(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonDto personDto)
    {
        var response = await _personService.CreatePersonAsync(personDto);
        if (!response.Success)
            return BadRequest(response.Message);

        return CreatedAtAction(nameof(GetById), new { id = response.Data }, personDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePersonDto personDto)
    {
        var response = await _personService.UpdatePersonAsync(id, personDto);
        if (!response.Success)
            return NotFound(response.Message);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _personService.DeletePersonAsync(id);
        if (!response.Success)
            return NotFound(response.Message);

        return NoContent();
    }
}
