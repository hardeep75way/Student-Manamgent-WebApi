using StudentManagement.Entities;

namespace StudentManagement.Services.Interfaces;

public interface IStudentRepository
{
    Task<List<Student>> GetAllStudentsAsync();
    Task<Student?> GetStudentByIdAsync(int id);
    Task<List<Student>> GetStudentByCourseAsync(string course);

    Task AddStudentAsync(Student student);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);
    Task AddRangeAsync(List<Student> students);


    Task SaveAsync();
}
