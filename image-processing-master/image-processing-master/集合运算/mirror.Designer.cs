namespace 集合运算
{
    partial class mirror
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startMirror = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.horMirror = new System.Windows.Forms.RadioButton();
            this.verMirror = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startMirror
            // 
            this.startMirror.Location = new System.Drawing.Point(28, 170);
            this.startMirror.Name = "startMirror";
            this.startMirror.Size = new System.Drawing.Size(75, 23);
            this.startMirror.TabIndex = 0;
            this.startMirror.Text = "确定";
            this.startMirror.UseVisualStyleBackColor = true;
            this.startMirror.Click += new System.EventHandler(this.startMirror_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(155, 170);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "退出";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.verMirror);
            this.groupBox1.Controls.Add(this.horMirror);
            this.groupBox1.Location = new System.Drawing.Point(38, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 105);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图像镜像";
            // 
            // horMirror
            // 
            this.horMirror.AutoSize = true;
            this.horMirror.Checked = true;
            this.horMirror.Location = new System.Drawing.Point(40, 35);
            this.horMirror.Name = "horMirror";
            this.horMirror.Size = new System.Drawing.Size(71, 16);
            this.horMirror.TabIndex = 0;
            this.horMirror.TabStop = true;
            this.horMirror.Text = "水平镜像";
            this.horMirror.UseVisualStyleBackColor = true;
            // 
            // verMirror
            // 
            this.verMirror.AutoSize = true;
            this.verMirror.Location = new System.Drawing.Point(40, 70);
            this.verMirror.Name = "verMirror";
            this.verMirror.Size = new System.Drawing.Size(71, 16);
            this.verMirror.TabIndex = 1;
            this.verMirror.TabStop = true;
            this.verMirror.Text = "垂直镜像";
            this.verMirror.UseVisualStyleBackColor = true;
            // 
            // mirror
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 221);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.startMirror);
            this.Name = "mirror";
            this.Text = "图像镜像";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startMirror;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton verMirror;
        private System.Windows.Forms.RadioButton horMirror;
    }
}