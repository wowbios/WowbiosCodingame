using ThomasAndTheFreightCars;

class Solution
{
    public static void Main(string[] args)
    {
        Console.ReadLine();
        string input = Console.ReadLine()!;
        Console.Error.WriteLine(input);
        int maxLength = Thomas.Solve(input);
        Console.WriteLine(maxLength);
    }
}
