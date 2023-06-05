using Business.Abstracts;
using Core.Entities.Concretes;
using Core.Exceptions.Types;
using Core.Utilities.Result;
using Core.Utilities.Security.Hashing;
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
            User user = _userRepository.Get(i => i.Email == email);
            userShouldNotBeNull(user);
            // Salt'ı al
            // HMACSHA512 salt ile kullanıldığında
            // password şifrelendiğinde kullanıcının HASH'i ile uyuşuyor mu?
            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash))
                return new ErrorResult("Şifre yanlış.");
            return new SuccessResult("Giriş başarılı.");
        }

        private static void userShouldNotBeNull(User user)
        {
            if (user == null)
            {
                throw new BusinessException("User not found");
            }
        }

        public IResult Register(string email, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            User user = new User()
            {
                Email = email,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                DeletedDate = DateTime.UtcNow,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userRepository.Add(user);
            return new SuccessResult("Kayıt başarılı");
        }
    }
}
