using Application.Interfaces;
using FluentAssertions;
using Infrastructure.Service;

namespace Unit.Tests.Infrastructure
{
    public class NumberServiceTests
    {
        private readonly INumberService _sut;

        public NumberServiceTests()
        {
            _sut = new NumberService();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        public void IsPrime_Should_Return_True_When_PrimesLessThan10(int value)
        {
            // Arrange / Act
            var result = _sut.IsPrime(value);

            // Assert
            result.Should().Be(true, $"{value} should be prime");
            Assert.True(result, $"{value} should be prime");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void IsPrime_Should_ReturnFalse_When_ValuesLessThan2(int value)
        {
            // Arrange / Act
            var result = _sut.IsPrime(value);

            // Assert
            result.Should().Be(false, $"{value} should not be prime");
        }

        [Theory]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(9)]
        public void IsPrime_Should_ReturnFalse_When_NonPrimesLessThan10(int value)
        {
            // Arrange / Act    
            var result = _sut.IsPrime(value);

            // Assert
            Assert.False(result, $"{value} should not be prime");
        }

        #region OddNumber

        [Theory]
        [InlineData(5, 1, 3, 9)]
        [InlineData(7, 1, 5, 3)]
        public void IsOdd_Should_Return_True_WithInlineData(int a, int b, int c, int d)
        {

            Assert.True(_sut.IsOddNumber(a));
            Assert.True(_sut.IsOddNumber(b));
            Assert.True(_sut.IsOddNumber(c));
            Assert.True(_sut.IsOddNumber(d));
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void AllNumbers_AreOdd_WithClassData(int a, int b, int c, int d)
        {
            Assert.True(_sut.IsOddNumber(a));
            Assert.True(_sut.IsOddNumber(b));
            Assert.True(_sut.IsOddNumber(c));
            Assert.True(_sut.IsOddNumber(d));
        }

        public class TestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] {5, 1, 3, 9},
                new object[] {7, 1, 5, 3}
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }


        #endregion
    }
}
