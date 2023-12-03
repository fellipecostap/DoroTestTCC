using DoroTest.Domain.Entities;
using DoroTest.Domain.Interfaces.Repository;
using DoroTest.Infrastructure.Common;
using DoroTest.Infrastructure.Context;

namespace DoroTest.Infrastructure.Repository;
public class BookRepository : BaseRepository<BookEntity>, IBookRepository
{
    public BookRepository(ApplicationDbContext context) : base(context)
    {

    }
}
