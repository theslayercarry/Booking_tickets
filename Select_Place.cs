using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Booking_tickets
{
    public partial class Select_Place: Form
    {
        public Select_Place()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            comboBox_wagons.DropDownStyle = ComboBoxStyle.DropDownList;
            panel_with_wagon_picture.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void Select_Place_Load(object sender, EventArgs e)
        {
            // Заполнение ComboBox_wagons со списком доступных вагонов
            DataTable wagons = GlobalData.db.ExecuteQuery(GlobalData.WagonsListQuery);
            HelperClasses.SetComboBoxDataSource(comboBox_wagons, wagons, "name", "idwagons");

            // Вызов метода для обновления мест в зависимости от выбранного вагона
            comboBox_wagons_SelectedIndexChanged(null, EventArgs.Empty);

        }

        private void comboBox_wagons_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedWagon = comboBox_wagons.SelectedValue.ToString(); // id выбранного вагона в ComboBox
            string imagePath; // путь к изображениям с вагонами
            int seatCount = 0; // количество мест в вагоне

            // Массив с расположением мест в вагоне
            Point[] selectedSeats = new Point[0];

            // Словарь с данными о вагонах
            var wagonsData = new Dictionary<string, (int seatCount, string imagePath, Point[] seatPositions)>
            {
                { "1", (36, Path.Combine(Directory.GetCurrentDirectory(), "Resources", "platzcart.png"), GlobalData.seatPositions_platzcart) },
                { "2", (24, Path.Combine(Directory.GetCurrentDirectory(), "Resources", "kupe.png"), GlobalData.seatPositions_kupe) },
                { "3", (8, Path.Combine(Directory.GetCurrentDirectory(), "Resources", "spalnii_vagon.png"), GlobalData.seatPositions_spalnii_vagon) }
            };

            // Проверяем, есть ли информация о выбранном вагоне
            if (wagonsData.TryGetValue(selectedWagon, out var wagonInfo))
            {
                seatCount = wagonInfo.seatCount;
                imagePath = wagonInfo.imagePath;
                selectedSeats = wagonInfo.seatPositions;

                panel_with_wagon_picture.BackgroundImage = Image.FromFile(imagePath);
            }

            // Устанавливаем положение мест в зависимости от вагона
            SetSeatLocation(selectedSeats);
            // Показываем нужные и скрываем ненужные места
            SetSeatVisibility(seatCount);


            // Получение цены выбранного вагона
            GlobalData.seat_cost = GlobalData.db.ExecuteScalarQuery(GlobalData.TicketCostQuery, new Dictionary<string, object>
            {
                  { "@idwagon", comboBox_wagons.SelectedValue }
            });
            label_cost.Text = $"{GlobalData.seat_cost} ₽";
        }

        private void SetSeatVisibility(int seatCount)
        {
            // Перебираем все элементы управления внутри панели, которая содержит кнопки
            foreach (Control control in panel_with_wagon_picture.Controls)
            {
                // Проверяем, является ли текущий элемент RadioButton
                if (control is RadioButton radioButton)
                {
                    // Разделяем имя радиокнопки по символу "_", чтобы получить номер
                    string[] parts = radioButton.Name.Split('_');

                    // Проверяем, что полученный массив parts состоит из двух частей, например ("radioButton", "1") и преобразовываем вторую строку в число
                    if (parts.Length == 2 && int.TryParse(parts[1], out int buttonNumber))
                    {
                        // Если номер кнопки больше, чем количество мест, скрываем её
                        if (buttonNumber > seatCount)
                        {
                            radioButton.Visible = false;  // Скрываем кнопку
                        }
                        else
                        {
                            radioButton.Visible = true;   // Показываем кнопку
                        }
                    }
                }
            }
        }

        private void SetSeatLocation (Point[] selectedSeats)
        {
            // Расположим RadioButton в зависимости от выбранного типа вагона
            for (int i = 0; i < selectedSeats.Length; i++)
            {
                // Находим RadioButton с именем "radioButton_1", "radioButton_2", и т.д.
                string radioButtonName = $"radioButton_{i + 1}";
                RadioButton radioButton = panel_with_wagon_picture.Controls.Find(radioButtonName, true).FirstOrDefault() as RadioButton;

                if (radioButton != null)
                {
                    // Устанавливаем координаты для каждого RadioButton
                    radioButton.Location = selectedSeats[i];
                    radioButton.Visible = true; 
                }
            }
        }

        private void SetSeatNumber()
        {
            // Перебираем все элементы управления внутри панели, которая содержит кнопки
            foreach (Control control in panel_with_wagon_picture.Controls)
            {
                // Проверяем, является ли текущий элемент RadioButton
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    // Извлекаем номер места из имени кнопки (например, radioButton_12 → 12)
                    string seatNumberString = radioButton.Name.Replace("radioButton_", "");
                    if (int.TryParse(seatNumberString, out int seatNumber))
                    {
                        GlobalData.seat_number = seatNumber;
                    }
                    break; 
                }
            }
        }

        private void button_confirm_seat_Click(object sender, EventArgs e) // Событие при нажатии на кнопку "Продолжить" - выполняется переход на 3 форму
        {
            SetSeatNumber();

            if (seat_availability_check())
            {
                return;
            }
            else
            {
                GlobalData.id_wagon = (int)comboBox_wagons.SelectedValue;
                GlobalData.wagon_name = comboBox_wagons.Text;

                GlobalData.ShowForm(this, GlobalData.form3);
            }
        }

        private Boolean seat_availability_check() //Проверка доступности места
        {

            DataTable table = GlobalData.db.ExecuteQuery(GlobalData.GetSeatAvailabilityQuery, new Dictionary<string, object>
            {
                  { "@id_route", GlobalData.id_route },
                  { "@id_train", GlobalData.id_train },
                  { "@id_wagon", comboBox_wagons.SelectedValue },
                  { "@seat_number", GlobalData.seat_number }

            });

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Это место уже продано.");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
