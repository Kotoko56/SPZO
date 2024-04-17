namespace SPZO.Model
{
    class Client(int clientID, string name, string surname, string pesel, string homeAddress, string vetNumber, string phoneNumber, string rhdNumber, string arimrNumber)
    {
        public int ClientID { get; set; } = clientID;
        public string Name { get; set; } = name;
        public string Surname { get; set; } = surname;
        public string Pesel { get; set; } = pesel;
        public string HomeAddress { get; set; } = homeAddress;
        public string VetNumber { get; set; } = vetNumber;
        public string PhoneNumber { get; set; } = phoneNumber;
        public string RhdNumber { get; set; } = rhdNumber;
        public string ArimrNumber { get; set; } = arimrNumber;
    }
}
