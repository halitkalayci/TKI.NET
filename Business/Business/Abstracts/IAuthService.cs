using Core.Utilities.Result;
using Core.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IAuthService
    {
        IDataResult<AccessToken> Login(string email, string password);
        IResult Register(string email, string password);

        IResult RequestResetPassword(string email);
        IResult ResetPassword(string email, string token, string newPassword);
    }
}
