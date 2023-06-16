using System.Net.Mime;
using DbLibrary;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("/books")]
public class BookController : ControllerBase
{
    private readonly Context _context;

    public BookController(Context context) => _context = context;

    #region Methods

    #region GET
    
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<Book> GetBooks()
    {
        Console.WriteLine($"Enter into GET: /books");

        var books = _context.Books;

        return Ok(books);
    }
    
    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<Book> GetBook(int id)
    {
        Console.WriteLine($"Enter into GET: /books/{id}");

        var book = Context.GetBook(_context, id);

        return Ok(book);
    }

    #endregion

    #region POST

    [HttpPost("create")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostBookAsync(Book book)
    {
        Console.WriteLine("Enter into POST: /books/create");

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        return Ok();
    }

    #endregion

    #endregion
}
