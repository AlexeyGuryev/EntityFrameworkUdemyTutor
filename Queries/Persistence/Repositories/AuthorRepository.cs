using Queries.Core.Domain;
using Queries.Core.Repositories;

namespace Queries.Persistence.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(PlutoContext ctx) : base(ctx) { }
    }
}
