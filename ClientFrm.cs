using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Bank
{
    public partial class ClientFrm : Form
    {
        Settings set = new Settings();
        TransferDetails transferDetails;
        UserDetails userDetails;
        Panel[] MainPanels;
        Panel[] InnerPanels;
        Panel[] MainInnerPanels;
        SimpleButton[] navButtons;
        DataGridView[] dgv;
        private string dateToday = DateTime.UtcNow.ToString();
        private int id;
        string password = null;
        public ClientFrm(UserDetails userDetails)
        {
            InitializeComponent();
            setDefaults();
            this.userDetails = userDetails;
            id = userDetails.Id;
        }
        public void setDefaults()
        {
            MainPanels = new Panel[] { pnl_button, pnl_header, pnl_main };
            InnerPanels = new Panel[] { pnl_headerInner, pnl_mainInner };
            MainInnerPanels = new Panel[] { pnl_Home, pnl_savings };
            navButtons = new SimpleButton[] { nav_btn_home, nav_btn_savings, nav_btn_checking, nav_btn_loan, nav_btn_send, nav_btn_settings };
            dgv = new DataGridView[] { savingAccountDataGridView, checkingAccountDataGridView, loanAccountDataGridView };
            set.setDataGridViews(dgv);
            setPanels(MainPanels, 15, Color.WhiteSmoke, new Padding(15));
            setPanels(InnerPanels, 15, Color.White, new Padding(0));
            panel1.Location = new Point(this.Width / 2 - panel1.Size.Width / 2, this.Height / 2 - panel1.Size.Height / 2);
            lbl_accountNumber.Text = id.ToString();
        }

        private void ClientFrm_Load(object sender, EventArgs e)
        {
            // Fill the account data based on the provided account ID
            this.accountTableAdapter.FillByAccountID(this.dB_BankDataSet.Account, id);
            this.checkingAccountTableAdapter.FillByAccountID(this.dB_BankDataSet.CheckingAccount, id);
            this.savingAccountTableAdapter.FillByID(this.dB_BankDataSet.SavingAccount, id);
            this.loanAccountTableAdapter.FillByID(this.dB_BankDataSet.LoanAccount, id);

            // Calculate and display totals for savings, checking, and loan accounts
            Totals(lbl_savingsTotal, savingAccountDataGridView, 1);
            Totals(lbl_CheckingTotal, checkingAccountDataGridView, 2);
            Totals(lbl_loanTotal, loanAccountDataGridView, 2);

            // Store the password from the text edit control
            password = passwordTextEdit.Text;

            // Determine the current time of day and set the greeting message
            string str = DateTime.Now.ToString("tt");
            if (str == "AM")
                lbl_greetings.Text = "Good Morning!";
            else
                lbl_greetings.Text = "Good Evening!";

            // Display user details
            lbl_name.Text = userDetails.Name;
            lbl_address.Text = userDetails.Role;
            lbl_accountNumber.Text = userDetails.Id.ToString();
        }

        private void setPanels(Panel[] pnl, int radius, Color color, Padding pad)
        {
            // Iterate through each panel in the array
            for (int i = 0; i < pnl.Length; i++)
            {
                // Set the rounded corners for the panel
                set.setElipse(pnl[i], radius);

                // Set the background color of the panel
                pnl[i].BackColor = color;

                // Set the padding for the panel
                pnl[i].Padding = pad;
            }
        }

        private void savingAccountBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the current data in the form
            this.Validate();

            // End the editing of the saving account binding source
            this.savingAccountBindingSource.EndEdit();

            // Update all changes in the dataset to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }
        private void checkingbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the current data in the form
            this.Validate();

            // End the editing of the checking account binding source
            this.checkingAccountBindingSource.EndEdit();

            // Update all changes in the dataset to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void accountbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the current data in the form
            this.Validate();

            // End the editing of the account binding source
            this.accountBindingSource.EndEdit();

            // Update all changes in the dataset to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }
        private void setNav(object obj)
        {
            // Iterate through each navigation button and set them to idle state
            for (int i = 0; i < navButtons.Length; i++)
                set.NavButtonIdle_Simple(navButtons[i]);

            // Cast the object to SimpleButton and set it to active state
            SimpleButton btn = (SimpleButton)obj;
            set.NavButtonActive_Simple(btn);
        }

        private void nav_btn_home_Click(object sender, EventArgs e)
        {
            // Set the navigation state for the home button and show the home panel
            setNav(sender);
            set.showPanel(pnl_Home, this, DockStyle.Fill);
        }

        private void nav_btn_savings_Click(object sender, EventArgs e)
        {
            // Set the navigation state for the savings button and show the savings panel
            setNav(sender);
            set.showPanel(pnl_savings, this, DockStyle.Fill);
        }

        private void nav_btn_checking_Click(object sender, EventArgs e)
        {
            // Set the navigation state for the checking button and show the checking panel
            setNav(sender);
            set.showPanel(pnl_checking, this, DockStyle.Fill);
        }

        private void nav_btn_loan_Click(object sender, EventArgs e)
        {
            // Set the navigation state for the loan button and show the loan panel
            setNav(sender);
            set.showPanel(pnl_loan, this, DockStyle.Fill);
        }

        private void nav_btn_send_Click(object sender, EventArgs e)
        {
            // Set the navigation state for the send money button and show the send money panel
            setNav(sender);
            set.showPanel(pnl_sendMoney, this, DockStyle.Fill);
        }

        private void nav_btn_settings_Click(object sender, EventArgs e)
        {
            // Set the navigation state for the settings button and show the settings panel
            setNav(sender);
            set.showPanel(pnl_settings, this, DockStyle.Fill);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            // Close the application
            Application.Exit();
        }

        private void Totals(Label displayLabel, DataGridView dgv, int col)
        {
            int sum = 0;

            // Check if the DataGridView has any rows
            if (dgv.Rows.Count > 0)
            {
                // Iterate through each row and sum the values in the specified column
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    sum += set.DecimaltoInt(dgv.Rows[i].Cells[col].Value.ToString());
                }
            }
            else
            {
                // If there are no rows, set the display label to "0.00"
                displayLabel.Text = "0.00";
                return;
            }

            // Set the display label to the calculated sum
            displayLabel.Text = sum.ToString();
        }

        private void btn_sendMoney_Click(object sender, EventArgs e)
        {
            // Check if the account number, name, and amount fields are not empty
            if (!string.IsNullOrEmpty(txt_sendAccountNumber.Text) && !string.IsNullOrEmpty(txt_sendName.Text) && !string.IsNullOrEmpty(txt_sendAmount.Text))
            {
                // Fill the dataset with customer and account information based on the account number
                this.customerAndAccountTableAdapter.FillByAccountID(this.dB_BankDataSet.CustomerAndAccount, Int32.Parse(lbl_accountNumber.Text));
                transferDetails = new TransferDetails();

                // Check if the send amount is less than the checking total
                if (Int32.Parse(txt_sendAmount.Text) < Int32.Parse(lbl_CheckingTotal.Text))
                {
                    // Check if any customer and account rows were returned
                    if (this.dB_BankDataSet.CustomerAndAccount.Rows.Count > 0)
                    {
                        // Log the column names for debugging purposes
                        foreach (DataColumn column in this.dB_BankDataSet.CustomerAndAccount.Columns)
                        {
                            Console.WriteLine(column.ColumnName);
                        }

                        // Get the first row of the dataset
                        var row = this.dB_BankDataSet.CustomerAndAccount.Rows[0];
                        transferDetails.AccountID = Int32.Parse(row["AccountID"].ToString());
                        transferDetails.Amount = set.DecimaltoInt(row["TOTAL"].ToString());
                        transferDetails.AccountName = row["FullName"].ToString();

                        // Perform the money transfer
                        CheckingAccount(txt_sendAmount.Text.ToString(), "ETranc", Int32.Parse(txt_sendAccountNumber.Text));
                        CheckingAccount($"-{txt_sendAmount.Text.ToString()}", "ETranc", Int32.Parse(lbl_accountNumber.Text));

                        // Show a success message
                        set.showMessageSuccess(this, "Transaction Successful.");

                        // Refresh the checking account data
                        this.checkingAccountTableAdapter.FillByAccountID(dB_BankDataSet.CheckingAccount, Int32.Parse(lbl_accountNumber.Text));

                        // Update the totals displayed
                        Totals(lbl_CheckingTotal, checkingAccountDataGridView, 2);
                    }
                    else
                    {
                        // Show an error message if the account does not exist
                        set.showMessageError(this, $"Account Number : {lbl_accountNumber.Text} does not exist.", "Try Again");
                        return;
                    }
                }
                else
                {
                    // Show an error message if there is not enough balance
                    set.showMessageError(this, $"Not enough balance.", null);
                    return;
                }
            }
        }

        private void searchAccount(string text, Label lbl_senderName, TransferDetails transferDetails)
        {

            
        }


        private void CheckingAccount(string amt, string to, int accountID)
        {
            // Simulate adding a new entry in the checking account
            checkingbindingNavigatorAddItem.PerformClick();

            // Set the account ID in the corresponding input field
            accountIDSpinEdit.Text = accountID.ToString();

            // Set the check number or transaction type
            checkNumbersTextEdit.Text = to;

            // Set the transaction amount
            amountSpinEdit.Text = amt;

            // Set the date of the transaction
            datePostedDateEdit.Text = dateToday;

            // Save the new entry to the checking account
            checkingbindingNavigatorSaveItem.PerformClick();
        }

        private void btn_saveAccount_Click(object sender, EventArgs e)
        {
            // Check if the current password entered matches the stored password
            if (password == txt_currentPassword.Text)
            {
                // Check if the new password and confirm password fields are not empty and match
                if (!string.IsNullOrEmpty(txt_newPasswrod.Text) && !string.IsNullOrEmpty(txt_confirmPassword.Text) && (txt_newPasswrod.Text == txt_confirmPassword.Text))
                {
                    // Set the new password in the password text edit field
                    passwordTextEdit.Text = txt_newPasswrod.Text;

                    // Save the changes using the binding navigator
                    accountbindingNavigatorSaveItem.PerformClick();

                    // Update the stored password variable
                    password = passwordTextEdit.Text;

                    // Show a success message to the user
                    set.showMessageSuccess(this, "Password is successfully saved.");
                }
                else
                {
                    // Show an error message if the new password and confirm password do not match
                    set.showMessageError(this, "New Password and Confirm Password do not match", "OK");
                }
            }
            else
            {
                // Show an error message if the current password does not match
                set.showMessageError(this, "Old Password does not match your current password.", "OK");
            }
        }
    }
}
