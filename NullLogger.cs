using System;

namespace Library;

public class NullLogger : ILogger
{
  public void Info(string message){}
  public void Warn(string message){}
  public void Error(string message){}
}
