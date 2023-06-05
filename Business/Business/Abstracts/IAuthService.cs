using Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IAuthService
    {
        IResult Login(string email, string password);
        IResult Register(string email, string password);

        IResult RequestResetPassword(string email);
        IResult ResetPassword(string email, string token, string newPassword);
    }
}
