using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public interface IUnitOfWork
    {
        INewIDepRepo departmentRepository { get; }
        INewStuRepo studentRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
