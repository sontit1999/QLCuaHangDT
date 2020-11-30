using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using _206_215_qtm18c.Class;


namespace _206_215_qtm18c
{
    public partial class frmDMNhanVien : Form
    {
        DataTable tblNV;
        public frmDMNhanVien()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public static DataTable DataSource { get; set; }

        private void frmDMNhanVien_Load(object sender, EventArgs e)
        {
            txtManhanvien.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            // hiển thị data lên datagridview
            LoadDataGridView();
        }

        public void LoadDataGridView()
        {   
            // truy vấn vào csdl lấy ds nhan viên và hiển thị lên datagridview
            string sql;
            sql = "SELECT Manhanvien,Tennhanvien,Gioitinh,Diachi,Dienthoai,Ngaysinh FROm tblNhanvien";
            tblNV = Functions.GetDataToTable(sql); //lấy dữ liệu
            DataGridView.DataSource = tblNV;
            DataGridView.Columns[0].HeaderText = "Mã nhân viên";
            DataGridView.Columns[1].HeaderText = "Tên nhân viên";
            DataGridView.Columns[2].HeaderText = "Giới tính";
            DataGridView.Columns[3].HeaderText = "Địa chỉ";
            DataGridView.Columns[4].HeaderText = "Điện thoại";
            DataGridView.Columns[5].HeaderText = "Ngày sinh";
            DataGridView.Columns[0].Width = 100;
            DataGridView.Columns[1].Width = 150;
            DataGridView.Columns[2].Width = 100;
            DataGridView.Columns[3].Width = 150;
            DataGridView.Columns[4].Width = 100;
            DataGridView.Columns[5].Width = 100;
            DataGridView.AllowUserToAddRows = false;
            DataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void DataGridView_Click(object sender, EventArgs e)
        {   
            // sự kiện khi datagricview click
            // kiểm tra button thêm có bị vô hiệu hóa hay ko ?
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtManhanvien.Focus();
                return;
            }
            // kiểm tra số hàng = 0 hay ko ?
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtManhanvien.Text = DataGridView.CurrentRow.Cells["Manhanvien"].Value.ToString();
            txtTennhanvien.Text = DataGridView.CurrentRow.Cells["Tennhanvien"].Value.ToString();
            if (DataGridView.CurrentRow.Cells["Gioitinh"].Value.ToString() == "Nam") chkGioitinh.Checked = true;
            else chkGioitinh.Checked = false;
            txtDiachi.Text = DataGridView.CurrentRow.Cells["Diachi"].Value.ToString();
            mskDienthoai.Text = DataGridView.CurrentRow.Cells["Dienthoai"].Value.ToString();
            mskNgaysinh.Text = DataGridView.CurrentRow.Cells["Ngaysinh"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {  
            
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtManhanvien.Enabled = true;
            txtManhanvien.Focus();
        }

        private void ResetValues()
        {
            // reset value các ô textbox
            txtManhanvien.Text = "";
            txtTennhanvien.Text = "";
            chkGioitinh.Checked = false;
            txtDiachi.Text = "";
            mskNgaysinh.Text = "";
            mskDienthoai.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {    
            // lưu nhân viên vào csdl
            string sql, gt;
            // ktra mã nhân viên rỗng hay ko ?
            if (txtManhanvien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtManhanvien.Focus();
                return;
            }
            // ktra tên nhân viên rỗng hay ko ?
            if (txtTennhanvien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTennhanvien.Focus();
                return;
            }
            // tương tự ...
            if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiachi.Focus();
                return;
            }
            // tương tự ...
            if (mskDienthoai.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskDienthoai.Focus();
                return;
            }
            // tương tự ...
            if (mskNgaysinh.Text == "  /  /")
            {
                MessageBox.Show("Bạn phải nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaysinh.Focus();
                return;
            }
            // kiểm tra đúng định dạng ngày hay chưa.
            if (!Functions.IsDate(mskNgaysinh.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // mskNgaysinh.Text = "";
                mskNgaysinh.Focus();
                return;
            }
            if (chkGioitinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            // ktra tồn tại nhân viên có mã này trong csdl hay chưa
            sql = "SELECT Manhanvien FROM tblNhanvien WHERE Manhanvien=N'" + txtManhanvien.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtManhanvien.Focus();
                txtManhanvien.Text = "";
                return;
            }
            //  thêm nhân viên vào csdl
            sql = "INSERT INTO tblNhanvien(Manhanvien,Tennhanvien,Gioitinh, Diachi,Dienthoai, Ngaysinh) VALUES (N'" + txtManhanvien.Text.Trim() + "',N'" + txtTennhanvien.Text.Trim() + "',N'" + gt + "',N'" + txtDiachi.Text.Trim() + "','" + mskDienthoai.Text + "','" + Functions.ConvertDateTime(mskNgaysinh.Text) + "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtManhanvien.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {  
            string sql, gt;
            // ktra số hàng = 0 hay ko ?
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // ktra mã nhân viên rỗng hay ko ?
            if (txtManhanvien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // tương tự ...
            if (txtTennhanvien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTennhanvien.Focus();
                return;
            }
            // tương tự ...
            if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiachi.Focus();
                return;
            }
            // tương tự ...
            if (mskDienthoai.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskDienthoai.Focus();
                return;
            }
            // tương tự ...
            if (mskNgaysinh.Text == "  /  /")
            {
                MessageBox.Show("Bạn phải nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaysinh.Focus();
                return;
            }
            // tương tự ...
            if (!Functions.IsDate(mskNgaysinh.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaysinh.Text = "";
                mskNgaysinh.Focus();
                return;
            }
            // ktra giới tính nào dc chọn
            if (chkGioitinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            // cập nhật lại thông tin nhân viên vào cơ sở dữ liệu
            sql = "UPDATE tblNhanvien SET  Tennhanvien=N'" + txtTennhanvien.Text.Trim().ToString() +
                    "',Diachi=N'" + txtDiachi.Text.Trim().ToString() +
                    "',Dienthoai='" + mskDienthoai.Text.ToString() + "',Gioitinh=N'" + gt +
                    "',Ngaysinh='" + Functions.ConvertDateTime(mskNgaysinh.Text) +
                    "' WHERE Manhanvien=N'" + txtManhanvien.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            // ktra số hàng = 0 hay ko ?
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // ktra mã nhân viên rỗng hay ko ?
            if (txtManhanvien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // hieernt hị thông báo xác nhận xóa hay ko
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblNhanvien WHERE Manhanvien=N'" + txtManhanvien.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {    
            // reset vallue control và vô hiệu hóa , kích hoạt button
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtManhanvien.Enabled = false;
        }

       

        

       
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtManhanvien_KeyUp(object sender, KeyEventArgs e)
        {   
            // ktra phím enter có dc nhấn hayt ko ?
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTennhanvien_KeyUp(object sender, KeyEventArgs e)
        {  
            // ktra phím enter có dc nhấn hayt ko ?
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtDiachi_KeyUp(object sender, KeyEventArgs e)
        {
            // ktra phím enter có dc nhấn hayt ko ?
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mskDienthoai_KeyUp(object sender, KeyEventArgs e)
        {
            // ktra phím enter có dc nhấn hayt ko ?
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mskNgaysinh_KeyUp(object sender, KeyEventArgs e)
        {
            // ktra phím enter có dc nhấn hayt ko ?
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        


        
    }
}
