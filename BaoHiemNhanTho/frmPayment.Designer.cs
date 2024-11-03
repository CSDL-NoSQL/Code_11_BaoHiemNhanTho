namespace BaoHiemNhanTho
{
    partial class frmPayment
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
            this.dataGridViewPayment = new System.Windows.Forms.DataGridView();
            this.comboBoxKhachHang = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonAddBeneficiary = new System.Windows.Forms.Button();
            this.buttonEditBeneficiary = new System.Windows.Forms.Button();
            this.buttonDeleteBeneficiary = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxPaymentMethod = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPaymentId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPayment)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewPayment
            // 
            this.dataGridViewPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPayment.Location = new System.Drawing.Point(57, 398);
            this.dataGridViewPayment.Name = "dataGridViewPayment";
            this.dataGridViewPayment.RowHeadersWidth = 51;
            this.dataGridViewPayment.RowTemplate.Height = 24;
            this.dataGridViewPayment.Size = new System.Drawing.Size(674, 188);
            this.dataGridViewPayment.TabIndex = 35;
            this.dataGridViewPayment.SelectionChanged += new System.EventHandler(this.dataGridViewPayment_SelectionChanged);
            // 
            // comboBoxKhachHang
            // 
            this.comboBoxKhachHang.FormattingEnabled = true;
            this.comboBoxKhachHang.Location = new System.Drawing.Point(57, 332);
            this.comboBoxKhachHang.Name = "comboBoxKhachHang";
            this.comboBoxKhachHang.Size = new System.Drawing.Size(674, 24);
            this.comboBoxKhachHang.TabIndex = 34;
            this.comboBoxKhachHang.SelectedIndexChanged += new System.EventHandler(this.comboBoxKhachHang_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonAddBeneficiary);
            this.groupBox2.Controls.Add(this.buttonEditBeneficiary);
            this.groupBox2.Controls.Add(this.buttonDeleteBeneficiary);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(745, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 328);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chức Năng";
            // 
            // buttonAddBeneficiary
            // 
            this.buttonAddBeneficiary.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.buttonAddBeneficiary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddBeneficiary.Location = new System.Drawing.Point(26, 55);
            this.buttonAddBeneficiary.Name = "buttonAddBeneficiary";
            this.buttonAddBeneficiary.Size = new System.Drawing.Size(149, 52);
            this.buttonAddBeneficiary.TabIndex = 6;
            this.buttonAddBeneficiary.Text = "Thêm Thanh Toán";
            this.buttonAddBeneficiary.UseVisualStyleBackColor = true;
            this.buttonAddBeneficiary.Click += new System.EventHandler(this.buttonAddBeneficiary_Click);
            // 
            // buttonEditBeneficiary
            // 
            this.buttonEditBeneficiary.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.buttonEditBeneficiary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditBeneficiary.Location = new System.Drawing.Point(26, 139);
            this.buttonEditBeneficiary.Name = "buttonEditBeneficiary";
            this.buttonEditBeneficiary.Size = new System.Drawing.Size(149, 52);
            this.buttonEditBeneficiary.TabIndex = 3;
            this.buttonEditBeneficiary.Text = "Sửa Thanh Toán";
            this.buttonEditBeneficiary.UseVisualStyleBackColor = true;
            this.buttonEditBeneficiary.Click += new System.EventHandler(this.buttonEditBeneficiary_Click);
            // 
            // buttonDeleteBeneficiary
            // 
            this.buttonDeleteBeneficiary.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.buttonDeleteBeneficiary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteBeneficiary.Location = new System.Drawing.Point(26, 232);
            this.buttonDeleteBeneficiary.Name = "buttonDeleteBeneficiary";
            this.buttonDeleteBeneficiary.Size = new System.Drawing.Size(149, 52);
            this.buttonDeleteBeneficiary.TabIndex = 2;
            this.buttonDeleteBeneficiary.Text = "Xóa Thanh Toán";
            this.buttonDeleteBeneficiary.UseVisualStyleBackColor = true;
            this.buttonDeleteBeneficiary.Click += new System.EventHandler(this.buttonDeleteBeneficiary_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dateTimePickerPaymentDate);
            this.groupBox3.Controls.Add(this.textBoxPaymentMethod);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBoxAmount);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.textBoxPaymentId);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox3.Location = new System.Drawing.Point(57, 72);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(674, 234);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông Tin Giao Dịch";
            // 
            // dateTimePickerPaymentDate
            // 
            this.dateTimePickerPaymentDate.Location = new System.Drawing.Point(313, 113);
            this.dateTimePickerPaymentDate.Name = "dateTimePickerPaymentDate";
            this.dateTimePickerPaymentDate.Size = new System.Drawing.Size(334, 26);
            this.dateTimePickerPaymentDate.TabIndex = 28;
            // 
            // textBoxPaymentMethod
            // 
            this.textBoxPaymentMethod.Location = new System.Drawing.Point(313, 193);
            this.textBoxPaymentMethod.Name = "textBoxPaymentMethod";
            this.textBoxPaymentMethod.Size = new System.Drawing.Size(334, 26);
            this.textBoxPaymentMethod.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 20);
            this.label6.TabIndex = 24;
            this.label6.Text = "PaymentMethod";
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.Location = new System.Drawing.Point(313, 154);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(334, 26);
            this.textBoxAmount.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "PaymentDate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "PaymentId";
            // 
            // textBoxPaymentId
            // 
            this.textBoxPaymentId.Location = new System.Drawing.Point(313, 73);
            this.textBoxPaymentId.Name = "textBoxPaymentId";
            this.textBoxPaymentId.Size = new System.Drawing.Size(334, 26);
            this.textBoxPaymentId.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(280, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(380, 42);
            this.label7.TabIndex = 41;
            this.label7.Text = "Thông Tin Giao Dịch";
            // 
            // frmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 617);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dataGridViewPayment);
            this.Controls.Add(this.comboBoxKhachHang);
            this.Name = "frmPayment";
            this.Text = "Thông TIn Giao Dịch";
            this.Load += new System.EventHandler(this.frmPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPayment)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPayment;
        private System.Windows.Forms.ComboBox comboBoxKhachHang;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonAddBeneficiary;
        private System.Windows.Forms.Button buttonEditBeneficiary;
        private System.Windows.Forms.Button buttonDeleteBeneficiary;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dateTimePickerPaymentDate;
        private System.Windows.Forms.TextBox textBoxPaymentMethod;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPaymentId;
        private System.Windows.Forms.Label label7;
    }
}