using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SixT.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SixT.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly MyeStoreContext _context;

        public HangHoaController(MyeStoreContext context)
        {
            _context = context;
        }

        // GET: HangHoa/Create
        public IActionResult Create()
        {
            ViewData["Loai"] = _context.Loais.ToList();
            ViewData["NhaCungCap"] = _context.NhaCungCaps.ToList();
            ViewBag.MaLoai = new SelectList(_context.Loais, "MaLoai", "TenLoai");
            ViewBag.MaNcc = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy");
            return View();
        }

        // POST: HangHoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HangHoa hangHoa, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // Upload file
                if (file != null && file.Length > 0)
                {
                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    var filePath = System.IO.Path.Combine("wwwroot/Hinh/HangHoa", fileName);

                    using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    hangHoa.Hinh = fileName;
                }

                // Lưu hàng hóa vào cơ sở dữ liệu
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index)); // Điều hướng về trang danh sách sau khi thêm thành công
            }

            ViewData["Loai"] = _context.Loais.ToList();
            ViewData["NhaCungCap"] = _context.NhaCungCaps.ToList();
            return View(hangHoa);
        }

        // GET: HangHoa/Index
        public IActionResult Index()
        {
            var hangHoas = _context.HangHoas.Include(h => h.MaLoaiNavigation).Include(h => h.MaNccNavigation).ToList();
            return View(hangHoas);
        }
    }
}
