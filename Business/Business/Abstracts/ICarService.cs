using Entities.Concretes;
using Entities.DTOs;

namespace Business.Abstracts
{
    public interface ICarService
    {
        List<CarForListingDto> GetAll();
        Car GetById(int id);
        void Add(CarForAddDto car);
        void Update(CarForUpdateDto car);
        void Delete(int id);
    }
}
