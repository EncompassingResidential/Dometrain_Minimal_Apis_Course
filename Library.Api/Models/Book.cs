
namespace Library.Api.Models
{
    /*
     * {
     * 	"isbn": "978-0137081073",
     * 	"title": "The Clean Coder",
     * 	"author": "Robert C. Martin",
     * 	"shortDescription": "In The Clean Coder: A Code of Conduct for Professional Programmers, legendary software expert Robert C. Martin introduces the disciplines, techniques, tools, and practices of true software craftsmanship",
     * 	"pageCount": 242,
     * 	"releaseDate": "2011-03-13"
     * }
     */
    public class Book
    {
        public string Isbn { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public int    PageCount { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
