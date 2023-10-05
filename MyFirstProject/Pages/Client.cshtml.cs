using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyFirstProject.Pages
{
    public class ClientModel : PageModel
    {
        public List<ClientInfo> items = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myfirstproject;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * from users";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo client = new ClientInfo();
                                client.id = reader.GetInt32(0);
                                client.name = reader.GetString(1);
                                client.lname = reader.GetString(2);
                                client.email = reader.GetString(3);
                                client.password = reader.GetString(4);

                                items.Add(client);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public class ClientInfo
        {
            public int id;
            public string name;
            public string lname;
            public string email;
            public string password;
        }
    }
}
