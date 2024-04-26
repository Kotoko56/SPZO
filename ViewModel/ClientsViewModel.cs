using Microsoft.EntityFrameworkCore;
using SPZO.Commands;
using SPZO.DataManagement;
using SPZO.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SPZO.ViewModel
{
    public class ClientsViewModel : BaseViewModel
    {
        private SQLiteDataAccess myDbContext;
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
            myDbContext = new SQLiteDataAccess();
            GetClientsFromDb();

        }

        public RelayCommands AddClientCommand => new RelayCommands(execute =>  AddClient());
        public RelayCommands DeleteClientCommand => new RelayCommands(execute => RemoveClient(), canExecute =>  SelectedClient != null);
        public RelayCommands SaveCommand => new RelayCommands(execute => Save(), canExecute => SelectedClient != null);
        private void AddClient()
        {
            Clients.Add(new Client()
            {
                ClientID = 0
            });
        }

        private void RemoveClient()
        {
            myDbContext.Clients.Remove(SelectedClient);

            Clients.Remove(SelectedClient);

            myDbContext.SaveChanges();
        }

        private void Save()
        {
            if (AddOrSave())
            {
                myDbContext.Entry(SelectedClient).CurrentValues.SetValues(SelectedClient);

                myDbContext.SaveChanges();
            }
            else
            {
                myDbContext.Clients.Add(SelectedClient);

                myDbContext.SaveChanges();
            }

        }

        public bool AddOrSave()
        {
            var selectedClientExists = myDbContext.Clients.Find(SelectedClient.ClientID);
            
            if(selectedClientExists != null)
            {
                return true;
            }
            else { return false; }

        }

        private void GetClientsFromDb()
        {
            var clientsFromDb = myDbContext.Clients.ToList();

            Clients = new ObservableCollection<Client>(clientsFromDb);
        }
    }
}
