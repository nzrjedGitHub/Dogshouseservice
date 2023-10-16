using Dogshouseservice.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dogshouseservice.API.Data
{
    public class DogsHouseServiceDbContext : DbContext
    {
        public DogsHouseServiceDbContext(DbContextOptions<DogsHouseServiceDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Dog> Dogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<Dog>().HasData(dogs);
        }
    }
}