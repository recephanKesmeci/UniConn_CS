using System;
using System.Linq;
using System.Web.Mvc;
using UniConn_CS.Models;
using System.Collections.Generic;

namespace UniConn_CS.Controllers
{
    public class HomeController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        public ActionResult Index()
        {
            // 1. Güvenlik: Giriş yapmamış kullanıcıyı Login'e at
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string currentUserId = Session["UserID"].ToString();

            // 2. Kişisel Akış Mantığı:
            // Önce kullanıcının üye olduğu (ve üyeliği aktif olan) toplulukların isimlerini çekiyoruz.
            var myCommunityNames = db.COMMUNITY_MEMBERSHIP
                                     .Where(m => m.student_id == currentUserId && m.is_active == true)
                                     .Select(m => m.community_name)
                                     .ToList();

            // 3. Postları Filtreleme:
            // Sadece bu topluluklara ait olan postları tarihe göre (yeniden eskiye) sıralayıp getiriyoruz.
            var myFeedPosts = db.POST
                                .Where(p => myCommunityNames.Contains(p.community_name))
                                .OrderByDescending(p => p.creation_date)
                                .ToList();

            // 4. Sağ taraf widget'ı için topluluk önerileri (İlk 5)
            ViewBag.Communities = db.COMMUNITY.Take(5).ToList();

            // Listeyi View'a gönder
            return View(myFeedPosts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "UniConn Hakkında";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "İletişim Sayfası";
            return View();
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