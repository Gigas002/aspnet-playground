using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DbLibrary;
using SystemTextJsonPatch.Operations;

namespace Client;

public class Program
{
    public const string ServerAddress = "http://localhost:5230";

    public static async Task Main()
    {
        await ClientAsync();
    }

    public static async Task ClientAsync()
    {
        var userId = 3;
        var bookId = 1;

        using var httpClient = new HttpClient();

        var uri = new Uri($"{ServerAddress}/users");

        var users = await GetUsersAsync(httpClient, uri);
        
        uri = new Uri($"{ServerAddress}/users/create");
        
        await PostUserAsync(httpClient, uri);

        uri = new Uri($"{ServerAddress}/books/{bookId}");
        
        var book = await GetBookAsync(httpClient, uri);
        
        uri = new Uri($"{ServerAddress}/users/{userId}");

        await PatchUserAsync(httpClient, uri, book);
        
        uri = new Uri($"{ServerAddress}/users/{userId}/names");

        await PutUserNamesAsync(httpClient, uri);
        
        uri = new Uri($"{ServerAddress}/users/{userId}");
        
        var user = await GetUserAsync(httpClient, uri);

        uri = new Uri($"{ServerAddress}/books/create");

        await PostBookAsync(httpClient, uri);
        
        uri = new Uri($"{ServerAddress}/users/{1}/relations");

        await PutUserRelationsAsync(httpClient, uri);
    }

    public static async Task<IEnumerable<User>> GetUsersAsync(HttpClient httpClient, Uri uri)
    {
        using var response = await httpClient.GetAsync(uri);

        return await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
    }

    public static async Task<User> GetUserAsync(HttpClient httpClient, Uri uri)
    {
        using var response = await httpClient.GetAsync(uri);

        return await response.Content.ReadFromJsonAsync<User>();
    }
    
    public static async Task PostUserAsync(HttpClient httpClient, Uri uri)
    {
        var user = new User { Age = 51 };
        user.AddName("Jack the Salesman");

        var json = JsonSerializer.Serialize(user);
        
        using var response = await httpClient.PostAsJsonAsync(uri, user);
    }

    public static async Task PatchUserAsync(HttpClient httpClient, Uri uri, Book bookToAdd)
    {
        var books = new List<Book>()
        {
            bookToAdd,
            new() { Title = "Kobzone" },
        };

        var userNames = new List<UsersNames>()
        {
            new(new User() { Id = 3 }, "gugobick")
        };
        
        var operations = new List<Operation<User>>
        {
            new(OperationType.Add.ToString(), "/books", null, books),
            new("add", "/UserNames", null, userNames),
            new("replace", "/age", null, 41)
        };

        var patchJson = JsonSerializer.Serialize(operations, options: new());
        using var content = new StringContent(patchJson, Encoding.UTF8, "application/json-patch+json");

        using var response = await httpClient.PatchAsync(uri, content).ConfigureAwait(false);
    }

    public static async Task<Book> GetBookAsync(HttpClient httpClient, Uri uri)
    {
        using var response = await httpClient.GetAsync(uri);

        return await response.Content.ReadFromJsonAsync<Book>();
    }

    public static async Task PostBookAsync(HttpClient httpClient, Uri uri)
    {
        var book = new Book { Title = "Willy Wonka and the chockolate factory" };
        
        var json = JsonSerializer.Serialize(book);
        
        using var response = await httpClient.PostAsJsonAsync(uri, book);
    }
    
    public static async Task PutUserNamesAsync(HttpClient httpClient, Uri uri)
    {
        var userNames = new List<UsersNames>()
        {
            new(new User() { Id = 3 }, "putname")
        };

        var json = JsonSerializer.Serialize(userNames);
        
        using var response = await httpClient.PutAsJsonAsync(uri, userNames).ConfigureAwait(false);
    }

    public static async Task PutUserRelationsAsync(HttpClient httpClient, Uri uri)
    {
        var relation = new UsersRelations
        {
            Origin = new User() { Id = 1 },
            Related = new User() { Id = 2 },
            Relation = Relation.Friend
        };

        var userRelations = new List<UsersRelations> { relation };

        var json = JsonSerializer.Serialize(userRelations);
        
        using var response = await httpClient.PutAsJsonAsync(uri, userRelations).ConfigureAwait(false);

    }
    
    public static async Task DirectDbAsync()
    {
        // Create db with default values
        Context.Initialize();

        // Serialize users json
        await using var jsonStream = await Context.SerializeUsersAsync();

        jsonStream.Position = 0;

        // Deserialize users
        // "User" only have ID
        var users = await Context.DeserializeAsync<IEnumerable<User>>(jsonStream);
    }
}
