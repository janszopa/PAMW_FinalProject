using TaskManagement.Domain.DTO;
using TaskManagement.Domain;

namespace TaskManagement.Domain.Services;

public interface IPersonService
{
    Task<ServiceReponse<List<PersonDto>>> GetAllPersonsAsync();
    Task<ServiceReponse<PersonDto>> GetPersonByIdAsync(int id);
    Task<ServiceReponse<int>> CreatePersonAsync(CreatePersonDto personDto);
    Task<ServiceReponse<bool>> UpdatePersonAsync(int id, UpdatePersonDto personDto);
    Task<ServiceReponse<bool>> DeletePersonAsync(int id);
}

