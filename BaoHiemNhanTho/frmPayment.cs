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
    public partial class frmPayment : Form
    {
        private IMongoCollection<Customer> _customerCollection;
        private List<Customer> _customers;

        public frmPayment()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("BHNT");
            _customerCollection = database.GetCollection<Customer>("BaoHiemNhanTho");
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            LoadCustomersIntoComboBox();
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
                LoadPaymentsForSelectedCustomer(selectedCustomerId);
            }
        }
        private void LoadPaymentsForSelectedCustomer(string customerId)
        {
            // Tìm khách hàng theo ID
            var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == customerId);
            if (selectedCustomer != null)
            {
                // Kiểm tra xem danh sách payments có khác null hay không
                var payments = selectedCustomer.payments;
                if (payments != null && payments.Any()) // Kiểm tra không null và có phần tử
                {
                    // Cập nhật DataGridView
                    dataGridViewPayment.DataSource = payments.Select(p => new
                    {
                        PaymentId = p.paymentId.ToString(), // Chuyển đổi ObjectId thành chuỗi để hiển thị
                        Amount = p.amount,
                        PaymentDate = p.paymentDate.ToString("dd/MM/yyyy"), // Định dạng ngày tháng
                        PaymentMethod = p.paymentMethod
                    }).ToList();
                }
                else
                {
                    // Nếu danh sách payments null hoặc không có phần tử, làm rỗng DataGridView
                    dataGridViewPayment.DataSource = null;
                }
            }
            else
            {
                // Nếu không tìm thấy khách hàng, làm rỗng DataGridView
                dataGridViewPayment.DataSource = null;
            }
        }

        private void dataGridViewPayment_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewPayment.SelectedRows.Count > 0) // Kiểm tra có dòng nào được chọn không
            {
                var selectedRow = dataGridViewPayment.SelectedRows[0];

                // Cập nhật các TextBox tương ứng với thông tin thanh toán
                textBoxPaymentId.Text = selectedRow.Cells["PaymentId"].Value.ToString();
                textBoxAmount.Text = selectedRow.Cells["Amount"].Value.ToString();

                // Cập nhật DateTimePicker với giá trị từ DataGridView
                
                textBoxPaymentMethod.Text = selectedRow.Cells["PaymentMethod"].Value.ToString();
            }
            else
            {
                // Nếu không có dòng nào được chọn, có thể xóa nội dung các TextBox
                textBoxPaymentId.Clear();
                textBoxAmount.Clear();
                dateTimePickerPaymentDate.Value = DateTime.Now; // Hoặc đặt về giá trị mặc định
                textBoxPaymentMethod.Clear();
            }
        }

        private void buttonAddBeneficiary_Click(object sender, EventArgs e)
        {
            // Tạo một đối tượng Payment mới từ thông tin trong các textbox
            // Tạo một đối tượng Payment mới từ thông tin trong các textbox
            var newPayment = new Payment
            {
                paymentId = ObjectId.GenerateNewId(), // Tạo ID mới cho Payment
                amount = decimal.Parse(textBoxAmount.Text), // Chuyển đổi từ textbox sang decimal
                paymentDate = dateTimePickerPaymentDate.Value, // Lấy giá trị từ DateTimePicker
                paymentMethod = textBoxPaymentMethod.Text // Lấy giá trị từ textbox
            };

            // Tìm khách hàng để thêm payment
            var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
            var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);

            if (selectedCustomer != null)
            {
                // Kiểm tra xem danh sách payments có khác null không
                if (selectedCustomer.payments == null)
                {
                    selectedCustomer.payments = new List<Payment>(); // Khởi tạo danh sách nếu là null
                }

                // Thêm payment vào danh sách payments của khách hàng
                selectedCustomer.payments.Add(newPayment);

                // Cập nhật vào MongoDB
                _customerCollection.ReplaceOne(c => c.customerId == selectedCustomerId, selectedCustomer);

                // Tải lại danh sách thanh toán
                LoadPaymentsForSelectedCustomer(selectedCustomerId);
                MessageBox.Show("Thêm thông tin thanh toán thành công!");
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng để thêm thanh toán!");
            }
        }

        private void buttonEditBeneficiary_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayment.SelectedRows.Count > 0) // Kiểm tra có dòng nào được chọn không
            {
                var selectedRow = dataGridViewPayment.SelectedRows[0];
                var paymentId = selectedRow.Cells["PaymentId"].Value.ToString();

                // Tìm khách hàng để sửa payment
                var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
                var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);

                if (selectedCustomer != null)
                {
                    // Tìm payment cần sửa
                    var paymentToEdit = selectedCustomer.payments.FirstOrDefault(p => p.paymentId.ToString() == paymentId);
                    if (paymentToEdit != null)
                    {
                        // Cập nhật thông tin payment
                        paymentToEdit.amount = decimal.Parse(textBoxAmount.Text);
                        paymentToEdit.paymentDate = dateTimePickerPaymentDate.Value;
                        paymentToEdit.paymentMethod = textBoxPaymentMethod.Text;

                        // Cập nhật vào MongoDB
                        _customerCollection.ReplaceOne(c => c.customerId == selectedCustomerId, selectedCustomer);

                        // Tải lại danh sách thanh toán
                        LoadPaymentsForSelectedCustomer(selectedCustomerId);
                        MessageBox.Show("Sửa thông tin thanh toán thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin thanh toán để sửa!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa thông tin thanh toán.");
            }
        }

        private void buttonDeleteBeneficiary_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayment.SelectedRows.Count > 0) // Kiểm tra có dòng nào được chọn không
            {
                var selectedRow = dataGridViewPayment.SelectedRows[0];
                var paymentId = selectedRow.Cells["PaymentId"].Value.ToString();

                // Xác nhận trước khi xóa
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin thanh toán này?",
                                                     "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Tìm khách hàng để xóa payment
                    var selectedCustomerId = comboBoxKhachHang.SelectedValue?.ToString();
                    var selectedCustomer = _customers.FirstOrDefault(c => c.customerId == selectedCustomerId);

                    if (selectedCustomer != null)
                    {
                        // Xóa payment khỏi danh sách payments của khách hàng
                        var paymentToDelete = selectedCustomer.payments.FirstOrDefault(p => p.paymentId.ToString() == paymentId);
                        if (paymentToDelete != null)
                        {
                            selectedCustomer.payments.Remove(paymentToDelete);

                            // Cập nhật vào MongoDB
                            _customerCollection.ReplaceOne(c => c.customerId == selectedCustomerId, selectedCustomer);

                            // Tải lại danh sách thanh toán
                            LoadPaymentsForSelectedCustomer(selectedCustomerId);
                            MessageBox.Show("Xóa thông tin thanh toán thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin thanh toán để xóa!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa thông tin thanh toán.");
            }
        }
    }
}
