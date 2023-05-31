using Core.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class RentalLocation : BaseEntity<int>
    {
        public RentalLocation()
        {
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
