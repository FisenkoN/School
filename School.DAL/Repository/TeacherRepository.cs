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
    public class TeacherRepository:BaseRepository<Teacher>, ITeacherRepository, IRelated<Teacher, Class>
    {
        public TeacherRepository(SchoolDbContext db):base(db) { }
        public override List<Teacher> GetAll<TSortField>(Expression<Func<Teacher, TSortField>> orderBy, bool @ascending)=>
            (@ascending ? GetRelatedData().OrderBy(orderBy) :
                GetRelatedData().OrderByDescending(orderBy)).ToList();

        public IIncludableQueryable<Teacher, Class> GetRelatedData() =>
            DbContext.Teachers.Include(t => t.Subjects)
                .Include(t => t.Class);

        public Teacher GetOneRelated(int? id) =>
            GetRelatedData().ToList().Find(t => t.Id == id);

        public override IQueryable<Teacher> GetSome(Expression<Func<Teacher, bool>> @where) =>
            GetRelatedData().Where(where).Select(t => t);
    }
}