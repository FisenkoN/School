using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.DAL.EF.Repository;
using School.Models;

namespace School.MVC.Controllers
{
    public class SubjectController:Controller
    {
        private SubjectRepository db;

        private TeacherRepository TeacherRepository;
        
        private StudentRepository StudentRepository;
        
        public SubjectController()
        {
            db = new SubjectRepository();

            TeacherRepository = new TeacherRepository();

            StudentRepository = new StudentRepository();
        }
        
        public IActionResult Index()
        {
            return View(db.GetAll());
        }
        
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = db.GetOneRelated(id);
            
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }
        
        public IActionResult Create()
        {
            ViewData["Teachers"] = new SelectList(TeacherRepository.GetAll(), "Id", "FullName");
            ViewData["Students"] = new SelectList(StudentRepository.GetAll(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Subject data)
        {
            db.Add(data);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = db.GetOneRelated(id);
            
            if (subject == null)
            {
                return NotFound();
            }

            ViewData["Teachers"] = new SelectList(TeacherRepository.GetAll(), "Id", "FullName");
            ViewData["Students"] = new SelectList(StudentRepository.GetAll(), "Id", "FullName");
            
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name,TeacherId,Id,Timestamp")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }
            
            db.Update(subject);
                
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = db.GetOneRelated(id);
            
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var subject = db.GetOneRelated(id);

            db.Delete(subject);
            
            return RedirectToAction(nameof(Index));
        }
    }
}