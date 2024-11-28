using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace SQLexecutor.Controllers
{
    public class TestController : Controller
    {
        private readonly string _connectionString;

        public TestController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult TestConnection()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                }
                return Content("¡Conexión exitosa con la base de datos!");
            }
            catch (Exception ex)
            {
                return Content($"Error al conectar con la base de datos: {ex.Message}");
            }
        }
    }
}
