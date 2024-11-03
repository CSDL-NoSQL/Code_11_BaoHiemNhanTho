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
    public partial class frmThongTinTaiKhoan : Form
    {
        public frmThongTinTaiKhoan()
        {
            InitializeComponent();
            LoadUserInfo();
        }
        private void LoadUserInfo()
        {
            // Hiển thị thông tin nhân viên đang đăng nhập
            if (CurrentUser.NhanVienDangNhap != null)
            {
                lblHoTen.Text = CurrentUser.NhanVienDangNhap.hoTen; // Label hiển thị họ tên
                lblChucVu.Text = CurrentUser.NhanVienDangNhap.chucVu; // Label hiển thị chức vụ
                lblEmail.Text = CurrentUser.NhanVienDangNhap.email; // Label hiển thị email
                lblSoDienThoai.Text = CurrentUser.NhanVienDangNhap.soDienThoai; // Label hiển thị số điện thoại
            }
        }

        
    }
}
