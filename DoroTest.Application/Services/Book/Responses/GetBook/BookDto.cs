using DoroTest.Application.Common.Mappings;
using DoroTest.Domain.Entities;
using DoroTest.Domain.Enums;

namespace DoroTest.Application.Services.Book.Responses.GetBook;
public class BookDto : IMapFrom<BookEntity>
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public int? PageSize { get; set; }

    public int? CurrentPage { get; set; }

    public GendersBookEnum? Gender { get; set; }
}
