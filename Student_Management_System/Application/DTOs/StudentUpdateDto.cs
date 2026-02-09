namespace StudentManagement.DTO;

public class StudentUpdateDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;
     [Required(ErrorMessage = "Course is required")]
    public string Course { get; set; } = string.Empty;
    [Range(0, 100, ErrorMessage = "Marks must be between 0 and 100")]
    public int Marks { get; set; }
}
