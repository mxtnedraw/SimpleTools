namespace 批量重命名
{
    partial class frmFileOper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileOper));
            this.gbFileOrDir = new System.Windows.Forms.GroupBox();
            this.btnCancelLast = new System.Windows.Forms.Button();
            this.txtInitialNum = new System.Windows.Forms.TextBox();
            this.lbInitialNum = new System.Windows.Forms.Label();
            this.lbFileNameView = new System.Windows.Forms.Label();
            this.txtFileNameView = new System.Windows.Forms.TextBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.txtAttach = new System.Windows.Forms.TextBox();
            this.rbback = new System.Windows.Forms.RadioButton();
            this.rbfront = new System.Windows.Forms.RadioButton();
            this.cbIndependChild = new System.Windows.Forms.CheckBox();
            this.cbOperChildren = new System.Windows.Forms.CheckBox();
            this.chbJoinChar = new System.Windows.Forms.CheckBox();
            this.chbAutoIncrease = new System.Windows.Forms.CheckBox();
            this.chbNewFileName = new System.Windows.Forms.CheckBox();
            this.txtJoinChar = new System.Windows.Forms.TextBox();
            this.txtNewFileName = new System.Windows.Forms.TextBox();
            this.txtInPath = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.fbdInPath = new System.Windows.Forms.FolderBrowserDialog();
            this.chbFileType = new System.Windows.Forms.CheckBox();
            this.txtFileType = new System.Windows.Forms.TextBox();
            this.ssProgress = new System.Windows.Forms.StatusStrip();
            this.tspbRename = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslRename = new System.Windows.Forms.ToolStripStatusLabel();
            this.chbDir = new System.Windows.Forms.CheckBox();
            this.gbFileOrDir.SuspendLayout();
            this.ssProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFileOrDir
            // 
            this.gbFileOrDir.BackColor = System.Drawing.Color.Transparent;
            this.gbFileOrDir.Controls.Add(this.btnCancelLast);
            this.gbFileOrDir.Controls.Add(this.txtInitialNum);
            this.gbFileOrDir.Controls.Add(this.lbInitialNum);
            this.gbFileOrDir.Controls.Add(this.lbFileNameView);
            this.gbFileOrDir.Controls.Add(this.txtFileNameView);
            this.gbFileOrDir.Controls.Add(this.btnRename);
            this.gbFileOrDir.Controls.Add(this.txtAttach);
            this.gbFileOrDir.Controls.Add(this.rbback);
            this.gbFileOrDir.Controls.Add(this.rbfront);
            this.gbFileOrDir.Controls.Add(this.cbIndependChild);
            this.gbFileOrDir.Controls.Add(this.cbOperChildren);
            this.gbFileOrDir.Controls.Add(this.chbJoinChar);
            this.gbFileOrDir.Controls.Add(this.chbAutoIncrease);
            this.gbFileOrDir.Controls.Add(this.chbNewFileName);
            this.gbFileOrDir.Controls.Add(this.txtJoinChar);
            this.gbFileOrDir.Controls.Add(this.txtNewFileName);
            this.gbFileOrDir.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbFileOrDir.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gbFileOrDir.Location = new System.Drawing.Point(23, 108);
            this.gbFileOrDir.Name = "gbFileOrDir";
            this.gbFileOrDir.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gbFileOrDir.Size = new System.Drawing.Size(388, 256);
            this.gbFileOrDir.TabIndex = 4;
            this.gbFileOrDir.TabStop = false;
            this.gbFileOrDir.Text = "重命名文件";
            // 
            // btnCancelLast
            // 
            this.btnCancelLast.Enabled = false;
            this.btnCancelLast.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancelLast.Location = new System.Drawing.Point(258, 27);
            this.btnCancelLast.Name = "btnCancelLast";
            this.btnCancelLast.Size = new System.Drawing.Size(87, 34);
            this.btnCancelLast.TabIndex = 6;
            this.btnCancelLast.Text = "撤销";
            this.btnCancelLast.UseVisualStyleBackColor = true;
            this.btnCancelLast.Visible = false;
            this.btnCancelLast.Click += new System.EventHandler(this.btnCancelLast_Click);
            // 
            // txtInitialNum
            // 
            this.txtInitialNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtInitialNum.Enabled = false;
            this.txtInitialNum.Location = new System.Drawing.Point(159, 159);
            this.txtInitialNum.Name = "txtInitialNum";
            this.txtInitialNum.Size = new System.Drawing.Size(60, 23);
            this.txtInitialNum.TabIndex = 16;
            this.txtInitialNum.Text = "1";
            this.txtInitialNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtInitialNum.TextChanged += new System.EventHandler(this.txtInitialNum_TextChanged);
            // 
            // lbInitialNum
            // 
            this.lbInitialNum.AutoSize = true;
            this.lbInitialNum.Enabled = false;
            this.lbInitialNum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbInitialNum.Location = new System.Drawing.Point(102, 162);
            this.lbInitialNum.Name = "lbInitialNum";
            this.lbInitialNum.Size = new System.Drawing.Size(59, 17);
            this.lbInitialNum.TabIndex = 7;
            this.lbInitialNum.Text = "起始编号:";
            // 
            // lbFileNameView
            // 
            this.lbFileNameView.AutoSize = true;
            this.lbFileNameView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbFileNameView.Location = new System.Drawing.Point(21, 44);
            this.lbFileNameView.Name = "lbFileNameView";
            this.lbFileNameView.Size = new System.Drawing.Size(136, 17);
            this.lbFileNameView.TabIndex = 5;
            this.lbFileNameView.Text = "文件名预览(不含扩展名)";
            // 
            // txtFileNameView
            // 
            this.txtFileNameView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFileNameView.Location = new System.Drawing.Point(21, 67);
            this.txtFileNameView.Name = "txtFileNameView";
            this.txtFileNameView.ReadOnly = true;
            this.txtFileNameView.Size = new System.Drawing.Size(344, 23);
            this.txtFileNameView.TabIndex = 7;
            this.txtFileNameView.TabStop = false;
            this.txtFileNameView.Text = "(filename)";
            // 
            // btnRename
            // 
            this.btnRename.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRename.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRename.Location = new System.Drawing.Point(258, 186);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(87, 48);
            this.btnRename.TabIndex = 19;
            this.btnRename.Text = "批量重命名";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // txtAttach
            // 
            this.txtAttach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtAttach.Location = new System.Drawing.Point(258, 128);
            this.txtAttach.Name = "txtAttach";
            this.txtAttach.Size = new System.Drawing.Size(107, 23);
            this.txtAttach.TabIndex = 14;
            this.txtAttach.TextChanged += new System.EventHandler(this.txtNewFileName_TextChanged);
            // 
            // rbback
            // 
            this.rbback.AutoSize = true;
            this.rbback.Checked = true;
            this.rbback.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbback.Location = new System.Drawing.Point(315, 105);
            this.rbback.Name = "rbback";
            this.rbback.Size = new System.Drawing.Size(50, 21);
            this.rbback.TabIndex = 13;
            this.rbback.TabStop = true;
            this.rbback.Text = "后缀";
            this.rbback.UseVisualStyleBackColor = true;
            this.rbback.CheckedChanged += new System.EventHandler(this.txtNewFileName_TextChanged);
            // 
            // rbfront
            // 
            this.rbfront.AutoSize = true;
            this.rbfront.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbfront.Location = new System.Drawing.Point(259, 105);
            this.rbfront.Name = "rbfront";
            this.rbfront.Size = new System.Drawing.Size(50, 21);
            this.rbfront.TabIndex = 12;
            this.rbfront.Text = "前缀";
            this.rbfront.UseVisualStyleBackColor = true;
            this.rbfront.CheckedChanged += new System.EventHandler(this.txtNewFileName_TextChanged);
            // 
            // cbIndependChild
            // 
            this.cbIndependChild.AutoSize = true;
            this.cbIndependChild.Checked = true;
            this.cbIndependChild.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIndependChild.Enabled = false;
            this.cbIndependChild.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbIndependChild.Location = new System.Drawing.Point(21, 224);
            this.cbIndependChild.Name = "cbIndependChild";
            this.cbIndependChild.Size = new System.Drawing.Size(135, 21);
            this.cbIndependChild.TabIndex = 18;
            this.cbIndependChild.Text = "子文件夹各自按规则";
            this.cbIndependChild.UseVisualStyleBackColor = true;
            this.cbIndependChild.Click += new System.EventHandler(this.cbIndependChild_Click);
            // 
            // cbOperChildren
            // 
            this.cbOperChildren.AutoSize = true;
            this.cbOperChildren.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbOperChildren.Location = new System.Drawing.Point(21, 197);
            this.cbOperChildren.Name = "cbOperChildren";
            this.cbOperChildren.Size = new System.Drawing.Size(99, 21);
            this.cbOperChildren.TabIndex = 17;
            this.cbOperChildren.Text = "处理子文件夹";
            this.cbOperChildren.UseVisualStyleBackColor = true;
            this.cbOperChildren.Click += new System.EventHandler(this.cbOperChildren_Click);
            // 
            // chbJoinChar
            // 
            this.chbJoinChar.AutoSize = true;
            this.chbJoinChar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chbJoinChar.Location = new System.Drawing.Point(144, 106);
            this.chbJoinChar.Name = "chbJoinChar";
            this.chbJoinChar.Size = new System.Drawing.Size(99, 21);
            this.chbJoinChar.TabIndex = 10;
            this.chbJoinChar.Text = "使用连接符号";
            this.chbJoinChar.UseVisualStyleBackColor = true;
            this.chbJoinChar.CheckedChanged += new System.EventHandler(this.chbJoinChar_CheckedChanged);
            // 
            // chbAutoIncrease
            // 
            this.chbAutoIncrease.AutoSize = true;
            this.chbAutoIncrease.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chbAutoIncrease.Location = new System.Drawing.Point(21, 161);
            this.chbAutoIncrease.Name = "chbAutoIncrease";
            this.chbAutoIncrease.Size = new System.Drawing.Size(75, 21);
            this.chbAutoIncrease.TabIndex = 15;
            this.chbAutoIncrease.Text = "自动编号";
            this.chbAutoIncrease.UseVisualStyleBackColor = true;
            this.chbAutoIncrease.CheckedChanged += new System.EventHandler(this.chbAutoIncrease_CheckedChanged);
            // 
            // chbNewFileName
            // 
            this.chbNewFileName.AutoSize = true;
            this.chbNewFileName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chbNewFileName.Location = new System.Drawing.Point(21, 105);
            this.chbNewFileName.Name = "chbNewFileName";
            this.chbNewFileName.Size = new System.Drawing.Size(99, 21);
            this.chbNewFileName.TabIndex = 8;
            this.chbNewFileName.Text = "使用新文件名";
            this.chbNewFileName.UseVisualStyleBackColor = true;
            this.chbNewFileName.CheckedChanged += new System.EventHandler(this.chbNewFileName_CheckedChanged);
            // 
            // txtJoinChar
            // 
            this.txtJoinChar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtJoinChar.Enabled = false;
            this.txtJoinChar.Location = new System.Drawing.Point(144, 127);
            this.txtJoinChar.Name = "txtJoinChar";
            this.txtJoinChar.Size = new System.Drawing.Size(75, 23);
            this.txtJoinChar.TabIndex = 11;
            this.txtJoinChar.TextChanged += new System.EventHandler(this.txtNewFileName_TextChanged);
            // 
            // txtNewFileName
            // 
            this.txtNewFileName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtNewFileName.Enabled = false;
            this.txtNewFileName.Location = new System.Drawing.Point(21, 128);
            this.txtNewFileName.Name = "txtNewFileName";
            this.txtNewFileName.Size = new System.Drawing.Size(100, 23);
            this.txtNewFileName.TabIndex = 9;
            this.txtNewFileName.TextChanged += new System.EventHandler(this.txtNewFileName_TextChanged);
            // 
            // txtInPath
            // 
            this.txtInPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtInPath.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInPath.Location = new System.Drawing.Point(23, 25);
            this.txtInPath.Name = "txtInPath";
            this.txtInPath.Size = new System.Drawing.Size(268, 27);
            this.txtInPath.TabIndex = 0;
            this.txtInPath.TextChanged += new System.EventHandler(this.txtInPath_TextChanged);
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectFolder.Location = new System.Drawing.Point(297, 18);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(94, 40);
            this.btnSelectFolder.TabIndex = 1;
            this.btnSelectFolder.Text = "选择文件夹";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // chbFileType
            // 
            this.chbFileType.AutoSize = true;
            this.chbFileType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chbFileType.Location = new System.Drawing.Point(23, 59);
            this.chbFileType.Name = "chbFileType";
            this.chbFileType.Size = new System.Drawing.Size(159, 21);
            this.chbFileType.TabIndex = 2;
            this.chbFileType.Text = "指定要操作的文件类型：";
            this.chbFileType.UseVisualStyleBackColor = true;
            this.chbFileType.Click += new System.EventHandler(this.chbFileType_Click);
            // 
            // txtFileType
            // 
            this.txtFileType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFileType.Enabled = false;
            this.txtFileType.Location = new System.Drawing.Point(189, 58);
            this.txtFileType.Name = "txtFileType";
            this.txtFileType.Size = new System.Drawing.Size(100, 21);
            this.txtFileType.TabIndex = 3;
            this.txtFileType.TextChanged += new System.EventHandler(this.txtFileType_TextChanged);
            // 
            // ssProgress
            // 
            this.ssProgress.BackColor = System.Drawing.Color.Transparent;
            this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbRename,
            this.tsslRename});
            this.ssProgress.Location = new System.Drawing.Point(0, 367);
            this.ssProgress.Name = "ssProgress";
            this.ssProgress.Size = new System.Drawing.Size(437, 22);
            this.ssProgress.SizingGrip = false;
            this.ssProgress.TabIndex = 5;
            this.ssProgress.Text = "statusStrip1";
            // 
            // tspbRename
            // 
            this.tspbRename.BackColor = System.Drawing.SystemColors.Control;
            this.tspbRename.Margin = new System.Windows.Forms.Padding(22, 3, -15, 3);
            this.tspbRename.Name = "tspbRename";
            this.tspbRename.Size = new System.Drawing.Size(100, 16);
            this.tspbRename.Visible = false;
            // 
            // tsslRename
            // 
            this.tsslRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslRename.ForeColor = System.Drawing.SystemColors.GrayText;
            this.tsslRename.Margin = new System.Windows.Forms.Padding(22, 3, 0, 2);
            this.tsslRename.Name = "tsslRename";
            this.tsslRename.Size = new System.Drawing.Size(32, 17);
            this.tsslRename.Text = "就绪";
            // 
            // chbDir
            // 
            this.chbDir.AutoSize = true;
            this.chbDir.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chbDir.Location = new System.Drawing.Point(309, 81);
            this.chbDir.Name = "chbDir";
            this.chbDir.Size = new System.Drawing.Size(99, 21);
            this.chbDir.TabIndex = 6;
            this.chbDir.Text = "重命名文件夹";
            this.chbDir.UseVisualStyleBackColor = true;
            this.chbDir.Click += new System.EventHandler(this.chbDir_Click);
            // 
            // frmFileOper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(437, 389);
            this.Controls.Add(this.chbDir);
            this.Controls.Add(this.ssProgress);
            this.Controls.Add(this.txtFileType);
            this.Controls.Add(this.chbFileType);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.txtInPath);
            this.Controls.Add(this.gbFileOrDir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmFileOper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量重命名";
            this.gbFileOrDir.ResumeLayout(false);
            this.gbFileOrDir.PerformLayout();
            this.ssProgress.ResumeLayout(false);
            this.ssProgress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFileOrDir;
        private System.Windows.Forms.TextBox txtInPath;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.TextBox txtAttach;
        private System.Windows.Forms.RadioButton rbback;
        private System.Windows.Forms.RadioButton rbfront;
        private System.Windows.Forms.CheckBox cbIndependChild;
        private System.Windows.Forms.CheckBox cbOperChildren;
        private System.Windows.Forms.CheckBox chbNewFileName;
        private System.Windows.Forms.TextBox txtNewFileName;
        private System.Windows.Forms.FolderBrowserDialog fbdInPath;
        private System.Windows.Forms.Label lbFileNameView;
        private System.Windows.Forms.TextBox txtFileNameView;
        private System.Windows.Forms.CheckBox chbJoinChar;
        private System.Windows.Forms.CheckBox chbAutoIncrease;
        private System.Windows.Forms.TextBox txtJoinChar;
        private System.Windows.Forms.CheckBox chbFileType;
        private System.Windows.Forms.TextBox txtFileType;
        private System.Windows.Forms.TextBox txtInitialNum;
        private System.Windows.Forms.Label lbInitialNum;
        private System.Windows.Forms.Button btnCancelLast;
        private System.Windows.Forms.StatusStrip ssProgress;
        private System.Windows.Forms.ToolStripProgressBar tspbRename;
        private System.Windows.Forms.ToolStripStatusLabel tsslRename;
        private System.Windows.Forms.CheckBox chbDir;
    }
}

