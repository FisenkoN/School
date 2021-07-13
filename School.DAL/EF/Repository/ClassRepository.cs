using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.Models;

namespace School.DAL.EF.Repository
{
    public class ClassRepository:BaseRepository<Class>, IClassRepository, IRelated<Class, Teacher>
    {
        public IIncludableQueryable<Class, Teacher> GetRelatedData() =>
            Context.Classes.Include(c => c.Students)
                .Include(c => c.Teacher);

        public override Class GetOne(int? id) =>
            GetRelatedData()
                .First(c => c.Id == id);

        public override List<Class> GetSome(Expression<Func<Class, bool>> @where) =>
            GetRelatedData().
                Where(where).
                Select(c => c).
                ToList();

        public override List<Class> GetAll<TSortField>(Expression<Func<Class, TSortField>> orderBy, bool @ascending) =>
            (ascending ? GetRelatedData().
                    OrderBy(orderBy) : 
                GetRelatedData().
                    OrderByDescending(orderBy)).
            ToList();
    }
}