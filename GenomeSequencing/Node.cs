using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Node
{
    public Node(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}