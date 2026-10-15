using System;
using System.Linq;
using System.Collections.Generic;

namespace Library;

public class User
{
    public User(string name, UserType type)
    {
        Name = name;
        Type = type;
    }
    public string Id { get; set; } // new
    public string Name { get; set; }
    public UserType Type { get; set; }
    public List<BorrowRecord> BorrowedBooks { get; set; } = new List<BorrowRecord>(); // <OverdueBook> new



    public bool IsVIP { get; set; } // new
    public int MaxBorrowLimit => IsVIP ? 10 : 3;
    //  public bool HasOverdueBook() => BorrowedBooks.Any(b => b.IsOverdue);




}

