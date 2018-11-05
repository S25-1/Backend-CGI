using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cgi.Controllers;

namespace cgiAPI.Models
{
    public class Employee : User
    {

        public List<JobOffer> ListAcceptedJob { get; private set; }


        public Employee(int userID, string userType, string name, string email, string password, Job_Type job_type, List<SkillType> listCompetences, List<Availability> availability) : base(userID, userType, name, email, password, job_type, listCompetences, availability)
        {
            UserID = userID;
            UserType = userType;
            Name = name;
            Email = email;
            Password = password;
            Job_Type = job_type;
            ListCompetences = listCompetences;
            Availability = availability;
            ListAcceptedJob = new List<JobOffer>();
        }
    }
}