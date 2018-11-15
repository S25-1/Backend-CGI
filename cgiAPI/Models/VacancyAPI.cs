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
        public int userID { get; set; }
        public string name { get; set; }
        public int jobType { get; set; }
        public List<int> requiredSkills { get; set; }
        public string description { get; set; }
        public DateTime beginDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public int minimalExperience { get; set; }
        static private string connectionString = @"Server=cgi-matchup.database.windows.net;Database=MatchUp;User ID = cgi; Password=Fontys12345;Trusted_Connection=False;";
        //static private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mike\OneDrive\school\mike_backend\CGIdatabase.mdf;Integrated Security=True;Connect Timeout=30";
        static private SqlConnection conn = new SqlConnection(connectionString);

        [JsonConstructor]
        public VacancyAPI(int userID, string name, int jobType, List<int> requiredSkills, string description, DateTime beginDateTime, DateTime endDateTime, int minimalExperience)
        {
            this.userID = userID;
            this.name = name;
            this.jobType = jobType;
            this.requiredSkills = requiredSkills;
            this.description = description;
            this.beginDateTime = beginDateTime;
            this.endDateTime = endDateTime;
            this.minimalExperience = minimalExperience;
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
                        command.Parameters.AddWithValue("@UserID", Vacancy.userID);
                        command.Parameters.AddWithValue("@Job_TypeID", Vacancy.jobType);
                        command.Parameters.AddWithValue("@Date_begin", Vacancy.beginDateTime);
                        command.Parameters.AddWithValue("@Date_end", Vacancy.endDateTime);
                        command.Parameters.AddWithValue("@Description", Vacancy.description);
                        command.Parameters.AddWithValue("@MinMonthsExperience", Vacancy.minimalExperience);
                        command.Parameters.AddWithValue("@Name", Vacancy.name);

                        command.CommandText =
                            "INSERT INTO Vacancy (UserID, Job_TypeID, Date_begin, Date_end, Description, MinMonthsExperience, Name) " + "VALUES (@UserID, @Job_TypeID, @Date_begin, @Date_end, @Description, @MinMonthsExperience, @Name)";
                        command.ExecuteNonQuery();

                        foreach (int skillID in Vacancy.requiredSkills)
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
        }
    }
