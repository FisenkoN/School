using System.Collections.Generic;
using School.Models;

namespace School.DAL.EF.Repository
{
    public interface ISubjectRepository:IRepository<Subject>
    {
        List<Subject> GetSubjectWithoutStudents();
    }
}