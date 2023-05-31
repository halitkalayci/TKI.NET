using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        // IoC => DI Container
        // DTO => Data Transfer Object
        // CarForListing => DTO => ColorForListing
        // Request-Response Pattern

        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Response Status Codes
            //return StatusCode(401, "Merhaba");
            List<Car> cars = _carRepository.GetAll();
            List<CarForListingDto> dtos = cars.Select(car => new CarForListingDto()
            {
                Id = car.Id,
                Plate = car.Plate,
                Kilometer = car.Kilometer,
                MinFindeksCreditRate = car.MinFindeksCreditRate,
                ModelYear = car.ModelYear,
                ColorName = car.Color.Name
            }).ToList();
            return Ok(dtos);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CarForAddDto carForAddDto)
        {
            // Manual Mapleme
            Car car = new Car()
            {
                Plate = carForAddDto.Plate,
                Kilometer = carForAddDto.Kilometer,
                IsAutomatic = carForAddDto.IsAutomatic,
                ColorId = carForAddDto.ColorId,
                ModelId = carForAddDto.ModelId,
                MinFindeksCreditRate= carForAddDto.MinFindeksCreditRate,
                ModelYear= carForAddDto.ModelYear,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                DeletedDate = DateTime.UtcNow,
            };
            _carRepository.Add(car);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] CarForUpdateDto carForUpdateDto)
        {
            Car carToUpdate = _carRepository.GetById(carForUpdateDto.Id);
            if (carToUpdate == null)
                return BadRequest("Böyle bir araba bulunamadı.");

            carToUpdate.Plate = carForUpdateDto.Plate;
            carToUpdate.Kilometer = carForUpdateDto.Kilometer;
            carToUpdate.ColorId = carForUpdateDto.ColorId;
            carToUpdate.IsAutomatic = carForUpdateDto.IsAutomatic;
            carToUpdate.ModelId = carForUpdateDto.ModelId;
            carToUpdate.MinFindeksCreditRate = carForUpdateDto.MinFindeksCreditRate;
            carToUpdate.ModelYear = carForUpdateDto.ModelYear;
            carToUpdate.UpdatedDate = DateTime.UtcNow;

            _carRepository.Update(carToUpdate);
            return Ok(carToUpdate);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]int id)
        {
            _carRepository.Delete(id);
            return Ok("Araba silindi.");
        }

        // Brand 
    }
}
