using System;

namespace Library;

public class ConsoleLogger : ILogger
{
  public void Info(string message)
  {
    Console.WriteLine($"[{DateTime.Now}] INFO: {message}");
  }
  public void Warn(string message)
  {
    Console.WriteLine($"[{DateTime.Now}] WARN: {message}");
  }
  public void Error(string message)
  {
    Console.WriteLine($"[{DateTime.Now}] ERROR: {message}");
  }
}
