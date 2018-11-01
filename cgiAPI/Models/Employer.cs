using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cgiAPI.Models;

namespace cgi.Controllers
{
    public class Employer : User
    {
        public List<JobOffer> ListVacancies { get; private set; }

        public Employer(int userID, string userType, string name, string email, string password, Job_Type job_type, List<SkillType> listCompetences, List<Availability> availability) : base(userID, userType, name, email, password, job_type, listCompetences, availability)
        {
            UserID = userID;
            UserType = userType;
            Name = name;
            Email = email;
            Password = password;
            Job_Type = job_type;
            ListCompetences = listCompetences;
            ListVacancies = new List<JobOffer>();

        }

        //public void CreateVacancy(string name, string requiredJob, List<SkillType> reqCompetence, string description, DateTime beginDate, DateTime endDate)
        //{
        //    JobOffer job = new JobOffer(this, name, requiredJob, reqCompetence, description, beginDate, endDate);

        //}

        public void CreateVacancy(JobOffer jobOffer)
        {
            
        }

    }
}
