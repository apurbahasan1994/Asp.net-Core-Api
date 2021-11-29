using DLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public interface IStudentRepository
    {
        Task<Student> GetAStudent(int id);
        Task<List<Student>> GetAllStudents();
        Task<Student> InsertStudent(Student Student);
        Task<Student> UpdateStudent(Student Student);
        Task<Student> DeleteStudent(int id);
        Task<Student> FindByEmail(string email);

    }
}
