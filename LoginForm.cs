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
                this.displayEmployeeTableAdapter.FillByUserAccount(this.dB_BankDataSet1.DisplayEmployee, Int32.Parse(uname), pass);
                this.employeeTableAdapter.FillByUsernameAndPassword(this.dB_BankDataSet.Employee, Int32.Parse(uname), pass);
                this.clientLoginTableAdapter.ClientLogin(this.dB_BankDataSet.ClientLogin, Int32.Parse(uname), pass);

                if (clientLoginDataGridView.Rows.Count > 1)
                {
                    details.Id = Int32.Parse(uname);
                    details.Name = clientLoginDataGridView.Rows[0].Cells[0].Value.ToString();
                    details.Role = clientLoginDataGridView.Rows[0].Cells[1].Value.ToString();
                    details.BranchID = Int32.Parse(clientLoginDataGridView.Rows[0].Cells[2].Value.ToString());

                    ClientFrm clientFrm = new ClientFrm(details);
                    clientFrm.Show();
                    this.Hide();

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

            //testingValues();
        }



        private void testingValues()
        {
            txt_uname.Text = "85";
            txt_password.Text = "new";

            Timer timer = new Timer();
            timer.Interval = 2000;

            timer.Tick += (s, e) =>
            {
                btn_login.PerformClick();
                timer.Stop();
            };

            timer.Start();
        }

        private void txt_password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login.PerformClick();
                e.SuppressKeyPress = false; 
            }
        }
    }
}
