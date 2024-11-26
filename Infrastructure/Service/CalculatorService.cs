using Application.Interfaces;

namespace Infrastructure.Service
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(int value1, int value2)
        {
            return value1 + value2;
        }

        public float Divide(int value1, int value2)
        {
            if (value2 == 0)
            {
                throw new Exception("Cannot divide by zero");
            }

            return value1 / value2;
        }

        public int Subtract(int value1, int value2)
        {
            return value1 - value2;
        }
    }
}
