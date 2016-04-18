namespace slimsim
{
    partial class Form2
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtn_Energy = new System.Windows.Forms.RadioButton();
            this.rbtn_Consumption = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(1537, 379);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtn_Consumption);
            this.panel1.Controls.Add(this.rbtn_Energy);
            this.panel1.Location = new System.Drawing.Point(403, 417);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(420, 81);
            this.panel1.TabIndex = 2;
            // 
            // rbtn_Energy
            // 
            this.rbtn_Energy.AutoSize = true;
            this.rbtn_Energy.Location = new System.Drawing.Point(52, 19);
            this.rbtn_Energy.Name = "rbtn_Energy";
            this.rbtn_Energy.Size = new System.Drawing.Size(120, 17);
            this.rbtn_Energy.TabIndex = 0;
            this.rbtn_Energy.TabStop = true;
            this.rbtn_Energy.Text = "Sensor node energy";
            this.rbtn_Energy.UseVisualStyleBackColor = true;
            // 
            // rbtn_Consumption
            // 
            this.rbtn_Consumption.AutoSize = true;
            this.rbtn_Consumption.Location = new System.Drawing.Point(221, 19);
            this.rbtn_Consumption.Name = "rbtn_Consumption";
            this.rbtn_Consumption.Size = new System.Drawing.Size(142, 17);
            this.rbtn_Consumption.TabIndex = 1;
            this.rbtn_Consumption.TabStop = true;
            this.rbtn_Consumption.Text = "Energy consumption rate";
            this.rbtn_Consumption.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1561, 556);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtn_Consumption;
        private System.Windows.Forms.RadioButton rbtn_Energy;
    }
}