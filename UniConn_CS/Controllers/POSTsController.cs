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
    public class POSTsController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        public bool isUserAuthorized(string community_name)
        {
            var userIDObj = Session["UserID"];
            if (userIDObj == null) return false;
            string userID = userIDObj.ToString();

            return db.COMMUNITY_ROLE_ASSIGNMENT
                .Where(ra => ra.student_id == userID && ra.community_name == community_name)
                .Any(ra => ra.ROLE.can_create_events.GetValueOrDefault(false));
        }

        // GET: POSTs
        public ActionResult Index()
        {
            if (Session["UserID"] == null) return RedirectToAction("Login", "Account");

            // Filtresiz, tüm postları getirir = Global Feed
            var posts = db.POST.OrderByDescending(p => p.creation_date).ToList();
            return View(posts);
        }
        // GET: POSTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POST pOST = db.POST.Find(id);
            if (pOST == null)
            {
                return HttpNotFound();
            }
            return View(pOST);
        }

        // GET: POSTs/Create
        public ActionResult Create()
        {
            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "community_name");
            ViewBag.event_reference_id = new SelectList(db.EVENTS, "event_id", "event_name");
            ViewBag.creator_student_id = new SelectList(db.STUDENTS, "student_id", "first_name");
            return View();
        }

        // POST: POSTs/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "post_id,community_name,creator_student_id,creation_date,title,content,image_url,event_reference_id")] POST post)
        {
            post.post_id = new Random().Next();
            post.creation_date = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.POST.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "community_name", post.community_name);
            ViewBag.event_reference_id = new SelectList(db.EVENTS, "event_id", "event_name", post.event_reference_id);
            ViewBag.creator_student_id = new SelectList(db.STUDENTS, "student_id", "first_name", post.creator_student_id);
            return View(post);
        }

        // GET: POSTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POST post = db.POST.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description", post.community_name);
            ViewBag.event_reference_id = new SelectList(db.EVENTS, "event_id", "event_name", post.event_reference_id);
            ViewBag.creator_student_id = new SelectList(db.STUDENTS, "student_id", "first_name", post.creator_student_id);
            return View(post);
        }

        // POST: POSTs/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "post_id,community_name,creator_student_id,creation_date,title,content,image_url,event_reference_id")] POST pOST)
        {
            

            if (ModelState.IsValid)
            {
                db.Entry(pOST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.community_name = new SelectList(db.COMMUNITY, "community_name", "description", pOST.community_name);
            ViewBag.event_reference_id = new SelectList(db.EVENTS, "event_id", "event_name", pOST.event_reference_id);
            ViewBag.creator_student_id = new SelectList(db.STUDENTS, "student_id", "first_name", pOST.creator_student_id);
            return View(pOST);
        }

        // GET: POSTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POST pOST = db.POST.Find(id);
            if (pOST == null)
            {
                return HttpNotFound();
            }
            return View(pOST);
        }

        // POST: POSTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POST pOST = db.POST.Find(id);
            db.POST.Remove(pOST);
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
