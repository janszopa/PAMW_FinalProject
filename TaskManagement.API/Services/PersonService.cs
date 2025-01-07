using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain;
using TaskManagement.Domain.Models;
using TaskManagement.API.Data;
using TaskManagement.Domain.DTO;
using TaskManagement.Domain.Services;

namespace TaskManagement.API.Services;

public class PersonService : IPersonService
{
    private readonly TaskManagementDbContext _context;

    public PersonService(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceReponse<List<PersonDto>>> GetAllPersonsAsync()
    {
        var response = new ServiceReponse<List<PersonDto>>();
        try
        {
            var persons = await _context.Persons
                .Select(p => new PersonDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                })
                .ToListAsync();

            response.Data = persons;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error retrieving persons: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceReponse<PersonDto>> GetPersonByIdAsync(int id)
    {
        var response = new ServiceReponse<PersonDto>();
        try
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                response.Success = false;
                response.Message = $"Person with id {id} not found.";
                return response;
            }

            response.Data = new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName
            };
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error retrieving person: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceReponse<int>> CreatePersonAsync(CreatePersonDto personDto)
    {
        var response = new ServiceReponse<int>();
        try
        {
            var person = new Person
            {
                FirstName = personDto.FirstName,
                LastName = personDto.LastName
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            response.Data = person.Id;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error creating person: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceReponse<bool>> UpdatePersonAsync(int id, UpdatePersonDto personDto)
    {
        var response = new ServiceReponse<bool>();
        try
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                response.Success = false;
                response.Message = $"Person with id {id} not found.";
                return response;
            }

            person.FirstName = personDto.FirstName;
            person.LastName = personDto.LastName;

            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
            response.Data = true;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error updating person: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceReponse<bool>> DeletePersonAsync(int id)
    {
        var response = new ServiceReponse<bool>();
        try
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                response.Success = false;
                response.Message = $"Person with id {id} not found.";
                return response;
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            response.Data = true;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error deleting person: {ex.Message}";
        }
        return response;
    }
}
