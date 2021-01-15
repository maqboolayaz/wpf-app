using CalculatorApplication.Utils;

namespace CalculatorApplication.Interfaces
{
    public interface ICalculatorOperations
    {
        string Expression { get; set; }
        string Result { get; set; }
        void AddInput(string digit);
        void AddOperation(Operations operation);
    }
}
