using DLL.Context;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public interface INewIDepRepo:IBaseRepo<Department>
    {

    }
    public class NewIDepRepo : BaseRepo<Department>, INewIDepRepo
    {
        private readonly ApplicationDbContext _context;
        public NewIDepRepo(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
       
    }
}
