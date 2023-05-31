using Business.Abstracts;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concretes
{
    public class CarManager : ICarService
    {
        // getter-only => sadece constructor içinde değiştirilebilir.
        private readonly ICarRepository _carRepository;
        // Bir manager kendisiyle ilgili entity dışındaki 
        // entitylerin repositorylerini enjekte edemez
        private readonly IBrandService _brandService;


        //private readonly IBrandRepository _brandRepository;

        // DI => Dependency Injection
        // IoC Container => Inversion Of Control
        // constructor
        // method overloading
        public CarManager(ICarRepository carRepository, IBrandService brandService)
        {
            _carRepository = carRepository;
            _brandService = brandService;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            throw new NotImplementedException();
        }

        public Car GetById(int id)
        {
            throw new NotImplementedException();
        }
        public void Add(Car car)
        {
            // CCC => Cross Cutting Concerns
            // Validation
            // Business Rules 
            // Aynı plakadan araç varsa bunu ekleme!
            carShouldNotExistWithPlate(car.Plate);
            _carRepository.Add(car);
            Console.WriteLine("Araba eklendi.");
        }


        public void Update(Car car)
        {
            carShouldNotExistWithPlate(car.Plate);
            throw new NotImplementedException();
        }
        // Global Exception Handling
        // BusinessException(string message)
        // message
        private void carShouldNotExistWithPlate(string plate)
        {
            if (_carRepository.GetByPlate(plate) != null)
            {
                Console.WriteLine("Bu plaka önceden eklenmiş");
                //throw new Exception();
            }
        }

        private void brandShouldHaveLessThanFiveCars(int brandId)
        {
            Brand brand = _brandService.GetById(brandId);
        }
    }
}
