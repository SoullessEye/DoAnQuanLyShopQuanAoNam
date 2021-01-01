using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanQADAL
{
    public class KetnoiDB
    {
        public static SqlConnection connect;
        public void MoKetNoi()
        {
            if (KetnoiDB.connect == null)
                KetnoiDB.connect = new SqlConnection("Data Source=DESKTOP-RK7GEUP;Initial Catalog=ClothesManager;Integrated Security=True");
            if (KetnoiDB.connect.State != ConnectionState.Open)
                KetnoiDB.connect.Open();
        }
        public void DongKetNoi()
        {
            if (KetnoiDB.connect != null)
            {
                if (KetnoiDB.connect.State == ConnectionState.Open)
                    KetnoiDB.connect.Close();
            }
        }
        public void ThucThiCauLenhSQL(string strSQl)
        {
            try
            {
                MoKetNoi();
                SqlCommand sqlcmd = new SqlCommand(strSQl, connect);
                sqlcmd.ExecuteNonQuery();
                DongKetNoi();
            }
            catch
            {

            }
        }
        public DataTable GetDataTable(string strSQL)
        {
            try
            {
                MoKetNoi();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlda = new SqlDataAdapter(strSQL, connect);
                sqlda.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}
