using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaoHiemNhanTho
{
    public partial class frmClaim : Form
    {
        private IMongoCollection<Customer> _customerCollection;
        private List<Customer> _customers;
        public frmClaim()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("BHNT");
            _customerCollection = database.GetCollection<Customer>("BaoHiemNhanTho");
        }

        private void frmClaim_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }
        private void LoadCustomers()
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
                    dataGridViewClaim.DataSource = null; // Xóa dữ liệu cũ
                }
            }
        }

        private void dataGridViewInsurancePolicies_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewInsurancePolicies.CurrentRow != null)
            {
                var selectedPolicyId = dataGridViewInsurancePolicies.CurrentRow.Cells["PolicyId"].Value.ToString();

                // Tìm bảo hiểm đã chọn trong danh sách của khách hàng
                var selectedCustomer = _customers.FirstOrDefault(c => c.customerId.ToString() == comboBoxKhachHang.SelectedValue.ToString());
                var selectedPolicy = selectedCustomer?.insurancePolicies
                    .FirstOrDefault(p => p.policyId.ToString() == selectedPolicyId);

                if (selectedPolicy != null)
                {
                    // Hiển thị danh sách yêu cầu bồi thường (claims) lên dataGridViewClaim
                    var claims = selectedPolicy.claims
                        .Select(claim => new
                        {
                            ClaimId = claim.claimId.ToString(),
                            ClaimNumber = claim.claimNumber,
                            DateOfClaim = claim.dateOfClaim.ToString("dd/MM/yyyy"),
                            ClaimAmount = claim.claimAmount,
                            Status = claim.status,
                            Description = claim.description
                        })
                        .ToList();

                    dataGridViewClaim.DataSource = claims;
                }
            }
        }

        private void dataGridViewClaim_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewClaim.CurrentRow != null)
            {
                // Lấy thông tin claim đã chọn
                var claimNumber = dataGridViewClaim.CurrentRow.Cells["ClaimNumber"].Value.ToString();
                var dateOfClaim = dataGridViewClaim.CurrentRow.Cells["DateOfClaim"].Value.ToString();
                var claimAmount = dataGridViewClaim.CurrentRow.Cells["ClaimAmount"].Value.ToString();
                var status = dataGridViewClaim.CurrentRow.Cells["Status"].Value.ToString();
                var description = dataGridViewClaim.CurrentRow.Cells["Description"].Value.ToString();

                // Hiển thị thông tin lên các TextBox
                txtClaimNumber.Text = claimNumber;
                
                txtClaimAmount.Text = claimAmount;
                txtStatus.Text = status;
                txtDescription.Text = description;
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
                        // Tạo một đối tượng Claim mới từ các TextBox
                        var newClaim = new Claim
                        {
                            claimNumber = txtClaimNumber.Text,
                            dateOfClaim = dtpDateOfClaim.Value,
                            claimAmount = Convert.ToDecimal(txtClaimAmount.Text),
                            status = txtStatus.Text,
                            description = txtDescription.Text
                        };

                        // Thêm yêu cầu bồi thường vào danh sách của bảo hiểm đã chọn
                        selectedPolicy.claims.Add(newClaim);

                        // Cập nhật DataGridView
                        dataGridViewClaim.DataSource = null; // Xóa dữ liệu cũ
                        dataGridViewClaim.DataSource = selectedPolicy.claims
                            .Select(c => new {
                                ClaimNumber = c.claimNumber,
                                DateOfClaim = c.dateOfClaim.ToString("dd/MM/yyyy"),
                                ClaimAmount = c.claimAmount,
                                Status = c.status,
                                Description = c.description
                            })
                            .ToList();

                        // Cập nhật xuống MongoDB
                        var filter = Builders<Customer>.Filter.Eq("customerId", selectedCustomerId);
                        var update = Builders<Customer>.Update.Set("insurancePolicies", selectedCustomer.insurancePolicies);
                        _customerCollection.UpdateOne(filter, update);

                        MessageBox.Show("Thêm yêu cầu bồi thường thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void buttonEditBeneficiary_Click(object sender, EventArgs e)
        {
            if (dataGridViewClaim.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewClaim.SelectedRows[0];
                var selectedClaimIndex = selectedRow.Index;
                var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
                var selectedPolicyId = dataGridViewInsurancePolicies.SelectedRows[0].Cells["PolicyId"].Value.ToString();
                var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);
                var selectedPolicy = selectedCustomer.insurancePolicies.FirstOrDefault(p => p.policyId == ObjectId.Parse(selectedPolicyId));

                if (selectedPolicy != null)
                {
                    // Cập nhật thông tin yêu cầu bồi thường từ TextBox
                    var selectedClaim = selectedPolicy.claims[selectedClaimIndex];
                    selectedClaim.claimNumber = txtClaimNumber.Text;
                    selectedClaim.dateOfClaim = dtpDateOfClaim.Value;
                    selectedClaim.claimAmount = Convert.ToDecimal(txtClaimAmount.Text);
                    selectedClaim.status = txtStatus.Text;
                    selectedClaim.description = txtDescription.Text;

                    // Cập nhật DataGridView
                    dataGridViewClaim.DataSource = null; // Xóa dữ liệu cũ
                    dataGridViewClaim.DataSource = selectedPolicy.claims
                        .Select(c => new {
                            ClaimNumber = c.claimNumber,
                            DateOfClaim = c.dateOfClaim.ToString("dd/MM/yyyy"),
                            ClaimAmount = c.claimAmount,
                            Status = c.status,
                            Description = c.description
                        })
                        .ToList();

                    // Cập nhật xuống MongoDB
                    var filter = Builders<Customer>.Filter.Eq("customerId", selectedCustomerId);
                    var update = Builders<Customer>.Update.Set("insurancePolicies", selectedCustomer.insurancePolicies);
                    _customerCollection.UpdateOne(filter, update);

                    MessageBox.Show("Sửa thông tin yêu cầu bồi thường thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một yêu cầu bồi thường để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonDeleteBeneficiary_Click(object sender, EventArgs e)
        {
            if (dataGridViewClaim.SelectedRows.Count > 0)
            {
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa yêu cầu bồi thường đã chọn?",
                                                     "Xác nhận xóa",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    var selectedRow = dataGridViewClaim.SelectedRows[0];
                    var selectedClaimIndex = selectedRow.Index;
                    var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
                    var selectedPolicyId = dataGridViewInsurancePolicies.SelectedRows[0].Cells["PolicyId"].Value.ToString();
                    var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);
                    var selectedPolicy = selectedCustomer.insurancePolicies.FirstOrDefault(p => p.policyId == ObjectId.Parse(selectedPolicyId));

                    if (selectedPolicy != null)
                    {
                        // Xóa yêu cầu bồi thường khỏi danh sách
                        selectedPolicy.claims.RemoveAt(selectedClaimIndex);

                        // Cập nhật DataGridView
                        dataGridViewClaim.DataSource = null; // Xóa dữ liệu cũ
                        dataGridViewClaim.DataSource = selectedPolicy.claims
                            .Select(c => new {
                                ClaimNumber = c.claimNumber,
                                DateOfClaim = c.dateOfClaim.ToString("dd/MM/yyyy"),
                                ClaimAmount = c.claimAmount,
                                Status = c.status,
                                Description = c.description
                            })
                            .ToList();

                        // Cập nhật xuống MongoDB
                        var filter = Builders<Customer>.Filter.Eq("customerId", selectedCustomerId);
                        var update = Builders<Customer>.Update.Set("insurancePolicies", selectedCustomer.insurancePolicies);
                        _customerCollection.UpdateOne(filter, update);

                        MessageBox.Show("Xóa yêu cầu bồi thường thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một yêu cầu bồi thường để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        
    }
}
