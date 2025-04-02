namespace Booking_tickets
{
    partial class Add_Service
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
            this.button_add_service = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.checkedListBox_services = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_discount_amount = new System.Windows.Forms.Label();
            this.label_total_ticket_cost = new System.Windows.Forms.Label();
            this.label_service_cost = new System.Windows.Forms.Label();
            this.label_services_list = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_ID_ticket = new System.Windows.Forms.Label();
            this.label_route = new System.Windows.Forms.Label();
            this.label_city_to = new System.Windows.Forms.Label();
            this.label_station_name = new System.Windows.Forms.Label();
            this.label_seat_number = new System.Windows.Forms.Label();
            this.label_wagon_name = new System.Windows.Forms.Label();
            this.label_seat_cost = new System.Windows.Forms.Label();
            this.label_train_name = new System.Windows.Forms.Label();
            this.label_arrival_time = new System.Windows.Forms.Label();
            this.label_departure_time = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button_add_service
            // 
            this.button_add_service.BackColor = System.Drawing.Color.White;
            this.button_add_service.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_add_service.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_add_service.Font = new System.Drawing.Font("Book Antiqua", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_add_service.Location = new System.Drawing.Point(17, 306);
            this.button_add_service.Name = "button_add_service";
            this.button_add_service.Size = new System.Drawing.Size(152, 39);
            this.button_add_service.TabIndex = 25;
            this.button_add_service.Text = "Добавить";
            this.button_add_service.UseVisualStyleBackColor = false;
            this.button_add_service.Click += new System.EventHandler(this.button_add_service_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Book Antiqua", 17.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(320, 32);
            this.label3.TabIndex = 24;
            this.label3.Text = "Дополнительные услуги:";
            // 
            // checkedListBox_services
            // 
            this.checkedListBox_services.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox_services.Font = new System.Drawing.Font("Book Antiqua", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBox_services.FormattingEnabled = true;
            this.checkedListBox_services.Location = new System.Drawing.Point(17, 140);
            this.checkedListBox_services.Name = "checkedListBox_services";
            this.checkedListBox_services.Size = new System.Drawing.Size(290, 78);
            this.checkedListBox_services.TabIndex = 27;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label_discount_amount);
            this.panel1.Controls.Add(this.label_total_ticket_cost);
            this.panel1.Controls.Add(this.label_service_cost);
            this.panel1.Controls.Add(this.label_services_list);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label_route);
            this.panel1.Controls.Add(this.label_city_to);
            this.panel1.Controls.Add(this.label_station_name);
            this.panel1.Controls.Add(this.label_seat_number);
            this.panel1.Controls.Add(this.label_wagon_name);
            this.panel1.Controls.Add(this.label_seat_cost);
            this.panel1.Controls.Add(this.label_train_name);
            this.panel1.Controls.Add(this.label_arrival_time);
            this.panel1.Controls.Add(this.label_departure_time);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Location = new System.Drawing.Point(2, 352);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 299);
            this.panel1.TabIndex = 29;
            // 
            // label_discount_amount
            // 
            this.label_discount_amount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_discount_amount.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_discount_amount.ForeColor = System.Drawing.Color.ForestGreen;
            this.label_discount_amount.Location = new System.Drawing.Point(687, 274);
            this.label_discount_amount.Name = "label_discount_amount";
            this.label_discount_amount.Size = new System.Drawing.Size(398, 23);
            this.label_discount_amount.TabIndex = 33;
            this.label_discount_amount.Text = "экономия по скидке (р)";
            // 
            // label_total_ticket_cost
            // 
            this.label_total_ticket_cost.AutoSize = true;
            this.label_total_ticket_cost.Font = new System.Drawing.Font("Bookman Old Style", 12.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_total_ticket_cost.ForeColor = System.Drawing.Color.Black;
            this.label_total_ticket_cost.Location = new System.Drawing.Point(687, 249);
            this.label_total_ticket_cost.Name = "label_total_ticket_cost";
            this.label_total_ticket_cost.Size = new System.Drawing.Size(227, 21);
            this.label_total_ticket_cost.TabIndex = 59;
            this.label_total_ticket_cost.Text = "Общая стоимость билета";
            this.label_total_ticket_cost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_service_cost
            // 
            this.label_service_cost.AutoSize = true;
            this.label_service_cost.Font = new System.Drawing.Font("Bookman Old Style", 12.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_service_cost.ForeColor = System.Drawing.Color.Black;
            this.label_service_cost.Location = new System.Drawing.Point(6, 249);
            this.label_service_cost.Name = "label_service_cost";
            this.label_service_cost.Size = new System.Drawing.Size(152, 21);
            this.label_service_cost.TabIndex = 58;
            this.label_service_cost.Text = "Стоимость услуг";
            // 
            // label_services_list
            // 
            this.label_services_list.AutoSize = true;
            this.label_services_list.BackColor = System.Drawing.Color.Transparent;
            this.label_services_list.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_services_list.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label_services_list.Location = new System.Drawing.Point(4, 152);
            this.label_services_list.Name = "label_services_list";
            this.label_services_list.Size = new System.Drawing.Size(67, 20);
            this.label_services_list.TabIndex = 57;
            this.label_services_list.Text = "-Услуги";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 12.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 22);
            this.label1.TabIndex = 55;
            this.label1.Text = "Услуги, входящие в билет:";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Booking_tickets.Properties.Resources.ticket_id_logo1;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Controls.Add(this.label_ID_ticket);
            this.panel2.Location = new System.Drawing.Point(10, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(187, 125);
            this.panel2.TabIndex = 54;
            // 
            // label_ID_ticket
            // 
            this.label_ID_ticket.BackColor = System.Drawing.Color.Transparent;
            this.label_ID_ticket.Font = new System.Drawing.Font("Book Antiqua", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ID_ticket.ForeColor = System.Drawing.Color.White;
            this.label_ID_ticket.Location = new System.Drawing.Point(108, 42);
            this.label_ID_ticket.Name = "label_ID_ticket";
            this.label_ID_ticket.Size = new System.Drawing.Size(64, 28);
            this.label_ID_ticket.TabIndex = 53;
            this.label_ID_ticket.Text = "ID";
            // 
            // label_route
            // 
            this.label_route.AutoSize = true;
            this.label_route.BackColor = System.Drawing.Color.Transparent;
            this.label_route.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_route.Location = new System.Drawing.Point(739, 79);
            this.label_route.Name = "label_route";
            this.label_route.Size = new System.Drawing.Size(76, 19);
            this.label_route.TabIndex = 52;
            this.label_route.Text = "маршрут";
            // 
            // label_city_to
            // 
            this.label_city_to.AutoSize = true;
            this.label_city_to.BackColor = System.Drawing.Color.Transparent;
            this.label_city_to.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_city_to.Location = new System.Drawing.Point(496, 79);
            this.label_city_to.Name = "label_city_to";
            this.label_city_to.Size = new System.Drawing.Size(132, 19);
            this.label_city_to.TabIndex = 51;
            this.label_city_to.Text = "город прибытия";
            // 
            // label_station_name
            // 
            this.label_station_name.AutoSize = true;
            this.label_station_name.BackColor = System.Drawing.Color.Transparent;
            this.label_station_name.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_station_name.Location = new System.Drawing.Point(245, 79);
            this.label_station_name.Name = "label_station_name";
            this.label_station_name.Size = new System.Drawing.Size(172, 19);
            this.label_station_name.TabIndex = 50;
            this.label_station_name.Text = "станция отправления";
            // 
            // label_seat_number
            // 
            this.label_seat_number.AutoSize = true;
            this.label_seat_number.BackColor = System.Drawing.Color.Transparent;
            this.label_seat_number.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_seat_number.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label_seat_number.Location = new System.Drawing.Point(954, 78);
            this.label_seat_number.Name = "label_seat_number";
            this.label_seat_number.Size = new System.Drawing.Size(51, 19);
            this.label_seat_number.TabIndex = 49;
            this.label_seat_number.Text = "место";
            // 
            // label_wagon_name
            // 
            this.label_wagon_name.AutoSize = true;
            this.label_wagon_name.BackColor = System.Drawing.Color.Transparent;
            this.label_wagon_name.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_wagon_name.Location = new System.Drawing.Point(954, 43);
            this.label_wagon_name.Name = "label_wagon_name";
            this.label_wagon_name.Size = new System.Drawing.Size(131, 19);
            this.label_wagon_name.TabIndex = 48;
            this.label_wagon_name.Text = "название вагона";
            // 
            // label_seat_cost
            // 
            this.label_seat_cost.AutoSize = true;
            this.label_seat_cost.Font = new System.Drawing.Font("Bookman Old Style", 12.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_seat_cost.ForeColor = System.Drawing.Color.Black;
            this.label_seat_cost.Location = new System.Drawing.Point(426, 249);
            this.label_seat_cost.Name = "label_seat_cost";
            this.label_seat_cost.Size = new System.Drawing.Size(159, 21);
            this.label_seat_cost.TabIndex = 0;
            this.label_seat_cost.Text = "Стоимость места";
            this.label_seat_cost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_train_name
            // 
            this.label_train_name.AutoSize = true;
            this.label_train_name.BackColor = System.Drawing.Color.Transparent;
            this.label_train_name.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_train_name.Location = new System.Drawing.Point(739, 44);
            this.label_train_name.Name = "label_train_name";
            this.label_train_name.Size = new System.Drawing.Size(132, 19);
            this.label_train_name.TabIndex = 47;
            this.label_train_name.Text = "название поезда";
            // 
            // label_arrival_time
            // 
            this.label_arrival_time.AutoSize = true;
            this.label_arrival_time.BackColor = System.Drawing.Color.Transparent;
            this.label_arrival_time.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_arrival_time.Location = new System.Drawing.Point(496, 44);
            this.label_arrival_time.Name = "label_arrival_time";
            this.label_arrival_time.Size = new System.Drawing.Size(136, 19);
            this.label_arrival_time.TabIndex = 46;
            this.label_arrival_time.Text = "время прибытия";
            // 
            // label_departure_time
            // 
            this.label_departure_time.AutoSize = true;
            this.label_departure_time.BackColor = System.Drawing.Color.Transparent;
            this.label_departure_time.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_departure_time.Location = new System.Drawing.Point(245, 44);
            this.label_departure_time.Name = "label_departure_time";
            this.label_departure_time.Size = new System.Drawing.Size(155, 19);
            this.label_departure_time.TabIndex = 45;
            this.label_departure_time.Text = "время отправления";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(954, 8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(144, 28);
            this.label13.TabIndex = 44;
            this.label13.Text = "Расположение";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(738, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 28);
            this.label12.TabIndex = 43;
            this.label12.Text = "Поезд";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(495, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(214, 28);
            this.label11.TabIndex = 42;
            this.label11.Text = "Прибытие, местное время";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(244, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(235, 28);
            this.label10.TabIndex = 41;
            this.label10.Text = "Отправление, местное время";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Booking_tickets.Properties.Resources.services;
            this.pictureBox2.Location = new System.Drawing.Point(12, 22);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(610, 78);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // Add_Service
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1104, 653);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkedListBox_services);
            this.Controls.Add(this.button_add_service);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Add_Service";
            this.Text = "Add_Service";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Add_Service_FormClosing);
            this.Load += new System.EventHandler(this.Add_Service_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_add_service;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckedListBox checkedListBox_services;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_seat_cost;
        private System.Windows.Forms.Label label_route;
        private System.Windows.Forms.Label label_city_to;
        private System.Windows.Forms.Label label_station_name;
        private System.Windows.Forms.Label label_seat_number;
        private System.Windows.Forms.Label label_wagon_name;
        private System.Windows.Forms.Label label_train_name;
        private System.Windows.Forms.Label label_arrival_time;
        private System.Windows.Forms.Label label_departure_time;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label_ID_ticket;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_services_list;
        private System.Windows.Forms.Label label_service_cost;
        private System.Windows.Forms.Label label_total_ticket_cost;
        private System.Windows.Forms.Label label_discount_amount;
    }
}