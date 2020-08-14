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
    class HDChiTietDB
    {
        string strConnection = @"server=.;database=QLBH_ViTinhDB;uid=sa;pwd=123456";
        SqlConnection cnn = null;


        public bool checkDuplicate(object MaHang, string MaHoaDon) {
            bool result = false;
            string query = "Select MaHang From tblHang where MaHang=@Code AND MaHoaDon=@";
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.Parameters.AddWithValue("@MaHang", MaHang);
            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
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
        public bool DeleteHDChiTiet(string MaHoaDon) {
            bool result;
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("DeleteHDChiTiet", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
                cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }
    }
}
