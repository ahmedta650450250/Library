using System;
using System.Collections.Generic;

namespace Library;

public class MemoryLogger : ILogger
{
    public List<string> Messages { get; } = new();
    public void Info(string m)  => Messages.Add($"INFO: {m}");
    public void Warn(string m)  => Messages.Add($"WARN: {m}");
    public void Error(string m) => Messages.Add($"ERROR: {m}");
}
