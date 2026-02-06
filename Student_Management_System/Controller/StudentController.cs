using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTO;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentDTOs>>> GetAll()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentDTOs>> GetById(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                    return NotFound("Student not found");

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("course/{course}")]
        public async Task<ActionResult<List<StudentDTOs>>> GetByCourse(string course)
        {
            try
            {
                var students = await _studentService.GetStudentsByCourseAsync(course);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("courses")]
        public async Task<ActionResult<List<string>>> GetCourses()
        {
            try
            {
                var courses = await _studentService.GetAllCoursesAsync();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(StudentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdStudent = await _studentService.AddStudentAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdStudent.Id },
                    createdStudent
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, StudentUpdateDto dto)
        {
            try
            {
                var updated = await _studentService.UpdateStudentAsync(id, dto);
                if (!updated)
                    return NotFound("Student not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload-excel")]
        public async Task<ActionResult> ImportFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a valid Excel file.");

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only .xlsx files are supported.");

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    var count = await _studentService.ImportStudentsFromExcelAsync(stream);
                    return Ok(new { Message = $"{count} students Enrolled." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
