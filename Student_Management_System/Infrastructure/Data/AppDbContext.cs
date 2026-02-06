using Microsoft.EntityFrameworkCore;
using StudentManagement.Entities;

namespace StudentManagement.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Student> Students => Set<Student>();
}
