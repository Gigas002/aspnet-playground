using System.Net.Mime;
using DbLibrary;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;
using SystemTextJsonPatch.Operations;

namespace Server.Controllers;

/*
 * UsersBooks (many-to-many) values can be pushed with PUT or PATCH. Both existing and new books can be pushed
 * UsersBooks can be obtained with GET
 * UsersNames (dependent on users) can be pushed with
 * UsersNames can be obtained with GET
 * Users can be pushed with POST
 * Users can be obtained with GET
 */

[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly Context _context;

    public UserController(Context context) => _context = context;

    #region Methods

    #region GET

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<User> GetUsers()
    {
        Console.WriteLine($"Enter into GET: /users");

        var users = _context.Users;

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<User> GetUser(int id)
    {
        Console.WriteLine($"Enter into GET: /users/{id}");

        var user = Context.GetUser(_context, id);

        return Ok(user);
    }

    [HttpGet("{id:int}/books")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<Book>> GetUserBooks(int id)
    {
        Console.WriteLine($"Enter into GET: /users/{id}/books");

        var books = Context.GetUserBooks(_context, id);

        return Ok(books);
    }

    [HttpGet("{id:int}/names")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<UsersNames>> GetUserNames(int id)
    {
        Console.WriteLine($"Enter into GET: /users/{id}/names");

        var names = Context.GetUserNames(_context, id);

        return Ok(names);
    }
    
    [HttpGet("{id:int}/relations")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<UsersNames>> GetUserRelations(int id)
    {
        Console.WriteLine($"Enter into GET: /users/{id}/relations");

        var relations = Context.GetUserRelations(_context, id);

        return Ok(relations);
    }

    #endregion

    #region POST

    [HttpPost("create")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostUserAsync(User user)
    {
        Console.WriteLine("Enter into POST: /users/create");

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok();
    }

    #endregion

    #region PUT

    [HttpPut("{id:int}/books")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutUserBooksAsync(int id, IEnumerable<Book> books)
    {
        Console.WriteLine($"Enter into PUT: /users/{id}/books");

        var user = Context.GetUser(_context, id);

        user.Books.AddRange(books);

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id:int}/names")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutUserNamesAsync(int id, IEnumerable<UsersNames> names)
    {
        Console.WriteLine($"Enter into PUT: /users/{id}/names");

        var user = Context.GetUser(_context, id);

        user.UserNames.AddRange(names);
        
        // in case IEnumerable<string> on input:
        // foreach (var name in names)
        // {
        //     user.AddName(name);
        // }

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id:int}/relations")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutUserRelationsAsync(int id, IEnumerable<UsersRelations> relations)
    {
        Console.WriteLine($"Enter into PUT: /users/{id}/names");

        var user = Context.GetUser(_context, id);

        user.UsersRelations.AddRange(relations);

        await _context.SaveChangesAsync();

        return Ok();
    }

    #endregion

    #region PATCH

    [HttpPatch("{id:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<ActionResult<User>> PatchUserAsync(int id, IEnumerable<Operation<User>> operations)
    {
        Console.WriteLine($"Enter into PATCH: /users/{id}");

        var patch = new JsonPatchDocument<User>(operations.ToList(), new());

        var user = Context.GetUser(_context, id);

        patch.ApplyTo(user);

        await _context.SaveChangesAsync();

        return Ok();
    }

    #endregion

    #endregion
}
