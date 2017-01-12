using System.Collections.Generic;

namespace Queries.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> ListTop(int count);
        IEnumerable<Course> ListWithAuthorsPaged(int pageIndex, int pageSize);
    }
}
