using Core.DataAccess;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    // I {entity_name} repository
    public interface ICarRepository : IRepository<Car>
    {
    }
}
