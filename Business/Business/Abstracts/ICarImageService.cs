using Core.Utilities.Result;
using Entities.DTOs.CarImage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IResult = Core.Utilities.Result.IResult;

namespace Business.Abstracts
{
    public interface ICarImageService
    {
        IResult Add(CarImageForAddDto addDto);
    }
}
