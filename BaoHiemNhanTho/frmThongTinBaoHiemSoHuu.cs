using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BaoHiemNhanTho
{
    public partial class frmThongTinBaoHiemSoHuu : Form
    {
        private IMongoCollection<Customer> _customerCollection;
        private List<Customer> _customers;
        public frmThongTinBaoHiemSoHuu()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("BHNT");
            _customerCollection = database.GetCollection<Customer>("BaoHiemNhanTho");
        }

        private void frmThongTinBaoHiemSoHuu_Load(object sender, EventArgs e)
        {
            // Load danh sách khách hàng lên ComboBox khi form được load
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            // Lấy tất cả khách hàng từ MongoDB
            _customers = _customerCollection.Find(_ => true).ToList();

            // Hiển thị danh sách khách hàng trong ComboBox
            comboBoxCustomers.DataSource = _customers.Select(c => new
            {
                Display = c.name.firstName + " " + c.name.lastName + " - " + c.phoneNumber,
                Value = c.customerId
            }).ToList();

            comboBoxCustomers.DisplayMember = "Display";
            comboBoxCustomers.ValueMember = "Value";
        }

        private void comboBoxCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy customerId của khách hàng được chọn từ ComboBox
            var selectedCustomerId = comboBoxCustomers.SelectedValue.ToString();

            // Tải danh sách bảo hiểm của khách hàng đó lên DataGridView
            LoadInsurancePolicies(selectedCustomerId);
        }

        private void LoadInsurancePolicies(string customerId)
        {
            // Lấy khách hàng có ID tương ứng từ MongoDB
            var customer = _customerCollection.Find(c => c.customerId == customerId).FirstOrDefault();

            if (customer != null)
            {
                // Lấy danh sách bảo hiểm từ khách hàng
                var insurancePolicies = customer.insurancePolicies;

                // Hiển thị danh sách bảo hiểm lên DataGridView
                dataGridViewInsurancePolicies.DataSource = insurancePolicies.Select(p => new
                {
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
}