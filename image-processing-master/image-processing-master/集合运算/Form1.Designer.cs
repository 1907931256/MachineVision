namespace 集合运算
{
    partial class Form1
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
            this.open = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.translation = new System.Windows.Forms.Button();
            this.mirror = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(37, 46);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(75, 23);
            this.open.TabIndex = 0;
            this.open.Text = "打开";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(37, 98);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // translation
            // 
            this.translation.Location = new System.Drawing.Point(37, 150);
            this.translation.Name = "translation";
            this.translation.Size = new System.Drawing.Size(75, 23);
            this.translation.TabIndex = 2;
            this.translation.Text = "图像平移";
            this.translation.UseVisualStyleBackColor = true;
            this.translation.Click += new System.EventHandler(this.translation_Click);
            // 
            // mirror
            // 
            this.mirror.Location = new System.Drawing.Point(37, 196);
            this.mirror.Name = "mirror";
            this.mirror.Size = new System.Drawing.Size(75, 23);
            this.mirror.TabIndex = 3;
            this.mirror.Text = "图像镜像";
            this.mirror.UseVisualStyleBackColor = true;
            this.mirror.Click += new System.EventHandler(this.mirror_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.mirror);
            this.Controls.Add(this.translation);
            this.Controls.Add(this.close);
            this.Controls.Add(this.open);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button translation;
        private System.Windows.Forms.Button mirror;
    }
}

