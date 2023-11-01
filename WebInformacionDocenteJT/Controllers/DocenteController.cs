using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInformacionDocenteJT.Models;

namespace WebInformacionDocenteJT.Controllers
{
    public class DocenteController : Controller
    {
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();

        private static List<Docente> oLista = new List<Docente>();

        // GET: Contacto
        public ActionResult Inicio()
        {

            oLista = new List<Docente>();

            using (SqlConnection oConexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM DOCENTE", oConexion);
                cmd.CommandType = CommandType.Text;
                oConexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Docente newDocente = new Docente();
                        newDocente.IdDocente = Convert.ToInt32(dr["IdDocente"]);
                        newDocente.Nombres = dr["Nombres"].ToString();
                        newDocente.Apellidos = dr["Apellidos"].ToString();
                        newDocente.Telefono = dr["Telefono"].ToString();
                        newDocente.Correo = dr["Correo"].ToString();
                        newDocente.Salario = Convert.ToDecimal(dr["Salario"]);
                        newDocente.FechaNacimiento = DateTime.Parse(dr["FechaNacimiento"].ToString());
                        oLista.Add(newDocente);
                    }
                }

            }

                return View(oLista);
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Docente oDocente)
        {

            using (SqlConnection oConexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Registrar", oConexion);
                cmd.Parameters.AddWithValue("Nombres", oDocente.Nombres);
                cmd.Parameters.AddWithValue("Apellidos", oDocente.Apellidos);
                cmd.Parameters.AddWithValue("Telefono", oDocente.Telefono);
                cmd.Parameters.AddWithValue("Correo", oDocente.Correo);
                cmd.Parameters.AddWithValue("Salario", oDocente.Salario);
                cmd.Parameters.AddWithValue("FechaNacimiento", oDocente.FechaNacimiento);
                cmd.CommandType = CommandType.StoredProcedure;
                oConexion.Open();

                cmd.ExecuteNonQuery();

            }

            return RedirectToAction("Inicio", "Docente");
        }

        [HttpGet]
        public ActionResult Editar(int? idDocente)
        {
            if (idDocente == null) return RedirectToAction("Inicio", "Docente");

            Docente oContacto = oLista.Where(c => c.IdDocente == idDocente).FirstOrDefault();

            return View(oContacto);
        }

        [HttpPost]
        public ActionResult EditContact(Docente oDocente)
        {

            using (SqlConnection oConexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Editar", oConexion);
                cmd.Parameters.AddWithValue("IdDocente", oDocente.IdDocente);
                cmd.Parameters.AddWithValue("Nombres", oDocente.Nombres);
                cmd.Parameters.AddWithValue("Apellidos", oDocente.Apellidos);
                cmd.Parameters.AddWithValue("Telefono", oDocente.Telefono);
                cmd.Parameters.AddWithValue("Correo", oDocente.Correo);
                cmd.Parameters.AddWithValue("Salario", oDocente.Salario);
                cmd.Parameters.AddWithValue("FechaNacimiento", oDocente.FechaNacimiento);
                cmd.CommandType = CommandType.StoredProcedure;
                oConexion.Open();

                cmd.ExecuteNonQuery();

            }

            return RedirectToAction("Inicio", "Docente");
        }

        [HttpGet]
        public ActionResult Eliminar(int? idDocente)
        {
            if (idDocente == null) return RedirectToAction("Inicio", "Docente");

            Docente oContacto = oLista.Where(c => c.IdDocente == idDocente).FirstOrDefault();

            return View(oContacto);
        }

        [HttpPost]
        public ActionResult DeleteContact(string idDocente)
        {

            using (SqlConnection oConexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Eliminar", oConexion);
                cmd.Parameters.AddWithValue("IdDocente", idDocente);
                cmd.CommandType = CommandType.StoredProcedure;
                oConexion.Open();

                cmd.ExecuteNonQuery();

            }

            return RedirectToAction("Inicio", "Docente");
        }

    }
}