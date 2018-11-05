using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace cgiAPI.Models
{
    public class Vacancy
    {
        public int VacancyID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public Job_Type Job { get; set; }
        public List<Skill> ReqCompetence { get; set; }
        public string Description { get; set; }
        public DateTime Date_begin { get; set; }
        public DateTime Date_end { get; set; }
        public int MinExperience { get; set; }
        public List<AcceptedUser> AcceptedUserList { get; set; }

        static private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mike\OneDrive\school\proftaak\project\cgiAPI\CGIdatabase.mdf;Integrated Security=True;Connect Timeout=30";
        static private SqlConnection conn = new SqlConnection(connectionString);

        //Database object
        [JsonConstructor]
        public Vacancy(int vacancyID, int userID, string name, Job_Type job, List<Skill> reqCompetence, string description, DateTime date_begin, DateTime date_end, int minExperience, List<AcceptedUser> acceptedUserList)
        {
            VacancyID = vacancyID;
            UserID = userID;
            Name = name;
            Job = job;
            ReqCompetence = reqCompetence;
            Description = description;
            Date_begin = date_begin;
            Date_end = date_end;
            MinExperience = minExperience;
            AcceptedUserList = acceptedUserList;
        }


        //Non-database object
        public Vacancy(int userID, string name, Job_Type job, List<Skill> reqCompetence, string description, DateTime date_begin, DateTime date_end, int minExperience, List<AcceptedUser> acceptedUserList)
        {
            VacancyID = -1;
            UserID = userID;
            Name = name;
            Job = job;
            ReqCompetence = reqCompetence;
            Description = description;
            Date_begin = date_begin;
            Date_end = date_end;
            MinExperience = minExperience;
            AcceptedUserList = acceptedUserList;
        }


        //public Vacancy(int vacancyID, int userID, string name, int requiredJob, List<SkillType> reqCompetence, DateTime dateBegin, DateTime dateEnd, string description, int minMonthsExperience)
        //{
        //    VacancyID = vacancyID;
        //    UserID = userID;
        //    Name = name;
        //    RequiredJob = requiredJob;
        //    ReqCompetence = reqCompetence;
        //    Description = description;
        //    DateBegin = dateBegin;
        //    DateEnd = dateEnd;
        //    MinMonthsExperience = minMonthsExperience;
        //}

        //static public bool AddVacancy(Vacancy Vacancy)
        //{
        //    using (SqlCommand command = new SqlCommand("INSERT INTO Vacancy (UserID, Job_TypeID, Date_begin, Date_end, Description, MinMonthsExperience) " +
        //           "VALUES (@UserID, @Job_TypeID, @Date_begin, @Date_end, @Description, @MinMonthsExperience)", conn))
        //    {
        //        command.Parameters.AddWithValue("@UserID", Vacancy.UserID);
        //        command.Parameters.AddWithValue("@Job_TypeID", Vacancy.RequiredJob);
        //        command.Parameters.AddWithValue("@Date_begin", Vacancy.DateBegin);
        //        command.Parameters.AddWithValue("@Date_end", Vacancy.DateEnd);
        //        command.Parameters.AddWithValue("@Description", Vacancy.Description);
        //        command.Parameters.AddWithValue("@MinMonthsExperience", Vacancy.MinMonthsExperience);

        //        conn.Open();

        //        int result = command.ExecuteNonQuery();
        //        conn.Close();
        //        if (result < 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        public static void AddVacancy(Vacancy Vacancy)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.Parameters.AddWithValue("@UserID", Vacancy.UserID);
                    command.Parameters.AddWithValue("@Job_TypeID", Vacancy.RequiredJob);
                    command.Parameters.AddWithValue("@Date_begin", Vacancy.DateBegin);
                    command.Parameters.AddWithValue("@Date_end", Vacancy.DateEnd);
                    command.Parameters.AddWithValue("@Description", Vacancy.Description);
                    command.Parameters.AddWithValue("@MinMonthsExperience", Vacancy.MinMonthsExperience);
                    command.Parameters.AddWithValue("@Name", Vacancy.Name);

                    command.CommandText =
                        "INSERT INTO Vacancy (UserID, Job_TypeID, Date_begin, Date_end, Description, MinMonthsExperience, Name) " + "VALUES (@UserID, @Job_TypeID, @Date_begin, @Date_end, @Description, @MinMonthsExperience, @Name)";
                    command.ExecuteNonQuery();

                    foreach (Skill skill in Vacancy.ReqCompetence)
                    {
                        command.CommandText =
                       "INSERT INTO Requested_Skill (Skill_ID, VacancyID) SELECT @SkillTypeID, VacancyID FROM Vacancy WHERE UserID=@UserID AND Job_TypeID=@Job_TypeID AND Date_begin=@Date_begin AND Date_end=@Date_end AND Description=@Description AND MinMonthsExperience=@MinMonthsExperience AND Name=@Name";
                        command.Parameters.AddWithValue("@SkillTypeID", skill.SkillTypeID);
                        command.ExecuteNonQuery();
                        command.Parameters.RemoveAt("@SkillTypeID");
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    Console.WriteLine("Both records are written to database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                }
            }
        }

        public static ArrayList GetListVacancy()
        {
            ArrayList vacancyList = new ArrayList();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "SELECT * FROM Vacancy";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Vacancy vacancy = new Vacancy(reader.GetInt32(0),reader.GetInt32(1), reader.GetString(7), reader.GetInt32(2), new List<Skill>(), reader.GetString(5), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetInt32(6));
                                vacancyList.Add(vacancy);
                            }
                        }
                    }

                    foreach (Vacancy v in vacancyList)
                    {
                        v.ReqCompetence = new List<Skill>();
                        command.CommandText = "SELECT Skill.Skill_ID, Skill.Skill_Name FROM Requested_Skill, Skill WHERE Requested_Skill.Skill_ID = Skill.Skill_ID AND Requested_Skill.VacancyID = @VacancyID";
                        command.Parameters.AddWithValue("@VacancyID", v.VacancyID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Skill skill = new Skill(reader.GetInt32(0), reader.GetString(1));
                                    v.ReqCompetence.Add(skill);
                                }
                            }
                            command.Parameters.RemoveAt("@VacancyID");
                        }
                    }


                    // Attempt to commit the transaction.
                    transaction.Commit();

                    Console.WriteLine("Both records are written to database.");
                    return vacancyList;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    return vacancyList;
                }
            }
        }
    }
}
            
        
    

