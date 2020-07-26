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
    public class CommentRepository:ICommentRepository
    {
        private DataContext _context;
        public CommentRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Comments>> GetsAsync()
        {
            var DataList = await _context.Comments.ToListAsync();
            return DataList;
        }


        public async Task<List<Comments>> GetCommentListAsync(CommentList data)
        {
            var list = data.CommentId;

            var getList = await _context.Comments.Where(x => x.CommentId == data.CommentId).ToListAsync();

            _context.Comments.RemoveRange(getList);
            return getList;
        }

        public async Task<List<Comments>> GetListOfCommentIdAsync(int id)
        {
            var DataList = await (from Articles in _context.Articles
                                  join Comments in _context.Comments on Articles.ArticleId equals Comments.CommentId
                                  where
                                    Comments.CommentId == id
                                  select new Comments
                                  {
                                      CommentId=Comments.CommentId,
                                      ArticleId=Comments.ArticleId,
                                      Comment=Comments.Comment,
                                      UserId=Comments.UserId,
                                      CommentDate=Comments.CommentDate,
                                      Active=Comments.Active                                      
                                  }).ToListAsync();

            return DataList;
        }


        public async Task<Comments> GetByIdAsync(int id)
        {
            var Data = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);
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
