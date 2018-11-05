using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cgiAPI.Models
{
    public class Employer : User
    {
        public List<Vacancy> ListVacancies { get; private set; }

        public Employer(int userID, int userTypeID, string name, string email, string password, int jobID, List<Skill> listCompetences) : base(userID, userTypeID, name, email, password, jobID, listCompetences)
        {
            UserID = userID;
            UserTypeID = userTypeID;
            Name = name;
            Email = email;
            Password = password;
            JobID = jobID;
            ListCompetences = listCompetences;
            ListVacancies = new List<Vacancy>();
        }

        //public void CreateVacancy(string name, string requiredJob, List<SkillType> reqCompetence, string description, DateTime beginDate, DateTime endDate)
        //{
        //    JobOffer job = new JobOffer(this, name, requiredJob, reqCompetence, description, beginDate, endDate);

        //}

        public void CreateVacancy(Vacancy jobOffer)
        {
            
        }

    }
}
