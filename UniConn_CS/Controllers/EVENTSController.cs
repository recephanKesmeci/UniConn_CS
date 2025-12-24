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
    public class EVENTSController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        // GET: EVENTS
        public ActionResult Index()
        {
            var eVENTS = db.EVENTS.Include(e => e.COMMUNITY).Include(e => e.STUDENTS);
            return View(eVENTS.ToList());
        }

        // GET: EVENTS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENTS eVENTS = db.EVENTS.Find(id);
            if (eVENTS == null)
            {
                return HttpNotFound();
            }
            return View(eVENTS);
        }

        // GET: EVENTS/Create
        public ActionResult Create()
        {
            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description");
            ViewBag.creator_student_id = new SelectList(db.STUDENTS, "student_id", "first_name");
            return View();
        }

        // POST: EVENTS/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "event_id,event_name,description,event_date,location,community_name,creator_student_id,creation_date,is_archived")] EVENTS eVENTS)
        {
            if (ModelState.IsValid)
            {
                db.EVENTS.Add(eVENTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description", eVENTS.community_name);
            ViewBag.creator_student_id = new SelectList(db.STUDENTS, "student_id", "first_name", eVENTS.creator_student_id);
            return View(eVENTS);
        }

        // GET: EVENTS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENTS eVENTS = db.EVENTS.Find(id);
            if (eVENTS == null)
            {
                return HttpNotFound();
            }
            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description", eVENTS.community_name);
            ViewBag.creator_student_id = new SelectList(db.STUDENTS, "student_id", "first_name", eVENTS.creator_student_id);
            return View(eVENTS);
        }

        // POST: EVENTS/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "event_id,event_name,description,event_date,location,community_name,creator_student_id,creation_date,is_archived")] EVENTS eVENTS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eVENTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description", eVENTS.community_name);
            ViewBag.creator_student_id = new SelectList(db.STUDENTS, "student_id", "first_name", eVENTS.creator_student_id);
            return View(eVENTS);
        }

        // GET: EVENTS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENTS eVENTS = db.EVENTS.Find(id);
            if (eVENTS == null)
            {
                return HttpNotFound();
            }
            return View(eVENTS);
        }

        // POST: EVENTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EVENTS eVENTS = db.EVENTS.Find(id);
            db.EVENTS.Remove(eVENTS);
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
