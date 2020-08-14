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
    class NhanHieuDB
    {
        string strConnection = @"server=.;database=QLBH_ViTinhDB;uid=sa;pwd=123456";
        SqlConnection cnn = null;

        public DataTable getListNhanHieu() {
            DataTable dt = new DataTable();
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("GetListNhanHieu", cnn);
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

        public bool InsertNhanHieu(NhanHieu n) {
            bool result;
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("InsertNhanHieu", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNhanHieu", n.MaNhanHieu);
                cmd.Parameters.AddWithValue("@TenNhanHieu", n.TenNhanHieu);
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
            string query = "Select MaNhanHieu From tblNhanHieu where MaNhanHieu=@Code";
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
        public bool UpdateNhanHieu(NhanHieu n) {
            bool result;
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("UpdateNhanHieu", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNhanHieu", n.MaNhanHieu);
                cmd.Parameters.AddWithValue("@TenNhanHieu", n.TenNhanHieu);
                cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }
        public bool DeleteNhanHieu(string MaNhanHieu) {
            bool result;
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("DeleteNhanHieu", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNhanHieu", MaNhanHieu);
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

            string sql = "DELETE tblNhanHieu WHERE MaNhanHieu=N'";
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
    }
}
