using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CarForAddDto
    {
        public string Plate { get; set; }
        public int Kilometer { get; set; }
        public int ModelYear { get; set; }
        public int MinFindeksCreditRate { get; set; }
        public bool IsAutomatic { get; set; }
        public int ColorId { get; set; }
        public int ModelId { get; set; }
    }
}
