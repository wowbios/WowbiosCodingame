public class StateMachine
{
    private readonly Action<int> _output;
    private readonly Stack<int> _stack = new();
    private readonly Dictionary<string, Action> _operations;
    private readonly Dictionary<string, List<string>> _functions = new();
    private bool _defining;
    private List<string>? _definingFunction;
    private readonly Stack<bool> _ifSkips = new ();

    public StateMachine(Action<int> output)
    {
        _output = output;
        _operations = new()
        {
            { "ADD", Add },
            { "SUB", Sub },
            { "MUL", Mul },
            { "DIV", Div },
            { "MOD", Mod },
            { "POP", () => Pop() },
            { "DUP", Dup },
            { "SWP", Swp },
            { "ROT", Rot },
            { "OVR", Ovr },
            { "POS", Pos },
            { "NOT", Not },
            { "OUT", Out },
        };
    }

    public void ProcessOperation(string operation)
    {
        bool ifSkip = _ifSkips.TryPeek(out bool s) && s;
        switch (operation)
        {
            case "DEF":
                _defining = true;
                break;
            case "END":
                _defining = false;
                break;
            case "FI" when !_defining:
                _ifSkips.Pop();
                break;
            case "ELS" when !_defining:
                _ifSkips.Push(!_ifSkips.Pop());
                break;
            case "IF" when !_defining:
                if (ifSkip)
                    _ifSkips.Push(true);
                else
                    _ifSkips.Push(!If());
                break;
            default:
                if (!_defining && ifSkip) break;

                if (_defining)
                {
                    if (IsKnownOperation(operation))
                    {
                        _definingFunction!.Add(operation);
                    }
                    else
                    {
                        _functions.Add(operation, _definingFunction = new List<string>());
                    }
                }
                else if (int.TryParse(operation, out int value))
                {
                    Push(value);
                }
                else if (_operations.TryGetValue(operation, out Action? op))
                {
                    op();
                }
                else if (_functions.TryGetValue(operation, out List<string>? function))
                {
                    foreach (string functionOperation in function)
                        ProcessOperation(functionOperation);
                }

                break;
        }
    }

    private bool IsKnownOperation(string operation)
        => int.TryParse(operation, out _)
           || _operations.ContainsKey(operation)
           || _functions.ContainsKey(operation)
           || operation is "IF" or "ELS" or "FI";

    private void Push(int value) => _stack.Push(value);
    private int Pop() => _stack.Pop();
    private void Add() => Push(Pop() + Pop());
    private void Mul() => Push(Pop() * Pop());
    private void Div()
    {
        int first = Pop();
        Push(Pop() / first);
    }

    private void Mod()
    {
        int first = Pop();
        Push(Pop() % first);
    }

    private void Pos() => Push(Pop() >= 0 ? 1 : 0);
    private void Not() => Push(Pop() == 0 ? 1 : 0);
    private void Out() => _output(Pop());
    
    private bool If() => Pop() != 0;
    
    private void Sub()
    {
        int first = Pop();
        Push(Pop() - first);
    }

    private void Dup()
    {
        int value = Pop();
        Push(value);
        Push(value);
    }

    private void Swp()
    {
        int first = Pop();
        int second = Pop();
        Push(first);
        Push(second);
    }

    private void Rot()
    {
        int first = Pop();
        int second = Pop();
        int third = Pop();
        Push(second);
        Push(first);
        Push(third);
    }

    private void Ovr()
    {
        int first = Pop();
        int second = Pop();
        Push(second);
        Push(first);
        Push(second);
    }
}