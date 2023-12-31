﻿using Microsoft.AspNetCore.Identity;

namespace TheRestaurant.Domain.Entities.Authentication
{
    public sealed class Employee : IdentityUser
    {
        public string Alias { get; set; }
        
        public bool IsDeleted { get; set; } = false;
        public DateTime EmploymentStarted { get; set; }
        public DateTime EmploymentEnded { get; set; } 

        public bool ParentalLeave { get; set; } = false;
        public DateTime ParentalLeaveStarted { get; set; }
        public DateTime ParentalLeaveEnded { get; set; }

        public Employee() { }

        public void CreateUser(string email, string alias)
        {
            Email = email;
            Alias = alias;
            EmploymentStarted = DateTime.Now;
        }

        public Employee LayoffEmployee(Employee employee)
        {
            employee.IsDeleted = true;
            employee.EmploymentEnded = DateTime.Now;
            return employee;
        }

        public Employee EmployeeHasParentalLeave(Employee employee)
        {
            employee.ParentalLeave = true;
            employee.ParentalLeaveStarted = DateTime.Now;
            return employee;
        }
        public Employee EndEmployeeParentalLeave(Employee employee)
        {
            employee.ParentalLeave = false;
            employee.ParentalLeaveEnded = DateTime.Now;
            return employee;
        }
    }
}
