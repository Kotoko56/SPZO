using SPZO.ViewModel;
using System.Windows.Input;

namespace SPZO.Commands
{
    public class UpdateViewCommand : ICommand
    {
        //This class inherits ICommand interface. It allows me to change view between EmptyStartView, ClientsView and PaymentsView
        private MainWindowViewModel viewModel;

        public UpdateViewCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true; //Always have to be executable
        }

        public void Execute(object? parameter)
        {
            if (parameter.ToString() == "Pay") //If additional parameter from xaml is "Pay", switch to PaymentViewModel
            {
                viewModel.SelectedViewModel = new PaymentsViewModel();
            }
            else if (parameter.ToString() == "Client") //If additional parameter from xaml is "Client", switch to ClientViewModel
            {
                viewModel.SelectedViewModel = new ClientsViewModel();
            }
            else
            {
                throw new NotImplementedException(); //There is no else
            }
        }
    }
}
