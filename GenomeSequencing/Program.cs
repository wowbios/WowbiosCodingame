int n = int.Parse(Console.ReadLine()!);
string[] words = new string[n];
for (int i = 0; i < n; i++)
{
    string sequence = Console.ReadLine()!;
    Print(sequence);
    words[i] = sequence;
}
// string[] words = { "AGATTA", "GATTACA", "TACAGA" };
// string[] words = { "ACC", "ACC" };
// string[] words = { "CCCTG", "TGACA", "CATGA" };

Graph graph = Graph.MakeGraph(words);
Print(graph);
const int max = 6 * 10 + 1;
int min = max;
foreach (Relation relation in graph.Relations.OrderByDescending(x => x.Weight))
{
    min = Math.Min(min, TryMerge(graph, relation.Result));
}
Console.WriteLine(min == max ? words[0].Length : min);

static int TryMerge(Graph graph, string? startRelation)
{
    while (graph.Nodes.Count > 1)
    {
        Print("\nMERGE");
        graph = graph.MergeMaxNode(startRelation);
        startRelation = null;
        // Print(graph);
    }
    // Print(graph);
    return graph.Nodes.First().Value.Value.Length;
}
static void Print(object obj) => Console.WriteLine(obj);