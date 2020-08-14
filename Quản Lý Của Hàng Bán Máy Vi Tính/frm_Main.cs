using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_Lý_Của_Hàng_Bán_Máy_Vi_Tính
{
    public partial class frm_Main : Form
    {
        public frm_Main() {
            InitializeComponent();
        }

        private void frm_Main_Load(object sender, EventArgs e) {
            Class.ConnectionDB.Connect();
        }

        private void mnuNhanHieu_Click(object sender, EventArgs e) {

            frm_NhanHieu frmNhanHieu = new frm_NhanHieu();
            frmNhanHieu.MdiParent = this;
            frmNhanHieu.Show();
        }

        private void mnuHangHoa_Click(object sender, EventArgs e) {
            frm_HangHoa hanghoa = new frm_HangHoa();
            hanghoa.MdiParent = this;
            hanghoa.Show();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e) {
            frm_NhanVien nhanvien = new frm_NhanVien();
            nhanvien.MdiParent = this;
            nhanvien.Show();
        }

        private void mnuHoaDon_Click(object sender, EventArgs e) {
            frm_HoaDon hoadon = new frm_HoaDon();
            hoadon.MdiParent = this;
            hoadon.Show();
        }

        private void mnuTimKiem_Click(object sender, EventArgs e) {
            frm_TimHDBan timkiem = new frm_TimHDBan();
            timkiem.MdiParent = this;
            timkiem.Show();
        }

        private void mnuGioiThieu_Click(object sender, EventArgs e) {
            frm_GioiThieu gioithieu = new frm_GioiThieu();
            gioithieu.MdiParent = this;
            gioithieu.Show();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e) {
            frm_KhachHang khachhang = new frm_KhachHang();
            khachhang.MdiParent = this;
            khachhang.Show();
        }

        private void mnuThoat_Click(object sender, EventArgs e) {
            Class.ConnectionDB.Disconnect();
            Application.Exit();
        }
    }
}
