using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cgiAPI.Models;

namespace cgi.Controllers
{
    public abstract class User
    {
        public int UserID { get; protected set; }
        public string UserType { get; protected set; }
        public Branch Branch { get; set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public float HourlyWage { get; set; }
        public Adress Address { get; set; }
        public Job_Type Job_Type { get; protected set; }
        public List<SkillType> ListCompetences { get; protected set; }
        //protected List<Availability> Availability { get; set; }
        public List<Availability> Availability  { get; set; }

        public User(int userID, string userType, string name, string email, string password, Job_Type job_type, List<SkillType> listCompetences, List<Availability> availability)
        {
            UserID = userID;
            UserType = userType;
            Name = name;
            Email = email;
            Password = password;
            Job_Type = job_type;
            ListCompetences = listCompetences;
            Availability = availability;
        }
    }
}
