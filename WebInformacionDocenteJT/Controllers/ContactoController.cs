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
    public class ContactoController : Controller
    {
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();

        private static List<Contacto> oLista = new List<Contacto>();

        // GET: Contacto
        public ActionResult Inicio()
        {

            oLista = new List<Contacto>();

            using (SqlConnection oConexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM CONTACTO", oConexion);
                cmd.CommandType = CommandType.Text;
                oConexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contacto newContact = new Contacto();
                        newContact.IdContacto = Convert.ToInt32(dr["IdContacto"]);
                        newContact.Nombres = dr["Nombres"].ToString();
                        newContact.Apellidos = dr["Apellidos"].ToString();
                        newContact.Telefono = dr["Telefono"].ToString();
                        newContact.Correo = dr["Correo"].ToString();

                        oLista.Add(newContact);
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
        public ActionResult Register(Contacto oContacto)
        {

            using (SqlConnection oConexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Registrar", oConexion);
                cmd.Parameters.AddWithValue("Nombres", oContacto.Nombres);
                cmd.Parameters.AddWithValue("Apellidos", oContacto.Apellidos);
                cmd.Parameters.AddWithValue("Telefono", oContacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", oContacto.Correo);
                cmd.CommandType = CommandType.StoredProcedure;
                oConexion.Open();

                cmd.ExecuteNonQuery();

            }

            return RedirectToAction("Inicio", "Contacto");
        }

        [HttpGet]
        public ActionResult Editar(int? idContacto)
        {
            if (idContacto == null) return RedirectToAction("Inicio", "Contacto");

            Contacto oContacto = oLista.Where(c => c.IdContacto == idContacto).FirstOrDefault();

            return View(oContacto);
        }

        [HttpPost]
        public ActionResult EditContact(Contacto oContacto)
        {

            using (SqlConnection oConexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Editar", oConexion);
                cmd.Parameters.AddWithValue("IdContacto", oContacto.IdContacto);
                cmd.Parameters.AddWithValue("Nombres", oContacto.Nombres);
                cmd.Parameters.AddWithValue("Apellidos", oContacto.Apellidos);
                cmd.Parameters.AddWithValue("Telefono", oContacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", oContacto.Correo);
                cmd.CommandType = CommandType.StoredProcedure;
                oConexion.Open();

                cmd.ExecuteNonQuery();

            }

            return RedirectToAction("Inicio", "Contacto");
        }

        [HttpGet]
        public ActionResult Eliminar(int? idContacto)
        {
            if (idContacto == null) return RedirectToAction("Inicio", "Contacto");

            Contacto oContacto = oLista.Where(c => c.IdContacto == idContacto).FirstOrDefault();

            return View(oContacto);
        }

        [HttpPost]
        public ActionResult DeleteContact(string IdContacto)
        {

            using (SqlConnection oConexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Eliminar", oConexion);
                cmd.Parameters.AddWithValue("IdContacto", IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                oConexion.Open();

                cmd.ExecuteNonQuery();

            }

            return RedirectToAction("Inicio", "Contacto");
        }

    }
}