using Core.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class Model : BaseEntity<int>
    {
        public Model()
        {
            Cars = new HashSet<Car>();
        }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand{ get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
