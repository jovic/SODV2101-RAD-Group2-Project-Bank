namespace Bank
{
    public class UserDetails
    {

        private int _branchID;
        public int BranchID
        {
            get { return _branchID; }
            set { _branchID = value; }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _role { get; set; }
        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }
        private string _name { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public UserDetails() { }
    }
}
