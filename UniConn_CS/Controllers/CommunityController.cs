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
    public class COMMUNITiesController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        // GET: COMMUNITies
        public ActionResult Index()
        {
            return View(db.COMMUNITY.ToList());
        }

        // ==========================================
        // GÜNCELLENEN KISIM: Details (Name Parametresi + Veriler)
        // ==========================================
        // Parametre adını 'id' yerine 'name' yaptık ki '&' karakteri sorun yaratmasın.
        public ActionResult Details(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Topluluğu ismine göre bul
            var community = db.COMMUNITY.Find(name);
            if (community == null)
            {
                return HttpNotFound();
            }

            // 1. Üyelik Kontrolü
            string currentUserId = Session["UserID"]?.ToString();
            bool isMember = false;
            if (currentUserId != null)
            {
                isMember = db.COMMUNITY_MEMBERSHIP.Any(m =>
                    m.student_id == currentUserId &&
                    m.community_name == name &&
                    m.is_active == true);
            }
            ViewBag.IsMember = isMember;

            // 2. Üye Sayısını Hesapla (1).sql]
            ViewBag.MemberCount = db.COMMUNITY_MEMBERSHIP.Count(m =>
                m.community_name == name && m.is_active == true);

            // 3. Gelecek Etkinlikleri Getir (1).sql]
            // Arşivlenmemiş etkinlikleri tarihe göre sıralayıp View'a gönderiyoruz
            var events = db.EVENTS
                           .Where(e => e.community_name == name && e.is_archived == false)
                           .OrderBy(e => e.event_date)
                           .ToList();

            ViewBag.CommunityEvents = events;

            return View(community);
        }

        // ==========================================
        // KATILMA İŞLEMİ (Join)
        // ==========================================
        // ==========================================
        // GÜNCELLENEN METOTLAR: Join ve Leave (name parametresi ile)
        // ==========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join(string name) // Parametre adı 'id' yerine 'name' oldu
        {
            if (Session["UserID"] == null) return RedirectToAction("Login", "Account");

            string userId = Session["UserID"].ToString();

            // Veritabanında bu kişi ve topluluk için kayıt var mı?
            var membership = db.COMMUNITY_MEMBERSHIP.FirstOrDefault(m => m.student_id == userId && m.community_name == name);

            if (membership != null)
            {
                // Kayıt varsa (daha önce çıkmışsa bile) tekrar aktif yap
                membership.is_active = true;
                membership.join_date = DateTime.Now;
            }
            else
            {
                // Hiç kaydı yoksa sıfırdan oluştur
                var newMember = new COMMUNITY_MEMBERSHIP
                {
                    student_id = userId,
                    community_name = name,
                    join_date = DateTime.Now,
                    is_active = true
                };
                db.COMMUNITY_MEMBERSHIP.Add(newMember);
            }

            db.SaveChanges();

            // İşlem bitince detay sayfasına 'name' parametresiyle dön
            return RedirectToAction("Details", new { name = name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Leave(string name) // Parametre adı 'id' yerine 'name' oldu
        {
            if (Session["UserID"] == null) return RedirectToAction("Login", "Account");

            string userId = Session["UserID"].ToString();

            // Üyeliği bul ve pasife çek
            var membership = db.COMMUNITY_MEMBERSHIP.FirstOrDefault(m => m.student_id == userId && m.community_name == name);

            if (membership != null)
            {
                membership.is_active = false;
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { name = name });
        }

        // GET: COMMUNITies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: COMMUNITies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "community_name,description,creation_date")] COMMUNITY community)
        {
            community.creation_date = DateTime.Now;
            

            if (ModelState.IsValid)
            {
                db.COMMUNITY.Add(community);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(community);
        }

        // GET: COMMUNITies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            COMMUNITY cOMMUNITY = db.COMMUNITY.Find(id);
            if (cOMMUNITY == null) return HttpNotFound();
            return View(cOMMUNITY);
        }

        // POST: COMMUNITies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "community_name,description,creation_date")] COMMUNITY cOMMUNITY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMMUNITY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOMMUNITY);
        }

        // GET: COMMUNITies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            COMMUNITY cOMMUNITY = db.COMMUNITY.Find(id);
            if (cOMMUNITY == null) return HttpNotFound();
            return View(cOMMUNITY);
        }

        // POST: COMMUNITies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            COMMUNITY cOMMUNITY = db.COMMUNITY.Find(id);
            db.COMMUNITY.Remove(cOMMUNITY);
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