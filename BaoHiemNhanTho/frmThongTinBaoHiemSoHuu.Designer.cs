namespace BaoHiemNhanTho
{
    partial class frmThongTinBaoHiemSoHuu
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
            this.dataGridViewInsurancePolicies = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.comboBoxCustomers = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInsurancePolicies)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewInsurancePolicies
            // 
            this.dataGridViewInsurancePolicies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInsurancePolicies.Location = new System.Drawing.Point(23, 140);
            this.dataGridViewInsurancePolicies.Name = "dataGridViewInsurancePolicies";
            this.dataGridViewInsurancePolicies.RowHeadersWidth = 51;
            this.dataGridViewInsurancePolicies.RowTemplate.Height = 24;
            this.dataGridViewInsurancePolicies.Size = new System.Drawing.Size(1006, 298);
            this.dataGridViewInsurancePolicies.TabIndex = 0;
            // 
            // comboBoxCustomers
            // 
            this.comboBoxCustomers.FormattingEnabled = true;
            this.comboBoxCustomers.Location = new System.Drawing.Point(440, 93);
            this.comboBoxCustomers.Name = "comboBoxCustomers";
            this.comboBoxCustomers.Size = new System.Drawing.Size(589, 24);
            this.comboBoxCustomers.TabIndex = 1;
            this.comboBoxCustomers.SelectedIndexChanged += new System.EventHandler(this.comboBoxCustomers_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(166, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(680, 42);
            this.label7.TabIndex = 29;
            this.label7.Text = "Quản Lý Thông Tin Bảo Hiểm Sở Hữu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(19, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 20);
            this.label1.TabIndex = 30;
            this.label1.Text = "Chọn Thông Tin Người Sở Hữu Bảo Hiểm";
            // 
            // frmThongTinBaoHiemSoHuu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBoxCustomers);
            this.Controls.Add(this.dataGridViewInsurancePolicies);
            this.Name = "frmThongTinBaoHiemSoHuu";
            this.Text = "frmThongTinBaoHiemSoHuu";
            this.Load += new System.EventHandler(this.frmThongTinBaoHiemSoHuu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInsurancePolicies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewInsurancePolicies;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox comboBoxCustomers;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
    }
}