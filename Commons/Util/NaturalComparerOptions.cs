using System;

namespace CG.Commons.Util
{
    [Flags]
    public enum NaturalComparerOptions
    {
        None,
        IgnoreCase,
        IgnoreWhiteSpace,
        CheckTrailingDecimalLength
    }
}