using Bunifu.Framework.UI;
using Bunifu.UI.WinForms;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bank
{
    internal class Settings
    {

        public Settings()
        {

        }

        public void setForm(Form frm)
        {
            // Create a new instance of BunifuElipse for rounded corners
            BunifuElipse bunifuElipse = new BunifuElipse();

            // Check if the provided form is not null
            if (frm != null)
            {
                // Set the font, background color, and other properties of the form
                frm.Font = new Font("Segoe UI", 10, FontStyle.Regular); // Set font style
                frm.BackColor = Color.White; // Set background color
                frm.StartPosition = FormStartPosition.CenterScreen; // Center the form on the screen
                frm.WindowState = FormWindowState.Maximized; // Maximize the form

                // Apply the elipse effect to the form
                bunifuElipse.TargetControl = frm;
                bunifuElipse.ElipseRadius = 25; // Set the radius for rounded corners
            }
        }

        public void setElipse(Object obj, int elipseRadius)
        {
            // Create a new instance of BunifuElipse for rounded corners
            BunifuElipse bunifuElipse = new BunifuElipse();

            // Check if the provided object is not null
            if (obj != null)
            {
                // Set the target control for the elipse effect
                bunifuElipse.TargetControl = (Control)obj;
                bunifuElipse.ElipseRadius = elipseRadius; // Set the elipse radius
            }
        }

        public void showMessageSuccess(Form frm, String msg)
        {
            // Create a new instance of BunifuSnackbar for displaying messages
            BunifuSnackbar snackbar = new BunifuSnackbar();

            // Show a success message using the snackbar
            snackbar.Show(frm, msg, BunifuSnackbar.MessageTypes.Success, 2000, "", BunifuSnackbar.Positions.MiddleCenter);
        }

        public void showMessageError(Form frm, String msg, String actionButton)
        {
            // Create a new instance of BunifuSnackbar for displaying messages
            BunifuSnackbar snackbar = new BunifuSnackbar();

            // Show an error message with an action button using the snackbar
            snackbar.Show(frm, msg, BunifuSnackbar.MessageTypes.Error, 3000, actionButton, BunifuSnackbar.Positions.MiddleCenter);
        }

        public void showPanel(Panel pnl, Form frm, DockStyle dockStyle)
        {
            // Set the docking style of the panel
            if (dockStyle == DockStyle.Fill)
                pnl.Dock = DockStyle.Fill; // Fill the parent container
            else
                // Center the panel in the form
                pnl.Location = new Point(frm.Width / 2 - pnl.Size.Width / 2, frm.Height / 2 - pnl.Size.Height / 2);

            // Make the panel visible and bring it to the front
            pnl.Visible = true;
            pnl.Show();
            pnl.BringToFront();
        }

        public void NavButtonIdle(Object obj)
        {
            // Cast the object to a Button
            Button btn = (Button)obj;

            // Reset button location
            btn.Location = new Point(0, 0);

            // Set button appearance for idle state
            btn.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight; // Change color on mouse over
            btn.FlatAppearance.MouseDownBackColor = Color.White; // Change color on mouse down
            btn.BackColor = Color.WhiteSmoke; // Set background color
        }

        public void NavButtonActive(Object obj)
        {
            // Cast the object to a Button
            Button btn = (Button)obj;

            // Adjust button location to indicate active state
            btn.Location = new Point(5, 0);
            btn.BackColor = Color.Gainsboro; // Set active background color
        }

        public void NavButtonEnabler(Button[] btnArray, bool enabler)
        {
            // Enable or disable each button in the provided array
            for (int i = 0; i < btnArray.Length; i++)
                btnArray[i].Enabled = enabler; // Set the enabled state of the button
        }
        internal void setDataGridViews(DataGridView[] dataGridViews)
        {
            // Iterate through each DataGridView in the provided array
            for (int i = 0; i < dataGridViews.Length; i++)
            {
                // Set various properties for each DataGridView
                dataGridViews[i].SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Select full rows
                dataGridViews[i].AllowUserToAddRows = false; // Disable adding new rows
                dataGridViews[i].AllowUserToDeleteRows = false; // Disable deleting rows
                dataGridViews[i].AllowUserToOrderColumns = false; // Disable column reordering
                dataGridViews[i].AllowUserToResizeRows = false; // Disable row resizing
                dataGridViews[i].DefaultCellStyle.BackColor = Color.White; // Set default cell background color
                dataGridViews[i].AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray; // Set alternating row color
                dataGridViews[i].AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Fill columns to fit
                dataGridViews[i].RowHeadersVisible = false; // Hide row headers
                dataGridViews[i].EditMode = DataGridViewEditMode.EditProgrammatically; // Set edit mode
                dataGridViews[i].Font = new Font("Segoe UI", 9); // Set font for the DataGridView
                dataGridViews[i].ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); // Set font for column headers
            }
        }

        public void PanelLayouts(Panel[] pnl, Color color)
        {
            // Iterate through each Panel in the provided array
            for (int i = 0; i < pnl.Length; i++)
            {
                setElipse(pnl[i], 10); // Apply rounded corners
                pnl[i].BackColor = color; // Set background color
                pnl[i].AutoScroll = false; // Disable auto-scrolling
            }
        }

        public void SinglePanelLayout(Panel pnl)
        {
            setElipse(pnl, 15); // Apply rounded corners with a larger radius
            pnl.BackColor = Color.WhiteSmoke; // Set background color to a light gray
        }

        public void NavButtonIdle_Simple(SimpleButton obj)
        {
            // Set the appearance of a SimpleButton in its idle state
            obj.AppearanceHovered.BackColor = SystemColors.ControlLight; // Change color on hover
            obj.AppearancePressed.BackColor = Color.White; // Change color when pressed
            obj.BackColor = Color.Transparent; // Set background to transparent
            obj.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light; // Set paint style
        }

        public void NavButtonActive_Simple(SimpleButton btn)
        {
            // Set the appearance of a SimpleButton in its active state
            btn.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Default; // Set default paint style
            btn.Appearance.BackColor = Color.White; // Set background color to white
        }

        public int DecimaltoInt(string amount)
        {
            // Convert a string representation of a decimal to an integer
            decimal amt = Decimal.Parse(amount.ToString()); // Parse the string to decimal
            string str = amt.ToString(".##"); // Format the decimal to two decimal places
            return Int32.Parse(str); // Convert the formatted string to an integer
        }
    }
}
