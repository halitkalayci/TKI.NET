using Business.Abstracts;
using Entities.DTOs.CarImage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private readonly ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] CarImageForAddDto carImageForAddDto)
        {
            return Ok(_carImageService.Add(carImageForAddDto));
        }
    }
}
