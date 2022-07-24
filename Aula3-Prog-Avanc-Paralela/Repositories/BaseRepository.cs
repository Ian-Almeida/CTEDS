using Aula3_Prog_Avanc_Paralela.Interfaces;
using System.Data.SqlClient;
using System.Reflection;

namespace Aula3_Prog_Avanc_Paralela.Repositories
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : new()
    {
        private readonly string CONNECTION_STRING = "Server=localhost;Database=Tests;Trusted_Connection=True;";

        private string tableName = string.Empty;

        private string idField = string.Empty;

        public BaseRepository(string tableName, string idField) { 
            this.tableName = tableName; 
            this.idField = idField;
        }

        public Fields GetClassFields(T payload)
        {
            var fieldsInfos = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);


            List<Tuple<string, FieldInfo>> classFields = new();

            foreach (var fieldInfo in fieldsInfos)
            {
                var fieldName = fieldInfo.Name.Split(">")[0].Replace("<", "");
                
                if (fieldInfo != null)
                {
                    classFields.Add(new Tuple<string, FieldInfo>(fieldName, fieldInfo));
                }
            }

            return new Fields(classFields);
        }

        public T FindById(string IdPayload)
        {
            List<T> itemsList = new();

            string query = $"SELECT * FROM {this.tableName} WHERE {this.idField}=@IdValue";

            SqlDataReader reader;

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdValue", IdPayload);
                    conn.Open();
                    reader = cmd.ExecuteReader();
                }

                while (reader.Read())
                {
                    T newItem = new();
                    var fields = GetClassFields(newItem);

                    foreach (var field in fields.classFields)
                    {
                        field.Item2.SetValue(newItem, Convert.ChangeType(reader[field.Item1], field.Item2.FieldType));
                    };
                    itemsList.Add(newItem);
                }
            }

            return itemsList[0];
        }

        public List<T> ReadAll()
        {
            List<T> itemsList = new();

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                string query = $"SELECT * FROM {this.tableName}";

                SqlDataReader reader;

                using (SqlCommand cmd = new(query, conn))
                {
                    conn.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        T newItem = new();
                        var fields = GetClassFields(newItem);

                        foreach (var field in fields.classFields)
                        {
                            field.Item2.SetValue(newItem, Convert.ChangeType(reader[field.Item1], field.Item2.FieldType));
                        };
                        itemsList.Add(newItem);
                    }
                }
            }

            return itemsList;
        }

        public void Create(T payload)
        {

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                var fields = GetClassFields(payload);

                string sqlFields = "";
                string sqlFieldsParams = "";

                foreach (var field in fields.classFields)
                {
                    sqlFields += (field.Item1 + ",");

                    string param = ("@" + field.Item1 + ",");

                    if (field.Item2.FieldType.Name == "String") param = ("@" + field.Item1 + ",");

                    sqlFieldsParams += param;
                }
                sqlFields = sqlFields.Substring(0, sqlFields.Length - 1);
                sqlFieldsParams = sqlFieldsParams.Substring(0, sqlFieldsParams.Length - 1);

                string query = $"INSERT INTO {this.tableName} ({sqlFields}) VALUES ({sqlFieldsParams})";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    foreach (var field in fields.classFields)
                    {
                        cmd.Parameters.AddWithValue("@" + field.Item1, field.Item2.GetValue(payload));
                    }
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

        }

        public void Update(T payload)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                var fields = GetClassFields(payload);
                var IdValue = fields.classFields.Find(x => x.Item1 == this.idField).Item2.GetValue(payload);

                string sqlUpdate = "";
                foreach (var field in fields.classFields)
                {
                    sqlUpdate += $"{field.Item1}=@{field.Item1},";
                }

                string query = $"UPDATE {this.tableName} SET {sqlUpdate.Substring(0, sqlUpdate.Length -1)} WHERE {this.idField}=@IdValue";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    foreach (var field in fields.classFields)
                    {
                        cmd.Parameters.AddWithValue("@" + field.Item1, field.Item2.GetValue(payload));
                    }
                    cmd.Parameters.AddWithValue("@IdValue", IdValue);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(string IdPayload)
        {
            string query = $"DELETE FROM {this.tableName} WHERE {this.idField}=@IdValue";

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdValue", IdPayload);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    class Fields
    {
        public List<Tuple<string, FieldInfo>> classFields { get; } = new List<Tuple<string, FieldInfo>>();

        public Fields(List<Tuple<string, FieldInfo>> classFields)
        {
            this.classFields = classFields;
        }
    }
}
