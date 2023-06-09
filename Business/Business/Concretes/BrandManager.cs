using Business.Abstracts;
using Business.ValidationResolvers.Brand;
using Core.Aspects.Autofac;
using Core.Aspects.Autofac.Authentication;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Exceptions.Types;
using Core.Utilities.Helpers;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        [CacheRemove("IBrandService.*")]
        [Authentication("Brand.Add,Admin")]
        [Validation(typeof(BrandForAddDtoValidator))]
        public void Add(BrandForAddDto brandForAddDto)
        {
            brandWithSameNameShouldNotExist(brandForAddDto.Name);
            var fileName = FileHelper.UploadFromBase64(brandForAddDto.LogoBase64);
            Brand brand = new Brand()
            {
                Name = brandForAddDto.Name,
                LogoUrl = fileName,
                CreatedDate = DateTime.UtcNow,
                DeletedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };
            _brandRepository.Add(brand);
        }

        [Authentication("Brand.Update,Admin")]
        [CacheRemove("IBrandService.*")]
        [Validation(typeof(BrandForUpdateDtoValidator))]
        public void Update(BrandForUpdateDto brandForUpdateDto)
        {

            Brand brand = _brandRepository.Get(i => i.Id == brandForUpdateDto.Id);
            brandShouldNotBeNull(brand);
            brandWithSameNameShouldNotExist(brandForUpdateDto.Name);

            brand.Name = brandForUpdateDto.Name;
            brand.LogoUrl = brandForUpdateDto.LogoUrl;
            brand.UpdatedDate = DateTime.UtcNow;

            _brandRepository.Update(brand);
        }


        private void brandWithSameNameShouldNotExist(string name)
        {
            // İsme göre sorgulama ihtiyacı
            Brand brandWithSameName = _brandRepository.Get(i=>i.Name == name);
            if (brandWithSameName != null)
                throw new BusinessException("Bu isimle bir marka zaten  var.");
        }



        [Authentication("Brand.Delete,Admin")]
        [CacheRemove("IBrandService.*")]
        public void Delete(int id)
        {
            Brand brandToDelete = _brandRepository.Get(brand=>brand.Id == id);
            brandShouldNotBeNull(brandToDelete);
            _brandRepository.Delete(brandToDelete);
        }

        [Performance(1)]
        [Cache]
        [Authentication]
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

        [Cache]
        public Brand GetById(int id)
        {
            return _brandRepository.Get(i=>i.Id==id);
        }
       
        private static void brandShouldNotBeNull(Brand brand)
        {
            if (brand == null)
                throw new BusinessException("Bu id ile bir marka bulunamadı.");
        }
    }
}
