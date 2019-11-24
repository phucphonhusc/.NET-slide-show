using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Model;
using WindowsFormsApplication1.Service;

namespace WindowsFormsApplication1
{
    public partial class frmContact : Form
    {
        string pathContact;
        string userName;

        public frmContact(string user)
        {
            InitializeComponent();
            this.CenterToScreen();
            pathContact = Application.StartupPath + @"\Data\contact.txt";
            userName = user;
            loadViewContact();
            

        }
        public void loadViewContact()
        {
            bdsContact.DataSource = null;
            dtgContact.AutoGenerateColumns = false;
            //var lsContact = ContactService.getContact(pathContact);
            var lsContact = ContactService.GetContactDB(userName);
            var lsContactSort = lsContact.OrderBy(a=>a.nameContact);
            if(lsContactSort != null)
            {
                bdsContact.DataSource = lsContactSort;
                
            }
            else
            {
                throw new Exception("Contact không tồn tại!");
            }
            dtgContact.DataSource = bdsContact;
            lblContact.Text = string.Format("Tổng mục danh bạ: {0}", lsContactSort.Count());
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            bdsContact.DataSource = null;
            dtgContact.AutoGenerateColumns = false;

            var listContact = ContactService.GetSearchContactDB(txtSearch.Text,userName);
            var listContactSort = listContact.OrderBy(a => a.nameContact);
            if (listContactSort == null)
            {
                throw new Exception("Không tồn tại contact này!");
            }
            else
            {
                bdsContact.DataSource = listContactSort;
            }

            dtgContact.DataSource = bdsContact;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var contact = bdsContact.Current as Contact;
            if (contact != null)
            {
                var f = new frmContactAdvanced(contact, pathContact, userName);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    loadViewContact();
 
                }
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            var frm = new frmContactAdvanced(null, pathContact, userName);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadViewContact();

            }
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            var contact = bdsContact.Current as Contact;
            if (contact != null)
            {
                var f = new frmContactAdvanced(contact, pathContact, userName);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    loadViewContact();

                }
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            var rs = MessageBox.Show("Bạn có chắc là muốn xóa contact này không?", "Thông báo", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);
            if (rs == DialogResult.OK)
            {
                var ct = (Contact)bdsContact.Current;
                var idCt = ct.idContact;
                ContactService.DeleteContactDB(idCt);
                loadViewContact();
                MessageBox.Show("Bạn đã xóa thành công");
            }
            else
            {
                MessageBox.Show("Bạn đã không xóa");
            }
        }
    }
}
