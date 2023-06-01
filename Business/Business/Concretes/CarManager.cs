using Business.Abstracts;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.DTOs;

namespace Business.Concretes
{
    public class CarManager : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarManager(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public void Add(CarForAddDto carForAddDto)
        {
            #region Business Rules
            // Veritabanımda halihazırda mevcut bir plaka ile
            // istek gelirse bu istek kabul edilmesin.
            carWithSamePlateShouldNotExist(carForAddDto.Plate);
            #endregion
            #region Manual Mapping
            Car car = new Car()
            {
                Plate = carForAddDto.Plate,
                Kilometer = carForAddDto.Kilometer,
                IsAutomatic = carForAddDto.IsAutomatic,
                ColorId = carForAddDto.ColorId,
                ModelId = carForAddDto.ModelId,
                MinFindeksCreditRate = carForAddDto.MinFindeksCreditRate,
                ModelYear = carForAddDto.ModelYear,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                DeletedDate = DateTime.UtcNow,
            };
            #endregion
            _carRepository.Add(car);
        }

        public void Delete(int id)
        {
            // Verilen id ile veritabanına bir eşleşme olması.
            carWithIdShouldExist(id);
            _carRepository.Delete(id);
        }


        public List<CarForListingDto> GetAll()
        {
            List<Car> cars = _carRepository.GetAll();
            List<CarForListingDto> dtos = cars.Select(car => new CarForListingDto()
            {
                Id = car.Id,
                Plate = car.Plate,
                Kilometer = car.Kilometer,
                MinFindeksCreditRate = car.MinFindeksCreditRate,
                ModelYear = car.ModelYear,
                Color = new ColorForListingDto()
                {
                    Id = car.Color.Id,
                    Name = car.Color.Name,
                }
            }).ToList();
            return dtos;
        }

        public Car GetById(int id)
        {
            //TODO: DTO Implementation.
            return _carRepository.GetById(id);
        }

        public void Update(CarForUpdateDto carForUpdateDto)
        {
            carWithIdShouldExist(carForUpdateDto.Id);
            //TODO => Plaka değiştiyse plakayı kontrol et
            carWithSamePlateShouldNotExist(carForUpdateDto.Plate);

            Car carToUpdate = _carRepository.GetById(carForUpdateDto.Id);
            carToUpdate.Plate = carForUpdateDto.Plate;
            carToUpdate.Kilometer = carForUpdateDto.Kilometer;
            carToUpdate.ColorId = carForUpdateDto.ColorId;
            carToUpdate.IsAutomatic = carForUpdateDto.IsAutomatic;
            carToUpdate.ModelId = carForUpdateDto.ModelId;
            carToUpdate.MinFindeksCreditRate = carForUpdateDto.MinFindeksCreditRate;
            carToUpdate.ModelYear = carForUpdateDto.ModelYear;
            carToUpdate.UpdatedDate = DateTime.UtcNow;

            _carRepository.Update(carToUpdate);
        }

        #region Business Rules

        private void carWithSamePlateShouldNotExist(string plate)
        {
            Car carWithSamePlate = _carRepository.GetByPlate(plate);
            if (carWithSamePlate != null)
            {
                throw new Exception("Bu plaka ile bir araç zaten mevcut.");
            }
        }

        private void carWithIdShouldExist(int id)
        {
            Car carToDelete = _carRepository.GetById(id);
            if (carToDelete == null)
                throw new Exception("Bu id ile bir araç bulunamadı.");
        }
        #endregion
    }
}
