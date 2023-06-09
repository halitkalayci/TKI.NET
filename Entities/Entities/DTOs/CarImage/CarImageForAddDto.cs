using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CarImage
{
    public class CarImageForAddDto
    {
        public int CarId { get; set; }
        public string Base64 { get; set; }
    }
}
