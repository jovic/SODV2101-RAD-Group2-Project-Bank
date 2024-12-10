namespace Bank
{
    public class UserDetails
    {
        // Private field to store the branch ID
        private int _branchID;

        // Public property to get or set the branch ID
        public int BranchID
        {
            get { return _branchID; }
            set { _branchID = value; }
        }

        // Private field to store the user ID
        private int _id;

        // Public property to get or set the user ID
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        // Auto-implemented property for the user's role
        private string _role { get; set; }

        // Public property to get or set the user's role
        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        // Auto-implemented property for the user's name
        private string _name { get; set; }

        // Public property to get or set the user's name
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Default constructor
        public UserDetails() { }
    }
}