using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Queries.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(PlutoContext ctx) : base(ctx) { }

        private PlutoContext PlutoContext { get { return _ctx as PlutoContext; } }

        public IEnumerable<Course> ListTop(int count)
        {
            return PlutoContext.Courses
                .OrderByDescending(c => c.FullPrice)
                .Take(count)
                .ToList();
        }

        public IEnumerable<Course> ListWithAuthorsPaged(int pageIndex, int pageSize)
        {
            return PlutoContext.Courses
                .Include(c => c.Author)
                .OrderBy(c => c.Name)
                .Skip((pageIndex) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
