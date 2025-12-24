using System;
using System.Linq;
using System.Web.Mvc;
using UniConn_CS.Models;

namespace UniConn_CS.Controllers
{
    public class AccountController : Controller
    {
        // Veritabanı bağlantısı
        private UniConnDBEntities db = new UniConnDBEntities();

        // --- LOGIN (GİRİŞ) KISMI ---
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = db.STUDENTS.FirstOrDefault(u => u.institutional_email == email && u.password == password);

            if (user != null)
            {
                Session["UserID"] = user.student_id;
                Session["UserName"] = user.first_name + " " + user.last_name;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Email veya şifre hatalı!";
                return View();
            }
        }

        // --- REGISTER (KAYIT OL) KISMI ---
        // Sayfayı Göster
        public ActionResult Register()
        {
            return View();
        }

        // Kaydet Butonuna Basılınca Çalışır
        [HttpPost]
        public ActionResult Register(STUDENTS yeniOgrenci)
        {
            // Email kontrolü: Daha önce alınmış mı?
            var varMi = db.STUDENTS.Any(x => x.institutional_email == yeniOgrenci.institutional_email);
            if (varMi)
            {
                ViewBag.Error = "Bu email adresi zaten kayıtlı!";
                return View();
            }

            try
            {
                // Otomatik doldurulacak alanlar
                yeniOgrenci.registration_date = DateTime.Now;
                yeniOgrenci.status = "Aktif";

                db.STUDENTS.Add(yeniOgrenci);
                db.SaveChanges();

                // Kayıt başarılıysa Giriş sayfasına yönlendir
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                ViewBag.Error = "Kayıt yapılırken bir hata oluştu. Bilgileri kontrol edin.";
                return View();
            }
        }

        // --- LOGOUT (ÇIKIŞ) KISMI ---
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}