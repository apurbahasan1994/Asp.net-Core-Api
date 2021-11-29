using DLL.Context;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Repository
{
    public interface INewStuRepo:IBaseRepo<Student>
    { }
    public class NewStuRepo:BaseRepo<Student>,INewStuRepo
    {
        private readonly ApplicationDbContext _context;
        public NewStuRepo(ApplicationDbContext context):base(context)
        {
            _context = context;

        }
    }
}
