using MediatR;

namespace DoroTest.Application.Services.Book.Commands.DeleteBook;
public class DeleteBookCommand : IRequest<Unit>
{
    public Guid? Id { get; set; }
}
