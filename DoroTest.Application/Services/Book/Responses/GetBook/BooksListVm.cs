namespace DoroTest.Application.Services.Book.Responses.GetBook;
public class BooksListVm
{
    public IList<BookDto> ListBooks { get; set; } = new List<BookDto>();
    public double? TotalPages { get; set; }
}
