using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concretes
{
    // Şifre 
    // Encrypt, Hash (plain)
    // 123456 => JSAD723JASKF124JKASDAS

    // Salt => JSAD723JASKF124JKASDAS

    // SHA512 SHA256
    public class User : BaseEntity<int>
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[]? ForgetPasswordHash { get; set; }
        public byte[]? ForgetPasswordSalt { get; set; }
        public DateTime? ForgetPasswordExpireTime { get; set; }
    }
}
