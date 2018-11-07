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
    public class Vacancy
    {
        public int VacancyID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public Job_Type Job { get; set; }
        public List<Skill> SkillList { get; set; }
        public string Description { get; set; }
        public DateTime Date_begin { get; set; }
        public DateTime Date_end { get; set; }
        public int MinExperience { get; set; }
        public List<AcceptedUser> AcceptedUserList { get; set; }
        static private string connectionString = @"Server=cgi-matchup.database.windows.net;Database=MatchUp;User ID = cgi; Password=Fontys12345;Trusted_Connection=False;";
        //static private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mike\OneDrive\school\mike_backend\CGIdatabase.mdf;Integrated Security=True;Connect Timeout=30";
        static private SqlConnection conn = new SqlConnection(connectionString);

        [JsonConstructor]
        public Vacancy(int vacancyID, int userID, string name, Job_Type job, List<Skill> skillList, string description, DateTime date_begin, DateTime date_end, int minExperience, List<AcceptedUser> acceptedUserList)
        {
            VacancyID = vacancyID;
            UserID = userID;
            Name = name;
            Job = job;
            SkillList = skillList;
            Description = description;
            Date_begin = date_begin;
            Date_end = date_end;
            MinExperience = minExperience;
            AcceptedUserList = acceptedUserList;
        }
        public Vacancy(string name)
        {
            Name = name;
        }

        public Vacancy(int userID, string name, Job_Type job, List<Skill> skillList, string description, DateTime date_begin, DateTime date_end, int minExperience)
        {
            VacancyID = -1;
            UserID = userID;
            Name = name;
            Job = job;
            SkillList = skillList;
            Description = description;
            Date_begin = date_begin;
            Date_end = date_end;
            MinExperience = minExperience;
            AcceptedUserList = new List<AcceptedUser>();
        }


        public static bool AddAcceptedUser(AcceptedUser user)
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
                    command.Parameters.AddWithValue("@VacancyID", user.VacancyID);
                    command.Parameters.AddWithValue("@UserID", user.UserID);
                    command.Parameters.AddWithValue("@Accepted", user.Accepted);
                    command.CommandText =
                        "INSERT INTO AcceptedUser (UserID, VacancyID, Accepted) " + "VALUES (@VacancyID, @UserID, @Accepted)";
                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    Console.WriteLine("Both records are written to database.");
                    return true;
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
                return false;
                }
            }
        }

        public static ArrayList GetListAcceptedUser()
        {
            ArrayList acceptedUserList = new ArrayList();

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
                    command.CommandText = "SELECT * FROM AcceptedUser";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                AcceptedUser acceptedUser = new AcceptedUser(reader.GetInt32(0), reader.GetInt32(1), reader.GetBoolean(2));
                                acceptedUserList.Add(acceptedUser);
                            }
                        }
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    Console.WriteLine("Both records are written to database.");

                    return acceptedUserList;

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
                    return acceptedUserList;
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
                    command.CommandText = "SELECT * FROM dbo.Vacancy";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Vacancy vacancy = new Vacancy(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(7), new Job_Type(reader.GetInt32(2), "null"), new List<Skill>(), reader.GetString(5), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetInt32(6), new List<AcceptedUser>());
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
        //public static ArrayList GetListVacancy()
        //{
        //    ArrayList vacancyList = new ArrayList();

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        SqlCommand command = connection.CreateCommand();
        //        SqlTransaction transaction;

        //        // Start a local transaction.
        //        transaction = connection.BeginTransaction("SampleTransaction");

        //        // Must assign both transaction object and connection
        //        // to Command object for a pending local transaction
        //        command.Connection = connection;
        //        command.Transaction = transaction;

        //        try
        //        {
        //            command.CommandText = "SELECT * FROM dbo.Vacancy";
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        Vacancy vacancy = new Vacancy(reader.GetString(7));
        //                        vacancyList.Add(vacancy);
        //                    }
        //                }
        //            }



        //            //foreach (Vacancy v in vacancyList)
        //            //{
        //            //    v.SkillList = new List<Skill>();

        //            //    command.CommandText = "SELECT Skill.Skill_ID, Skill.Skill_Name FROM Skill_Vacancy, Skill WHERE Skill_Vacancy.Skill_ID = Skill.Skill_ID AND Skill_Vacancy.VacancyID = @VacancyID";
        //            //    command.Parameters.AddWithValue("@VacancyID", v.VacancyID);
        //            //    using (SqlDataReader reader = command.ExecuteReader())
        //            //    {
        //            //        if (reader.HasRows)
        //            //        {
        //            //            while (reader.Read())
        //            //            {
        //            //                Skill skill = new Skill(reader.GetInt32(0), reader.GetString(1));
        //            //                v.SkillList.Add(skill);
        //            //            }
        //            //        }
        //            //        command.Parameters.RemoveAt("@VacancyID");
        //            //    }

        //            //    command.Parameters.AddWithValue("@Job_TypeID", v.Job.Job_typeID);
        //            //    command.CommandText = "SELECT * FROM Job_Type WHERE Job_TypeID = @Job_TypeID";
        //            //    using (SqlDataReader reader = command.ExecuteReader())
        //            //    {
        //            //        if (reader.HasRows)
        //            //        {
        //            //            while (reader.Read())
        //            //            {
        //            //                Job_Type job = new Job_Type(reader.GetInt32(0), reader.GetString(1));
        //            //                v.Job = job;
        //            //            }
        //            //        }
        //            //        command.Parameters.RemoveAt("@Job_TypeID");
        //            //    }
        //            //}


        //            // Attempt to commit the transaction.
        //            transaction.Commit();

        //            Console.WriteLine("Both records are written to database.");
        //            return vacancyList;

        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
        //            Console.WriteLine("  Message: {0}", ex.Message);

        //            // Attempt to roll back the transaction.
        //            try
        //            {
        //                transaction.Rollback();
        //            }
        //            catch (Exception ex2)
        //            {
        //                // This catch block will handle any errors that may have occurred
        //                // on the server that would cause the rollback to fail, such as
        //                // a closed connection.
        //                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
        //                Console.WriteLine("  Message: {0}", ex2.Message);
        //            }
        //            return vacancyList;
        //        }
        //    }}
    }
        }
            
        
    

