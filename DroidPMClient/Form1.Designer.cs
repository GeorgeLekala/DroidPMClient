namespace DroidPMClient
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.txtUname = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdlogin = new System.Windows.Forms.Button();
            this.lblprog = new System.Windows.Forms.Label();
            this.lblcommand = new System.Windows.Forms.Label();
            this.lbldb = new System.Windows.Forms.Label();
            this.cmdLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // txtUname
            // 
            this.txtUname.Location = new System.Drawing.Point(88, 22);
            this.txtUname.Name = "txtUname";
            this.txtUname.Size = new System.Drawing.Size(133, 20);
            this.txtUname.TabIndex = 2;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(88, 49);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(133, 20);
            this.txtPass.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // cmdlogin
            // 
            this.cmdlogin.Location = new System.Drawing.Point(12, 75);
            this.cmdlogin.Name = "cmdlogin";
            this.cmdlogin.Size = new System.Drawing.Size(102, 27);
            this.cmdlogin.TabIndex = 6;
            this.cmdlogin.Text = "Login";
            this.cmdlogin.UseVisualStyleBackColor = true;
            this.cmdlogin.Click += new System.EventHandler(this.cmdlogin_Click);
            // 
            // lblprog
            // 
            this.lblprog.AutoSize = true;
            this.lblprog.Location = new System.Drawing.Point(13, 188);
            this.lblprog.Name = "lblprog";
            this.lblprog.Size = new System.Drawing.Size(48, 13);
            this.lblprog.TabIndex = 8;
            this.lblprog.Text = "Progress";
            // 
            // lblcommand
            // 
            this.lblcommand.AutoSize = true;
            this.lblcommand.Location = new System.Drawing.Point(12, 155);
            this.lblcommand.Name = "lblcommand";
            this.lblcommand.Size = new System.Drawing.Size(54, 13);
            this.lblcommand.TabIndex = 9;
            this.lblcommand.Text = "Command";
            // 
            // lbldb
            // 
            this.lbldb.AutoSize = true;
            this.lbldb.Location = new System.Drawing.Point(13, 123);
            this.lbldb.Name = "lbldb";
            this.lbldb.Size = new System.Drawing.Size(22, 13);
            this.lbldb.TabIndex = 10;
            this.lbldb.Text = "DB";
            // 
            // cmdLogout
            // 
            this.cmdLogout.Location = new System.Drawing.Point(119, 75);
            this.cmdLogout.Name = "cmdLogout";
            this.cmdLogout.Size = new System.Drawing.Size(102, 27);
            this.cmdLogout.TabIndex = 11;
            this.cmdLogout.Text = "Logout";
            this.cmdLogout.UseVisualStyleBackColor = true;
            this.cmdLogout.Click += new System.EventHandler(this.cmdLogout_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 221);
            this.Controls.Add(this.cmdLogout);
            this.Controls.Add(this.lbldb);
            this.Controls.Add(this.lblcommand);
            this.Controls.Add(this.lblprog);
            this.Controls.Add(this.cmdlogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUname);
            this.Name = "Form1";
            this.Text = "PC Commands";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void cmdBlob_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox txtUname;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdlogin;
        private System.Windows.Forms.Label lblprog;
        private System.Windows.Forms.Label lblcommand;
        private System.Windows.Forms.Label lbldb;
        private System.Windows.Forms.Button cmdLogout;
    }
}

