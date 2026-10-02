using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Library;

public class BorrowingService
{
    private ILibraryRepository _repository;
    private ILogger _logger;
    public BorrowingService(ILibraryRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }




        

    public BorrowResult FailBorrow(string logMessage, string userMessage)
    {
       _logger.Warn(logMessage);
       return new BorrowResult { Success = false, Message = userMessage};
    }
    public ReturnResult FailReturn(string logMessage, string userMessage)
    {
       _logger.Warn(logMessage);
       return new ReturnResult { Success = false, Message = userMessage};
    }
    public BorrowResult Borrow(User user, Book book)
    {


      

        if (!IsLoggedIn(user))
        { 
            return FailBorrow($"User {user.Name} is not logged in", "User is not logged in");
        } // i need to complate replace below in FailBorrow()

        var b = _repository.Books.FirstOrDefault(x => x.ISBN == book.ISBN);
        if (b == null)
        { 
            return FailBorrow($"book {book.Title} is not found", "Book is not found");
      
        }  

        if (IsBlocked(user))
        { 
            return FailBorrow($"User {user.Name} is blocked", "User is blocked");
        
        }
        if (HasOverdueBook(user))
        { 
            return FailBorrow($"User {user.Name} has overdue book", "the user has overdue book");
        }
      
        if (b.AvailableCopies <= 0)
        { 
            return FailBorrow($"book {book.Title} book is not available", "Book is not available");
            
        }
        


        

        
        
        


      



        int limit = user.Type == UserType.VIP ? 10 : 3;
        if (user.BorrowedBooks.Count >= limit)
        {
            return FailBorrow($"limit reached for User {user.Name}", $"Limit reached for {user.Type} users ({limit})");
        
        } 
        
        
        b.AvailableCopies--;
        
        var record = new BorrowRecord
        {
            Book = b,
            BorrowDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(14)
        };

        user.BorrowedBooks.Add(record);

        _logger.Info($"User {user.Name} borrowed book {b.Title}");
        return new BorrowResult { Success = true, Message = "Borrowed Successfully"};
        
    }

    private bool IsLoggedIn(User user)
    {
        var u = _repository.GetLoggedUserByName(user.Name);
        return u != null; 
    }
    private bool IsAvailable(Book book)
    {
        Book b = _repository.Books.FirstOrDefault(x => x.ISBN == book.ISBN);
        if (b == null)
        {
            //Console.WriteLine($"{book.Title} is not found");
            return false;
        }
        if (b.AvailableCopies <= 0)
        {
            //Console.WriteLine($"{book.Title} is out of stock");
            return false;
        }
        return true;
    }
    public bool IsBlocked(User user)
    {
        if (_repository.BlockedUsers.Contains(user))
        {
            // Console.WriteLine($"You are blocked");
            return true;
        }
        return false;
    }
    public bool IsBookBorrowedByUser(Book book, User user)
    {
        var b = user.BorrowedBooks.FirstOrDefault(x => x.Book.ISBN == book.ISBN);
        if (b == null)
        {
           // Console.WriteLine($"{user.Name} is not borrowed this book");
           return false;
        }
        return true;
    }

    public bool HasOverdueBook(User user)
    {
        foreach (var record in user.BorrowedBooks)
        {
            if (record.DueDate < DateTime.Now)
            {
                //Console.WriteLine($"User {user.Name} Has overdue book: {record.Book.Title}");
                return true;
            }
        }
        return false;
    }


    public double CalculateFine(DateTime dueDate, DateTime returnDate)
    {
        double fine = 0;
        if (dueDate < returnDate)
        {
            TimeSpan late = returnDate - dueDate;
            fine = late.TotalDays * 1.0;
        }
        return fine;
        
    }
    public ReturnResult ReturnBook(User user, Book book)
    {
        if (!IsLoggedIn(user))
        { 
            return FailReturn($"user is not logged in", "user is not logged in");
        }
        var u = _repository.GetLoggedUserByName(user.Name);
        var b = _repository.GetBookByIsbn(book.ISBN);

        if (b == null)
        { 
            return FailReturn($"There is no book like this", "there is no book like this");
        }
        var record = u.BorrowedBooks.FirstOrDefault(x => x.Book.ISBN == b.ISBN); 
        if (record == null)
        {    
            return FailReturn($"User has not borrowed this book b.Name", "book is not borrowed by this user");
        }
        
        u.BorrowedBooks.Remove(record);    
        b.AvailableCopies++;
        
        var returnDate = DateTime.Now;
        
        var fine = CalculateFine(record.DueDate, returnDate);
        
        /*
        Console.WriteLine("--- Return Receipt ---");
        Console.WriteLine($"User: {u.Name}");
        Console.WriteLine($"Book: {b.Title}");
        Console.WriteLine($"Borrow Date: {record.BorrowDate}");
        Console.WriteLine($"Due Date: {record.DueDate}");
        Console.WriteLine($"Return Date: {returnDate}");
        Console.WriteLine($"Fine: ${fine:F2}");
        Console.WriteLine("----------------------");
        */
        _logger.Info($"{u.Name} returned {b.Title}, Fine: {fine:F2}");
        return new ReturnResult { Success = true, Message = "Book returnes successfully", Fine = fine };
        
    }

}
