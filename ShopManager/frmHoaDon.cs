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
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();
        }
        SqlConnection conn = null;
        string strConn = @"Data Source=DESKTOP-RK7GEUP;Initial Catalog=ClothesManager;Integrated Security=True";
        SqlDataAdapter daNhanvien = null;
        DataTable dtNhanvien = null;
        SqlDataAdapter daKhachhang = null;
        DataTable dtKhachhang = null;
        SqlDataAdapter daHoadon = null;
        DataTable dtHoadon = null;
        bool Them;
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'clothesManagerDataSet2.Hoadon' table. You can move, or remove it, as needed.
            //this.hoadonTableAdapter.Fill(this.clothesManagerDataSet2.Hoadon);
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
                daHoadon = new SqlDataAdapter("Select*from Hoadon", conn);
                dtHoadon = new DataTable();
                dtHoadon.Clear();
                daHoadon.Fill(dtHoadon);
                dgvHoadon.DataSource = dtHoadon;
                dgvHoadon.AutoResizeColumns();
                daKhachhang = new SqlDataAdapter("Select Makh from Khachhang", conn);
                dtKhachhang = new DataTable();
                dtKhachhang.Clear();
                daKhachhang.Fill(dtKhachhang);
                daNhanvien = new SqlDataAdapter("Select MaNV from Nhanvienn", conn);
                dtNhanvien = new DataTable();
                dtNhanvien.Clear();
                daNhanvien.Fill(dtNhanvien);
                txtMaHD.Text = "";
                dateTimeNgaylap.Text = "";

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
            txtMaHD.Text = "";
            dateTimeNgaylap.Text = "";
            this.btnLuu.Enabled = true;
            this.btnHuy.Enabled = true;
            this.panel1.Enabled = true;

            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnThoat.Enabled = false;
            cmbMakh.DataSource = dtKhachhang;
            cmbMakh.ValueMember = "Makh";
            cmbMakh.DisplayMember = "Makh";
            cmbMaNV.DataSource = dtNhanvien;
            cmbMaNV.ValueMember = "MaNV";
            cmbMaNV.DisplayMember = "MaNV"; 
            txtMaHD.Focus();
        }

        private void frmHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            dtHoadon.Dispose();
            dtHoadon = null;
            conn = null;
            dtKhachhang.Dispose();
            dtKhachhang = null;
            dtNhanvien.Dispose();
            dtNhanvien = null;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            cmbMakh.DataSource = dtKhachhang;
            cmbMakh.ValueMember = "Makh";
            cmbMakh.DisplayMember = "Makh";
            cmbMaNV.DataSource = dtNhanvien;
            cmbMaNV.ValueMember = "MaNV";
            cmbMaNV.DisplayMember = "MaNV";

            panel1.Enabled = true;
            int r = dgvHoadon.CurrentCell.RowIndex;
            txtMaHD.Text = dgvHoadon.Rows[r].Cells[0].Value.ToString();
            dateTimeNgaylap.Text = dgvHoadon.Rows[r].Cells[1].Value.ToString();
            cmbMakh.SelectedValue = dgvHoadon.Rows[r].Cells[2].Value.ToString();
            cmbMaNV.SelectedValue = dgvHoadon.Rows[r].Cells[3].Value.ToString();
            

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            panel1.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Enabled = false;
            txtMaHD.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (Them)
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "insert into Hoadon values(@MaHD, @NgayLap, @Makh, @MaNV)";
                    command.Connection = conn;
                    command.Parameters.Add("@MaHD", SqlDbType.NVarChar).Value = txtMaHD.Text;
                    command.Parameters.Add("@NgayLap", SqlDbType.Date).Value = dateTimeNgaylap.Text;
                    command.Parameters.Add("@Makh", SqlDbType.NVarChar).Value = cmbMakh.SelectedValue;
                    command.Parameters.Add("@MaNV", SqlDbType.NVarChar).Value = cmbMaNV.SelectedValue;
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
                    int r = dgvHoadon.CurrentCell.RowIndex;
                    string MaHD = dgvHoadon.Rows[r].Cells[0].Value.ToString();
                    command.CommandText = "update Hoadon set NgayLap=@NgayLap, Makh=@Makh, MaNV=@MaNV where MaHD=@MaHD";
                    command.Connection = conn;

                    command.Parameters.Add("@NgayLap", SqlDbType.Date).Value = dateTimeNgaylap.Text;
                    command.Parameters.Add("@Makh", SqlDbType.NVarChar).Value = cmbMakh.SelectedValue;
                    command.Parameters.Add("@MaNV", SqlDbType.NVarChar).Value = cmbMaNV.SelectedValue;
                    command.Parameters.Add("@MaHD", SqlDbType.NVarChar).Value = MaHD;
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
            txtMaHD.Text = "";
            dateTimeNgaylap.Text = "";

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
                int r = dgvHoadon.CurrentCell.RowIndex;
                string MaHD = dgvHoadon.Rows[r].Cells[0].Value.ToString();
                command.CommandText = "delete from Hoadon where MaHD=@MaHD";
                command.Connection = conn;
                command.Parameters.Add("@MaHD", SqlDbType.NVarChar).Value = MaHD;
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
