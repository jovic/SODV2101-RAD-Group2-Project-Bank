using Bunifu.UI.WinForms;
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
            navButtons = new Button[] { nav_dash, nav_open, nav_rates, nav_send, nav_statistics,nav_accounts};
            outerPanels = new Panel[] { pnl_left_outer, pnl_top_outer};
            LayoutPanels = new Panel[] { pnl_left_inner, pnl_top_inner, pnl_dashboard,pnl_openAccount, pnl_accounts, pnl_sendMoney,pnl_layout, pnl_statistics,pnl_Rates };
            innerPanels = new Panel[] { pnl_inner_dashboard,pnl_openAccount_Inner, pnl_accounts_inner,pnl_inner_sendMoney,pnl_inner_receiver,pnl_inner_sender,pnl_inner_statistics,pnl_inner_rates };
            dgv = new DataGridView[] { customerDataGridView, savingAccountDataGridView,checkingAccountDataGridView,loanAccountDataGridView, dataGridViewRates };
            
            pnl_main.Dock = DockStyle.Fill;
            setPanelElipse(LayoutPanels, 15);
            set.PanelLayouts(LayoutPanels, Color.WhiteSmoke);
            set.PanelLayouts(innerPanels, Color.White);
            set.setDataGridViews(dgv);
            
            


            string str = DateTime.Now.ToString("tt");
            if (str == "AM")
                lbl_greetings.Text = "Good Morning!";
            else
                lbl_greetings.Text = "Good Evening!";

            lbl_user.Text = userDetails.Name.ToString();
            lbl_position.Text = userDetails.Role.ToString();

            
        }

        private void setPanelElipse(Panel[] pnl, int radius)
        {
            for (int i = 0; i < pnl.Length; i++)
            {
                set.setElipse(pnl[i],radius);
            }
        }
        private void setNav(object sender)
        {
            for(int i=0; i< navButtons.Length; i++)
                set.NavButtonIdle(navButtons[i]);

            if(sender != null)
            {
                Button button = (Button)sender;
                set.NavButtonActive(sender);
            }
            
        }

       
        private void btn_exit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

       

        private void AccountantForm_Load(object sender, System.EventArgs e)
        {
            nav_dash.PerformClick();
            // TODO: This line of code loads data into the 'dB_BankDataSet.Branch' table. You can move, or remove it, as needed.
            this.branchTableAdapter.FillByBranchID(this.dB_BankDataSet.Branch, userDetails.BranchID);
            // TODO: This line of code loads data into the 'dB_BankDataSet.Account' table. You can move, or remove it, as needed.
            this.accountTableAdapter.Fill(this.dB_BankDataSet.Account);
            // TODO: This line of code loads data into the 'dB_BankDataSet.Customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter.Fill(this.dB_BankDataSet.Customer);
            // TODO: This line of code loads data into the 'dB_BankDataSet.CheckingAccount' table. You can move, or remove it, as needed.
            //this.checkingAccountTableAdapter.Fill(this.dB_BankDataSet.CheckingAccount);
            // TODO: This line of code loads data into the 'dB_BankDataSet.SavingAccount' table. You can move, or remove it, as needed.
            //this.savingAccountTableAdapter.Fill(this.dB_BankDataSet.SavingAccount);
            _deposit = set.DecimaltoInt(depositSpinEdit.Text);
            _loan = set.DecimaltoInt(loanSpinEdit.Text);


        }



        private void bunifuTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            fullNameTextEdit1.Text = txt_customerName.Text;
        }
        private void txt_homeAddress_KeyUp(object sender, KeyEventArgs e)
        {
            fullNameTextEdit.Text = txt_homeAddress.Text;
        }

        private void savingAccountBindingNavigatorSaveItem_Click(object sender, System.EventArgs e)
        {
            this.Validate();
            this.savingAccountBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);

        }
        private void customerbindingNavigatorSaveItem_Click(object sender, System.EventArgs e)
        {
            this.Validate();
            this.customerBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }
        private void accountbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.accountBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void savingsbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.savingAccountBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void branchbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.branchBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void checkingbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.checkingAccountBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void loanbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.loanAccountBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void nav_open_Click(object sender, System.EventArgs e)
        {
            setNav(sender);
            set.showPanel(pnl_openAccount,this, DockStyle.None);
            set.NavButtonEnabler(navButtons, false);
            customerbindingNavigatorAddItem.PerformClick();
        }

        private void nav_accounts_Click(object sender, EventArgs e)
        {
            setNav(sender);
            set.showPanel(pnl_accounts, this, DockStyle.Fill);
        }
        private void nav_dash_Click(object sender, System.EventArgs e)
        {
            setNav(sender);
            set.showPanel(pnl_dashboard, this, DockStyle.Fill);
        }

        private void nav_send_Click(object sender, EventArgs e)
        {
            setNav(sender);
            set.showPanel(pnl_sendMoney, this, DockStyle.Fill);
            pnl_layout.Location = new Point(pnl_inner_sendMoney.Width / 2 - pnl_layout.Size.Width / 2, pnl_inner_sendMoney.Height / 2 - pnl_layout.Size.Height / 2);
        }

        private async void nav_rates_Click(object sender, EventArgs e)
        {
            setNav(sender);
            set.showPanel(pnl_Rates, this, DockStyle.Fill);

            try
            {
                DataTable ratesTable = await GetExchangeRatesAsync();
                dataGridViewRates.DataSource = ratesTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void nav_statistics_Click(object sender, EventArgs e)
        {
            setNav(sender);
            set.showPanel(pnl_statistics, this, DockStyle.Fill);
        }

        private void btn_cancelAccount_Click(object sender, System.EventArgs e)
        {
            set.NavButtonEnabler(navButtons, true);
            customerbindingNavigatorDeleteItem.PerformClick();
            pnl_openAccount.SendToBack();
            txt_customerName.Text = "";
            txt_homeAddress.Text = "";
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            btn_cancelAccount.PerformClick();
        }

        private void txt_searchClient_KeyUp(object sender, KeyEventArgs e)
        {
            this.customerTableAdapter.FillByFullName(dB_BankDataSet.Customer, txt_searchClient.Text);
        }

        private void txt_searchName_Click(object sender, EventArgs e)
        {
            set.showPanel(pnl_searchclient, this, DockStyle.None);
        }

        private void btn_openAccount_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_customerName.Text) && !string.IsNullOrEmpty(txt_homeAddress.Text))
            {
                customerbindingNavigatorSaveItem.PerformClick();
                set.NavButtonEnabler(navButtons, true);
                set.showMessageSuccess(this,"Opening bank account is successfull.");
                txt_searchName.Text = txt_customerName.Text;

                accounts.setClientID(Int32.Parse(customerIDSpinEdit.Text));
                pnl_openAccount.SendToBack();
                txt_customerName.Text = "";
                txt_homeAddress.Text = "";

                nav_accounts.PerformClick();
            }
            else
                set.showMessageError(this, "Please fill all the fields.", "OK");
        }

        
        

        private void btn_createAccount_Click(object sender, EventArgs e)
        {
            int savingsList = savingAccountDataGridView.RowCount;
            int checkList = checkingAccountDataGridView.RowCount;
            int loanList = loanAccountDataGridView.RowCount;

            if(!string.IsNullOrEmpty(txt_amount.Text) && cbo_accountType.Text != "")
            {
                int i = cbo_accountType.SelectedIndex;
                if (accounts.getAccountID() != 0)
                    creeateAccount();

                if (i == 0)
                {
                    if (savingsList > 0)
                        SavingsAccount(txt_amount.Text, "Deposit");
                    else
                        SavingsAccount(txt_amount.Text, "Int. Deposit");
                }
                else if (i == 1)
                    CheckingAccount(txt_amount.Text, "Deposit", accounts.getAccountID());
                else if (i == 2)
                {
                    if(loanList > 0)
                        LoanAccount(txt_amount.Text,"Payment");
                    else
                        LoanAccount(txt_amount.Text, "APPR Amount");
                }
                    
            }
            else
                set.showMessageError(this, "Please fill all the fields.", "OK");
        }

        private void LoanAccount(string amt, string to)
        {
            string orno = $"{accounts.getLoanID()}{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}{DateTime.Today.Hour}{DateTime.Today.Minute}{DateTime.Today.Second}";
            loanbindingNavigatorAddItem.PerformClick();
            accountIDSpinEdit3.Text = accounts.getAccountID().ToString();
            branchIDSpinEdit2.Text = accounts.getBranchID().ToString();
            datePostedDateEdit2.Text = dateToday;
            amountSpinEdit2.Text = amt;
            oRNumTextEdit.Text = orno;
            loanbindingNavigatorSaveItem.PerformClick();
        }

        private void CheckingAccount(string amt, string to, int accountID)
        {
            checkingbindingNavigatorAddItem.PerformClick();
            accountIDSpinEdit2.Text = accountID.ToString();
            checkNumbersTextEdit.Text = to;
            amountSpinEdit1.Text = amt;
            datePostedDateEdit1.Text = dateToday;
            checkingbindingNavigatorSaveItem.PerformClick();    
        }

        private void SavingsAccount(string amt, string to)
        {
            savingsbindingNavigatorAddItem.PerformClick();
            accountIDSpinEdit1.Text = accounts.getAccountID().ToString();
            amountSpinEdit.Text = amt.ToString();
            datePostedDateEdit.Text = dateToday;
            interestRateTextEdit.Text = to;
            _deposit += set.DecimaltoInt(amt);
            savingsbindingNavigatorSaveItem.PerformClick();
            updateBank();
        }

        private void creeateAccount()
        {
            accountbindingNavigatorAddItem.PerformClick();
            customerIDSpinEdit1.Text = accounts.getClientID().ToString();
            accountTypeSpinEdit.Text = cbo_accountType.SelectedIndex.ToString();
            dateModefiedDateEdit.Text = dateToday.ToString();
            branchIDSpinEdit.Text = userDetails.BranchID.ToString();
            cardPINSpinEdit.Text = "0000";
            passwordTextEdit.Text = "0000";
            accountbindingNavigatorSaveItem.PerformClick();

            accounts.setAccountID(Int32.Parse(accountIDSpinEdit.Text));
            //set.showMessageSuccess(this, "Opening bank account is successfull.");
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_searchClient.Text = "";
            pnl_searchclient.SendToBack();
        }

        private void btn_closeSearchclient_Click(object sender, EventArgs e)
        {
            btn_cancel.PerformClick();
        }

        private void info_Click(object sender, EventArgs e)
        {
            MessageBox.Show(accounts.ShowAllDetails());
        }

        private void btn_selectClient_Click(object sender, EventArgs e)
        {
            if(accounts.getClientID() != 0)
            {
                txt_searchName.Text = accounts.getClientName().ToString();
                pnl_searchclient.SendToBack();
                txt_searchClient.Text = "";

                this.accountTableAdapter.FillByCustomerID(this.dB_BankDataSet.Account, accounts.getClientID());
                
                if(!string.IsNullOrEmpty(accountIDSpinEdit.Text))
                {
                    loadAccounts(accountIDSpinEdit.Text);  
                }

            }
            else
                set.showMessageError(this, "Please select a client.", null);
        }

        private void loadAccounts(string text)
        {
            accounts.setAccountID(Int32.Parse(text));
            this.savingAccountTableAdapter.FillByID(this.dB_BankDataSet.SavingAccount, accounts.getAccountID());
            this.checkingAccountTableAdapter.FillByAccountID(this.dB_BankDataSet.CheckingAccount, accounts.getAccountID());
            this.loanAccountTableAdapter.FillByID(this.dB_BankDataSet.LoanAccount, accounts.getAccountID());
        }

        private void customerDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedrowindex = customerDataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = customerDataGridView.Rows[selectedrowindex];
            accounts.setClientID(Int32.Parse(selectedRow.Cells[0].Value.ToString()));
            accounts.setClientName(selectedRow.Cells[1].Value.ToString());
            accounts.sethomeAddress(selectedRow.Cells[2].Value.ToString());

        }

        private void btn_selectAccount_Click(object sender, EventArgs e)
        {
            if(accounts.getClientID() != 0)
                set.showPanel(pnl_selectAccount, this, DockStyle.None);
            else
                set.showMessageError(this, "Please select a client.", null);
        }

        private void checkingAccountDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this item?\r\nDo you want to proceed?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                checkingbindingNavigatorDeleteItem.PerformClick();
                set.showMessageSuccess(this, "Transaction successfully deleted.");
            }
        }

        private void loanAccountDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this item?\r\nDo you want to proceed?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                loanbindingNavigatorDeleteItem.PerformClick();
                _loan += set.DecimaltoInt(amountSpinEdit2.Text);
                updateBank();
                set.showMessageSuccess(this, "Transaction successfully deleted.");
            }
        }

        private void btn_searchSender_Click(object sender, EventArgs e)
        {
            searchAccount(txt_senderAccountNumber.Text, lbl_senderName, transferDetailsSender = new TransferDetails());
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btn_searchReciever_Click(object sender, EventArgs e)
        {
            searchAccount(txt_senderAccountNumber.Text, lbl_receiverName, transferDetailsReceiver = new TransferDetails());
        }

        private void btn_sendMoney_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_sendAmount.Text) && txt_sendAmount.Text != "0")
            {
                if (Int32.Parse(txt_sendAmount.Text) <= transferDetailsSender.Amount)
                {
                    CheckingAccount(txt_sendAmount.Text.ToString(), "ETranc", transferDetailsReceiver.AccountID);
                    CheckingAccount($"-{txt_sendAmount.Text.ToString()}", "ETranc", transferDetailsSender.AccountID);
                    set.showMessageSuccess(this, "Transaction complete.");
                }
                else
                    set.showMessageError(this, "Account balance is not enough.", null);

            }
            else
                set.showMessageError(this, "Please enter amount to send.", "OK");
        }

       

        private void searchAccount(string text, Label lbl_senderName, TransferDetails transferDetails)
        {
            
            this.customerAndAccountTableAdapter.FillByAccountID(this.dB_BankDataSet.CustomerAndAccount, Int32.Parse(text));

            if (this.dB_BankDataSet.CustomerAndAccount.Rows.Count > 0)
            {

                foreach (DataColumn column in this.dB_BankDataSet.CustomerAndAccount.Columns)
                {
                    Console.WriteLine(column.ColumnName);
                }
                var row = this.dB_BankDataSet.CustomerAndAccount.Rows[0];
                transferDetails.AccountID = Int32.Parse(row["AccountID"].ToString());
                transferDetails.Amount = set.DecimaltoInt(row["TOTAL"].ToString());
                transferDetails.AccountName = row["FullName"].ToString();

                lbl_senderName.Text = transferDetails.AccountName;
            }
            else
                set.showMessageError(this, $"Account Number : {text} does not exist.", "Try Again");
        }

        private void btn_toggle_Click(object sender, EventArgs e)
        {
            toggleTimer.Start();
        }

        private void toggleTimer_Tick(object sender, EventArgs e)
        {
            if(!toggle)
            {
                if (pnl_left_outer.Width > pnl_left_outer.MinimumSize.Width)
                    pnl_left_outer.Width-=10;
                else
                {
                    toggleTimer.Stop();
                    toggle = true;
                }
            }
            else
            {
                if (pnl_left_outer.Width <pnl_left_outer.MaximumSize.Width)
                    pnl_left_outer.Width += 10;
                else
                {
                    toggleTimer.Stop();
                    toggle = false;
                }
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void savingAccountDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult dr= MessageBox.Show("Are you sure you want to delete this item?\r\nDo you want to proceed?","Warning",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);
           
            if (dr == DialogResult.Yes)
            {
                _deposit -= set.DecimaltoInt(amountSpinEdit.Text);
                updateBank();
                savingsbindingNavigatorDeleteItem.PerformClick();
                set.showMessageSuccess(this, "Transaction successfully deleted.");
            }
        }

        private void updateBank()
        {
            depositSpinEdit.Text = _deposit.ToString();
            loanSpinEdit.Text = _loan.ToString();
            branchbindingNavigatorSaveItem.PerformClick();
        }

        private async Task<DataTable> GetExchangeRatesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                JObject json = JObject.Parse(responseBody);

                var rates = json["rates"] as JObject;

                var cadRate = rates["CAD"]?.Value<decimal>();

                if (cadRate == null)
                {
                    throw new Exception("CAD rate not found in the API response.");
                }

                DataTable ratesTable = new DataTable();
                ratesTable.Columns.Add("Currency");
                ratesTable.Columns.Add("Rate");

                foreach (var rate in rates.Properties())
                {
                    string currency = rate.Name;
                    decimal rateValue = rate.Value.Value<decimal>();

                    decimal adjustedRate = rateValue / cadRate.Value;

                    ratesTable.Rows.Add(currency, adjustedRate.ToString("F6"));
                }

                return ratesTable;
            }
        }

    }
}
