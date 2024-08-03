using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixTMidTest.Data;
using SixTMidTest.Models;

namespace SixTMidTest.Controllers
{
    public class HDController : Controller
    {
        private MyeStoreContext db = new MyeStoreContext();
        public IActionResult Details()
        {
            var query = from hh in db.HangHoas
                        join ct in db.ChiTietHds on hh.MaHh equals ct.MaHh
                        select new Models.HangHoa
                        {
                            MaHH = hh.MaHh,
                            TenHH = hh.TenHh,
                            MoTaDonVi = hh.MoTaDonVi,
                            DonGia = (decimal)ct.DonGia,
                            SoLuong = ct.SoLuong,
                            ThanhTien = (decimal)(ct.SoLuong * ct.DonGia)
                        };

            var viewModel = query.ToList();
            return View(viewModel);
        }
    }
}

