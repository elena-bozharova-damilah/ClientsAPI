using Application.Interfaces;
using Infrastructure.Service;
using System.Collections;

namespace Unit.Tests.Infrastructure
{
    public class CalculatorServiceTests
    {
        private readonly ICalculatorService _sut;

        public CalculatorServiceTests()
        {
            _sut = new CalculatorService();
        }

        [Fact]
        public void CanAdd()
        {
            // Arrange
            int value1 = 1;
            int value2 = 2;

            // Act
            var result = _sut.Add(value1, value2);

            // Assert
            Assert.Equal(3, result);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-4, -6, -10)]
        [InlineData(-2, 2, 0)]
        [InlineData(int.MinValue, -1, int.MaxValue)]
        public void CanAddTheory(int value1, int value2, int expected)
        {
            var result = _sut.Add(value1, value2);

            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(CalculatorTestData))]
        public void CanAddTheoryClassData(int value1, int value2, int expected)
        {
            var result = _sut.Add(value1, value2);

            Assert.Equal(expected, result);
        }

        public class CalculatorTestData : IEnumerable<object[]>
        {
            public CalculatorTestData()
            {

            }
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 1, 2, 3 };
                yield return new object[] { -4, -6, -10 };
                yield return new object[] { -2, 2, 0 };
                yield return new object[] { int.MinValue, -1, int.MaxValue };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }


        [Theory]
        [MemberData(nameof(Data))]
        public void Add_Should_Retunr_Result(int value1, int value2, int expected)
        {
            var result = _sut.Add(value1, value2);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
            new object[] { 1, 2, 3 },
            new object[] { -4, -6, -10 },
            new object[] { -2, 2, 0 },
            new object[] { int.MinValue, -1, int.MaxValue },
            };
    }
}
