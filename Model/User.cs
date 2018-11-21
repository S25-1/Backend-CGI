using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cgiAPI.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; protected set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public Job_Type Job { get; set; }
        public decimal Hourly_wage { get; set; }
        public List<Skill> SkillList { get; set; }
        public Branch Branch { get; set; }
        public List<Availability> AvailabilityList { get; set; }

        public User(int userID, int userTypeID, string name, string email, string password, DateTime dateOfBirth, string phoneNumber, Address address, Job_Type job, decimal hourly_wage, List<Skill> skillList, Branch branch, List<Availability> availabilityList)
        {
            UserID = userID;
            UserTypeID = userTypeID;
            Name = name;
            Email = email;
            Password = password;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Address = address;
            Job = job;
            Hourly_wage = hourly_wage;
            SkillList = skillList;
            Branch = branch;
            AvailabilityList = availabilityList;
        }
    }
}
