using DLL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public interface IBaseRepo<T> where T:class
    {
        //genertate query not get data
        IQueryable<T> QueryAll(Expression<Func<T,bool>> expression=null);
        Task<List<T>> ListAll(Expression<Func<T, bool>> expression = null);
        Task CreateAsync(T entry);
        Task CreateAsyncList(List<T> entryList);
        void UpdateteAsync(T entry);
        void UpdateRange(List<T> entryList);
        void DeleteAsync(T entry);
        void Deleterange(List<T> entryList);
        Task<T>FindSingleAsync(Expression<Func<T, bool>> expression = null);
        Task<bool> SaveChangesAsync();
    }
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(T entry)
        {
             await _context.Set<T>().AddAsync(entry);
        }

        public async Task CreateAsyncList(List<T> entryList)
        {
            await _context.Set<T>().AddRangeAsync(entryList);
        }

        public void DeleteAsync(T entry)
        {
             _context.Set<T>().Remove(entry);
        }

        public void Deleterange(List<T> entryList)
        {
            _context.Set<T>().RemoveRange(entryList);
        }

        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> expression = null)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> ListAll(Expression<Func<T, bool>> expression = null)
        {
            return expression != null ? await _context.Set<T>().AsQueryable().Where(expression).AsNoTracking().ToListAsync() :await _context.Set<T>().AsQueryable().AsNoTracking().ToListAsync();
        }

        public  IQueryable<T> QueryAll(Expression<Func<T, bool>> expression = null)
        {
            return expression != null ? _context.Set<T>().AsQueryable().Where(expression).AsNoTracking() : _context.Set<T>().AsQueryable().AsNoTracking();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }

        public void UpdateRange(List<T> entryList)
        {
            _context.Set<T>().UpdateRange(entryList);
        }

        public void UpdateteAsync(T entry)
        {
            _context.Set<T>().Update(entry);
        }
    }
}
