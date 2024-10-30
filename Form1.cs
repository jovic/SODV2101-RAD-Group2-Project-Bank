using Bank.DB_BankDataSetTableAdapters;
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
    public partial class frm_Main : Form
    {
        //Classes or Objects
        Settings set = new Settings();
        Accounts accounts = new Accounts();
        UserDetails userDetails = new UserDetails();
        TransferDetails transferDetailsSender;
        TransferDetails transferDetailsReciever;
        Button[] navButtons;
        DataGridView[] dataGridViews;
        Panel[] panels;
        //Variables
        private bool navOnExpand = true;
        private string dateToday = DateTime.UtcNow.ToString();
        private bool isNewCreated = false;
        private static readonly string apiUrl = "http://api.exchangeratesapi.io/v1/latest?access_key=fd6f67d35cd5a99a5c7d6dafced15bc8";
        private int _accountBranchID, _deposit, _loan;
        public frm_Main(UserDetails user)
        {
            InitializeComponent();
            SetDefaults();
            branchTableAdapter1.Fill(dB_BankDataSet1.Branch);
            userDetails = user;
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            btn_nav_Dashboard.PerformClick();
            // TODO: This line of code loads data into the 'dB_BankDataSet.Branch' table. You can move, or remove it, as needed.
            //this.branchTableAdapter.Fill(this.dB_BankDataSet.Branch);
            this.accountTypeTableAdapter.Fill(this.dB_BankDataSet.AccountType);
            this.customerTableAdapter.Fill(this.dB_BankDataSet.Customer);

            string str = DateTime.Now.ToString("tt");
            if (str == "AM")
                lbl_greetings.Text = "Good Morning!";
            else
                lbl_greetings.Text = "Good Evening!";

            lbl_user.Text = userDetails.Name;
            lbl_position.Text = userDetails.Role;
            accounts.setBranchID(userDetails.BranchID);
        }


        private void pb_navigation_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (navOnExpand)
            {
                pnl_left.Width -= 20;
                if (pnl_left.Width <= 90)
                {
                    pnl_left.Width = 90;
                    navOnExpand = false;
                    timer1.Stop();
                }
            }
            else
            {
                pnl_left.Width += 20;
                if (pnl_left.Width >= 270)
                {
                    pnl_left.Width = 271;
                    navOnExpand = true;
                    timer1.Stop();
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_minize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void customerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customerBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);

        }

        private void txt_pnl_NewAccount_CustomerName_KeyUp(object sender, KeyEventArgs e)
        {
            fullNameTextEdit.Text = txt_pnl_NewAccount_CustomerName.Text;
        }

        private void txt_pnl_NewAccount_CompleteAddress_KeyUp(object sender, KeyEventArgs e)
        {
            homeAddressTextEdit.Text = txt_pnl_NewAccount_CompleteAddress.Text;
        }

        private void btn_pnl_NewAccount_Create_Click(object sender, EventArgs e)
        {
            string cn = txt_pnl_NewAccount_CustomerName.Text;
            string ha = txt_pnl_NewAccount_CompleteAddress.Text;

            if (cn.Length != 0 && ha.Length != 0)
            {
                customerBindingNavigatorSaveItem.PerformClick();
                txt_pnl_NewAccount_CustomerName.Text = "";
                txt_pnl_NewAccount_CompleteAddress.Text = "";

                //accounts.setAccountID(Int32.Parse(accountIDSpinEdit.Text));
                pnl_NewAccount.Hide();

                set.showMessageSuccess(this, "Account successfully created.");

                set.showPanel(pnl_Accounts, this, DockStyle.Fill);

                accounts.setClientName(fullNameTextEdit.Text);
                accounts.sethomeAddress(homeAddressTextEdit.Text);

                txt_Name.Text = accounts.getClientName();
                set.NavButtonEnabler(navButtons, true);

                string str = customerIDSpinEdit.Text.ToString();
                if (str.Length > 0)
                {
                    accounts.setClientID(Int32.Parse(str));
                }

            }

        }

        private void btn_pnl_NewAccount_close_Click(object sender, EventArgs e)
        {
            btn_pnl_NewAccount_Cancel.PerformClick();
        }

        private void btn_pnl_NewAccount_Cancel_Click(object sender, EventArgs e)
        {
            isNewCreated = false;
            bindingNavigatorDeleteItem.PerformClick();
            set.NavButtonEnabler(navButtons, true);
            pnl_NewAccount.Hide();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            pnl_NewAccount.Show();
        }


        private void btn_nav_NewAccount_Click(object sender, EventArgs e)
        {
            bindingNavigatorAddNewItem.PerformClick();
            set.showPanel(pnl_NewAccount, this, DockStyle.None);
        }

        public void setNavButtons(Button[] btn, Object obj)
        {
            for (int i = 0; i < btn.Length; i++)
                set.NavButtonIdle(btn[i]);

            if (obj != null)
            {
                set.NavButtonActive(obj);
            }

            if (obj != null)
            {
                Button getButtonText = (Button)obj;
                lbl_header.Text = getButtonText.Text.TrimStart();
            }


        }

        private void btn_nav_Dashboard_Click(object sender, EventArgs e)
        {
            set.showPanel(pnl_dashboard, this, DockStyle.Fill);
            setNavButtons(navButtons, sender);
        }

        private void btn_nav_NewAccount_Click_1(object sender, EventArgs e)
        {
            setNavButtons(navButtons, sender);
            isNewCreated = true;
            bindingNavigatorAddNewItem.PerformClick();
            set.showPanel(pnl_NewAccount, this, DockStyle.None);
            set.NavButtonEnabler(navButtons, false);
        }

        private void btn_nav_Accounts_Click(object sender, EventArgs e)
        {
            setNavButtons(navButtons, sender);
            set.showPanel(pnl_Accounts, this, DockStyle.Fill);
        }

        private async void btn_nav_Rates_Click(object sender, EventArgs e)
        {
            setNavButtons(navButtons, sender);
            try
            {
                DataTable ratesTable = await GetExchangeRatesAsync();
                dataGridViewRates.DataSource = ratesTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            set.showPanel(pnl_rates, this, DockStyle.Fill);
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

        private void btn_nav_statistics_Click(object sender, EventArgs e)
        {
            setNavButtons(navButtons, sender);
            set.showPanel(pnl_statistics, this, DockStyle.Fill);
        }


        private void btn_SelectAccountType_Click(object sender, EventArgs e)
        {
            if (accounts.getClientID() > 1)
            {
                set.showPanel(pnl_AccountType, this, DockStyle.None);
                customerIDSpinEdit1.Text = accounts.getClientID().ToString();
            }
            else
                set.showMessageError(this, "Please a client to create an account.", "OK");
        }

        private void pnl_ChooseAccount_Cancel_Click(object sender, EventArgs e)
        {
            AccountsbindingNavigatorDeleteItem.PerformClick();
            txt_amount.Text = "";
            pnl_AccountType.Hide();

            cbo_accountType.SelectedIndex = 0;
            cbo_accountType.Enabled = true;
        }

        private void pnl_ChooseAccount_CreateAccount_Click(object sender, EventArgs e)
        {
            string amount = txt_amount.Text;
            int accountType = cbo_accountType.SelectedIndex + 1;

            if (amount.Length > 0)
            {
                if (accounts.getAccountID() == 0)
                {
                    AccountsbindingNavigatorAddNewItem.PerformClick();
                    accountTypeSpinEdit.Text = accountType.ToString();
                    customerIDSpinEdit1.Text = accounts.getClientID().ToString();
                    dateModefiedDateEdit.Text = dateToday;
                    branchIDSpinEdit.Text = accounts.getBranchID().ToString();
                    AccountsbindingNavigatorSaveItem.PerformClick();

                    //accounts.setAccountID(Int32.Parse(accountIDSpinEdit.Text));
                }


                if (accountType == 1)
                {
                    if (savingAccountDataGridView.Rows.Count == 0)
                        CreateSavingsAccount(amount, "Init'l Deposit");
                    else
                        CreateSavingsAccount(amount, "Deposit");
                    updateBank();
                }
                else if (accountType == 2)
                {
                    CreateChequingAccount(amount, "Deposit", accounts.getAccountID());
                }

                else if (accountType == 3)
                {
                    if (accounts.getLoanID() == 0)
                        CreateLoanAccount(amount);
                    else
                        MakePayment(amount);
                    updateBank();
                }

                else
                    set.showMessageError(this, "Something went wrong. \nPlease Try Again.", "OK");
            }
            else if (amount.Length == 0)
            {
                set.showMessageError(this, "Please Enter amount.", "");
            }
            else if (cbo_accountType.Text == "")
            {
                set.showMessageError(this, "Please select account type.", "");
            }
            else
            {
                set.showMessageError(this, "Something went wrong. \nPlease try again.", "Ok");
            }

            pnl_AccountType.Hide();
        }

        private void MakePayment(string amount)
        {
            if (accounts.getLoanID() != 0)
            {
                int lastRow = DGV_LoanPaymentList.Rows.Count - 1;
                decimal camount = decimal.Parse(DGV_LoanPaymentList.Rows[lastRow].Cells[1].Value.ToString());
                string strAmount = camount.ToString(".##");
                int sum = Int32.Parse(strAmount) - Int32.Parse(amount);
                string orno = $"{accounts.getLoanID()}{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}{DateTime.Today.Hour}{DateTime.Today.Minute}{DateTime.Today.Second}";
                _loan -= Int32.Parse(amount);

                if (_loan <= 0) _loan = 0;

                InsertLoanPayment(Int32.Parse(amount), orno);
                InsertLoanPayment(sum, "");

                loanIDSpinEdit.Text = accounts.getLoanID().ToString();
                accountIDSpinEdit3.Text = accounts.getAccountID().ToString();
                branchIDSpinEdit1.Text = accounts.getBranchID().ToString();
                amountSpinEdit2.Text = sum.ToString();
                datePostedDateEdit2.Text = dateToday.ToString();
                LoanBindingNavigatorSave.PerformClick();

                set.showMessageSuccess(this, $"Payment amount of {amount} for Loan : {accounts.getLoanID()} was successful.");

                pnl_AccountType.SendToBack();
                cbo_accountType.SelectedIndex = 0;
                cbo_accountType.Enabled = true;
            }
        }

        private void InsertLoanPayment(int amount, string orno)
        {
            loanPaymentsbindingNavigatorAdd.PerformClick();
            loanIDSpinEdit1.Text = accounts.getLoanID().ToString();
            paymentSpinEdit.Text = amount.ToString();
            datePostedDateEdit.Text = dateToday;
            oRNumTextEdit.Text = orno;
            loanPaymentsbindingNavigatorSave.PerformClick();
        }

        private void LoadComboBox(System.Windows.Forms.ComboBox cbo_savingsAccountsList)
        {
            SavingAccountTableAdapter adapter = new SavingAccountTableAdapter();
            adapter.FillBySavingIDGroup(this.dB_BankDataSet.SavingAccount, accounts.getAccountID());
            MessageBox.Show(adapter.ToString());
        }

        private void CreateLoanAccount(string amount)
        {
            LoanBindingNavigatorAdd.PerformClick();
            accountIDSpinEdit3.Text = accounts.getAccountID().ToString();
            branchIDSpinEdit1.Text = accounts.getBranchID().ToString();
            amountSpinEdit2.Text = amount.ToString();
            datePostedDateEdit2.Text = dateToday.ToString();
            LoanBindingNavigatorSave.PerformClick();
            _loan += Int32.Parse(amount);
            //FillDropdownLoanList();
            InsertLoanPayment(Int32.Parse(amount), dateToday);
            set.showMessageSuccess(this, "Creating Loan Account is successful.");
        }

        private void CreateChequingAccount(string amount, string message, int accountID)
        {
            ChequingBindingNavigatorAdd.PerformClick();
            accountIDSpinEdit2.Text = accountID.ToString();
            checkNumbersTextEdit.Text = message.ToString();
            amountSpinEdit1.Text = amount.ToString();
            datePostedDateEdit1.Text = dateToday.ToString();
            ChequingBindingNavigatorSave.PerformClick();

            accounts.SetChequeingID(Int32.Parse(checkIDSpinEdit.Text));
            set.showMessageSuccess(this, "Transaction is successful.");

        }

        private void CreateSavingsAccount(string amount, string str)
        {
            if (str != "Deposit")
            {
                SavingsBindingNavigatorAdd.PerformClick();
                accountIDSpinEdit4.Text = accounts.getAccountID().ToString();
                interestRateLabel2.Text = $"{str}";
                amountSpinEdit3.Text = amount.ToString();
                datePostedDateEdit3.Text = dateToday;
                SavingsBindingNavigatorSave.PerformClick();
                amount = "0";

                set.showMessageSuccess(this, $"Creating Savings Account is successful.");
            }
            else
            {
                SavingsBindingNavigatorAdd.PerformClick();
                accountIDSpinEdit4.Text = accounts.getAccountID().ToString();
                interestRateLabel2.Text = $"{str}";
                amountSpinEdit3.Text = amount.ToString();
                datePostedDateEdit3.Text = dateToday;
                SavingsBindingNavigatorSave.PerformClick();

                int lastRow = savingAccountDataGridView.Rows.Count;
                decimal camount = decimal.Parse(savingAccountDataGridView.Rows[lastRow].Cells[2].Value.ToString());
                string strAmount = camount.ToString(".##");
                int currentAmount = Int32.Parse(strAmount);
                int sum = Int32.Parse(amount.ToString()) + currentAmount;

                SavingsBindingNavigatorAdd.PerformClick();
                accountIDSpinEdit4.Text = accounts.getAccountID().ToString();
                interestRateLabel2.Text = $"Total";
                amountSpinEdit3.Text = sum.ToString();
                datePostedDateEdit3.Text = dateToday;
                SavingsBindingNavigatorSave.PerformClick();
            }
            _deposit += Int32.Parse(amount.ToString());

        }

        private void AccountsbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.accountBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void SavingsBindingNavigatorSave_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.savingAccountBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void ChequingBindingNavigatorSave_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.checkingAccountBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void LoanBindingNavigatorSave_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.loanAccountBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }


        /*private void FillDropdownLoanList()
        {
            if(loanAccountDataGridView.Rows.Count>0)
                loanListAmount = new int[loanAccountDataGridView.Rows.Count - 1];
            
            cbo_LoanAccountsList.Items.Clear();
            for (int i = 0; i < loanAccountDataGridView.Rows.Count - 1; i++)
            {
                cbo_LoanAccountsList.Items.Add(loanAccountDataGridView.Rows[i].Cells[0].Value);
                loanListAmount[i] = Int32.Parse(loanAccountDataGridView.Rows[i].Cells[3].Value.ToString());
            }
        }*/

        /*private void cbo_LoanAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            accounts.setLoanID(Int32.Parse(cbo_LoanAccountsList.Text));
            loanAccountDataGridView.DataSource = loanPaymentsBindingSource;
            this.loanPaymentsTableAdapter.FillByID(this.dB_BankDataSet.LoanPayments, accounts.getLoanID());
            
            set.showPanel(pnl_AccountType, this, DockStyle.None);
            cbo_accountType.SelectedIndex = 2;
            cbo_accountType.Enabled = false;


        }*/

        private void txt_SearchName_KeyUp(object sender, KeyEventArgs e)
        {
            string str = txt_SearchName.Text;
            accounts.setClientID(0);
            this.customerTableAdapter.FillByFullName(this.dB_BankDataSet.Customer, str);

            lbl_MatchListNo.Text = $"Match : {customerDataGridView.Rows.Count.ToString()}";

        }

        private void txt_Name_Click(object sender, EventArgs e)
        {
            set.showPanel(pnl_SearchName, this, DockStyle.None);
        }

        private void pnl_SearchName_btn_Ok_Click(object sender, EventArgs e)
        {
            if (accounts.getClientID() > 0)
            {
                txt_Name.Text = accounts.getClientName();
                this.accountTableAdapter.FillByCustomerID(this.dB_BankDataSet.Account, accounts.getClientID());
                accounts.setAccountID(setIDs(accountIDSpinEdit.Text));

                this.savingAccountTableAdapter.FillByID(this.dB_BankDataSet.SavingAccount, accounts.getAccountID());
                this.checkingAccountTableAdapter.FillByID(this.dB_BankDataSet.CheckingAccount, accounts.getAccountID());
                this.loanAccountTableAdapter.FillByID(this.dB_BankDataSet.LoanAccount, accounts.getAccountID());


                accounts.setLoanID(setIDs(loanIDSpinEdit.Text.ToString()));

                /*  if (accounts.getLoanID() != 0)
                      this.loanPaymentsTableAdapter.FillByLoanID(this.dB_BankDataSet.LoanPayments, accounts.getLoanID());
  */
                if (depositSpinEdit.Text != "0")
                {
                    if (depositSpinEdit.Text == "0" || depositSpinEdit.Text == "")
                        _deposit = 0;
                    else
                        _deposit = DecimaltoInt(depositSpinEdit.Text);

                    if (loanSpinEdit.Text == "0" || loanSpinEdit.Text == "")
                        _loan = 0;
                    else
                        _loan = DecimaltoInt(loanSpinEdit.Text);
                }

                pnl_SearchName.SendToBack();
                txt_SearchName.Text = "";

            }
            else
                set.showMessageError(this, "Please select a client.", "OK");
        }

        private int setIDs(string ID)
        {
            if (ID == "" || ID == "0" || ID == null)
                return 0;
            else
            {
                return Int32.Parse(ID);
            }
        }


        private int DecimaltoInt(string amount)
        {
            decimal amt = Decimal.Parse(amount.ToString());
            string str = amt.ToString(".##");
            return Int32.Parse(str);
        }

        private void customerDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (customerDataGridView.SelectedCells.Count > 0)
            {
                int selectedrowindex = customerDataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = customerDataGridView.Rows[selectedrowindex];
                accounts.setClientID(Int32.Parse(selectedRow.Cells[0].Value.ToString()));
                accounts.setClientName(selectedRow.Cells[1].Value.ToString());
                accounts.sethomeAddress(selectedRow.Cells[2].Value.ToString());
            }
        }

        private void SetDefaults()
        {
            //lbl_MatchListNo.Text = "";

            set.setForm(this);
            navButtons = new Button[] { btn_nav_Dashboard, btn_nav_NewAccount, btn_nav_Accounts, btn_nav_Rates, btn_nav_statistics, btn_nav_Transfer };
            dataGridViews = new DataGridView[] { customerDataGridView, savingAccountDataGridView, chequingAccountDatagridView, dataGridViewRates, DGV_LoanPaymentList };
            panels = new Panel[] { pnl_Accounts, pnl_dashboard, pnl_rates, pnl_statistics, pnl_Transfer };
            
            
            set.PanelLayouts(panels, Color.White);
            set.SinglePanelLayout(panel10);
            set.setDataGridViews(dataGridViews);
            setNavButtons(navButtons, null);


            set.setElipse(pnl_nav1, 15);
            set.setElipse(pnl_top, 15);
            set.setElipse(pnl_main2, 15);
            accounts.setBranchID(8);
            lbl_sendername.Text = "";
            lbl_recievername.Text = "";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(accounts.ShowAllDetails());
        }

        /* private void accountIDSpinEdit_EditValueChanged(object sender, EventArgs e)
         {
             if(accountIDSpinEdit.Text !="")
             {
                 accounts.setAccountID(Int32.Parse(accountIDSpinEdit.Text));
                 _accountBranchID = Int32.Parse(branchIDSpinEdit.Text);

                 this.branchTableAdapter.FillByBranchID(this.dB_BankDataSet.Branch, _accountBranchID);
             }

             else
                 accounts.setAccountID(0);
         }*/

        private void checkIDSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (checkIDSpinEdit.Text != "")
                accounts.SetChequeingID(Int32.Parse(checkIDSpinEdit.Text));
            else
                accounts.SetChequeingID(0);
        }



        private void savingIDSpinEdit1_TextChanged(object sender, EventArgs e)
        {

            if (savingIDSpinEdit1.Text != "")
                accounts.setSavingsID(Int32.Parse(savingIDSpinEdit1.Text));
            else
                accounts.setSavingsID(0);

        }


        private void branchbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.branchBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void DGV_LoanPaymentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            set.showPanel(pnl_AccountType, this, DockStyle.None);
            cbo_accountType.SelectedIndex = 2;
            cbo_accountType.Enabled = false;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Total Deposit : \t{_deposit}\r\nTotal Loan : \t{_loan}");
        }

        private void btn_nav_Transfer_Click(object sender, EventArgs e)
        {
            setNavButtons(navButtons, sender);
            set.showPanel(pnl_Transfer, this, DockStyle.Fill);
        }

        private void pnl_main_Click(object sender, EventArgs e)
        {

        }

        private void searchAccountID(object sender, Label lbl, TransferDetails transferDetails)
        {
            BunifuTextBox tb = (BunifuTextBox)sender;
            this.customerAndAccountTableAdapter1.FillByAccountID(this.dB_BankDataSet.CustomerAndAccount, Int32.Parse(tb.Text));

            if (this.dB_BankDataSet.CustomerAndAccount.Rows.Count > 0)
            {

                foreach (DataColumn column in this.dB_BankDataSet.CustomerAndAccount.Columns)
                {
                    Console.WriteLine(column.ColumnName);
                }
                var row = this.dB_BankDataSet.CustomerAndAccount.Rows[0];
                transferDetails.AccountID = Int32.Parse(row["AccountID"].ToString());
                transferDetails.Amount = DecimaltoInt(row["TOTAL"].ToString());
                transferDetails.AccountName = row["FullName"].ToString();

                lbl.Text = transferDetails.AccountName;
            }
            else
                set.showMessageError(this, $"Account Number : {tb.Text} does not exist.", "Try Again");

        }

        private void btn_searchSender_Click(object sender, EventArgs e)
        {
            searchAccountID(txt_sender, lbl_sendername, transferDetailsSender = new TransferDetails());
        }

        private void btn_searchReciever_Click(object sender, EventArgs e)
        {
            searchAccountID(txt_reciever, lbl_recievername, transferDetailsReciever = new TransferDetails());
        }

        private void btn_transfer_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_sendAmount.Text) && txt_sendAmount.Text != "0")
            {
                if (Int32.Parse(txt_sendAmount.Text) <= transferDetailsSender.Amount)
                {
                    CreateChequingAccount(txt_sendAmount.Text.ToString(), "ETranc", transferDetailsReciever.AccountID);
                    CreateChequingAccount($"-{txt_sendAmount.Text.ToString()}", "ETranc", transferDetailsSender.AccountID);
                    set.showMessageSuccess(this, "Transaction complete.");
                }
                else
                    set.showMessageError(this, "Account balance is not enough.", null);

            }
            else
                set.showMessageError(this, "Please enter amount to send.", "OK");


        }

        private void frm_Main_Load_1(object sender, EventArgs e)
        {

        }

        private void loanPaymentsbindingNavigatorSave_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.loanPaymentsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
            /*
                        DGV_LoanPaymentList.DataSource = this.dB_BankDataSet.LoanPayments;*/
            DGV_LoanPaymentList.Update();
        }

        private void updateBank()
        {
            depositSpinEdit.Text = _deposit.ToString();
            loanSpinEdit.Text = _loan.ToString();
            branchbindingNavigatorSaveItem.PerformClick();
        }


    }
}
