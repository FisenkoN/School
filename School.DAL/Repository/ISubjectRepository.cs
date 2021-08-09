using System.Collections.Generic;
using School.DAL.Entities;

namespace School.DAL.Repository
{
    public interface ISubjectRepository:IRepository<Subject>
    {
        List<Subject> GetSubjectWithoutStudents();
    }
}