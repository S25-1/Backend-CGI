using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cgi.Controllers
{
    public class User
    {
        public int UserID { get; protected set; }
        public string UserType { get; protected set; }
        //public Branch Branch { get; set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        //protected DateTime DateOfBirth { get; set; }
        //protected string PhoneNumber { get; set; }
        //protected float HourlyWage { get; set; }
        //protected Address Address { get; set; }
        public string Job { get; protected set; }
        public List<SkillType> ListCompetences { get; protected set; }
        //protected List<Availability> Availability { get; set; }

        public User(int userID, string userType, string name, string email, string password, string job, List<SkillType> listCompetences)
        {
            UserID = userID;
            UserType = userType;
            Name = name;
            Email = email;
            Password = password;
            Job = job;
            ListCompetences = listCompetences;
        }
    }
}
