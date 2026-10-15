using System;
using System.Linq;
using System.Collections.Generic;

namespace Library;

public class ReturnResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public double Fine { get; set; }
}
