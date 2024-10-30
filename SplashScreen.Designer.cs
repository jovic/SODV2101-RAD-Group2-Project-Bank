namespace Bank
{
    partial class SplashScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuProgressBar1 = new Bunifu.UI.WinForms.BunifuProgressBar();
            this.bunifuPictureBox1 = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 30;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuProgressBar1
            // 
            this.bunifuProgressBar1.AllowAnimations = false;
            this.bunifuProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuProgressBar1.Animation = 0;
            this.bunifuProgressBar1.AnimationSpeed = 220;
            this.bunifuProgressBar1.AnimationStep = 10;
            this.bunifuProgressBar1.BackColor = System.Drawing.Color.Gainsboro;
            this.bunifuProgressBar1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuProgressBar1.BackgroundImage")));
            this.bunifuProgressBar1.BorderColor = System.Drawing.Color.Navy;
            this.bunifuProgressBar1.BorderRadius = 9;
            this.bunifuProgressBar1.BorderThickness = 1;
            this.bunifuProgressBar1.Location = new System.Drawing.Point(30, 354);
            this.bunifuProgressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bunifuProgressBar1.Maximum = 100;
            this.bunifuProgressBar1.MaximumValue = 100;
            this.bunifuProgressBar1.Minimum = 0;
            this.bunifuProgressBar1.MinimumValue = 0;
            this.bunifuProgressBar1.Name = "bunifuProgressBar1";
            this.bunifuProgressBar1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.bunifuProgressBar1.ProgressBackColor = System.Drawing.Color.Gainsboro;
            this.bunifuProgressBar1.ProgressColorLeft = System.Drawing.Color.DodgerBlue;
            this.bunifuProgressBar1.ProgressColorRight = System.Drawing.Color.DodgerBlue;
            this.bunifuProgressBar1.Size = new System.Drawing.Size(317, 29);
            this.bunifuProgressBar1.TabIndex = 0;
            this.bunifuProgressBar1.Value = 50;
            this.bunifuProgressBar1.ValueByTransition = 50;
            // 
            // bunifuPictureBox1
            // 
            this.bunifuPictureBox1.AllowFocused = false;
            this.bunifuPictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bunifuPictureBox1.AutoSizeHeight = true;
            this.bunifuPictureBox1.BorderRadius = 0;
            this.bunifuPictureBox1.Image = global::Bank.Properties.Resources.S_K_S;
            this.bunifuPictureBox1.IsCircle = true;
            this.bunifuPictureBox1.Location = new System.Drawing.Point(30, 29);
            this.bunifuPictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bunifuPictureBox1.Name = "bunifuPictureBox1";
            this.bunifuPictureBox1.Size = new System.Drawing.Size(317, 317);
            this.bunifuPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuPictureBox1.TabIndex = 1;
            this.bunifuPictureBox1.TabStop = false;
            this.bunifuPictureBox1.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Square;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei Light", 6.8F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(28, 393);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(322, 34);
            this.label4.TabIndex = 6;
            this.label4.Text = "by Group 2\r\n@ 2024 SKS Bank. All rights reserved";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 443);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bunifuProgressBar1);
            this.Controls.Add(this.bunifuPictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SplashScreen";
            this.Text = "SplashScreen";
            ((System.ComponentModel.ISupportInitialize)(this.bunifuPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.UI.WinForms.BunifuProgressBar bunifuProgressBar1;
        private Bunifu.UI.WinForms.BunifuPictureBox bunifuPictureBox1;
        private System.Windows.Forms.Label label4;
    }
}