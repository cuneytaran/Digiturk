using App.Data.Abstract;
using App.Data.Dtos;
using App.Entity;
using App.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Concrete
{
    public class ArticleRepository:IArticleRepository
    {
        private DataContext _context;
        public ArticleRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Articles>> GetsAsync()
        {
            var DataList = await _context.Articles.ToListAsync();
            return DataList;
        }       


        public async Task<Articles> GetByIdAsync(int id)
        {
            var Data = await _context.Articles.FirstOrDefaultAsync(x => x.ArticleId == id);
            return Data;
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


        public void DeleteList<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }


        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
