namespace Library;
using System;
using System.IO;
using System.Linq;

class Program
{   
    static void test1()
    {
        var repo = new FakeLibraryRepository();
        var logger = new ConsoleLogger();
        
        var manager = new LibraryManager(repo, logger);

        var u = new User ("jawad", UserType.Regular);

        manager.Login(u);
      
        if (repo.Users.Any(u => u.Name == "jawad"))
          Console.WriteLine("Test 1 PASS");
        else
          Console.WriteLine("Test 1 FAIL");
    }

    static void test2()
    {
        var repo = new FakeLibraryRepository();
        var logger = new ConsoleLogger();
        
        var manager = new LibraryManager(repo, logger);

        var u = new User ("jawad", UserType.Regular);

        var b = new Book ( "DoNotQuit", "ahmed","787436");
        
        repo.Books.Add(b);

        var r = manager.Borrow(u, b);
      
        
         // Console.WriteLine(r.Message);
        
          
    }

    


  
    static void Main(string[] args)
    {

        
    var repo = new FakeLibraryRepository();
    var logger = new MemoryLogger();
    var manager = new LibraryManager(repo, logger);  
    var u = new User("fayez", UserType.Regular);  
    var b = new Book("david wilson", "woods story", "555");
    repo.Books.Add(b);
      
      
    manager.Login(u);
    manager.Borrow(u,b);
    manager.ReturnBook(u, b);


  //  foreach (var e in logger.Messages) Console.WriteLine(e);
      
    // if(File.Exists("log.txt"))    
     //  Console.WriteLine(File.ReadAllText("log.txt"));


  
    }
        
}
