using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Entities;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Student>> GetAllStudentsAsync()
    {
        return _context.Students
            .Where(s => s.isActive)
            .ToListAsync();
    }

    public Task<Student?> GetStudentByIdAsync(int id)
    {
        return _context.Students
            .Where(s => s.isActive && s.Id == id)
            .FirstOrDefaultAsync();
    }

    public Task<List<Student>> GetStudentByCourseAsync(string course)
    {
        return _context.Students
            .Where(s => s.isActive && s.Course == course)
            .ToListAsync();
    }

    public async Task AddStudentAsync(Student student)
    {
        await _context.Students.AddAsync(student);
    }

    public Task UpdateStudentAsync(Student student)
    {
        _context.Students.Update(student);
        return Task.CompletedTask;
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            student.isActive = false;
            _context.Students.Update(student);
        }
    }
    
    public async Task AddRangeAsync(List<Student> students)
    {
        await _context.Students.AddRangeAsync(students);
    }


    public Task SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}
