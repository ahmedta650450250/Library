using System.IO;
using System.Text.Json;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Library;

public class LibraryRepository : ILibraryRepository
{
    public List<Book> Books { get; set; } = new List<Book>();
    public List<User> Users { get; set; } = new List<User>();
    public List<Admin> Admins { get; set; } = new List<Admin>();
    public List<User> BlockedUsers { get; set; } = new List<User>();

    public void SaveToJson()
    {
        var data = new
        {
            Books = Books,
            Users = Users,
            Admins = Admins,
            BlockedUsers = BlockedUsers
        };
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("database.json", json);
    }
    public void LoadFromJson()
    {
        if (!File.Exists("database.json"))
            return;
        string json = File.ReadAllText("database.json");
        var obj = JsonSerializer.Deserialize<LibraryData>(json);

        Books = obj.Books ?? new List<Book>();
        Users = obj.Users ?? new List<User>();
        BlockedUsers = obj.BlockedUsers ?? new List<User>();
        Admins = obj.Admins ?? new List<Admin>();


    }
    public Book GetBookByIsbn(string isbn)
    {
        return Books.FirstOrDefault(x => isbn == x.ISBN);
    }

    public User GetLoggedUserByName(string name)
    {
        return Users.FirstOrDefault(u => u.Name == name);

    }
    private class LibraryData
    {
        public List<Book> Books { get; set; }
        public List<User> Users { get; set; }
        public List<Admin> Admins { get; set; }
        public List<User> BlockedUsers { get; set; }
    }
}


