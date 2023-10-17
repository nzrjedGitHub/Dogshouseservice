using AutoMapper;
using Dogshouseservice.API.Controllers;
using Dogshouseservice.API.Models.Domain;
using Dogshouseservice.API.Models.DTO;
using Dogshouseservice.API.Repositories.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Dogshouseservice.Test.Controller
{
    public class DogsControllerTests
    {
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;

        public DogsControllerTests()
        {
            _dogRepository = A.Fake<IDogRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async void DogController_GetAll_ReturnsOkObjectResult()
        {
            var dogs = A.Fake<List<Dog>>();
            var dogsDto = A.Fake<List<DogDto>>();
            A.CallTo(() => _mapper.Map<List<DogDto>>(dogs)).Returns(dogsDto);
            var controller = new DogsController(_dogRepository, _mapper);

            var result = await controller.GetAll("name", "asc");

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void DogController_Create_ReturnsOkObjectResult()
        {
            var dogAddDto = A.Fake<AddDogRequestDto>();
            var dog = A.Fake<Dog>();
            var isDogNameAlreadyTaken = Task.FromResult(false);
            var controller = new DogsController(_dogRepository, _mapper);
            A.CallTo(() => _dogRepository.IsDogNameAlreadyTaken(dogAddDto.Name)).Returns(isDogNameAlreadyTaken);
            A.CallTo(() => _mapper.Map<Dog>(dogAddDto)).Returns(dog);
            A.CallTo(() => _dogRepository.CreateAsync(dog)).Returns(dog);

            var result = await controller.Create(dogAddDto);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void DogController_Create_ReturnsConflictObjectResult()
        {
            var dogAddDto = A.Fake<AddDogRequestDto>();
            var dog = A.Fake<Dog>();
            var isDogNameAlreadyTaken = Task.FromResult(true);
            var controller = new DogsController(_dogRepository, _mapper);
            A.CallTo(() => _dogRepository.IsDogNameAlreadyTaken(dogAddDto.Name)).Returns(isDogNameAlreadyTaken);
            A.CallTo(() => _mapper.Map<Dog>(dogAddDto)).Returns(dog);
            A.CallTo(() => _dogRepository.CreateAsync(dog)).Returns(dog);

            var result = await controller.Create(dogAddDto);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ConflictObjectResult));
        }
    }
}