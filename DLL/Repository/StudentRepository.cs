using DLL.Context;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Student> DeleteStudent(int id)
        {
            var oStudent = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == id);
            _context.Students.Remove(oStudent);
            await _context.SaveChangesAsync();
            return oStudent;
        }

        public async Task<Student> GetAStudent(int id)
        {
            var oStudent = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == id);
            return oStudent;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> InsertStudent(Student Student)
        {
            var oStudent = await _context.Students.AddAsync(Student);
            await _context.SaveChangesAsync();
            return Student;
        }


        public async Task<Student> UpdateStudent(Student Student)
        {
            var oStudent = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == Student.StudentId);
            oStudent = Student;
            _context.Students.Update(oStudent);
            await _context.SaveChangesAsync();
            return oStudent;

        }

        public async Task<Student> FindByEmail(string email)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.Email == email);
            
        }
    }
}
