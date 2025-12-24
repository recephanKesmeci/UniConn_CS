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
    public class COMMUNITY_ROLE_ASSIGNMENTController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        // GET: COMMUNITY_ROLE_ASSIGNMENT
        public ActionResult Index()
        {
            var cOMMUNITY_ROLE_ASSIGNMENT = db.COMMUNITY_ROLE_ASSIGNMENT.Include(c => c.STUDENTS).Include(c => c.STUDENTS1).Include(c => c.ROLE);
            return View(cOMMUNITY_ROLE_ASSIGNMENT.ToList());
        }

        // GET: COMMUNITY_ROLE_ASSIGNMENT/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMUNITY_ROLE_ASSIGNMENT cOMMUNITY_ROLE_ASSIGNMENT = db.COMMUNITY_ROLE_ASSIGNMENT.Find(id);
            if (cOMMUNITY_ROLE_ASSIGNMENT == null)
            {
                return HttpNotFound();
            }
            return View(cOMMUNITY_ROLE_ASSIGNMENT);
        }

        // GET: COMMUNITY_ROLE_ASSIGNMENT/Create
        public ActionResult Create()
        {
            ViewBag.assigned_by = new SelectList(db.STUDENTS, "student_id", "first_name");
            ViewBag.student_id = new SelectList(db.STUDENTS, "student_id", "first_name");
            ViewBag.community_name = new SelectList(db.ROLE, "community_name", "role_description");
            return View();
        }

        // POST: COMMUNITY_ROLE_ASSIGNMENT/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_id,community_name,role_title,assigned_by,assigned_date")] COMMUNITY_ROLE_ASSIGNMENT cOMMUNITY_ROLE_ASSIGNMENT)
        {
            if (ModelState.IsValid)
            {
                db.COMMUNITY_ROLE_ASSIGNMENT.Add(cOMMUNITY_ROLE_ASSIGNMENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.assigned_by = new SelectList(db.STUDENTS, "student_id", "first_name", cOMMUNITY_ROLE_ASSIGNMENT.assigned_by);
            ViewBag.student_id = new SelectList(db.STUDENTS, "student_id", "first_name", cOMMUNITY_ROLE_ASSIGNMENT.student_id);
            ViewBag.community_name = new SelectList(db.ROLE, "community_name", "role_description", cOMMUNITY_ROLE_ASSIGNMENT.community_name);
            return View(cOMMUNITY_ROLE_ASSIGNMENT);
        }

        // GET: COMMUNITY_ROLE_ASSIGNMENT/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMUNITY_ROLE_ASSIGNMENT cOMMUNITY_ROLE_ASSIGNMENT = db.COMMUNITY_ROLE_ASSIGNMENT.Find(id);
            if (cOMMUNITY_ROLE_ASSIGNMENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.assigned_by = new SelectList(db.STUDENTS, "student_id", "first_name", cOMMUNITY_ROLE_ASSIGNMENT.assigned_by);
            ViewBag.student_id = new SelectList(db.STUDENTS, "student_id", "first_name", cOMMUNITY_ROLE_ASSIGNMENT.student_id);
            ViewBag.community_name = new SelectList(db.ROLE, "community_name", "role_description", cOMMUNITY_ROLE_ASSIGNMENT.community_name);
            return View(cOMMUNITY_ROLE_ASSIGNMENT);
        }

        // POST: COMMUNITY_ROLE_ASSIGNMENT/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_id,community_name,role_title,assigned_by,assigned_date")] COMMUNITY_ROLE_ASSIGNMENT cOMMUNITY_ROLE_ASSIGNMENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMMUNITY_ROLE_ASSIGNMENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.assigned_by = new SelectList(db.STUDENTS, "student_id", "first_name", cOMMUNITY_ROLE_ASSIGNMENT.assigned_by);
            ViewBag.student_id = new SelectList(db.STUDENTS, "student_id", "first_name", cOMMUNITY_ROLE_ASSIGNMENT.student_id);
            ViewBag.community_name = new SelectList(db.ROLE, "community_name", "role_description", cOMMUNITY_ROLE_ASSIGNMENT.community_name);
            return View(cOMMUNITY_ROLE_ASSIGNMENT);
        }

        // GET: COMMUNITY_ROLE_ASSIGNMENT/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMUNITY_ROLE_ASSIGNMENT cOMMUNITY_ROLE_ASSIGNMENT = db.COMMUNITY_ROLE_ASSIGNMENT.Find(id);
            if (cOMMUNITY_ROLE_ASSIGNMENT == null)
            {
                return HttpNotFound();
            }
            return View(cOMMUNITY_ROLE_ASSIGNMENT);
        }

        // POST: COMMUNITY_ROLE_ASSIGNMENT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            COMMUNITY_ROLE_ASSIGNMENT cOMMUNITY_ROLE_ASSIGNMENT = db.COMMUNITY_ROLE_ASSIGNMENT.Find(id);
            db.COMMUNITY_ROLE_ASSIGNMENT.Remove(cOMMUNITY_ROLE_ASSIGNMENT);
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
