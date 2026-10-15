using System;

namespace Library;

public class LibrarySettings
{
    public double FinePerDay { get; set; } = 1.0;
    public int LoanDays { get; set; } = 14;
    public int RegularLimit { get; set; } = 3;
    public int VipLimit { get; set; } = 10;
    public int BorrowLimitPerDay { get; set; } = 5;

    public void Validate()
    {
        if (FinePerDay < 0)
            throw new InvalidOperationException("FinePerDay must be >= 0");
        if (LoanDays <= 0)
            throw new InvalidOperationException("LoanDays must be > 0");
        if (RegularLimit <= 0 || VipLimit <= 0)
            throw new InvalidOperationException("Limits must be > 0");
        if (VipLimit < RegularLimit)
            throw new InvalidOperationException("VipLimit must be >= RegularLimit");
    }
}
