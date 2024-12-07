using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace App_Bán_Hàng_Cafa
{
    public partial class FormThanhToan : Form
    {
        QuanLyCafeEntities _context = new QuanLyCafeEntities();
        private FormPhầnMềmQLBánHàng FQL;

        Timer timer;
        int timeleft = 1800;

        public FormThanhToan(FormPhầnMềmQLBánHàng formql)
        {
            InitializeComponent();
            FQL = formql;

            // Cấu trúc Timer
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeleft > 0)
            {
                timeleft--;
                txttime.Text = TimeSpan.FromSeconds(timeleft).ToString("mm\\:ss");
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Hết thời gian chuyển khoản!", "Thông Báo");
                // Có thể thêm mã để xử lý khi hết thời gian
            }
        }

        private void btncheckout_Click(object sender, EventArgs e)
        {
            var ban = FQL.GetCurrentBan(); // Lấy thông tin bàn hiện tại từ FormPhầnMềmQLBánHàng

            var hoadon = _context.HoaDons.FirstOrDefault(hd => hd.idBan == ban.id && hd.tinhtrang == 0);

            if (hoadon != null)
            {
                // Cập nhật hóa đơn đã thanh toán
                hoadon.tinhtrang = 1;
                hoadon.ngayvagiosau = DateTime.Now;

                _context.SaveChanges();

                // Cập nhật trạng thái bàn thành "Trống"
                ban.tinhtrang = "Trống";

                FQL.CapNhatTrangThaiBan();

                var lsvthongtinhoadon = FQL.GetThongTinHoaDonListView();
                lsvthongtinhoadon.Items.Clear();

                var txttongtien = FQL.GetTongTien();
                txttongtien.Text = 0.ToString("C", CultureInfo.CurrentCulture);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
