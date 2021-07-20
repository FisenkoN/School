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

        public TeacherController()
        {
            db = new TeacherRepository();
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

            var teacher = db.GetOne(id);
            
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teacher/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Age,Gender,ClassId,Password,Email,Role,IsRegistered,Id,Timestamp")] Teacher teacher)
        {
            db.Add(teacher);
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Teacher/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var teacher = db.GetOne(id);
            
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FirstName,LastName,Age,Gender,ClassId,Password,Email,Role,IsRegistered,Id,Timestamp")] Teacher teacher)
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

            var teacher = db.GetOne(id);
            
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
            var teacher = db.GetOne(id);

            db.Delete(teacher);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
