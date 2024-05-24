
using BE_exam_18.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BE_exam_18.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Teams.ToList());
        }

      
    }
}
