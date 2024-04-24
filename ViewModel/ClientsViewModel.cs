using SPZO.Commands;
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

        public RelayCommands AddClientCommand => new RelayCommands(execute =>  AddClient());
        public RelayCommands DeleteClientCommand => new RelayCommands(execute => RemoveClient(), canExecute =>  SelectedClient != null);
        public RelayCommands SaveCommand => new RelayCommands(execute => SaveClient(), canExecute => false);
        private void AddClient()
        {
            Clients.Add(new Client()
            {
                ClientID = 0
            });
        }

        private void RemoveClient()
        {
            Clients.Remove(SelectedClient);
        }

        private void SaveClient()
        {
            //saving to db here in future
            Clients.Add(SelectedClient);
        }
        /*
        private bool CanSave()
        {
            if (SelectedClient != null)
            {
                return true;
            }
        }
        */
    }
}
