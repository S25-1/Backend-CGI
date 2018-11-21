using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cgiAPI.Models
{
    public class Employer : User
    {
        public List<Vacancy> ListVacancies { get; private set; }

        public Employer(int userID, int userTypeID, string name, string email, string password, DateTime dateOfBirth, string phoneNumber, Address address, Job_Type job, decimal hourly_wage, List<Skill> skillList, Branch branch, List<Availability> availabilityList) : base(userID, userTypeID, name, email, password, dateOfBirth, phoneNumber, address, job, hourly_wage, skillList, branch, availabilityList)
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

        //public void CreateVacancy(string name, string requiredJob, List<SkillType> reqCompetence, string description, DateTime beginDate, DateTime endDate)
        //{
        //    JobOffer job = new JobOffer(this, name, requiredJob, reqCompetence, description, beginDate, endDate);

        //}

        public void CreateVacancy(Vacancy jobOffer)
        {
            
        }

    }
}
