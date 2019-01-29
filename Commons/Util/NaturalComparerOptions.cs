using System;

namespace CG.Commons.Util
{
    [Flags]
    public enum NaturalComparerOptions
    {
        None = 0x00000,
        IgnoreCase = 0x00001,
        IgnoreWhiteSpace = 0x00010,
        CheckTrailingDecimalLength = 0x10100,
        LowercaseFirst = 0x01000,
        DecimalPrecision = 0x10000,
    }
}