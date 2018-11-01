using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using cgiAPI.Models;

namespace cgi.Controllers
{
    public class JobOffer
    {
        public int JobOfferID { get; set; }
        public Employer JobOfferOwner { get; set; }
        public string Name { get; set; }
        public Job_Type Job_Type { get; set; }
        public List<SkillType> ReqCompetence { get; set; }
        public string Description { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public int MinMonthsExperience { get; set; }
        public List<User> ListUserAccepted { get; set; }
        public List<User> ListUserOffered { get; set; }

        static private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mike\OneDrive\school\proftaak\cgi\CGIdatabase.mdf;Integrated Security = True; Connect Timeout = 30";
        static private SqlConnection conn = new SqlConnection(connectionString);

        //Non-database object
        public JobOffer(Employer employer, string name, Job_Type job_type, List<SkillType> reqCompetence, string description, DateTime dateBegin, DateTime dateEnd, int minMonthsExperience)
        {
            JobOfferID = -1;
            JobOfferOwner = employer;
            Name = name;
            Job_Type = job_type;
            ReqCompetence = ReqCompetence;
            Description = description;
            DateBegin = dateBegin;
            DateEnd = dateEnd;
            MinMonthsExperience = minMonthsExperience;
        }

        //Database object
        public JobOffer(int jobOfferID, Employer jobOfferOwner, string name, Job_Type job_type, List<SkillType> reqCompetence, string description, DateTime dateBegin, DateTime dateEnd, int minMonthsExperience)
        {
            JobOfferID = jobOfferID;
            JobOfferOwner = jobOfferOwner;
            Name = name;
            Job_Type = job_type;
            ReqCompetence = reqCompetence;
            Description = description;
            DateBegin = dateBegin;
            DateEnd = dateEnd;
            MinMonthsExperience = minMonthsExperience;
        }

        static public bool AddJobOffer(User user, JobOffer jobOffer)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Vacancy (UserID, JobTypeID, Date_begin, Date_end, Description, MinMonthsExperience) " +
                   "VALUES (@UserID, @JobTypeID, @Date_begin, @Date_end, @Description, @MinMonthsExperience)", conn))
            {
                command.Parameters.AddWithValue("@UserID", user.UserID);
                command.Parameters.AddWithValue("@JobTypeID", jobOffer.Job_Type);
                command.Parameters.AddWithValue("@Date_begin", jobOffer.DateBegin);
                command.Parameters.AddWithValue("@Date_end", jobOffer.DateEnd);
                command.Parameters.AddWithValue("@Description", jobOffer.Description);
                command.Parameters.AddWithValue("@MinMonthsExperience", jobOffer.MinMonthsExperience);

                conn.Open();

                int result = command.ExecuteNonQuery();
                conn.Close();
                if (result < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
