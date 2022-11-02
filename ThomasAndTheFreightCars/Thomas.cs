namespace ThomasAndTheFreightCars;

public class Thomas
{
    private readonly Action<string> _output;
    public string Input { get; }

    public int Max { get; private set; }

    public Thomas(string input, Action<string> output)
    {
        _output = output;
        Input = input;
    }
    
    public int Solve()
    {
        int[] numbers = Input.Split(' ').Select(int.Parse).ToArray();
        int iterations = 0;
        var allLis = GetLis(numbers, ref iterations);

        int[] lisMax = null;
        int[] ldsMax = null;
        int max = 0;

        foreach (int[] lis in allLis)
        {
            int min = lis[0];
            int[] elements = numbers.Where(e => e < min).ToArray();
            foreach (int[] lds in GetLds(elements, ref iterations))
            {
                iterations++;
                int resultMax = lis.Length + lds.Length;
                if (resultMax < max)
                    continue;
                
                bool ok = true;
                int prev = min;
                foreach (int source in lds)
                {
                    if (prev < source)
                    {
                        ok = false;
                        break;
                    }

                    prev = source;
                }

                if (ok && max < resultMax)
                {
                    max = resultMax;
                    lisMax = lis;
                    ldsMax = lds;
                }
            }
        }

        _output("Iterations: " + iterations);
        _output("MAX: " + max);
        _output(string.Join(",", lisMax));
        _output(string.Join(",", ldsMax));
        Max = max;
        return max;
    }

    private static List<int[]> GetLis(int[] numbers, ref int iterations)
    {
        List<int[]> allOriginalLis = numbers.Select(x => new int[] { x }).ToList();
        for (int j = 1; j < numbers.Length; j++)
        {
            for (int i = 0; i < j; i++)
            {
                iterations++;
                if (numbers[i] < numbers[j] && allOriginalLis[j].Length < allOriginalLis[i].Length + 1)
                {
                    allOriginalLis[j] = new int[allOriginalLis[i].Length + 1];
                    allOriginalLis[i].CopyTo(allOriginalLis[j], 0);
                    allOriginalLis[j][allOriginalLis[j].Length - 1] = numbers[j];
                }
            }
        }

        List<int[]> cutLis = new(); 
        foreach (int[] originalLis in allOriginalLis)
        {
            for (int i = 1; i < originalLis.Length; i++)
            {
                cutLis.Add(originalLis[i..]);
            }
        }

        allOriginalLis.AddRange(cutLis);
        return allOriginalLis;
    }

    private static int[][] GetLds(int[] values, ref int iterations)
    {
        int[][] counts = values.Select(x => new[] { x }).ToArray();

        for (int j = 1; j < values.Length; j++)
        {
            for (int i = 0; i < j; i++)
            {
                iterations++;
                if (values[i] > values[j] && counts[j].Length < counts[i].Length + 1)
                {
                    counts[j] = counts[i].Append(values[j]).ToArray();
                }
            }
        }

        return counts;
    }
}