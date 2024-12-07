using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_Bán_Hàng_Cafa
{
    public partial class FormDangNhap : Form
    {
        QuanLyCafeEntities quanLyCafeEntities = new QuanLyCafeEntities();
        public FormDangNhap()
        {
            InitializeComponent();
        }

        // lưu trữ và truyền tham số tài khoản đến form hồ sơ tài khoản hiện tại
        public static TaiKhoan LoggedInUser { get; private set; }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            // Lấy Dữ Liễu Từ TextBox
            string taikhoan = txtdangnhap.Text;
            string matkhau = txtmatkhau.Text;

            // Kiểm Tra Thông Tin Đăng Nhập
            var user = quanLyCafeEntities.TaiKhoans.FirstOrDefault(tk => tk.taikhoan1 == taikhoan && tk.matkhau == matkhau);

            if (user != null)
            {
                // Lưu Thông Tin Người Dùng Đã Đăng Nhập
                LoggedInUser = user;

                // Kiểm Tra Loại Tài Khoản
                if (user.loaitk == 1)
                {
                    MessageBox.Show("Chào Mừng Quay Trở Lại Với Tư Cách Của Quản Trị Viên!", "Đăng Nhập Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở Form Quản Trị Viên
                    FormQuanTriVien fm = new FormQuanTriVien();
                    this.Hide();
                    fm.ShowDialog();
                    this.Show();
                }
                else if(user.loaitk == 0) 
                {
                    MessageBox.Show("Bạn Đã Đăng Nhập Thành Công Với Tư Cách Là Nhân Viên!", "Đăng Nhập Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở Form Phần Mềm Quản Lý Bán Hàng
                    FormPhầnMềmQLBánHàng pm = new FormPhầnMềmQLBánHàng();
                    this.Hide();
                    pm.ShowDialog();
                    this.Show();
                }               
            }
            else
            {
                MessageBox.Show("Tên Tài Khoản Hoặc Mật Khẩu Không Tồn Tài!", "Đăng Nhập Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn có muốn thoát chương trình!!", "Thông Báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
