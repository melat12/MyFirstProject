using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyFirstProject.Pages
{
    public class loginModel : PageModel
    {
        public string errorMessage = "";
        public string successMessage = "";
        public ClientInfo items = new ClientInfo();

        public string dbemail = "";
        public string dbpassword = "";
        public string wbemail = "";
        public string wbpassword = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
           
        }

        public class ClientInfo
        {
            public bool hasData;
            public bool loggedIn;
            public string email;
            public string password;
            public string confPassword;
        }
        public void OnPost()
        {
            int id = 4;

            items.email = Request.Form["email"];
            items.password = Request.Form["password"];
            items.confPassword = Request.Form["confpassword"];

            wbemail = items.email;
            wbpassword = items.password;

            if (items.email.Length == 0 || items.password.Length == 0)
            {
                errorMessage = "Please enter email and password to login";
                return;
            }

            try
            {
                
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myfirstproject;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * from users";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())

                        {


                            while (reader.Read())
                            {
                                for (int i = 0; i <= 5; i++)
                                {
                                    successMessage = ""+i;
                                    
                                        if (reader.GetString(i).Equals(wbemail))
                                        {-
                                            successMessage = "why";
                                        }
                                    
                                    dbemail = reader.GetString(3);
                                    dbpassword = reader.GetString(4);

                                    if (wbemail.Equals(dbemail))
                                    {
                                        if (wbpassword.Equals(dbpassword))
                                        {
                                            successMessage = "Successfully logged in!";
                                            successMessage = dbpassword + items.password;
                                            Response.Redirect("/Index");
                                        }

                                    }



                                }

                            }

                        }
                    }
                }
            }
            catch { }


        }
    }
}
