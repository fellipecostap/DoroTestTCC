using DoroTest.Application.Services.Book.Responses.GetBook;
using DoroTest.Domain.Enums;
using MediatR;

namespace DoroTest.Application.Services.Book.Commands.CreateBook;
public class CreateBookCommand : IRequest<BookDto>
{
    public string Title { get; set; }

    public GendersBookEnum Gender { get; set; }
}
