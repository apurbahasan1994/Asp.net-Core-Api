using BLL.Request;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface IStudentService
    {
        Task<Student> GetAStudent(int id);
        Task<List<Student>> GetAllStudents();
        Task<Student> InsertStudent(StudentViewModelInsert Student);
        Task<Student> UpdateStudent(StudentViewModelInsert Student);
        Task<Student> DeleteStudent(int id);
        Task<bool> EmailExist(string email);
    }
}
