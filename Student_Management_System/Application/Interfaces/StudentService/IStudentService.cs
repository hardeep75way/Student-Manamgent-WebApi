using StudentManagement.DTO;

namespace StudentManagement.Services.Interfaces;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllStudentsAsync();
    Task<List<StudentDto>> GetStudentsByCourseAsync(string course);
    Task<List<string>> GetAllCoursesAsync();
    Task<StudentDto?> GetStudentByIdAsync(int id);
    Task<StudentDto> AddStudentAsync(StudentCreateDto student);
    Task<bool> UpdateStudentAsync(int id, StudentUpdateDto student);
    Task<bool> DeleteStudentAsync(int id);
    Task<int> ImportStudentsFromExcelAsync(Stream stream);
}
