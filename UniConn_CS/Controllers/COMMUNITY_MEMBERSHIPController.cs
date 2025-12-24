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
    public class COMMUNITY_MEMBERSHIPController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        // GET: COMMUNITY_MEMBERSHIP
        public ActionResult Index()
        {
            var cOMMUNITY_MEMBERSHIP = db.COMMUNITY_MEMBERSHIP.Include(c => c.COMMUNITY).Include(c => c.STUDENTS);
            return View(cOMMUNITY_MEMBERSHIP.ToList());
        }

        // GET: COMMUNITY_MEMBERSHIP/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMUNITY_MEMBERSHIP cOMMUNITY_MEMBERSHIP = db.COMMUNITY_MEMBERSHIP.Find(id);
            if (cOMMUNITY_MEMBERSHIP == null)
            {
                return HttpNotFound();
            }
            return View(cOMMUNITY_MEMBERSHIP);
        }

        // GET: COMMUNITY_MEMBERSHIP/Create
        public ActionResult Create()
        {
            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description");
            ViewBag.student_id = new SelectList(db.STUDENTS, "student_id", "first_name");
            return View();
        }

        // POST: COMMUNITY_MEMBERSHIP/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_id,community_name,join_date,is_active")] COMMUNITY_MEMBERSHIP cOMMUNITY_MEMBERSHIP)
        {
            if (ModelState.IsValid)
            {
                db.COMMUNITY_MEMBERSHIP.Add(cOMMUNITY_MEMBERSHIP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description", cOMMUNITY_MEMBERSHIP.community_name);
            ViewBag.student_id = new SelectList(db.STUDENTS, "student_id", "first_name", cOMMUNITY_MEMBERSHIP.student_id);
            return View(cOMMUNITY_MEMBERSHIP);
        }

        // GET: COMMUNITY_MEMBERSHIP/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMUNITY_MEMBERSHIP cOMMUNITY_MEMBERSHIP = db.COMMUNITY_MEMBERSHIP.Find(id);
            if (cOMMUNITY_MEMBERSHIP == null)
            {
                return HttpNotFound();
            }
            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description", cOMMUNITY_MEMBERSHIP.community_name);
            ViewBag.student_id = new SelectList(db.STUDENTS, "student_id", "first_name", cOMMUNITY_MEMBERSHIP.student_id);
            return View(cOMMUNITY_MEMBERSHIP);
        }

        // POST: COMMUNITY_MEMBERSHIP/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_id,community_name,join_date,is_active")] COMMUNITY_MEMBERSHIP cOMMUNITY_MEMBERSHIP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMMUNITY_MEMBERSHIP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description", cOMMUNITY_MEMBERSHIP.community_name);
            ViewBag.student_id = new SelectList(db.STUDENTS, "student_id", "first_name", cOMMUNITY_MEMBERSHIP.student_id);
            return View(cOMMUNITY_MEMBERSHIP);
        }

        // GET: COMMUNITY_MEMBERSHIP/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMUNITY_MEMBERSHIP cOMMUNITY_MEMBERSHIP = db.COMMUNITY_MEMBERSHIP.Find(id);
            if (cOMMUNITY_MEMBERSHIP == null)
            {
                return HttpNotFound();
            }
            return View(cOMMUNITY_MEMBERSHIP);
        }

        // POST: COMMUNITY_MEMBERSHIP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            COMMUNITY_MEMBERSHIP cOMMUNITY_MEMBERSHIP = db.COMMUNITY_MEMBERSHIP.Find(id);
            db.COMMUNITY_MEMBERSHIP.Remove(cOMMUNITY_MEMBERSHIP);
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
