﻿namespace EduNet.Backend.Service.DTOs.Students.Students;

public class StudentForCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TelegramUserName { get; set; }
    public long UserId { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}
