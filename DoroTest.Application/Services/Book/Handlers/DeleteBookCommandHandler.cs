using AutoMapper;
using DoroTest.Application.Common.Exceptions;
using DoroTest.Application.Services.Book.Commands.DeleteBook;
using DoroTest.Domain.Interfaces.Repository;
using MediatR;

namespace DoroTest.Application.Services.Book.Handlers;
public class DeleteUserCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;
    public DeleteUserCommandHandler(IMapper mapper, IBookRepository bookRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
    }

    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var selectBook = await _bookRepository.SelectAsync(x => x.Id.Equals(request.Id), cancellationToken: cancellationToken);
        if (selectBook == null)
            throw new NotFoundException(nameof(selectBook));

        var deleteBook = await _bookRepository.DeleteAsync(x => x.Id.Equals(selectBook.Id));

        return Unit.Value;
    }
}
