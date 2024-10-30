namespace Bank
{
    public class TransferDetails
    {
        private int _AccountID;
        private string _AccountName;
        private int _Amount;
        public TransferDetails() { }

        public TransferDetails(int accountID, string accountName, int amount)
        {
            _AccountID = accountID;
            _AccountName = accountName;
            _Amount = amount;
        }

        public int AccountID { get => _AccountID; set => _AccountID = value; }
        public string AccountName { get => _AccountName; set => _AccountName = value; }
        public int Amount { get => _Amount; set => _Amount = value; }
    }
}
