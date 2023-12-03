using DoroTest.Application.Services.Book.Responses.GetBook;
using DoroTest.Domain.Enums;
using MediatR;

namespace DoroTest.Application.Services.Book.Commands.GetAllBooks;
public class GetAllBooksCommand : IRequest<BooksListVm>
{
    public string? Title { get; set; }
    public int? PageSize { get; set; }
    public int? PageLimit { get; set; }
    public GendersBookEnum? Gender { get; set; }
    public DateTime? CreatedDate { get; set; }
}
