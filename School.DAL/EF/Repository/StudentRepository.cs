using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.Models;

namespace School.DAL.EF.Repository
{
    public class StudentRepository:BaseRepository<Student>, IStudentRepository
    {
        public List<Student> GetMaleStudents() =>
            GetRelatedData().
                Where(s => s.Gender == Gender.Male).
                Select(s => s).
                ToList();
        
        public List<Student> GetStudentsOlderThen15Years() =>
            GetRelatedData().
                Where(s => s.Age >= 15).
                Select(s => s).
                ToList();
        

        public override List<Student> GetAll() =>
            GetRelatedData().ToList();

        public override Student GetOne(int? id) =>
             GetRelatedData().First(s => s.Id == id);
        
        private IIncludableQueryable<Student,Class> GetRelatedData() => 
            Context.Students.Include(s => s.Subjects)
                .Include(s => s.Class);
        
    }
}