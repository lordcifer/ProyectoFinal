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
    public class ProveedoresController : Controller
    {
        private SF db = new SF();

        // GET: Proveedores
        public ActionResult Index()
        {
            return View(db.Proveedores.ToList());   
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedore proveedore = db.Proveedores.Find(id);
            if (proveedore == null)
            {
                return HttpNotFound();
            }
            return View(proveedore);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Cedula,Telefono,Email")] Proveedore proveedore)
        {
            if (ModelState.IsValid)
            {
                db.Proveedores.Add(proveedore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proveedore);
        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedore proveedore = db.Proveedores.Find(id);
            if (proveedore == null)
            {
                return HttpNotFound();
            }
            return View(proveedore);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Cedula,Telefono,Email")] Proveedore proveedore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proveedore);
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedore proveedore = db.Proveedores.Find(id);
            if (proveedore == null)
            {
                return HttpNotFound();
            }
            return View(proveedore);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedore proveedore = db.Proveedores.Find(id);
            db.Proveedores.Remove(proveedore);
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
