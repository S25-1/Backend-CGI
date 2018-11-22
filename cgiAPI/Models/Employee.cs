using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cgiAPI.Controllers;

namespace cgiAPI.Models
{
    public class Employee : User
    {
        public List<Availability> AvailabilityList { get; set; }
        public List<VacancyAPI> ListAppliedJobs { get; set; }

        public Employee(int userID, string name, string email, string password, DateTime dateOfBirth, string phoneNumber, decimal hourly_wage, int userTypeID, Address address, int job, Branch branch, List<Skill> skillList, List<Availability> availabilityList, List<VacancyAPI> listAppliedJobs) : base(userID, name, email, password, dateOfBirth, phoneNumber, hourly_wage, userTypeID, address, job, branch, skillList)
        {
            /*niet */ UserID = userID;
            Name = name;
            Email = email;
            Password = password;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Address = address;
            JobType = job;
            Hourly_wage = hourly_wage;
            /*niet */ Branch = branch;
            /*niet */ UserTypeID = userTypeID;
            SkillList = skillList;
            /*niet */ AvailabilityList = availabilityList;
           /*niet */ ListAppliedJobs = listAppliedJobs;
        }
        public Employee(string name, string email, string password, DateTime dateOfBirth, string phoneNumber, decimal hourly_wage, int userTypeID, Address address, int job, Branch branch, List<Skill> skillList, List<Availability> availabilityList, List<VacancyAPI> listAppliedJobs) : base(-1, name, email, password, dateOfBirth, phoneNumber, hourly_wage, userTypeID, address, job, branch, skillList)
        {
            Name = name;
            Email = email;
            Password = password;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Address = address;
            JobType = job;
            Hourly_wage = hourly_wage;
            Branch = branch;
            UserTypeID = userTypeID;
            SkillList = skillList;
            AvailabilityList = availabilityList;
            ListAppliedJobs = listAppliedJobs;
        }
    }
}