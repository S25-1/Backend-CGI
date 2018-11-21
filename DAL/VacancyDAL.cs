using cgiAPI.Models;
using System;
using System.Collections;
using System.Data.SqlClient;

namespace DAL
{
    public class VacancyDAL
    {
        public static string AddVacancy(VacancyAPI Vacancy)
        {
            using (SqlConnection connection = new SqlConnection(Connection.connectionString))
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


        public static ArrayList GetListAcceptedUser(int vacancyID, int statusID)
        {
            ArrayList acceptedUserList = new ArrayList();

            using (SqlConnection connection = new SqlConnection(Connection.connectionString))
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
                                AcceptedUser acceptedUser = new AcceptedUser(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
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
    }
}
}
