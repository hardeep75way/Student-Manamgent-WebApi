using Microsoft.AspNetCore.Http;

namespace StudentManagement.DTO;

public class StudentExcelUploadDto
{
    public IFormFile File { get; set; } = null!;
}
