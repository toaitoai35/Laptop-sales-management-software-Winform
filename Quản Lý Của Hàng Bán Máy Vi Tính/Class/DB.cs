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
    class DB
    {
        public static SqlConnection Con;  //Khai báo đối tượng kết nối        

        public static void Connect() {
            Con = new SqlConnection();   //Khởi tạo đối tượng
            Con.ConnectionString = @"server=.;database=QLBH_ViTinhDB;uid=sa;pwd=123456";
            Con.Open();                  //Mở kết nối
            //Kiểm tra kết nối
            if (Con.State == ConnectionState.Open)
                MessageBox.Show("Kết nối thành công");
            else MessageBox.Show("Không thể kết nối với dữ liệu");

        }
        public static void Disconnect() {
            if (Con.State == ConnectionState.Open) {
                Con.Close();   	//Đóng kết nối
                Con.Dispose(); 	//Giải phóng tài nguyên
                Con = null;
            }
        }
        public static DataTable GetDataToTable(string sql) {
            Con = new SqlConnection();
            DataTable table = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(sql, Con); //Định nghĩa đối tượng thuộc lớp SqlDataAdapter

            dap.Fill(table);
            return table;
        }
        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten) {
            try {
                SqlDataAdapter dap = new SqlDataAdapter(sql, Con);

                DataTable table = new DataTable();
                Connect();
                dap.Fill(table);
                cbo.DataSource = table;
                cbo.ValueMember = ma; //Trường giá trị
                cbo.DisplayMember = ten; //Trường hiển thị
            } catch (SqlException ex) {
                throw new Exception(ex.Message);
            } finally {
                Disconnect();
            }
        }

        public static string ConvertDateTime(string date) {
            string[] elements = date.Split('/');
            string dt = string.Format("{0}/{1}/{2}", elements[0], elements[1], elements[2]);
            return dt;
        }
        public static string GetFieldValues(string sql) {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, Con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }

    }
}
