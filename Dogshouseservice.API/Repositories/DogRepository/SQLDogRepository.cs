using Dogshouseservice.API.Data;
using Dogshouseservice.API.Models.Domain;
using Dogshouseservice.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dogshouseservice.API.Repositories.DogRepository
{
    public class SQLDogRepository : IDogRepository
    {
        private readonly DogsHouseServiceDbContext _ctx;

        public SQLDogRepository(DogsHouseServiceDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Dog> CreateAsync(Dog dog)
        {
            await _ctx.Dogs.AddAsync(dog);
            await _ctx.SaveChangesAsync();
            return dog;
        }

        public async Task<List<Dog>> GetAllAsync(
            string? attribute = null, string order = "asc",
            int pageNumber = 1, int pageSize = 1000)
        {
            var dogs = _ctx.Dogs.AsQueryable();

            if (!string.IsNullOrEmpty(attribute))
            {
                if (attribute.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    dogs = order.Equals("desc", StringComparison.OrdinalIgnoreCase) ? dogs.OrderByDescending(d => d.Name) : dogs.OrderBy(d => d.Name);
                }
                if (attribute.Equals("color", StringComparison.OrdinalIgnoreCase))
                {
                    dogs = order.Equals("desc", StringComparison.OrdinalIgnoreCase) ? dogs.OrderByDescending(d => d.Color) : dogs.OrderBy(d => d.Color);
                }
                if (attribute.Equals("tail_length", StringComparison.OrdinalIgnoreCase))
                {
                    dogs = order.Equals("desc", StringComparison.OrdinalIgnoreCase) ? dogs.OrderByDescending(d => d.Tail_length) : dogs.OrderBy(d => d.Tail_length);
                }
                if (attribute.Equals("weight", StringComparison.OrdinalIgnoreCase))
                {
                    dogs = order.Equals("desc", StringComparison.OrdinalIgnoreCase) ? dogs.OrderByDescending(d => d.Weight) : dogs.OrderBy(d => d.Weight);
                }
            }
            var skipResults = (pageNumber - 1) * pageSize;
            return await dogs.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<bool> IsDogNameAlreadyTaken(string name)
        {
            return await _ctx.Dogs.AnyAsync(dog => dog.Name == name);
        }
    }
}