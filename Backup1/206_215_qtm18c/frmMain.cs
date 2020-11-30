using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _206_215_qtm18c.Class;

namespace _206_215_qtm18c
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Class.Functions.Connect();
            MessageBox.Show("Chúc mừng, bạn đã đăng nhập thành công");
            if (SqlHelper.loainguoidung == 2)
            {
                this.mnuNhaCC.Enabled = false;
                this.mnuHanghoa.Enabled = false;
                this.mnuNhanvien.Enabled = false;
                this.mnuTimkiem.Enabled = false;
                
            }
            else if (SqlHelper.loainguoidung == 3)
            {
                this.danhMụcToolStripMenuItem.Enabled = false;
                this.mnuHoadon.Enabled = false;
            }
            else if (SqlHelper.loainguoidung == 4)
            {
                this.mnuTimkiem.Enabled = false;
                this.mnuHoadon.Enabled = false;
                this.mnuKhachhang.Enabled = false;
                this.mnuNhanvien.Enabled = false;
            }
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Class.Functions.Disconnect(); //Đóng kết nối
            Application.Exit(); //Thoát
        }

        private void mnuNhaCC_Click(object sender, EventArgs e)
        {
            frmDMNhaCC frmNhaCC = new frmDMNhaCC();
            frmNhaCC.Show();
        }
           

        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDMNhanVien frmNhanVien = new frmDMNhanVien();
            frmNhanVien.Show();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDMKhachHang frmKhachhang = new frmDMKhachHang();
            frmKhachhang.Show();
        }

        private void hàngHóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDMDienThoai frmHanghoa = new frmDMDienThoai();
            frmHanghoa.Show();
        }

        private void hóaĐơnBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoadonBan frmHoadon = new frmHoadonBan();
            frmHoadon.Show(); 
        }

        private void mnuFindHoadon_Click(object sender, EventArgs e)
        {
            frmTimHDBan frmFindHDBan = new frmTimHDBan();
            frmFindHDBan.Show();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SqlHelper.con.State == ConnectionState.Open)
                SqlHelper.con.Close();
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void mnuLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            MessageBox.Show("Tạm biệt, hẹn gặp lại");
            SqlHelper.tennguoidung = "";
            SqlHelper.loainguoidung = -1;
            frmLogin f = new frmLogin();
            f.Show();
            
            
            
        }

      
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
      
    }
}
