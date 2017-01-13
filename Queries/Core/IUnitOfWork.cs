using Queries.Core.Repositories;
using System;

namespace Queries.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; set; }
        IAuthorRepository Authors { get; set; }
        void SaveChanges();
    }
}
