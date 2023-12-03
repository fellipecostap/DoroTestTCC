using AutoMapper;
using DoroTest.Application.Services.Book.Commands.CreateBook;
using DoroTest.Application.Services.Book.Responses.GetBook;
using DoroTest.Application.Services.User.Commands.CreateUser;
using DoroTest.Domain.Entities;
using DoroTest.Domain.Interfaces.Repository;
using MediatR;

namespace DoroTest.Application.Services.Book.Handlers;
public class CreateUserCommandHandler : IRequestHandler<CreateBookCommand, BookDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IBookRepository bookRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
    }

    public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var bookToCreate = new BookEntity()
        {
            Title = request.Title,
            Gender = request.Gender,
        };

        var bookCreated = await _bookRepository.InsertAsync(bookToCreate);

        return _mapper.Map<BookDto>(bookCreated);
    }
}
