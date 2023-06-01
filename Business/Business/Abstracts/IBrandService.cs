using Entities.Concretes;
using Entities.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IBrandService
    {
        // DTO
        List<BrandForListingDto> GetAll();
        Brand GetById(int id);
        void Add(BrandForAddDto brandForAddDto);
        void Update(BrandForUpdateDto brandForUpdateDto);
        void Delete(int id);
    }
}
