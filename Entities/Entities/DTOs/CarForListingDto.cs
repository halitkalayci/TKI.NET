using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CarForListingDto
    {
        public int Id { get; set; }
        public int ModelYear { get; set; }
        public string Plate { get; set; }
        public int Kilometer { get; set; }
        public int MinFindeksCreditRate { get; set; }
        public string ColorName { get; set; }
    }
}
