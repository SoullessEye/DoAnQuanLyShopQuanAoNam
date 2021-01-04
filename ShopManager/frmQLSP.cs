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
    public partial class frmQLSP : Form
    {
        public frmQLSP()
        {
            InitializeComponent();
        }
        SqlConnection conn = null;
        string strConn = @"Data Source=DESKTOP-CET28PS\SQLEXPRESS;Initial Catalog=ClothesManager;Integrated Security=True";
        SqlDataAdapter daSanpham = null;
        DataTable dtSanpham = null;
        bool Them;
        private void frmQLSP_Load(object sender, EventArgs e)
        {
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
                daSanpham = new SqlDataAdapter("Select*from SanPham", conn);
                dtSanpham = new DataTable();
                dtSanpham.Clear();
                daSanpham.Fill(dtSanpham);
                dgvSanpham.DataSource = dtSanpham;
                txtMasp.Text = "";
                txtTensp.Text = "";
                txtDongia.Text = "";
                txtNhaSX.Text = "";
                txtSize.Text = "";

                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                panel1.Enabled = false;

                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThoat.Enabled = true;
            }
            catch (SqlException)
            {
                MessageBox.Show("Loi !!!");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            txtMasp.Text = "";
            txtTensp.Text = "";
            txtDongia.Text = "";
            txtNhaSX.Text = "";
            txtSize.Text = "";

            this.btnLuu.Enabled = true;
            this.btnHuy.Enabled = true;
            this.panel1.Enabled = true;

            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnThoat.Enabled = false;
            txtMasp.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            panel1.Enabled = true;
            int r = dgvSanpham.CurrentCell.RowIndex;
            txtMasp.Text = dgvSanpham.Rows[r].Cells[0].Value.ToString();
            txtTensp.Text = dgvSanpham.Rows[r].Cells[1].Value.ToString();
            txtDongia.Text = dgvSanpham.Rows[r].Cells[2].Value.ToString();
            txtNhaSX.Text = dgvSanpham.Rows[r].Cells[3].Value.ToString();
            txtSize.Text = dgvSanpham.Rows[r].Cells[4].Value.ToString();
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            panel1.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Enabled = false;
            txtMasp.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(Them)
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "insert into SanPham values(@Masp,@Tensp,@Dongia,@NhaSX, @Size)";
                    command.Connection = conn;
                    command.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = txtMasp.Text;
                    command.Parameters.Add("@Tensp", SqlDbType.NVarChar).Value = txtTensp.Text;
                    command.Parameters.Add("@Dongia", SqlDbType.Float).Value = txtDongia.Text;
                    command.Parameters.Add("@NhaSX", SqlDbType.NVarChar).Value = txtNhaSX.Text;
                    command.Parameters.Add("@Size", SqlDbType.NVarChar).Value = txtSize.Text;
                    command.ExecuteNonQuery();
                    LoadData();
                    MessageBox.Show("Đã thêm xong");
                }
                catch (SqlException)
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
                    int r = dgvSanpham.CurrentCell.RowIndex;
                    string MaSP = dgvSanpham.Rows[r].Cells[0].Value.ToString();
                    command.CommandText = "update SanPham set Tensp=@Tensp, Dongia=@Dongia, NhaSX=@NhaSX, Size=@Size where Masp=@Masp";
                    command.Connection = conn;

                    command.Parameters.Add("@Tensp", SqlDbType.NVarChar).Value = txtTensp.Text;
                    command.Parameters.Add("@Dongia", SqlDbType.Float).Value = txtDongia.Text;
                    command.Parameters.Add("@NhaSX", SqlDbType.NVarChar).Value = txtNhaSX.Text;
                    command.Parameters.Add("@Size", SqlDbType.NVarChar).Value = txtSize.Text;
                    command.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = MaSP;
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
            txtMasp.Text = "";
            txtTensp.Text = "";
            txtDongia.Text = "";
            txtNhaSX.Text = "";
            txtSize.Text = "";

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
                int r = dgvSanpham.CurrentCell.RowIndex;
                string MaSP = dgvSanpham.Rows[r].Cells[0].Value.ToString();
                command.CommandText = "delete from SanPham where Masp=@Masp";
                command.Connection = conn;
                command.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = MaSP;
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
    }
}
