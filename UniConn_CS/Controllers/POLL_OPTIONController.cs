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
    public class POLL_OPTIONController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        // GET: POLL_OPTION
        public ActionResult Index()
        {
            var pOLL_OPTION = db.POLL_OPTION.Include(p => p.POLL);
            return View(pOLL_OPTION.ToList());
        }

        // GET: POLL_OPTION/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POLL_OPTION pOLL_OPTION = db.POLL_OPTION.Find(id);
            if (pOLL_OPTION == null)
            {
                return HttpNotFound();
            }
            return View(pOLL_OPTION);
        }

        // GET: POLL_OPTION/Create
        public ActionResult Create()
        {
            ViewBag.poll_id = new SelectList(db.POLL, "poll_id", "poll_title");
            return View();
        }

        // POST: POLL_OPTION/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "poll_option_id,poll_id,vote_count,option_text")] POLL_OPTION pOLL_OPTION)
        {
            if (ModelState.IsValid)
            {
                db.POLL_OPTION.Add(pOLL_OPTION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.poll_id = new SelectList(db.POLL, "poll_id", "poll_title", pOLL_OPTION.poll_id);
            return View(pOLL_OPTION);
        }

        // GET: POLL_OPTION/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POLL_OPTION pOLL_OPTION = db.POLL_OPTION.Find(id);
            if (pOLL_OPTION == null)
            {
                return HttpNotFound();
            }
            ViewBag.poll_id = new SelectList(db.POLL, "poll_id", "poll_title", pOLL_OPTION.poll_id);
            return View(pOLL_OPTION);
        }

        // POST: POLL_OPTION/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "poll_option_id,poll_id,vote_count,option_text")] POLL_OPTION pOLL_OPTION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pOLL_OPTION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.poll_id = new SelectList(db.POLL, "poll_id", "poll_title", pOLL_OPTION.poll_id);
            return View(pOLL_OPTION);
        }

        // GET: POLL_OPTION/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POLL_OPTION pOLL_OPTION = db.POLL_OPTION.Find(id);
            if (pOLL_OPTION == null)
            {
                return HttpNotFound();
            }
            return View(pOLL_OPTION);
        }

        // POST: POLL_OPTION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POLL_OPTION pOLL_OPTION = db.POLL_OPTION.Find(id);
            db.POLL_OPTION.Remove(pOLL_OPTION);
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
