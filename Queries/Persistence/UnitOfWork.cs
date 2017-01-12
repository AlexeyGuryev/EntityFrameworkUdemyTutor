using System;
using Queries.Core;
using Queries.Repositories;

namespace Queries.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PlutoContext _ctx;

        public UnitOfWork(PlutoContext ctx)
        {
            _ctx = ctx;
            Courses = new CourseRepository(_ctx);
            Authors = new AuthorRepository(_ctx);
        }

        public ICourseRepository Courses { get; set; }
        public IAuthorRepository Authors { get; set; }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
