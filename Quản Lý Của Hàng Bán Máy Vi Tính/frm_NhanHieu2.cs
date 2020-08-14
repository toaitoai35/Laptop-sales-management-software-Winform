using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Quản_Lý_Của_Hàng_Bán_Máy_Vi_Tính.Class;
namespace Quản_Lý_Của_Hàng_Bán_Máy_Vi_Tính
{
    public partial class frm_NhanHieu : Form
    {

        DataTable dtNH;
        public frm_NhanHieu()
        {
            InitializeComponent();
            LoadData();
            
        }

        public void LoadData()
        {
            string sql;
            sql = "SELECT MaNhanHieu, TenNhanHieu FROM tblNhanHieu";
            dtNH = ConnectionDBAndFuntion.GetDataToTable(sql);
            gdvNhanHIeu.DataSource = dtNH;
            gdvNhanHIeu.Columns[0].HeaderText = "Mã nhãn hiệu";
            gdvNhanHIeu.Columns[1].HeaderText = "Tên nhãn hiệu";
            gdvNhanHIeu.Columns[0].Width = 200;
            gdvNhanHIeu.Columns[1].Width = 250;
            gdvNhanHIeu.AllowUserToAddRows = false;
            gdvNhanHIeu.EditMode = DataGridViewEditMode.EditProgrammatically;
        }


        private void gdvNhanHIeu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đangthêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhanHieu.Focus();
                return;
            }
            if (dtNH.Rows.Count == 0) 
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaNhanHieu.Text = gdvNhanHIeu.CurrentRow.Cells["MaNhanHieu"].Value.ToString();
            txtTenNhanHieu.Text = gdvNhanHIeu.CurrentRow.Cells["TenNhanHieu"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnDong.Enabled = true;
        }

        private void frm_NhanHieu_Load(object sender, EventArgs e)
        {
            txtMaNhanHieu.Enabled = false;
            LoadData();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ResetValue() // xóa text
        {
            txtMaNhanHieu.Text = "";
            txtTenNhanHieu.Text = "";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnDong.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue();
            txtMaNhanHieu.Enabled = true;
            txtTenNhanHieu.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            //kiểm tra dữ liệu
            if (dtNH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhanHieu.Text == "") 
            {
                MessageBox.Show("Hãy chọn một dòng để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblNhanHieu WHERE MaNhanHieu=N'" + txtMaNhanHieu.Text + "'";
                ConnectionDBAndFuntion.XoaSQL(sql);
                LoadData();
                ResetValue();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaNhanHieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Hãy nhập Mã!!!!!");
                txtMaNhanHieu.Focus();
                return;
            }
            if (txtTenNhanHieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Hãy nhập Tên!!!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNhanHieu.Focus();
                return;
            }
            sql = "Select MaNhanHieu From tblNhanHieu where MaNhanHieu=N'" + txtMaNhanHieu.Text.Trim() + "'";
            if (ConnectionDBAndFuntion.Duplicate(sql))
            {
                MessageBox.Show("Mã này đã tồn tại !!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhanHieu.Focus();
                return;
            }

            sql = "INSERT INTO tblNhanHieu VALUES(N'" +
                txtMaNhanHieu.Text + "',N'" + txtTenNhanHieu.Text + "')";
            ConnectionDBAndFuntion.RunSQL(sql);
            LoadData(); 
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnDong.Enabled = true;
            btnLuu.Enabled = false;
            txtMaNhanHieu.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; 
            if (dtNH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhanHieu.Text == "")
            {
                MessageBox.Show("Chưa chọn dòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNhanHieu.Text.Trim().Length == 0) 
            {
                MessageBox.Show("chưa nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE tblNhanHieu SET TenNhanHieu=N'" +
                txtTenNhanHieu.Text.ToString() +
                "' WHERE MaNhanHieu=N'" + txtMaNhanHieu.Text + "'";
            ConnectionDBAndFuntion.RunSQL(sql);
            LoadData();
            ResetValue();
           
            
        }
    }
}
