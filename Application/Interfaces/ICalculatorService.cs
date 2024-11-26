namespace Application.Interfaces
{
    public interface ICalculatorService
    {
        int Add(int value1, int value2);

        int Subtract(int value1, int value2);

        float Divide(int value1, int value2);
    }
}
