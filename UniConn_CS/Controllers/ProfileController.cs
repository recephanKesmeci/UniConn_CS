using System;
using System.Linq;
using System.Web.Mvc;
using UniConn_CS.Models;
using System.Collections.Generic;

namespace UniConn_CS.Controllers
{
    public class ProfileController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        public ActionResult Index()
        {
            if (Session["UserID"] == null) return RedirectToAction("Login", "Account");

            // ID string olarak tutuluyor (S001 gibi)
            string currentUserId = Session["UserID"].ToString();

            // 1. Öğrenci Bilgisi
            var student = db.STUDENTS.FirstOrDefault(u => u.student_id == currentUserId);
            if (student == null) return HttpNotFound();

            // 2. İstatistikler
            int postCount = db.POST.Count(p => p.creator_student_id == currentUserId);
            int communityCount = db.COMMUNITY_MEMBERSHIP.Count(m => m.student_id == currentUserId && m.is_active == true);

            // 3. Üye Olduğu Kulüpler (Gelişmiş Yöntem)
            // Sadece aktif üyelikleri çekiyoruz ve rollerini buluyoruz.
            // Eğer özel bir rolü yoksa (Role tablosunda kaydı yoksa) varsayılan olarak "Member" yazdırıyoruz.
            var myCommunities = db.COMMUNITY_MEMBERSHIP
                                  .Where(m => m.student_id == currentUserId && m.is_active == true)
                                  .ToList() // Veriyi belleğe alıp işliyoruz
                                  .Select(m => new CommunityRoleInfo
                                  {
                                      CommunityName = m.community_name,
                                      RoleTitle = db.COMMUNITY_ROLE_ASSIGNMENT
                                                    .Where(r => r.student_id == currentUserId && r.community_name == m.community_name)
                                                    .Select(r => r.role_title)
                                                    .FirstOrDefault() ?? "Member"
                                  })
                                  .ToList();

            // 4. Kullanıcının Postları (YENİ EKLENEN KISIM)
            // Tarihe göre en yeniden eskiye sıralı
            var myPosts = db.POST
                            .Where(p => p.creator_student_id == currentUserId)
                            .OrderByDescending(p => p.creation_date)
                            .ToList();

            // 5. Modeli Doldur
            var model = new ProfileViewModel
            {
                Student = student,
                PostCount = postCount,
                CommunityCount = communityCount,
                MyCommunities = myCommunities,
                MyPosts = myPosts // Postları da gönderiyoruz
            };

            return View(model);
        }

        // ==========================================
        // YENİ: Topluluktan Ayrıl (Profil Sayfasından)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LeaveCommunity(string name)
        {
            if (Session["UserID"] == null) return RedirectToAction("Login", "Account");
            string userId = Session["UserID"].ToString();

            var membership = db.COMMUNITY_MEMBERSHIP.FirstOrDefault(m => m.student_id == userId && m.community_name == name);
            if (membership != null)
            {
                membership.is_active = false;
                db.SaveChanges();
            }
            // İşlem bitince sayfayı yenile (Index'e dön)
            return RedirectToAction("Index");
        }

        // ==========================================
        // YENİ: Post Sil (Profil Sayfasından)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (Session["UserID"] == null) return RedirectToAction("Login", "Account");
            string userId = Session["UserID"].ToString();

            var post = db.POST.Find(id);

            // Güvenlik Kontrolü: Sadece postun sahibi silebilir!
            if (post != null && post.creator_student_id == userId)
            {
                db.POST.Remove(post);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}