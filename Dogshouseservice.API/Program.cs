using Dogshouseservice.API.Data;
using Dogshouseservice.API.Mappings;
using Dogshouseservice.API.Repositories.DogRepository;
using Dogshouseservice.API.Repositories.Interfaces;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FixedWindowPolicy", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(1);
        opt.PermitLimit = 2; // I set 2 instead of 10 for easier testing to see if it works
    }).RejectionStatusCode = 429;
});

builder.Services.AddDbContext<DogsHouseServiceDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DogsHouseServiceConnectionString"));
});

builder.Services.AddScoped<IDogRepository, SQLDogRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();