using System.Diagnostics;

namespace TheHiddenFortress;

public class HiddenFortress
{
    private readonly int _size;
    private readonly int[,] _original;

    public HiddenFortress(int size, string field)
    {
        _size = size;
        _original = new int[size, size];
        string[] fieldRows = field
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        for (var i = 0; i < fieldRows.Length; i++)
        {
            for (var j = 0; j < fieldRows[i].Length; j++)
                _original[i, j] = int.Parse(fieldRows[i][j].ToString());
        }
    }

    public bool[,] Solve()
    {
        bool[,] result = new bool[_size, _size];
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
                result[i, j] = true;
        }

        var sw = Stopwatch.StartNew();
        bool ok = Guess(result, _original, 0, 0);
        Console.WriteLine(sw.Elapsed);
        if (!ok)
            Console.WriteLine("Fail");
        return result;
    }

    private bool Guess(bool[,] field, int[,] expected, int x, int y)
    {
        (int xx, int yy)? next = GetNext(x, y);
        if (next is null) // end
        {
            if (!Validate(field, expected))
            {
                field[x, y] = false;
                if (!Validate(field, expected))
                {
                    field[x, y] = true;
                    return false;
                }
            }

            return true;
        }

        (int xx, int yy) = next.Value;

        field[x, y] = false;
        if (!Guess(field, expected, xx, yy))
        {
            field[x, y] = true;
            if (!Guess(field, expected, xx, yy))
                return false;
        }

        return true;
    }

    private (int, int)? GetNext(int x, int y)
    {
        int last = _size - 1;
        if (x == last && y == last)
            return null;
        if (y == last)
            return (x + 1, 0);

        return (x, y + 1);
    }

    private bool Validate(bool[,] field, int[,] expected)
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                int total = 0;
                for (int c = 0; c < _size; c++)
                {
                    if (field[i, c]) total++;
                    if (field[c, j]) total++;
                }

                int result = field[i, j] ? total - 1 : total;
                if (result != expected[i, j])
                    return false;
            }
        }

        return true;
    }

    public static void Print(bool[,] next)
    {
        // print result
        for (int i = 0; i < next.GetLength(0); i++)
        {
            for (int j = 0; j < next.GetLength(1); j++)
            {
                Console.Write((next[i, j] ? 'O' : '.') + "\t\t");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}