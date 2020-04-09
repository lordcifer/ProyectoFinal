using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SistemaFacturaciones.Models;

namespace SistemaFacturaciones.Controllers
{
    public class ConsultasController : Controller
    {
        private SF db = new SF();
        private EntradaMercancia entradaMercancia = new EntradaMercancia();
        private Facturacion facturacion = new Facturacion();

        // GET: Consultas
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Productos(string query)
        {
            if (!string.IsNullOrEmpty(query))
                return View(db.Productos.Where(p => p.Nombre == query));

            return View(db.Productos.ToList());
        }
        public ActionResult Clientes(string query, string seleccion, string opcion)
        {
            if (!string.IsNullOrEmpty(query))
            {
                if (opcion.Equals("0"))
                    return View(db.Clientes.Where(p => p.Nombre == query));
                else
                {
                    if (seleccion.Equals("0"))
                        return View(db.Clientes.ToList());
                    else if (seleccion.Equals("1"))
                        return View(db.Clientes.Where(c => c.Categoria == "premium"));
                    else
                        return View(db.Clientes.Where(c => c.Categoria == "regular"));
                }
            }

            if (!string.IsNullOrEmpty(seleccion))
            {
                if (seleccion.Equals("0"))
                    return View(db.Clientes.ToList());
                else if (seleccion.Equals("1"))
                    return View(db.Clientes.Where(c => c.Categoria == "premium"));
                else
                    return View(db.Clientes.Where(c => c.Categoria == "regular"));
            }

            return View(db.Clientes.ToList());
        }

        public ActionResult Proveedores(string query, string seleccion)
        {
            if (!string.IsNullOrEmpty(query))
            {
                if (seleccion.Equals("0"))
                    return View(db.Proveedores.Where(p => p.Nombre == query));
                else
                    return View(db.Proveedores.Where(p => p.Email == query));
            }

            return View(db.Proveedores.ToList());
        }
    }
}