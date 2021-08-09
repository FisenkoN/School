using System.Collections.Generic;
using School.DAL.Entities;

namespace School.DAL.Repository
{
    public interface IStudentRepository:IRepository<Student>
    {
        List<Student> GetOtherStudents();

        List<Student> GetStudentsOlderThen15Years();
    }
}