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
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        SqlConnection conn = null;
        string strConn = @"Data Source=DESKTOP-CET28PS\SQLEXPRESS;Initial Catalog=ClothesManager;Integrated Security=True";
        SqlDataAdapter daNhanvien = null;
        DataTable dtNhanvien = null;
        bool Them;
        private void frmNhanVien_Load(object sender, EventArgs e)
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
                daNhanvien = new SqlDataAdapter("Select*from NhanVienn", conn);
                dtNhanvien = new DataTable();
                dtNhanvien.Clear();
                daNhanvien.Fill(dtNhanvien);
                dgvNhanvien.DataSource = dtNhanvien;
                txtMaNV.Text = "";
                txtTenNV.Text = "";
                txtDiachi.Text = "";
                txtDt.Text = "";
                txtTenDN.Text = "";
                txtMatkhau.Text = "";

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
            txtMaNV.Text = "";
            txtTenDN.Text = "";
            txtDiachi.Text = "";
            txtDt.Text = "";
            txtTenDN.Text = "";
            txtMatkhau.Text = "";

            this.btnLuu.Enabled = true;
            this.btnHuy.Enabled = true;
            this.panel1.Enabled = true;

            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnThoat.Enabled = false;
            txtMaNV.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            panel1.Enabled = true;
            int r = dgvNhanvien.CurrentCell.RowIndex;
            txtMaNV.Text = dgvNhanvien.Rows[r].Cells[0].Value.ToString();
            txtTenNV.Text = dgvNhanvien.Rows[r].Cells[1].Value.ToString();
            txtDiachi.Text = dgvNhanvien.Rows[r].Cells[2].Value.ToString();
            txtDt.Text = dgvNhanvien.Rows[r].Cells[3].Value.ToString();
            txtTenDN.Text = dgvNhanvien.Rows[r].Cells[4].Value.ToString();
            txtMatkhau.Text = dgvNhanvien.Rows[r].Cells[5].Value.ToString();
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            panel1.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Enabled = false;
            txtMaNV.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "insert into NhanVienn values(@MaNV,@TenNV,@Diachi,@Dienthoai, @TenDN, @Matkhau)";
                    command.Connection = conn;
                    command.Parameters.Add("@MaNV", SqlDbType.NVarChar).Value = txtMaNV.Text;
                    command.Parameters.Add("@TenNV", SqlDbType.NVarChar).Value = txtTenNV.Text;
                    command.Parameters.Add("@Diachi", SqlDbType.NVarChar).Value = txtDiachi.Text;
                    command.Parameters.Add("@Dienthoai", SqlDbType.NVarChar).Value = txtDt.Text;
                    command.Parameters.Add("@TenDN", SqlDbType.NVarChar).Value = txtTenDN.Text;
                    command.Parameters.Add("@Matkhau", SqlDbType.NVarChar).Value = txtMatkhau.Text;
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
                    int r = dgvNhanvien.CurrentCell.RowIndex;
                    string MaNV = dgvNhanvien.Rows[r].Cells[0].Value.ToString();
                    command.CommandText = "update NhanVienn set TenNV=@TenNV, Diachi=@Diachi, Dienthoai=@Dienthoai, TenDN=@TenDN, Matkhau=@Matkhau where MaNV=@MaNV";
                    command.Connection = conn;

                    command.Parameters.Add("@TenNV", SqlDbType.NVarChar).Value = txtTenNV.Text;
                    command.Parameters.Add("@Diachi", SqlDbType.NVarChar).Value = txtDiachi.Text;
                    command.Parameters.Add("@Dienthoai", SqlDbType.NVarChar).Value = txtDt.Text;
                    command.Parameters.Add("@TenDN", SqlDbType.NVarChar).Value = txtTenDN.Text;
                    command.Parameters.Add("@Matkhau", SqlDbType.NVarChar).Value = txtMatkhau.Text;
                    command.Parameters.Add("@MaNV", SqlDbType.NVarChar).Value = MaNV;
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
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtDiachi.Text = "";
            txtDt.Text = "";
            txtTenDN.Text = "";
            txtMatkhau.Text = "";

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
                int r = dgvNhanvien.CurrentCell.RowIndex;
                string MaNV = dgvNhanvien.Rows[r].Cells[0].Value.ToString();
                command.CommandText = "delete from NhanVienn where MaNV=@MaNV";
                command.Connection = conn;
                command.Parameters.Add("@MaNV", SqlDbType.NVarChar).Value = MaNV;
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
