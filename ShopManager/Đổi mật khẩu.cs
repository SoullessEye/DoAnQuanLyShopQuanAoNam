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

namespace ShopManager
{
    public partial class frmDoimatkhau : Form
    {
        string strConnection = @"Data Source=DESKTOP-RK7GEUP;Initial Catalog=ClothesManager;Integrated Security=True";
        SqlConnection conn;
        public frmDoimatkhau()
        {
            InitializeComponent();
        }

        private void frmDoimatkhau_Load(object sender, EventArgs e)
        {
            if (conn == null)
                conn = new SqlConnection(strConnection);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand comand = new SqlCommand();
            comand.CommandType = CommandType.Text;
            comand.CommandText = "select*from Account where Ma = 1";
            comand.Connection = conn;
            SqlDataReader reader = comand.ExecuteReader();
            if (reader.Read())
            {
                txtTendangnhap.Text = reader.GetString(1).Trim();
            }
            reader.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (conn == null)
                conn = new SqlConnection(strConnection);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select*from Account where Ma=1";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (txtMatkhaucu.Text == reader.GetString(2).Trim())
                {
                    reader.Close();
                    if (txtMatkhaumoi.Text == txtXacnhan.Text)
                    {
                        if (conn == null)
                            conn = new SqlConnection(strConnection);
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        command = new SqlCommand();
                        command.CommandType = CommandType.Text;
                        command.CommandText = "update Account set Matkhau=@matkhau where Ma=1";
                        command.Connection = conn;
                        command.Parameters.Add("@matkhau", SqlDbType.NVarChar).Value = txtMatkhaumoi.Text;
                        int ret = command.ExecuteNonQuery();
                        if(ret>0)
                        {
                            MessageBox.Show("Cập nhật mật khẩu thành công");
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật mật khẩu thất bại");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Xác nhận mật khẩu chưa chính xác");
                }
            }
            else
            {
                MessageBox.Show("Bạn nhập mật khẩu cũ chưa chính xác");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
