using OfficeOpenXml;
using StudentManagement.DTO;
using StudentManagement.Entities;
using StudentManagement.Mappings;
using StudentManagement.Services.Interfaces;


namespace StudentManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<StudentDTOs>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return students.Select(StudentMapper.ToDto).ToList();
        }

        public async Task<List<StudentDTOs>> GetStudentsByCourseAsync(string course)
        {
            var students = await _studentRepository.GetStudentByCourseAsync(course);
            return students.Select(StudentMapper.ToDto).ToList();
        }

        public async Task<List<string>> GetAllCoursesAsync()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return students
                .Select(s => s.Course)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .ToList();
        }

        public async Task<StudentDTOs?> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
                return null;

            return StudentMapper.ToDto(student);
        }

        public async Task<StudentDTOs> AddStudentAsync(StudentCreateDto studentDto)
        {
            var student = StudentMapper.ToEntity(studentDto);

            await _studentRepository.AddStudentAsync(student);
            await _studentRepository.SaveAsync();

            return StudentMapper.ToDto(student);
        }

        public async Task<bool> UpdateStudentAsync(int id, StudentUpdateDto studentDto)
        {
            var existing = await _studentRepository.GetStudentByIdAsync(id);
            if (existing == null)
                return false;

            existing.Name = studentDto.Name;
            existing.Course = studentDto.Course;
            existing.Marks = studentDto.Marks;

            await _studentRepository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var existing = await _studentRepository.GetStudentByIdAsync(id);
            if (existing == null)
                return false;

            await _studentRepository.DeleteStudentAsync(id);
            await _studentRepository.SaveAsync();

            return true;
        }
        

        public async Task<int> ImportStudentsFromExcelAsync(Stream stream)
        {
            if (stream == null)
                throw new Exception("Invalid file");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var students = new List<Student>();

            if (stream.CanSeek)
                stream.Position = 0;

            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0];

            int rowCount = worksheet.Dimension.Rows;

            // Start from row 2 (skip header)
            for (int row = 2; row <= rowCount; row++)
            {
                var name = worksheet.Cells[row, 1].Text;
                var course = worksheet.Cells[row, 2].Text;
                var marksText = worksheet.Cells[row, 3].Text;

                if (string.IsNullOrWhiteSpace(name))
                    continue;

                if (!int.TryParse(marksText, out var marks))
                    marks = 0;

                students.Add(new Student
                {
                    Name = name,
                    Course = course,
                    Marks = marks
                });
            }

            await _studentRepository.AddRangeAsync(students);
            await _studentRepository.SaveAsync();
            return students.Count;
        }

    }
}
