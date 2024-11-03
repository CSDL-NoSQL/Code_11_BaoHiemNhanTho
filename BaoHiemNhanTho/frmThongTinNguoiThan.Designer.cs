namespace BaoHiemNhanTho
{
    partial class frmThongTinNguoiThan
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
            this.comboBoxKhachHang = new System.Windows.Forms.ComboBox();
            this.dataGridViewNguoiThan = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridViewInsurancePolicies = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCoverageAmount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPremium = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPolicyType = new System.Windows.Forms.TextBox();
            this.aa = new System.Windows.Forms.Label();
            this.txtPolicyNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtPercentage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRelationship = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBeneficiaryName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonEditPolicy = new System.Windows.Forms.Button();
            this.buttonDeletePolicy = new System.Windows.Forms.Button();
            this.buttonAddBeneficiary = new System.Windows.Forms.Button();
            this.buttonEditBeneficiary = new System.Windows.Forms.Button();
            this.buttonDeleteBeneficiary = new System.Windows.Forms.Button();
            this.buttonAddPolicy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNguoiThan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInsurancePolicies)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxKhachHang
            // 
            this.comboBoxKhachHang.FormattingEnabled = true;
            this.comboBoxKhachHang.Location = new System.Drawing.Point(34, 372);
            this.comboBoxKhachHang.Name = "comboBoxKhachHang";
            this.comboBoxKhachHang.Size = new System.Drawing.Size(663, 24);
            this.comboBoxKhachHang.TabIndex = 0;
            this.comboBoxKhachHang.SelectedIndexChanged += new System.EventHandler(this.comboBoxKhachHang_SelectedIndexChanged);
            // 
            // dataGridViewNguoiThan
            // 
            this.dataGridViewNguoiThan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNguoiThan.Location = new System.Drawing.Point(734, 402);
            this.dataGridViewNguoiThan.Name = "dataGridViewNguoiThan";
            this.dataGridViewNguoiThan.RowHeadersWidth = 51;
            this.dataGridViewNguoiThan.RowTemplate.Height = 24;
            this.dataGridViewNguoiThan.Size = new System.Drawing.Size(395, 257);
            this.dataGridViewNguoiThan.TabIndex = 1;
            this.dataGridViewNguoiThan.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewNguoiThan_RowValidating);
            this.dataGridViewNguoiThan.SelectionChanged += new System.EventHandler(this.dataGridViewNguoiThan_SelectionChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(355, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(575, 42);
            this.label7.TabIndex = 32;
            this.label7.Text = "Quản lý bảo hiểm và người thân";
            // 
            // dataGridViewInsurancePolicies
            // 
            this.dataGridViewInsurancePolicies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInsurancePolicies.Location = new System.Drawing.Point(34, 402);
            this.dataGridViewInsurancePolicies.Name = "dataGridViewInsurancePolicies";
            this.dataGridViewInsurancePolicies.RowHeadersWidth = 51;
            this.dataGridViewInsurancePolicies.RowTemplate.Height = 24;
            this.dataGridViewInsurancePolicies.Size = new System.Drawing.Size(663, 257);
            this.dataGridViewInsurancePolicies.TabIndex = 33;
            this.dataGridViewInsurancePolicies.SelectionChanged += new System.EventHandler(this.dataGridViewInsurancePolicies_SelectionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCoverageAmount);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtPremium);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtPolicyType);
            this.groupBox1.Controls.Add(this.aa);
            this.groupBox1.Controls.Add(this.txtPolicyNumber);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(34, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(663, 247);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chính Sách Bảo Hiểm";
            // 
            // txtCoverageAmount
            // 
            this.txtCoverageAmount.Location = new System.Drawing.Point(507, 91);
            this.txtCoverageAmount.Name = "txtCoverageAmount";
            this.txtCoverageAmount.Size = new System.Drawing.Size(137, 22);
            this.txtCoverageAmount.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(357, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 16);
            this.label11.TabIndex = 12;
            this.label11.Text = "CoverageAmount";
            // 
            // txtPremium
            // 
            this.txtPremium.Location = new System.Drawing.Point(507, 51);
            this.txtPremium.Name = "txtPremium";
            this.txtPremium.Size = new System.Drawing.Size(137, 22);
            this.txtPremium.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(357, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 16);
            this.label12.TabIndex = 10;
            this.label12.Text = "Premium";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(151, 205);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(493, 22);
            this.dtpEndDate.TabIndex = 9;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(151, 148);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(493, 22);
            this.dtpStartDate.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(53, 205);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 16);
            this.label9.TabIndex = 6;
            this.label9.Text = "EndDate";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 153);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 16);
            this.label10.TabIndex = 4;
            this.label10.Text = "StartDate";
            // 
            // txtPolicyType
            // 
            this.txtPolicyType.Location = new System.Drawing.Point(151, 91);
            this.txtPolicyType.Name = "txtPolicyType";
            this.txtPolicyType.Size = new System.Drawing.Size(175, 22);
            this.txtPolicyType.TabIndex = 3;
            // 
            // aa
            // 
            this.aa.AutoSize = true;
            this.aa.Location = new System.Drawing.Point(50, 100);
            this.aa.Name = "aa";
            this.aa.Size = new System.Drawing.Size(76, 16);
            this.aa.TabIndex = 2;
            this.aa.Text = "PolicyType";
            // 
            // txtPolicyNumber
            // 
            this.txtPolicyNumber.Location = new System.Drawing.Point(151, 51);
            this.txtPolicyNumber.Name = "txtPolicyNumber";
            this.txtPolicyNumber.Size = new System.Drawing.Size(175, 22);
            this.txtPolicyNumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Policy Number";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtPercentage);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtRelationship);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtBeneficiaryName);
            this.groupBox3.Location = new System.Drawing.Point(734, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(395, 247);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Người Thụ Hưởng";
            // 
            // txtPercentage
            // 
            this.txtPercentage.Location = new System.Drawing.Point(186, 174);
            this.txtPercentage.Name = "txtPercentage";
            this.txtPercentage.Size = new System.Drawing.Size(100, 22);
            this.txtPercentage.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Percen Tage";
            // 
            // txtRelationship
            // 
            this.txtRelationship.Location = new System.Drawing.Point(186, 110);
            this.txtRelationship.Name = "txtRelationship";
            this.txtRelationship.Size = new System.Drawing.Size(100, 22);
            this.txtRelationship.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "Relationship";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Benefiname";
            // 
            // txtBeneficiaryName
            // 
            this.txtBeneficiaryName.Location = new System.Drawing.Point(186, 48);
            this.txtBeneficiaryName.Name = "txtBeneficiaryName";
            this.txtBeneficiaryName.Size = new System.Drawing.Size(100, 22);
            this.txtBeneficiaryName.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonEditPolicy);
            this.groupBox2.Controls.Add(this.buttonDeletePolicy);
            this.groupBox2.Controls.Add(this.buttonAddBeneficiary);
            this.groupBox2.Controls.Add(this.buttonEditBeneficiary);
            this.groupBox2.Controls.Add(this.buttonDeleteBeneficiary);
            this.groupBox2.Controls.Add(this.buttonAddPolicy);
            this.groupBox2.Location = new System.Drawing.Point(1146, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 453);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chức Năng";
            // 
            // buttonEditPolicy
            // 
            this.buttonEditPolicy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.buttonEditPolicy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditPolicy.Location = new System.Drawing.Point(43, 104);
            this.buttonEditPolicy.Name = "buttonEditPolicy";
            this.buttonEditPolicy.Size = new System.Drawing.Size(234, 52);
            this.buttonEditPolicy.TabIndex = 8;
            this.buttonEditPolicy.Text = "Sửa Bảo Hiểm";
            this.buttonEditPolicy.UseVisualStyleBackColor = true;
            this.buttonEditPolicy.Click += new System.EventHandler(this.buttonEditPolicy_Click);
            // 
            // buttonDeletePolicy
            // 
            this.buttonDeletePolicy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.buttonDeletePolicy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeletePolicy.Location = new System.Drawing.Point(43, 162);
            this.buttonDeletePolicy.Name = "buttonDeletePolicy";
            this.buttonDeletePolicy.Size = new System.Drawing.Size(234, 52);
            this.buttonDeletePolicy.TabIndex = 7;
            this.buttonDeletePolicy.Text = "Xóa Bảo Hiểm";
            this.buttonDeletePolicy.UseVisualStyleBackColor = true;
            this.buttonDeletePolicy.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonAddBeneficiary
            // 
            this.buttonAddBeneficiary.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.buttonAddBeneficiary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddBeneficiary.Location = new System.Drawing.Point(43, 263);
            this.buttonAddBeneficiary.Name = "buttonAddBeneficiary";
            this.buttonAddBeneficiary.Size = new System.Drawing.Size(234, 52);
            this.buttonAddBeneficiary.TabIndex = 6;
            this.buttonAddBeneficiary.Text = "Thêm Người Thụ Hưởng";
            this.buttonAddBeneficiary.UseVisualStyleBackColor = true;
            this.buttonAddBeneficiary.Click += new System.EventHandler(this.buttonAddBeneficiary_Click);
            // 
            // buttonEditBeneficiary
            // 
            this.buttonEditBeneficiary.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.buttonEditBeneficiary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditBeneficiary.Location = new System.Drawing.Point(43, 321);
            this.buttonEditBeneficiary.Name = "buttonEditBeneficiary";
            this.buttonEditBeneficiary.Size = new System.Drawing.Size(234, 52);
            this.buttonEditBeneficiary.TabIndex = 3;
            this.buttonEditBeneficiary.Text = "Sửa Người Thụ Hưởng";
            this.buttonEditBeneficiary.UseVisualStyleBackColor = true;
            this.buttonEditBeneficiary.Click += new System.EventHandler(this.buttonEditBeneficiary_Click);
            // 
            // buttonDeleteBeneficiary
            // 
            this.buttonDeleteBeneficiary.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.buttonDeleteBeneficiary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteBeneficiary.Location = new System.Drawing.Point(43, 379);
            this.buttonDeleteBeneficiary.Name = "buttonDeleteBeneficiary";
            this.buttonDeleteBeneficiary.Size = new System.Drawing.Size(234, 52);
            this.buttonDeleteBeneficiary.TabIndex = 2;
            this.buttonDeleteBeneficiary.Text = "Xóa Người Thụ Hưởng";
            this.buttonDeleteBeneficiary.UseVisualStyleBackColor = true;
            this.buttonDeleteBeneficiary.Click += new System.EventHandler(this.button9_Click);
            // 
            // buttonAddPolicy
            // 
            this.buttonAddPolicy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.buttonAddPolicy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddPolicy.Location = new System.Drawing.Point(43, 48);
            this.buttonAddPolicy.Name = "buttonAddPolicy";
            this.buttonAddPolicy.Size = new System.Drawing.Size(234, 52);
            this.buttonAddPolicy.TabIndex = 0;
            this.buttonAddPolicy.Text = "Thêm Bảo Hiểm";
            this.buttonAddPolicy.UseVisualStyleBackColor = true;
            this.buttonAddPolicy.Click += new System.EventHandler(this.buttonAddPolicy_Click);
            // 
            // frmThongTinNguoiThan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1882, 929);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewInsurancePolicies);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dataGridViewNguoiThan);
            this.Controls.Add(this.comboBoxKhachHang);
            this.Name = "frmThongTinNguoiThan";
            this.Text = "frmThongTinNguoiThan";
            this.Load += new System.EventHandler(this.frmThongTinNguoiThan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNguoiThan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInsurancePolicies)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxKhachHang;
        private System.Windows.Forms.DataGridView dataGridViewNguoiThan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridViewInsurancePolicies;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCoverageAmount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPremium;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPolicyType;
        private System.Windows.Forms.Label aa;
        private System.Windows.Forms.TextBox txtPolicyNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtRelationship;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBeneficiaryName;
        private System.Windows.Forms.TextBox txtPercentage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonEditBeneficiary;
        private System.Windows.Forms.Button buttonDeleteBeneficiary;
        private System.Windows.Forms.Button buttonAddPolicy;
        private System.Windows.Forms.Button buttonAddBeneficiary;
        private System.Windows.Forms.Button buttonEditPolicy;
        private System.Windows.Forms.Button buttonDeletePolicy;
    }
}