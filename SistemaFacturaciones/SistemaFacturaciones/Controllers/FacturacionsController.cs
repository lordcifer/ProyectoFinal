using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaFacturaciones.Models;

namespace SistemaFacturaciones.Controllers
{
    public class FacturacionsController : Controller
    {
        private SF db = new SF();

        // GET: Facturacions
        public ActionResult Index()
        {
            var facturacions = db.Facturacions.Include(f => f.Cliente);
            return View(facturacions.ToList());
        }

        // GET: Facturacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion facturacion = db.Facturacions.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            return View(facturacion);
        }

        // GET: Facturacions/Create
        public ActionResult Create()
        {
            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Cedula");
            return View();
        }

        // POST: Facturacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,cliente_id,Descuento,Monto,Fecha")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                db.Facturacions.Add(facturacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Cedula", facturacion.cliente_id);
            return View(facturacion);
        }

        // GET: Facturacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion facturacion = db.Facturacions.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Cedula", facturacion.cliente_id);
            return View(facturacion);
        }

        // POST: Facturacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,cliente_id,Descuento,Monto,Fecha")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Cedula", facturacion.cliente_id);
            return View(facturacion);
        }

        // GET: Facturacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion facturacion = db.Facturacions.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            return View(facturacion);
        }

        // POST: Facturacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facturacion facturacion = db.Facturacions.Find(id);
            db.Facturacions.Remove(facturacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
