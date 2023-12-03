using DoroTest.Application.Services.Book.Responses.GetBook;
using DoroTest.Domain.Enums;
using MediatR;

namespace DoroTest.Application.Services.Book.Commands.UpdateBook;
public class UpdateBookCommand : IRequest<BookDto>
{
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public GendersBookEnum? Gender { get; set; }
}
