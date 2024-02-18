using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Domain.Entities.Roles;
using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Domain.Entities.Courses;
using EduNet.Backend.Domain.Entities.Branches;
using EduNet.Backend.Domain.Entities.Payments;
using EduNet.Backend.Domain.Entities.Students;
using EduNet.Backend.Domain.Entities.Teachers;

namespace EduNet.Data.DbContexts;

public class AppDbContext : DbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<Course> Courses { get; set; }
    DbSet<Lesson> Lessons { get; set; }
    DbSet<Branch> Branches { get; set; }
    DbSet<Teacher> Teachers { get; set; }
    DbSet<Payment> Payments { get; set; }
    DbSet<Student> Students { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<UserCode> UserCodes { get; set; }
    DbSet<Permission> Permissions { get; set; }
    DbSet<Attendance> Attendances { get; set; }
    DbSet<Enrollment> Enrollments { get; set; }
    DbSet<TeacherCourse> TeachersCourse { get; set; }
    DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
