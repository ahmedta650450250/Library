using System;
using System.Linq;
using System.Collections.Generic;

namespace Library;

public class FakeLibraryRepository : ILibraryRepository
{
    public List<Book> Books { get; set; } = new List<Book>();
    public List<User> Users { get; set; } = new List<User>();
    public List<Admin> Admins { get; set; } = new List<Admin>();
    public List<User> BlockedUsers { get; set; } = new List<User>();

    public int SaveCount { get; private set; } = 0;

    public void SaveToJson() { SaveCount++; }   // لا يلمس القرص
    public void LoadFromJson() { /* لا شيء */ }

    public User GetLoggedUserByName(string name) 
        => Users.FirstOrDefault(u => u.Name == name);
    public Book GetBookByIsbn(string isbn) 
        => Books.FirstOrDefault(b => b.ISBN == isbn);
}
