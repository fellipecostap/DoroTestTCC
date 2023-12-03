using AutoMapper;
using DoroTest.Application.Common.Exceptions;
using DoroTest.Application.Services.Book.Commands.GetBook;
using DoroTest.Application.Services.Book.Responses.GetBook;
using DoroTest.Application.Services.User.Commands.GetUser;
using DoroTest.Domain.Interfaces.Repository;
using MediatR;

namespace DoroTest.Application.Services.Book.Handlers;
public class GetUserCommandHandler : IRequestHandler<GetBookCommand, BookDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetUserCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BookDto> Handle(GetBookCommand request, CancellationToken cancellationToken)
    {
        var selectBook = await _bookRepository.SelectAsync(x => x.Id.Equals(request.Id) || x.Title.Equals(request.Title), cancellationToken: cancellationToken);
        if (selectBook == null)
            throw new NotFoundException(nameof(selectBook));

        return _mapper.Map<BookDto>(selectBook);
    }
}
