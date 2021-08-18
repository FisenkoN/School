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
    public class ClassRepository:BaseRepository<Class>, IClassRepository, IRelated<Class, Teacher>
    {
        public ClassRepository(SchoolDbContext dbContext):base(dbContext) { }
        public IIncludableQueryable<Class, Teacher> GetRelatedData() =>
            DbContext.Classes.Include(c => c.Students)
                .Include(c => c.Teacher);

        public Class GetOneRelated(int? id) =>
            GetRelatedData()
                .First(c => c.Id == id);

        public override IQueryable<Class> GetSome(Expression<Func<Class, bool>> @where) =>
            GetRelatedData().Where(where).Select(c => c);

        public override List<Class> GetAll<TSortField>(Expression<Func<Class, TSortField>> orderBy, bool @ascending) =>
            (ascending ? GetRelatedData().OrderBy(orderBy) : GetRelatedData().OrderByDescending(orderBy)).ToList();

    }
}