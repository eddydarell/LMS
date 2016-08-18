using LMS_Grupp4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    [Authorize(Roles = "admin, teacher")]
    public class SchemaController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Schema
        public ActionResult Index()
        {
            var schemas = context.ClassSchemas.ToList();
            return View(schemas);
        }

        // GET: Schema/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Schema/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schema/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Schema/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Schema/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Schema/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Schema/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
