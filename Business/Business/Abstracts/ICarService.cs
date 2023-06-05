using Core.Utilities.Result;
using Entities.Concretes;
using Entities.DTOs.Car;

namespace Business.Abstracts
{
    public interface ICarService
    {
        List<CarForListingDto> GetAll();
        Car GetById(int id);
        IResult Add(CarForAddDto car);
        void Update(CarForUpdateDto car);
        void Delete(int id);
    }
}
