using Dogshouseservice.API.Models.Domain;

namespace Dogshouseservice.API.Repositories.Interfaces
{
    public interface IDogRepository
    {
        Task<List<Dog>> GetAllAsync(
             string? attribute = null, string order = "asc",
             int pageNumber = 1, int pageSize = 1000
             );

        Task<Dog> CreateAsync(Dog dog);

        Task<bool> IsDogNameAlreadyTaken(string name);
    }
}