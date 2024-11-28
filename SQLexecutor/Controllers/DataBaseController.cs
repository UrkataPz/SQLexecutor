using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace SQLexecutor.Controllers
{
    public class DataBaseController : Controller
    {
        private readonly string _connectionString;

        public IActionResult ExecuteQuery()
        {
            return View(); // Este es el GET, simplemente devuelve la vista sin hacer nada.
        }

        public DataBaseController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        //METODO PARA EJECUTAR LOS QUERY
        [HttpPost]
        public IActionResult ExecuteQuery(string sqlQuery)
        {
            if (string.IsNullOrEmpty(sqlQuery))
            {
                ViewBag.Error = "El query no puede estar vacío.";
                return View();
            }

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(sqlQuery, connection);

                    //DETECTA EL TIPO DE CONSULTA POR LA PRIMERA PALABRA
                    if (sqlQuery.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                    {
                        // SELECT MUESTRA LOS DATOS EN TABLA
                        using (var reader = command.ExecuteReader())
                        {
                            var columns = new List<string>();
                            var results = new List<List<object>>();

                            // OBTIENE LOS NOMBRES DE LAS COLUMNAS
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                columns.Add(reader.GetName(i));
                            }

                            // LEE LA FILA DE DATOS
                            while (reader.Read())
                            {
                                var row = new List<object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row.Add(reader[i]);
                                }
                                results.Add(row);
                            }

                            // ASIGNACION A LA VISTA
                            ViewBag.Columns = columns;
                            ViewBag.Results = results;
                        }
                    }
                    else
                    {
                        // PARA LAS OTRAS CONSULTAS
                        int affectedRows = command.ExecuteNonQuery();
                        ViewBag.Message = $"{affectedRows} filas afectadas.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al ejecutar el query: {ex.Message}";
            }

            return View();
        }



    }
}
