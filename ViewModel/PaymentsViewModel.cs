using SPZO.Commands;
using SPZO.DataManagement;
using SPZO.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace SPZO.ViewModel
{
    public class PaymentsViewModel : BaseViewModel
    {
        public SQLiteDataAccess myDbContext; //Define database
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

        public void GetClientsFromDb()
        {
            var clientsFromDb = myDbContext.Clients.ToList();

            Clients = new ObservableCollection<Client>(clientsFromDb);
        }


        public ObservableCollection<Prices> Prices { get; }

        private Prices selectedPrices;

        public Prices SelectedPrices
        {
            get { return selectedPrices; }
            set { 
                selectedPrices = value; 
                OnPropertyChanged(); 
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


        public PaymentsViewModel()
        {
            myDbContext = new SQLiteDataAccess();
            GetClientsFromDb();
            GetPaymentsFromDb(); //Get list of made payments
            Prices = new ObservableCollection<Prices>(); //Prices are hardcoded, so define for ObservableCollection is here.

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

            if (decimal.TryParse(beeAmount, out decimal _beeAmount)) //Calculate total amount based on beeAmount input
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
                    if (decimal.TryParse(beeAmount, out decimal _beeAmount)) //Calculate BeeFee based on beeAmount
                    {

                        decimal beeFeePayment = SelectedPrices.BeeHouse * _beeAmount;

                        return $"Ulowe: {beeFeePayment}";
                    }
                    else
                    {
                        return $"";

                    }
                }
                catch (NullReferenceException) //If user enters beeAmount before selecting payment type message box will be displayed, andd beeAmount deleted
                {
                    MessageBox.Show("Wpierw wybierz rodzaj płatności!");
                    beeAmount = null;
                    return $"";
                }
            }
        }


        public RelayCommands PaymentCommand => new RelayCommands(execute => MakePayment(), canExecute => (selectedClient != null && selectedPrices != null && beeAmount != null)); //button is availbe, if user is selected, payment type is selected and beeAmount is different than null. BeeAmount can be 0!

        private void MakePayment()
        {

            var newPeyment = new Payments()
            {
                PaymentID = GetLastPaymentId() + 1, //get last id from payment table and add one
                ClientID = selectedClient.ClientID, //get client id based on selected user
                PaymentType = selectedPrices.PaymentType, //get payment type based on selected payment type
                BeeHiveNumber = beeAmount, //gen beeAmount from textbox
                SumOfPayment = totalFee,
                PaymentDate = DateTime.Today //get Todays Date

            };

            Payments.Add(newPeyment); //add to class

            myDbContext.Payments.Add(newPeyment); //add record to database

            myDbContext.SaveChanges(); //save changed to database

        }


    }
}
