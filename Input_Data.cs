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
using MySqlX.XDevAPI.Relational;

namespace Booking_tickets
{
    public partial class Input_Data: Form
    {
        public static string DefaultPhoneText = "+7 XXX XXX XX XX";
        public static bool authorization_verification => GlobalData.last_id_passenger != -1;
        public Input_Data()
        {
            InitializeComponent();
            InitializeFormControls();
            StartPosition = FormStartPosition.CenterScreen;

            comboBox_gender.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Input_Data_Load(object sender, EventArgs e)
        {
            // Установление свойств видимости для элементов авторизации пользователя
            SetAuthorizationFields(authorization_verification);

            // Заполнение ComboBox_genders со списком для выбора пола
            DataTable genders = GlobalData.db.ExecuteQuery(GlobalData.LoadGenderListQuery);
            HelperClasses.SetComboBoxDataSource(comboBox_gender, genders, "name", "idgenders");
        }

        private void InitializeFormControls()
        {
            textBox_phone.MaxLength = 11;
            textBox_phone.Text = DefaultPhoneText;
            textBox_phone.ForeColor = Color.DimGray;

            textBox_name.MaxLength = 100;
            textBox_surname.MaxLength = 100;
            textBox_patronymic.MaxLength = 100;
            textBox_pass_series.MaxLength = 4;
            textBox_pass_number.MaxLength = 6;
            textBox_email.MaxLength = 100;

            maskedTextBox_birthday.MaxLength = 10;
            maskedTextBox_birthday.Mask = "0000-00-00";
            maskedTextBox_birthday.ValidatingType = typeof(DateTime);

            // Подключаем обработчик для всех нужных TextBox
            textBox_phone.KeyDown += HelperClasses.HandlePasteOnlyDigits;
            textBox_pass_series.KeyDown += HelperClasses.HandlePasteOnlyDigits;
            textBox_pass_number.KeyDown += HelperClasses.HandlePasteOnlyDigits;
        }

        private void textBox_phone_MouseLeave(object sender, EventArgs e)
        {
            if (textBox_phone.Text == "" || textBox_phone.Text == DefaultPhoneText)
                textBox_phone.ForeColor = Color.Gray;
        }

        private void textBox_phone_MouseHover(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(textBox_phone, "Минимальная длина номера телефона 11 цифр");
        }

        private void textBox_phone_MouseEnter(object sender, EventArgs e)
        {
            textBox_phone.ForeColor = Color.DarkSlateGray;
        }

        // Только символы, использующиеся при вводе номера телефона
        private void textBox_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperClasses.NumbersOnly_KeyPress(sender, e);
        }

        private void textBox_phone_Leave(object sender, EventArgs e)
        {
            if (textBox_phone.Text == "")
            {
                textBox_phone.Text = DefaultPhoneText;
                textBox_phone.ForeColor = Color.DimGray;
            }
        }

        private void textBox_phone_Enter(object sender, EventArgs e)
        {
            if (textBox_phone.Text == DefaultPhoneText)
                textBox_phone.Text = "";
        }

        // Только символы, использующиеся при вводе email
        private void textBox_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperClasses.EmailValidation_KeyPress(sender, e); 
        }

        // Только буквы при вводе в поля с ФИО
        private void textBox_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperClasses.LettersOnly_KeyPress(sender, e); 
        }

        private void textBox_surname_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperClasses.LettersOnly_KeyPress(sender, e); 
        }

        private void textBox_patronymic_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperClasses.LettersOnly_KeyPress(sender, e); 
        }

        // Только цифры при вводе в поля с серией и номером паспорта
        private void textBox_pass_series_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperClasses.NumbersOnly_KeyPress(sender, e);
        }

        private void textBox_pass_number_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperClasses.NumbersOnly_KeyPress(sender, e);
        }

        private void button_confirm_data_Click(object sender, EventArgs e)
        {      
            // Вызов метода проверки введённых данных
            if (ValidatePassengerData())
            {
                return;
            }
            else
            {
                FillPassengerFields(authorization_verification); // Вызываем метод с параметром
                GlobalData.ShowForm(this, GlobalData.form4);
            }
        }

        private Boolean ValidatePassengerData() //Проверка правильности ввода данных о пассажире
        {
            // Проверяем, существует ли введённый номер телефона в БД (если пользователь не авторизован)
            bool phoneExists = false;

            if (!authorization_verification)
            {
                DataTable result = GlobalData.db.ExecuteQuery(GlobalData.CheckPassengerPhoneQuery, new Dictionary<string, object>
                {
                      { "@phone", textBox_phone.Text }
                });

                phoneExists = Convert.ToInt32(result.Rows[0][0]) > 0;
            }

            // Словарь для хранения сообщений об ошибках
            Dictionary<string, Func<bool>> validationConditions = new Dictionary<string, Func<bool>>()
            {
                { "Укажите имя пассажира", () => textBox_name.TextLength < 1 },
                { "Укажите фамилию пассажира", () => textBox_surname.TextLength < 1 },
                { "Укажите отчество пассажира", () => textBox_patronymic.TextLength < 1 },
                { "Введите существующий адрес эл.почты", () => textBox_email.TextLength < 9 },
                { "Укажите дату в формате ГГГГ.ММ.ДД, например, 1990-12-25", () => maskedTextBox_birthday.TextLength < 10 || !maskedTextBox_birthday.MaskCompleted },
                { "Проверьте серию и номер паспорта", () => textBox_pass_series.TextLength < 4 || textBox_pass_number.TextLength < 6 },
                { "Укажите номер телефона", () => textBox_phone.TextLength < 11 || textBox_phone.Text == "+7 XXX XXX XX XX" },
                { "Пользователь с таким номером телефона уже зарегистрирован", () => !authorization_verification && phoneExists }
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
                MessageBox.Show("Ошибка при добавлении данных:\r\n-  " + string.Join("\r\n-  ", errors), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true; // Возвращаем true, если есть ошибки
            }

            // Если ошибок нет, можно продолжить обработку данных
            if(GlobalData.last_id_passenger != -1)
            {
                MessageBox.Show("Вы успешно авторизовались! Переход на следующую форму.", "Авторизация пройдена");
            }
            else
            {
                MessageBox.Show("Все данные введены корректно! Продолжаем оформление.", "Успех");
            }
            return false; // Возвращаем false, если ошибок нет
        }

        private void linkLabel_login_MouseHover(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(linkLabel_login, "Уже регистрировались? - Войдите в аккаунт");
        }

        private void linkLabel_login_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login loginForm = new Login();
            loginForm.ShowDialog(); // Открываем форму авторизации и ждем её закрытия

            FillPassengerFields(authorization_verification); // Вызываем метод с параметром
            SetAuthorizationFields(authorization_verification);
        }

        private void FillPassengerFields(bool isAuthorized) // Метод заполнения данных пользователя в зависимости от авторизации
        {
            if (isAuthorized)
            {
                textBox_surname.Text = GlobalData.passenger_surname;
                textBox_name.Text = GlobalData.passenger_name;
                textBox_patronymic.Text = GlobalData.passenger_patronymic;
                comboBox_gender.SelectedValue = GlobalData.id_passenger_gender; 
                maskedTextBox_birthday.Text = GlobalData.passenger_date_of_birth;
                textBox_pass_series.Text = GlobalData.passenger_passport_series;
                textBox_pass_number.Text = GlobalData.passenger_passport_number;
                textBox_phone.Text = GlobalData.passenger_phone;
                textBox_email.Text = GlobalData.passenger_email;
                HelperClasses.DisableEditingInPanel(panel_to_fill);
            }
            else
            {
                GlobalData.passenger_name = textBox_name.Text;
                GlobalData.passenger_surname = textBox_surname.Text;
                GlobalData.passenger_patronymic = textBox_patronymic.Text;
                GlobalData.id_passenger_gender = (int)comboBox_gender.SelectedValue;
                GlobalData.passenger_date_of_birth = maskedTextBox_birthday.Text;
                GlobalData.passenger_passport_series = textBox_pass_series.Text;
                GlobalData.passenger_passport_number = textBox_pass_number.Text;
                GlobalData.passenger_phone = textBox_phone.Text;
                GlobalData.passenger_email = textBox_email.Text;
            }
        }

        private void SetAuthorizationFields(bool isAuthorized) // Метод для установления свойств видимости элементам авторизации
        {
            linkLabel_login.Visible = !isAuthorized;
            linkLabel_output.Visible = isAuthorized;
            label_name_initials.Visible = isAuthorized;

            linkLabel_output.Location = linkLabel_login.Location;

            label_name_initials.Text = isAuthorized ? HelperClasses.GetShortFIO(GlobalData.passenger_surname, GlobalData.passenger_name, GlobalData.passenger_patronymic) : "";
        }

        private void linkLabel_output_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GlobalData.last_id_passenger = -1;
            SetAuthorizationFields(authorization_verification);

            HelperClasses.ClearTextInPanel(panel_to_fill);
            HelperClasses.EnableEditingInPanel(panel_to_fill);
        }
    }
}
