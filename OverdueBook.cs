using System;
using System.Linq;
using System.Collections.Generic;

namespace Library;

public class OverdueBook
{
    public Book book { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public DateTime BorrowDate { get; set; } = DateTime.Now;
    public bool IsOverdue => DateTime.Now > DueDate && book != null;

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public bool ValidateLogin(string username, string password) => Username == username && Password == password;

}
