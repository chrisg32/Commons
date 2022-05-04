### NaturalComparer

#### Benchmark

|                           Method |       Mean |    Error |   StdDev | Allocated |
|--------------------------------- |-----------:|---------:|---------:|----------:|
|                          Compare |   222.8 ns |  4.30 ns |  5.12 ns |     360 B |
|                CompareIgnoreCase |   350.6 ns |  7.04 ns | 10.32 ns |     536 B |
|          CompareIgnoreWhiteSpace | 3,831.2 ns | 46.87 ns | 46.04 ns |   3,464 B |
|      CompareIgnoreCaseWhiteSpace | 3,982.3 ns | 37.84 ns | 31.59 ns |   3,640 B |
|                     Compare_Span |   101.3 ns |  1.82 ns |  1.70 ns |         - |
|           CompareIgnoreCase_Span |   137.6 ns |  2.49 ns |  2.33 ns |         - |
|     CompareIgnoreWhiteSpace_Span | 1,632.6 ns | 12.74 ns |  9.95 ns |     376 B |
| CompareIgnoreCaseWhiteSpace_Span | 1,756.9 ns | 29.59 ns | 27.68 ns |     376 B |

#### Large Random String

|                           Method |         Mean |      Error |     StdDev | Allocated |
|--------------------------------- |-------------:|-----------:|-----------:|----------:|
|                          Compare |    541.04 ns |   5.690 ns |   5.322 ns |   2,360 B |
|                CompareIgnoreCase |  1,917.42 ns |  35.455 ns |  34.822 ns |   4,720 B |
|          CompareIgnoreWhiteSpace |  9,676.14 ns | 190.178 ns | 177.892 ns |   7,328 B |
|      CompareIgnoreCaseWhiteSpace | 10,953.94 ns | 147.037 ns | 137.539 ns |  10,816 B |
|                     Compare_Span |     32.67 ns |   0.689 ns |   1.052 ns |         - |
|           CompareIgnoreCase_Span |     24.55 ns |   0.416 ns |   0.369 ns |         - |
|     CompareIgnoreWhiteSpace_Span |  3,440.01 ns |  66.125 ns |  73.498 ns |   4,704 B |
| CompareIgnoreCaseWhiteSpace_Span |  3,797.10 ns |  49.217 ns |  41.098 ns |   4,688 B |

#### Large Similar String

|                           Method |     Mean |     Error |    StdDev | Allocated |
|--------------------------------- |---------:|----------:|----------:|----------:|
|                          Compare | 4.988 us | 0.0638 us | 0.0597 us |   2,736 B |
|                CompareIgnoreCase | 5.421 us | 0.0992 us | 0.0928 us |   2,736 B |
|          CompareIgnoreWhiteSpace | 8.286 us | 0.1003 us | 0.0938 us |   4,712 B |
|      CompareIgnoreCaseWhiteSpace | 8.806 us | 0.1670 us | 0.1787 us |   4,712 B |
|                     Compare_Span | 2.336 us | 0.0234 us | 0.0183 us |         - |
|           CompareIgnoreCase_Span | 2.905 us | 0.0336 us | 0.0314 us |         - |
|     CompareIgnoreWhiteSpace_Span | 3.189 us | 0.0609 us | 0.0570 us |     928 B |
| CompareIgnoreCaseWhiteSpace_Span | 3.757 us | 0.0733 us | 0.0612 us |     928 B |

##### Test Machine
```
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.202
[Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
```