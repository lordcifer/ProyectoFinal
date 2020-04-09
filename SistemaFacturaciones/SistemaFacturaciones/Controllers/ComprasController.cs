﻿using System;
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
    public class ComprasController : Controller
    {
        private SF db = new SF();

        // GET: Compras
        public ActionResult Index()
        {
            var compras = db.Compras.Include(c => c.Cliente).Include(c => c.Producto);
            return View(compras.ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Nombre");
            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,producto_id,cliente_id,Cantidad,Fecha")] Compra compra)
        {
            var existenciaStock = db.Stocks.SingleOrDefault(s => s.producto_id == compra.producto_id);
            int cantidadValida = 0;

            if (existenciaStock != null)
                cantidadValida = db.Stocks.SingleOrDefault(s => s.producto_id == compra.producto_id).Cantidad;

            if (ModelState.IsValid && cantidadValida >= compra.Cantidad)
            {
                if (existenciaStock != null)
                    existenciaStock.Cantidad -= compra.Cantidad;

                ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Nombre", compra.cliente_id);
                ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", compra.producto_id);

                compra.Fecha = DateTime.Now;
                db.Compras.Add(compra);
                db.SaveChanges();
                return View();
            }

            if (cantidadValida < compra.Cantidad)
            {
                ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Nombre", compra.cliente_id);
                ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", compra.producto_id);

                ViewBag.error = "Error, cantidad elevada, cantidad disponible es " + cantidadValida;

                return View(compra);
            }

            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Nombre", compra.cliente_id);
            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", compra.producto_id);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Cedula", compra.cliente_id);
            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", compra.producto_id);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,producto_id,cliente_id,Cantidad,Fecha")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Cedula", compra.cliente_id);
            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", compra.producto_id);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra compra = db.Compras.Find(id);
            db.Compras.Remove(compra);
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
