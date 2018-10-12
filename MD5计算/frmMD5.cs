using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace MD5计算
{
    public partial class frmMD5 : Form
    {
        public frmMD5()
        {
            InitializeComponent();
            //默认选中UTF8
            cmbEncode.SelectedIndex = 1;
        }
        //获取编码方式
        private void GetCodeType(out Encoding ec)
        {
            if (cmbEncode.Enabled)
            {
                //勾选其他编码
                switch (cmbEncode.SelectedIndex)
                {
                    case 5:
                        ec = Encoding.BigEndianUnicode;
                        break;
                    case 4:
                        ec = Encoding.UTF7;
                        break;
                    case 3:
                        ec = Encoding.UTF32;
                        break;
                    case 2:
                        ec = Encoding.Unicode;
                        break;
                    case 1:
                        ec = Encoding.UTF8;
                        break;
                    case 0:
                    default:
                        ec = Encoding.Default;
                        break;
                }
            }
            else
            {
                //默认UTF8
                ec = Encoding.UTF8;
            }
        }

        private void chbOtherEncode_CheckedChanged(object sender, EventArgs e)
        {
            //勾选其他编码后激活编码方式下拉框
            cmbEncode.Enabled = chbOtherEncode.Checked;
        }
        //文件选择
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (ofdInput.ShowDialog() == DialogResult.OK)
            {
                txtStrOrFile.Text = ofdInput.FileName;
            }
        }
        //此事件是拖拽进入文本框还未松开鼠标时的事件
        private void txtStrOrFile_DragEnter(object sender, DragEventArgs e)
        {
            //拖拽文件放入文本框才有效果
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
        }
        //此事件时拖拽完成后（松开鼠标后）的事件
        private void txtStrOrFile_DragDrop(object sender, DragEventArgs e)
        {
            //将文件全名称设置到文本框内（触发txtStrOrFile_TextChanged事件）
            txtStrOrFile.Clear();
            txtStrOrFile.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
        }

        private void txtStrOrFile_TextChanged(object sender, EventArgs e)
        {
            //清空文件信息框
            txtFileInfo.Clear();
            StringBuilder sb = new StringBuilder();
            Encoding ecTemp;
            //获取编码方式
            GetCodeType(out ecTemp);
            byte[] buffer, tempBytes;
            if (!chbString.Checked && File.Exists(txtStrOrFile.Text.Trim()))
            {
                //混合模式下获取文件信息
                FileInfo fi = new FileInfo(txtStrOrFile.Text.Trim());
                string strReadOnly = fi.IsReadOnly ? "(只读)" : string.Empty;
                sb.Append("文 件 名：" + fi.Name + System.Environment.NewLine + "文件大小：");
                #region calFileLength（计算文件大小）
                if (fi.Length >= 0 && fi.Length < 1024)
                {
                    sb.Append(fi.Length + "字节");
                }
                else if (fi.Length / 1024 >= 1 && fi.Length / 1024 < 1024)
                {
                    sb.Append(string.Format("{0:F}",(double)fi.Length/ 1024) + "KB");
                }
                else if (fi.Length / 1024 / 1024 >= 1 && fi.Length / 1024 / 1024 < 1024)
                {
                    sb.Append(string.Format("{0:F}",(double)fi.Length / 1024 / 1024) + "MB");
                }
                else if (fi.Length / 1024 / 1024 >= 1024)
                {
                    sb.Append(string.Format("{0:F}",(double)fi.Length / 1024 / 1024 / 1024) + "GB");
                }
                else
                {
                    sb.Append("未知");
                }
                #endregion
                sb.Append(strReadOnly + System.Environment.NewLine + "创建时间：" + fi.CreationTime.ToLongDateString() + " " + fi.CreationTime.ToLongTimeString() + System.Environment.NewLine + "修改时间：" + fi.LastWriteTime.ToLongDateString() + " " + fi.LastWriteTime.ToLongTimeString());
                txtFileInfo.AppendText(sb.ToString());
                sb.Clear();
                //开启新线程使用委托计算文件MD5，防止界面假死
                ThreadPool.QueueUserWorkItem(new WaitCallback(GetMD5ByFilePath), (object)sb);
            }
            else
            {
                //混合模式和仅字符串模式字符串MD5计算(仅字符串模式拖拽文件后，会计算文件全名称字符串的MD5，而不是文件的MD5)
                sb.Clear();
                if (txtStrOrFile.Text.Trim() == string.Empty)
                {
                    //空串不做处理
                    txtMD5.Text = string.Empty;
                    return;
                }
                //字符串MD5计算实现
                using (MD5 md5 = MD5.Create())
                {
                    //根据指定编码方式将字符串转换为对应的字节数组
                    buffer = ecTemp.GetBytes(txtStrOrFile.Text.Trim());
                    //计算字节数组的哈希值
                    tempBytes = md5.ComputeHash(buffer);
                    //释放资源
                    md5.Clear();
                    for (int i = 0; i < tempBytes.Length; i++)
                    {
                        //将字节的哈希码以16进制两位输出
                        sb.Append(rbUpper.Checked ? tempBytes[i].ToString("X2") : tempBytes[i].ToString("x2"));
                    }
                }
                txtMD5.Text = sb.ToString();
            }
        }
        //文件MD5计算实现
        private void GetMD5ByFilePath(object sb)
        {
            byte[] tempBytes;
            StringBuilder sb1 = (StringBuilder)sb;
            //使用委托进行UI界面计算状态的更新（开始计算）
            txtMD5.Invoke(new UpdateUIControlDelegate(StartProgress), new object[] { "0" });
            using (FileStream fsRead = new FileStream(txtStrOrFile.Text.Trim(),FileMode.Open,FileAccess.Read))
            {
                using (MD5 md5 = MD5.Create())
                {
                    //计算文件流FileStream的哈希值
                    tempBytes = md5.ComputeHash(fsRead);
                    //释放资源
                    md5.Clear();
                    for (int i = 0; i < tempBytes.Length; i++)
                    {
                        //将字节的哈希码以16进制两位输出
                        sb1.Append(rbUpper.Checked ? tempBytes[i].ToString("X2") : tempBytes[i].ToString("x2"));
                    }
                }
            }
            //使用委托进行UI界面计算状态的更新（计算完毕）
            txtMD5.Invoke(new UpdateUIControlDelegate(EndProgress), new object[] { sb1.ToString() });
        }
        //开始计算
        private void StartProgress(string val)
        {
            txtMD5.ForeColor = Color.FromArgb(100,100,100);
            txtMD5.Text = "正在计算，请稍候...";
        }
        //计算完毕
        private void EndProgress(string val)
        {
            txtMD5.ForeColor = Color.Black;
            txtMD5.Text = val;
        }
        //在新线程操作部分控件的委托
        public delegate void UpdateUIControlDelegate(string val);

        private void rbUpper_CheckedChanged(object sender, EventArgs e)
        {
            //切换MD5大小写显示
            txtMD5.Text = rbUpper.Checked ? (txtMD5.Text.Trim() == string.Empty ? string.Empty : txtMD5.Text.ToUpper()) : (txtMD5.Text.Trim() == string.Empty ? string.Empty : txtMD5.Text.ToLower());
        }

        private void txtCompare_TextChanged(object sender, EventArgs e)
        {
            //输入或复制粘贴MD5比对是否匹配（匹配MessageBox提示，不匹配字体变为红色）
            if (string.Compare(txtCompare.Text.Trim(), txtMD5.Text,true) == 0)
            {
                txtCompare.ForeColor = Color.Black;
                MessageBox.Show("MD5值匹配", "提示");
            }
            else
            {
                txtCompare.ForeColor = Color.Red;
            }
        }
    }
}
