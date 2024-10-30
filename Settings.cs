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
            BunifuElipse bunifuElipse = new BunifuElipse();
            if (frm != null)
            {
                frm.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                frm.BackColor = Color.White;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Maximized;
                bunifuElipse.TargetControl = frm;
                bunifuElipse.ElipseRadius = 25;
            }
        }



        public void setElipse(Object obj, int elipseRadius)
        {
            BunifuElipse bunifuElipse = new BunifuElipse();

            if (obj != null)
            {
                bunifuElipse.TargetControl = (Control)obj;
                bunifuElipse.ElipseRadius = elipseRadius;
            }
        }

        public void showMessageSuccess(Form frm, String msg)
        {
            BunifuSnackbar snackbar = new BunifuSnackbar();

            snackbar.Show(frm, msg, BunifuSnackbar.MessageTypes.Success, 2000, "", BunifuSnackbar.Positions.MiddleCenter);
        }

        public void showMessageError(Form frm, String msg, String actionButton)
        {
            BunifuSnackbar snackbar = new BunifuSnackbar();
            snackbar.Show(frm, msg, BunifuSnackbar.MessageTypes.Error, 3000, actionButton, BunifuSnackbar.Positions.MiddleCenter);
        }

        public void showPanel(Panel pnl, Form frm, DockStyle dockStyle)
        {
            if (dockStyle == DockStyle.Fill)
                pnl.Dock = DockStyle.Fill;
            else
                pnl.Location = new Point(frm.Width / 2 - pnl.Size.Width / 2, frm.Height / 2 - pnl.Size.Height / 2);

            pnl.Visible = true;
            pnl.Show();
            pnl.BringToFront();
        }

        public void NavButtonIdle(Object obj)
        {

            Button btn = (Button)obj;
            btn.Location = new Point(0, 0);

            btn.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
            btn.FlatAppearance.MouseDownBackColor = Color.White;
            btn.BackColor = Color.WhiteSmoke;
        }

        public void NavButtonActive(Object obj)
        {
            Button btn = (Button)obj;
            btn.Location = new Point(5, 0);
            btn.BackColor = Color.Gainsboro;

        }

        public void NavButtonEnabler(Button[] btnArray, bool enabler)
        {
            for (int i = 0; i < btnArray.Length; i++)
                btnArray[i].Enabled = enabler;
        }

        internal void setDataGridViews(DataGridView[] dataGridViews)
        {
            for (int i = 0; i < dataGridViews.Length; i++)
            {
                dataGridViews[i].SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViews[i].AllowUserToAddRows = false;
                dataGridViews[i].AllowUserToDeleteRows = false;
                dataGridViews[i].AllowUserToOrderColumns = false;
                dataGridViews[i].AllowUserToResizeRows = false;
                dataGridViews[i].DefaultCellStyle.BackColor = Color.White;
                dataGridViews[i].AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViews[i].AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViews[i].RowHeadersVisible = false;
                dataGridViews[i].EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridViews[i].Font = new Font("Segoe UI", 9);
                dataGridViews[i].ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            }
        }

        public void PanelLayouts(Panel[] pnl, Color color)
        {
            for (int i = 0; i < pnl.Length; i++)
            {
                setElipse(pnl[i], 10);
                pnl[i].BackColor = color;
                pnl[i].AutoScroll = false;
            }
        }

        public void SinglePanelLayout(Panel pnl)
        {
            setElipse(pnl, 15);
            pnl.BackColor = Color.WhiteSmoke;
        }

        public void NavButtonIdle_Simple(SimpleButton obj)
        {
            obj.AppearanceHovered.BackColor = SystemColors.ControlLight;
            obj.AppearancePressed.BackColor = Color.White;
            obj.BackColor = Color.Transparent;
            obj.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
        }

        public void NavButtonActive_Simple(SimpleButton btn)
        {
            btn.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Default;
            btn.Appearance.BackColor = Color.White;

        }

        public int DecimaltoInt(string amount)
        {
            decimal amt = Decimal.Parse(amount.ToString());
            string str = amt.ToString(".##");
            return Int32.Parse(str);
        }
    }
}
