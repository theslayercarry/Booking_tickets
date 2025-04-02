using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace Booking_tickets
{
    public partial class Verification_Data: Form
    {
        public Verification_Data()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Verification_Data_Load(object sender, EventArgs e)
        {
            LoadFullData();
        }

        private void LoadFullData()
        {
            label_name.Text = GlobalData.passenger_name;
            label_surname.Text = GlobalData.passenger_surname;
            label_patronymic.Text = GlobalData.passenger_patronymic;
            label_birthday.Text = GlobalData.passenger_date_of_birth;
            label_passport.Text = GlobalData.passenger_passport_series + " " + GlobalData.passenger_passport_number;
            label_phone.Text = GlobalData.passenger_phone;
            label_email.Text = GlobalData.passenger_email;

            label_departure_time.Text = GlobalData.departure_time;
            label_arrival_time.Text = GlobalData.arrival_time;
            label_train_name.Text = GlobalData.train_name;
            label_wagon_name.Text = GlobalData.wagon_name;
            label_station_name.Text = GlobalData.station_name;
            label_seat_number.Text = GlobalData.seat_number.ToString() + " место";
            label_city_to.Text = GlobalData.city_to;
            label_route.Text = GlobalData.city_from + " - " + GlobalData.city_to;
        }

        private void button_continue_Click(object sender, EventArgs e)
        {
            // Вызов метода для сохранения данных в бд
            if (TicketRegisterTransaction())
            {
                return;
            }
            else
            {
                GlobalData.ShowForm(this, GlobalData.form5);
            }
        }

        private Boolean TicketRegisterTransaction()
        {
            using (MySqlConnection connection = GlobalData.db.getConnection()) // Используем соединение из класса Database
            {
                GlobalData.db.openConnection(); // Открываем соединение
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    if (!Input_Data.authorization_verification)
                    {
                        // 1️) Добавляем пассажира
                        string passengerIdStr = GlobalData.db.ExecuteScalarQuery(GlobalData.NewPassengerInsertQuery, new Dictionary<string, object>
                        {
                              { "@name", GlobalData.passenger_name },
                              { "@surname", GlobalData.passenger_surname },
                              { "@patronymic", GlobalData.passenger_patronymic },
                              { "@id_gender", GlobalData.id_passenger_gender },
                              { "@date_of_birth", GlobalData.passenger_date_of_birth },
                              { "@passport_series", GlobalData.passenger_passport_series },
                              { "@passport_number", GlobalData.passenger_passport_number },
                              { "@phone", GlobalData.passenger_phone },
                              { "@email", GlobalData.passenger_email },
                              { "@password", HelperClasses.GetPass() },

                        }, transaction);

                        GlobalData.last_id_passenger = string.IsNullOrEmpty(passengerIdStr) ? -1 : Convert.ToInt32(passengerIdStr); // Получаем ID добавленного пассажира
                    }

                    // 2) Получаем общую сумму средств, потраченных пассажиром (для расчёта скидки)
                    string TotalPassengerSpentStr = GlobalData.db.ExecuteScalarQuery(GlobalData.TotalPassengerSpentQuery, new Dictionary<string, object>
                    {
                          { "@id_passenger", GlobalData.last_id_passenger }

                    }, transaction);

                    GlobalData.total_passenger_spent = string.IsNullOrEmpty(TotalPassengerSpentStr) ? "0" : TotalPassengerSpentStr;

                    // 3) Добавляем билет
                    string ticketIdStr = GlobalData.db.ExecuteScalarQuery(GlobalData.NewTicketInsertQuery, new Dictionary<string, object>
                    {
                          { "@id_passenger", GlobalData.last_id_passenger },
                          { "@id_train", GlobalData.id_train },
                          { "@id_route", GlobalData.id_route },
                          { "@id_wagon", GlobalData.id_wagon },
                          { "@departure_time", GlobalData.departure_time },
                          { "@arrival_time", GlobalData.arrival_time },
                          { "@ticket_cost", GlobalData.seat_cost },
                          { "@seat_number", GlobalData.seat_number },

                    }, transaction);

                    GlobalData.last_id_ticket = string.IsNullOrEmpty(ticketIdStr) ? -1 : Convert.ToInt32(ticketIdStr); // Получаем ID добавленного билета

                    // Подтверждаем транзакцию
                    transaction.Commit();

                    // 4️) Выводим сообщение пользователю
                    MessageBox.Show($"Билет №{GlobalData.last_id_ticket} успешно забронирован!\n" +
                        $"Пассажир: {GlobalData.passenger_surname} {GlobalData.passenger_name} {GlobalData.passenger_patronymic}\n" +
                        $"Паспорт: {GlobalData.passenger_passport_series} {GlobalData.passenger_passport_number}\n" +
                        $"Поезд: {GlobalData.train_name}\n" +
                        $"Место: {GlobalData.wagon_name}, {GlobalData.seat_number} место\n" +
                        $"Отправление: {GlobalData.station_name}, {GlobalData.departure_time}\n" +
                        $"Прибытие: {GlobalData.city_to}, {GlobalData.arrival_time}", "Регистрация успешна");

                    return false;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Откатываем, если ошибка
                    MessageBox.Show("Ошибка при бронировании билета: " + ex.Message, "Ошибка");
                    return true;
                }
                finally
                {
                    GlobalData.db.closeConnection(); // Закрываем соединение после завершения работы
                }
            }
        }

    }
}
