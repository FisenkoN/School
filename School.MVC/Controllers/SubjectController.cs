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
        
        public SubjectController()
        {
            db = new SubjectRepository();

            TeacherRepository = new TeacherRepository();
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

            var subject = db.GetOne(id);
            
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }
        
        public IActionResult Create()
        {
            ViewData["Teacher"] = new SelectList(TeacherRepository.GetAll(), "Id", "FullName");
            
            return View();
        }

        // POST: Class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Subject subject)
        {
            db.Add(subject);
            
            ViewData["Teacher"] = new SelectList(
                TeacherRepository.GetAll()
                , "Id", "FullName");
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Class/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = db.GetOne(id);
            
            if (subject == null)
            {
                return NotFound();
            }

            var teachers = new List<Teacher>(TeacherRepository.GetAll());
            
            ViewData["Teacher"] = new MultiSelectList(teachers.ToList(), "Id", "FirstName");
            
            return View(subject);
        }

        // POST: Class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Class/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = db.GetOne(id);
            
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Class/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var subject = db.GetOne(id);

            db.Delete(subject);
            
            return RedirectToAction(nameof(Index));
        }
    }
}