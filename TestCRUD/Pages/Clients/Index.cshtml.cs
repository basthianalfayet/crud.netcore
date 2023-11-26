using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TestCRUD.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> ListCLients = new List<ClientInfo>();

        public string SearchTerm { get; set; }

        public void OnGet()
        {
            try
            {
                string connectionstring = "Data Source=.\\sqlexpress;Initial Catalog=task_manager;Integrated Security=True";


				using (SqlConnection connection = new SqlConnection(connectionstring)) 
                {
                    connection.Open();
                    string sql = "SELECT * FROM tasks";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.title = reader.GetString(1);
                                clientInfo.description = reader.GetString(2);
                                clientInfo.status = reader.GetString(3);
                                clientInfo.created_at = reader.GetDateTime(4).ToString();
                                clientInfo.updated_at = reader.GetDateTime(5).ToString();

                                ListCLients.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ClientInfo 
    {
        public string id;
        public string title;
        public string description;
        public string status;
        public string created_at;
        public string updated_at;
    }
}
