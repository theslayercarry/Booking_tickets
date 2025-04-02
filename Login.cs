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

namespace Booking_tickets
{
    public partial class Login: Form
    {
        public Login()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            InitializeFormData();
        }

        private void InitializeFormData()
        {
            textBox_phone.MaxLength = 11;
            textBox_phone.Text = Input_Data.DefaultPhoneText;
            textBox_phone.ForeColor = Color.DimGray;

            textBox_password.PasswordChar = '●';
            textBox_password.MaxLength = 20;

            pictureBox_hide.Visible = true;  // Изначально пароль скрыт
            pictureBox_show.Visible = false;

            pictureBox_show.Location = pictureBox_hide.Location;
        }

        private int PassengerAccountCheck()
        {
            // Выполняем SQL-запрос для получения данных о пользователе
            DataTable result = GlobalData.db.ExecuteQuery(GlobalData.CheckPassengerAccQuery, new Dictionary<string, object>
            {
                { "@phone", textBox_phone.Text },       // Передаем параметры: номер телефона
                { "@password", textBox_password.Text }  // и пароль для проверки
            });

            // Проверяем, есть ли результат
            if (result.Rows.Count == 1) // Если совпадение найдено, загружаем данные пользователя
            {
                LoadPassengerData(result); 
            }
            else
            {
                GlobalData.last_id_passenger = -1;  // Если пользователь не найден, присваиваем -1
            }

            return GlobalData.last_id_passenger; 
        }

        private void LoadPassengerData(DataTable result)
        {
            // Получаем строку результата
            DataRow row = result.Rows[0];

            // Загружаем данные пользователя в GlobalData
            GlobalData.last_id_passenger = Convert.ToInt32(row["idpassengers"]);
            GlobalData.passenger_name = row["name"].ToString();
            GlobalData.passenger_surname = row["surname"].ToString();
            GlobalData.passenger_patronymic = row["patronymic"].ToString();
            GlobalData.id_passenger_gender = Convert.ToInt32(row["id_gender"]);
            GlobalData.passenger_date_of_birth = Convert.ToDateTime(row["date_of_birth"]).ToString("yyyy-MM-dd");
            GlobalData.passenger_passport_series = row["passport_series"].ToString();
            GlobalData.passenger_passport_number = row["passport_number"].ToString();
            GlobalData.passenger_phone = row["phone"].ToString();
            GlobalData.passenger_email = row["email"].ToString();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            // Вызов метода проверки введённых данных
            if (ValidateAuthorizationData())
            {
                return;
            }

            if (PassengerAccountCheck() != -1)
            {
                MessageBox.Show("Загрузка данных пользователя...", "Успешная авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Закрываем форму с успешным результатом
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден. Пожалуйста, проверьте свой пароль и номер телефона и попробуйте снова.", "Ошибка при входе", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private Boolean ValidateAuthorizationData() //Проверка правильности ввода данных о пассажире
        {
            // Словарь для хранения сообщений об ошибках
            Dictionary<string, Func<bool>> validationConditions = new Dictionary<string, Func<bool>>()
            {
                { "Введите номер телефона", () => textBox_phone.TextLength < 11 || textBox_phone.Text == "+7 XXX XXX XX XX" },
                { "Введите пароль", () => textBox_password.TextLength < 1 }
            };

            // Список ошибок
            List<string> errors = new List<string>();

            // Проходим по всем условиям и добавляем ошибки
            foreach (var condition in validationConditions)
            {
                if (condition.Value())
                {
                    errors.Add(condition.Key); // Добавляем ошибку, если условие выполнено
                }
            }

            // Если есть ошибки, показываем их
            if (errors.Any())
            {
                MessageBox.Show("Ошибка при вводе данных:\r\n-  " + string.Join("\r\n-  ", errors), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true; // Возвращаем true, если есть ошибки
            }

            return false; // Возвращаем false, если ошибок нет
        }

        private void TogglePasswordVisibility(object sender, EventArgs e)
        {
            // Меняем состояние пароля
            textBox_password.UseSystemPasswordChar = !textBox_password.UseSystemPasswordChar;

            // Переключаем видимость картинок
            pictureBox_hide.Visible = !textBox_password.UseSystemPasswordChar;
            pictureBox_show.Visible = textBox_password.UseSystemPasswordChar;
        }

        private void textBox_phone_Leave(object sender, EventArgs e)
        {
            if (textBox_phone.Text == "")
            {
                textBox_phone.Text = Input_Data.DefaultPhoneText;
                textBox_phone.ForeColor = Color.DimGray;
            }
        }

        private void textBox_phone_Enter(object sender, EventArgs e)
        {
            if (textBox_phone.Text == Input_Data.DefaultPhoneText)
                textBox_phone.Text = "";
        }

        private void textBox_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperClasses.NumbersOnly_KeyPress(sender, e);
        }

        private void textBox_phone_MouseEnter(object sender, EventArgs e)
        {
            textBox_phone.ForeColor = Color.DarkSlateGray;
        }

        private void textBox_phone_MouseLeave(object sender, EventArgs e)
        {
            if (textBox_phone.Text == "" || textBox_phone.Text == Input_Data.DefaultPhoneText)
                textBox_phone.ForeColor = Color.Gray;
        }

        private void pictureBox_hide_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(sender, e);
        }

        private void pictureBox_show_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(sender, e);
        }

        private void pictureBox_hide_MouseHover(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(pictureBox_hide, "Показать пароль");
        }

        private void pictureBox_show_MouseHover(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(pictureBox_show, "Скрыть пароль");
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void textBox_phone_KeyDown(object sender, KeyEventArgs e)
        {
            HelperClasses.HandlePasteOnlyDigits(sender, e);
        }
    }
}
