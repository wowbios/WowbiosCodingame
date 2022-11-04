using FluentAssertions;
using Xunit;

namespace Tests;

public class ObsoleteProgrammingTests
{
    [Theory]
    [MemberData(nameof(TestParameters))]
    public void Test(string[] lines, int[] expected)
    {
        List<int> result = new();
        Action<int> output = result.Add;

        var machine = new StateMachine(output);
        foreach (string line in lines)
        {
            foreach (string operation in line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                machine.ProcessOperation(operation);
        }

        result.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> TestParameters
    {
        get
        {
            yield return new object[]
            {
                new []
                {
                    "10 5 ADD OUT",
                    "10 5 SUB OUT",
                    "12 24 SUB OUT",
                    "30 10 MUL OUT",
                    "50 7 DIV OUT",
                    "50 7 MOD OUT"
                },
                new []
                {
                    15,
                    5,
                    -12,
                    300,
                    7,
                    1
                }
            };
            yield return new object[]
            {
                new []
                {
                    "1 5 4 IF ADD FI OUT"
                },
                new []
                {
                    6
                }
            };
            yield return new object[]
            {
                new[]
                {
                    "DEF IIF IF DUP IF DUP ELS 5 FI FI OUT END",
                    "4 1 IIF"
                },
                new[]
                {
                    4
                }
            };

            yield return new object[]
            {
                new []
                {
                    "DEF ABS DUP POS NOT IF 0 SWP SUB FI END",
                    "51 ABS OUT -5 ABS OUT 0 ABS OUT",
                    "DEF NZ",
                        "OVR ABS OVR ABS SUB",
                        "DUP NOT",
                        "IF POP DUP POS IF SWP FI",
                            "ELS",
                            "POS IF SWP FI",
                        "FI",
                        "POP",
                    "END",
                    "1 -2 NZ -8 NZ 4 NZ 5 NZ OUT",
                    "-12 -5 NZ -137 NZ OUT",
                    "42 -5 NZ 12 NZ 21 NZ 5 NZ 24 NZ OUT",
                    "42 5 NZ 12 NZ 21 NZ -5 NZ 24 NZ OUT",
                    "-5 -4 NZ -2 12 NZ NZ -40 4 NZ 2 18 NZ NZ NZ",
                    "11 5 NZ NZ OUT"
                },
                new []
                {
                    51,
                    5,
                    0,
                    5,
                    -137,
                    24,
                    24,
                    5
                }
            };
        }
    }
}