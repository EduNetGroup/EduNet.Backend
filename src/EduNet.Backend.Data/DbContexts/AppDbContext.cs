using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Domain.Entities.Roles;
using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Domain.Entities.Courses;
using EduNet.Backend.Domain.Entities.Branches;
using EduNet.Backend.Domain.Entities.Payments;
using EduNet.Backend.Domain.Entities.Students;
using EduNet.Backend.Domain.Entities.Teachers;
using EduNet.Backend.Domain.Entities.Messages;
using EduNet.Backend.Domain.Entities.Assets;

namespace EduNet.Backend.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<Asset> Assets { get; set; }
    DbSet<Course> Courses { get; set; }
    DbSet<Lesson> Lessons { get; set; }
    DbSet<Branch> Branches { get; set; }
    DbSet<Message> Messages { get; set; }
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
    DbSet<StudentProfilePhoto> StudentProfiles { get; set; }
    DbSet<TeacherProfilePhoto> TeacherProfilePhotos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
