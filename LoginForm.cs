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
            Application.Exit();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string uname = txt_uname.Text;
            string pass = txt_password.Text;

            if (!string.IsNullOrEmpty(uname) && !string.IsNullOrEmpty(pass))
            {
                this.accountTableAdapter.FillByUsernameAndPassword(this.dB_BankDataSet.Account, Int32.Parse(uname), pass);
                this.displayEmployeeTableAdapter.FillByUserAccount(this.dB_BankDataSet1.DisplayEmployee, Int32.Parse(uname), pass);
                this.employeeTableAdapter.FillByUsernameAndPassword(this.dB_BankDataSet.Employee, Int32.Parse(uname), pass);

                if (accountDataGridView.Rows.Count > 1)
                {
                    set.showMessageSuccess(this, $"Login as Client");
                    //Client to Do

                }
                else if (employeeDataGridView.Rows.Count > 1)
                {
                    details.Id = Int32.Parse(uname);
                    details.Name = employeeDataGridView.Rows[0].Cells[1].Value.ToString();
                    details.Role = employeeDataGridView.Rows[0].Cells[2].Value.ToString();
                    details.BranchID = Int32.Parse(employeeDataGridView1.Rows[0].Cells[1].Value.ToString());

                    if (Int32.Parse(employeeDataGridView1.Rows[0].Cells[2].Value.ToString()) > 3)
                    {
                        AccountantForm frm_Main = new AccountantForm(details);
                        //showForms(frm_Main);
                        frm_Main.Show();
                        this.Hide();
                    }
                    else
                    {
                        AdminForm frm_Main = new AdminForm(details);
                        frm_Main.Show();
                        this.Hide();
                    }
                }
                else
                    set.showMessageError(this, "Incorrect Username or Password.", "OK");
            }
            else
                set.showMessageError(this, "Invalid Username or Password.", "OK");

        }

        private void employeeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.employeeBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

            testingValues();
        }



        private void testingValues()
        {
            txt_uname.Text = "4";
            txt_password.Text = "mypassword";

            Timer timer = new Timer();
            timer.Interval = 2000;

            timer.Tick += (s, e) =>
            {
                btn_login.PerformClick();
                timer.Stop();
            };

            timer.Start();
        }

        public void showForms(Form form)
        {

            // Create the splash screen form
            using (var splashScreen = new SplashScreen())
            {
                splashScreen.TopMost = true;
                splashScreen.Show();

                // Set up the BackgroundWorker to load resources
                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += (s, e) =>
                {
                    // Simulate resource loading
                    System.Threading.Thread.Sleep(3000);  // Simulating a delay
                };

                backgroundWorker.RunWorkerCompleted += (s, e) =>
                {
                    splashScreen.Close(); // Close splash screen after loading
                };

                // Start the loading process
                backgroundWorker.RunWorkerAsync();

                form.Show();  // Replace MainForm with your main form
            }
        }

    }
}
