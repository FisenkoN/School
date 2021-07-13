using System.Collections.Generic;
using School.Models;

namespace School.DAL.EF.Repository
{
    public interface IStudentRepository:IRepository<Student>
    {
        List<Student> GetMaleStudents();

        List<Student> GetStudentsOlderThen15Years();
    }
}