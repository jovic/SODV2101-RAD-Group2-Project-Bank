
using Bank.DB_BankDataSetTableAdapters;
using DevExpress.Utils.Helpers;
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
        Button[] navButtons;
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
            // Initialize arrays for various UI components
            panelList = new Panel[] { left_innerPanel, Top_innerPanel, Main_innerPanel };
            navButtons = new Button[] { btn_nav_Branch, btn_nav_city, btn_nav_dashboard, btn_nav_manager, btn_nav_employee, btn_nav_location, btn_nav_reports, btn_nav_statistics, btn_nav_aboutus };
            branchButtons = new SimpleButton[] { btn_branchNew, btn_branchSave, btn_branchCancel };
            CityButtons = new SimpleButton[] { btn_cityNew, btn_citySave, btn_cityDelete };
            LocationButtons = new SimpleButton[] { btn_LocationNew, btn_LocationSave, btn_LocationDelete };
            EmployeeButtons = new SimpleButton[] { btn_EmployeeNew, btn_EmployeeSave, btn_EmployeeDelete };
            ManagersButtons = new SimpleButton[] { btn_ManagerNew, btn_ManagerSave, btn_ManagerDelete };
            dataGridViews = new DataGridView[] { branchDataGridView, locationDataGridView, cityDataGridView, employeeDataGridView, managerDataGridView };
            dashboardPanels = new Panel[] { pnl_vision, pnl_mission, pnl_core };
            innerPanels = new Panel[] { pnl_MI_dashboard, pnl_MI_branch, pnl_MI_city, pnl_IM_Location, pnl_MI_Managers, pnl_IM_Employee, pnl_MI_statistics, pnl_MI_about };

            // Set up data grid views
            set.setDataGridViews(dataGridViews);

            // Set panel layouts with specified colors
            set.PanelLayouts(panelList, Color.WhiteSmoke);
            set.PanelLayouts(dashboardPanels, Color.Transparent);
            set.PanelLayouts(innerPanels, Color.White);

            // Initialize button controls to a default state
            setButtonControls(branchButtons, 0, true);
            setButtonControls(LocationButtons, 0, true);
            setButtonControls(CityButtons, 0, true);
            setButtonControls(EmployeeButtons, 0, true);
            setButtonControls(ManagersButtons, 0, true);

            // Set the ellipse effect on the logo picture box
            set.setElipse(pb_logo, 5);
        }
        public void setNavButtons(Button[] btn, Object obj)
        {
            // Iterate through the array of buttons and set them to an idle state
            for (int i = 0; i < btn.Length; i++)
                set.NavButtonIdle(btn[i]);

            // If an object is provided, set it as the active navigation button
            if (obj != null)
                set.NavButtonActive(obj);

            // If an object is provided, update the header label with the active button's text
            if (obj != null)
            {
                Button getButtonText = (Button)obj;
                lbl_header.Text = (getButtonText.Text).Trim();
            }
        }

        private void setButtonControls(SimpleButton[] obj, int selectedButton, bool enabler)
        {
            // Iterate through the array of SimpleButtons
            for (int i = 0; i < obj.Length; i++)
            {
                // Enable the selected button and disable others
                if (i == selectedButton)
                    obj[i].Enabled = enabler;
                else
                    obj[i].Enabled = !enabler;
            }
        }

        private void btn_nav_statistics_Click(object sender, EventArgs e)
        {
            // Set the navigation buttons, marking the clicked button as active
            setNavButtons(navButtons, sender);

            // Show the dashboard panel, filling the available space
            set.showPanel(pnl_MI_dashboard, this, DockStyle.Fill);
        }

        private void btn_nav_Branch_Click(object sender, EventArgs e)
        {
            // Show the branch panel, filling the available space
            set.showPanel(pnl_MI_branch, this, DockStyle.Fill);

            // Set the navigation buttons, marking the clicked button as active
            setNavButtons(navButtons, sender);

            // Enable the branch-related buttons and disable others
            setButtonControls(branchButtons, 0, true);
        }

        private void btn_nav_city_Click(object sender, EventArgs e)
        {
            // Show the city panel, filling the available space
            set.showPanel(pnl_MI_city, this, DockStyle.Fill);

            // Set the navigation buttons, marking the clicked button as active
            setNavButtons(navButtons, sender);

            // Enable the city-related buttons and disable others
            setButtonControls(CityButtons, 0, true);
        }

        private void btn_nav_location_Click(object sender, EventArgs e)
        {
            // Set the navigation buttons, marking the clicked button as active
            setNavButtons(navButtons, sender);

            // Show the location panel, filling the available space
            set.showPanel(pnl_IM_Location, this, DockStyle.Fill);

            // Enable the location-related buttons and disable others
            setButtonControls(LocationButtons, 0, true);
        }

        private void btn_nav_employee_Click(object sender, EventArgs e)
        {
            // Set the navigation buttons, marking the clicked button as active
            setNavButtons(navButtons, sender);

            // Enable the employee-related buttons and disable others
            setButtonControls(EmployeeButtons, 0, true);

            // Show the employee panel, filling the available space
            set.showPanel(pnl_IM_Employee, this, DockStyle.Fill);
        }

        private void btn_nav_statistics_Click_1(object sender, EventArgs e)
        {
            // Show the statistics panel, filling the available space
            set.showPanel(pnl_MI_statistics, this, DockStyle.Fill);

            // Set the navigation buttons, marking the clicked button as active
            setNavButtons(navButtons, sender);
        }

        private void btn_nav_reports_Click(object sender, EventArgs e)
        {
            // Set the navigation buttons, marking the clicked button as active
            setNavButtons(navButtons, sender);

            // Show the reports panel, filling the available space
            set.showPanel(pnl_MI_reports, this, DockStyle.Fill);
        }

        private DataTable GetDataEmployee()
        {
            // Create a new instance of the dataset for employee data
            var dataSet = new DB_BankDataSet();

            // Create an adapter to fill the employee data
            var adapter = new DisplayEmployeeTableAdapter();
            adapter.Fill(dataSet.DisplayEmployee);

            // Return the filled employee data table
            return dataSet.DisplayEmployee;
        }

        private DataTable GetDataBranch()
        {
            // Create a new instance of the dataset for branch data
            var dataSet = new DB_BankDataSet();

            // Create an adapter to fill the branch data
            var adapter = new DisplayBranchTableAdapter();
            adapter.Fill(dataSet.DisplayBranch);

            // Return the filled branch data table
            return dataSet.DisplayBranch;
        }

        private DataTable GetDataManager()
        {
            // Create a new instance of the dataset for manager data
            var dataSet = new DB_BankDataSet();

            // Create an adapter to fill the manager data
            var adapter = new ManagerTableAdapter();
            adapter.Fill(dataSet.Manager);

            // Return the filled manager data table
            return dataSet.Manager;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            // Show a confirmation dialog to the user about closing the application
            DialogResult = MessageBox.Show("You are about to close the Application.\r\nDo you want to proceed?", "Application Closing", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            // If the user clicks OK, restart the application
            if (DialogResult == DialogResult.OK)
            {
                Application.Restart();
            }
        }

        private void branchBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the current data
            this.Validate();

            // End editing on the binding source
            this.branchBindingSource.EndEdit();

            // Update the database with the changes
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);

            // Refresh the DisplayBranch data
            displayBranchTableAdapter.Fill(dB_BankDataSet.DisplayBranch);

            // Show a success message to the user
            set.showMessageSuccess(this, "Successfully Save.");
        }
        private void AdminForm_Load(object sender, EventArgs e)
        {
            // Simulate a click on the dashboard navigation button to initialize the view
            btn_nav_dashboard.PerformClick();

            // Fill the DisplayBranch table with data from the database
            this.displayBranchTableAdapter.Fill(this.dB_BankDataSet.DisplayBranch);

            // Fill various tables with data from the database
            this.displayEmployeeTableAdapter.Fill(this.dB_BankDataSet.DisplayEmployee);
            this.employeeTypeTableAdapter.Fill(this.dB_BankDataSet.EmployeeType);
            this.managerTableAdapter.Fill(this.dB_BankDataSet.Manager);
            this.employeeTableAdapter.Fill(this.dB_BankDataSet.Employee);
            this.cityTableAdapter.Fill(this.dB_BankDataSet.City);
            this.locationTableAdapter.Fill(this.dB_BankDataSet.Location);
            this.branchTableAdapter.Fill(this.dB_BankDataSet.Branch);

            // Determine the current time of day and set a greeting message
            string str = DateTime.Now.ToString("tt");
            if (str == "AM")
                lbl_greetings.Text = "Good Morning!";
            else
                lbl_greetings.Text = "Good Evening!";

            // Display the user's name in the user label
            lbl_user.Text = userDetails.Name;
        }

        private void btn_branchNew_Click(object sender, EventArgs e)
        {
            try
            {
                // Disable certain button controls while adding a new branch
                setButtonControls(branchButtons, 0, false);

                // Simulate a click on the "Add New" button of the binding navigator
                bindingNavigatorAddNewItem.PerformClick();
            }
            catch (Exception ex)
            {
                // Show an error message if an exception occurs
                set.showMessageError(this, $"{ex.Message}", "OK");

                // Perform a click on the "Delete" button of the binding navigator to handle the error
                branchbindingNavigatorDeleteItem.PerformClick();
            }
        }

        private void btn_branchSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the required fields (locationID and cityID) are filled
                if (locationIDSpinEdit.Text != "" && cityIDSpinEdit.Text != "")
                {
                    // Enable button controls for saving
                    setButtonControls(branchButtons, 0, true);

                    // Simulate a click on the "Save" button of the binding navigator
                    branchBindingNavigatorSaveItem.PerformClick();
                }
                else
                {
                    // Show an error message if required fields are not filled
                    set.showMessageError(this, "Please fill all the required fields", "OK");
                }
            }
            catch (Exception ex)
            {
                // Ignore any errors that occur due to user actions
            }
        }

        private void btn_branchCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // Enable button controls for branch operations
                setButtonControls(branchButtons, 0, true);

                // Simulate a click on the "Delete" button of the binding navigator
                branchbindingNavigatorDeleteItem.PerformClick();

                // Update the database with the changes
                this.branchTableAdapter.Update(dB_BankDataSet.Branch);

                // Refresh the display of branches
                this.displayBranchTableAdapter.Fill(dB_BankDataSet.DisplayBranch);
            }
            catch (Exception ex)
            {
                // Show an error message if the branch cannot be deleted
                set.showMessageError(this, "Cannot delete Branch because it is still being used.\n\rCheck it first before deleting the branch.", "OK");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if a value is selected in comboBox1
            if (comboBox1.SelectedValue != null)
                // Set the locationIDSpinEdit text to the selected value
                locationIDSpinEdit.Text = comboBox1.SelectedValue.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if a value is selected in comboBox2 and Set the cityIDSpinEdit text to the selected value
            if (comboBox2.SelectedValue != null) cityIDSpinEdit.Text = comboBox2.SelectedValue.ToString();
        }

        private void btn_LocationNew_Click(object sender, EventArgs e)
        {
            // Disable button controls for location operations while adding a new location
            setButtonControls(LocationButtons, 0, false);

            // Simulate a click on the "Add New" button of the binding navigator
            locationBindingNavigatorAddNewItem.PerformClick();
        }

        private void btn_LocationSave_Click(object sender, EventArgs e)
        {
            // Check if the description field is not empty
            if (descriptionTextEdit.Text != "")
            {
                // Enable button controls for location operations
                setButtonControls(LocationButtons, 0, true);

                // Simulate a click on the "Save" button of the binding navigator
                locationBindingNavigatorSaveItem.PerformClick();
            }
            else
            {
                // Show an error message if the description is empty
                set.showMessageError(this, "Please name of the Location in the Description.", "OK");
            }
        }

        private void btn_LocationDelete_Click(object sender, EventArgs e)
        {
            // Enable button controls for location operations
            setButtonControls(LocationButtons, 0, true);

            // Simulate a click on the "Delete" button of the binding navigator
            locationBindingNavigatorDeleteItem.PerformClick();
        }

        private void locationBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the current data entry
            this.Validate();

            // End editing on the location binding source
            this.locationBindingSource.EndEdit();

            // Update all changes in the dataset to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);

            // Show a success message to the user
            set.showMessageSuccess(this, "Successfully Save.");
        }

        private void btn_citySave_Click(object sender, EventArgs e)
        {
            // Check if the description field for the city is not empty
            if (descriptionTextEdit1.Text != "")
            {
                // Enable button controls for city operations
                setButtonControls(CityButtons, 0, true);

                // Simulate a click on the "Save" button of the binding navigator
                cityBindingNavigatorSaveItem.PerformClick();
            }
            else
            {
                // Show an error message if the description is empty
                set.showMessageError(this, "Please name of the City in the Description.", "OK");
            }
        }

        private void cityBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the current data entry
            this.Validate();

            // End editing on the city binding source
            this.cityBindingSource.EndEdit();

            // Update all changes in the dataset to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);

            // Show a success message to the user
            set.showMessageSuccess(this, "Successfully Save.");
        }

        private void btn_cityNew_Click(object sender, EventArgs e)
        {
            try
            {
                // Disable button controls for city operations while adding a new city
                setButtonControls(CityButtons, 0, false);

                // Simulate a click on the "Add New" button of the binding navigator
                cityBindingNavigatorAddNewItem.PerformClick();
            }
            catch (Exception ex)
            {
                // Ignore error due to user's action
            }
        }

        private void btn_cityDelete_Click(object sender, EventArgs e)
        {
            // Simulate a click on the "Delete" button of the binding navigator
            cityBindingNavigatorDeleteItem.PerformClick();

            // Enable button controls for city operations after deletion
            setButtonControls(CityButtons, 0, true);
        }

        private void branchIDSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            // Check if both locationID and cityID fields are not empty
            if (locationIDSpinEdit.Text != "" && cityIDSpinEdit.Text != "")
            {
                // Set the selected value of comboBox1 to the locationID
                comboBox1.SelectedValue = locationIDSpinEdit.Text;

                // Set the selected value of comboBox2 to the cityID
                comboBox2.SelectedValue = cityIDSpinEdit.Text;

                // Disable button controls for branch operations
                setButtonControls(branchButtons, 0, false);
            }
        }

        private void locationIDSpinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            // Check if the locationID field is not empty  and Disable button controls for location operations
            if (locationIDSpinEdit1.Text != "") setButtonControls(LocationButtons, 0, false);
        }

        private void cityIDSpinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            // Check if the cityID field is not empty and Disable button controls for city operations
            if (cityIDSpinEdit1.Text != "") setButtonControls(CityButtons, 0, false);
        }

        private void employeeIDSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void btn_EmployeeNew_Click(object sender, EventArgs e)
        {
            try
            {
                // Simulate a click on the "Add New" button of the employee binding navigator
                employeeBindingNavigatorAddItem.PerformClick();

                // Disable button controls for employee operations while adding a new employee
                setButtonControls(EmployeeButtons, 0, false);
            }
            catch (Exception ex)
            {
                // Show an error message if an exception occurs
                set.showMessageError(this, $"{ex.Message}", "OK");

                // Simulate a click on the "Delete" button of the employee binding navigator
                employeeBindingNavigatorDelete.PerformClick();
            }
        }

        private void employeeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the current data entry
            this.Validate();

            // End the editing of the employee binding source
            this.employeeBindingSource.EndEdit();

            // Update all changes in the dataset to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);

            // Refresh the display of the employee table
            displayEmployeeTableAdapter.Fill(this.dB_BankDataSet.DisplayEmployee);

            // Refresh the DataGridView to reflect the latest changes
            employeeDataGridView.RefreshEdit();

            // Show a success message to the user
            set.showMessageSuccess(this, "Successfully Save.");
        }

        private void btn_EmployeeSave_Click(object sender, EventArgs e)
        {
            // Set the branch ID from the selected value in the branch combo box
            branchIDSpinEdit1.Text = cbo_BranchID.SelectedValue.ToString();

            // Set the location ID from the selected value in the location combo box
            locationIDSpinEdit2.Text = cbo_locationID.SelectedValue.ToString();

            // Set the manager ID from the selected value in the manager combo box
            managerIDSpinEdit.Text = cbo_managerID.SelectedValue.ToString();

            // Set the employee type from the selected value in the employee type combo box
            employeeTypeSpinEdit.Text = cbo_employeeType.SelectedValue.ToString();

            // Simulate a click on the "Save" button of the binding navigator
            employeeBindingNavigatorSaveItem.PerformClick();
        }
        private void employeeDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Set the position of the employee binding navigator to the currently selected row in the DataGridView
            this.employeeBindingNavigator.BindingSource.Position = employeeDataGridView.CurrentRow.Index;

            // Set the selected values in the combo boxes based on the current employee's data
            cbo_BranchID.SelectedValue = branchIDSpinEdit1.Text;
            cbo_locationID.SelectedValue = Int32.Parse(locationIDSpinEdit2.Text.ToString());
            cbo_managerID.SelectedValue = managerIDSpinEdit.Text;
            cbo_employeeType.SelectedValue = employeeTypeSpinEdit.Text;

            // Disable button controls for employee operations
            setButtonControls(EmployeeButtons, 0, false);
        }

        private void branchDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Set the position of the branch binding navigator to the currently selected row in the DataGridView
            this.branchBindingNavigator.BindingSource.Position = branchDataGridView.CurrentRow.Index;

            // Disable button controls for branch operations
            setButtonControls(branchButtons, 0, false);
        }

        private void btn_nav_manager_Click(object sender, EventArgs e)
        {
            // Set navigation buttons based on the sender (the button clicked)
            setNavButtons(navButtons, sender);

            // Show the panel for managers, filling the parent container
            set.showPanel(pnl_MI_Managers, this, DockStyle.Fill);

            // Enable button controls for manager operations
            setButtonControls(ManagersButtons, 0, true);
        }

        private void ManagerbindingNavigatorSave_Click(object sender, EventArgs e)
        {
            // Validate the current data entry
            this.Validate();

            // End the editing of the manager binding source
            this.managerBindingSource.EndEdit();

            // Update all changes in the dataset to the database
            this.tableAdapterManager.UpdateAll(this.dB_BankDataSet);

            // Show a success message to the user
            set.showMessageSuccess(this, "Successfully Save.");

            // Enable button controls for manager operations
            setButtonControls(ManagersButtons, 0, true);
        }

        private void btn_ManagerNew_Click(object sender, EventArgs e)
        {
            try
            {
                // Disable button controls for manager operations
                setButtonControls(ManagersButtons, 0, false);

                // Simulate a click on the "Add New" button of the manager binding navigator
                ManagerbindingNavigatorAddItem.PerformClick();
            }
            catch (Exception ex)
            {
                // Ignore error due to user's action
            }
        }

        private void btn_ManagerSave_Click(object sender, EventArgs e)
        {
            // Simulate a click on the "Save" button of the manager binding navigator
            ManagerbindingNavigatorSave.PerformClick();
        }

        private void btn_ManagerDelete_Click(object sender, EventArgs e)
        {
            // Simulate a click on the "Delete" button of the manager binding navigator
            ManagerbindingNavigatorDeleteItem.PerformClick();
        }

        private void btn_EmployeeDelete_Click(object sender, EventArgs e)
        {
            // Simulate a click on the "Delete" button of the employee binding navigator
            employeeBindingNavigatorDelete.PerformClick();
        }

        private void btn_nav_aboutus_Click(object sender, EventArgs e)
        {
            // Set navigation buttons based on the sender (the button clicked)
            setNavButtons(navButtons, sender);

            // Show the "About Us" panel, filling the parent container
            set.showPanel(pnl_MI_about, this, DockStyle.Fill);
        }

        private void cbo_reports_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected report type from the combo box
            string str = cbo_reports.Text;

            // Generate the appropriate report based on the selected item
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
                XtraReport2 report1 = new XtraReport2();
                report1.DataSource = GetDataBranch();
                documentViewer1.DocumentSource = report1;
                report1.CreateDocument();
                printTool = new ReportPrintTool(report1);
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
            // Show the print preview dialog for the generated report
            printTool.ShowPreviewDialog();
        }


    }
}
