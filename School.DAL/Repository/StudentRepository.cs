using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.DAL.EF.Repository;
using School.DAL.Entities;

namespace School.DAL.Repository
{
    public class StudentRepository:BaseRepository<Student>, IStudentRepository, IRelated<Student,Class>
    {
        public List<Student> GetOtherStudents() =>
            GetRelatedData().
                Where(s => s.Gender == Gender.Other).
                Select(s => s).
                ToList();
        
        public List<Student> GetStudentsOlderThen15Years() =>
            GetRelatedData().
                Where(s => s.Age >= 15).
                Select(s => s).
                ToList();
        
        public override Student GetOne(int? id) =>
             GetAll().FirstOrDefault(s => s.Id == id);
        
        public IIncludableQueryable<Student,Class> GetRelatedData() => 
            DbContext.Students.Include(s => s.Subjects)
                .Include(s => s.Class);

        public Student GetOneRelated(int? id) =>
            GetRelatedData().FirstOrDefault(s => s.Id == id);


        public override IQueryable<Student> GetSome(Expression<Func<Student, bool>> @where) =>
            GetRelatedData().Where(where);

        public override List<Student> GetAll<TSortField>(Expression<Func<Student, TSortField>> orderBy, bool ascending) =>
            (ascending ? GetRelatedData().OrderBy(orderBy) : GetRelatedData().OrderByDescending(orderBy)).ToList();
        
    }
}