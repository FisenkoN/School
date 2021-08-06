using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.DAL.EF.Repository;
using School.Models;

namespace School.MVC.Controllers
{
    public class ClassController : Controller
    {
        private ClassRepository db;
        
        private TeacherRepository TeacherRepository;
        
        private StudentRepository StudentRepository;
        
        
        public ClassController()
        {
            db = new ClassRepository();
            
            TeacherRepository = new TeacherRepository();
        
            StudentRepository = new StudentRepository();
        }
        
        // GET: Class
        public IActionResult Index()
        {
            return View(db.GetRelatedData());
        }

        // GET: Class/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = db.GetOneRelated(id);
            
            if (@class == null)
            {
                return NotFound();
            }
        
            return View(@class);
        }
        
        // GET: Class/Create
        public IActionResult Create()
        {
            ViewData["Teachers"] = new SelectList(TeacherRepository.GetSome(t => t.Class == null), "Id", "FullName");
        
            ViewData["Students"] = new MultiSelectList(StudentRepository.GetSome(s => s.ClassId == null), "Id", "FullName");
            
            return View();
        }

        // POST: Class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Class @class)
        {
            db.Add(@class);

            ViewData["Teachers"] = new SelectList(
                TeacherRepository.GetSome(t => t.Class == null)
                , "Id", "FullName");
            
            ViewData["Students"] = new MultiSelectList(
                StudentRepository.GetSome(s => s.ClassId == null)
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

            var @class = db.GetOneRelated(id);
            
            if (@class == null)
            {
                return NotFound();
            }
            
            var teachers = TeacherRepository.GetSome(t => t.Class == null).ToList();

            var students = StudentRepository.GetAll().Where(s => s.ClassId == null).ToList();
            
            teachers.Add(@class?.Teacher);
            
            students.AddRange(@class.Students);
            
            ViewData["Teachers"] = new SelectList(teachers, "Id", "FullName");

            ViewData["Students"] = new MultiSelectList(students, "Id", "FullName");
            
            return View(@class);
        }

        // POST: Class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name,TeacherId,Id,Timestamp,Students")] Class @class)
        {
            if (id != @class.Id) return BadRequest();

            if (!ModelState.IsValid) return View(@class);
            
            try
            {
                db.Update(@class);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty,
                    $@"Unable to save the record. Another user has updated it. {ex.Message}");
                return View(@class);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    $@"Unable to save the record. {ex.Message}");
                return View(@class);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Class/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = db.GetOneRelated(id);
            
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Class/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var @class = db.GetOneRelated(id);

            db.Delete(@class);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
