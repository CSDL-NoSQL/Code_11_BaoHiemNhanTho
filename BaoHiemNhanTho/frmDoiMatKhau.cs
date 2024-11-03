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
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;

            // Kiểm tra mật khẩu hiện tại có chính xác không
            if (CurrentUser.NhanVienDangNhap.password != currentPassword)
            {
                MessageBox.Show("Mật khẩu hiện tại không chính xác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cập nhật mật khẩu mới vào cơ sở dữ liệu
            UpdatePassword(newPassword);
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            lblAccount.Text = CurrentUser.NhanVienDangNhap.username;
        }
        private void UpdatePassword(string newPassword)
        {
            // Kết nối đến MongoDB
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("BHNT");
            var nhanVienCollection = database.GetCollection<NhanVien>("NhanVien");

            // Cập nhật mật khẩu cho nhân viên đang đăng nhập
            var filter = Builders<NhanVien>.Filter.Eq(nv => nv.username, CurrentUser.NhanVienDangNhap.username);
            var update = Builders<NhanVien>.Update.Set(nv => nv.password, newPassword);
            nhanVienCollection.UpdateOne(filter, update);

            // Cập nhật mật khẩu trong đối tượng CurrentUser
            CurrentUser.NhanVienDangNhap.password = newPassword;

            MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close(); // Đóng form sau khi đổi mật khẩu
        }
    }
}
