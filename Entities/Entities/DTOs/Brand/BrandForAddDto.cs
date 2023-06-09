using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Brand
{
    public class BrandForAddDto
    {
        public string Name { get; set; }
        public string LogoBase64 { get; set; }
    }
}
