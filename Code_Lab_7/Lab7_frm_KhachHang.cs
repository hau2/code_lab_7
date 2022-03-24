using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code_Lab_7
{
    public partial class frm_KhachHang : Form
    {
        LopDungChung lopchung;
        string duongdan = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\HinhAnh\\";
        public frm_KhachHang()
        {
            InitializeComponent();
            lopchung = new LopDungChung();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có muốn đóng không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            Application.Exit();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string sqlThem = "insert into KHACHHANG values('"+txt_MAKH.Text.Trim()+"',N'" + txt_HoTen.Text + "','" + dateTimePicker1.Value + "', '"+cb_TenHang.SelectedValue+"','" +txt_SoLuong.Text + "','" + list_DiaChi.SelectedValue + "','"+txt_MAKH.Text+".png"+"')";
            pictureBox1.Image.Save(duongdan + txt_MAKH.Text.Trim()+".jpg");  // LỖI  A generic error occurred in GDI+.  Thêm ký tự TextBox hình ảnh chứ ko nên để trống. Hoặc để mặc định trong ô TextBox Hình Ảnh là jpg_png_jpeg
            lopchung.Nonquery(sqlThem);
            LoadDataKH();
        }

        private void frm_KhachHang_Load(object sender, EventArgs e)
        {
            LoadDataKH();
            LoadCombobox();
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 10);
            LoadDiaChi();
        }

        private void LoadDataKH()
        {
            string chuoiketnoi = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Downloads\BTVN_Lab_7\Code_Lab_7\Code_Lab_7\SQL_KHACHHANG.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            string sqlLoad = "select * from KHACHHANG";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoad, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void LoadCombobox()
        {
            string chuoiketnoi = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Downloads\BTVN_Lab_7\Code_Lab_7\Code_Lab_7\SQL_KHACHHANG.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            string sqlLoad = "select * from HANG";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoad, conn);
            DataTable dtCombo = new DataTable();
            da.Fill(dtCombo);
            cb_TenHang.DataSource = dtCombo;
            cb_TenHang.DisplayMember = "TENHANG";
            cb_TenHang.ValueMember = "MAHANG";
        }
        private void LoadDiaChi()
        {
            string chuoiketnoi = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Downloads\BTVN_Lab_7\Code_Lab_7\Code_Lab_7\SQL_KHACHHANG.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            string sqlLoad = "select * from DIACHI";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoad, conn);
            DataTable dtCombo = new DataTable();
            da.Fill(dtCombo);
            list_DiaChi.DataSource = dtCombo;
            list_DiaChi.DisplayMember = "TENDIACHI";
            list_DiaChi.ValueMember = "MADIACHI";
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string sqlSua = "update KHACHHANG set TENKH = N'" + txt_HoTen.Text + "', NGAYMUAHANG = '" + dateTimePicker1.Value + "', MAHANG='" + cb_TenHang.SelectedValue + "', SOLUONG='" + txt_SoLuong.Text + "', MADIACHI='" + list_DiaChi.SelectedValue + "' where MAKH = '" + txt_MAKH.Text + "'"; //,'" +txt_SoLuong.Text +"')"
            pictureBox1.Image.Save(duongdan + txt_MAKH.Text + ".jpg");  // LỖI  A generic error occurred in GDI+.  Thêm ký tự TextBox hình ảnh chứ ko nên để trống. Hoặc để mặc định trong ô TextBox Hình Ảnh là jpg_png_jpeg
            lopchung.Nonquery(sqlSua);
            LoadDataKH();
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string sqlXoa = "delete from KHACHHANG where MAKH = '" + txt_MAKH.Text + "'";
            lopchung.Nonquery(sqlXoa);
            LoadDataKH();
        }

        private void btn_dem_Click(object sender, EventArgs e)
        {
            string sqlDem = "select COUNT(*) from KHACHHANG";
            int ketqua = (int)lopchung.Scalar(sqlDem);
            txt_dem.Text = ketqua.ToString();
        }
        int chon = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_MAKH.Text = dataGridView1.CurrentRow.Cells["MAKH"].Value.ToString().Trim();
            txt_HoTen.Text = dataGridView1.CurrentRow.Cells["TENKH"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["NGAYMUAHANG"].Value.ToString();
            txt_SoLuong.Text = dataGridView1.CurrentRow.Cells["SOLUONG"].Value.ToString();
            list_DiaChi.SelectedValue = dataGridView1.CurrentRow.Cells["MADIACHI"].Value.ToString();
            list_DiaChi.Text = dataGridView1.CurrentRow.Cells["MADIACHI"].Value.ToString();
            pictureBox1.ImageLocation = duongdan + txt_MAKH.Text+".jpg";
            chon = 1;
            cb_TenHang.SelectedValue = dataGridView1.CurrentRow.Cells["MAHANG"].Value.ToString();
        }

        private void rbo_Tang_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns["SOLUONG"], ListSortDirection.Ascending);
        }

        private void rbo_Giam_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns["SOLUONG"], ListSortDirection.Descending);
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            string sqlTim = "select * from KHACHHANG where TENKH like N'%" + txt_Tim.Text + "%' ";
            dataGridView1.DataSource = lopchung.LoadKhachHang(sqlTim);
        }
        private void LoadKhachHang()
        {
            string sqlKH = "select * from KHACHHANG";
            dataGridView1.DataSource = lopchung.LoadKhachHang(sqlKH);
        }
        private void btn_LoadLaibang_Click(object sender, EventArgs e)
        {
            LoadKhachHang();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Tất cả các file|*.*|JPG|*.jpg|chọn file PNG|*.png|JPEG|*.jpeg";
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(fileDialog.FileName);
            }
        }
       
        private void cb_TenHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_TenHang.SelectedValue != null && chon != 0)
            {
                string textCB = cb_TenHang.GetItemText(cb_TenHang.SelectedValue).ToString();
                string sqlCB = "select * from KHACHHANG where MAHANG ='" + textCB + "'";
                dataGridView1.DataSource = lopchung.LoadKhachHang(sqlCB);
            } else
            {
                chon = 1;
            }
        }
    }
}
