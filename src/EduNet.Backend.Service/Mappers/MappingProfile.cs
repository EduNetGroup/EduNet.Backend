using AutoMapper;
using EduNet.Backend.Service.DTOs.Logins;
using EduNet.Backend.Domain.Entities.Roles;
using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Service.DTOs.Branches;
using EduNet.Backend.Service.DTOs.Payments;
using EduNet.Backend.Domain.Entities.Courses;
using EduNet.Backend.Service.DTOs.Users.Users;
using EduNet.Backend.Domain.Entities.Branches;
using EduNet.Backend.Domain.Entities.Payments;
using EduNet.Backend.Domain.Entities.Students;
using EduNet.Backend.Domain.Entities.Teachers;
using EduNet.Backend.Service.DTOs.Roles.Roles;
using EduNet.Backend.Service.DTOs.Users.UserCodes;
using EduNet.Backend.Service.DTOs.Users.UserRoles;
using EduNet.Backend.Service.DTOs.Courses.Courses;
using EduNet.Backend.Service.DTOs.Courses.Lessons;
using EduNet.Backend.Service.DTOs.Roles.Permissions;
using EduNet.Backend.Service.DTOs.Teachers.Teachers;
using EduNet.Backend.Service.DTOs.Students.Students;
using EduNet.Backend.Service.DTOs.Students.Attendances;
using EduNet.Backend.Service.DTOs.Students.Enrollments;
using EduNet.Backend.Service.DTOs.Roles.RolePermissions;
using EduNet.Backend.Service.DTOs.Teachers.TeacherCourses;

namespace EduNet.Backend.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();

        // Role
        CreateMap<Role, RoleForResultDto>().ReverseMap();
        CreateMap<Role, RoleForUpdateDto>().ReverseMap();
        CreateMap<Role, RoleForCreationDto>().ReverseMap();

        // Course
        CreateMap<Course, CourseForResultDto>().ReverseMap();
        CreateMap<Course, CourseForUpdateDto>().ReverseMap();
        CreateMap<Course, CourseForCreationDto>().ReverseMap();
        
        // Branch
        CreateMap<Branch, BranchForResultDto>().ReverseMap();
        CreateMap<Branch, BranchForUpdateDto>().ReverseMap();
        CreateMap<Branch, BranchForCreationDto>().ReverseMap();
        
        // Lesson
        CreateMap<Lesson, LessonForResultDto>().ReverseMap();
        CreateMap<Lesson, LessonForUpdateDto>().ReverseMap();
        CreateMap<Lesson, LessonForCreationDto>().ReverseMap();

        // Teacher
        CreateMap<Teacher, TeacherForResultDto>().ReverseMap();
        CreateMap<Teacher, TeacherForUpdateDto>().ReverseMap();
        CreateMap<Teacher, TeacherForCreationDto>().ReverseMap();
        CreateMap<TeacherProfilePhoto, TeacherProfilePhotoForResultDto>().ReverseMap();

        // Payment
        CreateMap<Payment, PaymentForResultDto>().ReverseMap();
        CreateMap<Payment, PaymentForUpdateDto>().ReverseMap();
        CreateMap<Payment, PaymentForCreationDto>().ReverseMap();

        // Student
        CreateMap<Student, StudentForResultDto>().ReverseMap();
        CreateMap<Student, StudentForUpdateDto>().ReverseMap();
        CreateMap<Student, StudentForCreationDto>().ReverseMap();
        CreateMap<StudentProfilePhoto, StudentProfilePhotoForResultDto>().ReverseMap();

        // UserRole
        CreateMap<UserRole, UserRoleForResultDto>().ReverseMap();
        CreateMap<UserRole, UserRoleForUpdateDto>().ReverseMap();
        CreateMap<UserRole, UserRoleForCreationDto>().ReverseMap();

        // UserCode
        CreateMap<UserCode, UserCodeForResultDto>().ReverseMap();
        CreateMap<UserCode, UserCodeForCreationDto>().ReverseMap();

        // Attendance
        CreateMap<Attendance, AttendanceForResultDto>().ReverseMap();
        CreateMap<Attendance, AttendanceForUpdateDto>().ReverseMap();
        CreateMap<Attendance, AttendanceForCreationDto>().ReverseMap();

        // Permission
        CreateMap<Permission, PermissionForResultDto>().ReverseMap();
        CreateMap<Permission, PermissionForUpdateDto>().ReverseMap();
        CreateMap<Permission, PermissionForCreationDto>().ReverseMap();

        // Enrollment
        CreateMap<Enrollment, EnrollmentForResultDto>().ReverseMap();
        CreateMap<Enrollment, EnrollmentForUpdateDto>().ReverseMap();
        CreateMap<Enrollment, EnrollmentForCreationDto>().ReverseMap();
        
        // Login
        CreateMap<LoginForCreationDto, LoginForResultDto>().ReverseMap();

        // TeacherCourse
        CreateMap<TeacherCourse, TeacherCourseForResultDto>().ReverseMap();
        CreateMap<TeacherCourse, TeacherCourseForUpdateDto>().ReverseMap();
        CreateMap<TeacherCourse, TeacherCourseForCreationDto>().ReverseMap();

        // RolePermission
        CreateMap<RolePermission, RolePermissionForResultDto>().ReverseMap();
        CreateMap<RolePermission, RolePermissionForUpdateDto>().ReverseMap();
        CreateMap<RolePermission, RolePermissionForCreationDto>().ReverseMap();
    }
}
