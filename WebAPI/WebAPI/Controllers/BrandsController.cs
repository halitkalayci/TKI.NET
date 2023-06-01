using Business.Abstracts;
using Entities.DTOs.Brand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_brandService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_brandService.GetById(id));
        }
        [HttpPost]
        public IActionResult Add([FromBody] BrandForAddDto brandForAddDto)
        {
            _brandService.Add(brandForAddDto);
            return Created("","Marka eklendi");
        }
        [HttpPut]
        public IActionResult Update([FromBody] BrandForUpdateDto brandForUpdateDto)
        {
            _brandService.Update(brandForUpdateDto);
            return Ok("Marka güncellendi");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _brandService.Delete(id);
            return Ok("Marka silindi");
        }
    }
}
