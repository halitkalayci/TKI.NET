using Business.Abstracts;
using Core.Utilities.Helpers;
using Core.Utilities.Result;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.DTOs.CarImage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IResult = Core.Utilities.Result.IResult;

namespace Business.Concretes
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageRepository _carImageRepository;

        public CarImageManager(ICarImageRepository carImageRepository)
        {
            _carImageRepository = carImageRepository;
        }

        public IResult Add(CarImageForAddDto addDto)
        {
            var filePath = FileHelper.UploadFromBase64(addDto.Base64);

            CarImage carImage = new()
            {
                CarId = addDto.CarId,
                ImagePath = filePath,
            };
            _carImageRepository.Add(carImage);
            return new SuccessResult("Araba resmi eklendi.");
        }
    }
}
