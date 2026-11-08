using System;
using System.Collections.Generic;

namespace Alg_Lab_3
{
    public class ListAlgorithms<T>
    {
        // 1. Переворот списка (изменение ссылок in-place)
        public static void Reverse(DoublyLinkedList<T> list)
        {
            if (list.head == null || list.head.Next == null) return;

            var current = list.head;
            Node<T>? temp = null;

            // Меняем местами Next и Prev для каждого узла
            while (current != null)
            {
                temp = current.Prev;
                current.Prev = current.Next;
                current.Next = temp;
                current = current.Prev; // Идем дальше (бывший Next)
            }

            // Меняем местами head и tail
            temp = list.head;
            list.head = list.tail;
            list.tail = temp;
        }

        // 2.1. Перенос последнего элемента в начало
        public static void MoveLastToFront(DoublyLinkedList<T> list)
        {
            if (list.head == null || list.head == list.tail) return;

            var oldTail = list.tail!;
            var newTail = oldTail.Prev!;

            // Отсоединяем последний
            newTail.Next = null;
            list.tail = newTail;

            // Присоединяем в начало
            oldTail.Next = list.head;
            oldTail.Prev = null;
            list.head!.Prev = oldTail;
            list.head = oldTail;
        }

        // 2.2. Перенос первого элемента в конец
        public static void MoveFirstToEnd(DoublyLinkedList<T> list)
        {
            if (list.head == null || list.head == list.tail) return;

            var oldHead = list.head;
            var newHead = oldHead.Next!;

            // Отсоединяем первый
            newHead.Prev = null;
            list.head = newHead;

            // Присоединяем в конец
            oldHead.Prev = list.tail;
            oldHead.Next = null;
            list.tail!.Next = oldHead;
            list.tail = oldHead;
        }

        // 3. Количество различных элементов (список целых чисел)
        public static int CountDistinct(DoublyLinkedList<int> list)
        {
            if (list.head == null) return 0;

            // Используем HashSet для подсчета уникальных. 
            // Сложность: O(N) по времени, O(N) по памяти.
            var uniqueElements = new HashSet<int>();
            
            foreach (var item in list)
            {
                uniqueElements.Add(item);
            }

            return uniqueElements.Count;
        }

        // 4. Удаление неуникальных элементов (оставить только те, что встречаются 1 раз)
        public static void RemoveNonUnique(DoublyLinkedList<T> list)
        {
            if (list.head == null) return;

            // 1. Подсчитываем частоту каждого элемента
            var counts = new Dictionary<T, int>(EqualityComparer<T>.Default);
            var current = list.head;
            while (current != null)
            {
                if (!counts.ContainsKey(current.Value))
                    counts[current.Value] = 0;
                counts[current.Value]++;
                current = current.Next;
            }

            // 2. Проходим по списку и удаляем дубликаты
            current = list.head;
            while (current != null)
            {
                var nextNode = current.Next; // Запоминаем следующий, так как current можем удалить

                if (counts[current.Value] > 1)
                {
                    // Удаляем текущий узел
                    var prev = current.Prev;
                    
                    if (prev != null) prev.Next = nextNode;
                    else list.head = nextNode; // Если удаляем голову
                    
                    if (nextNode != null) nextNode.Prev = prev;
                    else list.tail = prev; // Если удаляем хвост

                    list.Count--;
                }

                current = nextNode;
            }
        }
    }
}