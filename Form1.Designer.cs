namespace Bank
{
    partial class frm_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frm_Main
            // 
            this.ClientSize = new System.Drawing.Size(1232, 640);
            this.Name = "frm_Main";
            this.Load += new System.EventHandler(this.frm_Main_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.UI.WinForms.BunifuSeparator bunifuSeparator1;
        private System.Windows.Forms.Panel pnl_nav1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pb_navigation;
        private System.Windows.Forms.PictureBox pb_box;
        private System.Windows.Forms.Timer timer1;
        private Bunifu.UI.WinForms.BunifuFormDock bunifuFormDock1;
        private System.Windows.Forms.Panel pnl_top;
        private DevExpress.XtraEditors.SimpleButton btn_close;
        private DevExpress.XtraEditors.SimpleButton btn_minize;
        private Bunifu.UI.WinForms.BunifuLabel lbl_current;
        private System.Windows.Forms.Panel pnl_labelHeader;
        private System.Windows.Forms.Label lbl_header;
        private System.Windows.Forms.Panel pnl_NewAccount;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.LabelControl lbl_AddEditTitle;
        private Bunifu.UI.WinForms.BunifuTextBox txt_pnl_NewAccount_CustomerName;
        private DevExpress.XtraEditors.SimpleButton btn_pnl_NewAccount_close;
        private System.Windows.Forms.Label label4;
        private Bunifu.UI.WinForms.BunifuTextBox txt_pnl_NewAccount_CompleteAddress;
        private System.Windows.Forms.Label label2;
        private DB_BankDataSet dB_BankDataSet;
        private DevExpress.XtraEditors.SimpleButton btn_pnl_NewAccount_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_pnl_NewAccount_Create;
        private System.Windows.Forms.BindingNavigator customerBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.BindingSource customerBindingSource;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton customerBindingNavigatorSaveItem;
        private System.Windows.Forms.Panel panel2;
        private DB_BankDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private DevExpress.XtraEditors.SpinEdit customerIDSpinEdit;
        private DevExpress.XtraEditors.TextEdit fullNameTextEdit;
        private DevExpress.XtraEditors.TextEdit homeAddressTextEdit;
        private DevExpress.XtraEditors.TextEdit passwordTextEdit;
        private DevExpress.XtraEditors.SpinEdit pINSpinEdit;
        private System.Windows.Forms.GroupBox groupBox1;
        private Bunifu.UI.WinForms.BunifuPanel panels2;
        private System.Windows.Forms.Label label3;
        private Bunifu.UI.WinForms.BunifuTextBox txt_Name;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel1;
        private Bunifu.UI.WinForms.BunifuPanel savingsPanel;
        private System.Windows.Forms.Label lbl_savings;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel3;
        private System.Windows.Forms.DataGridView chequingAccountDatagridView;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.BindingSource checkingAccountBindingSource;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel5;
        private System.Windows.Forms.DataGridView DGV_LoanPaymentList;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.BindingSource loanAccountBindingSource;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SimpleButton btn_SelectAccountType;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private System.Windows.Forms.Panel pnl_Accounts;
        private System.Windows.Forms.Panel pnl_AccountType;
        private DevExpress.XtraEditors.SimpleButton pnl_ChooseAccount_Cancel;
        private DevExpress.XtraEditors.SimpleButton pnl_ChooseAccount_CreateAccount;
        private System.Windows.Forms.Label label8;
        private Bunifu.UI.WinForms.BunifuTextBox txt_amount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Bunifu.UI.WinForms.BunifuDropdown cbo_accountType;
        private System.Windows.Forms.BindingSource accountTypeBindingSource;
        private System.Windows.Forms.Button btn_nav_Dashboard;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btn_nav_NewAccount;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btn_nav_Accounts;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btn_nav_Rates;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btn_nav_statistics;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.BindingSource accountBindingSource;
        private System.Windows.Forms.DataGridView savingAccountDataGridView;
        private System.Windows.Forms.BindingNavigator AccountsbindingNavigator;
        private System.Windows.Forms.ToolStripButton AccountsbindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton AccountsbindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton AccountsbindingNavigatorSaveItem;
        private DevExpress.XtraEditors.SpinEdit accountIDSpinEdit;
        private DevExpress.XtraEditors.SpinEdit customerIDSpinEdit1;
        private DevExpress.XtraEditors.SpinEdit accountTypeSpinEdit;
        private DevExpress.XtraEditors.DateEdit dateModefiedDateEdit;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolStripButton13;
        private System.Windows.Forms.ToolStripButton toolStripButton14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton toolStripButton15;
        private System.Windows.Forms.BindingNavigator SavingsbindingNavigator;
        private System.Windows.Forms.ToolStripButton SavingsBindingNavigatorAdd;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton SavingsBindingNavigatorDelete;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton SavingsBindingNavigatorSave;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.BindingNavigator ChequingBindingNavigator;
        private System.Windows.Forms.ToolStripButton ChequingBindingNavigatorAdd;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripButton ChequingBindingNavigatorDelete;
        private System.Windows.Forms.ToolStripButton toolStripButton18;
        private System.Windows.Forms.ToolStripButton toolStripButton19;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton toolStripButton20;
        private System.Windows.Forms.ToolStripButton toolStripButton21;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton ChequingBindingNavigatorSave;
        private System.Windows.Forms.GroupBox groupBox5;
        private DevExpress.XtraEditors.SpinEdit loanIDSpinEdit;
        private DevExpress.XtraEditors.SpinEdit accountIDSpinEdit3;
        private DevExpress.XtraEditors.SpinEdit branchIDSpinEdit1;
        private DevExpress.XtraEditors.SpinEdit amountSpinEdit2;
        private DevExpress.XtraEditors.DateEdit datePostedDateEdit2;
        private System.Windows.Forms.BindingNavigator LoanBindingNavigator;
        private System.Windows.Forms.ToolStripButton LoanBindingNavigatorAdd;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripButton LoanBindingNavigatorDelete;
        private System.Windows.Forms.ToolStripButton toolStripButton22;
        private System.Windows.Forms.ToolStripButton toolStripButton23;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton toolStripButton24;
        private System.Windows.Forms.ToolStripButton toolStripButton25;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripButton LoanBindingNavigatorSave;
        private DevExpress.XtraEditors.SpinEdit branchIDSpinEdit;
        private DevExpress.XtraEditors.SpinEdit savingIDSpinEdit1;
        private System.Windows.Forms.BindingSource savingAccountBindingSource;
        private DevExpress.XtraEditors.SpinEdit accountIDSpinEdit4;
        private DevExpress.XtraEditors.SpinEdit amountSpinEdit3;
        private DevExpress.XtraEditors.DateEdit datePostedDateEdit3;
        private DevExpress.XtraEditors.SpinEdit checkIDSpinEdit;
        private DevExpress.XtraEditors.SpinEdit accountIDSpinEdit2;
        private DevExpress.XtraEditors.TextEdit checkNumbersTextEdit;
        private DevExpress.XtraEditors.SpinEdit amountSpinEdit1;
        private DevExpress.XtraEditors.DateEdit datePostedDateEdit1;
        private System.Windows.Forms.DataGridView loanAccountDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.BindingSource loanPaymentsBindingSource;
        private System.Windows.Forms.Panel pnl_SearchName;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton pnl_SearchName_btn_Ok;
        private System.Windows.Forms.Label label10;
        private Bunifu.UI.WinForms.BunifuTextBox txt_SearchName;
        private System.Windows.Forms.Panel panel11;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Label lbl_MatchListNo;
        private System.Windows.Forms.DataGridView customerDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkNumbersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn datePostedDataGridViewTextBoxColumn1;
        private DB_BankDataSetTableAdapters.AccountTableAdapter accountTableAdapter;
        private DB_BankDataSetTableAdapters.AccountTypeTableAdapter accountTypeTableAdapter;
        private DB_BankDataSetTableAdapters.CheckingAccountTableAdapter checkingAccountTableAdapter;
        private DB_BankDataSetTableAdapters.CustomerTableAdapter customerTableAdapter;
        private DB_BankDataSetTableAdapters.LoanAccountTableAdapter loanAccountTableAdapter;
        private DB_BankDataSetTableAdapters.SavingAccountTableAdapter savingAccountTableAdapter;
        private System.Windows.Forms.Label interestRateLabel2;
        private System.Windows.Forms.GroupBox LoanPayments;
        private DevExpress.XtraEditors.SpinEdit paymentCounterSpinEdit;
        private DevExpress.XtraEditors.SpinEdit loanIDSpinEdit1;
        private DevExpress.XtraEditors.SpinEdit paymentSpinEdit;
        private DevExpress.XtraEditors.DateEdit datePostedDateEdit;
        private DevExpress.XtraEditors.TextEdit oRNumTextEdit;
        private System.Windows.Forms.BindingNavigator loanPaymentsbindingNavigator;
        private System.Windows.Forms.ToolStripButton loanPaymentsbindingNavigatorAdd;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripButton loanPaymentsbindingNavigatorDelete;
        private System.Windows.Forms.ToolStripButton toolStripButton26;
        private System.Windows.Forms.ToolStripButton toolStripButton27;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripButton toolStripButton28;
        private System.Windows.Forms.ToolStripButton toolStripButton29;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripButton loanPaymentsbindingNavigatorSave;
        private System.Windows.Forms.DataGridView dataGridViewRates;
        private System.Windows.Forms.Panel pnl_rates;
        private System.Windows.Forms.Panel pnl_statistics;
        private System.Windows.Forms.BindingSource branchBindingSource;
        private DB_BankDataSetTableAdapters.BranchTableAdapter branchTableAdapter;
        private System.Windows.Forms.BindingSource branchBindingSource1;
        private DB_BankDataSet dB_BankDataSet1;
        private DB_BankDataSetTableAdapters.BranchTableAdapter branchTableAdapter1;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private System.Windows.Forms.Panel pnl_dashboard;
        private System.Windows.Forms.Panel pnl_dashHeaader;
        private System.Windows.Forms.Label lbl_user;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label lbl_position;
        private System.Windows.Forms.Panel panel14;
        private DevExpress.XtraEditors.Controls.CalendarControl calendarControl1;
        private System.Windows.Forms.Label lbl_greetings;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.Panel pnl_core;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnl_mission;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnl_vision;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewTextBoxColumn savingIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn interestRateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datePostedDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingNavigator branchbindingNavigator;
        private System.Windows.Forms.ToolStripButton toolStripButton16;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripButton toolStripButton17;
        private System.Windows.Forms.ToolStripButton toolStripButton30;
        private System.Windows.Forms.ToolStripButton toolStripButton31;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripButton toolStripButton32;
        private System.Windows.Forms.ToolStripButton toolStripButton33;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripButton branchbindingNavigatorSaveItem;
        private System.Windows.Forms.GroupBox groupBox6;
        private DevExpress.XtraEditors.SpinEdit branchIDSpinEdit2;
        private DevExpress.XtraEditors.SpinEdit depositSpinEdit;
        private DevExpress.XtraEditors.SpinEdit loanSpinEdit;
        private DevExpress.XtraEditors.TextEdit branchNameTextEdit;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private System.Windows.Forms.DataGridViewTextBoxColumn paymentCounterDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn paymentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datePostedDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn oRNumDataGridViewTextBoxColumn;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Button btn_nav_Transfer;
        private System.Windows.Forms.Panel pnl_Transfer;
        private Bunifu.UI.WinForms.BunifuPanel pnl_main2;
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.Panel pnl_left;
        private System.Windows.Forms.Panel panel15;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel2;
        private System.Windows.Forms.Label label16;
        private Bunifu.UI.WinForms.BunifuTextBox txt_sender;
        private System.Windows.Forms.Label label17;
        private DevExpress.XtraEditors.SimpleButton btn_searchSender;
        private System.Windows.Forms.Label lbl_sendername;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel8;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel7;
        private System.Windows.Forms.Label label18;
        private DevExpress.XtraEditors.SimpleButton btn_searchReciever;
        private System.Windows.Forms.Label lbl_recievername;
        private System.Windows.Forms.Label label20;
        private Bunifu.UI.WinForms.BunifuTextBox txt_reciever;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.UI.WinForms.BunifuTextBox txt_sendAmount;
        private DevExpress.XtraEditors.SimpleButton btn_transfer;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DB_BankDataSetTableAdapters.CustomerAndAccountTableAdapter customerAndAccountTableAdapter1;
    }
}

