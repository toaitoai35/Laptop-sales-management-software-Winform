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
    public partial class frm_KhachHang : Form
    {
        DataTable dtKH;
        KhachHang kh = new KhachHang();
        KhachHangDB kb = new KhachHangDB();
        public frm_KhachHang() {
            InitializeComponent();
            LoadData();
        }

        public void LoadData() {
            dtKH = kb.getListKhachHang();
            gdvKhachHang.DataSource = dtKH;

            gdvKhachHang.Columns[0].HeaderText = "Mã khách hàng";
            gdvKhachHang.Columns[1].HeaderText = "Tên khách hàng";
            gdvKhachHang.Columns[2].HeaderText = "  Địa chỉ";
            gdvKhachHang.Columns[3].HeaderText = "Điện thoại";
            gdvKhachHang.Columns[4].HeaderText = "Giới tính";
            gdvKhachHang.Columns[5].HeaderText = "Ngày sinh";

            gdvKhachHang.Columns[2].Width = 260;
            gdvKhachHang.AllowUserToAddRows = false;
            gdvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void frm_KhachHang_Load(object sender, EventArgs e) {
            txtMaKhachHang.Enabled = false;
            LoadData();
        }

        private void gdvKhachHang_Click(object sender, EventArgs e) {
            if (btnThem.Enabled == false) {
                MessageBox.Show("Đang thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhachHang.Focus();
                return;
            }
            if (dtKH.Rows.Count == 0) {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaKhachHang.Text = gdvKhachHang.CurrentRow.Cells["MaKhach"].Value.ToString();
            txtTenKhachHang.Text = gdvKhachHang.CurrentRow.Cells["TenKhach"].Value.ToString();
            txtDiaChi.Text = gdvKhachHang.CurrentRow.Cells["DiaChi"].Value.ToString();
            mskDienThoai.Text = gdvKhachHang.CurrentRow.Cells["DienThoai"].Value.ToString();
            if (gdvKhachHang.CurrentRow.Cells["GioiTinh"].Value.ToString() == "Nam") chkGioiTinh.Checked = true;
            else chkGioiTinh.Checked = false;
            dtpNgaySinh.Value = (DateTime)gdvKhachHang.CurrentRow.Cells["NgaySinh"].Value;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

        }

        private void btnThem_Click(object sender, EventArgs e) {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnDong.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue();
            txtMaKhachHang.Enabled = true;
            txtTenKhachHang.Focus();
        }
        private void ResetValue() {
            txtMaKhachHang.Text = "";
            txtTenKhachHang.Text = "";
            txtDiaChi.Text = "";
            mskDienThoai.Text = "";
            chkGioiTinh.Checked = false;
            dtpNgaySinh.Value = DateTime.Now;
        }
        private void btnXoa_Click(object sender, EventArgs e) {
            string MaKhach = txtMaKhachHang.Text;
            if (dtKH.Rows.Count == 0) {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKhachHang.Text == "") {
                MessageBox.Show("Hãy chọn một dòng để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                try {
                    if (kb.DeleteKhachHang(MaKhach)) {
                        MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                } catch {
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    kb.CheckDelete();
                }

                LoadData();
                ResetValue();
            }
        }

        private void btnSua_Click(object sender, EventArgs e) {
            string gt = string.Empty;
            kh.MaKhach = txtMaKhachHang.Text;
            kh.TenKhach = txtTenKhachHang.Text;
            kh.DiaChi = txtDiaChi.Text;
            kh.DienThoai = mskDienThoai.Text;
            kh.GioiTinh = chkGioiTinh.Text;
            kh.NgaySinh = DateTime.Parse(dtpNgaySinh.Text);

            if (dtKH.Rows.Count == 0) {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKhachHang.Text == "") {
                MessageBox.Show("Chưa chọn dòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenKhachHang.Text.Trim().Length == 0) {
                MessageBox.Show("chưa nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (mskDienThoai.Text == "(   )    -") {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskDienThoai.Focus();
                return;
            }
            if (chkGioiTinh.Checked)
                gt = "Nam";
            else
                gt = "Nữ";
            try {
                kh.GioiTinh = gt;

                if (kb.UpdateKhachHang(kh)) {
                    MessageBox.Show("Sửa thành công ", "Thông báo", MessageBoxButtons.OK);
                }
            } catch {
                MessageBox.Show("Sửa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadData();
            ResetValue();
        }

        private void btnLuu_Click(object sender, EventArgs e) {
            kh.MaKhach = txtMaKhachHang.Text;
            kh.TenKhach = txtTenKhachHang.Text;
            kh.DiaChi = txtDiaChi.Text;
            kh.DienThoai = mskDienThoai.Text;
            kh.GioiTinh = chkGioiTinh.Text;
            kh.NgaySinh = DateTime.Parse(dtpNgaySinh.Text);

            string gt;
            if (txtMaKhachHang.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhachHang.Focus();
                return;
            }
            if (txtTenKhachHang.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKhachHang.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (mskDienThoai.Text == "(   )    -") {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskDienThoai.Focus();
                return;
            }

            if (chkGioiTinh.Checked)
                gt = "Nam";
            else
                gt = "Nữ";

            if (kb.checkDuplicate(txtMaKhachHang.Text.Trim())) {
                MessageBox.Show("Mã này đã tồn tại !!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhachHang.Focus();
                return;
            }
            try {
                kh.GioiTinh = gt;

                if (kb.InsertKhachHang(kh)) {
                    MessageBox.Show("Thêm mới thành công", "Thông báo", MessageBoxButtons.OK);
                }

            } catch {
                MessageBox.Show("Thêm mới thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadData();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnDong.Enabled = true;
            btnLuu.Enabled = false;
            txtMaKhachHang.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e) {
            this.Close();
        }



        private void txtTimKiem_TextChanged(object sender, EventArgs e) {

        }

        private void btnTimKiem_Click(object sender, EventArgs e) {
            try {
                gdvKhachHang.DataSource = kb.FindKhachHang(txtTimKiem.Text);
            } catch {
                MessageBox.Show("Ko tim thay"); ;
            }
        }
    }
}
