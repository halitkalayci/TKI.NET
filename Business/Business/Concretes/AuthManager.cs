using Business.Abstracts;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concretes;
using Core.Exceptions.Types;
using Core.Utilities.Result;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;
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
        private readonly ITokenHelper _tokenHelper;
        public AuthManager(IUserRepository userRepository, ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }


        [Logging(typeof(MSSQLLogger))]
        public IDataResult<AccessToken> Login(string email, string password)
        {
            User user = _userRepository
                .Get(i => i.Email == email, 
                include: i=>i.Include(i=>i.UserOperationClaims).ThenInclude(i=>i.OperationClaim));
            userShouldNotBeNull(user);
            // Salt'ı al
            // HMACSHA512 salt ile kullanıldığında
            // password şifrelendiğinde kullanıcının HASH'i ile uyuşuyor mu?
            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash))
                return new ErrorDataResult<AccessToken>(null);

            var token = _tokenHelper.CreateToken(user);
            return new SuccessDataResult<AccessToken>(token);
        }

        private static void userShouldNotBeNull(User user)
        {
            if (user == null)
            {
                throw new BusinessException("User not found");
            }
        }
        [Logging(typeof(MSSQLLogger))]
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

        public IResult RequestResetPassword(string email)
        {
            User user = _userRepository.Get(i => i.Email == email);
            userShouldNotBeNull(user);

            string token = new Random().Next(10000, 99999).ToString();
            byte[] tokenHash, tokenSalt;
            HashingHelper.CreatePasswordHash(token,out tokenHash, out tokenSalt);
            user.ForgetPasswordSalt = tokenSalt;
            user.ForgetPasswordHash = tokenHash;
            user.ForgetPasswordExpireTime = DateTime.UtcNow.AddMinutes(3);
            _userRepository.Update(user);
            return new SuccessResult("Şifre sıfırlama kodu emailinize gönderildi.");
        }

        public IResult ResetPassword(string email, string token, string newPassword)
        {
            User user = _userRepository.Get(i => i.Email == email);
            userShouldNotBeNull(user);

            bool isTokenTrue = HashingHelper.VerifyPasswordHash(token, user.ForgetPasswordSalt, user.ForgetPasswordHash);

            if(!isTokenTrue || user.ForgetPasswordExpireTime < DateTime.UtcNow)
            {
                return new ErrorResult("Token doğrulanamadı");
            }

            byte[] passwordSalt, passwordHash;
            HashingHelper.CreatePasswordHash(newPassword,out passwordHash,out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ForgetPasswordExpireTime = DateTime.UtcNow.AddMinutes(-3);
            _userRepository.Update(user);
            return new SuccessResult("Şifreniz başarıyla değiştirildi");
        }
    }
}
