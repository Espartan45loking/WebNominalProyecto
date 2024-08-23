using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNominal.Controllers;
using WebNominal.Models;

namespace WebNominal.Controllers
{
    public class AccesoController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
        {
            if (ModelState.IsValid)
            {
                bool isAuthenticated = false;
                string mensaje = "";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_LoginUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre_usuario", oUsuario.usuario);
                        cmd.Parameters.AddWithValue("@contrasena", oUsuario.pass);

                        // Parámetros de salida
                        SqlParameter esAutenticadoParam = new SqlParameter("@es_autenticado", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(esAutenticadoParam);

                        SqlParameter mensajeParam = new SqlParameter("@mensaje", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(mensajeParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        isAuthenticated = (bool)esAutenticadoParam.Value;
                        mensaje = mensajeParam.Value.ToString();
                    }
                }

                if (isAuthenticated)
                {
                    // Redirigir al usuario a la página principal
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Mostrar mensaje de error
                    ViewBag.Mensaje = mensaje;
                    return View();
                }
            }

            return View();
        }
    }
}