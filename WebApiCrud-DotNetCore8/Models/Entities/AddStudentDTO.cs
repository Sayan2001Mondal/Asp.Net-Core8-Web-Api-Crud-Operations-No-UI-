﻿namespace WebApiCrud_DotNetCore8.Models.Entities
{
    public class AddStudentDTO
    {
        public string? StudentName { get; set; }
        public string? StudentEmail { get; set; }

        public int StudentAge { get; set; }

        public string? StudentDepartment { get; set; }
    }
}
