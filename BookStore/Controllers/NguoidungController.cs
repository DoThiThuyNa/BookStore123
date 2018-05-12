using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using System.Web.Security;

namespace BookStore.Controllers
{
    public class NguoidungController : Controller
    {
        // GET: Nguoidung
        dbQLBansachDataContext db = new dbQLBansachDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["DienThoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được đễ trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Tên Đăng Nhập không được đễ trống";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Mật Khẩu không được đễ trống";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi5"] = "Phải nhập điện thoại";
            }
            if (String.IsNullOrEmpty(ngaysinh))
            {
                ViewData["Loi6"] = "Phải nhập ngày sinh";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi6"] = "Phải nhập email";
            }
            else
            {
                kh.HoTenKH = hoten;
                kh.TenDN = tendn;
                kh.Matkhau = matkhau;
                kh.Email = email;
                kh.DiachiKH = diachi;
                kh.DienthoaiKH = dienthoai;
                kh.Ngaysinh = DateTime.Parse(ngaysinh);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else
            {
                if (String.IsNullOrEmpty(matkhau))
                {
                    ViewData["Loi2"] = "Phải nhập mật khẩu";
                }
                else
                {
                    KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TenDN == tendn && n.Matkhau == matkhau);
                    if (kh != null)
                    {
                        ViewBag.Thongbao = "Chào mừng DN thành công";
                        Session["Taikhoan"] = kh;
                        return RedirectToAction("Dathang", "GioHang");
                    }
                    else
                        ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        public ActionResult Xinchao()
        {
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            if (kh != null)
                ViewBag.Thongbao = "Xin Chào " + kh.HoTenKH.ToString();
            else
                ViewBag.Thongbao = "";
            return PartialView();
        }
        public ActionResult Logout()
        {
            Session["Taikhoan"] = null;
            return RedirectToAction("Index", "Bookstore");
        }
        
    }
}