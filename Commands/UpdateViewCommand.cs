using SPZO.ViewModel;
using System.Windows.Input;

namespace SPZO.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainWindowViewModel _viewModel;

        public UpdateViewCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter.ToString() == "Pay")
            {
                _viewModel.SelectedViewModel = new PaymentsViewModel();
            }
            else if (parameter.ToString() == "Client")
            {
                _viewModel.SelectedViewModel = new ClientsViewModel();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
