using System;
using System.Linq;
using System.Collections.Generic;

namespace Library;

public record BorrowRecord
{
    public Book Book { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
}
