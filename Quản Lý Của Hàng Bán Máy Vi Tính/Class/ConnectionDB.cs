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
    class ConnectionDB
    {
        public static SqlConnection Con;

        public static void Connect() {
            Con = new SqlConnection();
            Con.ConnectionString = @"server=.;database=QLBH_ViTinhDB;uid=sa;pwd=123456";

            Con.Open();

            if (Con.State == ConnectionState.Open)
                MessageBox.Show("Kết nối thành công", "Thông báo", MessageBoxButtons.OK);
            else MessageBox.Show("Không thể kết nối với dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public static void Disconnect() {
            if (Con.State == ConnectionState.Open) {
                Con.Close();
                Con.Dispose();
                Con = null;
            }
        }
    }
}





