﻿using ThomasAndTheFreightCars;

class Solution
{
    public static void Main(string[] args)
    {
        Console.ReadLine();
        string input = Console.ReadLine()!;
        Console.Error.WriteLine(input);
        int maxLength = new Thomas(input).Solve();
        Console.WriteLine(maxLength);
    }
}
