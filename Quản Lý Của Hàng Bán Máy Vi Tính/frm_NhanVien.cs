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
    public partial class frm_NhanVien : Form
    {
        DataTable dtNV;
        NhanVien nv = new NhanVien();
        NhanVienDB nb = new NhanVienDB();
        public frm_NhanVien() {

            InitializeComponent();
            LoadData();
        }

        private void frm_NhanVien_Load(object sender, EventArgs e) {
            LoadData();
            txtMaNhanVien.Enabled = false;
        }
        public void LoadData() {


            dtNV = nb.getListNhanVien();
            dgvNhanVien.DataSource = dtNV;

            dgvNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns[1].HeaderText = "Tên nhân viên";
            dgvNhanVien.Columns[2].HeaderText = "  Địa chỉ";
            dgvNhanVien.Columns[3].HeaderText = "Điện thoại";
            dgvNhanVien.Columns[4].HeaderText = "Giới tính";
            dgvNhanVien.Columns[5].HeaderText = "Ngày sinh";

            dgvNhanVien.Columns[2].Width = 260;
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvNhanVien_Click(object sender, EventArgs e) {

            if (btnThem.Enabled == false) {
                MessageBox.Show("Đang thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhanVien.Focus();
                return;
            }
            if (dtNV.Rows.Count == 0) {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaNhanVien.Text = dgvNhanVien.CurrentRow.Cells["MaNhanVien"].Value.ToString();
            txtTenNhanVien.Text = dgvNhanVien.CurrentRow.Cells["TenNhanVien"].Value.ToString();
            txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtDienThoai.Text = dgvNhanVien.CurrentRow.Cells["DienThoai"].Value.ToString();
            if (dgvNhanVien.CurrentRow.Cells["GioiTinh"].Value.ToString() == "Nam") chkGioiTinh.Checked = true;
            else chkGioiTinh.Checked = false;
            dtpNgaySinh.Value = (DateTime)dgvNhanVien.CurrentRow.Cells["NgaySinh"].Value;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void ResetValue() {
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            chkGioiTinh.Checked = false;
            dtpNgaySinh.Value = DateTime.Now;
        }
        private void btnThem_Click(object sender, EventArgs e) {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnDong.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue();
            txtMaNhanVien.Enabled = true;
            txtTenNhanVien.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e) {
            string MaNhanVien = txtMaNhanVien.Text;
            if (dtNV.Rows.Count == 0) {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhanVien.Text == "") {
                MessageBox.Show("Hãy chọn một dòng để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                try {
                    if (nb.DeleteNhanVien(MaNhanVien)) {
                        MessageBox.Show("Xóa Thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                } catch {
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    nb.CheckDelete();
                }

                LoadData();
                ResetValue();
            }
        }

        private void btnSua_Click(object sender, EventArgs e) {
            string gt = string.Empty;
            nv.MaNhanVien = txtMaNhanVien.Text;
            nv.TenNhanVien = txtTenNhanVien.Text;
            nv.DiaChi = txtDiaChi.Text;
            nv.DienThoai = txtDienThoai.Text;
            nv.GioiTinh = chkGioiTinh.Text;
            nv.NgaySinh = DateTime.Parse(dtpNgaySinh.Text);

            if (dtNV.Rows.Count == 0) {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhanVien.Text == "") {
                MessageBox.Show("Chưa chọn dòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNhanVien.Text.Trim().Length == 0) {
                MessageBox.Show("chưa nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (txtDienThoai.Text == "(   )    -") {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return;
            }
            if (chkGioiTinh.Checked)
                gt = "Nam";
            else
                gt = "Nữ";
            try {
                nv.GioiTinh = gt;

                if (nb.UpdateNhanVien(nv)) {
                    MessageBox.Show("Sửa Thành công ", "Thông báo", MessageBoxButtons.OK);
                }
            } catch {
                MessageBox.Show("Sửa Thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadData();
            ResetValue();
        }

        private void btnLuu_Click(object sender, EventArgs e) {
            nv.MaNhanVien = txtMaNhanVien.Text;
            nv.TenNhanVien = txtTenNhanVien.Text;
            nv.DiaChi = txtDiaChi.Text;
            nv.DienThoai = txtDienThoai.Text;
            nv.GioiTinh = chkGioiTinh.Text;
            nv.NgaySinh = DateTime.Parse(dtpNgaySinh.Text);


            string gt;
            if (txtMaNhanVien.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhanVien.Focus();
                return;
            }
            if (txtTenNhanVien.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNhanVien.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (txtDienThoai.Text == "(   )    -") {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return;
            }

            if (chkGioiTinh.Checked)
                gt = "Nam";
            else
                gt = "Nữ";

            if (nb.checkDuplicate(txtMaNhanVien.Text.Trim())) {
                MessageBox.Show("Mã này đã tồn tại !!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhanVien.Focus();
                return;
            }
            try {
                nv.GioiTinh = gt;

                if (nb.InsertNhanVien(nv)) {
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
            txtMaNhanVien.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e) {
            try {
                dgvNhanVien.DataSource = nb.FindNhanVien(txtTimKiem.Text);
            } catch {
                MessageBox.Show("Không tìm thấy");
            }
        }
    }
}
