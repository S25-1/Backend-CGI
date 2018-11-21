using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace cgiAPI.Models
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VacancyAPI
    {
        public int VacancyID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public int JobType { get; set; }
        public List<int> RequiredSkills { get; set; }
        public string Description { get; set; }
        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int MinimalExperience { get; set; }
        static private string connectionString = @"Server=cgi-matchup.database.windows.net;Database=MatchUp;User ID = cgi; Password=Fontys12345;Trusted_Connection=False;";
        //static private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mike\OneDrive\school\mike_backend\CGIdatabase.mdf;Integrated Security=True;Connect Timeout=30";
        static private SqlConnection conn = new SqlConnection(connectionString);

        [JsonConstructor]
        public VacancyAPI(int userID, string name, int jobType, List<int> requiredSkills, string description, DateTime beginDateTime, DateTime endDateTime, int minimalExperience)
        {
            this.VacancyID = -1;
            this.UserID = userID;
            this.Name = name;
            this.JobType = jobType;
            this.RequiredSkills = requiredSkills;
            this.Description = description;
            this.BeginDateTime = beginDateTime;
            this.EndDateTime = endDateTime;
            this.MinimalExperience = minimalExperience;
        }

        public VacancyAPI(int vacancyID, int userID, string name, int jobType, List<int> requiredSkills, string description, DateTime beginDateTime, DateTime endDateTime, int minimalExperience)
        {
            this.VacancyID = vacancyID;
            this.UserID = userID;
            this.Name = name;
            this.JobType = jobType;
            this.RequiredSkills = requiredSkills;
            this.Description = description;
            this.BeginDateTime = beginDateTime;
            this.EndDateTime = endDateTime;
            this.MinimalExperience = minimalExperience;
        }

        public static string AddVacancy(VacancyAPI Vacancy)
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
                    command.Parameters.AddWithValue("@Job_TypeID", Vacancy.JobType);
                    command.Parameters.AddWithValue("@Date_begin", Vacancy.BeginDateTime);
                    command.Parameters.AddWithValue("@Date_end", Vacancy.EndDateTime);
                    command.Parameters.AddWithValue("@Description", Vacancy.Description);
                    command.Parameters.AddWithValue("@MinMonthsExperience", Vacancy.MinimalExperience);
                    command.Parameters.AddWithValue("@Name", Vacancy.Name);

                    command.CommandText =
                        "INSERT INTO Vacancy (UserID, Job_TypeID, Date_begin, Date_end, Description, MinMonthsExperience, Name) " + "VALUES (@UserID, @Job_TypeID, @Date_begin, @Date_end, @Description, @MinMonthsExperience, @Name)";
                    command.ExecuteNonQuery();

                    foreach (int skillID in Vacancy.RequiredSkills)
                    {
                        command.CommandText =
                        "INSERT INTO Skill_Vacancy (Skill_ID, VacancyID) SELECT @SkillTypeID, VacancyID FROM Vacancy WHERE UserID=@UserID AND Job_TypeID=@Job_TypeID AND Date_begin=@Date_begin AND Date_end=@Date_end AND Description=@Description AND MinMonthsExperience=@MinMonthsExperience AND Name=@Name";
                        command.Parameters.AddWithValue("@SkillTypeID", skillID);
                        command.ExecuteNonQuery();
                        command.Parameters.RemoveAt("@SkillTypeID");
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    Console.WriteLine("Both records are written to database.");
                    return "Record added to the database";
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
                    return ex.Message;
                }
            }
        }

        static public void AcceptEmployeeVacancy(List<RespondVacancyUser> employees)
        {

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
                    command.CommandText = "SELECT * FROM dbo.Vacancy";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                VacancyAPI vacancy = new VacancyAPI(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), new Job_Type(reader.GetInt32(2), "null"), new List<Skill>(), reader.GetString(5), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetInt32(6), new List<RespondVacancyUser>());
                                vacancyList.Add(vacancy);
                            }
                        }
                    }

                    foreach (Vacancy v in vacancyList)
                    {
                        v.SkillList = new List<Skill>();

                        command.CommandText = "SELECT Skill.Skill_ID, Skill.Skill_Name FROM Skill_Vacancy, Skill WHERE Skill_Vacancy.Skill_ID = Skill.Skill_ID AND Skill_Vacancy.VacancyID = @VacancyID";
                        command.Parameters.AddWithValue("@VacancyID", v.VacancyID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Skill skill = new Skill(reader.GetInt32(0), reader.GetString(1));
                                    v.SkillList.Add(skill);
                                }
                            }
                            command.Parameters.RemoveAt("@VacancyID");
                        }

                        command.Parameters.AddWithValue("@Job_TypeID", v.Job.Job_typeID);
                        command.CommandText = "SELECT * FROM Job_Type WHERE Job_TypeID = @Job_TypeID";
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Job_Type job = new Job_Type(reader.GetInt32(0), reader.GetString(1));
                                    v.Job = job;
                                }
                            }
                            command.Parameters.RemoveAt("@Job_TypeID");
                        }

                        v.RespondVacancyUserList = new List<RespondVacancyUser>();

                        command.CommandText = "SELECT * FROM dbo.AcceptedUser WHERE VacancyID = @VacancyID";
                        command.Parameters.AddWithValue("@VacancyID", v.VacancyID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    v.RespondVacancyUserList.Add(new RespondVacancyUser(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
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

        public static ArrayList GetListVacancy(int vacancyID)
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
                    command.Parameters.AddWithValue("@VacancyID", vacancyID);
                    command.CommandText = "SELECT * FROM dbo.Vacancy WHERE VacancyID = @VacancyID";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                VacancyAPI vacancy = new VacancyAPI(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(7), new Job_Type(reader.GetInt32(2), "null"), new List<Skill>(), reader.GetString(5), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetInt32(6), new List<RespondVacancyUser>());
                                vacancyList.Add(vacancy);
                            }
                        }
                    }

                    foreach (Vacancy v in vacancyList)
                    {
                        v.SkillList = new List<Skill>();

                        command.CommandText = "SELECT Skill.Skill_ID, Skill.Skill_Name FROM Skill_Vacancy, Skill WHERE Skill_Vacancy.Skill_ID = Skill.Skill_ID AND Skill_Vacancy.VacancyID = @VacancyID";
                        command.Parameters.AddWithValue("@VacancyID", v.VacancyID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Skill skill = new Skill(reader.GetInt32(0), reader.GetString(1));
                                    v.SkillList.Add(skill);
                                }
                            }
                            command.Parameters.RemoveAt("@VacancyID");
                        }

                        command.Parameters.AddWithValue("@Job_TypeID", v.Job.Job_typeID);
                        command.CommandText = "SELECT * FROM Job_Type WHERE Job_TypeID = @Job_TypeID";
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Job_Type job = new Job_Type(reader.GetInt32(0), reader.GetString(1));
                                    v.Job = job;
                                }
                            }
                            command.Parameters.RemoveAt("@Job_TypeID");
                        }

                        v.RespondVacancyUserList = new List<RespondVacancyUser>();

                        foreach (RespondVacancyUser item in GetListRespondVacancyUser(v.VacancyID))
                        {
                            v.RespondVacancyUserList.Add(item);
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

        static public ArrayList GetListRespondVacancyUser(int userID, int vacancyID, int statusID)
        {
            ArrayList RespondVacancyUserList = new ArrayList();

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
                    command.Parameters.AddWithValue("@VacancyID", vacancyID);
                    command.Parameters.AddWithValue("@StatusID", statusID);

                    command.CommandText = "SELECT * FROM AcceptedUser WHERE VacancyID = @VacancyID AND StatusID = @StatusID";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                RespondVacancyUser RespondVacancyUser = new RespondVacancyUser(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                                RespondVacancyUserList.Add(RespondVacancyUser);
                            }
                        }
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    Console.WriteLine("Both records are written to database.");

                    return RespondVacancyUserList;

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
                    return RespondVacancyUserList;
                }
            }
        }

        static public ArrayList GetListRespondVacancyUser(int vacancyID)
        {
            ArrayList RespondVacancyUserList = new ArrayList();

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
                    command.Parameters.AddWithValue("@VacancyID", vacancyID);

                    command.CommandText = "SELECT * FROM AcceptedUser WHERE VacancyID = @VacancyID";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                RespondVacancyUser RespondVacancyUser = new RespondVacancyUser(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                                RespondVacancyUserList.Add(RespondVacancyUser);
                            }
                        }
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    Console.WriteLine("Both records are written to database.");

                    return RespondVacancyUserList;

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
                    return RespondVacancyUserList;
                }
            }
        }
    }
}
