using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniConn_CS.Models; // Modellerine erişim için gerekli

namespace UniConn_CS.Controllers
{
    public class HomeController : Controller
    {
        // Veritabanı bağlantı ismin: UniConnDBEntities
        private UniConnDBEntities db = new UniConnDBEntities();

        public ActionResult Index()
        {
            // 1. Güvenlik: Giriş yapmamış kullanıcıyı Login sayfasına atar.
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // 2. Postları Çekme: 
            // HATA ÇÖZÜMÜ: Tablo adın 'POSTS' değil, 'POST' (tekil).
            // .OrderByDescending ile en yeni postun en üstte görünmesini sağlıyoruz.
            var posts = db.POST.OrderByDescending(p => p.post_id).ToList();

            // 3. Topluluk Önerileri: 
            // Sağ taraftaki widget için veritabanındaki topluluklardan ilk 5 tanesini alıyoruz.
            ViewBag.Communities = db.COMMUNITY.Take(5).ToList();

            // 4. Görünümü döndür: Post listesini sayfaya model olarak gönderiyoruz.
            return View(posts);
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

        // Veritabanı bağlantısını temizle
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