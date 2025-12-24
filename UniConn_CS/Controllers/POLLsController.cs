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
    public class POLLsController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        // GET: POLLs
        public ActionResult Index()
        {
            var pOLL = db.POLL.Include(p => p.STUDENTS).Include(p => p.POST);
            return View(pOLL.ToList());
        }

        // GET: POLLs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POLL pOLL = db.POLL.Find(id);
            if (pOLL == null)
            {
                return HttpNotFound();
            }
            return View(pOLL);
        }

        // GET: POLLs/Create
        public ActionResult Create()
        {
            ViewBag.poll_creator_student_id = new SelectList(db.STUDENTS, "student_id", "first_name");
            ViewBag.post_id = new SelectList(db.POST, "post_id", "community_name");
            return View();
        }

        // POST: POLLs/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "poll_id,poll_title,poll_creator_student_id,creation_date,post_id")] POLL pOLL)
        {
            if (ModelState.IsValid)
            {
                db.POLL.Add(pOLL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.poll_creator_student_id = new SelectList(db.STUDENTS, "student_id", "first_name", pOLL.poll_creator_student_id);
            ViewBag.post_id = new SelectList(db.POST, "post_id", "community_name", pOLL.post_id);
            return View(pOLL);
        }

        // GET: POLLs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POLL pOLL = db.POLL.Find(id);
            if (pOLL == null)
            {
                return HttpNotFound();
            }
            ViewBag.poll_creator_student_id = new SelectList(db.STUDENTS, "student_id", "first_name", pOLL.poll_creator_student_id);
            ViewBag.post_id = new SelectList(db.POST, "post_id", "community_name", pOLL.post_id);
            return View(pOLL);
        }

        // POST: POLLs/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "poll_id,poll_title,poll_creator_student_id,creation_date,post_id")] POLL pOLL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pOLL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.poll_creator_student_id = new SelectList(db.STUDENTS, "student_id", "first_name", pOLL.poll_creator_student_id);
            ViewBag.post_id = new SelectList(db.POST, "post_id", "community_name", pOLL.post_id);
            return View(pOLL);
        }

        // GET: POLLs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POLL pOLL = db.POLL.Find(id);
            if (pOLL == null)
            {
                return HttpNotFound();
            }
            return View(pOLL);
        }

        // POST: POLLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POLL pOLL = db.POLL.Find(id);
            db.POLL.Remove(pOLL);
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
