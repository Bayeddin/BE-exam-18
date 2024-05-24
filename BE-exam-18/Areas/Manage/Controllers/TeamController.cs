using BE_exam_18.DAL;
using BE_exam_18.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BE_exam_18.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment environment;

        public TeamController(AppDbContext context,IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            return View(context.Teams.ToList());
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Team team)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!team.ImgFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImgFile", "File tipini duz daxil edin");
                return View();
            }
            string path = environment.WebRootPath + @"\upload\";
            string filename = Guid.NewGuid() + team.ImgFile.FileName;
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                team.ImgFile.CopyTo(stream);
            }
            team.ImgUrl = filename;
            context.Teams.Add(team);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Update(int id)
        {
            var team = context.Teams.FirstOrDefault(x => x.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }
        [HttpPost]
        public IActionResult Update(Team team)
        {
            if (!ModelState.IsValid && team.ImgFile != null)
            {
                return View(team);
            }
            var oldTeam = context.Teams.FirstOrDefault(x => x.Id == team.Id);
            if (team.ImgFile != null)
            {
                string path = environment.WebRootPath + @"\upload\";
                FileInfo fileInfo = new FileInfo(path + oldTeam.ImgUrl);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                string filename = Guid.NewGuid() + team.ImgFile.FileName;
                using (FileStream stream = new FileStream(path + filename, FileMode.Create))
                {
                    team.ImgFile.CopyTo(stream);
                }
                oldTeam.ImgUrl = filename;
            }
            oldTeam.Fullname = team.Fullname;
            oldTeam.Position = team.Position;
            oldTeam.Description = team.Description;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
           var team=context.Teams.FirstOrDefault(x => x.Id==id);
            context.Remove(team);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

