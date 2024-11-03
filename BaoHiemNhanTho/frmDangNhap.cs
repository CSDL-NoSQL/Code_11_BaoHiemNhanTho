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
    public partial class frmDangNhap : Form
    {
        private readonly IMongoCollection<NhanVien> _nhanVienCollection;
        public frmDangNhap()
        {
            InitializeComponent();

            // Kết nối đến MongoDB
            var client = new MongoClient("mongodb://localhost:27017"); // Thay đổi nếu cần
            var database = client.GetDatabase("BHNT"); // Tên database của bạn
            _nhanVienCollection = database.GetCollection<NhanVien>("NhanVien");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text; // Giả sử txtUsername là TextBox nhập tài khoản
            string password = txtPassword.Text; // Giả sử txtPassword là TextBox nhập mật khẩu

            // Kiểm tra tài khoản và mật khẩu
            if (IsValidLogin(username, password))
            {
                CurrentUser.NhanVienDangNhap = _nhanVienCollection.Find(nv => nv.username == username).FirstOrDefault();
                // Nếu đăng nhập thành công, mở frmTrangChu
                frmTrangChu trangChuForm = new frmTrangChu();
                this.Hide(); // Ẩn form đăng nhập
                trangChuForm.Show(); // Hiển thị form trang chủ
            }
            else
            {
                // Nếu đăng nhập thất bại, hiển thị thông báo
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool IsValidLogin(string username, string password)
        {
            // Truy vấn đến MongoDB để kiểm tra tài khoản và mật khẩu
            var nhanVien = _nhanVienCollection.Find(nv => nv.username == username && nv.password == password).FirstOrDefault();
            return nhanVien != null; // Nếu tìm thấy nhân viên, trả về true
        }


    }
    public class NhanVien
    {
        public ObjectId Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public string hoTen { get; set; }
        public string chucVu { get; set; }
        public string email { get; set; }
        public string soDienThoai { get; set; }
        // Thêm các thuộc tính khác nếu cần
        public DateTime ngayTao { get; set; }

        public DateTime? deleteTime { get; set; }
    }
    public static class CurrentUser
    {
        public static NhanVien NhanVienDangNhap { get; set; }
    }
}
