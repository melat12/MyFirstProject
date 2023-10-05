using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyFirstProject.Pages
{
    public class registration_pageModel : PageModel
    {
        public bool hasData = false;
        public string errorMessage = "";
        public string errorMessage2 = "";
        public string successMessage = "";
        public UserLoginInfo userInfo  = new UserLoginInfo();

        public void OnGet()
        {
        }
        public void OnPost() {
            hasData = true;
            userInfo.name = Request.Form["name"];
            userInfo.lname = Request.Form["lname"];
            userInfo.email = Request.Form["email"];
            userInfo.password = Request.Form["password"];
            userInfo.confPassword = Request.Form["confpassword"];

            
            if(userInfo.name.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            // add to database

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myfirstproject;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO users" +
                                 "(name,lname,email,password) VALUES" +
                                 "(@name, @lname, @email, @password);";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userInfo.name);
                        command.Parameters.AddWithValue("@lname", userInfo.lname);
                        command.Parameters.AddWithValue("@email", userInfo.email);

                        command.Parameters.AddWithValue("@password", userInfo.password);


                        command.ExecuteNonQuery();
                    }
                }
            } catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            userInfo.name = ""; userInfo.lname = ""; userInfo.email = "";
            userInfo.password = "";
            successMessage = "Added successfully ";
            Response.Redirect("/Client");

        }

        public class UserLoginInfo
        {
            public string name;
            public string lname;
            public string email;
            public string password;
            public string confPassword;

        }

    }
}
