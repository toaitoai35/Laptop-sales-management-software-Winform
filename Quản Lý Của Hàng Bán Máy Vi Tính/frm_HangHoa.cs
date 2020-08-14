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
using System.Text.RegularExpressions;

namespace Quản_Lý_Của_Hàng_Bán_Máy_Vi_Tính
{
    public partial class frm_HangHoa : Form
    {
        DataTable dth;
        HangHoa hh = new HangHoa();
        HangHoaDB hb = new HangHoaDB();
        public frm_HangHoa() {
            InitializeComponent();
            LoadData();
        }

        public void LoadData() {
            dth = hb.getListHang();
            dgvHang.DataSource = dth;

            dgvHang.Columns[0].HeaderText = "Mã hàng";
            dgvHang.Columns[1].HeaderText = "Tên hàng";
            dgvHang.Columns[2].HeaderText = "Nhãn hiệu";
            dgvHang.Columns[3].HeaderText = "Số lượng";
            dgvHang.Columns[4].HeaderText = "Đơn giá nhập";
            dgvHang.Columns[5].HeaderText = "Đơn giá bán";
            dgvHang.Columns[6].HeaderText = "Ảnh";
            dgvHang.Columns[7].HeaderText = "Ghi chú";

            dgvHang.Columns[7].Width = 300;
            dgvHang.AllowUserToAddRows = false;
            dgvHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void frm_HangHoa_Load(object sender, EventArgs e) {
            LoadData();
            txtMaHang.Enabled = false;
            txtDonGiaNhap.Enabled = false;
            txtDonGiaBan.Enabled = false;
            btnLuu.Enabled = false;
            hb.FillCombo(cboMaNhanHieu, "MaNhanHieu", "TenNhanHieu");
            cboMaNhanHieu.SelectedIndex = -1;


        }
        private void ResetValues() {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cboMaNhanHieu.Text = "";
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
            txtAnh.Text = "";
            picAnh.Image = null;
            txtGhiChu.Text = "";
        }
        private void dgvHang_Click(object sender, EventArgs e) {
            string MaNhanHieu;
            string sql = "";
            if (btnThem.Enabled == false) {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (dth.Rows.Count == 0) {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaHang.Text = dgvHang.CurrentRow.Cells["MaHang"].Value.ToString();
            txtTenHang.Text = dgvHang.CurrentRow.Cells["TenHang"].Value.ToString();
            MaNhanHieu = dgvHang.CurrentRow.Cells["MaNhanHieu"].Value.ToString();
            cboMaNhanHieu.Text = hb.GetData(MaNhanHieu, "GetTenNhanHieu");

            txtSoLuong.Text = dgvHang.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtDonGiaNhap.Text = dgvHang.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
            txtDonGiaBan.Text = dgvHang.CurrentRow.Cells["DonGiaBan"].Value.ToString();

            txtAnh.Text = hb.GetData(txtMaHang.Text.Trim(), "GetAnhHang");
            picAnh.Image = Image.FromFile(txtAnh.Text);

            txtGhiChu.Text = hb.GetData(txtMaHang.Text.Trim(), "GetGhiChu");
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnDong.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e) {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnDong.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaHang.Enabled = true;
            txtTenHang.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e) {
            string MaHang = txtMaHang.Text;
            if (dth.Rows.Count == 0) {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHang.Text == "") {
                MessageBox.Show("Hãy chọn một dòng để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                try {
                    if (hb.DeleteHang(MaHang)) {
                        MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                } catch {
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    hb.CheckDelete();
                }

                LoadData();
                ResetValues();
            }
        }

        private void btnSua_Click(object sender, EventArgs e) {
            hh.MaHang = txtMaHang.Text;
            hh.TenHang = txtTenHang.Text;
            hh.MaNhanHieu = cboMaNhanHieu.Text;
            hh.SoLuong = float.Parse(txtSoLuong.Text);
            hh.DonGiaNhap = float.Parse(txtDonGiaNhap.Text);
            hh.DonGiaBan = float.Parse(txtDonGiaBan.Text);
            hh.Anh = txtAnh.Text;
            hh.GhiChu = txtGhiChu.Text;

            if (dth.Rows.Count == 0) {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHang.Text == "") {
                MessageBox.Show("Bạn chưa chọn dòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (txtMaHang.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (txtTenHang.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return;
            }
            if (cboMaNhanHieu.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải chọn Nhãn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaNhanHieu.Focus();
                return;
            }
            if (txtSoLuong.Text == "0") {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Focus();
                return;
            }

            if (txtDonGiaNhap.Text == "0") {
                MessageBox.Show("Bạn phải nhập đơn giá nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGiaNhap.Focus();
                return;
            }
            if (txtDonGiaBan.Text == "0") {
                MessageBox.Show("Bạn phải nhập đơn giá bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGiaBan.Focus();
                return;
            }
            if (txtAnh.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải chọn ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnMo.Focus();
                return;
            }
            try {
                if (hb.UpdateHang(hh)) {
                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
                }

            } catch {
                MessageBox.Show("Sửa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadData();
            ResetValues();

            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnDong.Enabled = true;
            btnLuu.Enabled = false;
            txtMaHang.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e) {
            hh.MaHang = txtMaHang.Text;
            hh.TenHang = txtTenHang.Text;
            hh.MaNhanHieu = cboMaNhanHieu.Text;
            hh.SoLuong = float.Parse(txtSoLuong.Text);
            hh.DonGiaNhap = float.Parse(txtDonGiaNhap.Text);
            hh.DonGiaBan = float.Parse(txtDonGiaBan.Text);
            hh.Anh = txtAnh.Text;
            hh.GhiChu = txtGhiChu.Text;

            if (txtMaHang.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (txtTenHang.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return;
            }
            if (cboMaNhanHieu.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải chọn Nhãn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaNhanHieu.Focus();
                return;
            }
            if (txtSoLuong.Text == "0") {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Focus();
                return;
            }

            if (txtDonGiaNhap.Text == "0") {
                MessageBox.Show("Bạn phải nhập đơn giá nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGiaNhap.Focus();
                return;
            }
            if (txtDonGiaBan.Text == "0") {
                MessageBox.Show("Bạn phải nhập đơn giá bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGiaBan.Focus();
                return;
            }

            if (txtAnh.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải chọn ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnMo.Focus();
                return;
            }
            if (hb.checkDuplicate(txtMaHang.Text.Trim())) {
                MessageBox.Show("Mã này đã tồn tại !!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            try {
                if (hb.InsertHang(hh)) {
                    MessageBox.Show("Thêm mới thành công", "Thông báo", MessageBoxButtons.OK);
                }

            } catch {
                MessageBox.Show("Thêm mới thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadData();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnDong.Enabled = true;
            btnLuu.Enabled = false;

            txtMaHang.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnMo_Click(object sender, EventArgs e) {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK) {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e) {
            try {
                dgvHang.DataSource = hb.FindHang(txtTimKiem.Text);
            } catch {
                MessageBox.Show("Không tìm thấy!!");
            }
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e) {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void txtDonGiaNhap_KeyPress(object sender, KeyPressEventArgs e) {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void txtDonGiaBan_KeyPress(object sender, KeyPressEventArgs e) {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }
    }
}
