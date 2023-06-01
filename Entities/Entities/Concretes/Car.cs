using Core.Entities.Abstracts;
using Core.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    // Code-First => Tablolarını ilk olarak kodla (modelle) 
    // DB-First => Tablolarını veritabanında oluştur

    // migration => oluşturulan kodlardan veritabanının güncellenmesi/oluşturulması
    // seed-data => Örnek verilerin db oluşturulurken oluşturulmasını sağlar.

    // OOP Concepts => Daha kolay ve okunabilir kod yazmamız için.
    // Inheritance (Miras,Kalıtım)
    // class ya da interface
    // 1 adet class, n adet interface
    public class Car : BaseEntity<int>
    {
        // constructor yoksa default parametresiz const. oluşturulur
        // bir adet bile ctor eklenirse default durum bozulur, boş ctor'ın manual eklenmesi gerekir.

        // NoArgsConstructor
        // AllArgsConstructor

        // ORM => Object Relation Mapping

        public Car()
        {
            Rentals = new HashSet<Rental>();
        }

        public string Plate { get; set; }
        public int Kilometer { get; set; }
        public int ModelYear { get; set; }
        public int MinFindeksCreditRate { get; set; }
        public bool IsAutomatic { get; set; }
        public int ColorId { get; set; }
        public int ModelId { get; set; }
        public virtual Color Color { get; set; }
    
        public virtual Model Model { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}
