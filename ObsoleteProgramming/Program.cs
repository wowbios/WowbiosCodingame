class Program
{
    public static void Main(string[] args)
    {
        var machine = new StateMachine(Console.WriteLine);
        int n = int.Parse(Console.ReadLine()!);
        for (int i = 0; i < n; i++)
        {
            string line = Console.ReadLine()!;
            foreach (string operation in line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                machine.ProcessOperation(operation);
        }
    }
}