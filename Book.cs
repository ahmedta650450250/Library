using System;
using System.Linq;
using System.Collections.Generic;

namespace Library;

public class Book
{
    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        TotalCopies = 3;
        AvailableCopies = 3;
        ISBN = isbn;
    }

    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public void DecreaseAvailable() => AvailableCopies--;
    public void IncreaseAvailable() => AvailableCopies++;
}
