using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.Models;

namespace School.DAL.EF.Repository
{
    public class TeacherRepository:BaseRepository<Teacher>, ITeacherRepository, IRelated<Teacher, Class>
    {
        public override List<Teacher> GetAll<TSortField>(Expression<Func<Teacher, TSortField>> orderBy, bool @ascending)=>
            (@ascending ? GetRelatedData().OrderBy(orderBy) :
                GetRelatedData().OrderByDescending(orderBy)).ToList();

        public IIncludableQueryable<Teacher, Class> GetRelatedData() =>
            Context.Teachers.Include(t => t.Subjects)
                .Include(t => t.Class);

        public override Teacher GetOne(int? id) =>
            GetRelatedData().First(t => t.Id == id);

        public IQueryable<Teacher> GetSome(Expression<Func<Teacher, bool>> @where) =>
            GetRelatedData().Where(where).Select(t => t);
    }
}