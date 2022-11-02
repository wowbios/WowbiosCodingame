namespace ThomasAndTheFreightCars;

public class Thomas
{
    public string Input { get; }

    public int Max { get; private set; }

    public Thomas(string input)
    {
        Input = input;
    }
    
    public int Solve()
    {
        int[] numbers = Input.Split(' ').Select(int.Parse).ToArray();
        int[][] allLis = GetLis(numbers);

        int[] lisMax = null;
        int[] ldsMax = null;
        int max = 0;
        foreach (int[] lis in allLis)
        {
            int[] elements = numbers.Where(e => e < lis[0]).ToArray();
            foreach (int[] lds in GetLds(elements))
            {
                bool ok = true;
                int prev = -1;
                bool first = true;
                foreach (var source in lis.Reverse().Union(lds))
                {
                    if (first)
                    {
                        prev = source;
                        first = false;
                        continue;
                    }

                    if (prev < source)
                    {
                        ok = false;
                        break;
                    }

                    prev = source;
                }

                int resultMax = lis.Length + lds.Length;
                if (ok && max < resultMax)
                {
                    Console.WriteLine($"ANALYZE LIS:{(string.Join(",", lis))} LDS:{(string.Join(",", lds))}");
                    max = resultMax;
                     lisMax = lis;
                     ldsMax = lds;
                }
            }
        }
        Console.WriteLine("MAX: " + max);
        Console.WriteLine(string.Join(",",lisMax));
        Console.WriteLine(string.Join(",",ldsMax));
        Max = max;
        return max;
    }

    private static int[][] GetLis(int[] numbers)
    {
        int[][] counts = numbers.Select(x => new int[] { x }).ToArray();
        for (int j = 1; j < numbers.Length; j++)
        {
            for (int i = 0; i < j; i++)
            {
                if (numbers[i] < numbers[j] && counts[j].Length < counts[i].Length + 1)
                {
                    counts[j] = new int[counts[i].Length + 1];
                    counts[i].CopyTo(counts[j], 0);
                    counts[j][counts[j].Length - 1] = numbers[j];
                }
            }
        }

        return counts;
    }

    private static int[][] GetLds(int[] values)
    {
        int[][] counts = values.Select(x => new[] { x }).ToArray();

        for (int j = 1; j < values.Length; j++)
        {
            for (int i = 0; i < j; i++)
            {
                if (values[i] > values[j] && counts[j].Length < counts[i].Length + 1)
                {
                    counts[j] = counts[i].Append(values[j]).ToArray();
                }
            }
        }

        return counts;
    }
}