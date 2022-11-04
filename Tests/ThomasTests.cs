using System.Diagnostics;
using FluentAssertions;
using FluentAssertions.Extensions;
using ThomasAndTheFreightCars;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class ThomasTests
{
    private readonly ITestOutputHelper _outputHelper;

    public ThomasTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Theory]
    [InlineData(5, "4 5 1 3 2", 4)]
    [InlineData(10, "5 9 1 6 8 7 3 10 4 2", 6)]
    [InlineData(20, "64 3 38 6 39 7 73 9 41 14 48 81 17 19 52 55 26 28 61 94", 11)]
    [InlineData(40, "57 47 50 4 26 1 60 44 30 54 19 21 52 32 24 15 45 8 16 22 55 46 12 39 36 25 9 28 11 7 23 51 35 40 29 37 58 53 34 18", 12)]
    [InlineData(60, "91 97 110 169 71 122 167 66 158 106 173 127 49 148 48 6 175 8 82 103 154 117 165 62 123 170 140 1 89 70 128 9 34 94 24 15 116 13 200 147 139 188 69 33 190 153 95 108 174 12 22 41 37 67 168 92 130 98 52 198", 21)]
    [InlineData(100, "161 226 182 80 39 178 60 48 141 51 211 195 78 81 105 70 127 124 113 24 171 50 85 15 228 154 149 112 69 23 218 82 207 53 5 117 223 68 17 144 57 20 147 237 96 125 25 213 235 54 221 72 139 194 2 6 142 107 162 153 187 37 64 3 240 201 191 155 104 119 210 209 71 27 109 42 18 116 30 122 34 172 35 238 43 93 102 92 83 225 77 204 21 36 248 157 67 106 52 189", 23)]
    public void Test(int n, string input, int expected)
    {
        var thomas = new Thomas(input, _outputHelper.WriteLine);
        var w = Stopwatch.StartNew();
        thomas.ExecutionTimeOf(t => t.Solve())
            .Should().BeLessThan(5.Seconds());
        _outputHelper.WriteLine(w.Elapsed.ToString());
        thomas.Max.Should().Be(expected);
    }
}