﻿using EduNet.Backend.Domain.Enums;
using EduNet.Backend.Domain.Enums;

namespace EduNet.Service.DTOs.Students.Attendances;

public class AttendanceForUpdateDto
{
    public long StudentId { get; set; }
    public long CourseId { get; set; }
    public string Date { get; set; }
    public Status Status { get; set; }
}