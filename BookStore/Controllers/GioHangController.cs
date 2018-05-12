using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        dbQLBansachDataContext db = new dbQLBansachDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public List<Giohang> layGioHang()
        {
            List<Giohang> lstGioHang = Session["GioHang"] as List<Giohang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<Giohang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGiohang(int Masach, string strUrl)
        {
            List<Giohang> lstGiohang = layGioHang();
            Giohang sanpham = lstGiohang.Find(n => n.iMaSach == Masach);
            if (sanpham == null)
            {
                sanpham = new Giohang(Masach);
                lstGiohang.Add(sanpham);
                return Redirect(strUrl);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strUrl);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGioHang = Session["GioHang"] as List<Giohang>;
            if (lstGioHang != null)
            {
                iTongTien = lstGioHang.Sum(n => n.iThanhTien);
            }
            return iTongTien;
        }
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = layGioHang();

            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        public ActionResult XoaGiohang(int Masach)
        {
            List<Giohang> lstGiohang = layGioHang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMaSach == Masach);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMaSach == Masach);
                return RedirectToAction("GioHang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGiohang(int iMaSach, FormCollection f)
        {
            List<Giohang> lstGiohang = layGioHang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMaSach == iMaSach);
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaAll()
        {
            List<Giohang> lstGiohang = layGioHang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "TrangChu");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if(Session["Taikhoan"] == null || Session["Taikhoan"].ToString()=="")
            {
                return RedirectToAction("Dangnhap", "Nguoidung");
            }
            if(Session["Giohang"]==null)
            {
                return RedirectToAction("Index", "Bookstore");
            }
            List<Giohang> lstGioHang = layGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<Giohang> gh = layGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDH = DateTime.Now;
            ddh.Trigia = (int)TongTien();
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", f["Ngaygiao"]);
            ddh.Ngaygiaohang = DateTime.Parse(ngaygiao);
            ddh.HTGiaohang = false;
            ddh.Dagiao = false;
            ddh.Tennguoinhan = kh.HoTenKH;
            ddh.Diachinhan = kh.DiachiKH;
            ddh.Dienthoainhan = kh.DienthoaiKH;
            db.DONDATHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            foreach(var item in gh)
            {
                CTDATHANG ctdh = new CTDATHANG();
                ctdh.SoDH = ddh.SoDH;
                ctdh.Masach = item.iMaSach;
                ctdh.Soluong = item.iSoLuong;
                ctdh.Dongia = (decimal)item.dDongia;
                db.CTDATHANGs.InsertOnSubmit(ctdh);
            }
            db.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "GioHang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}