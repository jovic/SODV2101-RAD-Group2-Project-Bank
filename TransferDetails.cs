namespace Bank
{
    public class TransferDetails
    {
        // Private field to store the account ID
        private int _AccountID;

        // Private field to store the account name
        private string _AccountName;

        // Private field to store the transfer amount
        private int _Amount;

        // Default constructor
        public TransferDetails() { }

        // Parameterized constructor to initialize the TransferDetails object
        public TransferDetails(int accountID, string accountName, int amount)
        {
            _AccountID = accountID; // Set the account ID
            _AccountName = accountName; // Set the account name
            _Amount = amount; // Set the transfer amount
        }

        // Public property to get or set the account ID
        public int AccountID
        {
            get => _AccountID;
            set => _AccountID = value;
        }

        // Public property to get or set the account name
        public string AccountName
        {
            get => _AccountName;
            set => _AccountName = value;
        }

        // Public property to get or set the transfer amount
        public int Amount
        {
            get => _Amount;
            set => _Amount = value;
        }
    }
}