﻿using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KhachSan
{
    public partial class frmAddUser : Form
    {
        private int cmnd;
        public frmAddUser(int cmnd)
        {
            this.cmnd = cmnd;

            InitializeComponent();
        }
        #region Hàm gọi MessageBox
        public void Alert(string msg, frmAlert.Type type)
        {
            Thread aleart = new Thread(new ThreadStart(() => Application.Run(new frmAlert(msg, type))));
            aleart.Start();
        }
        #endregion



        private void frmAddAcc_Load(object sender, EventArgs e)
        {
            botro.ApplyElipse(txtCMND, 40);
            if (cmnd != 0)
            {
                DTO_User u = BUS_User.GetUser(cmnd);
                txtCMND.Text = u.CMND.ToString();
                txtHoTen.Text = u.HoTen.ToString();
                txtPhone.Text = u.SDT.ToString();
                txtDiaChi.Text = u.DiaChi.ToString();
                ngaysinhpicker.Value = u.NgaySinh;
                if (u.Sex)
                {
                    checkNam.Checked = true;
                }
                else
                {
                    checkNu.Checked = true;
                }
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            bool check = true;
            try
            {
                if (txtCMND.Text.Length != 9)
                {
                    check = false;
                    Alert("Chứng minh không hợp lệ", frmAlert.Type.Info);
                }
                if ((cmnd == 0) && (BUS_User.GetAllUser().Find(p => p.CMND == Int32.Parse(txtCMND.Text)) != null))
                {

                    check = false;
                    Alert("Đã tồn tại người này trong CSDL", frmAlert.Type.Info);
                }
                if (txtPhone.Text.Length != 10)
                {
                    check = false;
                    Alert("Số di dộng không hợp lệ! ", frmAlert.Type.Info);
                }
                if (checkNam.Checked == false && checkNu.Checked == false)
                {
                    check = false;
                    Alert("Chưa chọn giới tính", frmAlert.Type.Info);
                }

                if (check)
                {


                    DTO_User u = new DTO_User(
                                              Int32.Parse(txtCMND.Text),
                                              txtHoTen.Text,
                                              ngaysinhpicker.Value,
                                              checkNam.Checked ? true : false,
                                              txtPhone.Text,
                                              txtDiaChi.Text);
                    if (cmnd == 0)
                    {

                        BUS_User.InsertUpdate(u);
                        Alert("Đã thêm thành công", frmAlert.Type.Success);
                    }
                    else
                    {
                        BUS_User.InsertUpdate(u);
                        Alert("Đã sửa thành công ^^", frmAlert.Type.Success);
                    }

                }
            }
            catch (Exception)
            {

                Alert("Đã xảy ra lỗi", frmAlert.Type.Error);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            Dispose();
        }

        private void checkNam_OnChange(object sender, EventArgs e)
        {
            if (checkNam.Checked)
            {
                checkNu.Checked = false;
            }
        }

        private void checkNu_OnChange(object sender, EventArgs e)
        {
            if (checkNu.Checked)
            {
                checkNam.Checked = false;
            }
        }
    }

}
