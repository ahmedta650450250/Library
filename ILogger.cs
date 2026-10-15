using System;

namespace Library;

public interface ILogger
{
    public void Info (string Message);
    public void Warn (string Message);
    public void Error (string Message);
    
}
