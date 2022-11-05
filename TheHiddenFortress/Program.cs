string field = 
@"65756
45655
43545
22423
56756";
const int size = 5;

// string field =
//     @"212
// 322
// 201";
// const int size = 3;
//
// string field =
//     @"122
// 212
// 221";
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
    for (int j = 0; j < size; j++)
    {
        result[i, j] = true;
    }
}
bool ok = Guess(result, arr, cols, rows, 0, 0);
if (!ok)
    Console.WriteLine("Fail");
Print(result);

static bool Guess(bool[,] field, int[,] expected, int[] cols, int[] rows, int x, int y)
{
    (int xx, int yy)? next = GetNext(x, y);
    if (next is null) // end
    {
        // if (!Validate(field, cols, rows))
        if (!Validate2(field, expected))
        {
            field[x, y] = false;
            // if (!Validate(field, cols, rows))
            if (!Validate2(field, expected))
            {
                field[x, y] = true;
                return false;
            }
        }
        return true;
    }

    (int xx, int yy) = next.Value;
 
    field[x, y] = false;   
    if (!Guess(field, expected, cols, rows, xx, yy))
    {
        field[x, y] = true;
        if (!Guess(field, expected, cols, rows, xx, yy))
            return false;
    }

    return true;
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

static bool Validate2(bool[,] field, int[,] expected)
{
    int[,] map = new int[size, size];

    for (int i = 0; i < size; i++)
    {
        for (int j = 0; j < size; j++)
        {
            int row = 0;
            int col = 0;
            for (int c = 0; c < size; c++)
            {
                if (field[i, c]) row++;
                if (field[c, j]) col++;
            }

            int result = field[i, j] ? row + col - 1 : row + col;
            if (result != expected[i, j])
                return false;
        }
    }

    return true;
}

static bool Validate(bool[,] field, int[] cols, int[] rows)
{
    for (int i = 0; i < field.GetLength(0); i++)
    {
        int sum = 0;
        for (int j = 0; j < field.GetLength(1); j++)
        {
            if (field[i, j]) sum++;
        }

        if (sum != rows[i]) return false;
    }
    for (int i = 0; i < field.GetLength(1); i++)
    {
        int sum = 0;
        for (int j = 0; j < field.GetLength(0); j++)
        {
            if (field[j, i]) sum++;
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