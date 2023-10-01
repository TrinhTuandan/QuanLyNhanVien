using Baitapbuoi6__EF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baitapbuoi6__EF
{
    public partial class frmQuanLyNhanSu : Form
    {
        NhanVienContextDB context;
        public frmQuanLyNhanSu()
        {
            InitializeComponent();
            context = new NhanVienContextDB();
        }
        private void frmQuanLyNhanSu_Load(object sender, EventArgs e)
        {
            cboPhongBan.SelectedIndex = 0;
            try
            {
                List<PhongBan> listFalcultys = context.PhongBan.ToList(); //lây các khoa
                List<NhanVien> listStudent = context.NhanVien.ToList(); //lây sinh viên
                FillFalcultyCombobox(listFalcultys);
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Hàm binding list có tên hiển thị là tên khoa, giá trị là Mã khoa - Phương thức FillFalcultyCombobox được sử dụng để đổ danh sách khoa vào combobox cmdKhoa.
        private void FillFalcultyCombobox(List<PhongBan> listFalcultys)
        {
            cboPhongBan.DataSource = listFalcultys;
            cboPhongBan.ValueMember = "MaPB";
            cboPhongBan.DisplayMember = "TenPB";
        }
        //Phương thức BindGrid được sử dụng để hiển thị danh sách sinh viên lên DataGridView 
        private void BindGrid(List<NhanVien> listNhanVien)
        {
            dgvDSNV.Rows.Clear();
            foreach (var item in listNhanVien)
            {
                int index = dgvDSNV.Rows.Add();
                dgvDSNV.Rows[index].Cells[0].Value = item.MaNV;
                dgvDSNV.Rows[index].Cells[1].Value = item.TenNV;
                dgvDSNV.Rows[index].Cells[2].Value = item.NgaySinh;
                dgvDSNV.Rows[index].Cells[3].Value = item.PhongBan.TenPB;
            }
        }
        //click DataGridView
        private void dgvDSNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDSNV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dgvDSNV.CurrentRow.Selected = true;

                    txtMaNV.Text = dgvDSNV.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    txtTenNV.Text = dgvDSNV.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                    dtpNgaySinh.Value = Convert.ToDateTime(dgvDSNV.Rows[e.RowIndex].Cells[2].FormattedValue);
                    cboPhongBan.SelectedIndex = cboPhongBan.FindString(dgvDSNV.Rows[e.RowIndex].Cells[3].FormattedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-------------------------------------------------------------------------------------------------------
        //Đây là một phương thức dùng để tìm dòng được chọn trong bảng dữ liệu (dgvDSNV) dựa trên MSSV (Mã số sinh viên) được truyền vào.
        private int GetSeclectedRow(string MSSV)
        {
            for (int i = 0; i < dgvDSNV.Rows.Count; i++)
            {
                if (dgvDSNV.Rows[i].Cells[0].Value != null)
                {
                    if (dgvDSNV.Rows[i].Cells[0].Value.ToString() == MSSV)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        // check validate
        private bool CheckValidate()
        {
            if (txtMaNV.Text == "" || txtTenNV.Text == "")
            {
                MessageBox.Show("Vui long Nhập Đây Đủ Thông Tin Nhân Viên!", "Thông Báo", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        //Load lại thông tin trên dataGridview
        private void reloadDGV()
        {
            List<NhanVien> ListStudents = context.NhanVien.ToList();
            BindGrid(ListStudents);
        }

        // Refresh các ô nhập dữ liệu
        private void Refresh()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            cboPhongBan.SelectedIndex = -1;
        }
        // Thêm Nhân Viên
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckValidate())
            {
                string MaNV = txtMaNV.Text.Trim();
                int rowIndex = GetSeclectedRow(MaNV);
                if (rowIndex == -1)
                {
                    NhanVien newNhanVien = new NhanVien
                    {
                        MaNV = txtMaNV.Text,
                        TenNV = txtTenNV.Text,
                        NgaySinh = dtpNgaySinh.Value,
                        MaPB = cboPhongBan.SelectedValue.ToString(),
                        
                    };
                    context.NhanVien.Add(newNhanVien);
                    context.SaveChanges();

                    reloadDGV();
                    Refresh();
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Nhân viên đã tồn tại!", "Thông Báo", MessageBoxButtons.OK);
                }
            }
        }

        // Sửa 1 Dòng trong dataGridview
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (CheckValidate())
            {
                string MaNV = txtMaNV.Text.Trim();
                int rowIndex = GetSeclectedRow(MaNV);
                if (rowIndex != -1)
                {
                    NhanVien dbUpdate = context.NhanVien.FirstOrDefault(nv => nv.MaNV == MaNV);
                    if (dbUpdate != null)
                    {
                        dbUpdate.TenNV = txtTenNV.Text;
                        dbUpdate.NgaySinh = dtpNgaySinh.Value;
                        dbUpdate.MaPB = cboPhongBan.SelectedValue.ToString();

                        context.SaveChanges(); // Lưu thay đổi

                        reloadDGV();
                        Refresh();
                        MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy Nhân viên cần sửa!", "Thông Báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Nhân viên không tồn tại!", "Thông Báo", MessageBoxButtons.OK);
                }
            }
        }


        // Xoa 1 Dong trong dataGridview
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaNV = txtMaNV.Text.Trim();
            int rowIndex = GetSeclectedRow(MaNV);
            if (rowIndex != -1)
            {
                NhanVien dbDelete = context.NhanVien.FirstOrDefault(nv => nv.MaNV == MaNV);
                if (dbDelete != null)
                {
                    context.NhanVien.Remove(dbDelete);
                    context.SaveChanges();
                    reloadDGV();
                    Refresh();
                    MessageBox.Show("Xóa Nhân viên thành công!", "Thông Báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Nhân viên cần xóa!", "Thông Báo", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Nhân viên không tồn tại!", "Thông Báo", MessageBoxButtons.OK);
            }
        }
        //Thoát Khỏi Form
        private void btnThoat_Click(object sender, EventArgs e)
        {        
            if (MessageBox.Show("Bạn có muốn Thoát không", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        // Refresh các ô nhập dữ liệu
        private void btnXoaThongTin_Click(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
