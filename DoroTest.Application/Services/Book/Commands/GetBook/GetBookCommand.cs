using DoroTest.Application.Services.Book.Responses.GetBook;
using MediatR;

namespace DoroTest.Application.Services.Book.Commands.GetBook;
public class GetBookCommand : IRequest<BookDto>
{
    public Guid? Id { get; set; }

    public string? Title { get; set; }
}
