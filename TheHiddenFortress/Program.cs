string field = 
@"65756
45655
43545
22423
56756";
const int size = 5;
//
// string field =
//     @"212
// 322
// 201";
// const int size = 3;

int[,] arr = new int[size, size];
var fieldRows = field
    .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
for (int i = 0; i < fieldRows.Length; i++)
{
    for (int j = 0; j < fieldRows[i].Length; j++)
    {
        arr[i, j] = int.Parse(fieldRows[i][j].ToString());
    }
}

(int[] cols, int[] rows) = FindMins(arr);
Console.WriteLine("ROW MINS: " + string.Join(", ", rows));
Console.WriteLine("COL MINS: " + string.Join(", ", cols));

bool[,] result = new bool[size, size];

for (int i = 0; i < size; i++)
{
    bool ok = false;
    for (int j = 0; j < size; j++)
    {
        Console.WriteLine($"START AT {i} {j}");
        if (Guess(result, cols, rows, i, j, true))
        {
            ok = true;
            break;
        }
        else
        {
            result = new bool[size, size];
        }
    }

    if (ok)
        break;
}

Print(result);

static bool Guess(bool[,] field, int[] cols, int[] rows, int x, int y, bool guess)
{
    // Console.WriteLine($"GUESS {x} {y}");
    

    field[x, y] = guess;
    if (Validate(field, cols, rows))
    {
        (int xx, int yy)? next = GetNext(x, y);
        if (next is null) return true;
        return Guess(field, cols, rows, next.Value.xx, next.Value.yy, guess);
    }
    else
    {
        (int xx, int yy)? next = GetNext(x, y);
        if (next is null) return false;
        return Guess(field, cols, rows, next.Value.xx, next.Value.yy, guess);
    }
}

static (int, int)? GetPrev(int x, int y)
{
    const int last = size - 1;
    if (x == 0 && y == 0)
        return null;
    if (y == 0)
        return (x - 1, last);
    
    return (x, y - 1);
}

static (int, int)? GetNext(int x, int y)
{
    const int last = size - 1;
    if (x == last && y == last)
        return null;
    if (y == last)
        return (x + 1, 0);
    
    return (x, y + 1);
}

static bool Validate(bool[,] field, int[] cols, int[] rows)
{
    for (int i = 0; i < field.GetLength(0); i++)
    {
        int sum = 0;
        for (int j = 0; j < field.GetLength(1); j++)
        {
            sum += field[i, j] ? 1 : 0;
        }

        if (sum != rows[i]) return false;
    }
    for (int i = 0; i < field.GetLength(1); i++)
    {
        int sum = 0;
        for (int j = 0; j < field.GetLength(0); j++)
        {
            sum += field[j, i] ? 1 : 0;
        }

        if (sum != cols[i]) return false;
    }

    return true;
}

static void Print(bool[,] next)
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

(int[] cols, int[] rows) FindMins(int[,] source)
{
    int[] rowMaxs = new int[size];
    int[] colMaxs = new int[size];

    // rows
    for (int i = 0; i < source.GetLength(0); i++)
    {
        int maxInRow = 1000;

        for (int j = 0; j < source.GetLength(1); j++)
        {
            maxInRow = Math.Min(maxInRow, source[i, j]);
        }

        rowMaxs[i] = maxInRow;
    }

    // cols
    for (int i = 0; i < size; i++)
    {
        int maxInRow = 1000;

        for (int j = 0; j < size; j++)
        {
            maxInRow = Math.Min(maxInRow, source[j, i]);
        }

        colMaxs[i] = maxInRow;
    }

    return (colMaxs, rowMaxs);
}