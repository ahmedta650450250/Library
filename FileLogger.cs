using System;
using System.IO;

namespace Library;

public class FileLogger : ILogger
{ 
  private readonly string _filePath;
  public FileLogger(string path)
  {
    _filePath = path;
  }

  public void Write(string level, string message)
  {
    File.AppendAllText(_filePath, $"[{DateTime.Now}] {level}: {message}\n");
  }
  
  public void Info(string message) => Write("INFO", message);
  public void Warn(string message) => Write("WARN", message);
  public void Error(string message) => Write("ERROR", message);
}
