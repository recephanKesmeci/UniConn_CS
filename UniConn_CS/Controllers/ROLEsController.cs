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
    public class ROLEsController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        // GET: ROLEs
        public ActionResult Index()
        {
            var rOLE = db.ROLE.Include(r => r.COMMUNITY);
            return View(rOLE.ToList());
        }

        // GET: ROLEs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROLE rOLE = db.ROLE.Find(id);
            if (rOLE == null)
            {
                return HttpNotFound();
            }
            return View(rOLE);
        }

        // GET: ROLEs/Create
        public ActionResult Create()
        {
            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description");
            return View();
        }

        // POST: ROLEs/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "community_name,role_title,role_description,is_allowed_to_vote,can_remove_add_members,can_create_events,can_participate_in_polls")] ROLE rOLE)
        {
            if (ModelState.IsValid)
            {
                db.ROLE.Add(rOLE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description", rOLE.community_name);
            return View(rOLE);
        }

        // GET: ROLEs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROLE rOLE = db.ROLE.Find(id);
            if (rOLE == null)
            {
                return HttpNotFound();
            }
            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description", rOLE.community_name);
            return View(rOLE);
        }

        // POST: ROLEs/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "community_name,role_title,role_description,is_allowed_to_vote,can_remove_add_members,can_create_events,can_participate_in_polls")] ROLE rOLE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOLE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description", rOLE.community_name);
            return View(rOLE);
        }

        // GET: ROLEs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROLE rOLE = db.ROLE.Find(id);
            if (rOLE == null)
            {
                return HttpNotFound();
            }
            return View(rOLE);
        }

        // POST: ROLEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ROLE rOLE = db.ROLE.Find(id);
            db.ROLE.Remove(rOLE);
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
