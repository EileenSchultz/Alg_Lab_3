using System.Collections;

namespace Alg_Lab_3;

public class Stack<T> : IEnumerable<T>
{
    private readonly Alg_Lab_3.LinkedList<T> _list = new();

    public int Count => _list.Count;

    public Stack() { }
    public void Push(T item)
    {
        _list.Insert(0, item);
    }

    public T Pop()
    {
        var item = Top();
        _list.RemoveAt(0);
        return item;
    }

    public T Top()
    {
        if (Count == 0)
            throw new InvalidOperationException("Stack is empty");
        return _list[0];
    }

    public bool IsEmpty() => Count == 0;

    public void Print()
    {
        var res = string.Join(" ", _list);
        Console.WriteLine(res);
    }

    public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}