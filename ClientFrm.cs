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
        Panel[] MainPanels;
        Panel[] InnerPanels;
        Panel[] MainInnerPanels;
        SimpleButton[] navButtons;
        DataGridView[] dgv;
        private string dateToday = DateTime.UtcNow.ToString();
        private int id;
        string password = null;
        public ClientFrm()
        {
            InitializeComponent();
            id = 85;
            setDefaults();
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
            // TODO: This line of code loads data into the 'dB_BankDataSet.Account' table. You can move, or remove it, as needed.
            this.accountTableAdapter.FillByAccountID(this.dB_BankDataSet.Account,id);

            this.checkingAccountTableAdapter.FillByAccountID(this.dB_BankDataSet.CheckingAccount, id);
            this.savingAccountTableAdapter.FillByID(this.dB_BankDataSet.SavingAccount, id);
            this.loanAccountTableAdapter.FillByID(this.dB_BankDataSet.LoanAccount,id);

            Totals(lbl_savingsTotal, savingAccountDataGridView, 1);
            Totals(lbl_CheckingTotal, checkingAccountDataGridView, 2);
            Totals(lbl_loanTotal, loanAccountDataGridView, 2);

            password = passwordTextEdit.Text; 
        }

        private void setPanels(Panel[] pnl, int radius, Color color, Padding pad)
        {

            for (int i = 0; i < pnl.Length; i++)
            {
                set.setElipse(pnl[i], radius);
                pnl[i].BackColor = color;
                pnl[i].Padding = pad;
            }
        }

        private void savingAccountBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.savingAccountBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);

        }
        private void checkingbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.checkingAccountBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }

        private void accountbindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.accountBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
        }
        private void setNav(object obj)
        {

            for (int i = 0; i < navButtons.Length; i++)
                set.NavButtonIdle_Simple(navButtons[i]);

            SimpleButton btn = (SimpleButton)obj;
            set.NavButtonActive_Simple(btn);
        }

        private void nav_btn_home_Click(object sender, EventArgs e)
        {
            setNav(sender);
            set.showPanel(pnl_Home, this, DockStyle.Fill);
        }

        private void nav_btn_savings_Click(object sender, EventArgs e)
        {
            setNav(sender);
            set.showPanel(pnl_savings, this, DockStyle.Fill);
        }

        private void nav_btn_checking_Click(object sender, EventArgs e)
        {
            setNav(sender);
            set.showPanel(pnl_checking, this, DockStyle.Fill);
        }

        private void nav_btn_loan_Click(object sender, EventArgs e)
        {
            setNav(sender);
            set.showPanel(pnl_loan, this, DockStyle.Fill);
        }

        private void nav_btn_send_Click(object sender, EventArgs e)
        {
            setNav(sender);
            set.showPanel(pnl_sendMoney, this, DockStyle.Fill);
        }

        private void nav_btn_settings_Click(object sender, EventArgs e)
        {
            setNav(sender);
            set.showPanel(pnl_settings, this, DockStyle.Fill);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Totals(Label displayLabel, DataGridView dgv, int col)
        {
            int sum = 0;
            if (dgv.Rows.Count > 0)
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    sum += set.DecimaltoInt(dgv.Rows[i].Cells[col].Value.ToString());
                }
            }
            else
            {
                displayLabel.Text = "0.00";
                return;
            }
            displayLabel.Text = sum.ToString();
        }

        private void btn_sendMoney_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_sendAccountNumber.Text) && !string.IsNullOrEmpty(txt_sendName.Text) && !string.IsNullOrEmpty(txt_sendAmount.Text))
            {
                this.customerAndAccountTableAdapter.FillByAccountID(this.dB_BankDataSet.CustomerAndAccount, Int32.Parse(lbl_accountNumber.Text));
                transferDetails = new TransferDetails();

                if(Int32.Parse(txt_sendAmount.Text) < Int32.Parse(lbl_CheckingTotal.Text))
                {
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

                        CheckingAccount(txt_sendAmount.Text.ToString(), "ETranc", Int32.Parse(txt_sendAccountNumber.Text));
                        CheckingAccount($"-{txt_sendAmount.Text.ToString()}", "ETranc", Int32.Parse(lbl_accountNumber.Text));

                        set.showMessageSuccess(this,"Transaction Successful.");
                        this.checkingAccountTableAdapter.FillByAccountID(dB_BankDataSet.CheckingAccount, Int32.Parse(lbl_accountNumber.Text));
                        Totals(lbl_CheckingTotal, checkingAccountDataGridView, 2);
                        
                    }
                    else
                    {
                        set.showMessageError(this, $"Account Number : {lbl_accountNumber.Text} does not exist.", "Try Again");
                        return;
                    }
                }
                else
                {
                    set.showMessageError(this, $"Not enought balance.", null);
                    return;
                }
                
                    
            }
        }

        private void searchAccount(string text, Label lbl_senderName, TransferDetails transferDetails)
        {

            
        }


        private void CheckingAccount(string amt, string to, int accountID)
        {
            checkingbindingNavigatorAddItem.PerformClick();
            accountIDSpinEdit.Text = accountID.ToString();
            checkNumbersTextEdit.Text = to;
            amountSpinEdit.Text = amt;
            datePostedDateEdit.Text = dateToday;
            checkingbindingNavigatorSaveItem.PerformClick();    
        }

        private void btn_saveAccount_Click(object sender, EventArgs e)
        {
            if (password == txt_currentPassword.Text)
            {
                if (!string.IsNullOrEmpty(txt_newPasswrod.Text) && !string.IsNullOrEmpty(txt_confirmPassword.Text) && (txt_newPasswrod.Text == txt_confirmPassword.Text))
                {
                    passwordTextEdit.Text = txt_newPasswrod.Text;
                    accountbindingNavigatorSaveItem.PerformClick();
                    password = passwordTextEdit.Text;
                    set.showMessageSuccess(this, "Password is successfully save.");
                }
                else
                    set.showMessageError(this, "New Password and Confirm Password does not match", "OK");
            }
            else
                set.showMessageError(this, "Old Password does not match your current password.", "OK");
        }

       
    }
}
