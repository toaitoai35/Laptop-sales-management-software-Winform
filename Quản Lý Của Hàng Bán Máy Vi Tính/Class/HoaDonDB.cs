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
    class HoaDonDB
    {
        string strConnection = @"server=.;database=QLBH_ViTinhDB;uid=sa;pwd=123456";
        SqlConnection cnn = null;


        public string GetMaHangHDCT(string MaHang, string MaHoaDon) {
            string result = string.Empty;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand("GetMaHangHD2Param", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
            cmd.Parameters.AddWithValue("@MaHang", MaHang);
            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.HasRows) {
                    reader.Read();
                    result = reader.GetValue(0).ToString();
                }
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }

        public DataTable GetDataToComboBox(string sql) {
            DataTable result = new DataTable();

            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                da.Fill(result);
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                if (cnn.State == ConnectionState.Open) cnn.Close();
            }

            return result;
        }

        public string ConvertDateTime(string date) {
            string[] elements = date.Split('/');
            string dt = string.Format("{0}/{1}/{2}", elements[0], elements[1], elements[2]);
            return dt;
        }

        public DataTable GetDataToTable(string sql) {
            DataTable result = null;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SqlDataAdapter dap = new SqlDataAdapter(sql, cnn); //Định nghĩa đối tượng thuộc lớp SqlDataAdapter
                                                                   //Khai báo đối tượng table thuộc lớp DataTable
                result = new DataTable();
                dap.Fill(result); //Fill kết quả từ câu lệnh sql vào table
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                if (cnn.State == ConnectionState.Open) cnn.Close();

            }
            return result;
        }

        public DataTable GetData(string MaHoaDon) {
            DataTable result = null;
            string sql = "SELECT a.MaHang, b.TenHang, a.SoLuong, b.DonGiaBan, a.GiamGia, a.ThanhTien " +
                "FROM tblHDChiTiet a, tblHang b " +
                "WHERE a.MaHoaDon = N'" + MaHoaDon + "' " +
                "AND a.MaHang=b.MaHang";
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SqlDataAdapter dap = new SqlDataAdapter(sql, cnn); //Định nghĩa đối tượng thuộc lớp SqlDataAdapter
                                                                   //Khai báo đối tượng table thuộc lớp DataTable
                result = new DataTable();
                dap.Fill(result); //Fill kết quả từ câu lệnh sql vào table
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                if (cnn.State == ConnectionState.Open) cnn.Close();

            }
            return result;
        }

        public string GetFieldValues(string sql) {
            string ma = string.Empty;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.HasRows) {
                    reader.Read();
                    ma = reader.GetValue(0).ToString();
                }

            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return ma;
        }

        public string GetFieldValuesDDatetime(string sql) {
            string ma = "";
            try {

                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand(sql, cnn);
                SqlDataReader reader;
                cnn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    reader.Read();
                    ma = reader.GetDateTime(0).ToString();
                }
                cnn.Close();
                //return ma;
            } catch (SqlException ex) {
                MessageBox.Show(ex.ToString()); // 

            } finally {
                cnn.Close();
            }
            return ma;
        }


        public string ConvertTimeTo24(string hour) {
            string h = "";
            switch (hour) {
                case "1":
                    h = "13";
                    break;
                case "2":
                    h = "14";
                    break;
                case "3":
                    h = "15";
                    break;
                case "4":
                    h = "16";
                    break;
                case "5":
                    h = "17";
                    break;
                case "6":
                    h = "18";
                    break;
                case "7":
                    h = "19";
                    break;
                case "8":
                    h = "20";
                    break;
                case "9":
                    h = "21";
                    break;
                case "10":
                    h = "22";
                    break;
                case "11":
                    h = "23";
                    break;
                case "12":
                    h = "0";
                    break;
            }
            return h;
        }
        public string CreateKey(string tiento) {
            string key = tiento;
            string[] partsDay;
            partsDay = DateTime.Now.ToShortDateString().Split('/');
            //Ví dụ 07/08/2009
            string d = String.Format("{0}{1}{2}", partsDay[0], partsDay[1], partsDay[2]);
            key = key + d;
            string[] partsTime;
            partsTime = DateTime.Now.ToLongTimeString().Split(':');
            //Ví dụ 7:08:03 PM hoặc 7:08:03 AM
            if (partsTime[2].Substring(3, 2) == "PM")
                partsTime[0] = ConvertTimeTo24(partsTime[0]);
            if (partsTime[2].Substring(3, 2) == "AM")
                if (partsTime[0].Length == 1)
                    partsTime[0] = "0" + partsTime[0];
            //Xóa ký tự trắng và PM hoặc AM
            partsTime[2] = partsTime[2].Remove(2, 3);
            string t;
            t = String.Format("_{0}{1}{2}", partsTime[0], partsTime[1], partsTime[2]);
            key = key + t;
            return key;
        }

        public void RunSQL(string sql) {
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            try {
                cnn.Open();
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            } finally {
                cnn.Close();
            }
        }
        public void InsertHoaDon(HoaDon hd) {
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand("InsertHoaDon", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@MaHoaDon", hd.MaHoaDon);
            cmd.Parameters.AddWithValue("@MaNhanVien", hd.MaNhanVien);
            cmd.Parameters.AddWithValue("@NgayBan", hd.NgayBan);
            cmd.Parameters.AddWithValue("@MaKhach", hd.MaKhach);
            cmd.Parameters.AddWithValue("@TongTien", hd.TongTien);

            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                cmd.ExecuteNonQuery();
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
        }

        public void InsertHDCT(HDChiTiet hdct) {
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand("InsertHDChiTiet", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@MaHoaDon", hdct.MaHoaDon);
            cmd.Parameters.AddWithValue("@MaHang", hdct.MaHang);
            cmd.Parameters.AddWithValue("@SoLuong", hdct.SoLuong);
            cmd.Parameters.AddWithValue("@DonGiaBan", hdct.DonGiaBan);
            cmd.Parameters.AddWithValue("@GiamGia", hdct.GiamGia);
            cmd.Parameters.AddWithValue("@ThanhTien", hdct.ThanhTien);


            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                cmd.ExecuteNonQuery();
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
        }

        public bool DeleteHoaDon(string MaHoaDon) {
            bool result;
            try {
                cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand("DeleteHoaDon", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
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

        public string GetDataToComponent(string MaHoaDon, string sql) { // from tblHoaDon
            string result = string.Empty;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.HasRows) {
                    reader.Read();
                    result = reader.GetValue(0).ToString();
                }
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }

        public string GetSoLuongBaseOnMaHangHD(string MaHang) {
            string result = string.Empty;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand("GetSoLuongBaseOnMaHangHD", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MaHang", MaHang);
            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.HasRows) {
                    reader.Read();
                    result = reader.GetValue(0).ToString();
                }

            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }

        public bool UpdateSoLuong(float SoLuong, string MaHang) {
            bool result = false;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand("UpdateSoLuong", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SoLuong", SoLuong);
            cmd.Parameters.AddWithValue("@MaHang", MaHang);

            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }

        public bool UpdateTongTien(double TongTien, string MaHoaDon) {
            bool result = false;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand("UpdateTongTien", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TongTien", TongTien);
            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);

            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }

        public dynamic GetDataFromNhanVien(string MaNhanVien, string sql) {
            dynamic result = null;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MaNhanVien", MaNhanVien);

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

        public dynamic GetDataFromHang(string MaHang, string sql) {
            dynamic result = null;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MaHang", MaHang);

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

        public dynamic GetDataFromKhachHang(string MaKhachHang, string sql) {
            dynamic result = null;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MaKhach", MaKhachHang);

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

        public DataTable GetDataTableFromHDChiTiet(string MaHoaDon, string sql) {
            DataTable result = null;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);

            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                result = new DataTable();
                dap.Fill(result);
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }

        public dynamic GetDataFromHoaDon(string MaHoadon, string sql) {
            dynamic result = null;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoadon);

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

        public bool DeleteHoaDonChiTiet(string MaHoaDon, string MaHang) {
            bool result = false;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand("DeleteHoaDonChiTiet", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
            cmd.Parameters.AddWithValue("@MaHang", MaHang);

            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }
        public DataTable GetThongTinHD(string MaHoaDon) {
            DataTable result = null;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand("GetThongTinHD", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);

            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                result = new DataTable();
                dap.Fill(result);
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }

        public DataTable GetThongTinHang(string MaHoaDon) {
            DataTable result = null;
            cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand("GetThongTinHang", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);

            try {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                result = new DataTable();
                dap.Fill(result);
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                cnn.Close();
            }
            return result;
        }
    }
}
