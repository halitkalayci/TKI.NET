using Business.Abstracts;
using Core.Exceptions.Types;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandManager(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public void Add(BrandForAddDto brandForAddDto)
        {
            // Mapping
            brandWithSameNameShouldNotExist(brandForAddDto.Name);

            Brand brand = new Brand()
            {
                Name = brandForAddDto.Name,
                LogoUrl = brandForAddDto.LogoUrl,
                CreatedDate = DateTime.UtcNow,
                DeletedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };
            _brandRepository.Add(brand);
        }

        private void brandWithSameNameShouldNotExist(string name)
        {
            // İsme göre sorgulama ihtiyacı
            Brand brandWithSameName = _brandRepository.Get(i=>i.Name == name);
            if (brandWithSameName != null)
                throw new BusinessException("Bu isimle bir marka zaten  var.");
        }

        public void Delete(int id)
        {
            Brand brandToDelete = _brandRepository.Get(brand=>brand.Id == id);
            brandShouldNotBeNull(brandToDelete);
            _brandRepository.Delete(brandToDelete);
        }

        public List<BrandForListingDto> GetAll()
        {
            List<Brand> brands = _brandRepository.GetAll();
            List<BrandForListingDto> dtos = brands.Select(i => new BrandForListingDto()
            {
                Id=i.Id,
                Name=i.Name,
                LogoUrl=i.LogoUrl,
            }).ToList();
            return dtos;
        }

        public Brand GetById(int id)
        {
            return _brandRepository.Get(i=>i.Id==id);
        }

        public void Update(BrandForUpdateDto brandForUpdateDto)
        {

            Brand brand = _brandRepository.Get(i=>i.Id == brandForUpdateDto.Id);
            brandShouldNotBeNull(brand);
            brandWithSameNameShouldNotExist(brandForUpdateDto.Name);

            brand.Name = brandForUpdateDto.Name;
            brand.LogoUrl = brandForUpdateDto.LogoUrl;
            brand.UpdatedDate = DateTime.UtcNow;

            _brandRepository.Update(brand);
        }

        private static void brandShouldNotBeNull(Brand brand)
        {
            if (brand == null)
                throw new BusinessException("Bu id ile bir marka bulunamadı.");
        }
    }
}
