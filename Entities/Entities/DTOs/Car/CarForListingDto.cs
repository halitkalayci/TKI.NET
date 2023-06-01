using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Car
{
    public class CarForListingDto
    {
        public int Id { get; set; }
        public int ModelYear { get; set; }
        public string Plate { get; set; }
        public int Kilometer { get; set; }
        public int MinFindeksCreditRate { get; set; }
        public ColorForListingDto Color { get; set; }
    }
}
