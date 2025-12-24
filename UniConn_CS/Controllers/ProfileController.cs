using System;
using System.Linq;
using System.Web.Mvc;
using UniConn_CS.Models;

namespace UniConn_CS.Controllers
{
    public class ProfileController : Controller
    {
        private UniConnDBEntities db = new UniConnDBEntities();

        public ActionResult Index()
        {
            if (Session["UserID"] == null) return RedirectToAction("Login", "Account");

            // Hatanın kökten çözümü: Session değerini önce string'e sonra int'e çeviriyoruz
            int currentUserId = int.Parse(Session["UserID"].ToString());

            var student = db.STUDENTS.FirstOrDefault(u => u.student_id == currentUserId.ToString());

            if (student == null) return HttpNotFound();

            return View(student);
        }
    }
}