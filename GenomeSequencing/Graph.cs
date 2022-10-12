class Graph
{
    public Dictionary<string, Node> Nodes { get; } = new();
    public List<Relation> Relations { get; } = new();

    public Node GetNode(string value)
    {
        if (!Nodes.TryGetValue(value, out var node))
            Nodes.Add(value, node = new Node(value));

        return node;
    }

    public void AddRelation(Node from, Node to, string result)
    {
        var relation = new Relation(
            from,
            to,
            result,
            (from.Value.Length + to.Value.Length) - result.Length);
        Relations.Add(relation);
    }

    public override string ToString()
        => $"Nodes:\n{string.Join('\n', Nodes.Values)}\nRelations:\n{string.Join('\n', Relations)}";

    public Graph? MergeMaxNode(string? startRelation = null)
    {
        var maxRelation = Relations
            .Where(r => startRelation is null || r.Result == startRelation)
            .MaxBy(x => x.Weight);
        if (maxRelation is null)
            return null;

        string[] newWords = Nodes.Values
            .Where(x => x != maxRelation.From && x != maxRelation.To)
            .Select(x=>x.Value)
            .Append(maxRelation.Result)
            .ToArray();
        return MakeGraph(newWords);
    }

    public static Graph MakeGraph(string[] words)
    {
        var graph = new Graph();
        if (words.Length == 1)
        {
            graph.GetNode(words[0]);
            return graph;
        }
        words = words.Distinct().ToArray();

        foreach (string word in words)
        {
            foreach (string otherWord in words)
            {
                if (word == otherWord)
                    continue;

                int length = GetIntersection(word, otherWord);
                string result = string.Concat(word, otherWord.AsSpan(length));
                graph.AddRelation(
                    graph.GetNode(word),
                    graph.GetNode(otherWord),
                    result);
            }
        }

        return graph;
    }
    
    
    private static int GetIntersection(string word, string otherWord)
    {
        for (int wi = 0; wi < word.Length; wi++)
        {
            if (word[wi] != otherWord[0]) continue;

            bool intersects = true;
            int length = 0;
            for (var i = 0; i < otherWord.Length; i++)
            {
                if (wi + i == word.Length)
                    break;

                if (otherWord[i] == word[wi + i])
                    length++;
                else
                {
                    intersects = false;
                    break;
                }
            }

            if (intersects)
                return length;
        }

        return 0;
    }
}