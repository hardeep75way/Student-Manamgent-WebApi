using StudentManagement.DTO;
using StudentManagement.Entities;

namespace StudentManagement.Mappings;

public static class StudentMapper
{
    public static StudentDTOs ToDto(Student student)
    {
        return new StudentDTOs()
        {
            Id = student.Id,
            Name = student.Name,
            Course = student.Course,
            Marks = student.Marks,
        };
    }
    
    public static Student ToEntity(StudentDTOs dto)
    {
        return new Student
        {
            Id = dto.Id,
            Name = dto.Name,
            Course = dto.Course,
            Marks = dto.Marks
        };
    }

    public static Student ToEntity(StudentCreateDto dto)
    {
        return new Student
        {
            Name = dto.Name,
            Course = dto.Course,
            Marks = dto.Marks
        };
    }

    public static Student ToEntity(StudentUpdateDto dto, int id)
    {
        return new Student
        {
            Id = id,
            Name = dto.Name,
            Course = dto.Course,
            Marks = dto.Marks
        };
    }
}
