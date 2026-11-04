namespace Alg_Lab_3;

public class LinkedListExtended<T> : LinkedList<T>
{
    // 8. Вставка элемента F перед первым вхождением E
    public bool InsertBeforeFirstOccurrence(T target, T item)
    {
        if (head == null || target is null) return false;

        var cmp = EqualityComparer<T>.Default;

        if (cmp.Equals(head.Value, target))
        {
            var temp = new Node<T>(item);
            temp.Next = head;
            head = temp;
            Count++;
            return true;
        }

        for (var prev = head; prev.Next != null; prev = prev.Next)
        {
            if (cmp.Equals(prev.Next.Value, target))
            {
                var temp = new Node<T>(item);
                temp.Next = prev.Next;
                prev.Next = temp;
                Count++;
                return true;
            }
        }

        return false;
    }

    // 9. Дописывание списка E к списку L
    public void AppendList(LinkedList<T> list)
    {
        if (list is null || ReferenceEquals(this, list) || list.head is null)
            return;

        var copy = list.Clone();

        if (head is null)
        {
            head = copy.head;
            tail = copy.tail;
            Count = copy.Count;
        }
        else
        {
            tail!.Next = copy.head;
            tail = copy.tail;
            Count += copy.Count;
        }
    }

    // 10. Разбиение списка на два по первому вхождению заданного числа
    public LinkedList<T> SplitList(T target)
    {
        if (head == null || target is null) return new LinkedList<T>();

        var cmp = EqualityComparer<T>.Default;

        if (cmp.Equals(head.Value, target))
        {
            var copy = this.Clone();
            this.Clear();
            return copy;
        }

        var index = 0;
        for (var prev = head; prev.Next != null; prev = prev.Next)
        {
            index++;
            if (cmp.Equals(prev.Next.Value, target))
            {
                var copy = this.Clone(index, Count - 1);
                prev.Next = null;
                tail = prev;
                Count = index;
                return copy;
            }
        }

        return new LinkedList<T>();
    }

    // 11. Удвоение списка (приписывание себя к концу)
    public void DoubleList()
    {
        if (head == null) return;

        var copy = this.Clone();

        tail!.Next = copy.head;
        tail = copy.tail;
        Count += copy.Count;
    }

    // 12. Поменять местами два элемента списка
    public bool SwapElements(T first, T second)
    {
        if (head == null) return false;

        var cmp = EqualityComparer<T>.Default;

        Node<T>? firstNode = null;
        Node<T>? secondNode = null;

        for (var cur = head; cur != null; cur = cur.Next)
        {
            if (firstNode == null && cmp.Equals(cur.Value, first))
            {
                firstNode = cur;
            }
            else if (secondNode == null && cmp.Equals(cur.Value, second))
            {
                secondNode = cur;
            }

            if (firstNode != null && secondNode != null) break;
        }

        if (firstNode == null || secondNode == null) return false;

        (firstNode.Value, secondNode.Value) = (secondNode.Value, firstNode.Value);
        return true;
    }
}