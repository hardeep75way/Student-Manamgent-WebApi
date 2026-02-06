using StudentManagement.DTO;

namespace StudentManagement.ViewModels;

public class StudentListViewModel
{
    public List<StudentDTOs> Students { get; set; } = new();
    public List<string> Courses { get; set; } = new();
    public int TotalStudents { get; set; }
    public string SelectedCourse { get; set; } = string.Empty;
}
