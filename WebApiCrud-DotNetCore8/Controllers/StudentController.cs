using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCrud_DotNetCore8.Data;
using WebApiCrud_DotNetCore8.Models.Entities;

namespace WebApiCrud_DotNetCore8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public StudentController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await applicationDbContext.Students.AsNoTracking().ToListAsync();
            
            return Ok(students);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetStudent(Guid id) {
            var student = await applicationDbContext.Students.FindAsync(id);
            if (student == null) {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentDTO addStudentDTO) {
            var student = new Student() {
                StudentName = addStudentDTO.StudentName,
                StudentEmail = addStudentDTO.StudentEmail,
                StudentAge = addStudentDTO.StudentAge,
                StudentDepartment = addStudentDTO.StudentDepartment,
            };
            applicationDbContext.Students.Add(student);
            await applicationDbContext.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateStudent(Guid id, UpdateStudentDTO updateStudentDTO) {
            var student = applicationDbContext.Students.Find(id);
            if (student == null) {
                return NotFound();
            }

            student.StudentName = updateStudentDTO.StudentName;
            student.StudentEmail = updateStudentDTO.StudentEmail;
            student.StudentAge = updateStudentDTO.StudentAge;
            student.StudentDepartment = updateStudentDTO.StudentDepartment;
            applicationDbContext.SaveChanges();
            return Ok(student);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteStudent(Guid id) {
            var student = applicationDbContext.Students.Find(id);
            if (student == null) {
                return NotFound();
            }
            applicationDbContext.Students.Remove(student);
            applicationDbContext.SaveChanges();
            return Ok(student);
        }
    }
}
