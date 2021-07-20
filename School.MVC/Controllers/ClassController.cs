using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            var @class = db.GetOne(id);
            
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Class/Create
        public IActionResult Create()
        {
            ViewData["Teacher"] = new SelectList(TeacherRepository.GetSome(t => t.IsClassMate == false), "Id", "FullName");

            var o = StudentRepository.GetSome(s => s.Class == null);
            
            ViewData["Students"] = new MultiSelectList(o, "Id", "FullName");
            
            return View();
        }

        // POST: Class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Class @class)//На цьому етапі class
                                                 //приходить з порожнім списком студентів
        {
            db.Add(@class);

            var r = new TeacherRepository();

            var t = r.GetOne(@class.TeacherId);

            t.IsClassMate = true;

            r.Update(t);
            
            ViewData["Teacher"] = new SelectList(
                TeacherRepository.GetSome(t => t.IsClassMate == false)
                , "Id", "FullName");
            
            ViewData["Students"] = new MultiSelectList(
                StudentRepository.GetSome(s => s.Class == null)
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

            var @class = db.GetOne(id);
            
            if (@class == null)
            {
                return NotFound();
            }

            var teachers = new List<Teacher>(TeacherRepository.GetSome(t => t.IsClassMate == false));
            
            teachers.Add(@class.Teacher);
            
            ViewData["TeacherId"] = new SelectList(teachers.ToList(), "Id", "FirstName");
            
            return View(@class);
        }

        // POST: Class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name,TeacherId,Id,Timestamp")] Class @class)
        {
            if (id != @class.Id)
            {
                return NotFound();
            }
            
            db.Update(@class);
                
            return RedirectToAction(nameof(Index));
        }

        // GET: Class/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = db.GetOne(id);
            
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
            var @class = db.GetOne(id);

            db.Delete(@class);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
