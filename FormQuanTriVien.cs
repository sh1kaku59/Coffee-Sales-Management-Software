using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_Bán_Hàng_Cafa
{
    public partial class FormQuanTriVien : Form
    {
        QuanLyCafeEntities quanLyCafeEntities = new QuanLyCafeEntities();

        public FormQuanTriVien()
        {
            InitializeComponent();
        }

        #region method

        //------------------------ void quản lý tài khoản ------------------------------//
        void Loadtaikhoanlist()
        {
            using (var context = new QuanLyCafeEntities()) 
            {
                var taikhoanlist = context.TaiKhoans.ToList();
                dtgvtk.DataSource = taikhoanlist;
            }
        }

        void CreateAccount()
        {
            string tentk = txttentk.Text;
            string tenht = txttenht.Text;
            string matkhau = "123";
            int loaitk = Convert.ToInt32(cboloaitk.SelectedValue);

            if (string.IsNullOrEmpty(tentk) || string.IsNullOrEmpty(tenht))
            {
                MessageBox.Show("Vui lòng không được để trống thông tin.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (loaitk < 0)
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản hợp lệ.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var them = new QuanLyCafeEntities())
            {
                var newtk = new TaiKhoan
                {
                    taikhoan1 = tentk,
                    tenhienthi = tenht,
                    matkhau = matkhau,
                    loaitk = loaitk,
                };

                them.TaiKhoans.Add(newtk);
                them.SaveChanges();

                Loadtaikhoanlist();

                MessageBox.Show("Thêm tài khoản thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void UpdateAccount()
        {
            if (dtgvtk.SelectedRows.Count > 0)
            {
                // xác định giá trị của khóa chính (taikhoan)
                String selectedTaiKhoan = dtgvtk.SelectedRows[0].Cells["taikhoan1"].Value.ToString();

                // TÌM TÀI KHOẢN CÓ TRONG DATABASE
                var accountToUpdate = quanLyCafeEntities.TaiKhoans.FirstOrDefault(tk => tk.taikhoan1 == selectedTaiKhoan);
                if (accountToUpdate != null)
                {
                    // CẬP NHẬP THÔNG TIN TỪ TEXTBOX VÀ COMBOBOX
                    accountToUpdate.tenhienthi = txttenht.Text;
                    accountToUpdate.loaitk = Convert.ToInt32(cboloaitk.SelectedItem);

                    // LƯU THAY ĐỔI VÀO CƠ SỞ DỮ LIỆU
                    quanLyCafeEntities.SaveChanges();

                    // CẬP NHẬP LẠI DATAGRIDVIEW
                    Loadtaikhoanlist();
                    MessageBox.Show("Chỉnh sửa tài khoản thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không Tìm Thấy Tài Khoản Cần Cập Nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui Lòng Chọn Lại Tài Khoản Cần Cập Nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        void DeleteAccount()
        {
            if (dtgvtk.SelectedRows.Count > 0)
            {
                // xác định giá trị của khóa chính (taikhoan)
                String selectedTaiKhoan = dtgvtk.SelectedRows[0].Cells["taikhoan1"].Value.ToString();

                // Kiểm tra nếu tài khoản đang chọn là tài khoản đang đăng nhập
                if (selectedTaiKhoan == FormDangNhap.LoggedInUser.taikhoan1)
                {
                    MessageBox.Show("Bạn không thể xóa tài khoản mà bạn đang đăng nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // xác nhận trước khi xóa
                DialogResult result = MessageBox.Show("Bạn Có Chắc Chắn Sẽ Xóa Tài Khoản Này?", "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Tìm tài khoản trong database dựa trên giá trị taikhoan
                    var accountToDelete = quanLyCafeEntities.TaiKhoans.FirstOrDefault(tk => tk.taikhoan1 == selectedTaiKhoan);
                    if (accountToDelete != null)
                    {
                        quanLyCafeEntities.TaiKhoans.Remove(accountToDelete);
                        quanLyCafeEntities.SaveChanges();
                        Loadtaikhoanlist();
                        MessageBox.Show("Xóa tài khoản thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không Tìm Tài Khoản Cần Xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui Lòng Chọn Tài Khoản Cần Xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //------------------------ void quản lý thức ăn ------------------------------//
        void Loadthucanlist()
        {
            using (var context = new QuanLyCafeEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;

                var thucanlist = context.ThucAns.OrderBy(ta => ta.idDanhMuc).ToList();
                dtgvthucan.DataSource = thucanlist;

                //loại bỏ cột column dư thừa
                dtgvthucan.Columns["DanhMuc"].Visible = false;
                dtgvthucan.Columns["HoaDonChiTiets"].Visible = false;
                dtgvthucan.Columns["id"].Visible = false;
            }
        }

        void Loadcbodanhmuc()
        {
            using (var context = new QuanLyCafeEntities())
            {
                var danhmuclist = context.DanhMucs.ToList();

                cbodanhmuc.DataSource = danhmuclist;
                cbodanhmuc.DisplayMember = "tendanhmuc";
                cbodanhmuc.ValueMember = "id";
            }
        }

        void TimKiemThucAn(string searchKeyword)
        {
            using (var timkiem = new QuanLyCafeEntities())
            {
                // Lazy loading phải được bật để tránh lỗi
                timkiem.Configuration.LazyLoadingEnabled = false;

                // Tìm kiếm gần đúng dựa trên tên hoặc giá món ăn
                var result = timkiem.ThucAns
                                    .Where(ta => ta.tenthucan.Contains(searchKeyword) || ta.gia.ToString().Contains(searchKeyword))
                                    .ToList();

                dtgvthucan.DataSource = result;
            }
        }

        void CreateFood()
        {
            string tenmon = txttenmon.Text;
            int iddanhmuc = Convert.ToInt32(cbodanhmuc.SelectedValue);
            int gia = (int)sogiamonan.Value;

            if (string.IsNullOrEmpty(tenmon))
            {
                MessageBox.Show("Tên món ăn không được để trống.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (iddanhmuc <= 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục hợp lệ.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gia <= 0)
            {
                MessageBox.Show("Vui lòng chọn mức giá hợp lệ.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var them = new QuanLyCafeEntities())
            {
                var newfood = new ThucAn
                {
                    tenthucan = tenmon,
                    idDanhMuc = iddanhmuc,
                    gia = gia,
                };

                them.ThucAns.Add(newfood);
                them.SaveChanges();

                Loadthucanlist();

                MessageBox.Show("Thêm món ăn thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        void UpdateFood()
        {
            if (dtgvthucan.SelectedRows.Count > 0)
            {
                // xác định giá trị của khóa chính (thức ăn)
                int selectedThucan = int.Parse(dtgvthucan.SelectedRows[0].Cells["id"].Value.ToString());

                // TÌM THỨC ĂN CÓ TRONG DATABASE
                var foodToUpdate = quanLyCafeEntities.ThucAns.FirstOrDefault(ta => ta.id == selectedThucan);
                if (foodToUpdate != null)
                {
                    // CẬP NHẬP THÔNG TIN TỪ TEXTBOX VÀ COMBOBOX
                    foodToUpdate.tenthucan = txttenmon.Text;
                    foodToUpdate.idDanhMuc = Convert.ToInt32(cbodanhmuc.SelectedValue);
                    foodToUpdate.gia = Convert.ToInt32(sogiamonan.Value);

                    // LƯU THAY ĐỔI VÀO CƠ SỞ DỮ LIỆU
                    quanLyCafeEntities.SaveChanges();

                    // CẬP NHẬP LẠI DATAGRIDVIEW
                    Loadthucanlist();

                    MessageBox.Show("Đã Cập Nhập Thành Công Món Ăn!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Không Tìm Thấy Thức Ăn Cần Cập Nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui Lòng Chọn Lại Thức Ăn Cần Cập Nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        } 

        void DeleteFood()
        {
            if (dtgvthucan.SelectedRows.Count > 0)
            {
                // xác định giá trị của khóa chính (thức ăn)
                int selectedThucan =int.Parse(dtgvthucan.SelectedRows[0].Cells["id"].Value.ToString());

                // xác nhận trước khi xóa
                DialogResult result = MessageBox.Show("Bạn Có Chắc Chắn Sẽ Xóa Thức Ăn Này?", "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Xóa Khóa Phụ Thuộc Của Thức Ăn là Hóa Đơn Chi Tiết
                    var chitietToDelete = quanLyCafeEntities.HoaDonChiTiets.Where(hdct => hdct.idThucAn == selectedThucan).ToList();
                    foreach (var chitiet in chitietToDelete)
                    {
                        quanLyCafeEntities.HoaDonChiTiets.Remove(chitiet);
                    }

                    // Tìm tài khoản trong database dựa trên giá trị taikhoan
                    var foodToDelete = quanLyCafeEntities.ThucAns.FirstOrDefault(ta => ta.id == selectedThucan);
                    if (foodToDelete != null)
                    {
                        quanLyCafeEntities.ThucAns.Remove(foodToDelete);
                        quanLyCafeEntities.SaveChanges();
                        Loadthucanlist();
                        MessageBox.Show("Xóa Thành Công Món Ăn!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không Tìm Thức Ăn Cần Xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui Lòng Chọn Thức Ăn Cần Xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //------------------------ void quản lý danh mục ------------------------------//
        void Loaddanhmuclist()
        {
            using (var context = new QuanLyCafeEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;

                var danhmuclist = context.DanhMucs.ToList();
                dtgvdanhmuc.DataSource = danhmuclist;

                //loại bỏ cột column dư thừa
                dtgvdanhmuc.Columns["ThucAns"].Visible = false;
            }
        }

        void Createdanhmuc()
        {
            string tendanhmuc = txttendanhmuc.Text;

            if (string.IsNullOrEmpty(tendanhmuc))
            {
                MessageBox.Show("Tên danh mục không được để trống.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var them = new QuanLyCafeEntities())
            {
                var newdanhmuc = new DanhMuc
                {
                    tendanhmuc = tendanhmuc,
                };

                them.DanhMucs.Add(newdanhmuc);
                them.SaveChanges();

                Loaddanhmuclist();

                MessageBox.Show("Thêm danh mục thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void Updatedanhmuc()
        {
            if (dtgvdanhmuc.SelectedRows.Count > 0)
            {
                // xác định giá trị của khóa chính (thức ăn)
                int selectedDanhmuc = int.Parse(dtgvdanhmuc.SelectedRows[0].Cells["id"].Value.ToString());

                // TÌM THỨC ĂN CÓ TRONG DATABASE
                var danhmucToUpdate = quanLyCafeEntities.DanhMucs.FirstOrDefault(dm => dm.id == selectedDanhmuc);
                if (danhmucToUpdate != null)
                {
                    // CẬP NHẬP THÔNG TIN TỪ TEXTBOX VÀ COMBOBOX
                    danhmucToUpdate.tendanhmuc = txttendanhmuc.Text;

                    // LƯU THAY ĐỔI VÀO CƠ SỞ DỮ LIỆU
                    quanLyCafeEntities.SaveChanges();

                    // CẬP NHẬP LẠI DATAGRIDVIEW
                    Loaddanhmuclist();

                    MessageBox.Show("Đã Cập Nhập Thành Công Danh Mục!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Không Tìm Thấy Danh Mục Cần Cập Nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui Lòng Chọn Lại Danh Mục Cần Cập Nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        void Deletedanhmuc()
        {
            if (dtgvdanhmuc.SelectedRows.Count > 0)
            {
                // xác định giá trị của khóa chính (danh muc)
                int selectedDanhmuc = int.Parse(dtgvdanhmuc.SelectedRows[0].Cells["id"].Value.ToString());

                // xác nhận trước khi xóa
                DialogResult result = MessageBox.Show("Bạn Có Chắc Chắn Sẽ Xóa Danh Mục Này?", "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Tìm tài khoản trong database dựa trên giá trị taikhoan
                    var danhmucToDelete = quanLyCafeEntities.DanhMucs.FirstOrDefault(dm => dm.id == selectedDanhmuc);
                    if (danhmucToDelete != null)
                    {
                        quanLyCafeEntities.DanhMucs.Remove(danhmucToDelete);
                        quanLyCafeEntities.SaveChanges();
                        Loaddanhmuclist();
                        MessageBox.Show("Xóa Thành Công Danh Mục!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không Tìm Danh Mục Cần Xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui Lòng Chọn Danh Mục Cần Xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //------------------------ void quản lý bàn ăn ------------------------------//
        void Loadbananlist()
        {
            using (var context = new QuanLyCafeEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;

                var bananlist = context.DatBans.ToList();
                dtgvban.DataSource = bananlist;

                //loại bỏ cột column dư thừa
                dtgvban.Columns["id"].Visible = false;
                dtgvban.Columns["HoaDons"].Visible = false;
            }
        }

        void Createbanan()
        {
            string tenban = txttenban.Text;
            string trangthai = cbotrangthai.SelectedItem.ToString();

            if (string.IsNullOrEmpty(tenban))
            {
                MessageBox.Show("Tên bàn không được để trống.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var them = new QuanLyCafeEntities())
            {
                var newbanan = new DatBan
                {
                    tenban = tenban,
                    tinhtrang = trangthai,
                };

                them.DatBans.Add(newbanan);
                them.SaveChanges();

                Loadbananlist();

                MessageBox.Show("Thêm bàn mới thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void Updatebanan()
        {
            if (dtgvban.SelectedRows.Count > 0)
            {
                // xác định giá trị của khóa chính (thức ăn)
                int selectedDatban = int.Parse(dtgvban.SelectedRows[0].Cells["id"].Value.ToString());

                // TÌM THỨC ĂN CÓ TRONG DATABASE
                var bananToUpdate = quanLyCafeEntities.DatBans.FirstOrDefault(db => db.id == selectedDatban);
                if (bananToUpdate != null)
                {
                    // CẬP NHẬP THÔNG TIN TỪ TEXTBOX VÀ COMBOBOX
                    bananToUpdate.tenban = txttenban.Text;
                    bananToUpdate.tinhtrang = cbotrangthai.SelectedItem.ToString();

                    // LƯU THAY ĐỔI VÀO CƠ SỞ DỮ LIỆU
                    quanLyCafeEntities.SaveChanges();

                    // CẬP NHẬP LẠI DATAGRIDVIEW
                    Loadbananlist();

                    MessageBox.Show("Đã Cập Nhập Thành Công Bàn Ăn!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Không Tìm Thấy Bàn Ăn Cần Cập Nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui Lòng Chọn Lại Bàn Ăn Cần Cập Nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        void Deletebanan()
        {
            if (dtgvban.SelectedRows.Count > 0)
            {
                // xác định giá trị của khóa chính (danh muc)
                int selectedBanan = int.Parse(dtgvban.SelectedRows[0].Cells["id"].Value.ToString());

                // xác nhận trước khi xóa
                DialogResult result = MessageBox.Show("Bạn Có Chắc Chắn Sẽ Xóa Bàn Ăn Này?", "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Xóa các chi tiết hóa đơn liên quan đến các hóa đơn của bàn ăn
                    var hoadons = quanLyCafeEntities.HoaDons.Where(hd => hd.idBan == selectedBanan).ToList();
                    foreach (var hoadon in hoadons)
                    {
                        var chitietToDelete = quanLyCafeEntities.HoaDonChiTiets.Where(hdct => hdct.idHoaDon == hoadon.id).ToList();
                        foreach (var chitiet in chitietToDelete)
                        {
                            quanLyCafeEntities.HoaDonChiTiets.Remove(chitiet);
                        }
                        quanLyCafeEntities.SaveChanges();
                        quanLyCafeEntities.HoaDons.Remove(hoadon);
                    }

                    // Tìm tài khoản trong database dựa trên giá trị bàn ăn
                    var bananToDelete = quanLyCafeEntities.DatBans.FirstOrDefault(db => db.id == selectedBanan);
                    if (bananToDelete != null)
                    {
                        quanLyCafeEntities.DatBans.Remove(bananToDelete);
                        quanLyCafeEntities.SaveChanges();
                        Loadbananlist();
                        MessageBox.Show("Xóa Thành Công Bàn Ăn!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không Tìm Bàn Ăn Cần Xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui Lòng Chọn Bàn Ăn Cần Xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        #region events

        //------------------------ quản lý tài khoản ------------------------------//
        private void btnloadtk_Click(object sender, EventArgs e)
        {
            Loadtaikhoanlist();
        }

        private void dtgvtk_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Loadtaikhoanlist();
        }

        private void dtgvtk_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvtk.Rows[e.RowIndex];
                txttentk.Text = row.Cells["taikhoan1"].Value.ToString();
                txttenht.Text = row.Cells["tenhienthi"].Value.ToString();
                cboloaitk.SelectedItem = row.Cells["loaitk"].Value.ToString();
            }
        }

        private void btnthemtk_Click(object sender, EventArgs e)
        {
            CreateAccount();
        }

        private void btnsuatk_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        } 
        
        private void btnxoatk_Click(object sender, EventArgs e)
        {
            DeleteAccount();
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            if (dtgvtk.SelectedRows.Count > 0)
            {
                // Lấy Thông Tin Được Chọn
                string selectedTK = dtgvtk.SelectedRows[0].Cells["taikhoan1"].Value.ToString();

                // Xác Nhận Reset Mật Khẩu
                DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn reset mật khẩu cho tài khoản này?", "Xác nhận Reset Mật Khẩu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (kq == DialogResult.Yes)
                {
                    // Tìm Tài Khoản Trong Database
                    var accountToReset = quanLyCafeEntities.TaiKhoans.FirstOrDefault(tk => tk.taikhoan1 == selectedTK);
                    if (accountToReset != null)
                    {
                        accountToReset.matkhau = "123";
                        quanLyCafeEntities.SaveChanges();
                        Loadtaikhoanlist();
                        MessageBox.Show("Reset mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài khoản cần reset!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn tài khoản cần reset!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //------------------------ quản lý hóa đơn ------------------------------//
        private void btnxem_Click(object sender, EventArgs e)
        {
            DateTime ngaybatdau = dtpdaungay.Value.Date;
            DateTime ngayketthuc = dtpcuoingay.Value.Date;

            using (var ngay = new QuanLyCafeEntities())
            {
                //Vô hiệu hóa Lazy Loading Nhấn Dòng
                ngay.Configuration.LazyLoadingEnabled = false;

                // truy vấn lấy danh sách hóa đơn trong khoảng thời gian
                var hoadonlist = ngay.HoaDons.Where(hd => hd.ngayvagiotruoc >= ngaybatdau && hd.ngayvagiotruoc <= ngayketthuc).ToList();

                dtgvhoadon.DataSource = hoadonlist;

                //loại bỏ cột column dư thừa
                dtgvhoadon.Columns["DatBan"].Visible = false;
                dtgvhoadon.Columns["HoaDonChiTiets"].Visible = false;
            }
        }

        //------------------------ quản lý thức ăn ------------------------------//
        private void btnload_Click(object sender, EventArgs e)
        {
            Loadthucanlist();
            Loadcbodanhmuc();
        }

        private void dtgvthucan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvthucan.Rows[e.RowIndex];
                txtidmon.Text = row.Cells["id"].Value.ToString();
                txttenmon.Text = row.Cells["tenthucan"].Value.ToString();
                sogiamonan.Text = row.Cells["gia"].Value.ToString();

                int idDanhMuc = Convert.ToInt32(row.Cells["idDanhMuc"].Value);
                cbodanhmuc.SelectedValue = idDanhMuc;
            }
        }

        private void dtgvthucan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Loadthucanlist();
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtmonan.Text.Trim();
            TimKiemThucAn(searchKeyword);
        }

        private void btnsuamon_Click(object sender, EventArgs e)
        {
            UpdateFood();
        }

        private void btnxoamon_Click(object sender, EventArgs e)
        {
            DeleteFood();
        }

        private void btnthemmon_Click(object sender, EventArgs e)
        {
            CreateFood();
        }

        //------------------------ quản lý danh mục ------------------------------//
        private void btnloaddanhmuc_Click(object sender, EventArgs e)
        {
           Loaddanhmuclist();
        }

        private void btnthemdanhmuc_Click(object sender, EventArgs e)
        {
            Createdanhmuc();
        }

        private void btnsuadanhmuc_Click(object sender, EventArgs e)
        {
            Updatedanhmuc();
        }

        private void btnxoadanhmuc_Click(object sender, EventArgs e)
        {
            Deletedanhmuc();
        }

        private void dtgvdanhmuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvdanhmuc.Rows[e.RowIndex];
                txtiddanhmuc.Text = row.Cells["id"].Value.ToString();
                txttendanhmuc.Text = row.Cells["tendanhmuc"].Value.ToString();
            }
        }

        //------------------------ quản lý bàn ăn ------------------------------//
        private void btnloadban_Click(object sender, EventArgs e)
        {
            Loadbananlist();
            cbotrangthai.Items.Clear();
            cbotrangthai.Items.Add("Trống");
            cbotrangthai.Items.Add("Có Người");
        }

        private void dtgvban_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvban.Rows[e.RowIndex];
                txtidban.Text = row.Cells["id"].Value.ToString();
                txttenban.Text = row.Cells["tenban"].Value.ToString();

                string tinhtrang = row.Cells["tinhtrang"].Value.ToString();
                cbotrangthai.SelectedItem = tinhtrang;
            }
        }

        private void dtgvban_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Loadbananlist();
        }

        private void btnthemban_Click(object sender, EventArgs e)
        {
            Createbanan();
        }

        private void btnxoaban_Click(object sender, EventArgs e)
        {
            Deletebanan();
        }

        private void btnsuaban_Click(object sender, EventArgs e)
        {
            Updatebanan();
        }
    }
    #endregion
}
