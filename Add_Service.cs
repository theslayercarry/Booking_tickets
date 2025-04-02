using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Booking_tickets.HelperClasses;

namespace Booking_tickets
{
    public partial class Add_Service: Form
    {
        private decimal TotalServicePrice, TotalTicketPrice, TotalPassengerSpent, new_price;
        public Add_Service()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Add_Service_Load(object sender, EventArgs e)
        {
            LoadTicketData();

            // Заполнение элемента CheckedListBox с перечнем услуг
            DataTable services = GlobalData.db.ExecuteQuery(GlobalData.ServicesListQuery);
            HelperClasses.SetCheckedListBoxDataSource(checkedListBox_services, services, "name", "idservices", "cost");

            // Автоматически устанавливаем размер CheckedListBox
            AdjustCheckedListBoxSize(checkedListBox_services);
        }

        private void LoadTicketData()
        {
            label_ID_ticket.Text = $"ID: {GlobalData.last_id_ticket}";
            label_departure_time.Text = GlobalData.departure_time;
            label_arrival_time.Text = GlobalData.arrival_time;
            label_train_name.Text = GlobalData.train_name;
            label_wagon_name.Text = GlobalData.wagon_name;
            label_station_name.Text = GlobalData.station_name;
            label_seat_number.Text = $"{GlobalData.seat_number} место";
            label_city_to.Text = GlobalData.city_to;
            label_route.Text = $"{GlobalData.city_from} - {GlobalData.city_to}";

            TotalPassengerSpent = decimal.Parse(GlobalData.total_passenger_spent); // Локальная переменная для хранения общей суммы средств, потраченных пассажиром
            TotalTicketPrice = decimal.Parse(GlobalData.seat_cost); // Локальная переменная для хранения общей стоимости билета
            TotalServicePrice = 0; // Локальная переменная для хранения общей стоимости выбранных услуг

            // Обновление цены билета в зависимости от величины скидки
            new_price = TotalTicketPrice;
            new_price = TotalTicketPrice - LoadAndApplyDiscount(TotalTicketPrice, TotalPassengerSpent);
            new_price = Math.Round(new_price, 2);

            GlobalData.db.openConnection();
            UpdateTicketPrice(new_price, GlobalData.db.getConnection());


            label_services_list.Text = "";
            label_service_cost.Text = "";
            label_seat_cost.Text = $"Стоимость места:  {GlobalData.seat_cost} ₽"; 

            // Устанавливаем позицию для полей со стоимостью и скидкой
            label_total_ticket_cost.Location = new Point(label_seat_cost.Right + 40, label_total_ticket_cost.Location.Y);
            label_discount_amount.Location = new Point(label_total_ticket_cost.Location.X, label_total_ticket_cost.Bottom + 7);

            label_total_ticket_cost.Text = $"Общая стоимость билета:  {new_price} ₽";
        }

        private void AdjustCheckedListBoxSize(CheckedListBox checkedListBox)
        {
            int maxWidth = 0;
            using (Graphics g = checkedListBox.CreateGraphics())
            {
                foreach (var item in checkedListBox.Items)
                {
                    string text = item.ToString();
                    int itemWidth = (int)g.MeasureString(text, checkedListBox.Font).Width + 20; // Добавляем небольшой запас
                    maxWidth = Math.Max(maxWidth, itemWidth);
                }
            }

            // Устанавливаем новую ширину, но не меньше стандартной
            checkedListBox.Width = Math.Max(checkedListBox.Width, maxWidth);

            // Устанавливаем высоту в зависимости от количества элементов
            int itemHeight = checkedListBox.GetItemHeight(0); // Получаем высоту одной строки
            checkedListBox.Height = Math.Min(checkedListBox.Items.Count * itemHeight + 4, 300); // Ограничиваем макс. высоту
        }

        private Boolean AddServicesTransaction()
        {
            if (checkedListBox_services.CheckedItems.Count == 0)
            {
                MessageBox.Show("Вы не выбрали ни одной услуги!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return false;
            }

            using (MySqlConnection connection = GlobalData.db.getConnection())
            {
                GlobalData.db.openConnection();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in checkedListBox_services.CheckedItems)
                        {
                            var selectedService = (KeyValuePair<int, string>)item;
                            int serviceId = selectedService.Key;

                            if (!ServiceAlreadyAddCheck(serviceId, transaction))
                            {
                                AddServiceToTicket(serviceId, connection, transaction);
                                string fullText = selectedService.Value;

                                TotalServicePrice += ExtractServicePrice(fullText);
                                TotalTicketPrice  += ExtractServicePrice(fullText);

                                new_price = TotalTicketPrice;

                                if (CalculateDiscount(TotalPassengerSpent) != 0)
                                {
                                    new_price = TotalTicketPrice - LoadAndApplyDiscount(TotalTicketPrice, TotalPassengerSpent);
                                }

                                new_price = Math.Round(new_price, 2);

                                // Добавляем в список выбранных услуг (на UI)
                                string serviceName = fullText.Substring(0, fullText.LastIndexOf(" ("));
                                label_services_list.Text += "- " + serviceName + "\n";

                                UpdateTicketPrice(new_price, connection, transaction);
                            }
                        }

                        transaction.Commit();
                        UpdateUI();

                        MessageBox.Show("Услуги успешно добавлены!");
                        return false;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Откат при ошибке
                        MessageBox.Show("Ошибка при добавлении услуг: " + ex.Message);
                        return true;
                    }
                }
            }

        }
      
        private Boolean ServiceAlreadyAddCheck(int serviceId, MySqlTransaction transaction)
        {
            DataTable check = GlobalData.db.ExecuteQuery(GlobalData.CheckTicket_to_ServiceQuery, new Dictionary<string, object>
            {
                { "@id_ticket", GlobalData.last_id_ticket },
                { "@id_service", serviceId }
            }, transaction);

            return check.Rows.Count > 0;
        }

        private void AddServiceToTicket(int serviceId, MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand cmd = new MySqlCommand(GlobalData.NewTicket_to_ServiceInsertQuery, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@id_ticket", GlobalData.last_id_ticket);
                cmd.Parameters.AddWithValue("@id_service", serviceId);
                cmd.Parameters.AddWithValue("@time_of_purchase", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        private decimal ExtractServicePrice(string fullText)
        {
            // Суммирование стоимости каждой выбранной услуги к общей сумме
            int priceStartIndex = fullText.LastIndexOf(" (") + 2; // Индекс начала цены
            int priceEndIndex = fullText.LastIndexOf(" ₽"); // Индекс конца цены

            string priceString = fullText.Substring(priceStartIndex, priceEndIndex - priceStartIndex);

            return decimal.Parse(priceString);
        }

        private void UpdateTicketPrice(decimal TotalTicketPrice, MySqlConnection connection, MySqlTransaction transaction = null)
        {
            using (MySqlCommand cmd = new MySqlCommand(GlobalData.UpdateTicketPriceQuery, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@new_cost", TotalTicketPrice);
                cmd.Parameters.AddWithValue("@id_ticket", GlobalData.last_id_ticket);
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateUI()
        {
            label_service_cost.Text = $"Общая стоимость выбранных услуг:  {TotalServicePrice} ₽";
            label_total_ticket_cost.Text = $"Общая стоимость билета:  {new_price} ₽";
        }

        private void button_add_service_Click(object sender, EventArgs e)
        {
            // Вызов метода для добавления услуги к билету
            if (AddServicesTransaction())
            {
                return;
            }
        }

        private void Add_Service_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Вызываем завершение приложения
            Environment.Exit(0);
        }

        private decimal LoadAndApplyDiscount(decimal TicketPrice, decimal TotalSpent)
        {
            // Рассчитываем скидку
            int discountPercentage = CalculateDiscount(TotalSpent);

            // Обновляем UI
            decimal SavedMoneyAmount = Math.Round(TicketPrice * (discountPercentage / 100m), 2);
            label_discount_amount.Text = $"Ваша экономия по накопительной скидке ({discountPercentage}%) составляет:  {SavedMoneyAmount} ₽";
            AdjustLabelFontSize(label_discount_amount);

            // Добавляем Tooltip с информацией о скидке
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(label_discount_amount, $"Общая сумма покупок: {TotalSpent} ₽, скидка: {discountPercentage}%");

            return SavedMoneyAmount;
        }

        private int CalculateDiscount(decimal TotalSpent)
        {
            if (TotalSpent >= 100000) return 6;
            if (TotalSpent >= 80000) return 5;
            if (TotalSpent >= 60000) return 4;
            if (TotalSpent >= 40000) return 3;
            if (TotalSpent >= 10000) return 2;
            if (TotalSpent >= 5000) return 1;
            return 0;
        }

        private void AdjustLabelFontSize(Label lbl) // Автоматически изменяем размер шрифта в label_discount_amount в зависимости от ширины текста
        {
            using (Graphics g = lbl.CreateGraphics())
            {
                float fontSize = lbl.Font.Size;
                SizeF textSize = g.MeasureString(lbl.Text, new Font(lbl.Font.FontFamily, fontSize));

                while (textSize.Width > lbl.Width && fontSize > 6) // Минимальный размер шрифта 6
                {
                    fontSize--;
                    textSize = g.MeasureString(lbl.Text, new Font(lbl.Font.FontFamily, fontSize));
                }

                lbl.Font = new Font(lbl.Font.FontFamily, fontSize);
            }
        }


    }
}
