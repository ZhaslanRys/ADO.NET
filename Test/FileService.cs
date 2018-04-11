using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class FileService:BaseService
    {
        public FileService(ConnectionStringSettings connectionString) : base(connectionString) { }
        public void Create(File file)
        {
            using (SqlConnection connection = new SqlConnection("ConnectionString"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO Images VALUES (@FileName, @Title, @ImageData)";
                command.Parameters.Add("@FileName", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@Title", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@ImageData", SqlDbType.Image, 1000000);

                string shortFileName = file.Name_file.Substring(file.Name_file.LastIndexOf('\\') + 1);

                using (System.IO.FileStream fs = new System.IO.FileStream(file.Name_file, FileMode.Open))
                {
                    file.Data_file = new byte[fs.Length];
                    fs.Read(file.Data_file, 0, file.Data_file.Length);
                }
                // передаем данные в команду через параметры
                command.Parameters["@FileName"].Value = shortFileName;
                command.Parameters["@Title"].Value = file.Content_file;
                command.Parameters["@ImageData"].Value = file.Data_file;

                command.ExecuteNonQuery();
            }
        }
        public List<Person> SelectAll()
        {
            using (SqlConnection connection = new SqlConnection("ConnectionString"))
            {
                connection.Open();
                string sql = "SELECT * FROM Images";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string filename = reader.GetString(1);
                    string title = reader.GetString(2);
                    byte[] data = (byte[])reader.GetValue(3);
                }
            }
        }
    }
}
