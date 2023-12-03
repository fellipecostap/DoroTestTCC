using DoroTest.Application.Services.Book.Commands.CreateBook;
using DoroTest.Application.Services.Book.Commands.DeleteBook;
using DoroTest.Application.Services.Book.Commands.GetAllBooks;
using DoroTest.Application.Services.Book.Commands.GetBook;
using DoroTest.Application.Services.Book.Commands.UpdateBook;
using DoroTest.Application.Services.Book.Responses.GetBook;
using DoroTest.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoroTest.Controllers;

public class BooksController : ApiControllerBase
{
    [Authorize("Admin")]
    [AllowAnonymous]
    [HttpGet()]
    public async Task<ActionResult<BooksListVm>> GetAll(string? title = null, GendersBookEnum? gender = null, int? pageLimit = null, int? pageSize = null, DateTime? createdDate = null)
    {
        return await Mediator.Send(new GetAllBooksCommand()
        {
            Title = title,
            Gender = gender, 
            PageLimit = pageLimit,
            PageSize = pageSize,
            CreatedDate = createdDate
        });
    }

    [Authorize("Admin")]
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> Get(Guid id)
    {
        return await Mediator.Send(new GetBookCommand() { Id = id });
    }

    [Authorize("Admin")]
    [AllowAnonymous]
    [HttpDelete]
    public async Task<ActionResult<Unit>> Delete(Guid id)
    {
        return await Mediator.Send(new DeleteBookCommand() { Id = id });
    }

    [Authorize("Admin")]
    [AllowAnonymous]
    [HttpPut]
    public async Task<ActionResult<BookDto>> Update(UpdateBookCommand command)
    {
        return await Mediator.Send(command);
    }

    [Authorize("Admin")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<BookDto>> Post(CreateBookCommand command)
    {
        return await Mediator.Send(command);
    }
}
