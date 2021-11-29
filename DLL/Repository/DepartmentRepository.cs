using DLL.Context;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Department> FindBayCode(string code)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);
            
        }

        public async Task<bool> DeleteDepartment(Department department)
        {
            var oDepartment = await _context.Departments.FirstOrDefaultAsync(x => x.Code == department.Code);
            _context.Departments.Remove(oDepartment);
            if (await _context.SaveChangesAsync()>0)
            {
                return true;
            }
            return false;
        }

        public async Task<Department> GetADepartment(string code)
        {
            var oDepartment = await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);
            return oDepartment;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> InsertDepartment(Department department)
        {
            var oDepartment=await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> FindByName(string name)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Name ==name);
          
        }

        public async Task<bool> UpdateDepartment(Department department)
        {
            var oDepartment = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == department.DepartmentId);
            oDepartment = department;
            _context.Departments.Update(oDepartment);
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;

        }
    }
}
