using System;
using System.Linq;
using System.Collections.Generic;

namespace Library;

public interface ILibraryRepository 
{       
    public List<Book> Books { get; set; } 
    public List<User> Users { get; set; }
    public List<Admin> Admins { get; set; }
    public List<User> BlockedUsers { get; set; }
    
    public void SaveToJson();
    public void LoadFromJson();
    
    public User GetLoggedUserByName(string name);
    public Book GetBookByIsbn(string isbn);
}
