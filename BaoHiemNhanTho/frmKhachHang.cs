using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BaoHiemNhanTho
{
    public partial class frmKhachHang : Form
    {
        private IMongoCollection<Customer> _customerCollection;
        private bool isAddingNewCustomer = true; // Flag để phân biệt thêm và sửa
        public frmKhachHang()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("BHNT");
            _customerCollection = database.GetCollection<Customer>("BaoHiemNhanTho");
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            txtCustomerID.Enabled = false;
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            // Lấy tất cả khách hàng từ MongoDB
            var customers = _customerCollection.Find(_ => true).ToList();

            // Chuyển danh sách khách hàng vào DataGridView
            dgvDSKhachHang.DataSource = customers.Select(c => new
            {
                CustomerId = c.customerId,
                FullName = c.name.firstName + " " + c.name.lastName,
                Gender = c.gender,
                Phone = c.phoneNumber,
                Email = c.email,
                Address = c.address.street + ", " + c.address.city + ", " + c.address.country
            }).ToList();
        }

        private void dgvDSKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dgvDSKhachHang.SelectedRows.Count > 0)
            {
                // Lấy hàng đã chọn
                var selectedRow = dgvDSKhachHang.SelectedRows[0].DataBoundItem;

                // Chuyển đổi và lấy thông tin
                if (selectedRow != null)
                {
                    // Chuyển đổi về dynamic để lấy các giá trị
                    var customer = (dynamic)selectedRow;

                    // Gán giá trị vào các TextBox
                    txtCustomerID.Text = customer.CustomerId;

                    // Tách tên và họ
                    var fullName = customer.FullName;
                    var spaceIndex = fullName.IndexOf(' '); // Tìm vị trí của dấu cách đầu tiên

                    if (spaceIndex > 0)
                    {
                        txtFirstName.Text = fullName.Substring(0, spaceIndex); // Lấy tên
                        txtLastName.Text = fullName.Substring(spaceIndex + 1); // Lấy họ (tất cả sau dấu cách)
                    }
                    else
                    {
                        txtFirstName.Text = fullName; // Nếu không có dấu cách, xem như toàn bộ là tên
                        txtLastName.Clear(); // Xóa họ
                    }

                    txtGender.Text = customer.Gender;
                    txtPhone.Text = customer.Phone;
                    txtEmail.Text = customer.Email;

                    // Tách địa chỉ
                    var addressParts = customer.Address.Split(',');
                    if (addressParts.Length == 3)
                    {
                        txtStreet.Text = addressParts[0].Trim(); // Đường
                        txtCity.Text = addressParts[1].Trim();   // Thành phố
                        txtCountry.Text = addressParts[2].Trim(); // Quốc gia
                    }
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dgvDSKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Không làm gì thêm nếu không có hàng nào được chọn
            }

            // Lấy hàng đã chọn
            var selectedRow = dgvDSKhachHang.SelectedRows[0].DataBoundItem;

            // Chuyển đổi về dynamic để lấy thông tin
            if (selectedRow != null)
            {
                var customer = (dynamic)selectedRow;

                // Tạo instance của frmTaoHoSoMoi và truyền thông tin khách hàng
                frmTaoHoSoMoi formttnv = new frmTaoHoSoMoi
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FullName.Split(' ')[0], // Lấy họ
                    LastName = customer.FullName.Split(' ')[1] + ' ' + customer.FullName.Split(' ')[2], // Lấy họ
                    Gender = customer.Gender,
                    Phone = customer.Phone,
                    Email = customer.Email,
                    Street = customer.Address.Split(',')[0].Trim(),
                    City = customer.Address.Split(',')[1].Trim(),
                    Country = customer.Address.Split(',')[2].Trim()
                };

                formttnv.ShowDialog(); // Hiện form như một cửa sổ con (modal)
            }
        }

        // Xóa trắng các TextBox khi bấm nút "Clear"
        private void button11_Click(object sender, EventArgs e)
        {
            txtCustomerID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtGender.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtStreet.Clear();
            txtCity.Clear();
            txtCountry.Clear();
            txtCustomerID.Enabled = true;
            // Đặt flag về trạng thái thêm khi xóa trắng
            isAddingNewCustomer = true;
        }

        // Thêm khách hàng mới khi bấm nút "Lưu"
        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isAddingNewCustomer)
            {
                // Tạo đối tượng Customer mới
                var newCustomer = new Customer
                {
                    customerId = txtCustomerID.Text, // hoặc sinh ID mới nếu cần
                    name = new Name
                    {
                        firstName = txtFirstName.Text,
                        lastName = txtLastName.Text
                    },
                    gender = txtGender.Text,
                    phoneNumber = txtPhone.Text,
                    email = txtEmail.Text,
                    address = new Address
                    {
                        street = txtStreet.Text,
                        city = txtCity.Text,
                        country = txtCountry.Text
                    }
                };

                // Thêm khách hàng mới vào MongoDB
                _customerCollection.InsertOne(newCustomer);
                txtCustomerID.Enabled = false;
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Sửa thông tin khách hàng
                var customerId = txtCustomerID.Text;

                // Tạo đối tượng Customer để cập nhật
                var update = Builders<Customer>.Update
                    .Set(c => c.name.firstName, txtFirstName.Text)
                    .Set(c => c.name.lastName, txtLastName.Text)
                    .Set(c => c.gender, txtGender.Text)
                    .Set(c => c.phoneNumber, txtPhone.Text)
                    .Set(c => c.email, txtEmail.Text)
                    .Set(c => c.address.street, txtStreet.Text)
                    .Set(c => c.address.city, txtCity.Text)
                    .Set(c => c.address.country, txtCountry.Text);

                // Cập nhật khách hàng trong MongoDB
                var filter = Builders<Customer>.Filter.Eq("customerId", customerId);
                var result = _customerCollection.UpdateOne(filter, update);

                if (result.ModifiedCount > 0)
                {
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không có thông tin nào được cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            // Reload danh sách khách hàng sau khi thêm hoặc sửa
            LoadCustomers();
        }


        private void button9_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dgvDSKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Không làm gì thêm nếu không có hàng nào được chọn
            }

            // Lấy hàng đã chọn
            var selectedRow = dgvDSKhachHang.SelectedRows[0].DataBoundItem;

            // Chuyển đổi về dynamic để lấy thông tin
            if (selectedRow != null)
            {
                var customer = (dynamic)selectedRow;

                // Hiển thị hộp thoại xác nhận xóa
                var confirmResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng có mã: {customer.CustomerId}, tên: {customer.FullName} không?",
                                                     "Xác nhận xóa",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    // Thực hiện xóa khách hàng
                    // Chuyển customer.CustomerId về kiểu phù hợp nếu cần
                    var customerId = customer.CustomerId.ToString(); // Chuyển đổi thành chuỗi nếu cần

                    // Sử dụng FilterDefinition để xác định điều kiện xóa
                    var filter = Builders<Customer>.Filter.Eq("customerId", customerId);
                    _customerCollection.DeleteOne(filter);

                    // Reload danh sách khách hàng sau khi xóa
                    LoadCustomers();

                    MessageBox.Show("Khách hàng đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dgvDSKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Không làm gì thêm nếu không có hàng nào được chọn
            }

            // Nếu có hàng được chọn, đặt txtCustomerID thành không cho phép chỉnh sửa
            
            isAddingNewCustomer = false;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox6.Text;

            if (radioTheoSDT.Checked) // Tìm theo số điện thoại
            {
                var filter = Builders<Customer>.Filter.Regex(c => c.phoneNumber, new MongoDB.Bson.BsonRegularExpression(searchText, "i"));
                var customers = _customerCollection.Find(filter).ToList();
                LoadCustomersToGrid(customers);
            }
            else if (radioTen.Checked) // Tìm theo tên
            {
                var filter = Builders<Customer>.Filter.Or(
                    Builders<Customer>.Filter.Regex(c => c.name.firstName, new MongoDB.Bson.BsonRegularExpression(searchText, "i")),
                    Builders<Customer>.Filter.Regex(c => c.name.lastName, new MongoDB.Bson.BsonRegularExpression(searchText, "i"))
                );
                var customers = _customerCollection.Find(filter).ToList();
                LoadCustomersToGrid(customers);
            }
        }

        // Hàm để load danh sách khách hàng vào DataGridView
        private void LoadCustomersToGrid(List<Customer> customers)
        {
            dgvDSKhachHang.DataSource = customers.Select(c => new
            {
                CustomerId = c.customerId,
                FullName = c.name.firstName + " " + c.name.lastName,
                Gender = c.gender,
                Phone = c.phoneNumber,
                Email = c.email,
                Address = c.address.street + ", " + c.address.city + ", " + c.address.country
            }).ToList();
        }

    }
}
