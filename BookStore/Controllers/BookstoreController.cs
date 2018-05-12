using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

using PagedList;
using PagedList.Mvc;
namespace BookStore.Controllers
{
    public class BookstoreController : Controller
    {
        //
        // GET: /Bookstore/
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        private List<SACH> LaySachMoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Index(int? page, string searchTerm)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);

            var sachMoi = LaySachMoi(15);
            var sp = from s in data.SACHes select s;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                sp = data.SACHes.Where(s => s.Tensach.Contains(searchTerm));
            }
            ViewBag.SearchTerm = searchTerm;
            return View(sp.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }
        public ActionResult Nhaxuatban()
        {
            var NXB = from nxb in data.NHAXUATBANs select nxb;
            return PartialView(NXB);
        }
        public ActionResult SPTheochude(int id)
        {
            var sach = from s in data.SACHes where s.MaCD == id select s;
            return View(sach);
        }
        public ActionResult SPTheoNXB(int id)
        {
            var sach = from s in data.SACHes where s.MaNXB == id select s;
            return View(sach);
        }
        public ActionResult Details(int id)
        {
            var sach = from s in data.SACHes
                       where s.Masach == id
                       select s;
            return View(sach.Single());
        }
	}
}