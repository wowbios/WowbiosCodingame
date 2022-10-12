class Relation
{
    public Relation(Node From, Node To, string Result, int Weight)
    {
        this.From = From;
        this.To = To;
        this.Result = Result;
        this.Weight = Weight;
    }

    public Node From { get; init; }
    public Node To { get; init; }
    public string Result { get; init; }
    public int Weight { get; init; }

    public override string ToString() => $"{Result} \t[ {From} \t=> \t{To} ]\t= {Weight}";
}