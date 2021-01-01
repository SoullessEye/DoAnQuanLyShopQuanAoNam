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
    public partial class Login : Form
    {
        string strConnection = @"Data Source=DESKTOP-RK7GEUP;Initial Catalog=ClothesManager;Integrated Security=True";
        SqlConnection conn;
        public Login()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(strConnection);
            conn.Open();
            string tk = txtTaiKhoan.Text;
            string mk = txtMatKhau.Text;
            string sql = "select * from Account where taikhoan = '" + tk + "' and matkhau = '" + mk + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                MessageBox.Show("Đăng nhập thành công");
                frmManager f = new frmManager();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ckbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbShowPass.Checked)
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
            }
        }
    }
}
