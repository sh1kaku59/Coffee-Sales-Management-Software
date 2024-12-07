using System.Windows.Forms;

namespace App_Bán_Hàng_Cafa
{
    partial class FormPhầnMềmQLBánHàng
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPhầnMềmQLBánHàng));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.quảnTrịViênToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýTàiKhoảnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hồSơCáNhânToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đổiMậtKhẩuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lsvthongtinhoadon = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbogiamgia = new System.Windows.Forms.ComboBox();
            this.txttongtien = new System.Windows.Forms.TextBox();
            this.cbochuyenban = new System.Windows.Forms.ComboBox();
            this.btnchuyenban = new System.Windows.Forms.Button();
            this.btngiamgia = new System.Windows.Forms.Button();
            this.btnthanhtoan = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnxoathucan = new System.Windows.Forms.Button();
            this.slmonan = new System.Windows.Forms.NumericUpDown();
            this.btnthemthucan = new System.Windows.Forms.Button();
            this.cbothucan = new System.Windows.Forms.ComboBox();
            this.cbodanhmuc = new System.Windows.Forms.ComboBox();
            this.FLPban = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnBanAn = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slmonan)).BeginInit();
            this.FLPban.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnTrịViênToolStripMenuItem,
            this.quảnLýTàiKhoảnToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1280, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // quảnTrịViênToolStripMenuItem
            // 
            this.quảnTrịViênToolStripMenuItem.Name = "quảnTrịViênToolStripMenuItem";
            this.quảnTrịViênToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.quảnTrịViênToolStripMenuItem.Text = "Quản Trị Viên";
            this.quảnTrịViênToolStripMenuItem.Click += new System.EventHandler(this.quảnTrịViênToolStripMenuItem_Click);
            // 
            // quảnLýTàiKhoảnToolStripMenuItem
            // 
            this.quảnLýTàiKhoảnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hồSơCáNhânToolStripMenuItem,
            this.đổiMậtKhẩuToolStripMenuItem,
            this.đăngXuấtToolStripMenuItem});
            this.quảnLýTàiKhoảnToolStripMenuItem.Name = "quảnLýTàiKhoảnToolStripMenuItem";
            this.quảnLýTàiKhoảnToolStripMenuItem.Size = new System.Drawing.Size(143, 24);
            this.quảnLýTàiKhoảnToolStripMenuItem.Text = "Quản Lý Tài Khoản";
            // 
            // hồSơCáNhânToolStripMenuItem
            // 
            this.hồSơCáNhânToolStripMenuItem.Name = "hồSơCáNhânToolStripMenuItem";
            this.hồSơCáNhânToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.hồSơCáNhânToolStripMenuItem.Text = "Hồ Sơ Cá Nhân";
            this.hồSơCáNhânToolStripMenuItem.Click += new System.EventHandler(this.hồSơCáNhânToolStripMenuItem_Click);
            // 
            // đổiMậtKhẩuToolStripMenuItem
            // 
            this.đổiMậtKhẩuToolStripMenuItem.Name = "đổiMậtKhẩuToolStripMenuItem";
            this.đổiMậtKhẩuToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.đổiMậtKhẩuToolStripMenuItem.Text = "Đổi Mật Khẩu";
            this.đổiMậtKhẩuToolStripMenuItem.Click += new System.EventHandler(this.đổiMậtKhẩuToolStripMenuItem_Click);
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.đăngXuấtToolStripMenuItem.Text = "Đăng Xuất";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lsvthongtinhoadon);
            this.panel2.Location = new System.Drawing.Point(625, 151);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(643, 286);
            this.panel2.TabIndex = 1;
            // 
            // lsvthongtinhoadon
            // 
            this.lsvthongtinhoadon.BackColor = System.Drawing.Color.Gold;
            this.lsvthongtinhoadon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvthongtinhoadon.GridLines = true;
            this.lsvthongtinhoadon.HideSelection = false;
            this.lsvthongtinhoadon.Location = new System.Drawing.Point(3, 3);
            this.lsvthongtinhoadon.Name = "lsvthongtinhoadon";
            this.lsvthongtinhoadon.Size = new System.Drawing.Size(634, 280);
            this.lsvthongtinhoadon.TabIndex = 0;
            this.lsvthongtinhoadon.UseCompatibleStateImageBehavior = false;
            this.lsvthongtinhoadon.View = System.Windows.Forms.View.List;
            this.lsvthongtinhoadon.SelectedIndexChanged += new System.EventHandler(this.lsvthongtinhoadon_SelectedIndexChanged);
            this.lsvthongtinhoadon.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsvthongtinhoadon_KeyDown);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panel3.Controls.Add(this.cbogiamgia);
            this.panel3.Controls.Add(this.txttongtien);
            this.panel3.Controls.Add(this.cbochuyenban);
            this.panel3.Controls.Add(this.btnchuyenban);
            this.panel3.Controls.Add(this.btngiamgia);
            this.panel3.Controls.Add(this.btnthanhtoan);
            this.panel3.Location = new System.Drawing.Point(625, 443);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(643, 132);
            this.panel3.TabIndex = 2;
            // 
            // cbogiamgia
            // 
            this.cbogiamgia.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbogiamgia.FormattingEnabled = true;
            this.cbogiamgia.Location = new System.Drawing.Point(240, 97);
            this.cbogiamgia.Name = "cbogiamgia";
            this.cbogiamgia.Size = new System.Drawing.Size(152, 24);
            this.cbogiamgia.TabIndex = 8;
            this.cbogiamgia.SelectedIndexChanged += new System.EventHandler(this.cbogiamgia_SelectedIndexChanged);
            // 
            // txttongtien
            // 
            this.txttongtien.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttongtien.ForeColor = System.Drawing.Color.Gold;
            this.txttongtien.Location = new System.Drawing.Point(434, 12);
            this.txttongtien.Name = "txttongtien";
            this.txttongtien.ReadOnly = true;
            this.txttongtien.Size = new System.Drawing.Size(194, 27);
            this.txttongtien.TabIndex = 7;
            // 
            // cbochuyenban
            // 
            this.cbochuyenban.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.cbochuyenban.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbochuyenban.FormattingEnabled = true;
            this.cbochuyenban.Location = new System.Drawing.Point(46, 97);
            this.cbochuyenban.Name = "cbochuyenban";
            this.cbochuyenban.Size = new System.Drawing.Size(157, 24);
            this.cbochuyenban.TabIndex = 4;
            this.cbochuyenban.UseWaitCursor = true;
            // 
            // btnchuyenban
            // 
            this.btnchuyenban.BackColor = System.Drawing.Color.Gold;
            this.btnchuyenban.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnchuyenban.Location = new System.Drawing.Point(46, 12);
            this.btnchuyenban.Name = "btnchuyenban";
            this.btnchuyenban.Size = new System.Drawing.Size(157, 61);
            this.btnchuyenban.TabIndex = 6;
            this.btnchuyenban.Text = "Chuyển Bàn";
            this.btnchuyenban.UseVisualStyleBackColor = false;
            this.btnchuyenban.Click += new System.EventHandler(this.btnchuyenban_Click);
            // 
            // btngiamgia
            // 
            this.btngiamgia.BackColor = System.Drawing.Color.Gold;
            this.btngiamgia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngiamgia.Location = new System.Drawing.Point(240, 12);
            this.btngiamgia.Name = "btngiamgia";
            this.btngiamgia.Size = new System.Drawing.Size(152, 61);
            this.btngiamgia.TabIndex = 5;
            this.btngiamgia.Text = "Giảm Giá";
            this.btngiamgia.UseVisualStyleBackColor = false;
            this.btngiamgia.Click += new System.EventHandler(this.btngiamgia_Click);
            // 
            // btnthanhtoan
            // 
            this.btnthanhtoan.BackColor = System.Drawing.Color.Gold;
            this.btnthanhtoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnthanhtoan.Location = new System.Drawing.Point(434, 54);
            this.btnthanhtoan.Name = "btnthanhtoan";
            this.btnthanhtoan.Size = new System.Drawing.Size(194, 67);
            this.btnthanhtoan.TabIndex = 4;
            this.btnthanhtoan.Text = "Thanh Toán";
            this.btnthanhtoan.UseVisualStyleBackColor = false;
            this.btnthanhtoan.Click += new System.EventHandler(this.btnthanhtoan_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panel4.Controls.Add(this.btnxoathucan);
            this.panel4.Controls.Add(this.slmonan);
            this.panel4.Controls.Add(this.btnthemthucan);
            this.panel4.Controls.Add(this.cbothucan);
            this.panel4.Controls.Add(this.cbodanhmuc);
            this.panel4.Location = new System.Drawing.Point(625, 41);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(643, 104);
            this.panel4.TabIndex = 3;
            // 
            // btnxoathucan
            // 
            this.btnxoathucan.BackColor = System.Drawing.Color.Tomato;
            this.btnxoathucan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnxoathucan.BackgroundImage")));
            this.btnxoathucan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnxoathucan.Location = new System.Drawing.Point(509, 19);
            this.btnxoathucan.Name = "btnxoathucan";
            this.btnxoathucan.Size = new System.Drawing.Size(128, 66);
            this.btnxoathucan.TabIndex = 4;
            this.btnxoathucan.UseVisualStyleBackColor = false;
            this.btnxoathucan.Click += new System.EventHandler(this.btnxoathucan_Click);
            // 
            // slmonan
            // 
            this.slmonan.Location = new System.Drawing.Point(250, 38);
            this.slmonan.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.slmonan.Name = "slmonan";
            this.slmonan.Size = new System.Drawing.Size(77, 22);
            this.slmonan.TabIndex = 3;
            this.slmonan.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnthemthucan
            // 
            this.btnthemthucan.BackColor = System.Drawing.Color.LawnGreen;
            this.btnthemthucan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnthemthucan.BackgroundImage")));
            this.btnthemthucan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnthemthucan.Location = new System.Drawing.Point(364, 20);
            this.btnthemthucan.Name = "btnthemthucan";
            this.btnthemthucan.Size = new System.Drawing.Size(128, 65);
            this.btnthemthucan.TabIndex = 2;
            this.btnthemthucan.UseVisualStyleBackColor = false;
            this.btnthemthucan.Click += new System.EventHandler(this.btnthemthucan_Click);
            // 
            // cbothucan
            // 
            this.cbothucan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbothucan.FormattingEnabled = true;
            this.cbothucan.Location = new System.Drawing.Point(3, 61);
            this.cbothucan.Name = "cbothucan";
            this.cbothucan.Size = new System.Drawing.Size(210, 24);
            this.cbothucan.TabIndex = 1;
            // 
            // cbodanhmuc
            // 
            this.cbodanhmuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbodanhmuc.FormattingEnabled = true;
            this.cbodanhmuc.Location = new System.Drawing.Point(3, 19);
            this.cbodanhmuc.Name = "cbodanhmuc";
            this.cbodanhmuc.Size = new System.Drawing.Size(210, 24);
            this.cbodanhmuc.TabIndex = 0;
            this.cbodanhmuc.SelectedIndexChanged += new System.EventHandler(this.cbodanhmuc_SelectedIndexChanged);
            // 
            // FLPban
            // 
            this.FLPban.AutoScroll = true;
            this.FLPban.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.FLPban.Controls.Add(this.BtnBanAn);
            this.FLPban.Location = new System.Drawing.Point(20, 41);
            this.FLPban.Name = "FLPban";
            this.FLPban.Size = new System.Drawing.Size(584, 534);
            this.FLPban.TabIndex = 4;
            // 
            // BtnBanAn
            // 
            this.BtnBanAn.BackColor = System.Drawing.Color.Gold;
            this.BtnBanAn.Location = new System.Drawing.Point(3, 3);
            this.BtnBanAn.Name = "BtnBanAn";
            this.BtnBanAn.Size = new System.Drawing.Size(138, 57);
            this.BtnBanAn.TabIndex = 0;
            this.BtnBanAn.UseVisualStyleBackColor = false;
            this.BtnBanAn.Click += new System.EventHandler(this.BtnBanAn_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // FormPhầnMềmQLBánHàng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(1280, 587);
            this.Controls.Add(this.FLPban);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormPhầnMềmQLBánHàng";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phần Mềm Quản Lý Bán Hàng ";
            this.Load += new System.EventHandler(this.FormPhầnMềmQLBánHàng_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.slmonan)).EndInit();
            this.FLPban.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quảnTrịViênToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLýTàiKhoảnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hồSơCáNhânToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đổiMậtKhẩuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lsvthongtinhoadon;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnthemthucan;
        private System.Windows.Forms.ComboBox cbothucan;
        private System.Windows.Forms.ComboBox cbodanhmuc;
        private System.Windows.Forms.NumericUpDown slmonan;
        private System.Windows.Forms.Button btngiamgia;
        private System.Windows.Forms.Button btnthanhtoan;
        private System.Windows.Forms.FlowLayoutPanel FLPban;
        private System.Windows.Forms.ComboBox cbochuyenban;
        private System.Windows.Forms.Button btnchuyenban;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button BtnBanAn;
        private System.Windows.Forms.TextBox txttongtien;
        private System.Windows.Forms.Button btnxoathucan;
        private ComboBox cbogiamgia;
    }
}