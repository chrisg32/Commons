using BenchmarkDotNet.Attributes;
using CG.Commons.Util;
#pragma warning disable CS0612

namespace Commons.Benchmark.Util;

[MemoryDiagnoser(false)]
public class NaturalComparerLargeRandomStringBenchmarks
{
    private NaturalComparerObsolete _comparerObsolete = null!;
    private NaturalComparerObsolete _comparerObsoleteIgnoreCase = null!;
    private NaturalComparerObsolete _comparerObsoleteIgnoreWhitespace = null!;
    private NaturalComparerObsolete _comparerObsoleteIgnoreCaseWhitespace = null!;
    private NaturalComparer _comparer = null!;
    private NaturalComparer _comparerIgnoreCase = null!;
    private NaturalComparer _comparerIgnoreWhitespace = null!;
    private NaturalComparer _comparerIgnoreCaseWhitespace = null!;

    private string _left = null!;
    private string _right = null!;

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

        _left = GenerateString(600);
        _right = GenerateString(555);
    }

    private static string GenerateString(int i)
    {
        var random = new Random();
        var array = new char[i--];
        for (; i >= 0; i--)
        {
            array[i] = (char)random.Next(32, 122);
        }
        return new string(array);
    }
    
    
    [Benchmark]
    public void Compare() => _ = _comparerObsolete.Compare(_left , _right);

    [Benchmark]
    public void CompareIgnoreCase() => _ = _comparerObsoleteIgnoreCase.Compare(_left, _right);

    [Benchmark]
    public void CompareIgnoreWhiteSpace() => _ = _comparerObsoleteIgnoreWhitespace.Compare(_left, _right);

    [Benchmark]
    public void CompareIgnoreCaseWhiteSpace() => _ = _comparerObsoleteIgnoreCaseWhitespace.Compare(_left, _right);
    
    
    
    [Benchmark]
    public void Compare_Span() => _ = _comparer.Compare(_left , _right);

    [Benchmark]
    public void CompareIgnoreCase_Span() => _ = _comparerIgnoreCase.Compare(_left, _right);

    [Benchmark]
    public void CompareIgnoreWhiteSpace_Span() => _ = _comparerIgnoreWhitespace.Compare(_left, _right);

    [Benchmark]
    public void CompareIgnoreCaseWhiteSpace_Span() => _ = _comparerIgnoreCaseWhitespace.Compare(_left, _right);
}