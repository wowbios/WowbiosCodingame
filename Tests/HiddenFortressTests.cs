using System.Diagnostics;
using FluentAssertions;
using TheHiddenFortress;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class HiddenFortressTests
{
    private readonly ITestOutputHelper _outputHelper;

    public HiddenFortressTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    
    [Theory]
    [MemberData(nameof(TestData))]
    public void Test(int size, string field, bool[,] expected)
    {
        var fortress = new HiddenFortress(size, field);
        var sw = Stopwatch.StartNew();
        var result = fortress.Solve();
        _outputHelper.WriteLine(sw.Elapsed.ToString());
        result.Should().BeEquivalentTo(expected);
        _outputHelper.WriteLine($"ITERATIONS: {fortress.Iterations}");
        _outputHelper.WriteLine($"VALIDATION ITERATIONS: {fortress.ValidationIterations}");
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[]
            {
                3,
                @"212
322
201",
                new[,]
                {
                    { true, false, false },
                    { true, false, true },
                    { false, false, false }
                }
            };

            yield return new object[]
            {
                3,
                @"122
212
221",
                new[,]
                {
                    { true, false, false },
                    { false, true, false },
                    { false, false, true }
                }
            };

            yield return new object[]
            {
                5,
                @"65756
45655
43545
22423
56756",
                new [,]
                {
                    { false, true, true, true, true },
                    { true, false, true, false, true },
                    { false, true, true, false, false },
                    { false, false, false, false, false },
                    { true, false, true, true, true }
                }
            };

            yield return new object[]
            {
                11,
                @"696a8758574
6a6c9869694
686b8757584
57598647473
8b9dba8a7b7
8a8ca87a796
696a9658574
6a7ba768695
9c8dca7a7a7
575a8547473
ac9ecb8b9b8",
                new bool[11,11]
            };
        }
    }
}