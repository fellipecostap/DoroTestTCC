using AutoMapper;
using DoroTest.Application.Common.Exceptions;
using DoroTest.Application.Common.Functions;
using DoroTest.Application.Services.Book.Commands.GetAllBooks;
using DoroTest.Application.Services.Book.Responses.GetBook;
using DoroTest.Application.Services.User.Commands.GetUser;
using DoroTest.Application.Services.User.Responses.GetUser;
using DoroTest.Domain.Entities;
using DoroTest.Domain.Interfaces.Repository;
using LinqKit;
using MediatR;
using OpenXmlPowerTools;
using System.Globalization;

namespace DoroTest.Application.Services.Book.Handlers;
public class GetAllUsersCommandHandler : IRequestHandler<GetAllBooksCommand, BooksListVm>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetAllUsersCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BooksListVm> Handle(GetAllBooksCommand request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<BookEntity>();
        bool filtered = false;

        if (!String.IsNullOrEmpty(request.Title))
        {
            predicate = predicate.And(x => x.Title.Contains(request.Title));
            filtered = true;
        }
        if (request.Gender.HasValue)
        {
            predicate = predicate.And(x => x.Gender.Equals(request.Gender));
            filtered = true;
        }
        if (request.CreatedDate.HasValue)
        {
            predicate = predicate.And(x => x.Created <= request.CreatedDate.Value.Date.ToUniversalTime());
            filtered = true;
        }

        IEnumerable<BookEntity> selectAllBooks;
        if (filtered)
            selectAllBooks = await _bookRepository.SelectAllAsync(predicate);
        else
            selectAllBooks = await _bookRepository.SelectAllAsync();

        if (selectAllBooks.Count() == 0)
            throw new NotFoundException(nameof(selectAllBooks));

        var pageSize = request.PageSize ?? 10;
        var pageLimit = request.PageLimit;
        double totalPages = pageLimit.HasValue ? pageLimit.Value : Math.Ceiling((double)selectAllBooks.Count() / pageSize);

        List<BookDto> booksList = new List<BookDto>();

        int bookCount = 1;
        int currentPage = 1;

        foreach (var book in selectAllBooks)
        {
            if (!pageLimit.HasValue || currentPage <= totalPages)
            {
                var bookResponse = new BookDto()
                {
                    Id = book.Id,
                    Gender = book.Gender,
                    PageSize = pageSize,
                    Title = book.Title,
                    CurrentPage = currentPage
                };
                booksList.Add(bookResponse);
                if (bookCount >= pageSize)
                {
                    bookCount = 0;
                    currentPage++;
                }
                bookCount++;
            }
        }

        return new BooksListVm
        {
            ListBooks = _mapper.Map<IList<BookDto>>(booksList),
            TotalPages = totalPages
        };
    }
}
