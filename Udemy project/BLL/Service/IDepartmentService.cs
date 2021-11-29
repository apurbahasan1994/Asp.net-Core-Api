using BLL.Request;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface IDepartmentService
    {
        Task<Department> GetADepartment(string code);
        Task<List<Department>> GetAllDepartments();
        Task<Department> InsertDepartment(DepartmentViewModelInsert department);
        Task<Department> UpdateDepartment(DepartmentViewModelInsert department);
        Task<Department> DeleteDepartment(string Code);
        Task<bool> CodeExist(string code);
        Task<bool> NameExist(string name);
    }
}
