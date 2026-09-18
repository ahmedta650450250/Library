namespace Library;

class Program
{
    static void Main(string[] args)
    {
        var repo = new FakeLibraryRepository();
        var manager = new LibraryManager (repo);
        repo.Books.Add(new Book ( "DoNotQuit", "ahmed","787436"));
        repo.Books.Add(new Book ( "CleanCode", "David","59908"));
        
        var b = repo.GetBookByIsbn("787436");
        
        var u = new User ("jawad", UserType.Regular);
        manager.Login(u);
        var result = manager.Borrow(u, b);
        if(result.Success == true)
        Console.WriteLine($"{result.Message}");
        else Console.WriteLine($"{result.Message}");
        
        // this was a successful borrowing
        //Next step: Making the user has over due book, then if he allowed to borrow another one
        
        var record = u.BorrowedBooks.FirstOrDefault(x => x.Book.ISBN == "787436");
        record.BorrowDate = DateTime.Now.AddDays(-20);
        record.DueDate = DateTime.Now.AddDays(-15);
        
        var result2 = manager.Borrow(u, b);
        if(result2.Success == true)
        Console.WriteLine($"Second Borrow Result: {result2.Message}");
        else Console.WriteLine($" Second Borrow Result {result2.Message}");
    }
}
