using BLL.Request;
using DLL.Models;

using DLL.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Util.Exceptions;

namespace BLL.Service
{
    public class DepartmentService:IDepartmentService
    {
        private readonly IDepartmentRepository _repo;
        public DepartmentService(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Department>> GetAllDepartments()
        {

            var departments=await _repo.GetAllDepartments();
            return departments;
        }
        public async Task<Department> GetADepartment(string code)
        {
            if(!String.IsNullOrWhiteSpace(code)|| !String.IsNullOrEmpty(code))
            {
                var department= await _repo.GetADepartment(code);
                if(department!=null)
                {
                    return department;
                }
                throw new ApplicationValidationException("Department not found");

            }
            throw new ApplicationValidationException("Code is emplty");
            
        }
        public async Task<Department> InsertDepartment(DepartmentViewModelInsert department)
        {
            Department oDepartment = new Department();
            oDepartment.Code = department.Code;
            oDepartment.Name = department.Name;


            return await _repo.InsertDepartment(oDepartment);
        }
        public async Task<Department> UpdateDepartment(DepartmentViewModelInsert department)
        {
            var aDepartment = await _repo.FindBayCode(department.Code);
            if (aDepartment==null)
            {
                throw new ApplicationValidationException("No department found");
            }
            if(!String.IsNullOrWhiteSpace(department.Code))
            {
                var existDepartment = await _repo.FindBayCode(department.Code);
                if(existDepartment==null)
                {
                    throw new ApplicationValidationException("Code already present in Database");

                }
                aDepartment.Code = department.Code;
                
            }
            if (!String.IsNullOrWhiteSpace(department.Name))
            {
                var existDepartment = await _repo.FindByName(department.Name);
                if (existDepartment==null)
                {
                    throw new ApplicationValidationException("Name already present in Database");

                }
                aDepartment.Name = department.Name;
            }

            if(await _repo.UpdateDepartment(aDepartment))
            {
                return aDepartment;
            }
            throw new ApplicationValidationException("Cant update the departement");
        }
        public async Task<Department> DeleteDepartment(string code)
        {
            var oDepartment =await _repo.FindBayCode(code);
            if(oDepartment==null)
            {
                throw new ApplicationValidationException("No department with this code");
            }
            if(!await _repo.DeleteDepartment(oDepartment))
            {
                throw new ApplicationValidationException("Unable to delte the department");
            }
            return oDepartment;

        }

        public  async Task<bool> CodeExist(string code)
        {
            var oDepartment = await _repo.FindBayCode(code);
            if(oDepartment==null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> NameExist(string name)
        {
            var oDepartment = await _repo.FindByName(name);
            if (oDepartment == null)
            {
                return true;
            }
            return false;
        }
    }
}
