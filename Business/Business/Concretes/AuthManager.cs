using Business.Abstracts;
using Core.Entities.Concretes;
using Core.Utilities.Result;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class AuthManager : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IResult Login(string email, string password)
        {
            #region Register
            // Kriptolama 
            
            #endregion
            return new SuccessResult();
        }

        public IResult Register(string email, string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                User user = new User()
                {
                    Email = email,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    DeletedDate = DateTime.UtcNow,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };
                // DB EKLEME
                _userRepository.Add(user);
                return new SuccessResult("Kayıt başarılı");
            }
        }
    }
}
