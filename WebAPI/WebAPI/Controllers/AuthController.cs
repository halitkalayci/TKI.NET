using Entities.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    // www.api.tki.com/api/auth HTTP GET REQUEST
    // 
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET => Kaynaktan okuma
        // POST => Kaynakta veri oluşturma
        // PUT => Kaynakta veri değiştirme
        // DELETE => Kaynakta veri silme


        [HttpGet("{name}/{surname}")]
        // Query String, Path Variable
        // https://localhost:44303/api/Auth?name=halit&surname=kalayci
        // https://localhost:44303/api/Auth/halit/kalayci
        public string Example([FromRoute(Name = "name")] string name, [FromRoute] string surname)
        {
            return "Merhaba " + name + " " + surname;
        }

        [HttpPost]
        // Araba eklerken isteyeceğim veri seti
        public string Example2()
        {
            return "POST Request atıldı.";
        }

        [HttpPut]
        public string Example3()
        {
            return "PUT Request atıldı.";
        }


        [HttpDelete]
        public string Example4()
        {
            return "Delete Request atıldı.";
        }
    }
}
