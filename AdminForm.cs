using Bank.DB_BankDataSetTableAdapters;
using DevExpress.DataAccess.Native.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Bank
{
    public partial class AdminForm : Form
    {
        Settings set = new Settings();
        UserDetails userDetails = new UserDetails();
        ReportPrintTool printTool;
        System.Windows.Forms.Button[] navButtons;
        SimpleButton[] branchButtons;
        SimpleButton[] CityButtons;
        SimpleButton[] LocationButtons;
        SimpleButton[] EmployeeButtons;
        SimpleButton[] ManagersButtons;
        DataGridView[] dataGridViews;
        Panel[] panelList;
        Panel[] dashboardPanels;
        Panel[] innerPanels;
        public AdminForm(UserDetails user)
        {
            InitializeComponent();
            SetDefaults();
            userDetails = user;
        }

        private void SetDefaults()
        {
            panelList = new Panel[] { left_innerPanel, Top_innerPanel, Main_innerPanel };
            navButtons = new System.Windows.Forms.Button[] { btn_nav_Branch, btn_nav_city, btn_nav_dashboard, btn_nav_manager, btn_nav_employee, btn_nav_location, btn_nav_reports, btn_nav_statistics, btn_nav_aboutus };
            branchButtons = new SimpleButton[] { btn_branchNew, btn_branchSave, btn_branchCancel };
            CityButtons = new SimpleButton[] { btn_cityNew, btn_citySave, btn_cityDelete };
            LocationButtons = new SimpleButton[] { btn_LocationNew, btn_LocationSave, btn_LocationDelete };
            EmployeeButtons = new SimpleButton[] { btn_EmployeeNew, btn_EmployeeSave, btn_EmployeeDelete };
            ManagersButtons = new SimpleButton[] { btn_ManagerNew, btn_ManagerSave, btn_ManagerDelete };
            dataGridViews = new DataGridView[] { branchDataGridView, locationDataGridView, cityDataGridView, employeeDataGridView, managerDataGridView };
            dashboardPanels = new Panel[] { pnl_vision, pnl_mission, pnl_core };
            innerPanels = new Panel[] { pnl_MI_dashboard, pnl_MI_branch, pnl_MI_city, pnl_IM_Location, pnl_MI_Managers, pnl_IM_Employee, pnl_MI_statistics, pnl_MI_about };

            set.setDataGridViews(dataGridViews);
            set.PanelLayouts(panelList, Color.WhiteSmoke);
            set.PanelLayouts(dashboardPanels, Color.Transparent);
            set.PanelLayouts(innerPanels, Color.White);

            setButtonControls(branchButtons, 0, true);
            setButtonControls(LocationButtons, 0, true);
            setButtonControls(CityButtons, 0, true);
            setButtonControls(EmployeeButtons, 0, true);
            setButtonControls(ManagersButtons, 0, true);

            set.setElipse(pb_logo, 5);

        }
        public void setNavButtons(System.Windows.Forms.Button[] btn, Object obj)
        {
            for (int i = 0; i < btn.Length; i++)
                set.NavButtonIdle(btn[i]);

            if (obj != null)
                set.NavButtonActive(obj);

            if (obj != null)
            {
                System.Windows.Forms.Button getButtonText = (System.Windows.Forms.Button)obj;
                lbl_header.Text = (getButtonText.Text).Trim();
            }

        }

        private void setButtonControls(SimpleButton[] obj, int selectedButton, bool enabler)
        {
            for (int i = 0; i < obj.Length; i++)
            {
                if (i == selectedButton)
                    obj[i].Enabled = enabler;
                else
                    obj[i].Enabled = !enabler;
            }
        }

        private void btn_nav_statistics_Click(object sender, EventArgs e)
        {
            setNavButtons(navButtons, sender);
            set.showPanel(pnl_MI_dashboard, this, DockStyle.Fill);


        }

        private void btn_nav_Branch_Click(object sender, EventArgs e)
        {
            set.showPanel(pnl_MI_branch, this, DockStyle.Fill);
            setNavButtons(navButtons, sender);
            setButtonControls(branchButtons, 0, true);
        }

        private void btn_nav_city_Click(object sender, EventArgs e)
        {
            set.showPanel(pnl_MI_city, this, DockStyle.Fill);
            setNavButtons(navButtons, sender);
            setButtonControls(CityButtons, 0, true);
        }

        private void btn_nav_location_Click(object sender, EventArgs e)
        {
            setNavButtons(navButtons, sender);
            set.showPanel(pnl_IM_Location, this, DockStyle.Fill);
            setButtonControls(LocationButtons, 0, true);
        }

        private void btn_nav_employee_Click(object sender, EventArgs e)
        {
            setNavButtons(navButtons, sender);
            setButtonControls(EmployeeButtons, 0, true);
            set.showPanel(pnl_IM_Employee, this, DockStyle.Fill);
        }

        private void btn_nav_statistics_Click_1(object sender, EventArgs e)
        {
            set.showPanel(pnl_MI_statistics, this, DockStyle.Fill);
            setNavButtons(navButtons, sender);
        }

        private void btn_nav_reports_Click(object sender, EventArgs e)
        {
            setNavButtons(navButtons, sender);
            set.showPanel(pnl_MI_reports,this,DockStyle.Fill);
        }

        private System.Data.DataTable GetDataEmployee()
        {
            DB_BankDataSet dataSet = new DB_BankDataSet();
            var adapter = new DB_BankDataSetTableAdapters.DisplayEmployeeTableAdapter();
            adapter.Fill(dataSet.DisplayEmployee);
            System.Data.DataTable dataTable = new System.Data.DataTable();

            foreach (System.Data.DataColumn column in dataSet.DisplayEmployee.Columns)
            {
                dataTable.Columns.Add(column.ColumnName, column.DataType);
            }

            foreach (DataRow row in dataSet.DisplayEmployee.Rows)
            {
                DataRow newRow = dataTable.NewRow();
                newRow.ItemArray = row.ItemArray;
                dataTable.Rows.Add(newRow);
            }

            return dataTable;
        }

        private System.Data.DataTable GetDataBranch()
        {

            DB_BankDataSet dataSet = new DB_BankDataSet();
            var adapter = new DB_BankDataSetTableAdapters.DisplayBranchTableAdapter();
            adapter.Fill(dataSet.DisplayBranch);
            System.Data.DataTable dataTable = new System.Data.DataTable();

            foreach (System.Data.DataColumn column in dataSet.DisplayBranch.Columns)
            {
                dataTable.Columns.Add(column.ColumnName, column.DataType);
            }

            foreach (DataRow row in dataSet.DisplayBranch.Rows)
            {
                DataRow newRow = dataTable.NewRow();
                newRow.ItemArray = row.ItemArray;
                dataTable.Rows.Add(newRow);
            }

            return dataTable;
        }

        private System.Data.DataTable GetDataManager()
        {

            DB_BankDataSet dataSet = new DB_BankDataSet();
            var adapter = new DB_BankDataSetTableAdapters.ManagerTableAdapter();
            adapter.Fill(dataSet.Manager);
            System.Data.DataTable dataTable = new System.Data.DataTable();

            foreach (System.Data.DataColumn column in dataSet.Manager.Columns)
            {
                dataTable.Columns.Add(column.ColumnName, column.DataType);
            }

            foreach (DataRow row in dataSet.Manager.Rows)
            {
                DataRow newRow = dataTable.NewRow();
                newRow.ItemArray = row.ItemArray;
                dataTable.Rows.Add(newRow);
            }

            return dataTable;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("You are about to close the Application.\r\nDo you want to proceed?", "Application Closing", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.OK)
            {
                Application.Restart();
            }

        }

        private void branchBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.branchBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);

            displayBranchTableAdapter.Fill(dB_BankDataSet.DisplayBranch);
            set.showMessageSuccess(this, "Successfully Save.");
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            btn_nav_dashboard.PerformClick();
            this.displayBranchTableAdapter.Fill(this.dB_BankDataSet.DisplayBranch);
            //this.displayBranchTableAdapter.Fill(this.dB_BankDataSet1.DisplayBranch);
            this.displayEmployeeTableAdapter.Fill(this.dB_BankDataSet.DisplayEmployee);

            this.employeeTypeTableAdapter.Fill(this.dB_BankDataSet.EmployeeType);
            this.managerTableAdapter.Fill(this.dB_BankDataSet.Manager);
            this.employeeTableAdapter.Fill(this.dB_BankDataSet.Employee);
            this.cityTableAdapter.Fill(this.dB_BankDataSet.City);
            this.locationTableAdapter.Fill(this.dB_BankDataSet.Location);
            this.branchTableAdapter.Fill(this.dB_BankDataSet.Branch);

            string str = DateTime.Now.ToString("tt");
            if (str == "AM")
                lbl_greetings.Text = "Good Morning!";
            else
                lbl_greetings.Text = "Good Evening!";

            lbl_user.Text = userDetails.Name;

        }

        private void btn_branchNew_Click(object sender, EventArgs e)
        {
            try
            {
                setButtonControls(branchButtons, 0, false);
                bindingNavigatorAddNewItem.PerformClick();
            }
            catch (Exception ex)
            {
                set.showMessageError(this, $"{ex.Message}", "OK");
                branchbindingNavigatorDeleteItem.PerformClick();
            }


        }

        private void btn_branchSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (locationIDSpinEdit.Text != "" && cityIDSpinEdit.Text != "")
                {
                    setButtonControls(branchButtons, 0, true);
                    branchBindingNavigatorSaveItem.PerformClick();
                }
                else
                    set.showMessageError(this, "Please fill all the required fields", "OK");

            }
            catch (Exception ex)
            {
                //Ignore Error due to user's action
            }

        }

        private void btn_branchCancel_Click(object sender, EventArgs e)
        {
            setButtonControls(branchButtons, 0, true);
            branchbindingNavigatorDeleteItem.PerformClick();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
                locationIDSpinEdit.Text = comboBox1.SelectedValue.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null)
                cityIDSpinEdit.Text = comboBox2.SelectedValue.ToString();
        }

        private void btn_LocationNew_Click(object sender, EventArgs e)
        {
            setButtonControls(LocationButtons, 0, false);
            locationBindingNavigatorAddNewItem.PerformClick();
        }

        private void btn_LocationSave_Click(object sender, EventArgs e)
        {
            if (descriptionTextEdit.Text != "")
            {
                setButtonControls(LocationButtons, 0, true);
                locationBindingNavigatorSaveItem.PerformClick();
            }
            else
                set.showMessageError(this, "Please name of the Location in the Description.", "OK");

        }

        private void btn_LocationDelete_Click(object sender, EventArgs e)
        {
            setButtonControls(LocationButtons, 0, true);
            locationBindingNavigatorDeleteItem.PerformClick();
        }

        private void locationBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.locationBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
            set.showMessageSuccess(this, "Successfully Save.");
        }

        private void btn_citySave_Click(object sender, EventArgs e)
        {
            if (descriptionTextEdit1.Text != "")
            {
                setButtonControls(CityButtons, 0, true);
                cityBindingNavigatorSaveItem.PerformClick();
            }
            else
                set.showMessageError(this, "Please name of the City in the Description.", "OK");

        }

        private void cityBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.cityBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);

            set.showMessageSuccess(this, "Successfully Save.");
        }

        private void btn_cityNew_Click(object sender, EventArgs e)
        {
            try
            {
                setButtonControls(CityButtons, 0, false);
                cityBindingNavigatorAddNewItem.PerformClick();
            }
            catch (Exception ex)
            {
                //Ignore Error due to user's action
            }

        }

        private void btn_cityDelete_Click(object sender, EventArgs e)
        {
            cityBindingNavigatorDeleteItem.PerformClick();
            setButtonControls(CityButtons, 0, true);
        }

        private void branchIDSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (locationIDSpinEdit.Text != "" && cityIDSpinEdit.Text != "")
            {
                comboBox1.SelectedValue = locationIDSpinEdit.Text;
                comboBox2.SelectedValue = cityIDSpinEdit.Text;
                setButtonControls(branchButtons, 0, false);
            }
        }

        private void locationIDSpinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (locationIDSpinEdit1.Text != "")
                setButtonControls(LocationButtons, 0, false);

        }

        private void cityIDSpinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (cityIDSpinEdit1.Text != "")
                setButtonControls(CityButtons, 0, false);
        }

        private void employeeIDSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void btn_EmployeeNew_Click(object sender, EventArgs e)
        {
            try
            {
                employeeBindingNavigatorAddItem.PerformClick();
                setButtonControls(EmployeeButtons, 0, false);
            }
            catch (Exception ex)
            {
                set.showMessageError(this, $"{ex.Message}", "OK");
                employeeBindingNavigatorDelete.PerformClick();
            }
        }

        private void employeeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.employeeBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);

            displayEmployeeTableAdapter.Fill(this.dB_BankDataSet.DisplayEmployee);
            employeeDataGridView.RefreshEdit();
            set.showMessageSuccess(this, "Successfully Save.");
        }

        private void btn_EmployeeSave_Click(object sender, EventArgs e)
        {
            branchIDSpinEdit1.Text = cbo_BranchID.SelectedValue.ToString();
            locationIDSpinEdit2.Text = cbo_locationID.SelectedValue.ToString();
            managerIDSpinEdit.Text = cbo_managerID.SelectedValue.ToString();
            employeeTypeSpinEdit.Text = cbo_employeeType.SelectedValue.ToString();

            employeeBindingNavigatorSaveItem.PerformClick();
        }



        private void employeeDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.employeeBindingNavigator.BindingSource.Position = employeeDataGridView.CurrentRow.Index;
            cbo_BranchID.SelectedValue = branchIDSpinEdit1.Text;
            cbo_locationID.SelectedValue = Int32.Parse(locationIDSpinEdit2.Text.ToString());
            cbo_managerID.SelectedValue = managerIDSpinEdit.Text;
            cbo_employeeType.SelectedValue = employeeTypeSpinEdit.Text;
            setButtonControls(EmployeeButtons, 0, false);
        }

        private void branchDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.branchBindingNavigator.BindingSource.Position = branchDataGridView.CurrentRow.Index;
            setButtonControls(branchButtons, 0, false);
        }

        private void btn_nav_manager_Click(object sender, EventArgs e)
        {
            setNavButtons(navButtons, sender);
            set.showPanel(pnl_MI_Managers, this, DockStyle.Fill);
            setButtonControls(ManagersButtons, 0, true);
        }

        private void ManagerbindingNavigatorSave_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.managerBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);
            set.showMessageSuccess(this, "Successfully Save.");
            setButtonControls(ManagersButtons, 0, true);
        }

        private void btn_ManagerNew_Click(object sender, EventArgs e)
        {
            try
            {
                setButtonControls(ManagersButtons, 0, false);
                ManagerbindingNavigatorAddItem.PerformClick();
            }
            catch (Exception ex)
            {
                //Ignore Error due to user's action
            }
        }

        private void btn_ManagerSave_Click(object sender, EventArgs e)
        {
            ManagerbindingNavigatorSave.PerformClick();
        }

        private void btn_ManagerDelete_Click(object sender, EventArgs e)
        {
            ManagerbindingNavigatorDeleteItem.PerformClick();
        }

        private void btn_EmployeeDelete_Click(object sender, EventArgs e)
        {
            employeeBindingNavigatorDelete.PerformClick();
        }

        private void btn_nav_aboutus_Click(object sender, EventArgs e)
        {
            setNavButtons(navButtons, sender);
            set.showPanel(pnl_MI_about, this, DockStyle.Fill);
        }

        private void cbo_reports_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = cbo_reports.Text;
            if (str == "List of Employees")
            {
                XtraReport1 report = new XtraReport1();
                report.DataSource = GetDataEmployee();
                documentViewer1.DocumentSource = report;
                report.CreateDocument();
                printTool = new ReportPrintTool(report);
            }
            else if (str == "Branch Report")
            {
                XtraReport2 report = new XtraReport2();
                report.DataSource = GetDataBranch();
                documentViewer1.DocumentSource = report;
                report.CreateDocument();
                printTool = new ReportPrintTool(report);
            }
            else if (str == "List of Managers")
            {
                XtraReport3 report = new XtraReport3();
                report.DataSource = GetDataManager();
                documentViewer1.DocumentSource = report;
                report.CreateDocument();
                printTool = new ReportPrintTool(report);
            }
            
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            printTool.ShowPreviewDialog();
        }
    }
}
