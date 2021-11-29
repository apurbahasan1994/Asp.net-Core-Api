using BLL.Request;
using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _repo;
        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            var student = await _repo.GetAllStudents();
            return student;
        }
        public async Task<Student> GetAStudent(int id)
        {
            return await _repo.GetAStudent(id);
        }
        public async Task<Student> InsertStudent(StudentViewModelInsert student)
        {
            Student oStudent = new Student();
            oStudent.Name = student.Name;
            oStudent.Email = student.Email;
            return await _repo.InsertStudent(oStudent);
        }
        public async Task<Student> UpdateStudent(StudentViewModelInsert student)
        {
            Student oStudent = new Student();
            oStudent.Name = student.Name;
            oStudent.Email = student.Email;
            return await _repo.UpdateStudent(oStudent);
        }
        public async Task<Student> DeleteStudent(int id)
        {
            return await _repo.DeleteStudent(id);

        }

        public async Task<bool> EmailExist(string email)
        {
            var oStudent= await _repo.FindByEmail(email);
            if(oStudent==null)
            {
                return true;
            }
            return false;
        }
    }
}
