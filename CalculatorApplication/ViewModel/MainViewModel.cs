using CalculatorApplication.Extensions;
using CalculatorApplication.Interfaces;
using CalculatorApplication.Utils;
using System;
using System.Windows.Input;

namespace CalculatorApplication.ViewModel
{
    public class MainViewModel : ViewModelBase, ICalculatorOperations
    {
        private readonly ICalculatorOperations _model;

        private string _experssions;
        public string Expression { get { return _model.Expression; } set { _experssions = value; } }
        private string _result;
        public string Result { get { return _model.Result; } set { _result = value; } }
        public bool IsProcessing { get; set; }
        public ICommand AddInputCommand => new DelegateCommand(o => AddInput((string)o));
        public ICommand ExecuteOperationCommand => new DelegateCommand(o => AddOperation((Operations)o));

        public MainViewModel()
        {
            //This must be resolved through Dependency Injection
            _model = new CalculatorUtilities();
            //Expression = _model.Expression;
            //Result = _model.Result;
        }

        public void AddInput(string digit)
        {
            _model.AddInput(digit);
            OnPropertyChanged(nameof(Expression));
            OnPropertyChanged(nameof(Result));
        }

        public async void AddOperation(Operations operation)
        {
            if (operation == Operations.Fib)
            {
                IsProcessing = true;
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(5));
            }
            _model.AddOperation(operation);
            OnPropertyChanged(nameof(Expression));
            OnPropertyChanged(nameof(Result));
            if (operation == Operations.Fib) IsProcessing = false;
        }
    }
}
