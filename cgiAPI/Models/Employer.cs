using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cgiAPI.Controllers
{
    public class Employer : User
    {
        public List<JobOffer> ListVacancies { get; private set; }

        public Employer(int userID, int userTypeID, string name, string email, string password, int jobID, List<SkillType> listCompetences) : base(userID, userTypeID, name, email, password, jobID, listCompetences)
        {
            UserID = userID;
            UserTypeID = userTypeID;
            Name = name;
            Email = email;
            Password = password;
            Job = job;
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
