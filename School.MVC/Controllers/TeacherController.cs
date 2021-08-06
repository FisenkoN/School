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
    public class TeacherController : Controller
    {
        private TeacherRepository db;
        
        private readonly ClassRepository ClassRepository;
        
        private readonly SubjectRepository SubjectRepository;

        public TeacherController()
        {
            db = new TeacherRepository();

            ClassRepository = new ClassRepository();

            SubjectRepository = new SubjectRepository();
        }

        // GET: Teacher
        public IActionResult Index()
        {
            return View(db.GetAll());
        }

        // GET: Teacher/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var teacher = db.GetOneRelated(id);
            
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teacher/Create
        public IActionResult Create()
        {
            ViewData["Classes"] = new SelectList(ClassRepository.GetSome(c=>c.Teacher==null), "Id", "Name");
            
            ViewData["Subjects"] = new MultiSelectList(SubjectRepository.GetAll(), "Id", "Name");
            
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Age,Gender,ClassId,Password,Email,Role,IsRegistered,Id,Timestamp,Subjects")] Teacher teacher)
        {
            db.Add(teacher);
            
            ViewData["Classes"] = new SelectList(ClassRepository.GetSome(c=>c.Teacher==null), "Id", "Name");
            
            ViewData["Subjects"] = new MultiSelectList(SubjectRepository.GetAll(), "Id", "Name");
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Teacher/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var teacher = db.GetOneRelated(id);
            
            if (teacher == null)
            {
                return NotFound();
            }

            var classesIQuery = ClassRepository.GetSome(c => c.Teacher == null);

            var classes = classesIQuery.ToList();


            if (teacher?.Class != null)
            {
                classes.Add(teacher?.Class);
            }
            

            var subjects = SubjectRepository.GetAll();
            
            ViewData["Classes"] = new SelectList(classes, "Id", "Name");
            
            ViewData["Subjects"] = new MultiSelectList(subjects, "Id", "Name");
            
            return View(teacher);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FirstName,LastName,Age,Gender,ClassId,Password,Email,Role,IsRegistered,Id,Timestamp, Subjects")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }
            
            db.Update(teacher);
                
            return RedirectToAction(nameof(Index));
        }

        // GET: Teacher/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var teacher = db.GetOneRelated(id);
            
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost][ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var teacher = db.GetOneRelated(id);

            db.Delete(teacher);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
