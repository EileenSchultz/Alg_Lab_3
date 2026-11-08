
using Xunit;
using Alg_Lab_3;

namespace Alg_Lab_3.Tests
{
    public class ListAlgorithmsTests
    {
        // --- 1. Переворот списка ---
        [Fact]
        public void Reverse_EmptyList_DoesNothing()
        {
            var list = new DoublyLinkedList<int>();
            ListAlgorithms<int>.Reverse(list);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Reverse_MultipleElements_ReversesCorrectly()
        {
            var list = new DoublyLinkedList<int>();
            list.Add(1); list.Add(2); list.Add(3); list.Add(4);

            ListAlgorithms<int>.Reverse(list);

            Assert.Equal(4, list.Count);
            Assert.Equal(4, list[0]); // Проверка нового начала
            Assert.Equal(1, list[3]); // Проверка нового конца
            Assert.Equal(3, list[1]);
            Assert.Equal(2, list[2]);
        }

        // --- 2.1 Перенос последнего в начало ---
        [Fact]
        public void MoveLastToFront_MultipleElements_MovesCorrectly()
        {
            var list = new DoublyLinkedList<int>();
            list.Add(1); list.Add(2); list.Add(3);

            ListAlgorithms<int>.MoveLastToFront(list);

            Assert.Equal(3, list[0]); // Бывший последний
            Assert.Equal(1, list[1]);
            Assert.Equal(2, list[2]); // Бывший предпоследний
        }

        // --- 2.2 Перенос первого в конец ---
        [Fact]
        public void MoveFirstToEnd_MultipleElements_MovesCorrectly()
        {
            var list = new DoublyLinkedList<int>();
            list.Add(1); list.Add(2); list.Add(3);

            ListAlgorithms<int>.MoveFirstToEnd(list);

            Assert.Equal(2, list[0]); // Бывший второй
            Assert.Equal(3, list[1]);
            Assert.Equal(1, list[2]); // Бывший первый
        }

        // --- 3. Подсчет различных элементов ---
        [Theory]
        [InlineData(new int[] { 1, 2, 2, 3, 1, 4 }, 4)] // Уникальные: 1, 2, 3, 4
        [InlineData(new int[] { 1, 1, 1, 1 }, 1)]       // Все одинаковые
        [InlineData(new int[] { }, 0)]                  // Пустой
        public void CountDistinct_ReturnsCorrectCount(int[] elements, int expectedCount)
        {
            var list = new DoublyLinkedList<int>();
            foreach (var el in elements) list.Add(el);

            var result = ListAlgorithms<int>.CountDistinct(list);

            Assert.Equal(expectedCount, result);
        }

        // --- 4. Удаление неуникальных (оставить только уникальные) ---
        [Fact]
        public void RemoveNonUnique_RemovesDuplicates_KeepsSingles()
        {
            // Arrange
            var list = new DoublyLinkedList<int>();
            list.Add(1); list.Add(2); list.Add(2); list.Add(3); list.Add(4); list.Add(4);

            // Act
            ListAlgorithms<int>.RemoveNonUnique(list);

            // Assert
            Assert.Equal(2, list.Count); // Остались только 1 и 3
            Assert.Equal(1, list[0]);
            Assert.Equal(3, list[1]);
        }

        [Fact]
        public void RemoveNonUnique_AllDuplicates_RemovesEverything()
        {
            var list = new DoublyLinkedList<int>();
            list.Add(7); list.Add(7); list.Add(7);

            ListAlgorithms<int>.RemoveNonUnique(list);

            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void RemoveNonUnique_RemovesHeadAndTailCorrectly()
        {
            var list = new DoublyLinkedList<int>();
            list.Add(9); list.Add(1); list.Add(9); // 9 в начале и в конце

            ListAlgorithms<int>.RemoveNonUnique(list);

            Assert.Equal(1, list.Count);
            Assert.Equal(1, list[0]); 
        }
    }
}