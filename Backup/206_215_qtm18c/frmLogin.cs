using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _206_215_qtm18c
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

            Class.Functions.Connect();
            SqlHelper.con = new SqlConnection(SqlHelper.ConnectString);
            SqlHelper.con.Open();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from tblLogin where tendangnhap=@tendangnhap and matkhau=@matkhau", SqlHelper.con);
            cmd.Parameters.AddWithValue("@tendangnhap", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@matkhau", txtPassword.Text.Trim());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SqlHelper.tennguoidung = dt.Rows[0]["tendangnhap"].ToString().Trim();
                SqlHelper.loainguoidung = Convert.ToInt32(dt.Rows[0]["loainguoidung"]);

                frmMain f = new frmMain();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại, Sai tên người dùng hoặc mật khẩu", "Thông báo đăng nhập thất bại", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
