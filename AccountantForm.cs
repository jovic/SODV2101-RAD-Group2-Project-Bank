using Bunifu.UI.WinForms;
using DevExpress.Utils.MVVM;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank
{
    public partial class AccountantForm : Form
    {
        //Objects
        Settings set = new Settings();
        Accounts accounts = new Accounts();
        TransferDetails transferDetailsSender, transferDetailsReceiver;
        UserDetails userDetails;

        //Variables
        Panel[] LayoutPanels;
        Panel[] innerPanels;
        Panel[] outerPanels;
        Button[] navButtons;
        DataGridView[] dgv;
        private bool toggle=false;
        private string dateToday = DateTime.UtcNow.ToString();
        private int _accountBranchID, _deposit, _loan;
        private static readonly string apiUrl = "http://api.exchangeratesapi.io/v1/latest?access_key=fd6f67d35cd5a99a5c7d6dafced15bc8";
        public AccountantForm(UserDetails user)
        {
            InitializeComponent();
            userDetails = user;
            setDefaults();
        }

        private void setDefaults()
        {
            // Initialize navigation buttons
            navButtons = new Button[] { nav_dash, nav_open, nav_rates, nav_send, nav_statistics, nav_accounts };

            // Initialize outer panels
            outerPanels = new Panel[] { pnl_left_outer, pnl_top_outer };

            // Initialize layout panels
            LayoutPanels = new Panel[] { pnl_left_inner, pnl_top_inner, pnl_dashboard, pnl_openAccount, pnl_accounts, pnl_sendMoney, pnl_layout, pnl_statistics, pnl_Rates };

            // Initialize inner panels
            innerPanels = new Panel[] { pnl_inner_dashboard, pnl_openAccount_Inner, pnl_accounts_inner, pnl_inner_sendMoney, pnl_inner_receiver, pnl_inner_sender, pnl_inner_statistics, pnl_inner_rates };

            // Initialize data grid views
            dgv = new DataGridView[] { customerDataGridView, savingAccountDataGridView, checkingAccountDataGridView, loanAccountDataGridView, dataGridViewRates };

            // Set main panel to fill the parent container
            pnl_main.Dock = DockStyle.Fill;

            // Set panel properties
            setPanelElipse(LayoutPanels, 15);
            set.PanelLayouts(LayoutPanels, Color.WhiteSmoke);
            set.PanelLayouts(innerPanels, Color.White);
            set.setDataGridViews(dgv);

            // Set greeting message based on the current time
            string str = DateTime.Now.ToString("tt");
            if (str == "AM")
                lbl_greetings.Text = "Good Morning!";
            else
                lbl_greetings.Text = "Good Evening!";

            // Set user details
            lbl_user.Text = userDetails.Name.ToString();
            lbl_position.Text = userDetails.Role.ToString();

            // Clear sender and receiver names
            lbl_senderName.Text = "";
            lbl_receiverName.Text = "";
        }
        private void setPanelElipse(Panel[] pnl, int radius)
        {
            // Loop through each panel in the array
            for (int i = 0; i < pnl.Length; i++)
            {
                // Set the ellipse for the current panel with the specified radius
                set.setElipse(pnl[i], radius);
            }
        }

        private void setNav(object sender)
        {
            // Reset all navigation buttons to idle state
            for (int i = 0; i < navButtons.Length; i++)
                set.NavButtonIdle(navButtons[i]);

            // Check if the sender is not null
            if (sender != null)
            {
                // Cast the sender to a Button
                Button button = (Button)sender;

                // Set the active state for the clicked navigation button
                set.NavButtonActive(sender);
            }
        }


        private void btn_exit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }



        private void AccountantForm_Load(object sender, System.EventArgs e)
        {
            // Simulate a click on the dashboard navigation button
            nav_dash.PerformClick();

            // Fill the Branch table with data based on the user's BranchID
            this.branchTableAdapter.FillByBranchID(this.dB_BankDataSet.Branch, userDetails.BranchID);

            // Fill the Account table with data
            this.accountTableAdapter.Fill(this.dB_BankDataSet.Account);

            // Fill the Customer table with data
            this.customerTableAdapter.Fill(this.dB_BankDataSet.Customer);

            // Convert the deposit and loan values from the spin edit controls to integers
            _deposit = set.DecimaltoInt(depositSpinEdit.Text);
            _loan = set.DecimaltoInt(loanSpinEdit.Text);
        }

        private void bunifuTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            // Update the full name text edit with the text from the customer name textbox
            fullNameTextEdit1.Text = txt_customerName.Text;
        }

        private void txt_homeAddress_KeyUp(object sender, KeyEventArgs e)
        {
            // Update the full name text edit with the text from the home address textbox
            fullNameTextEdit.Text = txt_homeAddress.Text;
        }

        private void savingAccountBindingNavigatorSaveItem_Click(object sender, System.EventArgs e)
        {
            // Validate the current data
            this.Validate();

            // End editing for the saving account binding source
            this.savingAccountBindingSource.EndEdit();

            // Update all changes to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void customerbindingNavigatorSaveItem_Click(object sender, System.EventArgs e)
        {
            // Validate the current data
            this.Validate();

            // End editing for the customer binding source
            this.customerBindingSource.EndEdit();

            // Update all changes to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void accountbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the current data
            this.Validate();

            // End editing for the account binding source
            this.accountBindingSource.EndEdit();

            // Update all changes to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void savingsbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the current data
            this.Validate();

            // End editing for the saving account binding source
            this.savingAccountBindingSource.EndEdit();

            // Update all changes to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void branchbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the current data
            this.Validate();

            // End editing for the branch binding source
            this.branchBindingSource.EndEdit();

            // Update all changes to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void checkingbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the current data
            this.Validate();

            // End editing for the checking account binding source
            this.checkingAccountBindingSource.EndEdit();

            // Update all changes to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void loanbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the current data
            this.Validate();

            // End editing for the loan account binding source
            this.loanAccountBindingSource.EndEdit();

            // Update all changes to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }
        private async void nav_rates_Click(object sender, EventArgs e)
        {
            // Set the navigation for the clicked item
            setNav(sender);

            // Show the rates panel and fill the available space
            set.showPanel(pnl_Rates, this, DockStyle.Fill);

            try
            {
                // Asynchronously retrieve the exchange rates
                DataTable ratesTable = await GetExchangeRatesAsync();

                // Bind the retrieved data to the DataGridView
                dataGridViewRates.DataSource = ratesTable;
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void nav_statistics_Click(object sender, EventArgs e)
        {
            // Set the navigation for the clicked item
            setNav(sender);

            // Show the statistics panel and fill the available space
            set.showPanel(pnl_statistics, this, DockStyle.Fill);
        }

        private void btn_cancelAccount_Click(object sender, System.EventArgs e)
        {
            // Enable navigation buttons
            set.NavButtonEnabler(navButtons, true);

            // Perform the delete action on the customer binding navigator
            customerbindingNavigatorDeleteItem.PerformClick();

            // Send the open account panel to the back
            pnl_openAccount.SendToBack();

            // Clear the customer name and home address text fields
            txt_customerName.Text = "";
            txt_homeAddress.Text = "";
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            // Trigger the cancel account button click event
            btn_cancelAccount.PerformClick();
        }

        private void txt_searchClient_KeyUp(object sender, KeyEventArgs e)
        {
            // Fill the customer table based on the search input
            this.customerTableAdapter.FillByFullName(dB_BankDataSet.Customer, txt_searchClient.Text);
        }

        private void txt_searchName_Click(object sender, EventArgs e)
        {
            // Show the search client panel without docking
            set.showPanel(pnl_searchclient, this, DockStyle.None);
        }

        private void btn_openAccount_Click(object sender, System.EventArgs e)
        {
            // Check if customer name and home address fields are filled
            if (!string.IsNullOrEmpty(txt_customerName.Text) && !string.IsNullOrEmpty(txt_homeAddress.Text))
            {
                // Perform the save action on the customer binding navigator
                customerbindingNavigatorSaveItem.PerformClick();

                // Enable navigation buttons
                set.NavButtonEnabler(navButtons, true);

                // Show a success message
                set.showMessageSuccess(this, "Opening bank account is successful.");

                // Set the search name to the customer name
                txt_searchName.Text = txt_customerName.Text;

                // Set the client ID for accounts
                accounts.setClientID(Int32.Parse(customerIDSpinEdit.Text));

                // Send the open account panel to the back
                pnl_openAccount.SendToBack();

                // Clear the customer name and home address text fields
                txt_customerName.Text = "";
                txt_homeAddress.Text = "";

                // Trigger the accounts navigation click event
                nav_accounts.PerformClick();
            }
            else
            {
                // Show an error message if fields are not filled
                set.showMessageError(this, "Please fill all the fields.", "OK");
            }
        }

        private void btn_createAccount_Click(object sender, EventArgs e)
        {
            // Get the number of rows in each account type DataGridView
            int savingsList = savingAccountDataGridView.RowCount;
            int checkList = checkingAccountDataGridView.RowCount;
            int loanList = loanAccountDataGridView.RowCount;

            // Check if the amount field is filled and an account type is selected
            if (!string.IsNullOrEmpty(txt_amount.Text) && cbo_accountType.Text != "")
            {
                int i = cbo_accountType.SelectedIndex; // Get the selected index of the account type

                // Create a new account if no account ID is set
                if (accounts.getAccountID() == 0)
                    createAccount();

                // Handle account creation based on the selected account type
                if (i == 0) // Savings Account
                {
                    if (savingsList > 0)
                        SavingsAccount(txt_amount.Text, "Deposit"); // Deposit for existing savings account
                    else
                        SavingsAccount(txt_amount.Text, "Int. Deposit"); // Initial deposit for new savings account
                }
                else if (i == 1) // Checking Account
                {
                    CheckingAccount(txt_amount.Text, "Deposit", accounts.getAccountID()); // Deposit for checking account
                }
                else if (i == 2) // Loan Account
                {
                    if (loanList > 0)
                        LoanAccount(txt_amount.Text, "Payment"); // Payment for existing loan account
                    else
                        LoanAccount(txt_amount.Text, "APPR Amount"); // Initial amount for new loan account
                }

                // Send the account selection panel to the back
                pnl_selectAccount.SendToBack();
            }
            else
            {
                // Show an error message if fields are not filled
                set.showMessageError(this, "Please fill all the fields.", "OK");
            }
        }

        private void LoanAccount(string amt, string to)
        {
            // Generate a unique OR number based on the loan ID and current date/time
            string orno = $"{accounts.getLoanID()}{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}{DateTime.Today.Hour}{DateTime.Today.Minute}{DateTime.Today.Second}";

            // Initiate the addition of a new loan entry
            loanbindingNavigatorAddItem.PerformClick();

            // Set the fields for the loan entry
            accountIDSpinEdit3.Text = accounts.getAccountID().ToString(); // Set account ID
            branchIDSpinEdit2.Text = userDetails.BranchID.ToString(); // Set branch ID
            datePostedDateEdit2.Text = dateToday; // Set the date posted
            tOTextEdit.Text = to; // Set the transaction type
            amountSpinEdit2.Text = amt; // Set the amount
            oRNumTextEdit.Text = orno; // Set the OR number

            // Save the loan entry
            loanbindingNavigatorSaveItem.PerformClick();
        }

        private void CheckingAccount(string amt, string to, int accountID)
        {
            // Initiate the addition of a new checking account entry
            checkingbindingNavigatorAddItem.PerformClick();

            // Set the fields for the checking account entry
            accountIDSpinEdit2.Text = accountID.ToString(); // Set account ID
            checkNumbersTextEdit.Text = to; // Set the check number or transaction type
            amountSpinEdit1.Text = amt; // Set the amount
            datePostedDateEdit1.Text = dateToday; // Set the date posted

            // Save the checking account entry
            checkingbindingNavigatorSaveItem.PerformClick();
        }

        private void SavingsAccount(string amt, string to)
        {
            // Initiate the addition of a new savings account entry
            savingsbindingNavigatorAddItem.PerformClick();

            // Set the fields for the savings account entry
            accountIDSpinEdit1.Text = accounts.getAccountID().ToString(); // Set account ID
            amountSpinEdit.Text = amt.ToString(); // Set the amount
            datePostedDateEdit.Text = dateToday; // Set the date posted
            interestRateTextEdit.Text = to; // Set the interest rate or transaction type

            // Update the total deposit amount
            _deposit += set.DecimaltoInt(amt);

            // Save the savings account entry
            savingsbindingNavigatorSaveItem.PerformClick();

            // Update the bank information
            updateBank();
        }

        private void createAccount()
        {
            // Get the selected account type index and adjust for 1-based indexing
            int selectedType = cbo_accountType.SelectedIndex + 1;
            MessageBox.Show(selectedType.ToString()); // Display the selected account type for debugging

            // Initiate the addition of a new account entry
            accountbindingNavigatorAddItem.PerformClick();

            // Set the fields for the new account entry
            customerIDSpinEdit1.Text = accounts.getClientID().ToString(); // Set customer ID
            accountTypeSpinEdit.Text = selectedType.ToString(); // Set account type
            dateModefiedDateEdit.Text = dateToday.ToString(); // Set the date modified
            branchIDSpinEdit.Text = userDetails.BranchID.ToString(); // Set branch ID
            cardPINSpinEdit.Text = "0000"; // Default card PIN
            passwordTextEdit.Text = "0000"; // Default password

            // Save the new account entry
            accountbindingNavigatorSaveItem.PerformClick();

            // Set the account ID for the newly created account
            accounts.setAccountID(Int32.Parse(accountIDSpinEdit.Text));
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            // Clear the search client text field and send the search panel to the back
            txt_searchClient.Text = "";
            pnl_searchclient.SendToBack();
        }

        private void btn_closeSearchclient_Click(object sender, EventArgs e)
        {
            // Trigger the cancel button click event
            btn_cancel.PerformClick();
        }

        private void info_Click(object sender, EventArgs e)
        {
            // Display all account details in a message box
            MessageBox.Show(accounts.ShowAllDetails());
        }

        private void btn_selectClient_Click(object sender, EventArgs e)
        {
            // Check if a client is selected
            if (accounts.getClientID() != 0)
            {
                // Set the search name text field to the selected client's name
                txt_searchName.Text = accounts.getClientName().ToString();
                pnl_searchclient.SendToBack(); // Send the search panel to the back
                txt_searchClient.Text = ""; // Clear the search client text field

                // Fill the account table with accounts associated with the selected client ID
                this.accountTableAdapter.FillByCustomerID(this.dB_BankDataSet.Account, accounts.getClientID());

                // Load accounts based on the selected account ID or default to "0"
                if (!string.IsNullOrEmpty(accountIDSpinEdit.Text))
                    loadAccounts(accountIDSpinEdit.Text);
                else
                    loadAccounts("0");
            }
            else
            {
                // Show an error message if no client is selected
                set.showMessageError(this, "Please select a client.", null);
            }
        }

        private void loadAccounts(string text)
        {
            // Set the account ID based on the provided text
            accounts.setAccountID(Int32.Parse(text));

            // Fill the respective account tables based on the account ID
            this.savingAccountTableAdapter.FillByID(this.dB_BankDataSet.SavingAccount, accounts.getAccountID());
            this.checkingAccountTableAdapter.FillByAccountID(this.dB_BankDataSet.CheckingAccount, accounts.getAccountID());
            this.loanAccountTableAdapter.FillByID(this.dB_BankDataSet.LoanAccount, accounts.getAccountID());
        }

        private void customerDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the index of the selected row
            int selectedrowindex = customerDataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = customerDataGridView.Rows[selectedrowindex];

            // Set the client ID, name, and home address based on the selected row's data
            accounts.setClientID(Int32.Parse(selectedRow.Cells[0].Value.ToString())); // Client ID
            accounts.setClientName(selectedRow.Cells[1].Value.ToString()); // Client Name
            accounts.sethomeAddress(selectedRow.Cells[2].Value.ToString()); // Home Address
        }

        private void btn_selectAccount_Click(object sender, EventArgs e)
        {
            // Check if a client is selected before showing the account selection panel
            if (accounts.getClientID() != 0)
                set.showPanel(pnl_selectAccount, this, DockStyle.None); // Show the account selection panel
            else
                set.showMessageError(this, "Please select a client.", null); // Show error if no client is selected
        }

        private void checkingAccountDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Prompt the user for confirmation before deleting an item
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this item?\r\nDo you want to proceed?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            // If the user confirms, perform the delete action
            if (dr == DialogResult.Yes)
            {
                checkingbindingNavigatorDeleteItem.PerformClick(); // Delete the selected item
                set.showMessageSuccess(this, "Transaction successfully deleted."); // Show success message
            }
        }

        private void loanAccountDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Prompt the user for confirmation before deleting a loan account entry
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this item?\r\nDo you want to proceed?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            // If the user confirms the deletion
            if (dr == DialogResult.Yes)
            {
                loanbindingNavigatorDeleteItem.PerformClick(); // Perform the delete action
                _loan += set.DecimaltoInt(amountSpinEdit2.Text); // Update the total loan amount
                updateBank(); // Update the bank's financial records
                set.showMessageSuccess(this, "Transaction successfully deleted."); // Show success message
            }
        }

        private void btn_searchSender_Click(object sender, EventArgs e)
        {
            // Search for the sender's account using the provided account number and update the UI with the sender's details
            searchAccount(txt_senderAccountNumber.Text, lbl_senderName, transferDetailsSender = new TransferDetails());
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btn_searchReciever_Click(object sender, EventArgs e)
        {
            // Search for the receiver's account using the provided account number and update the UI with the receiver's details
            searchAccount(txt_receiverAccountNumber.Text, lbl_receiverName, transferDetailsReceiver = new TransferDetails());
        }

        private void btn_sendMoney_Click(object sender, EventArgs e)
        {
            // Check if the send amount is valid and not zero
            if (!string.IsNullOrEmpty(txt_sendAmount.Text) && txt_sendAmount.Text != "0")
            {
                // Check if the send amount is less than or equal to the sender's available amount
                if (Int32.Parse(txt_sendAmount.Text) <= transferDetailsSender.Amount)
                {
                    // Perform the transaction: deduct from sender and add to receiver
                    CheckingAccount(txt_sendAmount.Text.ToString(), "ETranc", transferDetailsReceiver.AccountID); // Credit to receiver
                    CheckingAccount($"-{txt_sendAmount.Text.ToString()}", "ETranc", transferDetailsSender.AccountID); // Debit from sender

                    // Show success message upon completion of the transaction
                    set.showMessageSuccess(this, "Transaction complete.");
                }
                else
                {
                    // Show error if the account balance is insufficient
                    set.showMessageError(this, "Account balance is not enough.", null);
                }
            }
            else
            {
                // Show error if the send amount is not entered
                set.showMessageError(this, "Please enter amount to send.", "OK");
            }
        }



        private void searchAccount(string text, Label lbl_senderName, TransferDetails transferDetails)
        {
            // Fill the dataset with account information based on the provided account ID
            this.customerAndAccountTableAdapter.FillByAccountID(this.dB_BankDataSet.CustomerAndAccount, Int32.Parse(text));

            // Check if any rows were returned from the dataset
            if (this.dB_BankDataSet.CustomerAndAccount.Rows.Count > 0)
            {
                // Optional: Print the column names for debugging purposes
                /*foreach (DataColumn column in this.dB_BankDataSet.CustomerAndAccount.Columns)
                {
                    Console.WriteLine(column.ColumnName);
                }
                */
                // Get the first row of the dataset
                var row = this.dB_BankDataSet.CustomerAndAccount.Rows[0];

                // Populate the transfer details with the retrieved account information
                transferDetails.AccountID = Int32.Parse(row["AccountID"].ToString()); // Set Account ID
                transferDetails.Amount = set.DecimaltoInt(row["TOTAL"].ToString()); // Set Account Total
                transferDetails.AccountName = row["FullName"].ToString(); // Set Account Name

                // Update the label with the account name
                lbl_senderName.Text = transferDetails.AccountName;
            }
            else
            {
                // Show an error message if the account number does not exist
                set.showMessageError(this, $"Account Number : {text} does not exist.", "Try Again");
            }
        }

        private void btn_toggle_Click(object sender, EventArgs e)
        {
            // Start the toggle timer when the button is clicked
            toggleTimer.Start();
        }

        private void toggleTimer_Tick(object sender, EventArgs e)
        {
            // Check if the panel is currently collapsed or expanded
            if (!toggle)
            {
                // If the panel is wider than its minimum size, reduce its width
                if (pnl_left_outer.Width > pnl_left_outer.MinimumSize.Width)
                    pnl_left_outer.Width -= 10; // Decrease width by 10 pixels
                else
                {
                    // Stop the timer and set toggle to true when the minimum width is reached
                    toggleTimer.Stop();
                    toggle = true; // Panel is now collapsed
                }
            }
            else
            {
                // If the panel is narrower than its maximum size, increase its width
                if (pnl_left_outer.Width < pnl_left_outer.MaximumSize.Width)
                    pnl_left_outer.Width += 10; // Increase width by 10 pixels
                else
                {
                    // Stop the timer and set toggle to false when the maximum width is reached
                    toggleTimer.Stop();
                    toggle = false; // Panel is now expanded
                }
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            // Restart the application when the logout button is clicked
            Application.Restart();
        }

        private void savingAccountDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Show a confirmation dialog when a cell is double-clicked
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this item?\r\nDo you want to proceed?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            // If the user confirms the deletion
            if (dr == DialogResult.Yes)
            {
                // Deduct the amount from the deposit
                _deposit -= set.DecimaltoInt(amountSpinEdit.Text);

                // Update the bank data
                updateBank();

                // Perform the delete action on the binding navigator
                savingsbindingNavigatorDeleteItem.PerformClick();

                // Show a success message after deletion
                set.showMessageSuccess(this, "Transaction successfully deleted.");
            }
        }

        private void updateBank()
        {
            // Update the UI elements with the current deposit and loan values
            depositSpinEdit.Text = _deposit.ToString();
            loanSpinEdit.Text = _loan.ToString();

            // Save the changes using the binding navigator
            branchbindingNavigatorSaveItem.PerformClick();
        }

        private async Task<DataTable> GetExchangeRatesAsync()
        {
            // Create a new HttpClient instance to make the API request
            using (HttpClient client = new HttpClient())
            {
                // Send a GET request to the specified API URL
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Ensure the response indicates success (status code 200-299)
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the JSON response into a JObject
                JObject json = JObject.Parse(responseBody);

                // Extract the "rates" object from the JSON
                var rates = json["rates"] as JObject;

                // Get the CAD rate from the rates
                var cadRate = rates["CAD"]?.Value<decimal>();

                // Throw an exception if the CAD rate is not found
                if (cadRate == null)
                {
                    throw new Exception("CAD rate not found in the API response.");
                }

                // Create a DataTable to hold the currency rates
                DataTable ratesTable = new DataTable();
                ratesTable.Columns.Add("Currency"); // Column for currency code
                ratesTable.Columns.Add("Rate");     // Column for adjusted rate

                // Iterate through each property in the rates JObject
                foreach (var rate in rates.Properties())
                {
                    string currency = rate.Name; // Get the currency code
                    decimal rateValue = rate.Value.Value<decimal>(); // Get the rate value

                    // Adjust the rate based on the CAD rate
                    decimal adjustedRate = rateValue / cadRate.Value;

                    // Add a new row to the DataTable with the currency and adjusted rate
                    ratesTable.Rows.Add(currency, adjustedRate.ToString("F6")); // Format rate to 6 decimal places
                }

                // Return the populated DataTable
                return ratesTable;
            }
        }
    }
