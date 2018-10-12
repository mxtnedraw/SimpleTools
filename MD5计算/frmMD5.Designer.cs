namespace MD5计算
{
    partial class frmMD5
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMD5));
            this.cmbEncode = new System.Windows.Forms.ComboBox();
            this.lbSource = new System.Windows.Forms.Label();
            this.txtMD5 = new System.Windows.Forms.TextBox();
            this.txtStrOrFile = new System.Windows.Forms.TextBox();
            this.txtFileInfo = new System.Windows.Forms.TextBox();
            this.lbStr = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.ofdInput = new System.Windows.Forms.OpenFileDialog();
            this.lbFile = new System.Windows.Forms.Label();
            this.lbChar = new System.Windows.Forms.Label();
            this.chbOtherEncode = new System.Windows.Forms.CheckBox();
            this.lbMD5 = new System.Windows.Forms.Label();
            this.lbInfo2 = new System.Windows.Forms.Label();
            this.lbInfo1 = new System.Windows.Forms.Label();
            this.lbChar2 = new System.Windows.Forms.Label();
            this.chbString = new System.Windows.Forms.CheckBox();
            this.rbUpper = new System.Windows.Forms.RadioButton();
            this.rbLower = new System.Windows.Forms.RadioButton();
            this.txtCompare = new System.Windows.Forms.TextBox();
            this.lbCompare = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbEncode
            // 
            this.cmbEncode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncode.Enabled = false;
            this.cmbEncode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbEncode.FormattingEnabled = true;
            this.cmbEncode.Items.AddRange(new object[] {
            "GB2312",
            "UTF8",
            "UTF16",
            "UTF32",
            "UTF7",
            "UBE"});
            this.cmbEncode.Location = new System.Drawing.Point(99, 324);
            this.cmbEncode.Name = "cmbEncode";
            this.cmbEncode.Size = new System.Drawing.Size(121, 25);
            this.cmbEncode.TabIndex = 7;
            // 
            // lbSource
            // 
            this.lbSource.AutoSize = true;
            this.lbSource.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSource.Location = new System.Drawing.Point(-57, 99);
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(59, 17);
            this.lbSource.TabIndex = 10;
            this.lbSource.Text = "原始字符:";
            // 
            // txtMD5
            // 
            this.txtMD5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtMD5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMD5.Location = new System.Drawing.Point(71, 250);
            this.txtMD5.Name = "txtMD5";
            this.txtMD5.ReadOnly = true;
            this.txtMD5.Size = new System.Drawing.Size(259, 23);
            this.txtMD5.TabIndex = 3;
            // 
            // txtStrOrFile
            // 
            this.txtStrOrFile.AllowDrop = true;
            this.txtStrOrFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtStrOrFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStrOrFile.Location = new System.Drawing.Point(71, 27);
            this.txtStrOrFile.Name = "txtStrOrFile";
            this.txtStrOrFile.Size = new System.Drawing.Size(259, 23);
            this.txtStrOrFile.TabIndex = 0;
            this.txtStrOrFile.TextChanged += new System.EventHandler(this.txtStrOrFile_TextChanged);
            this.txtStrOrFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtStrOrFile_DragDrop);
            this.txtStrOrFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtStrOrFile_DragEnter);
            // 
            // txtFileInfo
            // 
            this.txtFileInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFileInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFileInfo.Location = new System.Drawing.Point(71, 71);
            this.txtFileInfo.Multiline = true;
            this.txtFileInfo.Name = "txtFileInfo";
            this.txtFileInfo.ReadOnly = true;
            this.txtFileInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFileInfo.Size = new System.Drawing.Size(337, 159);
            this.txtFileInfo.TabIndex = 2;
            // 
            // lbStr
            // 
            this.lbStr.AutoSize = true;
            this.lbStr.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStr.Location = new System.Drawing.Point(15, 24);
            this.lbStr.Name = "lbStr";
            this.lbStr.Size = new System.Drawing.Size(44, 17);
            this.lbStr.TabIndex = 13;
            this.lbStr.Text = "字符串";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectFile.Location = new System.Drawing.Point(336, 24);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 29);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "选择文件";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // ofdInput
            // 
            this.ofdInput.Filter = "文本文件|*.txt|图片文件|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tga|音频文件|*.mp3;*.ogg;*.wmv;*.aac" +
    ";*.flac;*.ape|视频文件|*.mp4;*.avi;*.mkv;*.rm;*.rmvb;*.mpg;*.vob|所有文件|*.*";
            this.ofdInput.FilterIndex = 5;
            // 
            // lbFile
            // 
            this.lbFile.AutoSize = true;
            this.lbFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFile.Location = new System.Drawing.Point(15, 38);
            this.lbFile.Name = "lbFile";
            this.lbFile.Size = new System.Drawing.Size(44, 17);
            this.lbFile.TabIndex = 13;
            this.lbFile.Text = "或文件";
            // 
            // lbChar
            // 
            this.lbChar.AutoSize = true;
            this.lbChar.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbChar.Location = new System.Drawing.Point(58, 32);
            this.lbChar.Name = "lbChar";
            this.lbChar.Size = new System.Drawing.Size(11, 17);
            this.lbChar.TabIndex = 13;
            this.lbChar.Text = ":";
            // 
            // chbOtherEncode
            // 
            this.chbOtherEncode.AutoSize = true;
            this.chbOtherEncode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chbOtherEncode.Location = new System.Drawing.Point(23, 328);
            this.chbOtherEncode.Name = "chbOtherEncode";
            this.chbOtherEncode.Size = new System.Drawing.Size(75, 21);
            this.chbOtherEncode.TabIndex = 6;
            this.chbOtherEncode.Text = "其他编码";
            this.chbOtherEncode.UseVisualStyleBackColor = true;
            this.chbOtherEncode.CheckedChanged += new System.EventHandler(this.chbOtherEncode_CheckedChanged);
            // 
            // lbMD5
            // 
            this.lbMD5.AutoSize = true;
            this.lbMD5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMD5.Location = new System.Drawing.Point(8, 256);
            this.lbMD5.Name = "lbMD5";
            this.lbMD5.Size = new System.Drawing.Size(55, 17);
            this.lbMD5.TabIndex = 13;
            this.lbMD5.Text = "MD5值 :";
            // 
            // lbInfo2
            // 
            this.lbInfo2.AutoSize = true;
            this.lbInfo2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbInfo2.Location = new System.Drawing.Point(21, 111);
            this.lbInfo2.Name = "lbInfo2";
            this.lbInfo2.Size = new System.Drawing.Size(32, 17);
            this.lbInfo2.TabIndex = 13;
            this.lbInfo2.Text = "信息";
            // 
            // lbInfo1
            // 
            this.lbInfo1.AutoSize = true;
            this.lbInfo1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbInfo1.Location = new System.Drawing.Point(21, 97);
            this.lbInfo1.Name = "lbInfo1";
            this.lbInfo1.Size = new System.Drawing.Size(32, 17);
            this.lbInfo1.TabIndex = 13;
            this.lbInfo1.Text = "文件";
            // 
            // lbChar2
            // 
            this.lbChar2.AutoSize = true;
            this.lbChar2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbChar2.Location = new System.Drawing.Point(56, 104);
            this.lbChar2.Name = "lbChar2";
            this.lbChar2.Size = new System.Drawing.Size(11, 17);
            this.lbChar2.TabIndex = 13;
            this.lbChar2.Text = ":";
            // 
            // chbString
            // 
            this.chbString.AutoSize = true;
            this.chbString.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chbString.Location = new System.Drawing.Point(312, 326);
            this.chbString.Name = "chbString";
            this.chbString.Size = new System.Drawing.Size(99, 21);
            this.chbString.TabIndex = 8;
            this.chbString.Text = "仅字符串模式";
            this.chbString.UseVisualStyleBackColor = true;
            // 
            // rbUpper
            // 
            this.rbUpper.AutoSize = true;
            this.rbUpper.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbUpper.Location = new System.Drawing.Point(348, 236);
            this.rbUpper.Name = "rbUpper";
            this.rbUpper.Size = new System.Drawing.Size(50, 21);
            this.rbUpper.TabIndex = 4;
            this.rbUpper.Text = "大写";
            this.rbUpper.UseVisualStyleBackColor = true;
            this.rbUpper.CheckedChanged += new System.EventHandler(this.rbUpper_CheckedChanged);
            // 
            // rbLower
            // 
            this.rbLower.AutoSize = true;
            this.rbLower.Checked = true;
            this.rbLower.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbLower.Location = new System.Drawing.Point(348, 265);
            this.rbLower.Name = "rbLower";
            this.rbLower.Size = new System.Drawing.Size(50, 21);
            this.rbLower.TabIndex = 5;
            this.rbLower.TabStop = true;
            this.rbLower.Text = "小写";
            this.rbLower.UseVisualStyleBackColor = true;
            // 
            // txtCompare
            // 
            this.txtCompare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCompare.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCompare.Location = new System.Drawing.Point(71, 284);
            this.txtCompare.Name = "txtCompare";
            this.txtCompare.Size = new System.Drawing.Size(259, 23);
            this.txtCompare.TabIndex = 3;
            this.txtCompare.TextChanged += new System.EventHandler(this.txtCompare_TextChanged);
            // 
            // lbCompare
            // 
            this.lbCompare.AutoSize = true;
            this.lbCompare.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCompare.Location = new System.Drawing.Point(12, 287);
            this.lbCompare.Name = "lbCompare";
            this.lbCompare.Size = new System.Drawing.Size(56, 17);
            this.lbCompare.TabIndex = 13;
            this.lbCompare.Text = "比较值：";
            // 
            // frmMD5
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(425, 366);
            this.Controls.Add(this.rbLower);
            this.Controls.Add(this.rbUpper);
            this.Controls.Add(this.chbString);
            this.Controls.Add(this.chbOtherEncode);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.lbChar2);
            this.Controls.Add(this.lbChar);
            this.Controls.Add(this.lbCompare);
            this.Controls.Add(this.lbMD5);
            this.Controls.Add(this.lbInfo2);
            this.Controls.Add(this.lbFile);
            this.Controls.Add(this.lbInfo1);
            this.Controls.Add(this.lbStr);
            this.Controls.Add(this.txtFileInfo);
            this.Controls.Add(this.cmbEncode);
            this.Controls.Add(this.lbSource);
            this.Controls.Add(this.txtCompare);
            this.Controls.Add(this.txtMD5);
            this.Controls.Add(this.txtStrOrFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMD5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MD5计算";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEncode;
        private System.Windows.Forms.Label lbSource;
        private System.Windows.Forms.TextBox txtMD5;
        private System.Windows.Forms.TextBox txtStrOrFile;
        private System.Windows.Forms.TextBox txtFileInfo;
        private System.Windows.Forms.Label lbStr;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.OpenFileDialog ofdInput;
        private System.Windows.Forms.Label lbFile;
        private System.Windows.Forms.Label lbChar;
        private System.Windows.Forms.CheckBox chbOtherEncode;
        private System.Windows.Forms.Label lbMD5;
        private System.Windows.Forms.Label lbInfo2;
        private System.Windows.Forms.Label lbInfo1;
        private System.Windows.Forms.Label lbChar2;
        private System.Windows.Forms.CheckBox chbString;
        private System.Windows.Forms.RadioButton rbUpper;
        private System.Windows.Forms.RadioButton rbLower;
        private System.Windows.Forms.TextBox txtCompare;
        private System.Windows.Forms.Label lbCompare;
    }
}

