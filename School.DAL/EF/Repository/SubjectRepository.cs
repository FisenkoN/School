﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.Models;

namespace School.DAL.EF.Repository
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository, IRelated<Subject,ICollection<Teacher>>
    {
        public override List<Subject> GetAll<TSortField>(Expression<Func<Subject, TSortField>> orderBy, bool @ascending) =>
            (@ascending ? GetRelatedData().OrderBy(orderBy) :
                GetRelatedData().OrderByDescending(orderBy)).ToList();

        public List<Subject> GetSubjectWithoutStudents() =>
             GetRelatedData().
                 Where(s => s.Students.Count == 0).
                 Select(s => s).
                 ToList();

        public override Subject GetOne(int? id) =>
            GetRelatedData().
                First(s => s.Id == id);

        public IIncludableQueryable<Subject, ICollection<Teacher>> GetRelatedData() => 
            Context.Subjects.Include(s => s.Students)
                .Include(s => s.Teachers);


        public override List<Subject> GetSome(Expression<Func<Subject, bool>> @where) =>
            GetRelatedData().
                Where(where).
                ToList();
    }
}