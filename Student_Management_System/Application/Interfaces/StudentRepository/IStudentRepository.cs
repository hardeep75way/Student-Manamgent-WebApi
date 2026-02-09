using StudentManagement.Entities;

namespace StudentManagement.Services.Interfaces;

public interface IStudentRepository
{
    Task<List<Student>> GetAllStudentsDB();
    Task<Student?> GetStudentByIdDB(int id);
    Task<List<Student>> GetStudentByCourseDB(string course);

    Task AddStudentDB(Student student);
    Task UpdateStudentDB(Student student);
    Task DeleteStudentDB(int id);
    Task BulkStudentsDB(List<Student> students);


    Task SaveAsync();
}
