using BenchmarkDotNet.Attributes;
using CG.Commons.Util;

namespace Commons.Benchmark.Util;

[MemoryDiagnoser(false)]
public class NaturalComparerBenchmarks
{
    private NaturalComparer _comparer = null!;
    private NaturalComparer _comparerIgnoreCase = null!;
    private NaturalComparer _comparerIgnoreWhitespace = null!;
    private NaturalComparer _comparerIgnoreCaseWhitespace = null!;
    private NaturalComparerSpan _comparer_span = null!;
    private NaturalComparerSpan _comparerIgnoreCase_span = null!;
    private NaturalComparerSpan _comparerIgnoreWhitespace_span = null!;
    private NaturalComparerSpan _comparerIgnoreCaseWhitespace_span = null!;
    
    [GlobalSetup]
    public void Setup()
    {
        _comparer = new NaturalComparer();
        _comparerIgnoreCase = new NaturalComparer(NaturalComparerOptions.IgnoreCase);
        _comparerIgnoreWhitespace = new NaturalComparer(NaturalComparerOptions.IgnoreWhiteSpace);
        _comparerIgnoreCaseWhitespace = new NaturalComparer(NaturalComparerOptions.IgnoreCase | NaturalComparerOptions.IgnoreWhiteSpace);
        
        _comparer_span = new NaturalComparerSpan();
        _comparerIgnoreCase_span = new NaturalComparerSpan(NaturalComparerOptions.IgnoreCase);
        _comparerIgnoreWhitespace_span = new NaturalComparerSpan(NaturalComparerOptions.IgnoreWhiteSpace);
        _comparerIgnoreCaseWhitespace_span = new NaturalComparerSpan(NaturalComparerOptions.IgnoreCase | NaturalComparerOptions.IgnoreWhiteSpace);
    }

    private const string Left = "  ThisIsA StringWithANumber00201.3   ";
    private const string Right = " ThisIsA  StringWithANumber00100.6     ";
    
    [Benchmark]
    public void Compare() => _ = _comparer.Compare(Left , Right);

    [Benchmark]
    public void CompareIgnoreCase() => _ = _comparerIgnoreCase.Compare(Left, Right);

    [Benchmark]
    public void CompareIgnoreWhiteSpace() => _ = _comparerIgnoreWhitespace.Compare(Left, Right);

    [Benchmark]
    public void CompareIgnoreCaseWhiteSpace() => _ = _comparerIgnoreCaseWhitespace.Compare(Left, Right);

    
    
    
    [Benchmark]
    public void Compare_Span() => _ = _comparer_span.Compare(Left , Right);

    [Benchmark]
    public void CompareIgnoreCase_Span() => _ = _comparerIgnoreCase_span.Compare(Left, Right);

    [Benchmark]
    public void CompareIgnoreWhiteSpace_Span() => _ = _comparerIgnoreWhitespace_span.Compare(Left, Right);

    [Benchmark]
    public void CompareIgnoreCaseWhiteSpace_Span() => _ = _comparerIgnoreCaseWhitespace_span.Compare(Left, Right);
}