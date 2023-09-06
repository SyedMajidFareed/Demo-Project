using Microsoft.Data.SqlClient;
namespace Website.Models
{
    public class UserRepository
    {
        public static void addUser(User user)
        {
            

            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebsiteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to insert data
            string query = $"insert into UserTable(Username, Password) " +
                           $"values('{user.Username}','{user.Password}')";
            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            int alteredrows = cmd.ExecuteNonQuery();
            connection.Close();

        }
        public static List<User> getAllUsers()
        {
            List<User> users = new List<User>();
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebsiteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to fetxh data
            string query = $"select * from UserTable";
            SqlCommand cmd = new SqlCommand(query, connection);

            
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                User user = new User();
                user.Username = Convert.ToString(dr[1]);
                user.Password = Convert.ToString(dr[2]);
                
                users.Add(user);
            }
            connection.Close();
            return users;
        }
        public static User GetUser(User user)
        {
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebsiteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to fetxh data
            string query = $"select * from UserTable where Username = @U AND Password = @P";
            SqlCommand cmd = new SqlCommand(query, connection);

            //defining parameters
            SqlParameter p1 = new SqlParameter("U", user.Username);
            SqlParameter p2 = new SqlParameter("P", user.Password);

            //adding parameter
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                connection.Close();
                return user;
            }
            else
            {
                connection.Close();
                return null;
            }    
            
        }
    }
}
