using Microsoft.EntityFrameworkCore.Query;

namespace School.DAL.EF.Repository
{
    public interface IRelated<T,Y>
    {
        public IIncludableQueryable<T, Y> GetRelatedData();
    }
}