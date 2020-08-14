using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Quản_Lý_Của_Hàng_Bán_Máy_Vi_Tính.Class
{
    class HangHoaDB
    {
        string strConnection = @"server=.;database=QLBH_ViTinhDB;uid=sa;pwd=123456";
        SqlConnection cnn = null;

        public DataTable getListHang() {
            DataTable dt = new DataTable();
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("GetListHang", cnn);

                cmd.CommandType = CommandType.StoredProcedure;
                cnn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return dt;

        }
        public void FillCombo(ComboBox cbo, string ma, string ten) {
            string sql = "SELECT * from tblNhanHieu";
            SqlDataAdapter dap = new SqlDataAdapter(sql, cnn);
            DataTable table = new DataTable();
            dap.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma;
            cbo.DisplayMember = ten;
        }

        public string GetFieldValues(string sql) {
            string ma = "";
            try {


                SqlCommand cmd = new SqlCommand(sql, cnn);
                cnn.Open();
                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                    ma = reader.GetValue(0).ToString();
                reader.Close();

            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return ma;
        }

        public bool InsertHang(HangHoa h) {
            bool result;
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("InsertHang", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHang", h.MaHang);
                cmd.Parameters.AddWithValue("@TenHang", h.TenHang);
                cmd.Parameters.AddWithValue("@MaNhanHieu", h.MaNhanHieu);
                cmd.Parameters.AddWithValue("@SoLuong", h.SoLuong);
                cmd.Parameters.AddWithValue("@DonGiaNhap", h.DonGiaNhap);
                cmd.Parameters.AddWithValue("@DonGiaBan", h.DonGiaBan);
                cmd.Parameters.AddWithValue("@Anh", h.Anh);
                cmd.Parameters.AddWithValue("@GhiChu", h.GhiChu);

                cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }
        public bool checkDuplicate(string code) {
            bool result = false;
            string query = "Select MaHang From tblHang where MaHang=@Code";
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.Parameters.AddWithValue("@Code", code);
            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                var da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);
                if (table.Rows.Count > 0)
                    result = true;
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                if (cnn.State == ConnectionState.Open) cnn.Close();

            }

            return result;
        }
        public bool UpdateHang(HangHoa h) {
            bool result;
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("UpdateHang", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHang", h.MaHang);
                cmd.Parameters.AddWithValue("@TenHang", h.TenHang);
                cmd.Parameters.AddWithValue("@MaNhanHieu", h.MaNhanHieu);
                cmd.Parameters.AddWithValue("@SoLuong", h.SoLuong);
                cmd.Parameters.AddWithValue("@DonGiaNhap", h.DonGiaNhap);
                cmd.Parameters.AddWithValue("@DonGiaBan", h.DonGiaBan);
                cmd.Parameters.AddWithValue("@Anh", h.Anh);
                cmd.Parameters.AddWithValue("@GhiChu", h.GhiChu);

                cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException ex){
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }
        public bool DeleteHang(string MaHang) {
            bool result;
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("DeleteHang", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHang", MaHang);
                cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }
        public bool CheckDelete() {

            string sql = "DELETE tblHang WHERE MaHang=N'";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = ConnectionDB.Con;
            cmd.CommandText = sql;
            try {
                cmd.ExecuteNonQuery();
            } catch {
                MessageBox.Show("Dữ liệu đang được dùng, không thể xoá...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            cmd.Dispose();
            cmd = null;
            return true;
        }
        public DataTable FindHang(string keySearch) {
            DataTable dt = new DataTable();
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("FindHang", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Filter", keySearch);
                cnn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                dt.Load(r);
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return dt;
        }

        public dynamic GetData (string param, string sql) {
            dynamic result = null;
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Param", param);

            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.HasRows) {
                    reader.Read();
                    result = reader.GetValue(0);
                }

            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            if (result == null) result = string.Empty;
            return result;
        }
    }
}
