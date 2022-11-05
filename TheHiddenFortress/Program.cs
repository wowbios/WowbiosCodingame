using System.Text;
using TheHiddenFortress;

var sb = new StringBuilder();
int size = int.Parse(Console.ReadLine());
for (int i = 0; i < size; i++)
{
    sb.AppendLine(Console.ReadLine());
}

var fortress = new HiddenFortress(size, sb.ToString());
bool[,] result = fortress.Solve();
HiddenFortress.Print(result);