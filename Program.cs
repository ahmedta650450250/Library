namespace Library;
using System;
using System.Text.Json;
using System.IO;
using System.Linq;

class Program
{   
    static void test1()
    {
        var repo = new FakeLibraryRepository();
        var logger = new ConsoleLogger();

        LibrarySettings settings;
     /*   if (File.Exists("settings.json"))
    {
        settings = new SettingsProvider("settings.json").Load();
    }
    else
    {
        settings = new LibrarySettings();
    } */
        settings = new LibrarySettings();
        var manager = new LibraryManager(repo, logger, settings);

        var u = new User ("jawad", UserType.VIP);
        manager.Login(u);

        var b1 = new Book("Clean Code", "David", "379");
        var b2 = new Book("Wolves", "franc", "840");
        var b3 = new Book("woods", "stan", "830");
        var b4 = new Book("parties", "zebb", "820");
        var b5 = new Book("rap", "henry", "810");
        var b6 = new Book("rich man", "jo", "900");

        var r1 = new BorrowRecord { Book = b1, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(2)};
        var r2 = new BorrowRecord { Book = b2, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(2)};
        var r3 = new BorrowRecord { Book = b3, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(2)};
        var r4 = new BorrowRecord { Book = b4, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(2)};
        var r5 = new BorrowRecord { Book = b5, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(2)};
        var r6 = new BorrowRecord { Book = b6, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(2)};

        repo.Books.AddRange( new[] { b1, b2, b3, b4, b5, b6 } );
      
        u.BorrowedBooks.AddRange( new[] {r1, r2, r3});
      
        Console.WriteLine("Borrowed Books: ");
        foreach(var e in u.BorrowedBooks)
        { 
          
          Console.WriteLine(e.Book.Title);
        }

        manager.Borrow(u, b4);

        Console.WriteLine("Borrowed Books: ");
        foreach(var e in u.BorrowedBooks)
        { 
          
          Console.WriteLine(e.Book.Title);
        }
      
        manager.Borrow(u, b5);

        Console.WriteLine("Borrowed Books: ");
        foreach(var e in u.BorrowedBooks)
        { 
          
          Console.WriteLine(e.Book.Title);
        }

        manager.Borrow(u, b6);

        Console.WriteLine("Borrowed Books: ");
        foreach(var e in u.BorrowedBooks)
        { 
          
          Console.WriteLine(e.Book.Title);
        }
    }




    static void test2()
    {
      
    }

    
    


  
    static void Main(string[] args)
    { 

     
     
    test1();
  
    }
        
}
