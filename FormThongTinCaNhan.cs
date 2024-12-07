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
    public partial class FormThongTinCaNhan : Form
    {
        QuanLyCafeEntities tk = new QuanLyCafeEntities();
        private TaiKhoan dangnhap;

        public FormThongTinCaNhan(TaiKhoan user)
        {
            InitializeComponent();
            dangnhap = user;
        }

        private void FormThongTinCaNhan_Load(object sender, EventArgs e)
        {
            if (dangnhap != null)
            {
                txtdangnhap.Text = dangnhap.taikhoan1;
                txttenhienthi.Text = dangnhap.tenhienthi;
                txtdangnhap.ReadOnly = true;
            }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (dangnhap != null)
            {
                dangnhap.tenhienthi = txttenhienthi.Text;

                tk.TaiKhoans.AddOrUpdate(dangnhap);
                tk.SaveChanges();
                MessageBox.Show("Tên hiển thị đã được cập nhật!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Không thể cập nhật tên hiển thị. Người dùng không tồn tại.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
