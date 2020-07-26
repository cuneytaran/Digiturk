using App.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Abstract
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveAll();

        List<Users> GetUsers();
        List<Users> GetUsersById(int UserId);
        Users GetUserById(int UserId);
    }
}
