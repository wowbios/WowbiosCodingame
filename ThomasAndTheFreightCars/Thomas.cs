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

        Lis? lisMax = null;
        int? ldsMaxLen = null;
        int max = 0;

        foreach (Lis lis in allLis)
        {
            int min = lis.Min;
            int[] elements = numbers.Where(e => e < min).ToArray();
            foreach (int lds in GetLds(elements, ref iterations).OrderByDescending(x => x))
            {
                iterations++;
                int resultMax = lis.Length + lds;
                if (resultMax < max)
                    continue;

                max = resultMax;
                lisMax = lis;
                ldsMaxLen = lds;
            }
        }

        _output("Iterations: " + iterations);
        _output("MAX: " + max);
        _output(string.Join(",", lisMax));
        _output(string.Join(",", ldsMaxLen));
        Max = max;
        return max;
    }

    readonly record struct Lis(int Min, int Length); // 1 2 4 5 => (1, 4)
    
    private static Lis[] GetLis(int[] numbers, ref int iterations)
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

        List<Lis> cutLis = new(); 
        foreach (int[] originalLis in allOriginalLis)
        {
            for (int i = 1; i < originalLis.Length; i++)
                cutLis.Add(new Lis(originalLis[i], originalLis.Length - i));
        }

        return allOriginalLis.Select(x=> new Lis(x[0], x.Length))
            .Union(cutLis)
            .ToArray();
    }

    private static int[] GetLds(int[] values, ref int iterations)
    {
        int[] counts = new int[values.Length];
        Array.Fill(counts, 1);

        for (int j = 1; j < values.Length; j++)
        {
            for (int i = 0; i < j; i++)
            {
                iterations++;
                if (values[i] > values[j] && counts[j] < counts[i] + 1)
                {
                    counts[j] = counts[i] + 1;
                }
            }
        }

        return counts;
    }
}