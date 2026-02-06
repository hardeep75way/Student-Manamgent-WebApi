namespace StudentManagement.DTO;

public class StudentUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Course { get; set; } = string.Empty;
    public int Marks { get; set; }
}
