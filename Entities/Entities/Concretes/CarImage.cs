using Core.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class CarImage : BaseEntity<int>
    {
        public string ImagePath { get; set; }
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}
