using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Model;
using WindowsFormsApplication1.Service;

namespace WindowsFormsApplication1
{
    public partial class frmThongTinSinhVien : Form
    {
        String anhDaiDienPathDirectory;
        String anhDaiDienPathFile;
        String studentPathFile;
        String qthtPathFile;
        public frmThongTinSinhVien(string idStudent)
        {
            InitializeComponent();
            picAnhDaiDien.AllowDrop = true;
            anhDaiDienPathDirectory = Application.StartupPath + @"\AnhDaiDien";
            anhDaiDienPathFile = anhDaiDienPathDirectory + @"\avatar.png";
            studentPathFile = Application.StartupPath + @"\Data\student_data.txt";
            qthtPathFile = Application.StartupPath + @"\Data\quatrinhhoctap.txt";
            loadViewQTHT();
            if (File.Exists(anhDaiDienPathFile))
            {
                FileStream fileStream = new FileStream(anhDaiDienPathFile, FileMode.Open, FileAccess.Read);
                picAnhDaiDien.Image = Image.FromStream(fileStream);
            }
            //var student = StudentService.getStudent(idStudent);
            var student = StudentService.getStudent(studentPathFile, idStudent);
            
            if (student == null)
            {
                throw new Exception("Lỗi rồi!");
            }
            else
            {
                
                txtMaSV.Text = student.ID;
                txtHoTen.Text = student.FullName;
                txtNgaySinh.Value = student.DateOfBirth;
                txtNoiSinh.Text = student.PlaceOfBirth;
                txtGioiTinh.Checked = student.Gender == Model.GENDER.Male;

                //student.qtht1 = QTHTService.getQTHT(idStudent);
                student.qtht1 = QTHTService.getQTHT(qthtPathFile, idStudent);
                
                bdsQTHT.DataSource = student.qtht1;
                //không cho tự tạo colum
                dtgQTHT.AutoGenerateColumns = false;
                dtgQTHT.DataSource = bdsQTHT;
                lblMuc.Text = string.Format("{0} mục", student.qtht1.Count());
   
            }

        }

        private void frmThongTinSinhVien_Load(object sender, EventArgs e)
        {

        }

        private void lnkChonAnhDaiDien_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //MessageBox.Show("Chon anh dai dien");
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "File ảnh(*.jpg, *png)|*.png; *.jpg";
            fileDialog.Title = "Chọn ảnh đại diện";
            if(fileDialog.ShowDialog()== DialogResult.OK)
            {
                #region Hiển thị ảnh đc hiển thị lên picturebox
                var fileName = fileDialog.FileName;
                FileStream fileStream = new FileStream(anhDaiDienPathFile, FileMode.Open, FileAccess.Read);
                var anhDaiDien = Image.FromFile(fileName);
                
                #endregion

                #region Lưu ảnh đại diện vào thư mục của chương trình
                if (!Directory.Exists(anhDaiDienPathDirectory))
                {
                    Directory.CreateDirectory(anhDaiDienPathDirectory);
                }
                anhDaiDien.Save(anhDaiDienPathFile);
                picAnhDaiDien.Image = anhDaiDien;
                fileStream.Close();
                #endregion
            }
        }

        private void picAnhDaiDien_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void picAnhDaiDien_DragDrop(object sender, DragEventArgs e)
        {
            var fileNameList = (string[])e.Data.GetData(DataFormats.FileDrop);
            FileStream fileStream = new FileStream(fileNameList.FirstOrDefault(), FileMode.Open, FileAccess.Read);
            var anhDaiDien = Image.FromStream(fileStream);
            #region Lưu ảnh đại diện vào thư mục của chương trình
            if (!Directory.Exists(anhDaiDienPathDirectory))
            {
                Directory.CreateDirectory(anhDaiDienPathDirectory);
            }
            anhDaiDien.Save(anhDaiDienPathFile);
            picAnhDaiDien.Image = anhDaiDien;
            fileStream.Close();
            #endregion
        }

       /* private void btnXoa_Click(object sender, EventArgs e)
        {
            var rs = MessageBox.Show("Bạn có thực sự muốn xóa hay không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if(rs== DialogResult.OK)
            {
                var history = (QTHT)bdsQTHT.Current;
                List<QTHT> h1 = (List<QTHT>) bdsQTHT.DataSource;//ép kiểu
                h1.Remove(history);
                bdsQTHT.DataSource = h1;
                bdsQTHT.ResetBindings(true);
                String xoa = history.ID + "#" + history.FromYear + "#" + history.ToYear + "#" + history.SchoolName;
                var Lines = File.ReadAllLines(qthtPathFile);
                var newLines = Lines.Where(line => !line.Contains(xoa));
                File.WriteAllLines(qthtPathFile, newLines);
                //Khai báo ô được chọn
                //int rowIndex = dtgQTHT.CurrentCell.RowIndex;
                //Xóa ô 
                //dtgQTHT.Rows.RemoveAt(rowIndex);
                MessageBox.Show("Đã xóa thành công!");
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }*/
        private void btnXoa_Click(object sender, EventArgs e)
        {
            var history = bdsQTHT.Current as QTHT;
            if (history != null)
            {
                var rs = MessageBox.Show("Bạn có thực sự muốn xóa hay không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (rs == DialogResult.OK)
                {
                    QTHTService.Remove(qthtPathFile, history);
                    bdsQTHT.RemoveCurrent();
                    //Khai báo ô được chọn
                    //int rowIndex = dtgQTHT.CurrentCell.RowIndex;
                    //Xóa ô 
                    //dtgQTHT.Rows.RemoveAt(rowIndex);
                    MessageBox.Show("Đã xóa thành công!");
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
            
        }
        public void loadViewQTHT()
        {
            bdsQTHT.DataSource = null;
            dtgQTHT.AutoGenerateColumns = false;
            var lsQTHT = QTHTService.getQTHT(qthtPathFile);
            if (lsQTHT != null)
            {
                bdsQTHT.DataSource = lsQTHT;

            }
            else
            {
                throw new Exception("QTHT không tồn tại!");
            }
            dtgQTHT.DataSource = bdsQTHT;
            //lblContact.Text = string.Format("Tổng contact: {0} mục", lsContactSort.Count());
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmQTHTChiTiet themqtht = new frmQTHTChiTiet();
            themqtht.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var qtht = bdsQTHT.Current as QTHT;
            if(qtht != null)
            {
                var f = new frmQTHTChiTiet(qtht);
                f.ShowDialog();
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
