using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Code_Lab_7
{
    public partial class frm_login : Form
    {
        LopDungChung lopchung;
        public frm_login()
        {
            InitializeComponent();
            lopchung = new LopDungChung();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string chuoiketnoi = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Downloads\BTVN_Lab_7\Code_Lab_7\Code_Lab_7\SQL_KHACHHANG.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            string sqlCheck = "select count (*) from TAIKHOAN where username='" + txt_username.Text + "' and password='" + txt_password.Text + "'";
            int ketqua = (int)lopchung.Scalar(sqlCheck);
            if (ketqua != 0)
            {
                Form frm_KhachHang = new frm_KhachHang();
                frm_KhachHang.Show();
            }
            else
            {
                MessageBox.Show("Thông tin đăng nhập không đúng");
            }
        }
    }
}
