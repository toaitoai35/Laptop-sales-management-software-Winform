using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Quản_Lý_Của_Hàng_Bán_Máy_Vi_Tính.Class;

namespace Quản_Lý_Của_Hàng_Bán_Máy_Vi_Tính
{
    public partial class frm_TimHDBan : Form
    {
        DataTable thd;
        HoaDonDB hb = new HoaDonDB();
        public frm_TimHDBan() {
            InitializeComponent();
        }

        private void frm_TimHDBan_Load(object sender, EventArgs e) {
            ResetValues();
            dgvTimKiemHoaDon.DataSource = null;
        }
        private void LoadData() {
            dgvTimKiemHoaDon.Columns[0].HeaderText = "Mã Hóa đơn";
            dgvTimKiemHoaDon.Columns[1].HeaderText = "Mã nhân viên";
            dgvTimKiemHoaDon.Columns[2].HeaderText = "Ngày bán";
            dgvTimKiemHoaDon.Columns[3].HeaderText = "Mã khách";
            dgvTimKiemHoaDon.Columns[4].HeaderText = "Tổng tiền";
            dgvTimKiemHoaDon.Columns[0].Width = 180;
            dgvTimKiemHoaDon.Columns[1].Width = 100;
            dgvTimKiemHoaDon.Columns[2].Width = 120;
            dgvTimKiemHoaDon.Columns[3].Width = 120;
            dgvTimKiemHoaDon.Columns[4].Width = 120;
            dgvTimKiemHoaDon.AllowUserToAddRows = false;
            dgvTimKiemHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void ResetValues() {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtMaHDBan.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e) {
            string sql;
            if ((txtMaHDBan.Text == "") && (txtThang.Text == "") && (txtNam.Text == "") &&
               (txtMaNhanVien.Text == "") && (txtMaKhachHang.Text == "") &&
               (txtTongTien.Text == "")) {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM tblHoadon WHERE 1=1";
            if (txtMaHDBan.Text != "")
                sql = sql + " AND MaHoaDon Like N'%" + txtMaHDBan.Text + "%'";
            if (txtThang.Text != "")
                sql = sql + " AND MONTH(NgayBan) =" + txtThang.Text;
            if (txtNam.Text != "")
                sql = sql + " AND YEAR(NgayBan) =" + txtNam.Text;
            if (txtMaNhanVien.Text != "")
                sql = sql + " AND MaNhanVien Like N'%" + txtMaNhanVien.Text + "%'";
            if (txtMaKhachHang.Text != "")
                sql = sql + " AND MaKhach Like N'%" + txtMaKhachHang.Text + "%'";
            if (txtTongTien.Text != "")
                sql = sql + " AND TongTien <=" + txtTongTien.Text;
            thd = hb.GetDataToTable(sql);
            if (thd.Rows.Count == 0) {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
                MessageBox.Show("Có " + thd.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK);
            dgvTimKiemHoaDon.DataSource = thd;
            LoadData();
        }

        private void btnTimLai_Click(object sender, EventArgs e) {
            ResetValues();
            dgvTimKiemHoaDon.DataSource = null;
        }

        private void dgvTimKiemHoaDon_DoubleClick(object sender, EventArgs e) {
            string mahd;
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                mahd = dgvTimKiemHoaDon.CurrentRow.Cells["MaHoaDon"].Value.ToString();
                frm_HoaDon frm = new frm_HoaDon();
                frm.txtMaHDBan.Text = mahd;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void btnDong_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
