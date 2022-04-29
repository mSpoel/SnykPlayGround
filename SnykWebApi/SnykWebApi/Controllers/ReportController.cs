using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace SnykWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        [HttpPost("download")]
        public IActionResult DownloadReports([FromBody] string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return NotFound();
            }

            string dirName = Path.Combine(path, "Reports");

            var files = Directory.GetFiles(dirName);

            return Ok(files);
        }

        [HttpGet("sqlInjection")]
        public bool LoginIsValid_INSECURE_EXAMPLE(string account, string password)
        {
            using (SqlConnection connection = new SqlConnection("connection_string"))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT COUNT(*) FROM [User] WHERE [Account] = '" + account + "' AND [Password] = '" + password + "'";

                    connection.Open();

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        [HttpGet("sqlInjectionSolved")]
        public bool LoginIsValid_SECURE_EXAMPLE(string account, string password)
        {
            using (SqlConnection connection = new SqlConnection("connection_string"))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT COUNT(*) FROM [User] WHERE [Account] = @Account AND [Password] = @Password";

                    command.Parameters.Add(new SqlParameter("@Account", account));
                    command.Parameters.Add(new SqlParameter("@Password", password));

                    connection.Open();

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        [HttpGet("passwordleak")]
        public bool Password(string account, string password)
        {
            using (var connection = new SqlConnection("Server=myServerAddress;Database=myDataBase;User Id=username;Password=45jclkdjr323;"))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT COUNT(*) FROM [User] WHERE [Account] = @Account AND [Password] = @Password";

                    command.Parameters.Add(new SqlParameter("@Account", account));
                    command.Parameters.Add(new SqlParameter("@Password", password));

                    connection.Open();

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        return true;
                    }
                }

                return false;

            }
        }
    }
}
