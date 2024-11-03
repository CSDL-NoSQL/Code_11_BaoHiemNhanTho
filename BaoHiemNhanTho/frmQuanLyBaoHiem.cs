using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BaoHiemNhanTho
{
    public partial class frmQuanLyBaoHiem : Form
    {
        private IMongoCollection<Customer> _customerCollection;

        public frmQuanLyBaoHiem()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("BHNT");
            _customerCollection = database.GetCollection<Customer>("BaoHiemNhanTho");
        }

        private void LoadInsurancePolicies()
        {
            // Lấy tất cả khách hàng từ MongoDB
            var customers = _customerCollection.Find(_ => true).ToList();

            // Lấy danh sách tất cả các bảo hiểm từ khách hàng
            var insurancePolicies = customers
                .SelectMany(c => c.insurancePolicies) // Duyệt qua các khách hàng và lấy các bảo hiểm
                .ToList();

            // Chuyển danh sách bảo hiểm vào DataGridView
            dataGridView1.DataSource = insurancePolicies.Select(p => new
            {
                PolicyId = p.policyId,
                PolicyNumber = p.policyNumber,
                PolicyType = p.policyType,
                StartDate = p.startDate.ToShortDateString(),
                EndDate = p.endDate.ToShortDateString(),
                Premium = p.premium,
                CoverageAmount = p.coverageAmount
            }).ToList();
        }

        private void frmQuanLyBaoHiem_Load_1(object sender, EventArgs e)
        {
            LoadInsurancePolicies();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            var searchText = textBox6.Text.Trim();

            // Lấy danh sách khách hàng từ MongoDB
            var customers = _customerCollection.Find(_ => true).ToList();

            // Dựa vào radioButton được chọn để tìm kiếm theo PolicyNumber hoặc PolicyType
            var filteredPolicies = customers
                .SelectMany(c => c.insurancePolicies)
                .Where(p =>
                    (radioButton1.Checked && p.policyNumber.Contains(searchText)) || // Tìm theo PolicyNumber
                    (radioButton2.Checked && p.policyType.Contains(searchText))       // Tìm theo PolicyType
                )
                .ToList();

            // Chuyển danh sách kết quả tìm kiếm vào DataGridView
            dataGridView1.DataSource = filteredPolicies.Select(p => new
            {
                PolicyId = p.policyId,
                PolicyNumber = p.policyNumber,
                PolicyType = p.policyType,
                StartDate = p.startDate.ToShortDateString(),
                EndDate = p.endDate.ToShortDateString(),
                Premium = p.premium,
                CoverageAmount = p.coverageAmount
            }).ToList();
        }
    }
}
