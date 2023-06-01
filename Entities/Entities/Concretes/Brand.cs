using Core.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class Brand : BaseEntity<int>
    {
        public Brand()
        {
            Models = new HashSet<Model>();
        }
        // ? => Nullable
        public string Name { get; set; }
        // Dosya olarak al, upload
        public string LogoUrl { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
