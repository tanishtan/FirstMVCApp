using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FirstAzApp.Models
{
    public class Student
    {
        [Key]
        public int RollNo { get; set; }
        public string StudentName { get; set; }
        public int StatusId { get; set; }
        
    }

    public class StudentsDbContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public StudentsDbContext(DbContextOptions<StudentsDbContext> options) : base(options) { }
    }
}
