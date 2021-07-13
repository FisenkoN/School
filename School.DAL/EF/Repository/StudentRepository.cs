using System.Collections.Generic;
using System.Linq;
using School.Models;

namespace School.DAL.EF.Repository
{
    public class StudentRepository:BaseRepository<Student>, IStudentRepository
    {
        public List<Student> GetMaleStudents()
        {
            return Context.Students.Where(s => s.Gender == Gender.Male).Select(s => s).ToList();
        }

        public List<Student> GetStudentsOlderThen15Years()
        {
            return Context.Students.Where(s => s.Age >= 15).Select(s => s).ToList();
        }
    }
}