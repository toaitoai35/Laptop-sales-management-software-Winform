using Quản_Lý_Của_Hàng_Bán_Máy_Vi_Tính.Class;
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
using System.Globalization;
using COMExcel = Microsoft.Office.Interop.Excel;
namespace Quản_Lý_Của_Hàng_Bán_Máy_Vi_Tính
{
    public partial class frm_HoaDon : Form
    {
        DataTable tblCTHDB;
        HoaDon hd = new HoaDon();

        HoaDonDB DB = new HoaDonDB();
        HDChiTietDB ctb = new HDChiTietDB();
        public frm_HoaDon() {
            InitializeComponent();
            LoadDataGridView();
        }
        private void LoadDataGridView() {
            tblCTHDB = DB.GetData(txtMaHDBan.Text);

            dgvHDBanHang.DataSource = tblCTHDB;
            dgvHDBanHang.Columns[0].HeaderText = "Mã hàng";
            dgvHDBanHang.Columns[1].HeaderText = "Tên hàng";
            dgvHDBanHang.Columns[2].HeaderText = "Số lượng";
            dgvHDBanHang.Columns[3].HeaderText = "Đơn giá";
            dgvHDBanHang.Columns[4].HeaderText = "Giảm giá %";
            dgvHDBanHang.Columns[5].HeaderText = "Thành tiền";
            dgvHDBanHang.Columns[0].Width = 80;
            dgvHDBanHang.Columns[1].Width = 200;
            dgvHDBanHang.Columns[2].Width = 150;
            dgvHDBanHang.Columns[3].Width = 150;
            dgvHDBanHang.Columns[4].Width = 100;
            dgvHDBanHang.Columns[5].Width = 150;
            dgvHDBanHang.AllowUserToAddRows = false;
            dgvHDBanHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void LoadInfoHoaDon() {
            dtpNgayBan.Value = DateTime.Parse(DB.GetDataFromHoaDon(txtMaHDBan.Text, "GetNgayBanHD").ToString());
            cboMaNhanVien.Text = DB.GetDataFromHoaDon(txtMaHDBan.Text, "GetMaNhanVienHD") + String.Empty;
            cboMaKhachHang.Text = DB.GetDataFromHoaDon(txtMaHDBan.Text, "GetMaKhachHD") + String.Empty;
            txtTongTien.Text = DB.GetDataFromHoaDon(txtMaHDBan.Text, "GetTongTienHD") + String.Empty;
        }

        private void LoadSettings() {
            btnThemHoaDon.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            txtMaHDBan.ReadOnly = true;
            txtTenNhanVien.ReadOnly = true;
            txtTenKhachHang.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtDienThoai.ReadOnly = true;
            txtTenHang.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtGiamGia.Text = "0";
            txtTongTien.Text = "0";
            FillComboBox(cboMaKhachHang, "MaKhach", "MaKhach", "GetMaKhachToCbxHD");
            FillComboBox(cboMaNhanVien, "MaNhanVien", "TenKhach", "GetMaNhanVienToCbxHD");
            FillComboBox(cboMaHang, "MaHang", "MaHang", "GetMaHangToCbxHD");
        }

        private void FillComboBox(ComboBox cbx, string ma, string ten, string sql) {
            cbx.DataSource = DB.GetDataToComboBox(sql);
            cbx.ValueMember = ma;
            cbx.DisplayMember = ten;
            cbx.SelectedIndex = -1;
        }
        private void frm_HoaDon_Load(object sender, EventArgs e) {
            LoadSettings();

            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (txtMaHDBan.Text != string.Empty) {
                LoadInfoHoaDon();
                btnXoa.Enabled = true;

            }
            LoadDataGridView();
        }
        private void ResetValues() {
            txtMaHDBan.Text = string.Empty;
            dtpNgayBan.Value = DateTime.Now;
            cboMaNhanVien.Text = string.Empty;
            cboMaKhachHang.Text = string.Empty;
            txtTongTien.Text = "0";

            cboMaHang.Text = string.Empty;
            txtSoLuong.Text = "0";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }

        private void ResetValuesHang() {
            cboMaHang.Text = string.Empty;
            txtSoLuong.Text = string.Empty;
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }
        private void btnThem_Click(object sender, EventArgs e) {

            txtSoLuong.Text = "0";
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            btnThemHoaDon.Enabled = false;
            ResetValues();
            txtMaHDBan.Text = DB.CreateKey("HDB");
            LoadDataGridView();
        }
        private bool CheckMaHoaDon() {
            return DB.GetDataFromHoaDon(txtMaHDBan.Text, "GetMaHoaDonHD") != string.Empty;
        }

        private void btnLuu_Click(object sender, EventArgs e) {

            float SLConLai, sl;
            double Tong, TongMoi;
            if (!CheckMaHoaDon()) {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa

                if (cboMaNhanVien.Text.Length == 0) {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNhanVien.Focus();
                    return;
                }
                if (cboMaKhachHang.Text.Length == 0) {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaKhachHang.Focus();
                    return;
                }
                HoaDon newHD = new HoaDon {
                    MaHoaDon = txtMaHDBan.Text.Trim(),
                    MaNhanVien = cboMaNhanVien.Text.Trim(),
                    NgayBan = dtpNgayBan.Value,
                    MaKhach = cboMaKhachHang.Text.Trim(),
                    TongTien = float.Parse(txtTongTien.Text.Trim())
                };
                DB.InsertHoaDon(newHD);
            }
            // Lưu thông tin của các mặt hàng
            if (cboMaHang.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHang.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0")) {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = string.Empty;
                txtSoLuong.Focus();
                return;
            }
            if (txtGiamGia.Text.Trim().Length == 0) {
                MessageBox.Show("Bạn phải nhập giảm giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiamGia.Focus();
                return;
            }
            if (CheckMaHangHD()) {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesHang();
                cboMaHang.Focus();
                return;
            }
            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
            sl = float.Parse(DB.GetSoLuongBaseOnMaHangHD(cboMaHang.Text.Trim()));
            if (float.Parse(txtSoLuong.Text) > sl) {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = string.Empty;
                txtSoLuong.Focus();
                return;
            }
            HDChiTiet hdct = new HDChiTiet {
                MaHoaDon = txtMaHDBan.Text.Trim(),
                MaHang = cboMaHang.Text.Trim(),
                SoLuong = float.Parse(txtSoLuong.Text.Trim()),
                DonGiaBan = float.Parse(txtDonGia.Text.Trim()),
                GiamGia = float.Parse(txtGiamGia.Text.Trim()),
                ThanhTien = float.Parse(txtThanhTien.Text.Trim())
            };
            DB.InsertHDCT(hdct);
            LoadDataGridView();

            // Cập nhật lại số lượng của mặt hàng vào bảng tblHang
            SLConLai = sl - float.Parse(txtSoLuong.Text);
            DB.UpdateSoLuong(SLConLai, cboMaHang.Text);

            // Cập nhật lại tổng tiền cho hóa đơn bán//
            Tong = float.Parse(DB.GetDataFromHoaDon(txtMaHDBan.Text, "GetTongTienHD")+ string.Empty);
            TongMoi = Tong + float.Parse(txtThanhTien.Text);
            DB.UpdateTongTien(TongMoi, txtMaHDBan.Text);
            txtTongTien.Text = TongMoi.ToString();

            ResetValuesHang();
            btnXoa.Enabled = true;
            btnThemHoaDon.Enabled = true;
        }

        private bool CheckMaHangHD() {
            return DB.GetMaHangHDCT(cboMaHang.Text, txtMaHDBan.Text.Trim()) != string.Empty;
        }

        private void cboMaNhanVien_TextChanged(object sender, EventArgs e) {
            if (cboMaNhanVien.Text == string.Empty) txtTenNhanVien.Text = string.Empty;
            txtTenNhanVien.Text = DB.GetDataFromNhanVien(cboMaNhanVien.Text.Trim(), "GetTenNhanVien").ToString();
        }

        private void cboMaKhachHang_SelectedIndexChanged(object sender, EventArgs e) {
            if (cboMaKhachHang.Text == string.Empty) {
                txtTenKhachHang.Text = string.Empty;
                txtDiaChi.Text = string.Empty;
                txtDienThoai.Text = string.Empty;
            }
            txtTenKhachHang.Text = DB.GetDataFromKhachHang(cboMaKhachHang.Text, "GetTenKhach");
            txtDiaChi.Text = DB.GetDataFromKhachHang(cboMaKhachHang.Text, "GetDiaChi");
            txtDienThoai.Text = DB.GetDataFromKhachHang(cboMaKhachHang.Text, "GetDienThoai");
        }

        private void cboMaHang_TextChanged(object sender, EventArgs e) {
            if (cboMaHang.Text.Trim() == string.Empty) {
                txtTenHang.Text = string.Empty;
                txtDonGia.Text = string.Empty;
            }else {
                txtTenHang.Text = DB.GetDataFromHang(cboMaHang.Text, "GetTenHang");
                txtDonGia.Text = DB.GetDataFromHang(cboMaHang.Text, "GetDonGiaBan") + String.Empty;
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e) {
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == string.Empty)
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == string.Empty)
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == string.Empty)
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e) {
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == string.Empty)
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == string.Empty)
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == string.Empty)
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e) {

            if (cboMaHDBan.Text == string.Empty) {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHDBan.Focus();
                return;
            }
            txtMaHDBan.Text = cboMaHDBan.Text;
            LoadInfoHoaDon();
            LoadDataGridView();
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            cboMaHDBan.SelectedIndex = -1;
        }

        private void cboMaHDBan_DropDown(object sender, EventArgs e) {
            FillComboBox(cboMaHDBan, "MaHoaDon", "MaHoaDon", "GetMaHoaDonToCbxHD");
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e) {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void txtGiamGia_KeyPress(object sender, KeyPressEventArgs e) {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e) {
            string MaHoaDon = txtMaHDBan.Text;
            float sl, slcon, slxoa;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                DataTable tblHang = DB.GetDataTableFromHDChiTiet(txtMaHDBan.Text.Trim(), "GetMaHangAndSoLuong");
                for (int hang = 0; hang <= tblHang.Rows.Count - 1; hang++) {
                    // Cập nhật lại số lượng cho các mặt hàng
                    sl = float.Parse(DB.GetDataFromHoaDon(tblHang.Rows[hang][0].ToString(), "GetSoLuong") + String.Empty);
                    slxoa = float.Parse(tblHang.Rows[hang][1].ToString());
                    slcon = sl + slxoa;
                    DB.UpdateSoLuong(slcon, "UpdateSoLuong");
                }

                //Xóa chi tiết hóa đơn
                ctb.DeleteHDChiTiet(MaHoaDon);

                //Xóa hóa đơn
                DB.DeleteHoaDon(MaHoaDon);
                ResetValues();
                LoadDataGridView();
                btnXoa.Enabled = false;

            }
        }

        private void btnDong_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void dgvHoaDon_DoubleClick(object sender, EventArgs e) {
            string MaHoaDon = txtMaHDBan.Text;
            string MaHangXoa;
            float ThanhTienXoa, SLXoa, SL, SLCon;
            double Tong, TongMoi;
            if (tblCTHDB.Rows.Count == 0) {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)) {
                //Xóa hàng và cập nhật lại số lượng hàng 
                MaHangXoa = dgvHDBanHang.CurrentRow.Cells["MaHang"].Value.ToString().Trim();
                SLXoa = Convert.ToInt32(dgvHDBanHang.CurrentRow.Cells["SoLuong"].Value.ToString().Trim());
                ThanhTienXoa = float.Parse(dgvHDBanHang.CurrentRow.Cells["ThanhTien"].Value.ToString().Trim());
                DB.DeleteHoaDonChiTiet(txtMaHDBan.Text.Trim(), MaHangXoa);

                // Cập nhật lại số lượng cho các mặt hàng
                SL = float.Parse(DB.GetDataFromHang(MaHangXoa, "GetSLHang") + string.Empty);
                SLCon = SL + SLXoa; // cộng số lượng hàng ban đầu vào kho
                DB.UpdateSoLuong(SLCon, MaHangXoa);

                Tong = float.Parse(DB.GetDataFromHoaDon(txtMaHDBan.Text, "GetTongTienHD") + string.Empty);
                TongMoi = Tong - ThanhTienXoa;
                DB.UpdateTongTien(TongMoi, txtMaHDBan.Text);
                txtTongTien.Text = TongMoi.ToString();

                if (tblCTHDB.Rows.Count - 1 == 0)
                    if ((MessageBox.Show("Hoá đơn trống, bạn có muốn xóa hóa đơn này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                        DB.DeleteHoaDon(MaHoaDon);
            }
            LoadDataGridView();
        }

        private void btnThemMatHang_Click(object sender, EventArgs e) {
            cboMaHang.Text = string.Empty;
            txtTenHang.Text = string.Empty;
            txtDonGia.Text = string.Empty;
            txtGiamGia.Text = "0";
            txtSoLuong.Text = "0";
            txtThanhTien.Text = string.Empty;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            btnThemHoaDon.Enabled = false;

            LoadDataGridView();
        }

        private void btnInHoaDon_Click(object sender, EventArgs e) {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "eShop";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Bình Thạnh- TP HCM";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (04)38526419";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";
            // Biểu diễn thông tin chung của hóa đơn bán
            tblThongtinHD = DB.GetThongTinHD(txtMaHDBan.Text.Trim());
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tblThongtinHD.Rows[0][5].ToString();
            //Lấy thông tin các mặt hàng
            tblThongtinHang = DB.GetThongTinHang(txtMaHDBan.Text.Trim());
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên hàng";

            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Giảm giá";
            exRange.Range["F11:F11"].Value = "Thành tiền";
            for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++) {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 12
                {
                    exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString() + "%";
                }
            }
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;

            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Hồ Chí Minh, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][6];
            exSheet.Name = "Hóa đơn nhập";
            exApp.Visible = true;
        }
    }
}

