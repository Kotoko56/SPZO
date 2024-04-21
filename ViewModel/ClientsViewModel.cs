using SPZO.Model;
using System.Collections.ObjectModel;

namespace SPZO.ViewModel
{
    public class ClientsViewModel : BaseViewModel
    {

        private Client selectedClient;

        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Client> Clients { get; set; }

        public ClientsViewModel()
        {
            Clients = new ObservableCollection<Client>();

        }
    }
}
