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
    public partial class frmQLKH : Form
    {
        public frmQLKH()
        {
            InitializeComponent();
        }
        SqlConnection conn = null;
        string strConn = @"Data Source=DESKTOP-RK7GEUP;Initial Catalog=ClothesManager;Integrated Security=True";
        SqlDataAdapter daKhachhang = null;
        DataTable dtKhachhang = null;
        bool Them;
        private void frmQLKH_Load(object sender, EventArgs e)
        {
            this.khachhangTableAdapter.Fill(this.clothesManagerDataSet.Khachhang);
            LoadData();
        }
        void LoadData()
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(strConn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                daKhachhang = new SqlDataAdapter("Select*from Khachhang", conn);
                dtKhachhang = new DataTable();
                dtKhachhang.Clear();
                daKhachhang.Fill(dtKhachhang);
                dgvKhachhang.DataSource = dtKhachhang;
                txtMakh.Text = "";
                txtTenkh.Text = "";
                txtDiachi.Text = "";
                txtDt.Text = "";

                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                panel1.Enabled = false;

                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThoat.Enabled = true;
            }
            catch(SqlException)
            {
                MessageBox.Show("Loi !!!");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            txtMakh.Text = "";
            txtTenkh.Text = "";
            txtDiachi.Text = "";
            txtDt.Text = "";

            this.btnLuu.Enabled = true;
            this.btnHuy.Enabled = true;
            this.panel1.Enabled = true;

            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnThoat.Enabled = false;
            txtMakh.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            panel1.Enabled = true;
            int r = dgvKhachhang.CurrentCell.RowIndex;
            txtMakh.Text = dgvKhachhang.Rows[r].Cells[0].Value.ToString();
            txtTenkh.Text = dgvKhachhang.Rows[r].Cells[1].Value.ToString();
            txtDiachi.Text = dgvKhachhang.Rows[r].Cells[2].Value.ToString();
            txtDt.Text = dgvKhachhang.Rows[r].Cells[3].Value.ToString();
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            panel1.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Enabled = false;
            txtMakh.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "insert into Khachhang values(@Makh,@Tenkh,@Diachi,@Dienthoai)";
                    command.Connection = conn;
                    command.Parameters.Add("@Makh", SqlDbType.NVarChar).Value = txtMakh.Text;
                    command.Parameters.Add("@Tenkh", SqlDbType.NVarChar).Value = txtTenkh.Text;
                    command.Parameters.Add("@Diachi", SqlDbType.NVarChar).Value = txtDiachi.Text;
                    command.Parameters.Add("@Dienthoai", SqlDbType.NVarChar).Value = txtDt.Text;
                    command.ExecuteNonQuery();
                    LoadData();
                    MessageBox.Show("Đã thêm xong");
                }
                catch(SqlException)
                {
                    MessageBox.Show("không thêm được, lỗi !!!");
                }
            }
            if (!Them)
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    int r = dgvKhachhang.CurrentCell.RowIndex;
                    string Makh = dgvKhachhang.Rows[r].Cells[0].Value.ToString();
                    command.CommandText = "update Khachhang set Tenkh=@Tenkh, Diachi=@Diachi, Dienthoai=@Dienthoai where Makh=@Makh";
                    command.Connection = conn;
                    
                    command.Parameters.Add("@Tenkh", SqlDbType.NVarChar).Value = txtTenkh.Text;
                    command.Parameters.Add("@Diachi", SqlDbType.NVarChar).Value = txtDiachi.Text;
                    command.Parameters.Add("@Dienthoai", SqlDbType.NVarChar).Value = txtDt.Text;
                    command.Parameters.Add("@Makh", SqlDbType.NVarChar).Value = Makh;
                    command.ExecuteNonQuery();
                    LoadData();
                    MessageBox.Show("Đã cập nhật xong");
                }
                catch (SqlException)
                {
                    MessageBox.Show("không cập nhật được, lỗi !!!");
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMakh.Text = "";
            txtTenkh.Text = "";
            txtDiachi.Text = "";
            txtDt.Text = "";

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            panel1.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThoat.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                int r = dgvKhachhang.CurrentCell.RowIndex;
                string Makh = dgvKhachhang.Rows[r].Cells[0].Value.ToString();
                command.CommandText = "delete from Khachhang where Makh=@Makh";
                command.Connection = conn;
                command.Parameters.Add("@Makh", SqlDbType.NVarChar).Value = Makh;
                command.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Xóa xong");
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được, lỗi");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmQLKH_FormClosing(object sender, FormClosingEventArgs e)
        {
            dtKhachhang.Dispose();
            dtKhachhang = null;
            conn = null;
        }
    }
}
