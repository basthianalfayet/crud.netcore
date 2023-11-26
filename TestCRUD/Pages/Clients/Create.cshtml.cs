using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TestCRUD.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientinfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {

        }

        public void OnPost() 
        {
            clientinfo.title = Request.Form["title"];
            clientinfo.description = Request.Form["description"];
            clientinfo.status = Request.Form["status"];

            if (clientinfo.title.Length == 0 || clientinfo.description.Length == 0 ||
                clientinfo.status.Length == 0) 
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new Task into the database

            try
            {
                string connectionsString = "Data Source=.\\sqlexpress;Initial Catalog=task_manager;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionsString)) 
                {
                    connection.Open();
                    string sql = "INSERT INTO tasks " +
                        "(title,description,status) VALUES" +
                        "(@title,@description,@status);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@title", clientinfo.title);
                        command.Parameters.AddWithValue("@description", clientinfo.description);
                        command.Parameters.AddWithValue("@status", clientinfo.status);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientinfo.title = ""; clientinfo.description = ""; clientinfo.status = "";
            successMessage = "New task added correctly";

            Response.Redirect("/Clients/Index");
        }
    }
}
