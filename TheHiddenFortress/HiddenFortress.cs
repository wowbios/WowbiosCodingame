using System.Diagnostics;

namespace TheHiddenFortress;

public class HiddenFortress
{
    public Action<string> Output { get; }
    private readonly int _size;
    private readonly int[,] _original;

    public HiddenFortress(int size, string field, Action<string> output)
    {
        Output = output;
        _size = size;
        _original = new int[size, size];
        string[] fieldRows = field
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        for (var i = 0; i < fieldRows.Length; i++)
        {
            for (var j = 0; j < fieldRows[i].Length; j++)
            {
                char ch = fieldRows[i][j];
                int value = char.IsDigit(ch)
                    ? int.Parse(ch.ToString())
                    : (ch is >= 'A' and <= 'Z'
                        ? ch - 29
                        : ch - 87);

                _original[i, j] = value;
            }
        }
    }

    public bool[,] Solve()
    {
        double total = 0;
        for (int i = 0; i < _size; i++)
        for (int j = 0; j < _size; j++)
            total += _original[i, j];

        total /= (2d * _size - 1);

        bool[,] result = new bool[_size, _size];
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                int value = _original[i, j];
                double r = 0;
                double c = 0;
                for (int k = 0; k < _size; k++)
                {
                    r += _original[i, k];
                    c += _original[k, j];
                }

                double rrr = (r - total) / (_size - 1d) + (c - total) / (_size - 1d);
                int expected = (int)rrr;
                if (value != expected)
                    result[i, j] = true;
            }
        }

        return result;
    }

    public long Iterations { get; private set; }
    public long ValidationIterations { get; private set; }

    public static void Print(bool[,] next)
    {
        // print result
        for (int i = 0; i < next.GetLength(0); i++)
        {
            for (int j = 0; j < next.GetLength(1); j++)
            {
                Console.Write((next[i, j] ? 'O' : '.'));
            }

            Console.WriteLine();
        }
    }
}
