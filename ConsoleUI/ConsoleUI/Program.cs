//using DataAccess.Abstracts;
//using DataAccess.Concretes.EntityFramework;
//using DataAccess.Concretes.InMemory;
//using Entities.Concretes;

//Console.WriteLine("Hello, World!");

//ICarRepository carRepository = new InMemoryCarRepository();

//Car car = new() { Id=1 };
//Car car2 = new() { Id=2 };


//carRepository.Add(car);

//carRepository.Add(car2);

//carRepository.Delete(1);
//List<Car> cars = carRepository.GetAll();

//foreach (Car carInfo in cars)
//{
//    Console.WriteLine(carInfo.Id);
//}



// constructing an instance
using Business.Abstracts;
using Business.Concretes;
using DataAccess.Concretes.EntityFramework;
using DataAccess.Concretes.InMemory;
using Entities.Concretes;

IBrandService brandService = new BrandManager(null);


//car.Plate = "34ABC06";
//Car car2 = new() { Id = 2 };
//car2.Plate = "34ABC06";
//carService.Add(car);
//carService.Add(car2);
// instance (örnek)
// constructing an instance
//Car car = new Car("34ABC06", 30000, 2023, "M5", true, 1);
//car.CreatedDate = DateTime.Now;

//Car car2 = new Car();

//Brand brand = new Brand();
//brand.CreatedDate = DateTime.Now;
