using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_Bán_Hàng_Cafa
{
    public partial class FormPhầnMềmQLBánHàng : Form
    {
        QuanLyCafeEntities _context = new QuanLyCafeEntities();

        private DatBan _banHT;
        private decimal _giagoc;

        public static TaiKhoan LoggedInUser { get; private set; }

        public FormPhầnMềmQLBánHàng()
        {
            InitializeComponent();
            LoadBanAn();
            LoadDanhMuc();
            LoadDSBanTrong();
            LoadGiamGia();
            CapNhatTrangThaiBan();

            FLPban.AutoScroll = true;       
            cbodanhmuc.SelectedIndexChanged += new EventHandler(cbodanhmuc_SelectedIndexChanged);
        }

        #region methods
        //-------------Tải Dữ Liệu Bàn Ăn---------------//
        void LoadBanAn()
        {
            // xóa hết các control trước khi thêm cái mới
            FLPban.Controls.Clear();

            // lấy danh sách bàn ăn từ cơ sở dữ liệu
            var bananlist = _context.DatBans.ToList();

            foreach ( var ban in bananlist)
            {
                // Kiểm tra và điều chỉnh trạng thái nếu không phù hợp
                if (string.IsNullOrEmpty(ban.tinhtrang))
                {
                    ban.tinhtrang = "Trống";
                }

                Button btnbanan = new Button
                {
                    Width = 80,
                    Height = 50,
                    Text = $"{ban.tenban}\n{ban.tinhtrang}",
                    BackColor = ban.tinhtrang == "Trống" ? Color.Gold : Color.Red,
                    Tag = ban
                };

                // thêm sự kiện click cho button
                btnbanan.Click += BtnBanAn_Click;

                // thêm button vào flow layout panel
                FLPban.Controls.Add(btnbanan);
            }
        }

        //-------------Tải Dữ Liệu Danh Mục---------------//
        void LoadDanhMuc()
        {
            var dml = _context.DanhMucs.ToList();
            cbodanhmuc.DataSource = dml;
            cbodanhmuc.DisplayMember = "tendanhmuc";
            cbodanhmuc.ValueMember = "id";
        }

        //-------------Tải Dữ Liệu Thức Ăn Theo Danh Mục---------------//
        void LoadThucAnTheoDanhMuc(int idDanhMuc)
        {
            var tal = _context.ThucAns.Where(ta => ta.idDanhMuc == idDanhMuc).ToList();
            cbothucan.DataSource = tal;
            cbothucan.DisplayMember = "tenthucan";
            cbothucan.ValueMember = "id";
        }

        //-------------Tải Dữ Liệu Hiện Tải Còn Đang Trống---------------//
        void LoadDSBanTrong()
        {
            var bantrong = _context.DatBans.Where(b => b.tinhtrang == "Trống").ToList();
            cbochuyenban.DataSource = bantrong;
            cbochuyenban.DisplayMember = "tenban";
            cbochuyenban.ValueMember = "id";
        }

        //-------------Tải Dữ Liệu Chế Độ Voucher---------------//
        void LoadGiamGia()
        {
            var giamgia = new List<string>
            {
                "Không có (0%)",
                "Nhân viên (30%)",
                "Người quen (50%)",
                "Người nhà (70%)"
            };
            cbogiamgia.DataSource = giamgia;
        }

        //-------------Tải Dữ Liệu Trạng Thái Bàn Ăn---------------//
        public void CapNhatTrangThaiBan()
        {
            foreach (Control control in FLPban.Controls)
            {
                if (control is Button btnBan)
                {
                    var ban = btnBan.Tag as DatBan;
                    if (ban != null)
                    {
                        // Lấy hóa đơn liên quan đến bàn
                        var hoadon = _context.HoaDons.FirstOrDefault(hd => hd.idBan == ban.id && hd.tinhtrang == 0);

                        // Nếu không có hóa đơn hoặc hóa đơn đã thanh toán thì đặt trạng thái là "Trống"
                        if (hoadon == null || !hoadon.HoaDonChiTiets.Any())
                        {
                            ban.tinhtrang = "Trống";
                            btnBan.BackColor = Color.Gold;  // Màu vàng cho trạng thái "Trống"
                        }
                        else
                        {
                            ban.tinhtrang = "Có Người";
                            btnBan.BackColor = Color.Red;   // Màu đỏ cho trạng thái "Có Người"
                        }

                        // Cập nhật lại thông tin trạng thái của bàn trong cơ sở dữ liệu
                        _context.Entry(ban).State = EntityState.Modified;
                    }
                }
            }
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
        }

        //-------------Tải Dữ Liệu Danh Sách Hóa Đơn---------------//
        public void CapNhatDSHoaDon(DatBan ban)
        {
            var hoadon = _context.HoaDons.FirstOrDefault(hd => hd.idBan == _banHT.id && hd.tinhtrang == 0);
            lsvthongtinhoadon.Items.Clear();
            decimal tongtien = 0;

            if (hoadon != null)
            {
                var hoadoncts = _context.HoaDonChiTiets
                    .Where(hdct => hdct.idHoaDon == hoadon.id)
                    .ToList();

                foreach (var ct in hoadoncts)
                {
                    // Lấy thông tin tên của bảng Thức Ăn
                    var thucan = _context.ThucAns.FirstOrDefault(ta => ta.id == ct.idThucAn);
                        
                    if (thucan != null)
                    {
                        // Kiểm tra nếu `ngayvagiosau` là null thì hiển thị là "Chưa check out"
                        var ngayvagiosau = hoadon.ngayvagiosau?.ToString() ?? "Chưa Check Out";

                        // Chuyển đổi tình trạng thanh toán
                        var tinhtrang = hoadon.tinhtrang == 1 ? "Đã Thanh Toán" : "Chưa Thanh Toán";

                        // nếu bàn đó có hóa đơn thì thêm thông tin vào list view
                        var lsv = new ListViewItem(hoadon.ngayvagiotruoc.ToString())
                        {
                            SubItems =
                            {
                                ngayvagiosau, tinhtrang, thucan.tenthucan, ct.soluong.ToString(),
                                thucan.gia.ToString("C")
                            }
                        };
                        
                        // thêm dòng vào listview 
                        lsvthongtinhoadon.Items.Add(lsv);

                        // lập công thức tính tổng tiền
                        tongtien += ct.soluong * thucan.gia;
                    }
                }

                _giagoc = tongtien;
                // chuyển tiền tệ sang vnđ
                //CultureInfo culture = new CultureInfo("vi-VN");
                //Thread.CurrentThread.CurrentCulture = culture;
                // hiện thị tổng tiền lên textbox
                txttongtien.Text = tongtien.ToString("C");
            }
            else
            {
                // nếu bàn đó không có hóa đơn
                lsvthongtinhoadon.Items.Add(new ListViewItem("Chưa Có Hóa Đơn!!"));

                // Đặt giá gốc là 0 khi không có hóa đơn
                _giagoc = 0;

                // đặt gốc default tổng tiền là 0
                txttongtien.Text = 0.ToString("C");
            }
        }
        #endregion


        #region events
        //-------------Xử Lý Bàn Ăn---------------//
        private void BtnBanAn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            _banHT = btn?.Tag as DatBan;

            if (_banHT != null)
            {
                CapNhatDSHoaDon(_banHT);
                MessageBox.Show($"Bạn Chọn Bàn Số {_banHT.tenban}");

                // Cập nhật trạng thái bàn sau khi chọn
                CapNhatTrangThaiBan();
            }
        }

        //-------------Xử Lý Thêm Thức Ăn Vào Hóa Đơn---------------//
        private void btnthemthucan_Click(object sender, EventArgs e)
        {
            // kiểm tra bàn đã chọn chưa
            if (_banHT == null)
            {
                MessageBox.Show("Vui Lòng Chọn Bàn Trước Khi Thêm Thức Ăn");
                return;
            }
            
            // Lấy danh mục và thức ăn đã chọn
            var danhmuc = cbodanhmuc.SelectedItem as DanhMuc;
            var thucan = cbothucan.SelectedItem as ThucAn;

            if (danhmuc == null || thucan == null)
            {
                MessageBox.Show("Vui Lòng Chọn Danh Mục Và Món Ăn");
                return;
            }

            // Lấy Số Lượng Numberic
            short soluong = (short)slmonan.Value;
            if (soluong <= 0) 
            {
                MessageBox.Show("Vui Lòng Chọn Số Lượng Hợp Lệ!");
                return;
            }

            // truy vấn hóa đơn từ cơ sở dữ liệu dựa trên id bàn
            var hoadon = _context.HoaDons
                .FirstOrDefault(hd => hd.idBan == _banHT.id && hd.tinhtrang == 0);
            if (hoadon == null)
            {
                // nếu không có hóa đơn thì tạo hóa đơn mới
                hoadon = new HoaDon
                {
                    idBan = _banHT.id,
                    ngayvagiotruoc = DateTime.Now,
                    tinhtrang = 0
                };
                _context.HoaDons.Add(hoadon);
                _context.SaveChanges();
            }

            // kiểm tra xem món ăn đã có trong chi tiết hóa đơn không
            var hdct = new HoaDonChiTiet
            {
                idHoaDon = hoadon.id,
                idThucAn = thucan.id,
                soluong = soluong
            };
            _context.HoaDonChiTiets.Add(hdct);
            _context.SaveChanges();

            // cập nhập lại listview để hiển thị món ăn vừa thêm
            CapNhatDSHoaDon(_banHT);

            // làm mới lại danh sách bàn ăn sau khi thêm
            CapNhatTrangThaiBan();

            // Kiểm tra xem hóa đơn có còn tồn tại hay không và cập nhật trạng thái bàn
            var cnhoadon = _context.HoaDons.FirstOrDefault(hd => hd.idBan == _banHT.id && hd.tinhtrang == 0);
            if (cnhoadon == null || !cnhoadon.HoaDonChiTiets.Any())
            {
                _banHT.tinhtrang = "Có Người";
                _context.Entry(_banHT).State = EntityState.Modified;
                _context.SaveChanges();
                BtnBanAn.BackColor = Color.Red;
            }

            // Cập nhật lại danh sách bàn ăn
            LoadBanAn();

            // Cập nhật cbochuyenban
            LoadDSBanTrong();
        }

        //-------------Xử Lý 3 gọi hàm đến FormThanhToan---------------//
        public ListView GetThongTinHoaDonListView()
        {
            return lsvthongtinhoadon;
        }

        public TextBox GetTongTien()
        {
            return txttongtien;
        }

        public DatBan GetCurrentBan()
        {
            return _banHT;
        }

        //-------------Xử Lý Khi Click Chọn 1 Hóa Đơn---------------//
        private void lsvthongtinhoadon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvthongtinhoadon.SelectedItems.Count > 0)
            {
                var si = lsvthongtinhoadon.SelectedItems[0];
                MessageBox.Show($"Tên Thức Ăn: {si.SubItems[3].Text}, Số Lượng: {si.SubItems[4].Text}");
            }
        }

        //-------------Xử Lý Tổ Hợp Phím Ctrl + A Để Bôi Đen Hết DS Hóa Đơn---------------//
        private void lsvthongtinhoadon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                foreach (ListViewItem item in lsvthongtinhoadon.Items)
                {
                    item.Selected = true;
                }
            }
        }

        //-------------Xử Lý Xóa Thức Ăn Trong Hóa Đơn---------------//
        private void btnxoathucan_Click(object sender, EventArgs e)
        {
            if (lsvthongtinhoadon.SelectedItems.Count > 0)
            {
                var Result = MessageBox.Show("Bạn đã chắc chắn chưa?", "Xóa Món Ăn", MessageBoxButtons.YesNo);
                if (Result == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lsvthongtinhoadon.SelectedItems)
                    {
                        // Lấy tên thức ăn và số lượng từ item được chọn
                        var tenthucan = item.SubItems[3].Text;
                        var soluong = int.Parse(item.SubItems[4].Text);

                        // Lấy thông tin hóa đơn hiện tại
                        var hoadon = _context.HoaDons.FirstOrDefault(hd => hd.idBan == _banHT.id && hd.tinhtrang == 0);

                        if (hoadon != null)
                        {
                            // Lấy thông tin chi tiết hóa đơn cần xóa
                            var hdct = _context.HoaDonChiTiets
                                .FirstOrDefault(ct => ct.idHoaDon == hoadon.id && ct.ThucAn.tenthucan == tenthucan && ct.soluong == soluong);

                            if (hdct != null)
                            {
                                // Xóa chi tiết hóa đơn khỏi cơ sở dữ liệu
                                _context.HoaDonChiTiets.Remove(hdct);
                                _context.SaveChanges();

                                // Xóa item khỏi list view
                                lsvthongtinhoadon.Items.Remove(item);
                            }
                        }
                        else
                        {
                            lsvthongtinhoadon.Items.Add(new ListViewItem("Chưa Có Hóa Đơn!!"));
                        }
                    }

                    // Cập nhật lại tổng tiền sau khi xóa món ăn
                    CapNhatDSHoaDon(_banHT);

                    // Cập nhật lại trạng thái bàn ăn
                    CapNhatTrangThaiBan();

                    // Kiểm tra xem hóa đơn có còn tồn tại hay không và cập nhật trạng thái bàn
                    var cnhoadon = _context.HoaDons.FirstOrDefault(hd => hd.idBan == _banHT.id && hd.tinhtrang == 0);
                    if (cnhoadon == null || !cnhoadon.HoaDonChiTiets.Any())
                    {
                        _banHT.tinhtrang = "Trống";
                        _context.Entry(_banHT).State = EntityState.Modified;
                        _context.SaveChanges();
                        BtnBanAn.BackColor = Color.Gold;
                    }

                    // Cập nhật lại danh sách bàn ăn
                    LoadBanAn();

                    // Cập nhật cbochuyenban
                    LoadDSBanTrong();
                }
                else
                {
                    MessageBox.Show("Vui Lòng Chọn Món Ăn Để Xóa", "Thông Báo");
                }
            }
            else
            {
                MessageBox.Show("Không có món ăn nào được chọn để xóa", "Thông Báo");
            }
        }

        //-------------Xử Lý Thiết Kế Danh Sách Hóa Đơn---------------//
        private void FormPhầnMềmQLBánHàng_Load(object sender, EventArgs e)
        {
            // cấu trúc các cột
            lsvthongtinhoadon.Columns.Add("ngày và giờ check in", 200, HorizontalAlignment.Left);
            lsvthongtinhoadon.Columns.Add("ngày và giờ check out", 200, HorizontalAlignment.Left);
            lsvthongtinhoadon.Columns.Add("Tình Trạng thanh toán", 200, HorizontalAlignment.Left);
            lsvthongtinhoadon.Columns.Add("Tên Thức Ăn", 200, HorizontalAlignment.Center);
            lsvthongtinhoadon.Columns.Add("Số Lượng", 100, HorizontalAlignment.Center);
            lsvthongtinhoadon.Columns.Add("Giá", 100, HorizontalAlignment.Center);

            // các thuộc tính khác
            lsvthongtinhoadon.View = View.Details;
            lsvthongtinhoadon.FullRowSelect = true;

            // cbodanhmuc lấy dữ liệu
            LoadDanhMuc();
        }

        //-------------Xử Lý Chọn Thức Ăn Dựa Theo Danh Mục---------------//
        private void cbodanhmuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedDanhMuc = cbodanhmuc.SelectedItem as DanhMuc;
            if (selectedDanhMuc != null)
            {
                int idDanhMuc = selectedDanhMuc.id; // Lấy ID từ đối tượng DanhMuc
                LoadThucAnTheoDanhMuc(idDanhMuc);
            }
        }

        //-------------Xử Lý Quyền Truy Cập Của QTV---------------//
        private void quảnTrịViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chỉ có quản trị viên mới được truy cập.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //-------------Xử Lý Xem Hồ Sơ Cá Nhân Và Chỉnh Sửa---------------//
        private void hồSơCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormThongTinCaNhan f = new FormThongTinCaNhan(FormDangNhap.LoggedInUser);
            f.ShowDialog();
        }

        //-------------Xử Lý Đổi Mật Khẩu---------------//
        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDoiMatKhau f = new FormDoiMatKhau(FormDangNhap.LoggedInUser);
            f.ShowDialog(); 
        }

        //-------------Xử Lý Đăng Xuất---------------//
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //-------------Xử Lý Thanh Toán Thức Ăn Trong Hóa Đơn---------------//
        private void btnthanhtoan_Click(object sender, EventArgs e)
        {
            if (_banHT == null || string.IsNullOrEmpty(_banHT.tenban))
            {
                MessageBox.Show("Vui Lòng Chọn Bàn Trước Khi Thanh Toán!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hoadon = _context.HoaDons.FirstOrDefault(hd => hd.idBan == _banHT.id && hd.tinhtrang == 0);

            if (hoadon == null)
            {
                MessageBox.Show("Bàn này không có hóa đơn nên không thể thanh toán", "Thông Báo", MessageBoxButtons.OK);
                return;
            }

            // Truyền FormPhầnMềmQLBánHàng vào FormThanhToan
            FormThanhToan tt = new FormThanhToan(this);
            tt.ShowDialog();

            if (tt.DialogResult == DialogResult.OK)
            {
                // Cập nhật tình trạng hóa đơn và bàn sau khi thanh toán
                hoadon.tinhtrang = 1;
                hoadon.ngayvagiosau = DateTime.Now;
                _context.Entry(hoadon).State = EntityState.Modified;
                _context.SaveChanges();

                _banHT.tinhtrang = "Trống";
                _context.Entry(_banHT).State = EntityState.Modified;
                _context.SaveChanges();

                LoadBanAn();
                CapNhatDSHoaDon(_banHT);
                // Cập nhật cbochuyenban
                LoadDSBanTrong();
            }
        }

        //-------------Xử Lý Lựa Chọn Chuyển Bàn Ăn---------------//
        private void cbochuyenban_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBanAn();
            LoadDSBanTrong();
        }

        //-------------Xử Lý Chuyển Bàn Ăn Và Hóa Đơn---------------//
        private void btnchuyenban_Click(object sender, EventArgs e)
        {
            if (_banHT == null)
            {
                MessageBox.Show("Vui lòng chọn bàn cần chuyển.");
                return;
            }

            var banMoi = cbochuyenban.SelectedItem as DatBan;

            if (banMoi == null)
            {
                MessageBox.Show("Vui lòng chọn bàn mới để chuyển.");
                return;
            }

            // Chuyển hóa đơn sang bàn mới
            var hoadonHienTai = _context.HoaDons.FirstOrDefault(hd => hd.idBan == _banHT.id && hd.tinhtrang == 0);
            if (hoadonHienTai != null)
            {
                hoadonHienTai.idBan = banMoi.id;
                _context.Entry(hoadonHienTai).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Bàn này không có hóa đơn để chuyển!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cập nhật trạng thái của bàn cũ thành "Trống"
            _banHT.tinhtrang = "Trống";
            _context.Entry(_banHT).State = EntityState.Modified;

            // Cập nhật trạng thái của bàn mới thành "Có Người"
            banMoi.tinhtrang = "Có Người";
            _context.Entry(banMoi).State = EntityState.Modified;

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            // Tải lại danh sách bàn ăn và cập nhật giao diện
            LoadBanAn();
            CapNhatDSHoaDon(banMoi);
            LoadDSBanTrong();

            MessageBox.Show($"Đã chuyển bàn {_banHT.tenban} sang bàn {banMoi.tenban}.");
        }

        //-------------Xử Lý Áp Voucher Vào Hóa Đơn---------------//
        private void btngiamgia_Click(object sender, EventArgs e)
        {
            if (_banHT != null)
            {
                decimal tongtien = _giagoc;
                int giamgiagt = 0;

                // Kiểm tra lựa chọn trong ComboBox
                string selectedDiscount = cbogiamgia.SelectedItem.ToString();

                switch (selectedDiscount)
                {
                    case "Không có (0%)":
                        tongtien = _giagoc;
                        break;
                    case "Nhân viên (30%)":
                        tongtien *= 0.7m; // Giảm 30%
                        giamgiagt = 1;
                        break;
                    case "Người quen (50%)":
                        tongtien *= 0.5m; // Giảm 50%
                        giamgiagt = 2;
                        break;
                    case "Người nhà (70%)":
                        tongtien *= 0.3m;  // Giảm 70%
                        giamgiagt = 3;
                        break;
                    default:
                        MessageBox.Show("Mã giảm giá không hợp lệ.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                txttongtien.Text = tongtien.ToString("C");
                // cập giảm giá vào sql
                var hoadon = _context.HoaDons.FirstOrDefault(hd => hd.idBan == _banHT.id && hd.tinhtrang == 0);
                if ( hoadon != null)
                {
                    hoadon.giamgia = giamgiagt;
                    _context.SaveChanges();
                }

                MessageBox.Show($"Đã Áp Dụng Giảm Giá {_banHT.tenban}!");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bàn để áp dụng giảm giá!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //-------------Xử Lý Lựa CHọn Đối Tượng Voucher Vào Hóa Đơn---------------//
        private void cbogiamgia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion   
    }
}
