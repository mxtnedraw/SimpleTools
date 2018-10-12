using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace 批量重命名
{
    public partial class frmFileOper : Form
    {

        //private static string strMax = "";
        //private static string strMin = "";

        public frmFileOper()
        {
            InitializeComponent();
            GetMinMax();
            RefreshFNView();
        }
        //声明一些字段存放临时数据
        private string strMax = "";     //文件（夹）名预览（RefreshFNView()）时最大编号
        private string strMin = "";     //文件（夹）名预览时最大编号
        private string oldName = "";        //文件（夹）名预览时示例名称（显示为[dirName]或[fileName]）
        private string units = "";      //文件（夹）名预览时文件（夹）单位（显示为dirs或files）
        private string strBeforeChange = string.Empty;  //记录txtInPath的文本（txtInPath_TextChanged）
        private string strFTBeforeChange = string.Empty;    //记录txtFileType的文本（txtFileType_TextChanged）

        private int fileCount = 0;      //记录待重命名的文件(夹)总数（GetMaxLength()中赋值，RenameWithNewThread()和RenameDirWithNewThread()中调用以计算进度条进度）

        private Dictionary<FileInfo, string> dicCancel = new Dictionary<FileInfo, string>();        //记录撤销上次文件操作时所需信息
        private List<string> ltDirsCancel = new List<string>();     //记录撤销上次文件夹操作时所需信息
        //选择文件夹按钮单击事件
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if (fbdInPath.ShowDialog() == DialogResult.OK)
            {
                txtInPath.Text = fbdInPath.SelectedPath;
            }
        }
        //使用新文件（夹）名checked改变事件
        private void chbNewFileName_CheckedChanged(object sender, EventArgs e)
        {
            //切换各textbox和checkbox的启用和选中状态
            txtNewFileName.Enabled = chbNewFileName.Checked;
            chbAutoIncrease.Checked = chbNewFileName.Checked;
            chbAutoIncrease.Enabled = !chbNewFileName.Checked;
            chbJoinChar.Enabled = !chbNewFileName.Checked;
            txtJoinChar.Enabled = !chbNewFileName.Checked && chbJoinChar.Checked;
            txtAttach.Enabled = !chbNewFileName.Checked;
            
            RefreshFNView();
        }
        //处理子文件夹按钮单击事件
        private void cbOperChildren_Click(object sender, EventArgs e)
        {
            if (CancelQue())
            {
                cbOperChildren.Checked = !cbOperChildren.Checked;
            } 
            cbIndependChild.Enabled = cbOperChildren.Checked;
            GetMinMax();
            RefreshFNView();
        }
        /// <summary>
        /// 返回"更改此选项后上次操作无法撤销，是否继续？"提示消息框的选择bool值
        /// </summary>
        /// <returns>true:不做更改，可以撤销；false:无法撤销</returns>
        private bool CancelQue()
        {
            if (chbDir.Checked && btnCancelLast.Visible == true && btnCancelLast.Enabled == true)
            {
                DialogResult drQue = MessageBox.Show("更改此选项后上次操作无法撤销，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (drQue == DialogResult.Yes)
                {
                    btnCancelLast.Enabled = false;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 更新进度条信息
        /// </summary>
        /// <param name="val">进度条进度，0-100</param>
        private void UpdateProgressBar(int val)
        {
            if (val >= 100)
            {
                tspbRename.Value = 100;
                tspbRename.Visible = false;
                btnRename.Enabled = true;
                btnSelectFolder.Enabled = true;
                //tspbRename.Value = 0;
            }
            else
            {
                tspbRename.Value = val;
            }
        }
        /// <summary>
        /// 更新进度文字信息
        /// </summary>
        /// <param name="val">进度，0-100</param>
        private void UpdateProgressLabel(int val)
        {
            tsslRename.Text = val >= 100 ? "就绪" : "已完成" + val + "%";
        }
        /// <summary>
        /// 显示撤销按钮
        /// </summary>
        /// <param name="holder"></param>
        private void UpdateBtnCancelLast(int holder)
        {
            btnCancelLast.Enabled = true;
            btnCancelLast.Visible = true;
        }

        //批量重命名按钮单击事件，执行重命名操作
        private void btnRename_Click(object sender, EventArgs e)
        {
            tspbRename.Visible = true;
            btnRename.Enabled = false;
            btnSelectFolder.Enabled = false;
            if (btnCancelLast.Visible == true)
            {
                btnCancelLast.Enabled = false;
            }
            tspbRename.Value = 0;
            Thread thProgress = chbDir.Checked ? new Thread(RenameDirWithNewThread) : new Thread(RenameWithNewThread);
            thProgress.IsBackground = true;
            thProgress.Start();
        }

        //重命名
        /// <summary>
        /// 开启一个新线程对指定文件进行重命名
        /// </summary>
        private void RenameWithNewThread()
        {
            try
            {
                string strAttach = txtAttach.Enabled ? txtAttach.Text : "";
                string strJoinChar = txtJoinChar.Enabled ? txtJoinChar.Text : "";
                string strNewFileName = txtNewFileName.Enabled ? txtNewFileName.Text : "";
                if (Regex.IsMatch(txtInPath.Text.Trim(), @"^[a-zA-Z]:{1}\\{1}$") || Regex.IsMatch(txtInPath.Text.Trim(), @"^[a-zA-Z]:{1}$"))
                {
                    MessageBox.Show("出于安全性考虑，不允许对根目录进行操作！ ", "提示");
                    return;
                }
                if (Regex.IsMatch(txtInPath.Text, @"[\*\?\<\>\|""]"))
                {
                    MessageBox.Show("文件路径错误，请核对后再试 ", "提示");
                    return;
                }
                if (Regex.IsMatch(strAttach, @"[\\\/\*\?\<\>\|:""]") || Regex.IsMatch(strJoinChar, @"[\\\/\*\?\<\>\|:""]") || Regex.IsMatch(strNewFileName, @"[\\\/\*\?\<\>\|:""]"))
                {
                    MessageBox.Show("文件名中包含非法字符，请确保文件名中不包含以下字符：\n\n\n\t/ ? * : < > \" | \\ ", "提示");
                    return;
                }
                int renameCount = 0;
                List<FileInfo[]> ltFinal = GetFileInfos();
                if (ltFinal == null)
                {
                    MessageBox.Show("没有找到要操作的文件或文件路径错误，请修改筛选条件后再试！", "提示");
                    return;
                }

                //设置起始编号
                int initialNum;
                string strInitialNum = txtInitialNum.Enabled ? txtInitialNum.Text : "";
                if (Int32.TryParse(strInitialNum, out initialNum))
                {
                    initialNum = initialNum < 0 ? 1 : initialNum;
                }
                else
                {
                    initialNum = 1;
                }

                int progressNum = 0;
                dicCancel.Clear();
                if (chbNewFileName.Checked)
                {
                    //使用新文件名
                    foreach (FileInfo[] fi in ltFinal)
                    {
                        renameCount += fi.Length; //记录影响的文件个数

                        for (int i = 1; i <= fi.Length; i++)
                        {
                            dicCancel.Add(fi[i - 1], fi[i - 1].FullName.ToString()); //记录文件初始状态以便于撤销操作
                            string tempOrderNum = GetOrderNum(fi.Length + initialNum - 1, i + initialNum - 1); //获得当前操作文件的编号
                            //FileReNames(fi[i - 1], rbfront.Checked, strAttach, tempOrderNum, strNewFileName);
                            progressNum++;
                            //UpdateProgress((progressNum * 100) / fileCount);
                            tspbRename.ProgressBar.Invoke(new UpdateUIControlDelegate(UpdateProgressBar), new object[] { (progressNum * 100) / fileCount });
                            tsslRename.Owner.Invoke(new UpdateUIControlDelegate(UpdateProgressLabel), new object[] { (progressNum * 100) / fileCount });
                            FileReNames(fi[i - 1], rbfront.Checked, strAttach, tempOrderNum, strNewFileName);

                        }
                    }
                }
                else if (chbAutoIncrease.Checked)
                {
                    //使用原文件名，自动编号
                    foreach (FileInfo[] fi in ltFinal)
                    {
                        //fi.ToList().ForEach(i => ltCancel.Add(i.FullName));
                        renameCount += fi.Length;
                        for (int i = 1; i <= fi.Length; i++)
                        {
                            dicCancel.Add(fi[i - 1], fi[i - 1].FullName.ToString());
                            string tempOrderNum = GetOrderNum(fi.Length + initialNum - 1, i + initialNum - 1);
                            //FileReNames(fi[i - 1], rbfront.Checked, strJoinChar, strAttach, tempOrderNum, true);
                            progressNum++;
                            tspbRename.ProgressBar.Invoke(new UpdateUIControlDelegate(UpdateProgressBar), new object[] { (progressNum * 100) / fileCount });
                            tsslRename.Owner.Invoke(new UpdateUIControlDelegate(UpdateProgressLabel), new object[] { (progressNum * 100) / fileCount });
                            FileReNames(fi[i - 1], rbfront.Checked, strJoinChar, strAttach, tempOrderNum, true);
                        }
                    }
                }
                else
                {
                    ////使用原文件名，不自动编号
                    foreach (FileInfo[] fi in ltFinal)
                    {
                        //fi.ToList().ForEach(i => ltCancel.Add(i.FullName));
                        renameCount += fi.Length;
                        for (int i = 1; i <= fi.Length; i++)
                        {
                            dicCancel.Add(fi[i - 1], fi[i - 1].FullName.ToString());
                            //string tempOrderNum = GetOrderNum(fi.Length + initialNum - 1, i + initialNum - 1);
                            //FileReNames(fi[i - 1], rbfront.Checked, strJoinChar, strAttach);
                            progressNum++;
                            tspbRename.ProgressBar.Invoke(new UpdateUIControlDelegate(UpdateProgressBar), new object[] { (progressNum * 100) / fileCount });
                            tsslRename.Owner.Invoke(new UpdateUIControlDelegate(UpdateProgressLabel), new object[] { (progressNum * 100) / fileCount });
                            FileReNames(fi[i - 1], rbfront.Checked, strJoinChar, strAttach);
                        }
                    }
                }
                btnCancelLast.Invoke(new UpdateUIControlDelegate(UpdateBtnCancelLast), new object[] { 1 });
                DialogResult drFinish = MessageBox.Show("重命名成功，" + renameCount + "个文件受影响。\n 点击”确定“打开文件目录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (drFinish == DialogResult.OK)
                {
                    System.Diagnostics.Process.Start("Explorer.exe", txtInPath.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("未知错误！" + ex.Message + "请联系开发者以完善程序", "警告");
                WriteLog(Application.StartupPath + "\\error.log", ex.Message);
            }
            finally
            {
                tspbRename.ProgressBar.Invoke(new UpdateUIControlDelegate(UpdateProgressBar), new object[] { 100 });
                Thread.CurrentThread.Abort();

            }

        }
        /// <summary>
        /// 开启一个新线程对指定目录进行重命名
        /// </summary>
        private void RenameDirWithNewThread()
        {
            try
            {
                string strAttach = txtAttach.Enabled ? txtAttach.Text : "";
                string strJoinChar = txtJoinChar.Enabled ? txtJoinChar.Text : "";
                string strNewFileName = txtNewFileName.Enabled ? txtNewFileName.Text : "";
                string strInPath = txtInPath.Text.Trim();
                if (Regex.IsMatch(strInPath, @"^[a-zA-Z]:{1}\\{1}$") || Regex.IsMatch(strInPath, @"^[a-zA-Z]:{1}$"))
                {
                    MessageBox.Show("出于安全性考虑，不允许对根目录进行操作！ ", "提示");
                    return;
                }
                if (Regex.IsMatch(strInPath, @"[\*\?\<\>\|""]"))
                {
                    MessageBox.Show("文件路径错误，请核对后再试 ", "提示");
                    return;
                }
                if (Regex.IsMatch(strAttach, @"[\\\/\*\?\<\>\|:""]") || Regex.IsMatch(strJoinChar, @"[\\\/\*\?\<\>\|:""]") || Regex.IsMatch(strNewFileName, @"[\\\/\*\?\<\>\|:""]"))
                {
                    MessageBox.Show("文件名中包含非法字符，请确保文件名中不包含以下字符：\n\n\n\t/ ? * : < > \" | \\ ", "提示");
                    return;
                }
                int renameCount = 0;
                List<DirectoryInfo[]> ltFinal = GetDirectoryInfos();
                if (ltFinal == null)
                {
                    MessageBox.Show("没有找到要操作的文件或文件路径错误，请修改筛选条件后再试！", "提示");
                    return;
                }

                //设置起始编号
                int initialNum;
                string strInitialNum = txtInitialNum.Enabled ? txtInitialNum.Text : "";
                if (Int32.TryParse(strInitialNum, out initialNum))
                {
                    initialNum = initialNum < 0 ? 1 : initialNum;
                }
                else
                {
                    initialNum = 1;
                }

                int progressNum = 0;
                ltDirsCancel.Clear();
                if (chbNewFileName.Checked)
                {
                    //使用新文件夹名
                    foreach (DirectoryInfo[] di in ltFinal)
                    {
                        renameCount += di.Length; //记录影响的文件个数

                        for (int i = 1; i <= di.Length; i++)
                        {
                            ltDirsCancel.Add(di[i - 1].Name.ToString()); //记录文件初始状态以便于撤销操作
                            string tempOrderNum = (cbOperChildren.Checked && !cbIndependChild.Checked) ? GetOrderNum(di.Length + initialNum - 1, di.Length-i + initialNum) : GetOrderNum(di.Length + initialNum - 1, i + initialNum - 1); //获得当前操作文件的编号,如果处理子文件夹并且不是各自按规则，则反序编号。
                            
                            //DirReNames(di[i - 1], rbfront.Checked, strAttach, tempOrderNum, strNewFileName);
                            progressNum++;
                            //UpdateProgress((progressNum * 100) / fileCount);
                            tspbRename.ProgressBar.Invoke(new UpdateUIControlDelegate(UpdateProgressBar), new object[] { (progressNum * 100) / fileCount });
                            tsslRename.Owner.Invoke(new UpdateUIControlDelegate(UpdateProgressLabel), new object[] { (progressNum * 100) / fileCount });
                            DirReNames(di[i - 1], rbfront.Checked, strAttach, tempOrderNum, strNewFileName);

                        }
                    }
                }
                else if (chbAutoIncrease.Checked)
                {
                    //使用原文件夹名，自动编号
                    foreach (DirectoryInfo[] di in ltFinal)
                    {
                        //fi.ToList().ForEach(i => ltCancel.Add(i.FullName));
                        renameCount += di.Length;
                        for (int i = 1; i <= di.Length; i++)
                        {
                            ltDirsCancel.Add(di[i - 1].Name.ToString());
                            string tempOrderNum = (cbOperChildren.Checked && !cbIndependChild.Checked) ? GetOrderNum(di.Length + initialNum - 1, di.Length - i + initialNum) : GetOrderNum(di.Length + initialNum - 1, i + initialNum - 1);
                            //DirReNames(di[i - 1], rbfront.Checked, strJoinChar, strAttach, tempOrderNum, true);
                            progressNum++;
                            tspbRename.ProgressBar.Invoke(new UpdateUIControlDelegate(UpdateProgressBar), new object[] { (progressNum * 100) / fileCount });
                            tsslRename.Owner.Invoke(new UpdateUIControlDelegate(UpdateProgressLabel), new object[] { (progressNum * 100) / fileCount });
                            DirReNames(di[i - 1], rbfront.Checked, strJoinChar, strAttach, tempOrderNum, true);
                        }
                    }
                }
                else
                {
                    //使用原文件夹名，不自动编号
                    foreach (DirectoryInfo[] di in ltFinal)
                    {
                        //fi.ToList().ForEach(i => ltCancel.Add(i.FullName));
                        renameCount += di.Length;
                        for (int i = 1; i <= di.Length; i++)
                        {
                            ltDirsCancel.Add(di[i - 1].Name.ToString());
                            //string tempOrderNum = GetOrderNum(fi.Length + initialNum - 1, i + initialNum - 1);
                            //DirReNames(di[i - 1], rbfront.Checked, strJoinChar, strAttach);
                            progressNum++;
                            tspbRename.ProgressBar.Invoke(new UpdateUIControlDelegate(UpdateProgressBar), new object[] { (progressNum * 100) / fileCount });
                            tsslRename.Owner.Invoke(new UpdateUIControlDelegate(UpdateProgressLabel), new object[] { (progressNum * 100) / fileCount });
                            DirReNames(di[i - 1], rbfront.Checked, strJoinChar, strAttach);
                        }
                    }
                }
                btnCancelLast.Invoke(new UpdateUIControlDelegate(UpdateBtnCancelLast), new object[] { 1 });
                DialogResult drFinish = MessageBox.Show("重命名成功，" + renameCount + "个文件夹受影响。\n 点击”确定“打开文件目录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (drFinish == DialogResult.OK)
                {
                    System.Diagnostics.Process.Start("Explorer.exe", txtInPath.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("未知错误！" + ex.Message + "请联系开发者以完善程序", "警告");
                WriteLog(Application.StartupPath + "\\error.log", ex.Message);
            }
            finally
            {
                tspbRename.ProgressBar.Invoke(new UpdateUIControlDelegate(UpdateProgressBar), new object[] { 100 });
                Thread.CurrentThread.Abort();
            }

        }

        public delegate void UpdateUIControlDelegate(int val);      //在新线程操作部分控件的委托

        /// <summary>
        /// 返回当前文件编号数字字符串
        /// </summary>
        /// <param name="p">当前文件总数</param>
        /// <param name="i">当前文件序号</param>
        /// <returns>最终显示在文件（夹）名中的编号数字字符串</returns>
        private string GetOrderNum(int p, int i)
        {
            string orderNum = "";
            //p,ToString().ToCharArray()
            //if (i < 10)
            //{
            //    orderNum = "0";
            //    for (int j = 0; j < p.ToString().Length - 2; j++)
            //    {
            //        orderNum += "0";
            //    }
            //    return orderNum + i.ToString();
            //}
            //else
            //{

            //根据要操作的文件个数实现编号前补“0”；如：共12个，则编号01-12，共123个，则编号001-123，......
            for (int j = 0; j < p.ToString().Length - i.ToString().Length; j++)
            {
                orderNum += "0";
            }
            return orderNum + i.ToString();
            //}
        }
        
        /// <summary>
        /// 重命名，使用原文件名，不自动编号
        /// </summary>
        /// <param name="fi">需要重命名的文件信息</param>
        /// <param name="front">true:前缀，false:后缀</param>
        /// <param name="txtJoinChar">连接符</param>
        /// <param name="txtAttach">前（后）缀</param>
        private void FileReNames(FileInfo fi, bool front, string txtJoinChar, string txtAttach)
        {
            string tempFN = front ? fi.DirectoryName + "\\" + txtAttach + txtJoinChar + fi.Name : fi.DirectoryName + "\\" + Regex.Replace(fi.Name, @"\" + fi.Extension + "$", "", RegexOptions.IgnoreCase) + txtJoinChar + txtAttach + fi.Extension;
            fi.MoveTo(tempFN);
        }
        /// <summary>
        /// 重命名，使用原文件夹名，不自动编号
        /// </summary>
        /// <param name="di">需要重命名的文件夹信息</param>
        /// <param name="front">true:前缀，false:后缀</param>
        /// <param name="txtJoinChar">连接符</param>
        /// <param name="txtAttach">前（后）缀</param>
        private void DirReNames(DirectoryInfo di, bool front, string txtJoinChar, string txtAttach)
        {
            string tempDN = front ? Regex.Replace(di.FullName, @"\\[^\\]+$", "", RegexOptions.IgnoreCase) + "\\" + txtAttach + txtJoinChar + di.Name : di.FullName + txtJoinChar + txtAttach;
            if (di.FullName != tempDN)
            {
                di.MoveTo(tempDN);
            }
        }

        /// <summary>
        /// 重命名，使用原文件名，自动编号
        /// </summary>
        /// <param name="fi">需要重命名的文件信息</param>
        /// <param name="front">true:前缀，false:后缀</param>
        /// <param name="txtJoinChar">连接符</param>
        /// <param name="txtAttach">前（后）缀</param>
        /// <param name="strOrderNum">编号数字字符串</param>
        /// <param name="autoIncrease">重载需要所增加的参数，只有一个值：true</param>
        private void FileReNames(FileInfo fi, bool front, string txtJoinChar, string txtAttach, string strOrderNum, bool autoIncrease)
        {
            string tempFN = front ? fi.DirectoryName + "\\" + strOrderNum + txtJoinChar + txtAttach + fi.Name : fi.DirectoryName + "\\" + Regex.Replace(fi.Name, @"\" + fi.Extension + "$", "", RegexOptions.IgnoreCase) + txtAttach + txtJoinChar + strOrderNum + fi.Extension;
            fi.MoveTo(tempFN);
        }
        /// <summary>
        /// 重命名，使用原文件夹名，自动编号
        /// </summary>
        /// <param name="di">需要重命名的文件夹信息</param>
        /// <param name="front">true:前缀，false:后缀</param>
        /// <param name="txtJoinChar">连接符</param>
        /// <param name="txtAttach">前（后）缀</param>
        /// <param name="strOrderNum">编号数字字符串</param>
        /// <param name="autoIncrease">重载需要所增加的参数，只有一个值：true</param>
        private void DirReNames(DirectoryInfo di, bool front, string txtJoinChar, string txtAttach, string strOrderNum, bool autoIncrease)
        {
            string tempDN = front ? Regex.Replace(di.FullName, @"\\[^\\]+$", "", RegexOptions.IgnoreCase) + "\\" + strOrderNum + txtJoinChar + txtAttach + di.Name : di.FullName + txtAttach + txtJoinChar + strOrderNum;
            if (di.FullName != tempDN)
            {
                di.MoveTo(tempDN);
            }
        }

        /// <summary>
        /// 重命名，使用新文件名，必须自动编号
        /// </summary>
        /// <param name="fi">需要重命名的文件信息</param>
        /// <param name="front">true:前缀，false:后缀</param>
        /// <param name="txtAttach">前（后）缀</param>
        /// <param name="strOrderNum">编号数字字符串</param>
        /// <param name="newName">新文件名</param>
        private void FileReNames(FileInfo fi, bool front, string txtAttach, string strOrderNum, string newName)
        {
            string tempFN = front ? fi.DirectoryName + "\\" + strOrderNum + txtAttach + newName + fi.Extension : fi.DirectoryName + "\\" + newName + txtAttach + strOrderNum + fi.Extension;
            fi.MoveTo(tempFN);
        }
        /// <summary>
        /// 重命名，使用新文件夹名，必须自动编号
        /// </summary>
        /// <param name="di">需要重命名的文件夹信息</param>
        /// <param name="front">true:前缀，false:后缀</param>
        /// <param name="txtAttach">前（后）缀</param>
        /// <param name="strOrderNum">编号数字字符串</param>
        /// <param name="newName">新文件名</param>
        private void DirReNames(DirectoryInfo di, bool front, string txtAttach, string strOrderNum, string newName)
        {
            string tempDN = front ? Regex.Replace(di.FullName, @"\\[^\\]+$", "", RegexOptions.IgnoreCase) + "\\" + strOrderNum + txtAttach + newName : Regex.Replace(di.FullName, @"\\[^\\]+$", "", RegexOptions.IgnoreCase) + "\\" + newName + txtAttach + strOrderNum;
            if (di.FullName != tempDN)
            {
                di.MoveTo(tempDN);
            }
        }
        //连接符复选框checked改变事件
        private void chbJoinChar_CheckedChanged(object sender, EventArgs e)
        {
            txtJoinChar.Enabled = chbJoinChar.Checked;
            txtJoinChar.Text = "";
            RefreshFNView();
        }
        //新文件（夹）名文本框TextChanged事件
        private void txtNewFileName_TextChanged(object sender, EventArgs e)
        {
            RefreshFNView();
        }

        /// <summary>
        /// 刷新文件名预览框的显示
        /// </summary>
        private void RefreshFNView()
        {
            oldName = chbDir.Checked ? "[dirName]" : "[fileName]";
            units = chbDir.Checked ? "dirs" : "files";
            if (chbNewFileName.Checked == true)
            {
                txtFileNameView.Text = rbfront.Checked ? strMin + txtNewFileName.Text.TrimEnd() + "，...，" + strMax + txtNewFileName.Text.TrimEnd() + "-----" + fileCount + units : txtNewFileName.Text.TrimStart() + strMin + "，...，" + txtNewFileName.Text.TrimStart() + strMax + "-----" + fileCount + units;
            }
            else if (chbNewFileName.Checked == false && chbAutoIncrease.Checked == true)
            {
                txtFileNameView.Text = rbfront.Checked ? strMin + txtJoinChar.Text + txtAttach.Text + oldName + "，...，" + strMax + txtJoinChar.Text + txtAttach.Text + oldName + "-----" + fileCount + units : oldName + txtAttach.Text + txtJoinChar.Text + strMin + "，...，" + oldName + txtAttach.Text + txtJoinChar.Text + strMax + "-----" + fileCount + units;
            }
            else
            {
                txtFileNameView.Text = rbfront.Checked ? txtAttach.Text.TrimStart() + txtJoinChar.Text + oldName + "-----" + fileCount + units : oldName + txtJoinChar.Text + txtAttach.Text.TrimEnd() + "-----" + fileCount + units;
            }
        }
        //选择文件夹文本框TextChanged事件
        private void txtInPath_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtInPath.Text.Trim(), @"^[a-zA-Z]:{1}\\{1}$") || Regex.IsMatch(txtInPath.Text.Trim(), @"^[a-zA-Z]:{1}$"))
            {
                MessageBox.Show("出于安全性考虑，不允许对根目录进行操作！ ", "提示");
                string strAppend = txtInPath.Text.EndsWith("\\") ? "subdirectory" : "\\subdirectory";
                txtInPath.AppendText(strAppend);
                return;
            }
            if (txtInPath.Text.Trim() != strBeforeChange && CancelQue())
            {
                txtInPath.Text = strBeforeChange;
            }
            else
            {
                strBeforeChange = txtInPath.Text.Trim();
            }
            GetMinMax();
            RefreshFNView();
        }

        /// <summary>
        /// 获取文件最大、最小编号
        /// </summary>
        private void GetMinMax()
        {
            int fileNum = 0;
            if (!chbDir.Checked)
            {
                List<FileInfo[]> ltResult = GetFileInfos();
                fileNum = GetMaxLength(ltResult);
            }
            else
            {
                List<DirectoryInfo[]> ltResult = GetDirectoryInfos();
                fileNum = GetMaxLength(ltResult);
            }
            strMax = fileNum.ToString();

            int initialNum;
            string strInitialNum = txtInitialNum.Enabled ? txtInitialNum.Text : "";
            if (Int32.TryParse(strInitialNum, out initialNum))
            {
                initialNum = initialNum < 0 ? 1 : initialNum;
                strMax = (fileNum + initialNum - 1).ToString();
            }
            else
            {
                initialNum = 1;
            }
            if (fileNum == 0)
            {
                strMin = "";
                strMax = "";
                //MessageBox.Show("没有找到要操作的文件或文件路径错误，请修改筛选条件后再试！", "提示");
                return;
            }
            else
            {
                strMin = "";
                for (int i = 0; i < strMax.Length - initialNum.ToString().Length; i++)
                {
                    strMin += "0";
                }
                strMin += initialNum.ToString();
            }
        }

        /// <summary>
        /// 获得List<FileInfo[]>中最大的文件数量
        /// </summary>
        /// <param name="ltResult">待处理的文件信息数组集合</param>
        /// <returns>List<FileInfo[]>中最大的文件数量</returns>
        private int GetMaxLength(List<FileInfo[]> ltResult)
        {
            fileCount = 0;
            if (ltResult == null)
            {
                return 0;
            }
            //int max = ltResult[0].Length;
            int max = 0;
            for (int i = 0; i < ltResult.Count; i++)
            {
                max = max >= ltResult[i].Length ? max : ltResult[i].Length;
                fileCount += ltResult[i].Length;
            }
            return max;
        }
        /// <summary>
        /// 获得List<DirectoryInfo[]>中最多的文件数量
        /// </summary>
        /// <param name="ltResult">待处理的文件夹信息数组集合</param>
        /// <returns>List<DirectoryInfo[]>中最多的文件数量</returns>
        private int GetMaxLength(List<DirectoryInfo[]> ltResult)
        {
            fileCount = 0;
            if (ltResult == null)
            {
                return 0;
            }
            //int max = ltResult[0].Length;
            int max = 0;
            for (int i = 0; i < ltResult.Count; i++)
            {
                max = max >= ltResult[i].Length ? max : ltResult[i].Length;
                fileCount += ltResult[i].Length;
            }
            return max;
        }

        /// <summary>
        /// 根据指定路径获得指定模式下的List<FileInfo[]>
        /// </summary>
        /// <returns>指定模式下的List<FileInfo[]></returns>
        private List<FileInfo[]> GetFileInfos()
        {
            try
            {
                //处理文件类型输入框输入，主要为方便用户输入各种类型的筛选条件
                string strFileType = Regex.Replace(txtFileType.Text.Trim(), @"^\*\.", "", RegexOptions.IgnoreCase);
                if (strFileType == "")
                { strFileType = "*"; }
                if (strFileType.StartsWith("."))
                { strFileType = Regex.Replace(strFileType, @"^\.", "", RegexOptions.IgnoreCase); }
                //输入文件夹路径时如果存在非法字符，则不予处理
                if (txtInPath.Text.Trim() == "" || Regex.IsMatch(txtInPath.Text, @"[\*\?\<\>\|""]") || Regex.IsMatch(txtInPath.Text, @"\\+\/+") || Regex.IsMatch(txtInPath.Text, @"\/+\\+") || Regex.IsMatch(txtInPath.Text, @"\\+\\+") || Regex.IsMatch(txtInPath.Text, @"\/+\/+"))
                { return null; }

                DirectoryInfo difIn = new DirectoryInfo(txtInPath.Text.Trim());
                if (!difIn.Exists)
                {
                    return null;
                }
                List<FileInfo[]> ltFileInfo = new List<FileInfo[]>();
                if (cbOperChildren.Checked == false)
                {
                    //不处理子文件夹
                    FileInfo[] fiIns = txtFileType.Enabled ? difIn.GetFiles("*." + strFileType, SearchOption.TopDirectoryOnly) : difIn.GetFiles();
                    ltFileInfo.Add(fiIns);
                }
                else if (cbIndependChild.Checked == false)
                {
                    //处理子文件夹，统一排序规则
                    FileInfo[] fiIns = txtFileType.Enabled ? difIn.GetFiles("*." + strFileType, SearchOption.AllDirectories) : difIn.GetFiles("*.*", SearchOption.AllDirectories);
                    ltFileInfo.Add(fiIns);
                }
                else
                {
                    //处理子文件夹，各自按规则
                    //List<FileInfo[]> ltFileInfo = new List<FileInfo[]>();
                    FileInfo[] fiIns = txtFileType.Enabled ? difIn.GetFiles("*." + strFileType, SearchOption.TopDirectoryOnly) : difIn.GetFiles();
                    DirectoryInfo[] difChildren = difIn.GetDirectories("*", SearchOption.AllDirectories);
                    foreach (DirectoryInfo dif in difChildren)
                    {
                        FileInfo[] fiChild = txtFileType.Enabled ? dif.GetFiles("*." + strFileType, SearchOption.TopDirectoryOnly) : dif.GetFiles();
                        ltFileInfo.Add(fiChild);
                    }
                    ltFileInfo.Add(fiIns);
                }
                return ltFileInfo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("拒绝访问！" + ex.Message, "警告");
                WriteLog(Application.StartupPath + "\\error.log", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 根据指定路径获得指定模式下的List<DirectoryInfo[]>
        /// </summary>
        /// <returns>指定模式下的List<DirectoryInfo[]></returns>
        private List<DirectoryInfo[]> GetDirectoryInfos()
        {
            try
            {
                //处理匹配文件夹名输入框输入，主要为方便用户输入各种类型的筛选条件
                string strFileType = txtFileType.Text.Trim();
                if (strFileType == "")
                { strFileType = "*"; }
                //输入文件夹路径时如果存在非法字符，则不予处理
                if (txtInPath.Text.Trim() == "" || Regex.IsMatch(txtInPath.Text, @"[\*\?\<\>\|""]") || Regex.IsMatch(txtInPath.Text, @"\\+\/+") || Regex.IsMatch(txtInPath.Text, @"\/+\\+") || Regex.IsMatch(txtInPath.Text, @"\\+\\+") || Regex.IsMatch(txtInPath.Text, @"\/+\/+"))
                { return null; }

                DirectoryInfo difIn = new DirectoryInfo(txtInPath.Text.Trim());
                if (!difIn.Exists)
                {
                    return null;
                }
                List<DirectoryInfo[]> ltDirectoryInfo = new List<DirectoryInfo[]>();
                if (cbOperChildren.Checked == false)
                {
                    //不处理子文件夹
                    DirectoryInfo[] diIns = txtFileType.Enabled ? difIn.GetDirectories("*" + strFileType + "*", SearchOption.TopDirectoryOnly) : difIn.GetDirectories();
                    ltDirectoryInfo.Add(diIns);
                }
                else if (cbIndependChild.Checked == false)
                {
                    //处理子文件夹，统一排序规则
                    DirectoryInfo[] diIns = txtFileType.Enabled ? difIn.GetDirectories("*" + strFileType + "*", SearchOption.AllDirectories) : difIn.GetDirectories("*", SearchOption.AllDirectories);
                    Array.Reverse(diIns);
                    ltDirectoryInfo.Add(diIns);
                }
                else
                {
                    //处理子文件夹，各自按规则
                    ltDirectoryInfo = GetListDirInfos(strFileType, difIn);
                }
                return ltDirectoryInfo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("拒绝访问！" + ex.Message, "警告");
                WriteLog(Application.StartupPath + "\\error.log", ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 子文件夹各自按规则模式，递归取得所有文件夹列表
        /// </summary>
        /// <param name="strFileType">文件夹名匹配字符串</param>
        /// <param name="diIns">待处理的文件夹信息（不含子文件夹）</param>
        /// <returns>待处理的所有文件夹列表</returns>
        private List<DirectoryInfo[]> GetListDirInfos(string strFileType, DirectoryInfo diIns)
        {
            List<DirectoryInfo[]> ltDirInfo = new List<DirectoryInfo[]>();
            DirectoryInfo[] diChild = txtFileType.Enabled ? diIns.GetDirectories("*" + strFileType + "*", SearchOption.TopDirectoryOnly) : diIns.GetDirectories();
            for (int i = 0; i < diChild.Length; i++)
            {
                ltDirInfo.AddRange(GetListDirInfos(strFileType, diChild[i]));
            }
            if (diChild.Length != 0)
            {
                ltDirInfo.Add(diChild);
            }
            return ltDirInfo;
        }


        /// <summary>
        /// 将可能出现的错误写入到日志文件
        /// </summary>
        /// <param name="logFileFullName">日志文件路径</param>
        /// <param name="errMsg">错误信息</param>
        private void WriteLog(string logFileFullName, string errMsg)
        {
            using (StreamWriter sw = new StreamWriter(logFileFullName, true, Encoding.UTF8))
            {
                sw.WriteLine(" " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\t" + errMsg);
            }
        }
        //要处理的文件类型或要匹配的文件夹名复选框单击事件
        private void chbFileType_Click(object sender, EventArgs e)
        {
            if (CancelQue())
            {
                chbFileType.Checked = !chbFileType.Checked;
            } 
            txtFileType.Enabled = chbFileType.Checked;
            GetMinMax();
            RefreshFNView();
        }
        //要处理的文件类型或要匹配的文件夹名文本框TextChanged事件
        private void txtFileType_TextChanged(object sender, EventArgs e)
        {
            if (txtInPath.Text.Trim() != strBeforeChange && CancelQue())
            {
                txtInPath.Text = strFTBeforeChange;
            }
            else
            {
                strFTBeforeChange = txtInPath.Text.Trim();
            }
            GetMinMax();
            RefreshFNView();
        }
        //子文件夹各自按规则复选框单击事件
        private void cbIndependChild_Click(object sender, EventArgs e)
        {
            if (CancelQue())
            {
                cbIndependChild.Checked = !cbIndependChild.Checked;
            } 
            GetMinMax();
            RefreshFNView();
        }
        //自动编号复选框checked改变事件
        private void chbAutoIncrease_CheckedChanged(object sender, EventArgs e)
        {
            lbInitialNum.Enabled = chbAutoIncrease.Checked;
            txtInitialNum.Enabled = chbAutoIncrease.Checked;
            GetMinMax();
            RefreshFNView();
        }
        //起始编号文本框TextChanged事件
        private void txtInitialNum_TextChanged(object sender, EventArgs e)
        {
            GetMinMax();
            RefreshFNView();
        }

        //撤销按钮单击事件，撤销上次更改，方便误操作后回退（只能回退最后一次操作）
        private void btnCancelLast_Click(object sender, EventArgs e)
        {
            btnCancelLast.Enabled = false;
            btnRename.Enabled = false;
            btnSelectFolder.Enabled = false;
            tspbRename.Visible = true;
            if (!chbDir.Checked)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(CancelRename), (object)dicCancel);
            }
            else
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(CancelDirRename), (object)ltDirsCancel);
            }

            //CancelRename(dicCancel);
            //btnCancelLast.Enabled = false;
        }

        /// <summary>
        /// 通过预存的键值对回滚文件操作
        /// </summary>
        /// <param name="dic">预存的键值对 dicCancel</param>
        private void CancelRename(object dic)
        {
            try
            {
                int currentfileNum = 0;
                Dictionary<FileInfo, string> dic1 = (Dictionary<FileInfo, string>)dic;
                foreach (FileInfo fi in dic1.Keys)
                {
                    //fi.MoveTo(dic1[fi]);
                    currentfileNum++;
                    tspbRename.ProgressBar.Invoke(new UpdateUIControlDelegate(UpdateProgressBar), new object[] { (currentfileNum * 100) / dic1.Count });
                    tsslRename.Owner.Invoke(new UpdateUIControlDelegate(UpdateProgressLabel), new object[] { (currentfileNum * 100) / dic1.Count });
                    fi.MoveTo(dic1[fi]);
                }
                MessageBox.Show("撤销文件操作成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("未知错误！" + ex.Message + "请联系开发者以完善程序", "警告");
                WriteLog(Application.StartupPath + "\\error.log", ex.Message);
            }
            finally
            {
                tspbRename.ProgressBar.Invoke(new UpdateUIControlDelegate(UpdateProgressBar), new object[] { 100 });
            }
        }
        /// <summary>
        /// 通过预存的字符串集合回滚文件夹操作
        /// </summary>
        /// <param name="lt">预存的字符串集合 ltDirsCancel</param>
        private void CancelDirRename(object lt)
        {
            try
            {
                int currentfileNum = 0;
                List<string> lt1 = (List<string>)lt;

                List<DirectoryInfo[]> ltCancel = GetDirectoryInfos();
                List<DirectoryInfo> ltDirs = new List<DirectoryInfo>(); ;
                foreach (var di in ltCancel)
                { ltDirs.AddRange(di); }
                if (lt1.Count != ltDirs.Count)
                { 
                    MessageBox.Show("撤销失败");
                    return;
                }
                for (int i = 0; i < ltDirs.Count; i++)
                {
                    //ltDirs[i].MoveTo(Regex.Replace(Regex.Replace(ltDirs[i].FullName, @"\\+$", "", RegexOptions.IgnoreCase), @"\\[^\\]+$", "", RegexOptions.IgnoreCase) + "\\" + lt1[i]);
                    currentfileNum++;
                    tspbRename.ProgressBar.Invoke(new UpdateUIControlDelegate(UpdateProgressBar), new object[] { (currentfileNum * 100) / ltDirs.Count });
                    tsslRename.Owner.Invoke(new UpdateUIControlDelegate(UpdateProgressLabel), new object[] { (currentfileNum * 100) / ltDirs.Count });
                    ltDirs[i].MoveTo(Regex.Replace(Regex.Replace(ltDirs[i].FullName, @"\\+$", "", RegexOptions.IgnoreCase), @"\\[^\\]+$", "", RegexOptions.IgnoreCase) + "\\" + lt1[i]);
                }

                MessageBox.Show("撤销文件夹操作成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("未知错误！" + ex.Message + "请联系开发者以完善程序", "警告");
                WriteLog(Application.StartupPath + "\\error.log", ex.Message);
            }
            finally
            {
                tspbRename.ProgressBar.Invoke(new UpdateUIControlDelegate(UpdateProgressBar), new object[] { 100 });
            }
        }
        //重命名文件夹复选框单击事件
        private void chbDir_Click(object sender, EventArgs e)
        {
            if (CancelQue())
            {
                chbDir.Checked = !chbDir.Checked;
            } 
            if (chbDir.Checked == true)
            {
                gbFileOrDir.Text = "重命名文件夹";
                lbFileNameView.Text = "文件夹名预览";
                chbNewFileName.Text = "使用新文件夹名";
                chbFileType.Text = "指定要匹配的文件夹名：";
                //chbFileType.Enabled = false;

            }
            else if (chbDir.Checked == false)
            {
                gbFileOrDir.Text = "重命名文件";
                lbFileNameView.Text = "文件名预览(不含扩展名)";
                chbNewFileName.Text = "使用新文件名";
                chbFileType.Text = "指定要操作的文件类型：";
                //chbFileType.Enabled = true;
            }
            GetMinMax();
            RefreshFNView();
        }

        //尝试使用XmlSerializer序列化文件信息数组实现深拷贝，未成功，后改用键值对和集合完成
        //private static FileInfo[] DeepCopy(FileInfo[] fis)
        //{
        //    object retval;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        XmlSerializer xml = new XmlSerializer(typeof(FileInfo[]));
        //        xml.Serialize(ms, fis);
        //        ms.Seek(0, SeekOrigin.Begin);
        //        retval = xml.Deserialize(ms);
        //        ms.Close();
        //    }
        //    return (FileInfo[])retval;
        //}
    }
}
