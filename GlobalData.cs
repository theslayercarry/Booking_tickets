using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Booking_tickets
{
    public class GlobalData
    {
        // Экземпляр базы данных
        public static Database db { get; } = new Database();

        // Глобальные переменные для хранения данных о билете
        public static string city_from { get; set; } // название города отправления
        public static string city_to { get; set; } // название города прибытия
        public static int id_route { get; set; } // id маршрута
        public static int id_train { get; set; } // id поезда
        public static string departure_time { get; set; } // время отправления
        public static string arrival_time { get; set; } // время прибытия
        public static string train_name { get; set; } // название поезда
        public static string station_name { get; set; } // название станции
        public static int id_wagon { get; set; } // id вагона
        public static string wagon_name { get; set; } // название вагона
        public static int seat_number { get; set; } // место посадки
        public static string seat_cost { get; set; } // стоимость билета
        public static int last_id_ticket { get; set; } // id только что добавленного билета (для соблюдения порядка добавления данных в бд)

        // Глобальные переменные для хранения данных о пассажире
        public static int id_passenger_gender { get; set; } // id пола пассажира (мужской/женский)
        public static string passenger_name { get; set; } // имя пассажира
        public static string passenger_surname { get; set; } // фамилия пассажира
        public static string passenger_patronymic { get; set; } // отчество пассажира
        public static string passenger_date_of_birth { get; set; } // дата рождения пассажира
        public static string passenger_passport_series { get; set; } // серия паспорта пассажира
        public static string passenger_passport_number { get; set; } // номер паспорта пассажира
        public static string passenger_phone { get; set; } // номер телефона пассажира
        public static string passenger_email { get; set; } // email пассажира
        public static string total_passenger_spent { get; set; } // общая сумма средств, потраченная пассажиром
        public static int last_id_passenger { get; set; } = -1; //id только что добавленного пассажира (для соблюдения порядка добавления данных в бд)
                                                                // -1 по умолчанию (пользователь не авторизован)

        
        // Управление экземплярами всех имеющихся форм
        public static Main form1;
        public static Select_Place form2;
        public static Input_Data form3;
        public static Verification_Data form4;
        public static Add_Service form5;

        // Инициализация форм
        public static void InitForms()
        {
            form1 = new Main();
            form2 = new Select_Place();
            form3 = new Input_Data();
            form4 = new Verification_Data();
            form5 = new Add_Service();
        }
        public static void ShowForm(Form current, Form next)
        {
            current.Hide();  // Скрываем текущую форму
            next.ShowDialog(); // Показываем следующую форму
            current.Show();  // Возвращаемся к предыдущей
        }


        // Координаты мест в вагонах
        public static Point[] seatPositions_spalnii_vagon = new Point[] // (пример для спального вагона)
        {
             new Point(342, 126), new Point(342, 65), new Point(569, 126), new Point(569, 65), // с 1 по 4
             new Point(662, 126), new Point(662, 65), new Point(890, 126), new Point(890, 65)  // с 5 по 8
        };

        public static Point[] seatPositions_kupe = new Point[] // (пример для купе)
        {
             new Point(228, 125), new Point(228, 71), new Point(281, 125), new Point(281, 71), new Point(356, 126), new Point(356, 71), // с 1 по 6
             new Point(409, 126), new Point(409, 71), new Point(484, 126), new Point(484, 71), new Point(539, 125), new Point(539, 71), // с 7 по 12
             new Point(614, 125), new Point(614, 71), new Point(669, 125), new Point(669, 71), new Point(743, 125), new Point(743, 71), // с 13 по 18
             new Point(797, 126), new Point(797, 71), new Point(871, 126), new Point(871, 71), new Point(924, 126), new Point(924, 71), // с 19 по 24
        };

        public static Point[] seatPositions_platzcart = new Point[] // (пример для плацкарта)
        {
             new Point(228, 125), new Point(228, 71), new Point(281, 125), new Point(281, 71), new Point(356, 126), new Point(356, 71), // с 1 по 6
             new Point(409, 126), new Point(409, 71), new Point(484, 126), new Point(484, 71), new Point(539, 125), new Point(539, 71), // с 7 по 12
             new Point(614, 125), new Point(614, 71), new Point(669, 125), new Point(669, 71), new Point(743, 125), new Point(743, 71), // с 13 по 18
             new Point(797, 126), new Point(797, 71), new Point(871, 126), new Point(871, 71), new Point(924, 126), new Point(924, 71), // с 19 по 24
             new Point(871, 202), new Point(924, 202), new Point(743, 201), new Point(797, 202), new Point(614, 201), new Point(669, 201), // с 25 по 30
             new Point(484, 202), new Point(539, 201), new Point(356, 202), new Point(409, 202), new Point(228, 201), new Point(281, 201), // с 31 по 36
        };

        // Sql - запросы к базе данных:
        // Получение стоимости билета в выбранном вагоне
        public static string TicketCostQuery = "select cost from wagons where idwagons = @idwagon";

        // Получение списка вагонов
        public static string WagonsListQuery = "select idwagons, name from wagons";

        // Получение списка городов
        public static string CititesListQuery = "select idcities, name from cities";

        // Получение маршрута поезда, исходя из выбранных городов
        public static string CurrentRouteQuery = "select idroutes from routes where id_city_from = @id_city_from and id_city_to = @id_city_to";

        // Получение времени, которое занимает маршрут поезда (часов и минут)
        public static string GetTravelTimeQuery = "select travel_time_hours, travel_time_minutes from trains_to_routes where id_train = @id_train and id_route = @id_route";

        // Запрос на проверку доступности билета
        public static string GetSeatAvailabilityQuery = "select * from tickets where id_route = @id_route and id_train = @id_train and id_wagon = @id_wagon and seat_number = @seat_number";

        // Получение списка полов (мужской/женский)
        public static string LoadGenderListQuery = "select idgenders, name from genders";

        // Добавление нового пассажира в базу данных
        public static string NewPassengerInsertQuery = "INSERT INTO passengers (name, surname, patronymic, id_gender, date_of_birth, passport_series, passport_number, phone, email, password) VALUES (@name, @surname, @patronymic, @id_gender, @date_of_birth, @passport_series, @passport_number, @phone, @email, @password); SELECT LAST_INSERT_ID();";

        // Добавление нового билета в базу данных
        public static string NewTicketInsertQuery = "INSERT INTO tickets (id_passenger, id_train, id_route, id_wagon, departure_time, arrival_time, ticket_cost, seat_number) VALUES (@id_passenger, @id_train, @id_route, @id_wagon, @departure_time, @arrival_time, @ticket_cost, @seat_number); SELECT LAST_INSERT_ID();";

        // Получение списка дополнительных услуг в вагоне
        public static string ServicesListQuery = "select idservices, name, cost from services";

        // Добавление услуг к билету
        public static string NewTicket_to_ServiceInsertQuery = "insert into tickets_to_services (id_ticket, id_service, time_of_purchase) values (@id_ticket, @id_service, @time_of_purchase)";

        // Проверка на наличие уже добавленных услуг
        public static string CheckTicket_to_ServiceQuery = "select * from tickets_to_services WHERE id_ticket = @id_ticket and id_service = @id_service";

        // Обновление цены билета
        public static string UpdateTicketPriceQuery = "UPDATE tickets SET ticket_cost = @new_cost WHERE idtickets = @id_ticket";

        // Получение общей суммы средств, потраченных пассажиром
        public static string TotalPassengerSpentQuery = "SELECT SUM(ticket_cost) FROM tickets WHERE id_passenger = @id_passenger";

        // Проверка номера телефона при регистрации пользователя

        public static string CheckPassengerPhoneQuery = "SELECT COUNT(*) FROM passengers WHERE phone = @phone";

        // Поиск аккаунта пользователя (при авторизации)
        public static string CheckPassengerAccQuery = "select idpassengers, name, surname, patronymic, id_gender, date_of_birth, passport_series, passport_number, phone, email from passengers where phone = @phone and password = @password;";
    }
}
