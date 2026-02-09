using System.ComponentModel.DataAnnotations;

namespace StudentManagement.DTO;

public class StudentCreateDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    public string Course { get; set; }

    [Range(0, 100)]
    public int Marks { get; set; }
}
