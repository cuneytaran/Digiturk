using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Entity.Models;

namespace App.Data.Abstract
{
    public interface IAuthRepository
    {
        Task<Users> Register(Users user, string password);
        Task<Users> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}
