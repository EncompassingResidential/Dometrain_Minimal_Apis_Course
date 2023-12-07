// This was all added in C# version 9.0
// My current language version is 11.0 on 11/13/2023
// Using .NET 7

using Library.Api;
using Library.Api.Data;
using Library.Api.Models;
using Library.Api.Services;
using FluentValidation;
using FluentValidation.Results;

var builder = WebApplication.CreateBuilder(args);

// Service Registration starts here

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Could set up configuration in appsettings.json
builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new SqliteConnectionFactory(
        builder.Configuration.GetValue<string>("Database:ConnectionString")
        ?? throw new InvalidOperationException("Database:ConnectionString not found in configuration file appsettings.json")
        ));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<IBookService, BookService>();

// <Program> is good enough because the Dependency FluentValidation
// scans the entire Assembly / project and looks for "Validator" classes
// and automatically adds them if you add the top of the Assembly i.e. Program.cs
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Service Registration ENDs Here

var app = builder.Build();

// Middleware registration starts here and ends below right before app.Run()
// ORDER MATTERS in Middleware

app.UseSwagger();
// https://localhost:7100/swagger
app.UseSwaggerUI();


// https://localhost:7100/
// returns Hello World!
app.MapGet("/", () => "Hello World!");

/* POST
 * https://localhost:7100/books
 * with Body with raw JSON 
 * see data in 
 */
app.MapPost("books", async (Book book, IBookService bookService,
    IValidator<Book> validator) =>
{
    var validationResult = await validator.ValidateAsync(book);
    if (!validationResult.IsValid)
    {
        // Nick said you could have exposed / returns the validationResult object
        // but you need to decide if you want to expose that object / data.
        return Results.BadRequest(validationResult.Errors);

        // Nick said this is another way
        // return Results.ValidationProblem( Nick didn't say what would be in here );
    }

    var created = await bookService.CreateAsync(book);
    if (!created)
    {
        return Results.BadRequest(new List<ValidationFailure>
        {
            new ValidationFailure("Isbn", "A book with this ISBN-13 already exists")
        });
    }

    // Points to the URI Header of this response from where this item was created
    return Results.Created($"/books/{book.Isbn}", book);
});

/*
 * https://localhost:7100/books/
 * 
 * returns ALL Books  status 200 Ok
 * , so dangerous for a large database
 * 
 * Overloaded this MapGet Endpoint with , string? searchTerm
 * which makes this parameter optional (The ? is the character that makes it NULLable / optional)
 * URL - https://localhost:7100/books?searchTerm=testing
 * this returns all books with title of "testing" inside it.
 */
app.MapGet("books", async (IBookService bookService, string? searchTerm) =>
{
    if (searchTerm is not null && !string.IsNullOrWhiteSpace(searchTerm))
    {
        var matchedBooks = await bookService.SearchByTitleAsync(searchTerm);
        return Results.Ok(matchedBooks);
    }

    var books = await bookService.GetAllAsync();
    return Results.Ok(books);
});

/*
 * He said one option is to have Endpoint listed here as:
 * books/{isbn:regex()}
 * but then you have to have code on the service. 
 * 
 * You will have to have code on the Update, and then from the Request Body and the Route
 * 
 * the one consideration to have the extra here is if you have an API limitation:
 * then you could have it here.
 * 
 */
/*
 * https://localhost:7100/books/123-4567890123
 * returns 200 Ok
 * https://localhost:7100/books/123-456789
 * returns 404 Not Found
 */
app.MapGet("books/{isbn}", async (string isbn, IBookService bookService) =>
{
    var book = await bookService.GetByIsbnAsync(isbn);
    // i.e.              200 if it does exist  :  04 if it doesn't exit
    return book is not null ? Results.Ok(book) : Results.NotFound();
});

/* PUT  Modify / Update a book
 * https://localhost:7100/books/isbn-string
 * with Body with raw JSON 
 * see data in C:\repos\Dometrain\Minimal APIs\minimal-apis-course-final-master\3.LetsBuild\Library.Api\Data\Library_Books_examples.json
 */
app.MapPut("books/{isbn}", async (string isbn, Book book, IBookService bookService,
    IValidator<Book> validator) =>
{
    book.Isbn = isbn;
    var validationResult = await validator.ValidateAsync(book);
    if (!validationResult.IsValid)
    {
        // Nick said in the above Creation EndPoint (POST) that
        // you could have exposed / returns the validationResult object
        // but you need to decide if you want to expose that object / data.
        // not sure if that applies to here as well.
        return Results.BadRequest(validationResult.Errors);
    }

    var updated = await bookService.UpdateAsync(book);

    return updated ? Results.Ok(book) : Results.NotFound();
});



// DB Init Here

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

// Middleware registration ENDs here

app.Run();


// 11/13/23 
// Nick said that minimal APIs perform better, usually than Controllers
