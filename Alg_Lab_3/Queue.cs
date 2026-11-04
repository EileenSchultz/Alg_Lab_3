using System.Collections;

namespace Alg_Lab_3;

public class Queue<T> : IEnumerable<T>
{
    private readonly Alg_Lab_3.LinkedList<T> _list = new();

    public int Count => _list.Count;
    public bool IsEmpty => Count == 0;

    public void Enqueue(T item) => _list.Add(item);

    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty.");
        return _list[0];
    }

    public T Dequeue()
    {
        var item = Peek();
        _list.RemoveAt(0);
        return item;
    }

    public void Print()
    {
        var res = string.Join(" ", _list);
        Console.WriteLine(res);
    }

    public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}