using Business.Abstracts;
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

        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Response Status Codes
            //return StatusCode(401, "Merhaba");
            return Ok(_carService.GetAll());
        }
        // /api/cars => GetAll
        // /api/cars/1 => GetById(1)
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_carService.GetById(id));
        }


        [HttpPost]
        public IActionResult Add([FromBody] CarForAddDto carForAddDto)
        {
            _carService.Add(carForAddDto);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] CarForUpdateDto carForUpdateDto)
        {
            _carService.Update(carForUpdateDto);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]int id)
        {
            _carService.Delete(id);
            return Ok("Araba silindi.");
        }

       
    }
}
