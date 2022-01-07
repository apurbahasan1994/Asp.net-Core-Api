using DLL.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed=false;

        private readonly INewStuRepo _stuRepo;
        private readonly INewIDepRepo _depRepo;
        public INewIDepRepo departmentRepository => _depRepo ?? new NewIDepRepo(_context);

        public INewStuRepo studentRepository => _stuRepo ?? new NewStuRepo(_context);

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
           _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
