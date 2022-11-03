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
        int max = 0;
        foreach (int number in numbers)
        {
            int maxLis = GetMaxLis(numbers.Where(x => x >= number).ToArray());
            int maxLds = GetMaxLis(numbers.Reverse().Where(x => x < number).ToArray());
            max = Math.Max(maxLis + maxLds, max);
        }

        Max = max;
        return Max;
    }

    private int GetMaxLis(int[] numbers)
    {
        if (numbers.Length == 0)
            return 0;

        int max = 1;
        int[] liss = new int[numbers.Length];
        Array.Fill(liss, 1);
        for (int j = 1; j < numbers.Length; j++)
        {
            for (int i = 0; i < j; i++)
            {
                if (numbers[i] < numbers[j] && liss[j] < liss[i] + 1)
                {
                    liss[j] = liss[i] + 1;
                    max = Math.Max(liss[j], max);
                }
            }
        }

        return max;
    }
}
