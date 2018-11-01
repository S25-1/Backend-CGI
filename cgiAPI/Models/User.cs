using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cgiAPI.Controllers
{
    public class User
    {
        public int UserID { get; protected set; }
        public int UserTypeID { get; protected set; }
        //public Branch Branch { get; set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        //protected DateTime DateOfBirth { get; set; }
        //protected string PhoneNumber { get; set; }
        //protected float HourlyWage { get; set; }
        //protected Address Address { get; set; }
        public int JobID { get; protected set; }
        public List<SkillType> ListCompetences { get; protected set; }
        //protected List<Availability> Availability { get; set; }

        public User(int userID, int userTypeID, string name, string email, string password, int jobID, List<SkillType> listCompetences)
        {
            UserID = userID;
            UserTypeID = userTypeID;
            Name = name;
            Email = email;
            Password = password;
            JobID = jobID;
            ListCompetences = listCompetences;
        }
    }
}
