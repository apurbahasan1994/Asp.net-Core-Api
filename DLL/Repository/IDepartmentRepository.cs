using DLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public interface IDepartmentRepository
    {
       Task<Department> GetADepartment(string code);
        Task<List<Department>> GetAllDepartments();
        Task<Department> InsertDepartment(Department department);
       Task<bool> UpdateDepartment(Department department);
        Task<bool> DeleteDepartment(Department department);
        Task<Department> FindBayCode(string code);
        Task<Department> FindByName(string name);
    }
}
