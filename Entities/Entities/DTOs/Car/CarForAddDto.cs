using System.ComponentModel.DataAnnotations;

// SOLID => 5 adet prensip
// S => Single Responsibility Principle
// O => Open-Closed Principle
// L => Liskov Substition Principle
// I => Interface Segregation Principle
// D => Dependency Inversion Principle


// FluentValidation
namespace Entities.DTOs.Car
{
    public class CarForAddDto
    {
        public string Plate { get; set; }
        public int Kilometer { get; set; }
        public int ModelYear { get; set; }
        public int MinFindeksCreditRate { get; set; }
        public bool IsAutomatic { get; set; }
        public int ColorId { get; set; }
        public int ModelId { get; set; }

        
    }
}
