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
    public class VOTEsController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        // GET: VOTEs
        public ActionResult Index()
        {
            var vOTE = db.VOTE.Include(v => v.POLL).Include(v => v.POLL_OPTION).Include(v => v.STUDENTS);
            return View(vOTE.ToList());
        }

        // GET: VOTEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOTE vOTE = db.VOTE.Find(id);
            if (vOTE == null)
            {
                return HttpNotFound();
            }
            return View(vOTE);
        }

        // GET: VOTEs/Create
        public ActionResult Create()
        {
            ViewBag.poll_id = new SelectList(db.POLL, "poll_id", "poll_title");
            ViewBag.poll_option_id = new SelectList(db.POLL_OPTION, "poll_option_id", "option_text");
            ViewBag.student_id = new SelectList(db.STUDENTS, "student_id", "first_name");
            return View();
        }

        // POST: VOTEs/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "poll_id,student_id,poll_option_id,vote_date")] VOTE vOTE)
        {
            if (ModelState.IsValid)
            {
                db.VOTE.Add(vOTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.poll_id = new SelectList(db.POLL, "poll_id", "poll_title", vOTE.poll_id);
            ViewBag.poll_option_id = new SelectList(db.POLL_OPTION, "poll_option_id", "option_text", vOTE.poll_option_id);
            ViewBag.student_id = new SelectList(db.STUDENTS, "student_id", "first_name", vOTE.student_id);
            return View(vOTE);
        }

        // GET: VOTEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOTE vOTE = db.VOTE.Find(id);
            if (vOTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.poll_id = new SelectList(db.POLL, "poll_id", "poll_title", vOTE.poll_id);
            ViewBag.poll_option_id = new SelectList(db.POLL_OPTION, "poll_option_id", "option_text", vOTE.poll_option_id);
            ViewBag.student_id = new SelectList(db.STUDENTS, "student_id", "first_name", vOTE.student_id);
            return View(vOTE);
        }

        // POST: VOTEs/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "poll_id,student_id,poll_option_id,vote_date")] VOTE vOTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vOTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.poll_id = new SelectList(db.POLL, "poll_id", "poll_title", vOTE.poll_id);
            ViewBag.poll_option_id = new SelectList(db.POLL_OPTION, "poll_option_id", "option_text", vOTE.poll_option_id);
            ViewBag.student_id = new SelectList(db.STUDENTS, "student_id", "first_name", vOTE.student_id);
            return View(vOTE);
        }

        // GET: VOTEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOTE vOTE = db.VOTE.Find(id);
            if (vOTE == null)
            {
                return HttpNotFound();
            }
            return View(vOTE);
        }

        // POST: VOTEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VOTE vOTE = db.VOTE.Find(id);
            db.VOTE.Remove(vOTE);
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
