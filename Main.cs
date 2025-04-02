using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Booking_tickets
{
    public partial class Main : Form
    {
        bool i = true; // проверка на создание уже имеющихся колонок в datagridview
        string time; // вспомогательная переменная для извлечения значения времени отбытия поезда
        public Main()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            comboBox_сity_from.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_city_to.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Заполнение ComboBox_сity_from со списком городов отбытия
            DataTable cityFrom = GlobalData.db.ExecuteQuery(GlobalData.CititesListQuery);
            HelperClasses.SetComboBoxDataSource(comboBox_сity_from, cityFrom, "name", "idcities");

            // Заполнение ComboBox_city_to со списком городов прибытия
            DataTable cityTo = GlobalData.db.ExecuteQuery(GlobalData.CititesListQuery);
            HelperClasses.SetComboBoxDataSource(comboBox_city_to, cityTo, "name", "idcities");
        }

        private void pictureBox_exchange_Click(object sender, EventArgs e) //Смена маршрута поезда на обратный
        {
            int value = (int)comboBox_сity_from.SelectedValue;
            comboBox_сity_from.SelectedValue = comboBox_city_to.SelectedValue;
            comboBox_city_to.SelectedValue = value;
        }

        private void CreateColumns_dgw_trains() //Создание колонок для datagridview_trains с отображением доступных поездов
        {
            dataGridView_trains.Columns.Add("trains.idtrains", "id");
            dataGridView_trains.Columns.Add("trains.name", "Поезд");
            dataGridView_trains.Columns.Add("cities.name", "Станция отправления");
            dataGridView_trains.Columns.Add("trains.description", "Услуги и сервисы");
            dataGridView_trains.Columns.Add("travel_time", "Время в пути");
            dataGridView_trains.Columns.Add("departure time", "Время отправления");
            dataGridView_trains.Columns[1].Width = 185;
            dataGridView_trains.Columns[2].Width = 140;
            dataGridView_trains.Columns[3].Width = 240;
            dataGridView_trains.Columns[0].Visible = false;

        }

        private void ReadSingleRow_dwg_trains(DataGridView dwg, IDataRecord record) //Всмомогательный метод для Refresh_dwg_trains()
        {
            // Создание случайного времени отправления поезда
            Random rand = new Random();
            TimeSpan startTime = TimeSpan.FromHours(6); // 06:00
            TimeSpan endTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59)); // 23:59
            TimeSpan randomTime = TimeSpan.FromMinutes(rand.Next((int)startTime.TotalMinutes, (int)endTime.TotalMinutes));

            string formattedTime = randomTime.ToString(@"hh\:mm");

            dwg.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), formattedTime);
        }

        private void Refresh_dwg_trains(DataGridView dwg) //Заполнение колонок в datagridview_trains
        {
            dwg.Rows.Clear();

            string query = "select trains.idtrains, trains.name, stations.name, trains.description, concat(trains_to_routes.travel_time_hours,' ч ',trains_to_routes.travel_time_minutes, ' м') as travel_time from trains\r\njoin stations on trains.id_station = stations.idstations\r\njoin trains_to_routes on trains.idtrains = trains_to_routes.id_train\r\njoin routes on trains_to_routes.id_route = routes.idroutes\r\nwhere routes.id_city_from = @id_city_from and routes.id_city_to = @id_city_to";

            MySqlCommand command = new MySqlCommand(query, GlobalData.db.getConnection());
            command.Parameters.Add("@id_city_from", MySqlDbType.VarChar).Value = (int)comboBox_сity_from.SelectedValue;
            command.Parameters.Add("@id_city_to", MySqlDbType.VarChar).Value = (int)comboBox_city_to.SelectedValue;

            GlobalData.db.openConnection();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow_dwg_trains(dwg, reader);
            }
            reader.Close();

            GlobalData.db.closeConnection();
        }

        private void button_found_tickets_Click(object sender, EventArgs e) // Событие при нажатии на кнопку "Поиск билетов"
        {
            if ((int)comboBox_сity_from.SelectedValue == (int)comboBox_city_to.SelectedValue)
            {
                MessageBox.Show("Пункт отправления и пункт назначения не могут быть одинаковыми. Пожалуйста, выберите разные города.", "Ошибка выбора города", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (i == true)
                {
                    CreateColumns_dgw_trains();
                    i = false;
                }
                Refresh_dwg_trains(dataGridView_trains);
                dataGridView_trains.ClearSelection();

                // Обновить значение маршрута, исходя из выбранных городов
                string routeIdStr = GlobalData.db.ExecuteScalarQuery(GlobalData.CurrentRouteQuery, new Dictionary<string, object>
                {
                  { "@id_city_from", comboBox_сity_from.SelectedValue },
                  { "@id_city_to", comboBox_city_to.SelectedValue }
                });
                GlobalData.id_route = !string.IsNullOrEmpty(routeIdStr) ? Convert.ToInt32(routeIdStr) : -1;

                GlobalData.city_from = comboBox_сity_from.Text;
                GlobalData.city_to = comboBox_city_to.Text;

                // Обновляем параметры билета при поиске нового маршрута (обнуляем предыдущее значение id выбранного поезда)
                GlobalData.id_train = -1;
                GlobalData.departure_time = "";
                time = "";
                label1.Text = ""; 
                label2.Text = "";
            }
        }

        private void dataGridView_trains_CellClick(object sender, DataGridViewCellEventArgs e) // Событие при нажатии на ячейки в dataGridView
        {
            int selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_trains.Rows[selectedRow];

                GlobalData.id_train = Convert.ToInt32(row.Cells[0].Value.ToString());

                GlobalData.train_name = row.Cells[1].Value.ToString();
                GlobalData.station_name = row.Cells[2].Value.ToString();

                time = row.Cells[5].Value.ToString();
                GlobalData.departure_time = dateTimePicker1.Value.ToString("yyyy-MM-dd") + " " + time;

                label1.Text = GlobalData.train_name;
                label2.Text = GlobalData.departure_time;
            }
        }

        private void button_choose_place_Click(object sender, EventArgs e) // Событие при нажатии на кнопку "Выбрать место" - выполняется переход на 2 форму
        {
            if (dataGridView_trains.RowCount == 0)
            {
                MessageBox.Show("Не найдено ни одного поезда.");
            }
            else if (dataGridView_trains.RowCount > 0 && GlobalData.id_train == -1)
            {
                MessageBox.Show("Выберите поезд.");
            }
            else
            {
                DataTable result = GlobalData.db.ExecuteQuery(GlobalData.GetTravelTimeQuery, new Dictionary<string, object>
                {
                  { "@id_train", GlobalData.id_train },
                  { "@id_route", GlobalData.id_route }
                });
                GlobalData.arrival_time = HelperClasses.CalculateArrivalTime(result, GlobalData.departure_time);

                GlobalData.ShowForm(this, GlobalData.form2);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) // Событие при выборе даты в календаре
        {
            if(GlobalData.id_train != -1)
            {
                GlobalData.departure_time = dateTimePicker1.Value.ToString("yyyy-MM-dd") + " " + time;
                label2.Text = GlobalData.departure_time;
            }
        }
    }
}
