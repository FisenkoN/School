using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.DAL.EF;
using School.DAL.EF.Repository;
using School.Models;

namespace School.MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentRepository db;

        private readonly ClassRepository ClassRepository;
        
        private readonly SubjectRepository SubjectRepository;

        public StudentController()
        {
            db = new StudentRepository();
            
            ClassRepository = new ClassRepository();

            SubjectRepository = new SubjectRepository();
        }

        // GET: Student
        public IActionResult Index()
        {
            return View(db.GetAll());
        }

        // GET: Student/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = db.GetOne(id);
            
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(ClassRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Age,Gender,ClassId,Password,Email,Role,IsRegistered,Id,Timestamp")] Student student)
        {
            db.Add(student);
            
            ViewData["ClassId"] = new SelectList(ClassRepository.GetAll(), "Id", "Name", student.ClassId);
            
            return View(student);
        }

        // GET: Student/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = db.GetOne(id);
            
            if (student == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(ClassRepository.GetAll(), "Id", "Name", student.ClassId);
            
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FirstName,LastName,Age,Gender,ClassId,Password,Email,Role,IsRegistered,Id,Timestamp")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            db.Update(student);
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Student/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = db.GetOne(id);
            
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost][ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = db.GetOne(id);

            db.Delete(student);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
