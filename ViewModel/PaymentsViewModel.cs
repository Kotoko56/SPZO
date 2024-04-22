using SPZO.Model;
using System.Collections.ObjectModel;

namespace SPZO.ViewModel
{
    public class PaymentsViewModel : BaseViewModel
    {
        public ObservableCollection<Client> Clients { get; }
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

        public ObservableCollection<Prices> Prices { get; }

        private Prices getPrices;

        public Prices GetPrices
        {
            get { return getPrices; }
            set { getPrices = value; }
        }


        public PaymentsViewModel()
        {
            Clients = new ObservableCollection<Client>();
            Prices = new ObservableCollection<Prices>();

			Clients.Add(new Client
			{
				ClientID = 1,
				Name = "Andrzej",
                Surname = "A"
			});

            Clients.Add(new Client
            {
                ClientID = 2,
                Name = "Roman",
                Surname = "B"
            });
             
            Prices.Add(new Prices
            {
                Membership = 35,
            });


        }

	}
}
