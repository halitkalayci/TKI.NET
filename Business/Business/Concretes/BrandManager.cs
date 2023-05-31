using Business.Abstracts;
using DataAccess.Abstracts;
using Entities.Concretes;
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

        public void Add(Brand brand)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Brand> GetAll()
        {
            return _brandRepository.GetAll();
        }

        public Brand GetById(int id)
        {
            return _brandRepository.GetById(id);
        }

        public void Update(Brand brand)
        {
            throw new NotImplementedException();
        }
    }
}
