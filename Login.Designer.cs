namespace Booking_tickets
{
    partial class Login
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_login = new System.Windows.Forms.Button();
            this.pictureBox_show = new System.Windows.Forms.PictureBox();
            this.pictureBox_hide = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_phone = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_show)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_hide)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.button_login);
            this.panel1.Controls.Add(this.pictureBox_show);
            this.panel1.Controls.Add(this.pictureBox_hide);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_password);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox_phone);
            this.panel1.Location = new System.Drawing.Point(129, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 307);
            this.panel1.TabIndex = 0;
            // 
            // button_login
            // 
            this.button_login.BackColor = System.Drawing.Color.LightCoral;
            this.button_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_login.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_login.ForeColor = System.Drawing.Color.White;
            this.button_login.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_login.Location = new System.Drawing.Point(119, 195);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(200, 58);
            this.button_login.TabIndex = 27;
            this.button_login.Text = "Войти";
            this.button_login.UseVisualStyleBackColor = false;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // pictureBox_show
            // 
            this.pictureBox_show.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_show.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_show.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_show.Image = global::Booking_tickets.Properties.Resources.show_new;
            this.pictureBox_show.Location = new System.Drawing.Point(422, 211);
            this.pictureBox_show.Name = "pictureBox_show";
            this.pictureBox_show.Size = new System.Drawing.Size(36, 26);
            this.pictureBox_show.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_show.TabIndex = 28;
            this.pictureBox_show.TabStop = false;
            this.pictureBox_show.Click += new System.EventHandler(this.pictureBox_show_Click);
            this.pictureBox_show.MouseHover += new System.EventHandler(this.pictureBox_show_MouseHover);
            // 
            // pictureBox_hide
            // 
            this.pictureBox_hide.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_hide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_hide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_hide.Image = global::Booking_tickets.Properties.Resources.hide_new;
            this.pictureBox_hide.Location = new System.Drawing.Point(391, 145);
            this.pictureBox_hide.Name = "pictureBox_hide";
            this.pictureBox_hide.Size = new System.Drawing.Size(36, 26);
            this.pictureBox_hide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_hide.TabIndex = 27;
            this.pictureBox_hide.TabStop = false;
            this.pictureBox_hide.Click += new System.EventHandler(this.pictureBox_hide_Click);
            this.pictureBox_hide.MouseHover += new System.EventHandler(this.pictureBox_hide_MouseHover);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(62, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 25;
            this.label2.Text = "ПАРОЛЬ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_password
            // 
            this.textBox_password.BackColor = System.Drawing.Color.White;
            this.textBox_password.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_password.ForeColor = System.Drawing.Color.Black;
            this.textBox_password.Location = new System.Drawing.Point(65, 139);
            this.textBox_password.Multiline = true;
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(368, 38);
            this.textBox_password.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(62, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 21);
            this.label1.TabIndex = 22;
            this.label1.Text = "ВХОД В СИСТЕМУ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_phone
            // 
            this.textBox_phone.BackColor = System.Drawing.Color.White;
            this.textBox_phone.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_phone.ForeColor = System.Drawing.Color.Black;
            this.textBox_phone.Location = new System.Drawing.Point(65, 70);
            this.textBox_phone.Multiline = true;
            this.textBox_phone.Name = "textBox_phone";
            this.textBox_phone.Size = new System.Drawing.Size(368, 38);
            this.textBox_phone.TabIndex = 23;
            this.textBox_phone.Enter += new System.EventHandler(this.textBox_phone_Enter);
            this.textBox_phone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_phone_KeyDown);
            this.textBox_phone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_phone_KeyPress);
            this.textBox_phone.Leave += new System.EventHandler(this.textBox_phone_Leave);
            this.textBox_phone.MouseEnter += new System.EventHandler(this.textBox_phone_MouseEnter);
            this.textBox_phone.MouseLeave += new System.EventHandler(this.textBox_phone_MouseLeave);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Booking_tickets.Properties.Resources.login_logo1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(713, 431);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Shown += new System.EventHandler(this.Login_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_show)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_hide)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox_show;
        private System.Windows.Forms.PictureBox pictureBox_hide;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_phone;
        private System.Windows.Forms.Button button_login;
    }
}