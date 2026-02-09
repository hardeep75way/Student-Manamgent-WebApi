using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTO;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentDto>>> GetAll()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentDto>> GetById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound("Student not found");

            return Ok(student);
        }

        [HttpGet("course/{course}")]
        public async Task<ActionResult<List<StudentDto>>> GetByCourse(string course)
        {
            var students = await _studentService.GetStudentsByCourseAsync(course);
            return Ok(students);
        }

        [HttpGet("courses")]
        public async Task<ActionResult<List<string>>> GetCourses()
        {
            var courses = await _studentService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpPost]
        public async Task<ActionResult> Create(StudentCreateDto dto)
        {
 

            var createdStudent = await _studentService.AddStudentAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdStudent.Id },
                createdStudent
            );
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, StudentUpdateDto dto)
        {
            var updated = await _studentService.UpdateStudentAsync(id, dto);
            if (!updated)
                return NotFound("Student not found");

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return NoContent();
        }

        [HttpPost("upload-excel")]
        public async Task<ActionResult> ImportFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a valid Excel file.");

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only .xlsx files are supported.");

            using (var stream = file.OpenReadStream())
            {
                var count = await _studentService.ImportStudentsFromExcelAsync(stream);
                return Ok(new { Message = $"{count} students Enrolled." });
            }
        }

        
    }
}
