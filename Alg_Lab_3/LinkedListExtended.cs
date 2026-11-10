namespace Alg_Lab_3;

public class LinkedListExtended<T> : LinkedList<T>

{
    public static void Reverse<T>(LinkedList<T> list)
        {
            if (list.head == null || list.head.Next == null) return;

            Node<T>? prev = null;
            Node<T>? current = list.head;
            list.tail = current; // Старый head станет новым хвостом

            while (current != null)
            {
                Node<T>? nextTemp = current.Next; // Запоминаем следующий узел
                current.Next = prev; // Разворачиваем ссылку назад
                prev = current; // Двигаем prev вперед
                current = nextTemp; // Двигаем current вперед
            }

            list.head = prev; // Новый head - это последний обработанный узел
        }

        // 2.1. Перенос последнего элемента в начало (для односвязного)
        public static void MoveLastToFront<T>(LinkedList<T> list)
        {
            if (list.head == null || list.head == list.tail) return;

            Node<T>? current = list.head;

            // Ищем предпоследний элемент (тот, чей Next указывает на tail)
            while (current.Next != list.tail)
            {
                current = current.Next;
            }

            Node<T>? oldTail = list.tail;

            current.Next = null; // Отвязываем старый хвост
            list.tail = current; // Новый хвост - это предпоследний
            oldTail.Next = list.head; // Привязываем старый хвост к началу
            list.head = oldTail; // Новый head - это бывший хвост
        }

        // 2.2. Перенос первого элемента в конец (для односвязного)
        public static void MoveFirstToEnd<T>(LinkedList<T> list)
        {
            if (list.head == null || list.head == list.tail) return;

            Node<T>? oldHead = list.head;

            list.head = oldHead.Next; // Сдвигаем head
            oldHead.Next = null; // Отвязываем старый первый

            list.tail.Next = oldHead; // Привязываем его в конец
            list.tail = oldHead; // Обновляем tail
        }

        // 3. Количество различных элементов (список целых чисел)
        public static int CountDistinct<T>(LinkedList<int> list)
        {
            if (list.head == null) return 0;

            // HashSet автоматически хранит только уникальные значения
            var uniqueElements = new HashSet<int>();
            var current = list.head;

            while (current != null)
            {
                uniqueElements.Add(current.Value);
                current = current.Next;
            }

            return uniqueElements.Count;
        }

        // 4. Удаление неуникальных элементов (оставить только те, что встречаются 1 раз)
        public static void RemoveNonUnique<T>(LinkedList<T> list)
        {
            if (list.head == null) return;

            // 1. Считаем частоту каждого элемента
            var counts = new Dictionary<T, int>();
            var current = list.head;
            while (current != null)
            {
                if (!counts.ContainsKey(current.Value))
                    counts[current.Value] = 0;
                counts[current.Value]++;
                current = current.Next;
            }

            // 2. Удаляем дубликаты
            Node<T>? prev = null;
            current = list.head;

            while (current != null)
            {
                if (counts[current.Value] > 1) // Элемент повторяется
                {
                    if (prev == null)
                    {
                        // Удаляем первый элемент (head)
                        list.head = current.Next;
                    }
                    else
                    {
                        // Перекидываем ссылку через удаляемый элемент
                        prev.Next = current.Next;
                    }

                    // Если удалили хвост, сдвигаем tail
                    if (current == list.tail)
                    {
                        list.tail = prev;
                    }

                    list.Count--;
                }
                else
                {
                    // Двигаем prev только если текущий узел остается
                    prev = current;
                }

                current = current.Next;
            }
        }
        
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