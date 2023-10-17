using AutoMapper;
using Dogshouseservice.API.CustomActionFilters;
using Dogshouseservice.API.Models.Domain;
using Dogshouseservice.API.Models.DTO;
using Dogshouseservice.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Dogshouseservice.API.Controllers
{
    [Route("api")]
    [ApiController]
    [EnableRateLimiting("FixedWindowPolicy")]
    public class DogsController : ControllerBase
    {
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;

        public DogsController(IDogRepository dogRepository, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _mapper = mapper;
        }

        [HttpGet("dogs")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? attribute, [FromQuery] string? order,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var dogs = await _dogRepository.GetAllAsync(
                attribute, order ?? "asc",
                pageNumber, pageSize);
            var dogsDto = _mapper.Map<List<DogDto>>(dogs);
            return Ok(dogsDto);
        }

        [HttpPost("dog")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddDogRequestDto requestDto)
        {
            if (await _dogRepository.IsDogNameAlreadyTaken(requestDto.Name))
            {
                return Conflict("This dog name already taken");
            }
            var dog = _mapper.Map<Dog>(requestDto);
            await _dogRepository.CreateAsync(dog);

            var dogDto = _mapper.Map<DogDto>(dog);
            return Ok(dogDto);
        }
    }
}