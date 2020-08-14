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
    class NhanVienDB
    {
        string strConnection = @"server=.;database=QLBH_ViTinhDB;uid=sa;pwd=123456";
        SqlConnection cnn = null;

        public DataTable getListNhanVien() {
            DataTable dt = new DataTable();
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("GetListNhanVien", cnn);
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
        public bool InsertNhanVien(NhanVien n) {
            bool result;
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("InsertNhanVien", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNhanVien", n.MaNhanVien);
                cmd.Parameters.AddWithValue("@TenNhanVien", n.TenNhanVien);
                cmd.Parameters.AddWithValue("@DiaChi", n.DiaChi);
                cmd.Parameters.AddWithValue("@DienThoai", n.DienThoai);
                cmd.Parameters.AddWithValue("@GioiTinh", n.GioiTinh);
                cmd.Parameters.AddWithValue("@NgaySinh", n.NgaySinh);

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
            string query = "Select MaNhanVien From tblNhanVien where MaNhanVien=@Code";
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
        public bool UpdateNhanVien(NhanVien n) {
            bool result;
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("UpdateNhanVien", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNhanVien", n.MaNhanVien);
                cmd.Parameters.AddWithValue("@TenNhanVien", n.TenNhanVien);
                cmd.Parameters.AddWithValue("@DiaChi", n.DiaChi);
                cmd.Parameters.AddWithValue("@DienThoai", n.DienThoai);
                cmd.Parameters.AddWithValue("@GioiTinh", n.GioiTinh);
                cmd.Parameters.AddWithValue("@NgaySinh", n.NgaySinh);
                cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException ex){
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }
        public bool DeleteNhanVien(string MaNhanVien) {
            bool result;
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("DeleteNhanVien", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNhanVien", MaNhanVien);
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

            string sql = "DELETE tblNhanVien WHERE MaNhanVien=N'";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = ConnectionDB.Con;
            cmd.CommandText = sql;
            try {
                cmd.ExecuteNonQuery();
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            }
            cmd.Dispose();
            cmd = null;
            return true;
        }
        public DataTable FindNhanVien(string keySearch) {
            DataTable dt = new DataTable();
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("FindNhanVien", cnn);
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
    }
}
