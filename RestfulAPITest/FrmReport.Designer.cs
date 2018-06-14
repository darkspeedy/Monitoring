namespace RestfulAPITest
{
    partial class FrmReport
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.mEndDate = new MetroFramework.Controls.MetroDateTime();
            this.mStartDate = new MetroFramework.Controls.MetroDateTime();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cartesianChartReport = new LiveCharts.WinForms.CartesianChart();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.mEndDate);
            this.panel1.Controls.Add(this.mStartDate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1021, 79);
            this.panel1.TabIndex = 0;
            // 
            // mEndDate
            // 
            this.mEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.mEndDate.CustomFormat = "MMMM dd, yyyy HH:mm:ss";
            this.mEndDate.Enabled = false;
            this.mEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mEndDate.Location = new System.Drawing.Point(593, 23);
            this.mEndDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mEndDate.MinimumSize = new System.Drawing.Size(0, 30);
            this.mEndDate.Name = "mEndDate";
            this.mEndDate.Size = new System.Drawing.Size(324, 30);
            this.mEndDate.TabIndex = 12;
            // 
            // mStartDate
            // 
            this.mStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.mStartDate.CustomFormat = "MMMM dd, yyyy HH:mm:ss";
            this.mStartDate.Enabled = false;
            this.mStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mStartDate.Location = new System.Drawing.Point(168, 23);
            this.mStartDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mStartDate.MinimumSize = new System.Drawing.Size(0, 30);
            this.mStartDate.Name = "mStartDate";
            this.mStartDate.Size = new System.Drawing.Size(324, 30);
            this.mStartDate.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label2.Location = new System.Drawing.Point(523, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label1.Location = new System.Drawing.Point(88, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "From";
            // 
            // cartesianChartReport
            // 
            this.cartesianChartReport.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cartesianChartReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChartReport.Location = new System.Drawing.Point(0, 79);
            this.cartesianChartReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cartesianChartReport.Name = "cartesianChartReport";
            this.cartesianChartReport.Size = new System.Drawing.Size(1021, 459);
            this.cartesianChartReport.TabIndex = 1;
            this.cartesianChartReport.Text = "cartesianChartReport";
            // 
            // FrmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 538);
            this.Controls.Add(this.cartesianChartReport);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmReport";
            this.Text = "Report";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private LiveCharts.WinForms.CartesianChart cartesianChartReport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroDateTime mEndDate;
        private MetroFramework.Controls.MetroDateTime mStartDate;
    }
}