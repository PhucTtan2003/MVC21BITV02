using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixTMidTest.Data;
using SixTMidTest.Models;

namespace SixTMidTest.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly MyeStoreContext _storeContext;
        public NhaCungCapController(MyeStoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public IActionResult Index()
        {
            var data = _storeContext.NhaCungCaps.ToList();
            return View(data);
        }
        public IActionResult CreateNCC()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNCC(NhaCungCap ncc, IFormFile NCCLogo)
        {
            var existingNCC = _storeContext.NhaCungCaps
            .FirstOrDefault(sp => sp.TenCongTy == ncc.TenCongTy);

            if (existingNCC != null)
            {
                ModelState.AddModelError("TenCongTy", "Tên công ty đã tồn tại.");
            }
            if (!ModelState.IsValid)
            {
                return View(ncc);
            }
            if (NCCLogo != null)
            {
                ncc.Logo = MyTools.UploadImageToFolder(NCCLogo, "NhaCC");
            }
            _storeContext.Add(ncc);
            _storeContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
