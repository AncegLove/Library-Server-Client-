namespace Library__Web_
{
    public class Book
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string[] Genres { get; set; }
        public bool IsAviable { get; set; }

        public Book(int id, string title, string author, int year, string[] genres, bool isAviable)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            Genres = genres;
            IsAviable = isAviable;
        }
    }
}
