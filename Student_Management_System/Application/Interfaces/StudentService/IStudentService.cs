using StudentManagement.DTO;

namespace StudentManagement.Services.Interfaces;

public interface IStudentService
{
    Task<List<StudentDTOs>> GetAllStudentsAsync();
    Task<List<StudentDTOs>> GetStudentsByCourseAsync(string course);
    Task<List<string>> GetAllCoursesAsync();
    Task<StudentDTOs?> GetStudentByIdAsync(int id);
    Task<StudentDTOs> AddStudentAsync(StudentCreateDto student);
    Task<bool> UpdateStudentAsync(int id, StudentUpdateDto student);
    Task<bool> DeleteStudentAsync(int id);
    Task<int> ImportStudentsFromExcelAsync(Stream stream);
}
