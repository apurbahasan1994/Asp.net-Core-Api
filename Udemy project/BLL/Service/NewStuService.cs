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
    public interface INewStudService
    {
        Task<Student> GetAStudent(int id);
        Task<List<Student>> GetAllStudents();
        Task<Student> InsertStudent(StudentViewModelInsert Student);
        Task<Student> UpdateStudent(StudentViewModelInsert Student);
        Task<Student> DeleteStudent(int id);
        Task<bool> EmailExist(string email);
    }
    public class NewStuService : INewStudService
    {
        private readonly INewStuRepo _repo;
        public NewStuService(INewStuRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            var student = await _repo.ListAll();
            return student;
        }
        public async Task<Student> GetAStudent(int id)
        {
            var oStudent= await _repo.FindSingleAsync(x=>x.StudentId==id);
            if(oStudent==null)
            {
                throw new ApplicationValidationException("Student not found");
            }
            return oStudent;
        }
        public async Task<Student> InsertStudent(StudentViewModelInsert student)
        {
            var exist = await _repo.FindSingleAsync(x => x.Email== student.Email);
            if(exist==null)
            {
                Student oStudent = new Student();
                oStudent.Name = student.Name;
                oStudent.Email = student.Email;
                await _repo.CreateAsync(oStudent);
                if(await _repo.SaveChangesAsync())
                {
                    return oStudent;
                }
                throw new ApplicationValidationException("Cant Save the student");
            }
            throw new ApplicationValidationException("Email already exist");



        }
        public async Task<Student> UpdateStudent(StudentViewModelInsert student)
        {
            Student oStudent = new Student();
            oStudent.Name = student.Name;
            oStudent.Email = student.Email;
            var exist = await _repo.FindSingleAsync(x => x.Email == student.Email);
            if(exist==null)
            {
                _repo.UpdateteAsync(oStudent);
                if(await _repo.SaveChangesAsync())
                {
                    return oStudent;
                }
                throw new ApplicationValidationException("Cant update the student");
            }
            throw new ApplicationValidationException("Email already exist");
        }
        public async Task<Student> DeleteStudent(int id)
        {
            var exist = await _repo.FindSingleAsync(x => x.StudentId ==id);
            if(exist==null)
            {
                throw new ApplicationValidationException("There is no student with this id");
            }
            _repo.DeleteAsync(exist);
            if(await _repo.SaveChangesAsync())
            {
                return exist;
            }
            throw new ApplicationValidationException("Cant delete this student. Something went wrong");

        }

        public async Task<bool> EmailExist(string email)
        {
            var oStudent = await _repo.FindSingleAsync(x=>x.Email==email);
            if (oStudent == null)
            {
                return true;
            }
            return false;
        }
    }
}
