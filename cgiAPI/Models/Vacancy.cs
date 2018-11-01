using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace cgiAPI.Controllers
{
    public class Vacancy
    {
        public int VacancyID { get; set; }
        //public Employer VacancyOwner { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public int RequiredJob { get; set; }
        public List<SkillType> ReqCompetence { get; set; }
        public string Description { get; set; }
        public string DateBegin { get; set; }
        public string DateEnd { get; set; }
        public int MinMonthsExperience { get; set; }
        //public List<User> ListUserAccepted { get; set; }
        //public List<User> ListUserOffered { get; set; }

        static private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mike\OneDrive\school\proftaak\project\cgiAPI\CGIdatabase.mdf;Integrated Security = True; Connect Timeout = 30";
        static private SqlConnection conn = new SqlConnection(connectionString);

        //Non-database object
        public Vacancy(int userID, string name, int requiredJob, List<SkillType> reqCompetence, string description, string dateBegin, string dateEnd, int minMonthsExperience)
        {
            VacancyID = -1;
            UserID = userID;
            Name = name;
            RequiredJob = requiredJob;
            ReqCompetence = ReqCompetence;
            Description = description;
            DateBegin = dateBegin;
            DateEnd = dateEnd;
            MinMonthsExperience = minMonthsExperience;
        }

        //Database object
        public Vacancy(int vacancyID, int userID, string name, int requiredJob, List<SkillType> reqCompetence, string description, string dateBegin, string dateEnd, int minMonthsExperience)
        {
            VacancyID = vacancyID;
            UserID = userID;
            Name = name;
            RequiredJob = requiredJob;
            ReqCompetence = reqCompetence;
            Description = description;
            DateBegin = dateBegin;
            DateEnd = dateEnd;
            MinMonthsExperience = minMonthsExperience;
        }

        static public bool AddVacancy(Vacancy Vacancy)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Vacancy (UserID, Job_TypeID, Date_begin, Date_end, Description, MinMonthsExperience) " +
                   "VALUES (@UserID, @Job_TypeID, @Date_begin, @Date_end, @Description, @MinMonthsExperience)", conn))
            {
                command.Parameters.AddWithValue("@UserID", Vacancy.UserID);
                command.Parameters.AddWithValue("@Job_TypeID", Vacancy.RequiredJob);
                command.Parameters.AddWithValue("@Date_begin", Vacancy.DateBegin);
                command.Parameters.AddWithValue("@Date_end", Vacancy.DateEnd);
                command.Parameters.AddWithValue("@Description", Vacancy.Description);
                command.Parameters.AddWithValue("@MinMonthsExperience", Vacancy.MinMonthsExperience);

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
