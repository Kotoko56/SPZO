namespace SPZO.Model
{
    class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }
        public string HomeAddress { get; set; }
        public string VetNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string RhdNumber { get; set; }
        public string ArimrNumber { get; set; }

        public Client(int clientID, string name, string surname, string pesel, string homeAddress, string vetNumber, string phoneNumber, string rhdNumber, string arimrNumber)
        {
            ClientID = clientID;
            Name = name;
            Surname = surname;
            Pesel = pesel;
            HomeAddress = homeAddress;
            VetNumber = vetNumber;
            PhoneNumber = phoneNumber;
            RhdNumber = rhdNumber;
            ArimrNumber = arimrNumber;
        }
    }
}
