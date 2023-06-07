using AutoMapper;
using Business.Abstracts;
using Business.ValidationResolvers.Car;
using Core.Exceptions.Types;
using Core.Utilities.Result;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.DTOs;
using Entities.DTOs.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Business.Concretes
{
    // Autofac
    // AOP => Aspect Oriented Programming
    public class CarManager : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarManager(ICarRepository carRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public Core.Utilities.Result.IResult Add(CarForAddDto carForAddDto)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                throw new AuthorizeException();
            AddCarDtoValidator validator = new AddCarDtoValidator();
            var validationResult = validator.Validate(carForAddDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(i=>i.ErrorMessage).ToList(),"Validasyon hatası.");
            // [ {ErrorMessage:"deneme 1", Code:400}, {ErrorMessage:"deneme", Code:400}, {ErrorMessage:"deneme 3", Code:400} ] 
            // ["deneme 1","deneme", "deneme 3"]
            #region Business Rules
            // Veritabanımda halihazırda mevcut bir plaka ile
            // istek gelirse bu istek kabul edilmesin.
            carWithSamePlateShouldNotExist(carForAddDto.Plate);
            #endregion
            #region Manual Mapping
            //Car car = new Car()
            //{
            //    Plate = carForAddDto.Plate,
            //    Kilometer = carForAddDto.Kilometer,
            //    IsAutomatic = carForAddDto.IsAutomatic,
            //    ColorId = carForAddDto.ColorId,
            //    ModelId = carForAddDto.ModelId,
            //    MinFindeksCreditRate = carForAddDto.MinFindeksCreditRate,
            //    ModelYear = carForAddDto.ModelYear,
            //    CreatedDate = DateTime.UtcNow,
            //    UpdatedDate = DateTime.UtcNow,
            //    DeletedDate = DateTime.UtcNow,
            //};
            #endregion
            #region Auto Mapping
            Car car = _mapper.Map<Car>(carForAddDto);
            #endregion
            _carRepository.Add(car);
            return new SuccessResult("Araba başarıyla eklendi.");
        }

        public void Delete(int id)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                throw new AuthorizeException();
            // Verilen id ile veritabanına bir eşleşme olması.
            carWithIdShouldExist(id);
            Car carToDelete = _carRepository.Get(i => i.Id == id);
            _carRepository.Delete(carToDelete);
        }


        public IDataResult<List<CarForListingDto>> GetAll()
        {
            // Kullanıcı giriş yapmış mı?
            var isUserLoggedIn = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            if (!isUserLoggedIn)
                throw new Exception("Giriş yapılmadı.");

            List<Car> cars = _carRepository.GetAll(include: i => i.Include(i => i.Color).Include(i => i.Model).ThenInclude(i => i.Brand));
            #region Manual Mapping
            //List<CarForListingDto> dtos = cars.Select(car => new CarForListingDto()
            //{
            //    Id = car.Id,
            //    Plate = car.Plate,
            //    Kilometer = car.Kilometer,
            //    MinFindeksCreditRate = car.MinFindeksCreditRate,
            //    ModelYear = car.ModelYear,
            //    Color = new ColorForListingDto()
            //    {
            //        Id = car.Color.Id,
            //        Name = car.Color.Name,
            //    }
            //}).ToList();
            #endregion
            #region Auto Mapping
            List<CarForListingDto> dtos = _mapper.Map<List<CarForListingDto>>(cars);
            #endregion
            return new SuccessDataResult<List<CarForListingDto>>(dtos, "Arabalar listelendi");
        }

        public Car GetById(int id)
        {
            //TODO: DTO Implementation.
            return _carRepository
                .Get(filter: i => i.Id == id, include: i => i.Include(i => i.Color));
        }

        public void Update(CarForUpdateDto carForUpdateDto)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                throw new AuthorizeException();
            carWithIdShouldExist(carForUpdateDto.Id);
            //TODO => Plaka değiştiyse plakayı kontrol et
            carWithSamePlateShouldNotExist(carForUpdateDto.Plate);

            Car carToUpdate = _carRepository.Get(i => i.Id == carForUpdateDto.Id);
            carToUpdate = _mapper.Map<Car>(carForUpdateDto);

            _carRepository.Update(carToUpdate);
        }

        #region Business Rules

        private void carWithSamePlateShouldNotExist(string plate)
        {
            Car carWithSamePlate = _carRepository.Get(i => i.Plate == plate);
            if (carWithSamePlate != null)
            {
                throw new Exception("Bu plaka ile bir araç zaten mevcut.");
            }
        }

        private void carWithIdShouldExist(int id)
        {
            Car carToDelete = _carRepository.Get(i => i.Id == id);
            if (carToDelete == null)
                throw new BusinessException("Bu id ile bir araç bulunamadı.");
        }
        #endregion
    }
}
