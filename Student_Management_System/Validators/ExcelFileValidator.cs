namespace Student_Management_System.Validators;

public static class ExcelFileValidator
{
    private const long MaxFileSize = 5 * 1024 * 1024;
    private const string AllowedExtension = ".xlsx";

    public static string ErrorMessage { get; private set; } = string.Empty;

    public static bool IsValid(IFormFile? file)
    {
        if (file is null || file.Length == 0)
        {
            ErrorMessage = "Excel file is required.";
            return false;
        }

        if (file.Length > MaxFileSize)
        {
            ErrorMessage = "Excel file size exceeds 5 MB.";
            return false;
        }

        if (!string.Equals(
                Path.GetExtension(file.FileName),
                AllowedExtension,
                StringComparison.OrdinalIgnoreCase))
        {
            ErrorMessage = "Only .xlsx files are supported.";
            return false;
        }

        return true;
    }
}
