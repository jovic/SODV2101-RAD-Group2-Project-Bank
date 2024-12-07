using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bank
{
    public partial class LoginForm : Form
    {
        Settings set = new Settings();
        Accounts accounts = new Accounts();
        UserDetails details = new UserDetails();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            // Exit the application when the cancel button is clicked
            Application.Exit();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            // Get username and password from text boxes
            string uname = txt_uname.Text;
            string pass = txt_password.Text;

            // Check if username and password are not empty
            if (!string.IsNullOrEmpty(uname) && !string.IsNullOrEmpty(pass))
            {
                // Fill the employee and client login datasets based on username and password
                this.displayEmployeeTableAdapter.FillByUserAccount(this.dB_BankDataSet1.DisplayEmployee, Int32.Parse(uname), pass);
                this.employeeTableAdapter.FillByUsernameAndPassword(this.dB_BankDataSet.Employee, Int32.Parse(uname), pass);
                this.clientLoginTableAdapter.ClientLogin(this.dB_BankDataSet.ClientLogin, Int32.Parse(uname), pass);

                // Check if client login was successful
                if (clientLoginDataGridView.Rows.Count > 1)
                {
                    // Set details for the client
                    details.Id = Int32.Parse(uname);
                    details.Name = clientLoginDataGridView.Rows[0].Cells[0].Value.ToString();
                    details.Role = clientLoginDataGridView.Rows[0].Cells[1].Value.ToString();
                    details.BranchID = Int32.Parse(clientLoginDataGridView.Rows[0].Cells[2].Value.ToString());

                    // Show the client form and hide the login form
                    ClientFrm clientFrm = new ClientFrm(details);
                    clientFrm.Show();
                    this.Hide();
                }
                // Check if employee login was successful
                else if (employeeDataGridView.Rows.Count > 1)
                {
                    // Set details for the employee
                    details.Id = Int32.Parse(uname);
                    details.Name = employeeDataGridView.Rows[0].Cells[1].Value.ToString();
                    details.Role = employeeDataGridView.Rows[0].Cells[2].Value.ToString();
                    details.BranchID = Int32.Parse(employeeDataGridView1.Rows[0].Cells[1].Value.ToString());

                    // Determine which form to show based on employee role
                    if (Int32.Parse(employeeDataGridView1.Rows[0].Cells[2].Value.ToString()) > 3)
                    {
                        // Show Accountant form for employees with role ID greater than 3
                        AccountantForm frm_Main = new AccountantForm(details);
                        frm_Main.Show();
                        this.Hide();
                    }
                    else
                    {
                        // Show Admin form for employees with role ID 3 or less
                        AdminForm frm_Main = new AdminForm(details);
                        frm_Main.Show();
                        this.Hide();
                    }
                }
                else
                    // Show error message if login fails
                    set.showMessageError(this, "Incorrect Username or Password.", "OK");
            }
            else
                // Show error message if username or password is invalid
                set.showMessageError(this, "Invalid Username or Password.", "OK");
        }

        private void employeeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate and save changes to the employee dataset
            this.Validate();
            this.employeeBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

            //testingValues();
        }


        private void testingValues()
        {
            // Set test values for username and password
            txt_uname.Text = "85";
            txt_password.Text = "new";

            // Create a timer to delay the login attempt
            Timer timer = new Timer();
            timer.Interval = 2000; // Set the timer interval to 2000 milliseconds (2 seconds)

            // Define the action to perform when the timer ticks
            timer.Tick += (s, e) =>
            {
                // Simulate a button click on the login button
                btn_login.PerformClick();
                timer.Stop(); // Stop the timer after the click
            };

            timer.Start(); // Start the timer
        }

        private void txt_password_KeyUp(object sender, KeyEventArgs e)
        {
            // Check if the Enter key was pressed
            if (e.KeyCode == Keys.Enter)
            {
                // Simulate a button click on the login button
                btn_login.PerformClick();
                e.SuppressKeyPress = false; // Allow the Enter key press to be processed
            }
        }
    }
}
