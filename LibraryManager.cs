using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Library;
using System.Text.Json;

namespace Library;

public class LibraryManager
{
    private readonly ILibraryRepository _repository;
    private readonly BorrowingService _borrowingservice;
    
    public LibraryManager(ILibraryRepository repository)
    {
        _repository = repository;
        _borrowingservice = new BorrowingService(_repository);
    }
    public List<Book> Books => _repository.Books;
    public List<Admin> LoggedInAdmins => _repository.Admins;
    public List<User> LoggedInUsers => _repository.Users;
    public List<User> BlockedUsers => _repository.BlockedUsers;

    public void SaveToJson() => _repository.SaveToJson();
  
    public void LoadFromJson() => _repository.LoadFromJson();
    
    public void Login(Admin admin)
    {
        LoggedInAdmins.Add(admin);

        SaveToJson();
    }
    public void Login(User user)
    {
        LoggedInUsers.Add(user);

        SaveToJson();
    }

    private bool IsLoggedIn(Admin admin)
    {
        Admin a = LoggedInAdmins.FirstOrDefault(x => x.Name == admin.Name);

        if (a == null)
        {
            
            return false;
        }
        return true;
    }

    

    public void AddBook(Admin admin, Book book)
    {
        if (!IsLoggedIn(admin))
        {
            return;
        }
        Books.Add(book);
        _repository.SaveToJson();
    }
    
    
    public BorrowResult Borrow(User user, Book book)
    {
        return _borrowingservice.Borrow(user, book);
    }
    public ReturnResult ReturnBook(User user, Book book)
    {
        return _borrowingservice.ReturnBook(user, book);
    }
    
    


    
    



    
}
