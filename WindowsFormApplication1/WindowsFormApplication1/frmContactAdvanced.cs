using System;
using WindowsFormsApplication1.Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Model;

namespace WindowsFormsApplication1
{
    public partial class frmContactAdvanced : Form
    {
        Contact contact;
        string pathContact;
        String userName;
        public frmContactAdvanced(Contact contact = null,string pathContactFile=null ,string userName = null)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.contact = contact;
            this.pathContact = pathContactFile;
            this.userName = userName;
            if (contact != null)
            {
                this.Text = "Chỉnh sửa liên lạc";
                txtName.Text = contact.nameContact;
                txtPhone.Text = contact.phoneContact;
                txtEmail.Text = contact.emailContact;


            }
            else
            {
                this.Text = "Thêm liên lạc";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (contact != null)
            {
                var name = txtName.Text;
                var phone = txtPhone.Text;
                var email = txtEmail.Text;
                ContactService.EditContactDB(contact.idContact, name, phone, email);
            }
            else
            {
                var name = txtName.Text;
                var phone = txtPhone.Text;
                var email = txtEmail.Text;
                ContactService.AddContactDB( name, phone, email, userName);
            }
            MessageBox.Show("Đã cập nhật dữ liệu thành công");
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
