using Application.Interfaces;

namespace Infrastructure.Service
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(int value1, int value2)
        {
            return value1 + value2;
        }
    }
}
