using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public static class Connection
    {
        public static string connectionString { get; set; } = @"Server=cgi-matchup.database.windows.net;Database=MatchUp;User ID = cgi; Password=Fontys12345;Trusted_Connection=False;";
        //static private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mike\OneDrive\school\mike_backend\CGIdatabase.mdf;Integrated Security=True;Connect Timeout=30";
    }
}
