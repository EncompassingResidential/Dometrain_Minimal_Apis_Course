// JavaScript source code

//ISBN-13 Regex for validation
^ (?= (?: \D *\d) { 10 } (?: (?: \D *\d) { 3 })?$)[\d -] + $

// Search book endpoint GET
"/books?searchTerm=lean"

// Get book by ISBN endpoint GET
"/books/978-0137081073"

// Get all books endpoint GET
"/books"

// Create book endpoint POST
"/books"

// Create book request body POST
{
	"isbn": "978-0137081073",
	"title": "The Clean Coder",
	"author": "Robert C. Martin",
	"shortDescription": "In The Clean Coder: A Code of Conduct for Professional Programmers, legendary software expert Robert C. Martin introduces the disciplines, techniques, tools, and practices of true software craftsmanship",
	"pageCount": 242,
	"releaseDate": "2011-03-13"
}

// Update book endpoint PUT
"/books/978-0137081073"

// Update book request body PUT
{
	"title": "The Clean Code",
	"author": "Robert C. Martin",
	"shortDescription": "In The Clean Coder: A Code of Conduct for Professional Programmers, legendary software expert Robert C. Martin introduces the disciplines, techniques, tools, and practices of true software craftsmanship",
	"pageCount": 242,
	"releaseDate": "2011-03-13"
}

// Delete book endpoint
"/books/978-0137081073"

// Book.cs
public class Book {
	public string Isbn { get; set; } = default !;

    public string Title { get; set; } = default !;

    public string Author { get; set; } = default !;

    public string ShortDescription { get; set; } = default !;

    public int PageCount { get; set; }

    public DateTime ReleaseDate { get; set; }
}