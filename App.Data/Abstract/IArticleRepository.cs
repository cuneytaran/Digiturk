using App.Data.Dtos;
using App.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Abstract
{
    public interface IArticleRepository
    {
        Task<List<Articles>> GetsAsync();
        Task<Articles> GetByIdAsync(int id);

        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteList<T>(T entity) where T : class;
        Task<bool> SaveAll();
    }
}
