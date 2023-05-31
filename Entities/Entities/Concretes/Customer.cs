using Core.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class Customer : BaseEntity<int>
    {
        public Customer()
        {
            Rentals = new HashSet<Rental>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalityId { get; set; }
        public int FindeksCreditRate { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
