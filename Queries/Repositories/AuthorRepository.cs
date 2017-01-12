namespace Queries.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(PlutoContext ctx) : base(ctx) { }
    }
}
