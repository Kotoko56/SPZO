using SPZO.Commands;
using SPZO.DataManagement;
using SPZO.Model;
using System.Collections.ObjectModel;

namespace SPZO.ViewModel
{
    public class ClientsViewModel : BaseViewModel
    {
        //Main class for ClientsView code-behind
        private SQLiteDataAccess myDbContext; //Define database
        
        public ObservableCollection<Client> Clients { get; set; }

        //Define 
        private Client selectedClient;
        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged(); //This allows me to update every filed, that bids data to selected client, when client has changed
            }
        }

        //Get clients from database and make it ObservableCollection. ObservableCollection allows me to pass data to each textblock depending on what is my selectedClient
        private void GetClientsFromDb()
        {
            var clientsFromDb = myDbContext.Clients.ToList(); //Get all records from Clients table and make in a list

            Clients = new ObservableCollection<Client>(clientsFromDb);
        }


        public ClientsViewModel()
        {
            myDbContext = new SQLiteDataAccess(); //Get all data from database
            GetClientsFromDb(); //Get clients from database.

        }

        public RelayCommands AddClientCommand => new RelayCommands(execute =>  AddClient()); //Button allways CanExecute
        public RelayCommands DeleteClientCommand => new RelayCommands(execute => RemoveClient(), canExecute =>  SelectedClient != null); //Button cannot execute, if Client from datagrid is not selected
        public RelayCommands SaveCommand => new RelayCommands(execute => Save(), canExecute => SelectedClient != null); //Button cannot execute, if Client from datagrid is not selected
        private void AddClient() //Add only to datagrid for data fill
        {
            Clients.Add(new Client()
            {
                ClientID = 0
            });
        }

        private void RemoveClient()
        {
            myDbContext.Clients.Remove(SelectedClient); //Remove Client from Database.

            Clients.Remove(SelectedClient); //Removeclient from Class.

            myDbContext.SaveChanges(); //SaveChanges to database
        }

        private void Save()
        {
            if (AddOrSave()) 
            {
                myDbContext.Entry(SelectedClient).CurrentValues.SetValues(SelectedClient); //If client exists, update data in database

                myDbContext.SaveChanges(); //SaveChanges to database
            }
            else
            {
                myDbContext.Clients.Add(SelectedClient); //If client is new add record to database

                myDbContext.SaveChanges(); //SaveChanges to database
            }

        }

        public bool AddOrSave()
        {
            var selectedClientExists = myDbContext.Clients.Find(SelectedClient.ClientID); //Find if client with this id exists in database
            
            if(selectedClientExists != null) { return true; } //If exists than update data
            else { return false; } //If not add to db

        }

    }
}
