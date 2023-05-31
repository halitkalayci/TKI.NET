using Core.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class Color : BaseEntity<int>
    {
        public Color()
        {
            Cars = new HashSet<Car>();
        }
        public string Name { get; set; }

        // KÖTÜ BİR ÇÖZÜM!!
        [JsonIgnore]
        public virtual ICollection<Car> Cars { get; set; }
    }
}
