using System;
using System.Linq;
using System.Collections.Generic;

namespace Library;

public class BorrowingService
{
    private ILibraryRepository _repository;
    public BorrowingService(ILibraryRepository repository)
    {
        _repository = repository;
    }

    
    public BorrowResult Borrow(User user, Book book)
    {

        if (!IsLoggedIn(user))
            return new BorrowResult { Success = false, Message = "User is not logged in"};

        if (!IsAvailable(book))
            return new BorrowResult { Success = false, Message = "Book is not available"};

        if (IsBlocked(user))
            return new BorrowResult { Success = false, Message = "user is blocked"};

        if (HasOverdueBook(user))
            return new BorrowResult { Success = false, Message = "the user has overdue book"};



        int limit = user.Type == UserType.VIP ? 10 : 3;
        if (user.BorrowedBooks.Count >= limit)
        {
            
            return new BorrowResult { Success = false, Message = $"Limit reached for {user.Type} users ({limit})"};
        } 
        var b = _repository.Books.FirstOrDefault(x => x.ISBN == book.ISBN);
        if (b == null)
            return new BorrowResult { Success = false, Message = "Book is not found" };
        b.AvailableCopies--;
        
        var record = new BorrowRecord
        {
            Book = b,
            BorrowDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(14)
        };
        user.BorrowedBooks.Add(record);
        
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
            return new ReturnResult { Success = false, Message = "user is not logged in"};

        var u = _repository.GetLoggedUserByName(user.Name);
        var b = _repository.GetBookByIsbn(book.ISBN);

        if (b == null)
            return new ReturnResult { Success = false, Message = "there is no book like this"};

        var record = u.BorrowedBooks.FirstOrDefault(x => x.Book.ISBN == b.ISBN); 
        if (record == null)
        {    
            return new ReturnResult { Success = false, Message = "book is not borrowed by this user"};
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
        
        return new ReturnResult { Success = true, Message = "Book returnes successfully", Fine = fine };
        
    }

}
