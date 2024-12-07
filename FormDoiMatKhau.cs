using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_Bán_Hàng_Cafa
{
    public partial class FormDoiMatKhau : Form
    {
        QuanLyCafeEntities mk = new QuanLyCafeEntities();
        private TaiKhoan matkhaulogin;

        public FormDoiMatKhau(TaiKhoan user)
        {
            InitializeComponent();
            matkhaulogin = user;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            string matkhaucu = txtmatkhau.Text;
            string matkhaumoi = txtmatkhaumoi.Text;
            string matkhauxacnhan = txtnhaplai.Text;

            // kiểm tra mật khẩu cũ có đúng không
            if (matkhaulogin.matkhau != matkhaucu)
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // kiểm tra độ dài mật khẩu
            if (matkhaumoi.Length > 10)
            {
                MessageBox.Show("Mật khẩu mới không được dài quá 10 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // kiểm tra mật khẩu mới chỉ chứa ký tự số
            if (!matkhaumoi.All(char.IsDigit))
            {
                MessageBox.Show("Mật khẩu mới chỉ được chứa các số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // kiểm tra mật khẩu nhập lại có khớp với mật mới không
            if (matkhaumoi != matkhauxacnhan)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            matkhaulogin.matkhau = matkhaumoi;
            mk.TaiKhoans.AddOrUpdate(matkhaulogin);
            mk.SaveChanges();
            MessageBox.Show("Mật khẩu đã được thay đổi thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
