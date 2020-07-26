using App.Data.Abstract;
using App.Entity;
using App.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Data.Concrete.EfCore
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }



        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public List<Users> GetUsers()
        {
            var Users = _context.Users.ToList();
            return Users;
        }

        public List<Users> GetUsersById(int UserId)
        {
            throw new NotImplementedException();
        }

        public Users GetUserById(int UserId)
        {
            var User = _context.Users.FirstOrDefault(x => x.UserId == UserId);
            return User;
        }
    }
}
