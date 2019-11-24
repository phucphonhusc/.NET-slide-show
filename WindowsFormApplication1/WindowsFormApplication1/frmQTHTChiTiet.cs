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
    
    
    public partial class frmQTHTChiTiet : Form
    {
        string pathQTHT;
        QTHT qtht;
        public frmQTHTChiTiet(QTHT qtht= null, string pathQTHT = null)
        {
            InitializeComponent();
            this.qtht = qtht;
            this.pathQTHT = pathQTHT;
            if(qtht != null)
            {
                this.Text = "Chỉnh sửa quá trình học tập";
                numTuNam.Value = qtht.FromYear;
                numDenNam.Value = qtht.ToYear;
                txtNoiHoc.Text = qtht.SchoolName;
            }
            else
            {
                this.Text = "Thêm quá trình học tập";
            }
        }


        private void btnBoQua_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDongY_Click_1(object sender, EventArgs e)
        {
            if (qtht != null)
            {
                var tunam = (int)numTuNam.Value;
                var dennam = (int)numDenNam.Value;
                var noihoc = txtNoiHoc.Text;
                QTHTService.Update(qtht.ID, pathQTHT, tunam, dennam, noihoc);
            }
            else
            {
                
                var tunam = (int)numTuNam.Value;
                var dennam = (int)numDenNam.Value;
                var noihoc = txtNoiHoc.Text;
                QTHTService.Add(pathQTHT, tunam, dennam, noihoc);
            }
            MessageBox.Show("Đã cập nhật dữ liệu thành công");
            DialogResult = DialogResult.OK;
        }
    }
}
