using Xunit;
using Alg_Lab_3;
using System;

namespace Alg_Lab_3.Tests
{
    public class MathAlgorithmsTests
    {
        // --- Тесты для вычисления постфиксной записи (Пункт 1) ---
        [Theory]
        [InlineData("7 8 + 3 2 + /", 3)] // Пример из задания
        [InlineData("2 3 ^", 8)]
        [InlineData("4 sqrt", 2)]
        [InlineData("5 1 2 + 4 * + 3 -", 14)] // Классический пример ОПЗ
        [InlineData("10 ln", 2.302585092994046)] // ln(10)
        [InlineData("1 2 <<", 4)] // 1 побитовый сдвиг влево на 2 = 4
        public void Evaluate_PostfixExpression_ReturnsExpectedResult(string expression, double expected)
        {
            // Act
            var result = PostfixEvaluator.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result, precision: 10); // 10 знаков после запятой для точности
        }

        [Fact]
        public void Evaluate_DivisionByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            string expression = "1 0 /";

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => PostfixEvaluator.Evaluate(expression));
        }

        // --- Тесты для перевода Инфикс -> Постфикс (Пункт 2) ---
        [Theory]
        [InlineData("2+2", "2 2 +")] // Пример из задания
        [InlineData("3 + 4 * 2", "3 4 2 * +")] // Приоритет операторов
        [InlineData("(1 - 5)", "1 5 -")] // Скобки
        [InlineData("3 + 4 * 2 / (1 - 5) ^ 2", "3 4 2 * 1 5 - 2 ^ / +")] // Сложное выражение
        [InlineData("sin(10) + 5", "10 sin 5 +")] // Унарные функции
        public void Convert_InfixToPostfix_ReturnsCorrectExpression(string infix, string expectedPostfix)
        {
            // Act
            var result = InfixToPostfixConverter.Convert(infix);

            // Assert
            Assert.Equal(expectedPostfix, result);
        }
    }
}