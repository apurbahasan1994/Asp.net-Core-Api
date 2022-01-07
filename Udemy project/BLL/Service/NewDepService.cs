using BLL.Request;
using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Util.Exceptions;

namespace BLL.Service
{
    public interface INewDepService
    {
        Task<Department> GetADepartment(string code);
        Task<List<Department>> GetAllDepartments();
        Task<Department> InsertDepartment(DepartmentViewModelInsert department);
        Task<Department> UpdateDepartment(DepartmentViewModelInsert department);
        Task<Department> DeleteDepartment(string Code);
        Task<bool> CodeExist(string code);
        Task<bool> NameExist(string name);
    }
    public class NewDepService : INewDepService
    {
        //    private readonly INewIDepRepo _repo;
        //    public NewDepService(INewIDepRepo repo)
        //    {
        //        _repo = repo;
        //    }

        //    public async Task<List<Department>> GetAllDepartments()
        //    {

        //        var departments = await _repo.ListAll();
        //        return departments;
        //    }
        //    public async Task<Department> GetADepartment(string code)
        //    {
        //        if (!String.IsNullOrWhiteSpace(code) || !String.IsNullOrEmpty(code))
        //        {
        //            var department = await _repo.FindSingleAsync(x => x.Code == code);
        //            if (department != null)
        //            {
        //                return department;
        //            }
        //            throw new ApplicationValidationException("Department not found");

        //        }
        //        throw new ApplicationValidationException("Code is emplty");

        //    }
        //    public async Task<Department> InsertDepartment(DepartmentViewModelInsert department)
        //    {
        //        Department oDepartment = new Department();
        //        oDepartment.Code = department.Code;
        //        oDepartment.Name = department.Name;
        //        var exist = await _repo.FindSingleAsync(x => x.Code == department.Code);
        //        if (exist != null)
        //        {
        //            throw new ApplicationValidationException("Code is already exist");
        //        }
        //        await _repo.CreateAsync(oDepartment);
        //        if (await _repo.SaveChangesAsync())
        //        {
        //            return oDepartment;
        //        }
        //        throw new ApplicationValidationException("Could not insert this department");
        //    }
        //    public async Task<Department> UpdateDepartment(DepartmentViewModelInsert department)
        //    {
        //        if (String.IsNullOrWhiteSpace(department.Name)||String.IsNullOrEmpty(department.Name))
        //        {
        //                throw new ApplicationValidationException("Name cant be empty");
        //        }
        //        if (String.IsNullOrWhiteSpace(department.Code) || String.IsNullOrEmpty(department.Code))
        //        {
        //            throw new ApplicationValidationException("Code cant be empty");
        //        }
        //        var aDepartment = await _repo.FindSingleAsync(x => x.Code == department.Code);
        //        if (aDepartment == null)
        //        {
        //            throw new ApplicationValidationException("No department found");
        //        }

        //        aDepartment.Code = department.Code;
        //        aDepartment.Name = department.Name;
        //        _repo.UpdateteAsync(aDepartment);

        //        if (await _repo.SaveChangesAsync())
        //        {
        //            return aDepartment;
        //        }
        //        throw new ApplicationValidationException("Cant update the departement");
        //    }
        //    public async Task<Department> DeleteDepartment(string code)
        //    {
        //        var oDepartment = await _repo.FindSingleAsync(x=>x.Code==code);
        //        if (oDepartment == null)
        //        {
        //            throw new ApplicationValidationException("No department with this code");
        //        }
        //        _repo.DeleteAsync(oDepartment);
        //        if(await _repo.SaveChangesAsync())
        //        {
        //            throw new ApplicationValidationException("Unable to delte the department");
        //        }
        //        return oDepartment;

        //    }

        //    public async Task<bool> CodeExist(string code)
        //    {
        //        var oDepartment = await _repo.FindSingleAsync(x=>x.Code==code);
        //        if (oDepartment == null)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }

        //    public async Task<bool> NameExist(string name)
        //    {
        //        var oDepartment = await _repo.FindSingleAsync(x=>x.Name==name);
        //        if (oDepartment == null)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }





        //implementing using unit of work pattern



        private IUnitOfWork _unitOfWork;
       
        public NewDepService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<List<Department>> GetAllDepartments()
        {

            var departments = await _unitOfWork.departmentRepository.ListAll();
            return departments;
        }
        public async Task<Department> GetADepartment(string code)
        {
            if (!String.IsNullOrWhiteSpace(code) || !String.IsNullOrEmpty(code))
            {
                var department = await _unitOfWork.departmentRepository.FindSingleAsync(x => x.Code == code);
                if (department != null)
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
            var exist = await _unitOfWork.departmentRepository.FindSingleAsync(x => x.Code == department.Code);
            if (exist != null)
            {
                throw new ApplicationValidationException("Code is already exist");
            }
            await _unitOfWork.departmentRepository.CreateAsync(oDepartment);
            if (await _unitOfWork.departmentRepository.SaveChangesAsync())
            {
                return oDepartment;
            }
            throw new ApplicationValidationException("Could not insert this department");
        }
        public async Task<Department> UpdateDepartment(DepartmentViewModelInsert department)
        {
            if (String.IsNullOrWhiteSpace(department.Name) || String.IsNullOrEmpty(department.Name))
            {
                throw new ApplicationValidationException("Name cant be empty");
            }
            if (String.IsNullOrWhiteSpace(department.Code) || String.IsNullOrEmpty(department.Code))
            {
                throw new ApplicationValidationException("Code cant be empty");
            }
            var aDepartment = await _unitOfWork.departmentRepository.FindSingleAsync(x => x.Code == department.Code);
            if (aDepartment == null)
            {
                throw new ApplicationValidationException("No department found");
            }

            aDepartment.Code = department.Code;
            aDepartment.Name = department.Name;
            _unitOfWork.departmentRepository.UpdateteAsync(aDepartment);

            if (await _unitOfWork.departmentRepository.SaveChangesAsync())
            {
                return aDepartment;
            }
            throw new ApplicationValidationException("Cant update the departement");
        }
        public async Task<Department> DeleteDepartment(string code)
        {
            var oDepartment = await _unitOfWork.departmentRepository.FindSingleAsync(x => x.Code == code);
            if (oDepartment == null)
            {
                throw new ApplicationValidationException("No department with this code");
            }
            _unitOfWork.departmentRepository.DeleteAsync(oDepartment);
            if (await _unitOfWork.departmentRepository.SaveChangesAsync())
            {
                throw new ApplicationValidationException("Unable to delte the department");
            }
            return oDepartment;

        }

        public async Task<bool> CodeExist(string code)
        {
            var oDepartment = await _unitOfWork.departmentRepository.FindSingleAsync(x => x.Code == code);
            if (oDepartment == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> NameExist(string name)
        {
            var oDepartment = await _unitOfWork.departmentRepository.FindSingleAsync(x => x.Name == name);
            if (oDepartment == null)
            {
                return true;
            }
            return false;
        }


    }
}
