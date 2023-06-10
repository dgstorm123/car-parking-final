﻿using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GUI
{
    public partial class FEditCustomer : Form
    {
        FMainManager fmm;
        static string sdtcu = "";
        public FEditCustomer(FMainManager fmm)
        {
            InitializeComponent();
            this.fmm = fmm;
        }

        
        private void FEditCustomer_Load(object sender, EventArgs e)
        {
            pb_update.Parent = pb_background;
            lb_validate_phone.Parent = pb_background;
            lb_validate_name.Parent = pb_background;
            lb_validate_pass.Parent = pb_background;
            pb_update.BackColor = Color.Transparent;
            DataRowView rowSelect = (DataRowView) fmm.bs_khachhang.Current;   // Get the row that is selected in Form Main
            tb_phone.Text = rowSelect["SODT"].ToString();
            sdtcu = rowSelect["SODT"].ToString();
            tb_name.Text = rowSelect["TENKH"].ToString();
            tb_pass.Text = rowSelect["PASS"].ToString();
            tb_point.Text = rowSelect["DIEMTICHLUY"].ToString();
        }

        public const string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        public static bool IsPhoneNbr(string number)
        {
            if (number != null) return Regex.IsMatch(number, motif);
            else return false;
        }

        private void pb_update_Click(object sender, EventArgs e)
        {

            if(!IsPhoneNbr(tb_phone.Text.ToString()) || tb_phone.Text.ToString().Length <= 9)
            {
                lb_validate_phone.Text = "Số điện thoại nhập chưa đúng!";
                tb_phone.Focus();     // Focus on input textbox for user
                return;
            }

            lb_validate_phone.Text = lb_validate_name.Text = lb_validate_pass.Text = "";
            // validating when click on button "Dang Nhap"
            if (tb_phone.Text == "")   // Show message to user
            {
                lb_validate_phone.Text = "Số điện thoại chưa nhập!";
                tb_phone.Focus();     // Focus on input textbox for user
                return;
            }
            if (tb_name.Text == "")    // Show message to user
            {
                lb_validate_name.Text = "Họ và tên chưa nhập!";
                tb_name.Focus();
                return;
            }
            if (tb_pass.Text == "")    // Show message to user
            {
                lb_validate_pass.Text = "Mật khẩu chưa nhập!";
                tb_pass.Focus();
                return;
            }
            

            // validating user account
            this.ActiveControl = null;
            String sdt = tb_phone.Text.ToString();
            String name = tb_name.Text.ToString();
            int diemtl = Convert.ToInt16(tb_point.Text.ToString());
            String pass = tb_pass.Text.ToString();
            if (sdtcu != sdt && KhachHangBUS.Instance.checkExistAccount(sdt))
            {
                lb_validate_phone.Text = "Số điện thoại tồn tại!";
                return;
            }
            bool check = KhachHangBUS.Instance.updateAccountKhachHang(sdtcu, sdt, name, pass, diemtl);
            if (check)
            {
                MessageBox.Show("Cập nhật thành công");
                this.Hide();
                fmm.loadCustomer();


            }
            else
            {
                MessageBox.Show("Cập nhật không thành công");
            }
        }

    }
}
