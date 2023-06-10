﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using System.IO;
using BUS;
using DTO;
using System.Drawing.Printing;
using System.Reflection.Metadata;
using iTextSharp.text;
using DAO;

namespace GUI
{
    public partial class FMainManager : Form
    {
        System.Drawing.Image imgBackgroundChoose = null;  // Variable that store background image
        public BindingSource bs_khachhang, bs_nhanvien, bs_baigiu, bs_loaixe;
        private NhanVien nhanvien = new NhanVien();

        public NhanVien Nhanvien { get => nhanvien; set => nhanvien = value; }
        public FMainManager()
        {
            InitializeComponent();
            bs_khachhang = new BindingSource();
            bs_nhanvien = new BindingSource();
            bs_baigiu = new BindingSource();
            bs_loaixe = new BindingSource();
        }
       
        private void FMainManager_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            CenterTheBox();
            changeVisible(pnl_default);
            // Set background of label to transparent
            lb_qlnv.Parent = pnl_qlnv;
            lb_qlbx.Parent = pnl_qlbx;
            lb_qlkh.Parent = pnl_qlkh;
            lb_lsrv.Parent = pnl_lsrv;
            lb_bcdt.Parent = pnl_bcdt;
            lb_qlnv.BackColor = Color.Transparent;
            lb_qlbx.BackColor = Color.Transparent;
            lb_qlkh.BackColor = Color.Transparent;
            lb_lsrv.BackColor = Color.Transparent;
            lb_bcdt.BackColor = Color.Transparent;
            this.imgBackgroundChoose = Properties.Resources.background_change;
            loadCustomer();
            loadStaff();
            loadBaiGiu();
            loadLoaiXe();

        }
        private void FMainManager_Resize(object sender, EventArgs e)
        {
            CenterTheBox();
        }
        // Function to center delete and update button in tab QuanLyKhachHang
        private void CenterTheBox()
        {
            var left_update = pnl_bottom.ClientRectangle.Width / 2 - (pb_delete_qlkh.ClientRectangle.Width + 30);
            var left_delete = pnl_bottom.ClientRectangle.Width / 2 + 30;
            var left_add = pnl_bottom_qlnv.ClientRectangle.Width / 2 - (pb_add_qlnv.ClientRectangle.Width / 2);
            var left_update_qlnv = pnl_bottom_qlnv.ClientRectangle.Width / 2 - (pb_add_qlnv.ClientRectangle.Width + pb_add_qlnv.ClientRectangle.Width / 2 + 30);
            var left_delete_qlnv = pnl_bottom_qlnv.ClientRectangle.Width / 2 + (pb_add_qlnv.ClientRectangle.Width / 2 + 30);
            var top = (pnl_bottom.ClientRectangle.Height - pb_delete_qlkh.ClientRectangle.Height) / 2;
            pb_update_qlkh.Location = new Point(pnl_bottom.ClientRectangle.Location.X + left_update, pnl_bottom.ClientRectangle.Location.Y + top);
            pb_delete_qlkh.Location = new Point(pnl_bottom.ClientRectangle.Location.X + left_delete, pnl_bottom.ClientRectangle.Location.Y + top);
            pb_add_qlnv.Location = new Point(pnl_bottom.ClientRectangle.Location.X + left_add, pnl_bottom.ClientRectangle.Location.Y + top);
            pb_update_qlnv.Location = new Point(pnl_bottom.ClientRectangle.Location.X + left_update_qlnv, pnl_bottom.ClientRectangle.Location.Y + top);
            pb_delete_qlnv.Location = new Point(pnl_bottom.ClientRectangle.Location.X + left_delete_qlnv, pnl_bottom.ClientRectangle.Location.Y + top);
            pb_add_qlbx.Location = new Point(pnl_bottom.ClientRectangle.Location.X + left_add, pnl_bottom.ClientRectangle.Location.Y + top);
            pb_update_qlbx.Location = new Point(pnl_bottom.ClientRectangle.Location.X + left_update_qlnv, pnl_bottom.ClientRectangle.Location.Y + top);
            pb_remove_qlbx.Location = new Point(pnl_bottom.ClientRectangle.Location.X + left_delete_qlnv, pnl_bottom.ClientRectangle.Location.Y + top);
            pb_add_lx.Location = new Point(pnl_bottom.ClientRectangle.Location.X + left_add, pnl_bottom.ClientRectangle.Location.Y + top);
            pb_update_lx.Location = new Point(pnl_bottom.ClientRectangle.Location.X + left_update_qlnv, pnl_bottom.ClientRectangle.Location.Y + top);
            pb_remove_lx.Location = new Point(pnl_bottom.ClientRectangle.Location.X + left_delete_qlnv, pnl_bottom.ClientRectangle.Location.Y + top);
        }

        // Function to change visible attribute to false
        private void changeVisible(Panel pnl=null)
        {
            pnl_default.Visible = false;
            pnl_control_qlkh.Visible = false;
            pnl_control_qlnv.Visible = false;
            pnl_control_bcdt.Visible = false;
            pnl_control_qlbx.Visible = false;
            pnl_qlbx_qlbx.Visible = false;
            pnl_lx_qlbx.Visible = false;
            pnl_control_lsrv.Visible = false;
            if (pnl != null)
            {
                pnl.Visible = true;
            }
        }

        // Function to change background of enable panel
        private void changeBackgroundPanel(Panel pnl) 
        {
            // Change background image of all panel to none value
            pnl_qlnv.BackgroundImage = base.BackgroundImage;
            pnl_qlkh.BackgroundImage = base.BackgroundImage;
            pnl_qlbx.BackgroundImage = base.BackgroundImage;
            pnl_lsrv.BackgroundImage = base.BackgroundImage;
            pnl_bcdt.BackgroundImage = base.BackgroundImage;    
            // Change background image of the pnl that clicked
            pnl.BackgroundImage = imgBackgroundChoose;
            pnl.BackgroundImageLayout = ImageLayout.Stretch;
        }

      
        // QLNV Functions
        private void pnl_qlnv_Click(object sender, EventArgs e)
        {
            changeBackgroundPanel((Panel)sender);
            changeVisible(pnl_control_qlnv);
        }
        private void lb_qlnv_Click(object sender, EventArgs e)
        {
            changeBackgroundPanel(pnl_qlnv);
            changeVisible(pnl_control_qlnv);

        }
        private void pb_add_qlnv_Click(object sender, EventArgs e)
        {
            FAddEditStaff f = new FAddEditStaff(this);
            f.Text = "Thêm nhân viên mới";
            f.Show();
        }
        private void pb_update_qlnv_Click(object sender, EventArgs e)
        {
            FAddEditStaff f = new FAddEditStaff(this, false);
            f.Text = "Thay đổi thông tin nhân viên";
            f.Show();
        }
        private void pb_delete_qlnv_Click(object sender, EventArgs e)
        {
            if (dgv_qlnv.CurrentRow.Cells["SODT_NV"].Value != DBNull.Value)
            {
                if (MessageBox.Show("Bạn có chắc chắn xóa nhân viên này không?", "Xóa nhân viên", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (dgv_qlnv.CurrentRow != null)
                    {
                        string sdt = dgv_qlnv.CurrentRow.Cells["SODT_NV"].Value.ToString();
                        bool check = BUS.NhanVienBUS.Instance.deleteAccountNhanVien(sdt);
                        if (check)
                        {
                            loadStaff();
                        }
                    }
                }
            }
        }
        // QLBX Functions
        private void pnl_qlbx_Click(object sender, EventArgs e)
        {
            changeBackgroundPanel((Panel)sender);
            changeVisible(pnl_control_qlbx);
        }
        private void lb_qlbx_Click(object sender, EventArgs e)
        {
            changeBackgroundPanel(pnl_qlbx);
            changeVisible(pnl_control_qlbx);
        }
        private void pb_add_qlbx_Click(object sender, EventArgs e)
        {
            FAddEditParking f = new FAddEditParking(this);
            f.Show();
        }
        private void pb_update_qlbx_Click(object sender, EventArgs e)
        {
            FAddEditParking f = new FAddEditParking(this, false);
            f.Show();
        }
        private void pb_remove_qlbx_Click(object sender, EventArgs e)
        {
            if (dgv_qlbx_qlbx.CurrentRow.Cells["MABAI"].Value.ToString() != null)
            {
                int mabai = (int)dgv_qlbx_qlbx.CurrentRow.Cells["MABAI"].Value;
                if (MessageBox.Show("Bạn có chắc chắn xóa bãi xe này không?", "Xóa bãi xe", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool checkRemove = BUS.BaiGiuBUS.Instance.removeBaiGiu(mabai);
                    if (checkRemove)
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo");
                        this.loadBaiGiu();

                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công", "Thông báo");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại xe muốn xóa");
            }
        }
        private void pb_bx_qlbx_Click(object sender, EventArgs e)
        {
            pnl_lx_qlbx.Visible = false;
            pnl_qlbx_qlbx.Visible = true;
        }
        
        private void pb_lx_qlbx_Click(object sender, EventArgs e)
        {
            pnl_qlbx_qlbx.Visible = false;
            pnl_lx_qlbx.Visible = true;
        }
        private void pb_add_lx_Click(object sender, EventArgs e)
        {
            FAddEditCarType f = new FAddEditCarType(this);
            f.Show();
        }
        

        private void pb_update_lx_Click(object sender, EventArgs e)
        {
            FAddEditCarType f = new FAddEditCarType(this, false);
            f.Show();
        }

        private void pb_remove_lx_Click(object sender, EventArgs e)
        {
            if(dgv_lx_qlbx.CurrentRow.Cells["MALOAI"].Value.ToString() != null)
            {
                string maloai = dgv_lx_qlbx.CurrentRow.Cells["MALOAI"].Value.ToString();
                if (MessageBox.Show("Bạn có chắc chắn xóa loại xe này không?", "Xóa loại xe", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool checkRemove = BUS.LoaiXeBUS.Instance.removeLoaiXe(maloai);
                    if(checkRemove)
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo");
                        this.loadLoaiXe();

                    } else
                    {
                        MessageBox.Show("Xóa không thành công", "Thông báo");
                    }
                }
            } else
            {
                MessageBox.Show("Vui lòng chọn loại xe muốn xóa");
            }
        }
        // QLKH Functions
        private void pnl_qlkh_Click(object sender, EventArgs e)
        {   
            changeBackgroundPanel((Panel)sender);
            changeVisible(pnl_control_qlkh);

        }
        private void lb_qlkh_Click(object sender, EventArgs e)
        {
            changeBackgroundPanel(pnl_qlkh);
            changeVisible(pnl_control_qlkh);
        }
        private void pb_update_qlkh_Click(object sender, EventArgs e)
        {
            FEditCustomer f = new FEditCustomer(this);
            f.Show();
        }
        private void pb_delete_qlkh_Click(object sender, EventArgs e)
        {
            if (dgv_qlkh.CurrentRow.Cells["SODT"].Value != DBNull.Value)
            {
                if (MessageBox.Show("Bạn có chắc chắn xóa khách hàng này không?", "Xóa khách hàng", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool check = BUS.KhachHangBUS.Instance.deleteAccount(dgv_qlkh.CurrentRow.Cells["SODT"].Value.ToString());
                    if (check)
                    {
                        loadCustomer();
                    }
                }
            }
        }
        // LSRV Functions
        private void pnl_lsrv_Click(object sender, EventArgs e)
        {
            changeBackgroundPanel((Panel)sender);
            changeVisible(pnl_control_lsrv);
        }
        private void lb_lsrv_Click(object sender, EventArgs e)
        {
            changeBackgroundPanel(pnl_lsrv);
            changeVisible(pnl_control_lsrv);
        }

        // BCDT Functions
        private void pnl_bcdt_Click(object sender, EventArgs e)
        {
            changeBackgroundPanel((Panel)sender);
            changeVisible(pnl_control_bcdt);
        }
        private void lb_bcdt_Click(object sender, EventArgs e)
        {
            changeBackgroundPanel(pnl_bcdt);
            changeVisible(pnl_control_bcdt);
        }

        // Logout Functions
        private void pnl_dangxuat_Click(object sender, EventArgs e)
        {
            this.Close();
            FLogin f = new FLogin();
            f.Show();
        }

        private void lb_dangxuat_Click(object sender, EventArgs e)
        {
            this.Close();
            FLogin f = new FLogin();
            f.Show();
        }


       

       



        // Function that load all customer in database
        public void loadCustomer()
        {
            
            DataTable dt = new DataTable();
            dt = BUS.KhachHangBUS.Instance.getAllCustomer();
            bs_khachhang.DataSource = dt;
            dgv_qlkh.DataSource = bs_khachhang;
        }

       





        // Function that load all staff in database
        public void loadStaff()
        {
            DataTable dt = new DataTable();
            dt = BUS.NhanVienBUS.Instance.getAllStaff();
            DataTable dtCloned = dt.Clone();
            dtCloned.Columns[4].DataType = typeof(String);
            foreach (DataRow row in dt.Rows)
            {
                dtCloned.ImportRow(row);
            }
            IDictionary<int, string> loainvDict = new Dictionary<int, string>() { { 0, "Nhân viên bãi" }, { 1, "Quản lý"} };
            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                dtCloned.Rows[rowIndex][4] = loainvDict[Convert.ToInt32(dt.Rows[rowIndex][4])];
            }
            bs_nhanvien.DataSource = dtCloned;
            dgv_qlnv.DataSource = bs_nhanvien; 
        }

        private void dtp_start_bcdt_ValueChanged(object sender, EventArgs e)
        {
            loadHoaDon();
        }

        private void dtp_end_bcdt_ValueChanged(object sender, EventArgs e)
        {
            loadHoaDon();
        }

        // Function that load all parking in database
        public void loadBaiGiu()
        {
            DataTable dt = new DataTable();
            dt = BUS.BaiGiuBUS.Instance.getAllBaiGiu();
            bs_baigiu.DataSource = dt;
            dgv_qlbx_qlbx.DataSource = bs_baigiu;
        }

        private void pb_print_bcdt_Click(object sender, EventArgs e)
        {
            
            printPDF(); 
        }

        private DataGridView CopyDataGridView(DataGridView dgv_org)
        {
            DataGridView dgv_copy = new DataGridView();
            try
            {
                if (dgv_copy.Columns.Count == 0)
                {
                    foreach (DataGridViewColumn dgvc in dgv_org.Columns)
                    {
                        dgv_copy.Columns.Add(dgvc.Clone() as DataGridViewColumn);
                    }
                }

                DataGridViewRow row = new DataGridViewRow();

                for (int i = 0; i < dgv_org.Rows.Count; i++)
                {
                    row = (DataGridViewRow)dgv_org.Rows[i].Clone();
                    int intColIndex = 0;
                    foreach (DataGridViewCell cell in dgv_org.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    dgv_copy.Rows.Add(row);
                }
                dgv_copy.AllowUserToAddRows = false;
                dgv_copy.Refresh();

            }
            catch (Exception ex)
            {
                //cf.ShowExceptionErrorMsg("Copy DataGridViw", ex);
            }
            return dgv_copy;
        }

        // Function that load all car type in database
        public void loadLoaiXe()
        {
            DataTable dt = new DataTable();
            dt = BUS.LoaiXeBUS.Instance.getAllLoaiXeDT();
            bs_loaixe.DataSource = dt;
            dgv_lx_qlbx.DataSource = bs_loaixe;
        }
        

        public void loadHoaDon()
        {
            if (dtp_start_bcdt.Value > dtp_end_bcdt.Value)
            {
                MessageBox.Show("Ngày kết thúc không hợp lệ");
                return;
            }
            string start = dtp_start_bcdt.Value.ToString("yyyy-MM-dd");
            string end = dtp_end_bcdt.Value.AddDays(1).ToString("yyyy-MM-dd");
            DataTable dt = BUS.HoaDonBUS.Instance.getAllHoaDon(start, end);


            List<HoaDon> lisst = HoaDonDAO.Instance.gethdlist(start, end);
            BindingList<HoaDon> bindingList = new BindingList<HoaDon>(lisst);
            dgv_listmonan.DataSource = bindingList;


            dgv_dshd.DataSource = dt;
            tb_sohoadon.Text = BUS.HoaDonBUS.Instance.getSoLuongHoaDonTheoNgay(start, end).ToString();
            tb_tongtien.Text = BUS.HoaDonBUS.Instance.getTongTien(start, end).ToString();
        }
      

        public void printPDF( )
        {
            if (dgv_listmonan.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (.pdf)|.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Không thể ghi dữ liệu tới ổ đĩa. Mô tả lỗi:" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dgv_listmonan.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;

                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;


                            pdfTable.SpacingBefore = 10f; // set the margin before the table
                            pdfTable.SpacingAfter = 10f;

                            foreach (DataGridViewColumn column in dgv_listmonan.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }
                            int count = dgv_listmonan.Rows.Count-1;
                            int i = 0;
                            foreach (DataGridViewRow row in dgv_listmonan.Rows)
                            {
                               
                                foreach (DataGridViewCell cell in row.Cells)
                                {

                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                                i++;
                                if (count == i)
                                {
                                    break;
                                }
                                
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                String text1 = "Chi tiết doanh thu của bãi với số đơn: " + tb_sohoadon.Text.ToString() ;
                                Paragraph para = new Paragraph(text1);


                                pdfDoc.Add(para);
                                pdfDoc.Add(pdfTable);
                                String tongtien = "Tổng tiền :" + tb_tongtien.Text.ToString();
                                Paragraph para1 = new Paragraph(tongtien);
                                pdfDoc.Add(para1);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Dữ liệu Export thành công!!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Mô tả lỗi :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có bản ghi nào được Export!!!", "Info");
            }
        }
    }
    }
    

  


        
 

