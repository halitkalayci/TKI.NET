using Core.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class Rental : BaseEntity<int>
    {
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int RentalStartLocationId { get; set; }
        //TODO: Cascade
        //public virtual RentalLocation RentalStartLocation { get; set; }
        public int RentalEndLocationId { get; set; }
        //public virtual RentalLocation RentalEndLocation { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
