using AutoMapper;
using DoroTest.Application.Common.Exceptions;
using DoroTest.Application.Services.Book.Commands.UpdateBook;
using DoroTest.Application.Services.Book.Responses.GetBook;
using DoroTest.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace DoroTest.Application.Services.Book.Handlers;
public class UpdateUserCommandHandler : IRequestHandler<UpdateBookCommand, BookDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var selectBook = await _bookRepository.SelectAsync(x => x.Id.Equals(request.Id), cancellationToken: cancellationToken);
        if (selectBook == null)
            throw new NotFoundException(nameof(selectBook));

        selectBook.Title = request.Title.IsNullOrEmpty() ? selectBook.Title : request.Title;
        selectBook.Gender = request.Gender.HasValue ? selectBook.Gender : request.Gender.Value;

        var bookUpdated = await _bookRepository.UpdateAsync(selectBook);

        return _mapper.Map<BookDto>(bookUpdated);
    }
}
