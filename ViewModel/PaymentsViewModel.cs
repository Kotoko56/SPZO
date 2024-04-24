using SPZO.Commands;
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

        private Prices selectedPrices;

        public Prices SelectedPrices
        {
            get { return selectedPrices; }
            set { selectedPrices = value; OnPropertyChanged(); }
        }

        public PaymentsViewModel()
        {
            Clients = new ObservableCollection<Client>();
            Prices = new ObservableCollection<Prices>();
            Payments = new ObservableCollection<Payments>();

			Clients.Add(new Client
			{
				ClientID = 1,
				Name = "Andrzej",
                Surname = "A",
                Pesel = "15695161651",
                HomeAddress = "sadgfasedfsdfgsdfg",
                VetNumber = "654165161651",
                PhoneNumber = "9854141596818596",
                RhdNumber = "546+26161511",
                ArimrNumber = "91516516156156156156156"
			});

            Clients.Add(new Client
            {
                ClientID = 2,
                Name = "Roman",
                Surname = "B",
                Pesel = "15695161651",
                HomeAddress = "sADGADRFGHDFGH",
                VetNumber = "8273645834",
                PhoneNumber = "346543256",
                RhdNumber = "5463456345634576",
                ArimrNumber = "2346528346"
            });
            
            //Hardcoded prices because they don't change
            Prices.Add(new Prices
            {
                PaymentType = "Standardowa",
                BeeHouse = 3m,
                Membership = 50m,
                Entry_fee = 0m,
                Insurance = 15m
            });

            Prices.Add(new Prices
            {
                PaymentType = "Bez ubezpieczenia",
                BeeHouse = 3m,
                Membership = 35m,
                Entry_fee = 0m,
                Insurance = 0m
            });

            Prices.Add(new Prices
            {
                PaymentType = "Tylko członkowskie",
                BeeHouse = 0m,
                Membership = 35m,
                Entry_fee = 0m,
                Insurance = 0m
            });

            Prices.Add(new Prices
            {
                PaymentType = "Nowy płatnik",
                BeeHouse = 3m,
                Membership = 35m,
                Entry_fee = 50m,
                Insurance = 15m
            });

            Prices.Add(new Prices
            {
                PaymentType = "Nowy płatnik bez ubezpieczenia",
                BeeHouse = 3m,
                Membership = 35m,
                Entry_fee = 50m,
                Insurance = 15m
            });

        }

        private string totalAmount;

        public string TotalAmount
        {
            get { return totalAmount; }
            set 
            {
                totalAmount = value;
                OnPropertyChanged(nameof(SelectedPrices));
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        private string beeAmount;

        public string BeeAmount
        {
            get { return beeAmount; }
            set
            {
                beeAmount = value;
                OnPropertyChanged(nameof(BeeAmount));
                OnPropertyChanged(nameof(BeeFeeAmount));
                CalculateTotalFeeAmount();
            }
        }

        public void CalculateTotalFeeAmount()
        {
            
            if (decimal.TryParse(beeAmount, out decimal _beeAmount))
            {

                decimal totalFeeAmount = (SelectedPrices.BeeHouse * _beeAmount) + SelectedPrices.Membership + SelectedPrices.Entry_fee + SelectedPrices.Insurance;

                TotalAmount = "Suma: " + totalFeeAmount.ToString();

            }
        }

        public string BeeFeeAmount
        {
            get
            {
                if (decimal.TryParse(beeAmount, out decimal _beeAmount))
                {

                    decimal beeFeePayment = SelectedPrices.BeeHouse * _beeAmount;

                    return $"Ulowe: {beeFeePayment}";
                }
                else
                {
                    return $"";
                }
            }
        }

        public ObservableCollection<Payments> Payments { get; set; }

        private Payments payments;

        public Payments Payment
        {
            get { return payments; }
            set
            {
                payments = value;
                OnPropertyChanged(nameof(Payment));
            }

        }

        public RelayCommands PaymentCommand => new RelayCommands(execute => MakePayment(), canExecute => (selectedClient != null && selectedPrices != null));

        private void MakePayment()
        {
            Payments.Add(new Payments()
            {
                //musi pobierać z bazy ostatnie id i podawać +1
                PaymentID = 1,
                ClientID = selectedClient.ClientID,
                PaymentType = selectedPrices.PaymentType,
                BeeHiveNumber = beeAmount,
                SumOfPayment = totalAmount,
                PaymentDate = DateTime.Today
            });
        }
    }
}
