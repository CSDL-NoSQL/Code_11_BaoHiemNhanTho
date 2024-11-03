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
    public partial class frmTaoHoSoMoi : Form
    {
        private IMongoCollection<Customer> _customerCollection;
        // Các thuộc tính để lưu thông tin khách hàng
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public frmTaoHoSoMoi()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("BHNT");
            _customerCollection = database.GetCollection<Customer>("BaoHiemNhanTho");
        }

        private void frmTaoHoSoMoi_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin khách hàng lên các TextBox
            txtCustomerID.Text = CustomerId;
            txtFirstName.Text = FirstName;
            txtLastName.Text = LastName;
            txtGender.Text = Gender;
            txtPhone.Text = Phone;
            txtEmail.Text = Email;
            txtStreet.Text = Street;
            txtCity.Text = City;
            txtCountry.Text = Country;
        }

        private void ssa_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ form
                var policyNumber = txtPolicyNumber.Text;
                var policyType = txtPolicyType.Text;
                var startDate = dtpStartDate.Value;
                var endDate = dtpEndDate.Value;
                var premium = Convert.ToDecimal(txtPremium.Text);
                var coverageAmount = Convert.ToDecimal(txtCoverageAmount.Text);

                // Khởi tạo danh sách người thụ hưởng
                var beneficiaries = new List<Beneficiary>();

                // Duyệt qua các dòng trong dgvBeneficiaries để lấy thông tin người thụ hưởng
                foreach (DataGridViewRow row in dgvBeneficiaries.Rows)
                {
                    // Kiểm tra nếu dòng không phải là dòng trống cuối cùng
                    if (!row.IsNewRow)
                    {
                        var beneficiaryName = row.Cells["beneficiaryname"].Value?.ToString();
                        var relationship = row.Cells["relationship"].Value?.ToString();
                        var percentage = Convert.ToInt32(row.Cells["percentage"].Value);

                        // Thêm người thụ hưởng vào danh sách
                        beneficiaries.Add(new Beneficiary
                        {
                            name = beneficiaryName,
                            relationship = relationship,
                            percentage = percentage
                        });
                    }
                }

                // Tạo một đối tượng bảo hiểm mới
                var newPolicy = new InsurancePolicy
                {
                    policyId = ObjectId.GenerateNewId(),
                    policyNumber = policyNumber,
                    policyType = policyType,
                    startDate = startDate,
                    endDate = endDate,
                    premium = premium,
                    coverageAmount = coverageAmount,
                    beneficiaries = beneficiaries,
                    claims = new List<Claim>() // Khởi tạo danh sách yêu cầu trống
                };

                // Cập nhật khách hàng trong CSDL
                var filter = Builders<Customer>.Filter.Eq(c => c.customerId, CustomerId);

                // Đọc tài liệu khách hàng để kiểm tra insurancePolicies
                var customer = _customerCollection.Find(filter).FirstOrDefault();

                if (customer == null)
                {
                    MessageBox.Show("Khách hàng không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra và khởi tạo insurancePolicies nếu cần
                var update = Builders<Customer>.Update.Push("insurancePolicies", newPolicy);
                if (customer.insurancePolicies == null)
                {
                    // Khởi tạo insurancePolicies nếu nó là null
                    update = Builders<Customer>.Update.Set("insurancePolicies", new List<InsurancePolicy> { newPolicy });
                }

                // Cập nhật tài liệu
                _customerCollection.UpdateOne(filter, update);

                MessageBox.Show("Thêm hồ sơ bảo hiểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm hồ sơ bảo hiểm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBeneficiaries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
