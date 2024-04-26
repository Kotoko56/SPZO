using SPZO.Commands;
using SPZO.DataManagement;
using SPZO.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace SPZO.ViewModel
{
    public class PaymentsViewModel : BaseViewModel
    {
        public SQLiteDataAccess myDbContext;
        public ObservableCollection<Client> Clients { get; set; }
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
            myDbContext = new SQLiteDataAccess();
            GetClientsFromDb();
            GetPaymentsFromDb();
            Prices = new ObservableCollection<Prices>();

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
                try
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
                catch (NullReferenceException)
                {
                    MessageBox.Show("Wpierw wybierz rodzaj płatności!");
                    beeAmount = null;
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
            }

        }

        public RelayCommands PaymentCommand => new RelayCommands(execute => MakePayment(), canExecute => (selectedClient != null && selectedPrices != null && beeAmount != null));

        private void MakePayment()
        {

            var newPeyment = new Payments()
            {
                //musi pobierać z bazy ostatnie id i podawać +1
                PaymentID = GetLastPaymentId() + 1,
                ClientID = selectedClient.ClientID,
                PaymentType = selectedPrices.PaymentType,
                BeeHiveNumber = beeAmount,
                SumOfPayment = totalAmount,
                PaymentDate = DateTime.Today

            };

            Payments.Add(newPeyment);

            myDbContext.Payments.Add(newPeyment);

            myDbContext.SaveChanges();

        }

        public void GetClientsFromDb()
        {
            var clientsFromDb = myDbContext.Clients.ToList();

            Clients = new ObservableCollection<Client>(clientsFromDb);
        }

        public void GetPaymentsFromDb()
        {
            var paymentsFromDb = myDbContext.Payments.ToList();

            Payments = new ObservableCollection<Payments>(paymentsFromDb);
        }

        public int GetLastPaymentId()
        {
            if (myDbContext.Payments.Any())
            {
                int lasd = myDbContext.Payments.Max(Payments => Payments.PaymentID);

                Console.WriteLine(lasd);

                return lasd;
            }
            else { return 1; }
        }
    }
}
