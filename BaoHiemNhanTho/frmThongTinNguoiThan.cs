    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
using System.Windows.Forms;
namespace BaoHiemNhanTho
{
        public partial class frmThongTinNguoiThan : Form
        {
            private IMongoCollection<Customer> _customerCollection;
            private List<Customer> _customers;
            private List<Beneficiary> _editedBeneficiaries = new List<Beneficiary>();
            private List<Beneficiary> _beneficiaries = new List<Beneficiary>();
            public frmThongTinNguoiThan()
            {
                InitializeComponent();
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("BHNT");
                _customerCollection = database.GetCollection<Customer>("BaoHiemNhanTho");
            }

            private void frmThongTinNguoiThan_Load(object sender, EventArgs e)
            {
                LoadCustomersIntoComboBox();

                // Cho phép thêm dòng mới trên DataGridView
                dataGridViewInsurancePolicies.AllowUserToAddRows = true;
                dataGridViewNguoiThan.AllowUserToAddRows = true;

                // Gán sự kiện CellValueChanged để theo dõi thay đổi trên các DataGridView
                dataGridViewInsurancePolicies.CellValueChanged += DataGridViewInsurancePolicies_CellValueChanged;
                dataGridViewNguoiThan.CellValueChanged += DataGridViewNguoiThan_CellValueChanged;
            }

            private void DataGridViewInsurancePolicies_CellValueChanged(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0 && e.RowIndex < dataGridViewInsurancePolicies.Rows.Count)
                {
                    var row = dataGridViewInsurancePolicies.Rows[e.RowIndex];

                    // Lấy PolicyId và PolicyName từ hàng được chỉnh sửa
                    var policyId = row.Cells["PolicyId"].Value?.ToString();
                    var policyName = row.Cells["PolicyName"].Value?.ToString();

                    // Cập nhật thông tin cho bảo hiểm trong danh sách _customers nếu cần thiết
                    var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
                    var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);

                    if (selectedCustomer != null)
                    {
                        // Tìm bảo hiểm tương ứng trong danh sách insurancePolicies
                        var selectedPolicy = selectedCustomer.insurancePolicies
                            .FirstOrDefault(p => p.policyId.ToString() == policyId);

                        if (selectedPolicy != null)
                        {
                            // Cập nhật loại bảo hiểm
                            selectedPolicy.policyType = policyName;

                            // Nếu cần, cập nhật lại xuống cơ sở dữ liệu
                            var filter = Builders<Customer>.Filter.Eq("customerId", selectedCustomerId);
                            var update = Builders<Customer>.Update.Set("insurancePolicies", selectedCustomer.insurancePolicies);
                            _customerCollection.UpdateOne(filter, update);
                        }
                    }
                }
            }

     
            private void dataGridViewNguoiThan_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
            {
                var row = dataGridViewNguoiThan.Rows[e.RowIndex];

                if (row.IsNewRow) return;

                var name = row.Cells["Name"].Value?.ToString();
                var relationship = row.Cells["Relationship"].Value?.ToString();
                var percentage = row.Cells["Percentage"].Value != null ? Convert.ToDouble(row.Cells["Percentage"].Value) : 0;

                // Kiểm tra dữ liệu hợp lệ, nếu không thì huỷ thao tác chỉnh sửa
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(relationship) || percentage <= 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin và tỷ lệ phải lớn hơn 0.");
                }
            }

            private void DataGridViewNguoiThan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0 && e.RowIndex < _beneficiaries.Count)
                {
                    var row = dataGridViewNguoiThan.Rows[e.RowIndex];
                    _beneficiaries[e.RowIndex].name = row.Cells["Name"].Value?.ToString();
                    _beneficiaries[e.RowIndex].relationship = row.Cells["Relationship"].Value?.ToString();
                    _beneficiaries[e.RowIndex].percentage = Convert.ToInt32(row.Cells["Percentage"].Value);
                }
            }

            private void LoadCustomersIntoComboBox()
            {
                // Lấy tất cả khách hàng từ MongoDB
                _customers = _customerCollection.Find(_ => true).ToList();

                // Hiển thị danh sách khách hàng trong ComboBox
                comboBoxKhachHang.DataSource = _customers.Select(c => new
                {
                    Display = c.name.firstName + " " + c.name.lastName + " - " + c.phoneNumber,
                    Value = c.customerId
                }).ToList();

                comboBoxKhachHang.DisplayMember = "Display";
                comboBoxKhachHang.ValueMember = "Value";
            }

            private void comboBoxKhachHang_SelectedIndexChanged(object sender, EventArgs e)
            {
            var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();

            if (selectedCustomerId != null)
            {
                var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);

                if (selectedCustomer != null)
                {
                    // Lấy danh sách bảo hiểm của khách hàng và hiển thị trong dataGridViewInsurancePolicies
                    var policies = selectedCustomer.insurancePolicies
                        .Select(p => new
                        {
                            PolicyId = p.policyId.ToString(),           // Mã bảo hiểm
                            PolicyNumber = p.policyNumber,              // Số hợp đồng
                            PolicyType = p.policyType,                  // Loại bảo hiểm
                            StartDate = p.startDate.ToString("dd/MM/yyyy"), // Ngày bắt đầu
                            EndDate = p.endDate.ToString("dd/MM/yyyy"),     // Ngày hết hạn
                            Premium = p.premium,                        // Số tiền phí
                            CoverageAmount = p.coverageAmount           // Số tiền bảo hiểm
                        })
                        .ToList();

                    // Gán dữ liệu cho DataGridView
                    dataGridViewInsurancePolicies.DataSource = policies;
                    dataGridViewInsurancePolicies.ReadOnly = false; // Cho phép chỉnh sửa
                    dataGridViewNguoiThan.DataSource = null; // Xóa dữ liệu cũ
                    _beneficiaries.Clear(); // Xóa danh sách người thụ hưởng cũ
                }
            }

        }

            private void button7_Click(object sender, EventArgs e)
            {
                var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
                if (selectedCustomerId != null)
                {
                    var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);

                    if (selectedCustomer != null)
                    {
                        // Cập nhật danh sách bảo hiểm từ dataGridViewInsurancePolicies
                        selectedCustomer.insurancePolicies = dataGridViewInsurancePolicies.Rows
                            .Cast<DataGridViewRow>()
                            .Where(row => !row.IsNewRow)
                            .Select(row => new InsurancePolicy
                            {
                                policyId = ObjectId.Parse(row.Cells["PolicyId"].Value?.ToString() ?? ObjectId.GenerateNewId().ToString()),
                                policyType = row.Cells["PolicyName"].Value?.ToString(),
                                beneficiaries = selectedCustomer.insurancePolicies
                                    .FirstOrDefault(p => p.policyId == ObjectId.Parse(row.Cells["PolicyId"].Value?.ToString() ?? ObjectId.GenerateNewId().ToString()))
                                    ?.beneficiaries ?? new List<Beneficiary>()
                            })
                            .ToList();

                        // Cập nhật danh sách người thân từ dataGridViewNguoiThan cho bảo hiểm đang chọn
                        var selectedPolicyId = dataGridViewInsurancePolicies.SelectedRows[0].Cells["PolicyId"].Value.ToString();
                        var selectedPolicy = selectedCustomer.insurancePolicies
                            .FirstOrDefault(p => p.policyId == ObjectId.Parse(selectedPolicyId));

                        if (selectedPolicy != null)
                        {
                            selectedPolicy.beneficiaries = dataGridViewNguoiThan.Rows
                                .Cast<DataGridViewRow>()
                                .Where(row => !row.IsNewRow)
                                .Select(row => new Beneficiary
                                {
                                    name = row.Cells["Name"].Value?.ToString(),
                                    relationship = row.Cells["Relationship"].Value?.ToString(),
                                    percentage = Convert.ToInt32(row.Cells["Percentage"].Value)
                                })
                                .ToList();
                        }

                        // Cập nhật xuống MongoDB
                        var filter = Builders<Customer>.Filter.Eq("customerId", selectedCustomerId);
                        var update = Builders<Customer>.Update.Set("insurancePolicies", selectedCustomer.insurancePolicies);
                        _customerCollection.UpdateOne(filter, update);

                        MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            private void dataGridViewInsurancePolicies_SelectionChanged(object sender, EventArgs e)
            {
                if (dataGridViewInsurancePolicies.SelectedRows.Count > 0)
                {
                    var selectedRow = dataGridViewInsurancePolicies.SelectedRows[0];
                    var selectedPolicyId = selectedRow.Cells["PolicyId"].Value.ToString();

                    // Tìm khách hàng đã chọn từ comboBox
                    var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
                    var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);

                    if (selectedCustomer != null)
                    {
                        // Lấy danh sách người thân trong bảo hiểm đang chọn và hiển thị trong dataGridViewNguoiThan
                        var selectedPolicy = selectedCustomer.insurancePolicies
                            .FirstOrDefault(p => p.policyId == ObjectId.Parse(selectedPolicyId));

                        if (selectedPolicy != null)
                        {
                            // Hiển thị thông tin bảo hiểm vào TextBox
                            txtPolicyNumber.Text = selectedPolicy.policyId.ToString();
                            txtPolicyType.Text = selectedPolicy.policyType;
                            dtpStartDate.Text = selectedPolicy.startDate.ToString();
                            dtpEndDate.Text = selectedPolicy.endDate.ToString();    
                            txtCoverageAmount.Text = selectedPolicy.coverageAmount.ToString();
                            txtPremium.Text = selectedPolicy.premium.ToString();
                            // Cập nhật danh sách người thân
                            _beneficiaries = selectedPolicy.beneficiaries.ToList();
                            dataGridViewNguoiThan.DataSource = _beneficiaries
                                .Select(b => new { Name = b.name, Relationship = b.relationship, Percentage = b.percentage })
                                .ToList();
                            dataGridViewNguoiThan.ReadOnly = false; // Cho phép nhập liệu cho DataGridView này
                        }
                    }
                }
            }

        private void dataGridViewNguoiThan_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewNguoiThan.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewNguoiThan.SelectedRows[0];

                // Lấy thông tin từ hàng được chọn
                var name = selectedRow.Cells["Name"].Value?.ToString();
                var relationship = selectedRow.Cells["Relationship"].Value?.ToString();
                var percentage = selectedRow.Cells["Percentage"].Value?.ToString();

                // Cập nhật các TextBox tương ứng
                txtBeneficiaryName.Text = name;
                txtRelationship.Text = relationship;
                txtPercentage.Text = percentage;
            }
            else
            {
                // Nếu không có hàng nào được chọn, xóa các TextBox
                txtBeneficiaryName.Clear();
                txtRelationship.Clear();
                txtPercentage.Clear();
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonAddPolicy_Click(object sender, EventArgs e)
        {
            var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
            if (selectedCustomerId != null)
            {
                var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);

                if (selectedCustomer != null)
                {
                    // Tạo một đối tượng InsurancePolicy mới từ các TextBox
                    var newPolicy = new InsurancePolicy
                    {
                        policyId = ObjectId.GenerateNewId(), // Tạo một ID mới cho bảo hiểm
                        policyNumber = txtPolicyNumber.Text,
                        policyType = txtPolicyType.Text,
                        startDate = dtpStartDate.Value,
                        endDate = dtpEndDate.Value,
                        premium = Convert.ToDecimal(txtPremium.Text),
                        coverageAmount = Convert.ToDecimal(txtCoverageAmount.Text),
                        beneficiaries = new List<Beneficiary>() // Khởi tạo danh sách người thụ hưởng rỗng
                    };

                    // Thêm bảo hiểm vào danh sách
                    selectedCustomer.insurancePolicies.Add(newPolicy);

                    // Cập nhật DataGridView
                    dataGridViewInsurancePolicies.DataSource = null; // Xóa dữ liệu cũ
                    dataGridViewInsurancePolicies.DataSource = selectedCustomer.insurancePolicies
                        .Select(p => new
                        {
                            PolicyId = p.policyId.ToString(),
                            PolicyNumber = p.policyNumber,
                            PolicyType = p.policyType,
                            StartDate = p.startDate.ToString("dd/MM/yyyy"),
                            EndDate = p.endDate.ToString("dd/MM/yyyy"),
                            Premium = p.premium,
                            CoverageAmount = p.coverageAmount
                        })
                        .ToList();

                    // Cập nhật xuống MongoDB
                    var filter = Builders<Customer>.Filter.Eq("customerId", selectedCustomerId);
                    var update = Builders<Customer>.Update.Set("insurancePolicies", selectedCustomer.insurancePolicies);
                    _customerCollection.UpdateOne(filter, update);

                    MessageBox.Show("Thêm bảo hiểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void buttonAddBeneficiary_Click(object sender, EventArgs e)
        {
            var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
            if (selectedCustomerId != null && dataGridViewInsurancePolicies.SelectedRows.Count > 0)
            {
                var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);

                if (selectedCustomer != null)
                {
                    // Lấy mã bảo hiểm đã chọn
                    var selectedPolicyId = dataGridViewInsurancePolicies.SelectedRows[0].Cells["PolicyId"].Value.ToString();
                    var selectedPolicy = selectedCustomer.insurancePolicies.FirstOrDefault(p => p.policyId == ObjectId.Parse(selectedPolicyId));

                    if (selectedPolicy != null)
                    {
                        // Tạo một đối tượng Beneficiary mới từ các TextBox
                        var newBeneficiary = new Beneficiary
                        {
                            name = txtBeneficiaryName.Text,
                            relationship = txtRelationship.Text,
                            percentage = Convert.ToInt32(txtPercentage.Text)
                        };

                        // Thêm người thụ hưởng vào danh sách của bảo hiểm đã chọn
                        selectedPolicy.beneficiaries.Add(newBeneficiary);

                        // Cập nhật DataGridView
                        dataGridViewNguoiThan.DataSource = null; // Xóa dữ liệu cũ
                        dataGridViewNguoiThan.DataSource = selectedPolicy.beneficiaries
                            .Select(b => new { Name = b.name, Relationship = b.relationship, Percentage = b.percentage })
                            .ToList();

                        // Cập nhật xuống MongoDB
                        var filter = Builders<Customer>.Filter.Eq("customerId", selectedCustomerId);
                        var update = Builders<Customer>.Update.Set("insurancePolicies", selectedCustomer.insurancePolicies);
                        _customerCollection.UpdateOne(filter, update);

                        MessageBox.Show("Thêm người thân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void buttonEditBeneficiary_Click(object sender, EventArgs e)
        {
            if (dataGridViewNguoiThan.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewNguoiThan.SelectedRows[0];
                var selectedBeneficiary = _beneficiaries[selectedRow.Index];

                // Cập nhật thông tin người thụ hưởng từ TextBox
                selectedBeneficiary.name = txtBeneficiaryName.Text;
                selectedBeneficiary.relationship = txtRelationship.Text;
                selectedBeneficiary.percentage = Convert.ToInt32(txtPercentage.Text);

                // Cập nhật DataGridView
                dataGridViewNguoiThan.DataSource = null; // Xóa dữ liệu cũ
                dataGridViewNguoiThan.DataSource = _beneficiaries
                    .Select(b => new { Name = b.name, Relationship = b.relationship, Percentage = b.percentage })
                    .ToList();

                // Cập nhật xuống MongoDB
                var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
                var selectedPolicyId = dataGridViewInsurancePolicies.SelectedRows[0].Cells["PolicyId"].Value.ToString();
                var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);
                var selectedPolicy = selectedCustomer.insurancePolicies.FirstOrDefault(p => p.policyId == ObjectId.Parse(selectedPolicyId));

                if (selectedPolicy != null)
                {
                    selectedPolicy.beneficiaries = _beneficiaries;
                    var filter = Builders<Customer>.Filter.Eq("customerId", selectedCustomerId);
                    var update = Builders<Customer>.Update.Set("insurancePolicies", selectedCustomer.insurancePolicies);
                    _customerCollection.UpdateOne(filter, update);
                }

                MessageBox.Show("Sửa thông tin người thân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người thụ hưởng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridViewNguoiThan.SelectedRows.Count > 0)
            {
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa người thụ hưởng đã chọn?",
                                                     "Xác nhận xóa",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    var selectedRow = dataGridViewNguoiThan.SelectedRows[0];
                    var selectedBeneficiaryIndex = selectedRow.Index;

                    // Xóa người thụ hưởng khỏi danh sách
                    _beneficiaries.RemoveAt(selectedBeneficiaryIndex);

                    // Cập nhật DataGridView
                    dataGridViewNguoiThan.DataSource = null; // Xóa dữ liệu cũ
                    dataGridViewNguoiThan.DataSource = _beneficiaries
                        .Select(b => new { Name = b.name, Relationship = b.relationship, Percentage = b.percentage })
                        .ToList();

                    // Cập nhật xuống MongoDB
                    var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
                    var selectedPolicyId = dataGridViewInsurancePolicies.SelectedRows[0].Cells["PolicyId"].Value.ToString();
                    var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);
                    var selectedPolicy = selectedCustomer.insurancePolicies.FirstOrDefault(p => p.policyId == ObjectId.Parse(selectedPolicyId));

                    if (selectedPolicy != null)
                    {
                        selectedPolicy.beneficiaries = _beneficiaries;
                        var filter = Builders<Customer>.Filter.Eq("customerId", selectedCustomerId);
                        var update = Builders<Customer>.Update.Set("insurancePolicies", selectedCustomer.insurancePolicies);
                        _customerCollection.UpdateOne(filter, update);
                    }

                    MessageBox.Show("Xóa người thân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người thụ hưởng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonEditPolicy_Click(object sender, EventArgs e)
        {
            if (dataGridViewInsurancePolicies.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewInsurancePolicies.SelectedRows[0];
                var selectedPolicyId = selectedRow.Cells["PolicyId"].Value.ToString();

                var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
                var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);

                if (selectedCustomer != null)
                {
                    var selectedPolicy = selectedCustomer.insurancePolicies.FirstOrDefault(p => p.policyId == ObjectId.Parse(selectedPolicyId));

                    if (selectedPolicy != null)
                    {
                        // Cập nhật thông tin từ các TextBox vào chính sách được chọn
                        selectedPolicy.policyNumber = txtPolicyNumber.Text;
                        selectedPolicy.policyType = txtPolicyType.Text;
                        selectedPolicy.startDate = dtpStartDate.Value;
                        selectedPolicy.endDate = dtpEndDate.Value;
                        selectedPolicy.premium = Convert.ToDecimal(txtPremium.Text);
                        selectedPolicy.coverageAmount = Convert.ToDecimal(txtCoverageAmount.Text);

                        // Cập nhật xuống DataGridView
                        dataGridViewInsurancePolicies.DataSource = null; // Xóa dữ liệu cũ
                        dataGridViewInsurancePolicies.DataSource = selectedCustomer.insurancePolicies
                            .Select(p => new
                            {
                                PolicyId = p.policyId.ToString(),
                                PolicyNumber = p.policyNumber,
                                PolicyType = p.policyType,
                                StartDate = p.startDate.ToString("dd/MM/yyyy"),
                                EndDate = p.endDate.ToString("dd/MM/yyyy"),
                                Premium = p.premium,
                                CoverageAmount = p.coverageAmount
                            })
                            .ToList();

                        // Cập nhật xuống MongoDB
                        var filter = Builders<Customer>.Filter.Eq("customerId", selectedCustomerId);
                        var update = Builders<Customer>.Update.Set("insurancePolicies", selectedCustomer.insurancePolicies);
                        _customerCollection.UpdateOne(filter, update);

                        MessageBox.Show("Cập nhật thông tin chính sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chính sách bảo hiểm để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewInsurancePolicies.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa chính sách bảo hiểm này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var selectedRow = dataGridViewInsurancePolicies.SelectedRows[0];
                    var selectedPolicyId = selectedRow.Cells["PolicyId"].Value.ToString();

                    var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
                    var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);

                    if (selectedCustomer != null)
                    {
                        // Xóa chính sách bảo hiểm khỏi danh sách
                        var selectedPolicy = selectedCustomer.insurancePolicies.FirstOrDefault(p => p.policyId == ObjectId.Parse(selectedPolicyId));
                        if (selectedPolicy != null)
                        {
                            selectedCustomer.insurancePolicies.Remove(selectedPolicy);

                            // Cập nhật xuống DataGridView
                            dataGridViewInsurancePolicies.DataSource = null; // Xóa dữ liệu cũ
                            dataGridViewInsurancePolicies.DataSource = selectedCustomer.insurancePolicies
                                .Select(p => new
                                {
                                    PolicyId = p.policyId.ToString(),
                                    PolicyNumber = p.policyNumber,
                                    PolicyType = p.policyType,
                                    StartDate = p.startDate.ToString("dd/MM/yyyy"),
                                    EndDate = p.endDate.ToString("dd/MM/yyyy"),
                                    Premium = p.premium,
                                    CoverageAmount = p.coverageAmount
                                })
                                .ToList();

                            // Cập nhật xuống MongoDB
                            var filter = Builders<Customer>.Filter.Eq("customerId", selectedCustomerId);
                            var update = Builders<Customer>.Update.Set("insurancePolicies", selectedCustomer.insurancePolicies);
                            _customerCollection.UpdateOne(filter, update);

                            MessageBox.Show("Đã xóa chính sách bảo hiểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chính sách bảo hiểm để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
    }
    }
    
