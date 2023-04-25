# region 1

// using Microsoft.AspNetCore.Authorization;
 
// var builder = WebApplication.CreateBuilder();
 
// builder.Services.AddAuthentication("Bearer")  // добавление сервисов аутентификации
//     .AddJwtBearer();      // подключение аутентификации с помощью jwt-токенов
// builder.Services.AddAuthorization();            // добавление сервисов авторизации
 
// var app = builder.Build();
 
// app.UseAuthentication();   // добавление middleware аутентификации 
// app.UseAuthorization();   // добавление middleware авторизации 
 
// app.Map("/hello", [Authorize]() => "Hello World!");
// app.Map("/", () => "Home Page");
 
// app.Run();

#endregion

# region jwt

// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
 
// var builder = WebApplication.CreateBuilder();
 
// builder.Services.AddAuthorization();
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             // указывает, будет ли валидироваться издатель при валидации токена
//             ValidateIssuer = true,
//             // строка, представляющая издателя
//             ValidIssuer = AuthOptions.ISSUER,
//             // будет ли валидироваться потребитель токена
//             ValidateAudience = true,
//             // установка потребителя токена
//             ValidAudience = AuthOptions.AUDIENCE,
//             // будет ли валидироваться время существования
//             ValidateLifetime = true,
//             // установка ключа безопасности
//             IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//             // валидация ключа безопасности
//             ValidateIssuerSigningKey = true,
//          };
// });
// var app = builder.Build();
 
// app.UseAuthentication();
// app.UseAuthorization();
 
// app.Map("/login/{username}", (string username) => 
// {
//     var claims = new List<Claim> {new Claim(ClaimTypes.Name, username) };
//     // создаем JWT-токен
//     var jwt = new JwtSecurityToken(
//             issuer: AuthOptions.ISSUER,
//             audience: AuthOptions.AUDIENCE,
//             claims: claims,
//             expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
//             signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
//     return new JwtSecurityTokenHandler().WriteToken(jwt);
// });
 
// app.Map("/data", [Authorize] () => new { message= "Hello World!" });
 
// app.Run();
 
// public class AuthOptions
// {
//     public const string ISSUER = "MyAuthServer"; // издатель токена
//     public const string AUDIENCE = "MyAuthClient"; // потребитель токена
//     const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
//     public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
//         new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
// }

# endregion

#region jwt+js

// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
 
// // условная бд с пользователями
// var people = new List<Person>
//  {
//     new Person("tom@gmail.com", "12345"),
//     new Person("bob@gmail.com", "55555")
// };
 
// var builder = WebApplication.CreateBuilder();
 
// builder.Services.AddAuthorization();
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidIssuer = AuthOptions.ISSUER,
//             ValidateAudience = true,
//             ValidAudience = AuthOptions.AUDIENCE,
//             ValidateLifetime = true,
//             IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//             ValidateIssuerSigningKey = true
//          };
// });
// var app = builder.Build();
 
// app.UseDefaultFiles();
// app.UseStaticFiles();
 
// app.UseAuthentication();
// app.UseAuthorization();
 
// app.MapPost("/login", (Person loginData) => 
// {
//     // находим пользователя 
//     Person? person = people.FirstOrDefault(p => p.Email == loginData.Email && p.Password == loginData.Password);
//     // если пользователь не найден, отправляем статусный код 401
//     if(person is null) return Results.Unauthorized();
     
//     var claims = new List<Claim> {new Claim(ClaimTypes.Name, person.Email) };
//     // создаем JWT-токен
//     var jwt = new JwtSecurityToken(
//             issuer: AuthOptions.ISSUER,
//             audience: AuthOptions.AUDIENCE,
//             claims: claims,
//             expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
//             signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
//     var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
//     // формируем ответ
//     var response = new
//     {
//         access_token = encodedJwt,
//         username = person.Email
//     };
 
//     return Results.Json(response);
// });
// app.Map("/data", [Authorize] () => new { message= "Hello World!" });
 
// app.Run();
 
// public class AuthOptions
// {
//     public const string ISSUER = "MyAuthServer"; // издатель токена
//     public const string AUDIENCE = "MyAuthClient"; // потребитель токена
//     const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
//     public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
//         new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
// }
 
// record class Person(string Email, string Password);

#endregion

#region cookies

// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Authentication.Cookies;
// using System.Security.Claims;
// using Microsoft.AspNetCore.Authentication;
 
// var builder = WebApplication.CreateBuilder();
 
// // условная бд с пользователями
// var people = new List<Person>
// {
//     new Person("tom@gmail.com", "12345"),
//     new Person("bob@gmail.com", "55555")
// };
// // аутентификация с помощью куки
// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(options => options.LoginPath = "/login");
// builder.Services.AddAuthorization();
 
// var app = builder.Build();
 
// app.UseAuthentication();   // добавление middleware аутентификации 
// app.UseAuthorization();   // добавление middleware авторизации 
 
// app.MapGet("/login", async (HttpContext context) =>
// {
//     context.Response.ContentType = "text/html; charset=utf-8";
//     // html-форма для ввода логина/пароля
//     string loginForm = @"<!DOCTYPE html>
//     <html>
//     <head>
//         <meta charset='utf-8' />
//         <title>METANIT.COM</title>
//     </head>
//     <body>
//         <h2>Login Form</h2>
//         <form method='post'>
//             <p>
//                 <label>Email</label><br />
//                 <input name='email' />
//             </p>
//             <p>
//                 <label>Password</label><br />
//                 <input type='password' name='password' />
//             </p>
//             <input type='submit' value='Login' />
//         </form>
//     </body>
//     </html>";
//     await context.Response.WriteAsync(loginForm);
// });
 
// app.MapPost("/login", async (string? returnUrl, HttpContext context) =>
// {
//     // получаем из формы email и пароль
//     var form = context.Request.Form;
//     // если email и/или пароль не установлены, посылаем статусный код ошибки 400
//     if (!form.ContainsKey("email") || !form.ContainsKey("password"))
//         return Results.BadRequest("Email и/или пароль не установлены");
 
//     string email = form["email"];
//     string password = form["password"];
     
//     // находим пользователя 
//     Person? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
//     // если пользователь не найден, отправляем статусный код 401
//     if (person is null) return Results.Unauthorized();
 
//     var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email) };
//     // создаем объект ClaimsIdentity
//     ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
//     // установка аутентификационных куки
//     await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
//     return Results.Redirect(returnUrl??"/");
// });
 
// app.MapGet("/logout", async (HttpContext context) =>
// {
//     await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//     return Results.Redirect("/login");
// });
 
// app.Map("/", [Authorize]() => $"Hello World!");
 
// app.Run();
 
// record class Person(string Email, string Password);

#endregion

#region roles_auth

// using Microsoft.AspNetCore.Authentication.Cookies;
// using System.Security.Claims;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authorization;
 
// var adminRole = new Role("admin");
// var userRole  = new Role("user");
// var people = new List<Person> 
// { 
//     new Person("tom@gmail.com", "12345", adminRole),
//     new Person("bob@gmail.com", "55555", userRole),
// };
 
// var builder = WebApplication.CreateBuilder();
// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(options =>
//     {
//         options.LoginPath = "/login";
//         options.AccessDeniedPath = "/accessdenied";
//     });
// builder.Services.AddAuthorization();
 
// var app = builder.Build();
 
// app.UseAuthentication();
// app.UseAuthorization();   // добавление middleware авторизации 
 
// app.MapGet("/accessdenied", async (HttpContext context) =>
// {
//     context.Response.StatusCode = 403;
//     await context.Response.WriteAsync("Access Denied");
// });
// app.MapGet("/login", async (HttpContext context) =>
// {
//     context.Response.ContentType = "text/html; charset=utf-8";
//     // html-форма для ввода логина/пароля
//     string loginForm = @"<!DOCTYPE html>
//     <html>
//     <head>
//         <meta charset='utf-8' />
//         <title>METANIT.COM</title>
//     </head>
//     <body>
//         <h2>Login Form</h2>
//         <form method='post'>
//             <p>
//                 <label>Email</label><br />
//                 <input name='email' />
//             </p>
//             <p>
//                 <label>Password</label><br />
//                 <input type='password' name='password' />
//             </p>
//             <input type='submit' value='Login' />
//         </form>
//     </body>
//     </html>";
// await context.Response.WriteAsync(loginForm);
// });
 
// app.MapPost("/login", async (string? returnUrl, HttpContext context) =>
// {
//     // получаем из формы email и пароль
//     var form = context.Request.Form;
//     // если email и/или пароль не установлены, посылаем статусный код ошибки 400
//     if (!form.ContainsKey("email") || !form.ContainsKey("password"))
//         return Results.BadRequest("Email и/или пароль не установлены");
//     string email = form["email"];
//     string password = form["password"];
 
//     // находим пользователя 
//     Person? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
//     // если пользователь не найден, отправляем статусный код 401
//     if (person is null) return Results.Unauthorized();
//     var claims = new List<Claim>
//     {
//         new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
//         new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.Name)
//     };
//     var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
//     var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
//     await context.SignInAsync(claimsPrincipal);
//     return Results.Redirect(returnUrl ?? "/");
// });
// // доступ только для роли admin
// app.Map("/admin", [Authorize(Roles = "admin")]() => "Admin Panel");
 
// // доступ только для ролей admin и user
// app.Map("/", [Authorize(Roles = "admin, user")](HttpContext context) =>
// {
//     var login = context.User.FindFirst(ClaimsIdentity.DefaultNameClaimType);
//     var role = context.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType);  
//     return $"Name: {login?.Value}\nRole: {role?.Value}";
// });
// app.MapGet("/logout", async (HttpContext context) =>
// {
//     await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//     return "Данные удалены";
// });
 
// app.Run();

// class Person
// {
//     public string Email { get; set; }
//     public string Password { get; set; }
//     public Role Role { get; set; }
//     public Person(string email, string password, Role role)
//     {
//         Email = email;
//         Password = password;
//         Role = role;
//     }
// }
// class Role
// {
//     public string Name { get; set; }
//     public Role(string name) => Name = name;
// }

#endregion

#region claims_auth

// using Microsoft.AspNetCore.Authentication.Cookies;
// using System.Security.Claims;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authorization;
 
// var people = new List<Person> 
// { 
//     new Person("tom@gmail.com", "12345", "London", "Microsoft"),
//     new Person("bob@gmail.com", "55555", "Лондон", "Google"),
//     new Person("sam@gmail.com", "11111", "Berlin", "Microsoft")
// };
 
// var builder = WebApplication.CreateBuilder();
// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(options =>
//     {
//         options.LoginPath = "/login";
//         options.AccessDeniedPath = "/login";
//     });
// builder.Services.AddAuthorization(opts => {
 
//     opts.AddPolicy("OnlyForLondon", policy => {
//         policy.RequireClaim(ClaimTypes.Locality, "Лондон", "London");
//     });
//     opts.AddPolicy("OnlyForMicrosoft", policy => {
//         policy.RequireClaim("company", "Microsoft");
//     });
// });
 
// var app = builder.Build();
 
// app.UseAuthentication();
// app.UseAuthorization();
 
// app.MapGet("/login", async (HttpContext context) =>
// {
//     context.Response.ContentType = "text/html; charset=utf-8";
//     // html-форма для ввода логина/пароля
//     string loginForm = @"<!DOCTYPE html>
//     <html>
//     <head>
//         <meta charset='utf-8' />
//         <title>METANIT.COM</title>
//     </head>
//     <body>
//         <h2>Login Form</h2>
//         <form method='post'>
//             <p>
//                 <label>Email</label><br />
//                 <input name='email' />
//             </p>
//             <p>
//                 <label>Password</label><br />
//                 <input type='password' name='password' />
//             </p>
//             <input type='submit' value='Login' />
//         </form>
//     </body>
//     </html>";
// await context.Response.WriteAsync(loginForm);
// });
 
// app.MapPost("/login", async (string? returnUrl, HttpContext context) =>
// {
//     // получаем из формы email и пароль
//     var form = context.Request.Form;
//     // если email и/или пароль не установлены, посылаем статусный код ошибки 400
//     if (!form.ContainsKey("email") || !form.ContainsKey("password"))
//         return Results.BadRequest("Email и/или пароль не установлены");
//     string email = form["email"];
//     string password = form["password"];
 
//     // находим пользователя 
//     Person? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
//     // если пользователь не найден, отправляем статусный код 401
//     if (person is null) return Results.Unauthorized();
//     var claims = new List<Claim>
//     {
//         new Claim(ClaimTypes.Name, person.Email),
//         new Claim(ClaimTypes.Locality, person.City),
//         new Claim("company", person.Company)
//     };
//     var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
//     var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
//     await context.SignInAsync(claimsPrincipal);
//     return Results.Redirect(returnUrl ?? "/");
// });
// // доступ только для City = London
// app.Map("/london", [Authorize(Policy = "OnlyForLondon")]() => "You are living in London");
 
// // доступ только для Company = Microsoft
// app.Map("/microsoft", [Authorize(Policy = "OnlyForMicrosoft")]() => "You are working in Microsoft");
 
// app.Map("/", [Authorize](HttpContext context) =>
// {
//     var login = context.User.FindFirst(ClaimTypes.Name);
//     var city = context.User.FindFirst(ClaimTypes.Locality);
//     var company = context.User.FindFirst("company");
//     return $"Name: {login?.Value}\nCity: {city?.Value}\nCompany: {company?.Value}";
// });
// app.MapGet("/logout", async (HttpContext context) =>
// {
//     await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//     return "Данные удалены";
// });
 
// app.Run();

// record class Person(string Email, string Password, string City, string Company);

#endregion

#region auth_restrict

using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
 
var people = new List<Person> 
{ 
    new Person("tom@gmail.com", "12345", 1984),
    new Person("bob@gmail.com", "55555", 2006)
};
 
var builder = WebApplication.CreateBuilder();
// встраиваем сервис AgeHandler
builder.Services.AddTransient<IAuthorizationHandler, AgeHandler>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/login";
    });
builder.Services.AddAuthorization(opts => {
    // устанавливаем ограничение по возрасту
    opts.AddPolicy("AgeLimit", policy => policy.Requirements.Add(new AgeRequirement(18)));
});
 
var app = builder.Build();
 
app.UseAuthentication();
app.UseAuthorization();
 
app.MapGet("/login", async (HttpContext context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    // html-форма для ввода логина/пароля
    string loginForm = @"<!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8' />
        <title>METANIT.COM</title>
    </head>
    <body>
        <h2>Login Form</h2>
        <form method='post'>
            <p>
                <label>Email</label><br />
                <input name='email' />
            </p>
            <p>
                <label>Password</label><br />
                <input type='password' name='password' />
            </p>
            <input type='submit' value='Login' />
        </form>
    </body>
    </html>";
await context.Response.WriteAsync(loginForm);
});
 
app.MapPost("/login", async (string? returnUrl, HttpContext context) =>
{
    // получаем из формы email и пароль
    var form = context.Request.Form;
    // если email и/или пароль не установлены, посылаем статусный код ошибки 400
    if (!form.ContainsKey("email") || !form.ContainsKey("password"))
        return Results.BadRequest("Email и/или пароль не установлены");
    string email = form["email"];
    string password = form["password"];
 
    // находим пользователя 
    Person? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
    // если пользователь не найден, отправляем статусный код 401
    if (person is null) return Results.Unauthorized();
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, person.Email),
        new Claim(ClaimTypes.DateOfBirth, person.Year.ToString())
    };
    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
    await context.SignInAsync(claimsPrincipal);
    return Results.Redirect(returnUrl ?? "/");
});
// доступ только для тех, кто соответствует ограничению AgeLimit
app.Map("/age", [Authorize(Policy = "AgeLimit")]() => "Age Limit is passed");
 
app.Map("/", [Authorize](HttpContext context) =>
{
    var login = context.User.FindFirst(ClaimTypes.Name);
    var year = context.User.FindFirst(ClaimTypes.DateOfBirth);
    return $"Name: {login?.Value}\nYear: {year?.Value}";
});
app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return "Данные удалены";
});
 
app.Run();

record class Person(string Email, string Password, int Year);
 
class AgeRequirement : IAuthorizationRequirement
{
    protected internal int Age { get; set; }
    public AgeRequirement(int age) => Age = age;
}

class AgeHandler : AuthorizationHandler<AgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AgeRequirement requirement)
    {
        // получаем claim с типом ClaimTypes.DateOfBirth - год рождения
        var yearClaim = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth);
        if (yearClaim is not null)
        {
            // если claim года рождения хранит число
            if (int.TryParse(yearClaim.Value, out var year))
            {
                // и разница между текущим годом и годом рождения больше требуемого возраста
                if ((DateTime.Now.Year - year) >= requirement.Age)
                {
                    context.Succeed(requirement); // сигнализируем, что claim соответствует ограничению
                }
            }
        }
        return Task.CompletedTask;
    }
}

#endregion
