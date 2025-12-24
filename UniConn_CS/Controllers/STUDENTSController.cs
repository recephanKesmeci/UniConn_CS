using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniConn_CS.Models;

namespace UniConn_CS.Controllers
{
    public class STUDENTSController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        // GET: STUDENTS
        public ActionResult Index()
        {
            return View(db.STUDENTS.ToList());
        }

        // GET: STUDENTS/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STUDENTS sTUDENTS = db.STUDENTS.Find(id);
            if (sTUDENTS == null)
            {
                return HttpNotFound();
            }
            return View(sTUDENTS);
        }

        // GET: STUDENTS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: STUDENTS/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_id,first_name,last_name,institutional_email,registration_date,status")] STUDENTS sTUDENTS)
        {
            if (ModelState.IsValid)
            {
                db.STUDENTS.Add(sTUDENTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sTUDENTS);
        }

        // GET: STUDENTS/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STUDENTS sTUDENTS = db.STUDENTS.Find(id);
            if (sTUDENTS == null)
            {
                return HttpNotFound();
            }
            return View(sTUDENTS);
        }

        // POST: STUDENTS/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_id,first_name,last_name,institutional_email,registration_date,status")] STUDENTS sTUDENTS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTUDENTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sTUDENTS);
        }

        // GET: STUDENTS/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STUDENTS sTUDENTS = db.STUDENTS.Find(id);
            if (sTUDENTS == null)
            {
                return HttpNotFound();
            }
            return View(sTUDENTS);
        }

        // POST: STUDENTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            STUDENTS sTUDENTS = db.STUDENTS.Find(id);
            db.STUDENTS.Remove(sTUDENTS);
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
