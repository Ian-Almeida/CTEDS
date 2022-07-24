using AvaliacaoD3.Models;
using System.Data.SqlClient;

namespace AvaliacaoD3.Repositories
{
    internal class UserRepository : BaseRepository<User>
    {
        public string loginLogPath = "logs/login_log.txt";
        public UserRepository() : base("users", "Id")
        {
        }

        public User? Login(string email, string pwd)
        {
            string query = $"SELECT * FROM {base.tableName} WHERE Email=@Email AND Senha=@Password";
            SqlDataReader reader;
            User user = new();

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", pwd);
                    conn.Open();
                    reader = cmd.ExecuteReader();
                }

                
                while (reader.Read())
                {
                    
                    var fields = GetClassFields(user);

                    foreach (var field in fields.classFields)
                    {
                        if (field.Item2.FieldType.Name == "Guid")
                        {
                            field.Item2.SetValue(user, Guid.Parse(Convert.ToString(reader[field.Item1])));
                            continue;
                        }
                        field.Item2.SetValue(user, Convert.ChangeType(reader[field.Item1], field.Item2.FieldType));
                    };
                }
            }

            if (String.IsNullOrEmpty(user.Email)) return null;
            return user;
        }
    }
}
