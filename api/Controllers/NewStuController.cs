using BLL.Request;
using BLL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewStuController : ControllerBase
    {
        private readonly INewStudService _studentrepo;

        public NewStuController(INewStudService studentRepository)
        {
            _studentrepo = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await _studentrepo.GetAllStudents());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAStudent(int id)
        {
            return Ok(await _studentrepo.GetAStudent(id));
        }
        [HttpPost]
        public async Task<IActionResult> InsertAStudent(StudentViewModelInsert student)
        {
            return Ok(await _studentrepo.InsertStudent(student));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAStudent(StudentViewModelInsert student)
        {
            return Ok(await _studentrepo.UpdateStudent(student));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAStudent(int id)
        {
            return Ok(await _studentrepo.DeleteStudent(id));
        }
    }
}
