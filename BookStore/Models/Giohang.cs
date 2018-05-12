using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Giohang
    {
        dbQLBansachDataContext db = new dbQLBansachDataContext();
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sHinhminhhoa { get; set; }
        public Double dDongia { get; set; }
        public int iSoLuong { get; set; }
        public Double iThanhTien
        {
            get { return iSoLuong * dDongia; }
        }
        public Giohang(int Masach)
        {
            iMaSach = Masach;
            SACH sach = db.SACHes.Single(n => n.Masach == Masach);
            sTenSach = sach.Tensach;
            sHinhminhhoa = sach.Hinhminhhoa;
            dDongia = double.Parse(sach.Dongia.ToString());
            iSoLuong = 1;
        }
    }
}