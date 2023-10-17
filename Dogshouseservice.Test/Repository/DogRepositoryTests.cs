using Dogshouseservice.API.Data;
using Dogshouseservice.API.Models.Domain;
using Dogshouseservice.API.Repositories.DogRepository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Dogshouseservice.Test.Repository
{
    public class DogRepositoryTests
    {
        private async Task<DogsHouseServiceDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DogsHouseServiceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new DogsHouseServiceDbContext(options);
            dbContext.Database.EnsureCreated();
            if (!await dbContext.Dogs.AnyAsync())
            {
                var dogs = new List<Dog>()
            {
                new Dog
                {
                    Id = Guid.Parse("e16b336b-0797-47e2-ba77-04e7aef98f35"),
                    Name = "Neo",
                    Color = "red&amber",
                    Tail_length = 22,
                    Weight = 32
                },
                new Dog
                {
                    Id = Guid.Parse("32369c97-b9a1-4810-8b64-1ab5922d8cd7"),
                    Name = "Jessy",
                    Color = "black&white",
                    Tail_length = 7,
                    Weight = 14
                },
                new Dog
                {
                    Id = Guid.Parse("54c986fb-0d40-4e99-b6c7-a7134f55f081"),
                    Name = "Tom",
                    Color = "brown&white",
                    Tail_length = 12,
                    Weight = 41
                },
                new Dog
                {
                    Id = Guid.Parse("284b3048-3da7-477f-93df-44ec48951399"),
                    Name = "Jerry",
                    Color = "grey",
                    Tail_length = 12,
                    Weight = 15
                },
            };

                await dbContext.AddRangeAsync(dogs);
                await dbContext.SaveChangesAsync();
            }
            return dbContext;
        }

        [Fact]
        public async void DogRepository_GetAllAsync_ReturnsDogs()
        {
            var dbContext = await GetDbContext();
            var dogRepository = new SQLDogRepository(dbContext);

            var result = await dogRepository.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().BeOfType<List<Dog>>();
            result.Count.Should().Be(4);
        }

        [Fact]
        public async Task DogRepository_IsDogNameAlreadyTaken_ReturnsTrueForExistingName()
        {
            var dbContext = await GetDbContext();
            var dogRepository = new SQLDogRepository(dbContext);

            string existingName = "Neo";

            var result = await dogRepository.IsDogNameAlreadyTaken(existingName);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DogRepository_IsDogNameAlreadyTaken_ReturnsFalseForNonExistingName()
        {
            var dbContext = await GetDbContext();
            var dogRepository = new SQLDogRepository(dbContext);

            string nonExistingName = "Oen";

            var result = await dogRepository.IsDogNameAlreadyTaken(nonExistingName);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DogRepository_CreateAsync_AddsAndReturnsNewDog()
        {
            var dbContext = await GetDbContext();
            var dogRepository = new SQLDogRepository(dbContext);

            var newDog = new Dog
            {
                Name = "Doggy",
                Color = "red",
                Tail_length = 173,
                Weight = 33
            };

            var result = await dogRepository.CreateAsync(newDog);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dog>();
            result.Id.Should().NotBeEmpty();

            var addedDog = await dbContext.Dogs.FirstOrDefaultAsync(d => d.Id == result.Id);
            addedDog.Should().NotBeNull();
            addedDog.Name.Should().Be("Doggy");
            addedDog.Color.Should().Be("red");
            addedDog.Tail_length.Should().Be(173);
            addedDog.Weight.Should().Be(33);
        }
    }
}