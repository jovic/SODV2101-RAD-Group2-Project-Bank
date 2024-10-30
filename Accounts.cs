namespace Bank
{
    public class Accounts
    {

        private int AccountID;
        private int SavingsID;
        private int ChequeingID;
        private int LoanID;
        private int clientID;
        private string clientName;
        private string homeAddress;
        private string AccountantName;
        private int BranchID;
        private int interestRate = 5;
        private int UserID;
        public Accounts() { }
        public Accounts(int clientID) { }

        public void setClientID(int clientID)
        {
            this.clientID = clientID;
        }

        public int getClientID()
        {
            return clientID;
        }

        public void setUserID(int userID)
        {
            UserID = userID;
        }
        public int getUserID()
        {
            return UserID;
        }
        public void setSavingsID(int savingsID)
        {
            SavingsID = savingsID;
        }

        public int getSavingsID()
        {
            return SavingsID;
        }

        public void SetChequeingID(int chequeingID)
        {
            ChequeingID = chequeingID;
        }
        public int getChequeingID()
        {
            return ChequeingID;
        }

        public void setLoanID(int loanID)
        {
            LoanID = loanID;
        }

        public int getLoanID()
        {
            return LoanID;
        }

        public void setClientName(string clientName)
        {
            this.clientName = clientName;
        }

        public string getClientName()
        {
            return clientName;
        }

        public void sethomeAddress(string homeAddress)
        {
            this.homeAddress = homeAddress;
        }

        public void setAccountantName(string accountantName)
        {
            AccountantName = accountantName;
        }

        public string getAccountantName()
        {
            return AccountantName;
        }

        public void setBranchID(int branchID)
        {
            BranchID = branchID;
        }

        public int getInterestRate()
        {
            return interestRate;
        }

        public void setAccountID(int accountID)
        {
            AccountID = accountID;
        }

        public int getAccountID()
        {
            return AccountID;
        }

        public int getBranchID()
        {
            return BranchID;
        }

        public string ShowAllDetails()
        {
            string notes = "**********************************************\r\n";
            string details = $"{notes}" +
                $"ClientID : \t\t{getClientID()}\r\n" +
                $"AccountID :  \t\t{getAccountID()}\r\n" +
                $"SavingID :  \t\t{getSavingsID()}\r\n" +
                $"ChequingID :  \t\t{getChequeingID()}\r\n" +
                $"LoanID :  \t\t{getLoanID()}\r\n" +
                $"{notes}" +
                $"BranchID :  \t\t{getBranchID()}\r\n" +
                $"Int't Rate:  \t\t{interestRate}\r\n" +
                $"{notes}" +
                $"Customer:  \t\t{clientName}\r\n" +
                $"Address:  \t\t{homeAddress}\r\n" +
                $"{notes}";
            return details;
        }
    }
}
