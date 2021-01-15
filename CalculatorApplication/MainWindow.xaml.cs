using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace CalculatorApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void inputTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            if(regex.IsMatch(e.Text))
            {
                MessageBox.Show("Input should be integer!");
                e.Handled = true;
            }
        }

        //private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.Key)
        //    {
        //        case Key.Return:
        //            if (DataContext is MainViewModel viewModel)
        //            {
        //                viewModel.AddOperation(Utils.Operations.Equal);
        //            }
        //            break;
        //        case Key.Back:
        //            if (DataContext is MainViewModel mainViewModel)
        //            {
        //                mainViewModel.AddOperation(Utils.Operations.Backspace);
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}
