using BenchmarkDotNet.Attributes;
using CG.Commons.Util;
#pragma warning disable CS0612

namespace Commons.Benchmark.Util;

[MemoryDiagnoser(false)]
public class NaturalComparerBenchmarks
{
    private NaturalComparerObsolete _comparerObsolete = null!;
    private NaturalComparerObsolete _comparerObsoleteIgnoreCase = null!;
    private NaturalComparerObsolete _comparerObsoleteIgnoreWhitespace = null!;
    private NaturalComparerObsolete _comparerObsoleteIgnoreCaseWhitespace = null!;
    private NaturalComparer _comparer = null!;
    private NaturalComparer _comparerIgnoreCase = null!;
    private NaturalComparer _comparerIgnoreWhitespace = null!;
    private NaturalComparer _comparerIgnoreCaseWhitespace = null!;

    [GlobalSetup]
    public void Setup()
    {
        _comparerObsolete = new NaturalComparerObsolete();
        _comparerObsoleteIgnoreCase = new NaturalComparerObsolete(NaturalComparerOptions.IgnoreCase);
        _comparerObsoleteIgnoreWhitespace = new NaturalComparerObsolete(NaturalComparerOptions.IgnoreWhiteSpace);
        _comparerObsoleteIgnoreCaseWhitespace = new NaturalComparerObsolete(NaturalComparerOptions.IgnoreCase | NaturalComparerOptions.IgnoreWhiteSpace);
        
        _comparer = new NaturalComparer();
        _comparerIgnoreCase = new NaturalComparer(NaturalComparerOptions.IgnoreCase);
        _comparerIgnoreWhitespace = new NaturalComparer(NaturalComparerOptions.IgnoreWhiteSpace);
        _comparerIgnoreCaseWhitespace = new NaturalComparer(NaturalComparerOptions.IgnoreCase | NaturalComparerOptions.IgnoreWhiteSpace);
    }

    private const string Left = "  ThisIsA StringWithANumber00201.3   ";
    private const string Right = " ThisIsA  StringWithANumber00100.6     ";
    
    [Benchmark]
    public void Compare() => _ = _comparerObsolete.Compare(Left , Right);

    [Benchmark]
    public void CompareIgnoreCase() => _ = _comparerObsoleteIgnoreCase.Compare(Left, Right);

    [Benchmark]
    public void CompareIgnoreWhiteSpace() => _ = _comparerObsoleteIgnoreWhitespace.Compare(Left, Right);

    [Benchmark]
    public void CompareIgnoreCaseWhiteSpace() => _ = _comparerObsoleteIgnoreCaseWhitespace.Compare(Left, Right);
    
    
    
    [Benchmark]
    public void Compare_Span() => _ = _comparer.Compare(Left , Right);

    [Benchmark]
    public void CompareIgnoreCase_Span() => _ = _comparerIgnoreCase.Compare(Left, Right);

    [Benchmark]
    public void CompareIgnoreWhiteSpace_Span() => _ = _comparerIgnoreWhitespace.Compare(Left, Right);

    [Benchmark]
    public void CompareIgnoreCaseWhiteSpace_Span() => _ = _comparerIgnoreCaseWhitespace.Compare(Left, Right);
}