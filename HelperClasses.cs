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
    public class HelperClasses
    {
        //Вспомогательный метод для заполнения элемента ComboBox
        public static void SetComboBoxDataSource(ComboBox comboBox, DataTable dataSource, string displayMember, string valueMember) 
        {
            comboBox.DataSource = dataSource;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
        }

        //Вспомогательный метод для заполнения CheckListBox с перечнем услуг
        public static void SetCheckedListBoxDataSource(CheckedListBox checkedListBox, DataTable dataSource, string displayMember, string valueMember, string priceMember)
        {
            checkedListBox.Items.Clear(); // Очищаем перед добавлением новых данных

            foreach (DataRow row in dataSource.Rows)
            {
                int id = Convert.ToInt32(row[valueMember]);       // ID услуги
                string name = row[displayMember].ToString();      // Название услуги
                decimal price = Convert.ToDecimal(row[priceMember]); // Цена услуги

                // Добавляем в список элемент в формате "Название (Цена)"
                checkedListBox.Items.Add(new KeyValuePair<int, string>(id, $"{name} ({price} ₽)"));
            }
            checkedListBox.DisplayMember = "Value";
        }

        // Вспомогательный метод для расчёта приблизительного времени прибытия поезда
        public static string CalculateArrivalTime(DataTable result, string departureTime) 
        {
            if (result.Rows.Count == 0)
                return string.Empty; // Если нет данных, возвращаем пустую строку

            int hours = Convert.ToInt32(result.Rows[0]["travel_time_hours"]);
            int minutes = Convert.ToInt32(result.Rows[0]["travel_time_minutes"]);

            DateTime arrivalTime = DateTime.Parse(departureTime)
                .AddHours(hours)
                .AddMinutes(minutes);

            return arrivalTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //  Метод для проверки ввода букв и пробелов
        public static void LettersOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            char inputChar = e.KeyChar;
            // Разрешаем буквы и пробелы, а также клавишу "Backspace"
            if (inputChar != 8 && !Char.IsLetter(inputChar) && inputChar != 32)
            {
                e.Handled = true; 
            }
        }

        // Метод для проверки ввода цифр
        public static void NumbersOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            char inputChar = e.KeyChar;
            // Разрешаем только цифры и клавишу "Backspace"
            if (!Char.IsDigit(inputChar) && inputChar != 8)
            {
                e.Handled = true; 
            }
        }

        // Метод для проверки ввода email
        public static void EmailValidation_KeyPress(object sender, KeyPressEventArgs e)
        {
            char inputChar = e.KeyChar;
            if (!Char.IsLetterOrDigit(inputChar) && inputChar != 8 && inputChar != 45 && inputChar != 46 && inputChar != 64 && inputChar != 95)
            {
                e.Handled = true;
            }
        }

        // Метод для отключения редактирования всех элементов управления в указанной панели
        public static void DisableEditingInPanel(Panel panel) 
        {
            foreach (Control control in panel.Controls)
            {
                if (control is TextBox || control is MaskedTextBox || control is ComboBox)
                {
                    // Убираем возможность редактирования текста
                    control.Enabled = false;
                }
            }
        }

        // Метод для включения редактирования всех элементов управления в указанной панели
        public static void EnableEditingInPanel(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is TextBox || control is MaskedTextBox || control is ComboBox)
                {
                    // Разблокируем возможность редактирования текста
                    control.Enabled = true;
                }
            }
        }

        // Метод для очистки текста во всех полях ввода в указанной панели
        public static void ClearTextInPanel(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is TextBox || control is MaskedTextBox)
                {
                    // Очищаем текст
                    control.Text = string.Empty;
                }
            }
        }

        // Метод для генерации случайного пароля из 12 символов (ASCII-коды от 33 до 90)
        public static string GetPass()
        {
            int[] arr = new int[12];
            Random rnd = new Random();
            string Password = "";

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(33, 90);
                Password += (char)arr[i];
            }
            return Password;
        }

        // Метод для преобразования ФИО в инициалы
        public static string GetShortFIO(string surname, string name, string patronymic)
        {
            if (string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(name))
                return surname; // Если нет имени, оставляем только фамилию

            string initials = $"{surname}";

            if (!string.IsNullOrWhiteSpace(name))
                initials += $" {name[0]}";

            if (!string.IsNullOrWhiteSpace(patronymic))
                initials += $".{patronymic[0]}.";

            return initials;
        }

        // Метод для вставки только цифр в TextBox из буфера обмена
        public static void HandlePasteOnlyDigits(object sender, KeyEventArgs e)
        {
            // Проверяем, что sender - это TextBox
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return; // Если это не TextBox, выходим из метода.

            // Проверяем, был ли нажат Ctrl + V (вставка)
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Получаем текст из буфера обмена
                string clipboardText = Clipboard.GetText();

                // Проверяем, что в буфере обмена только цифры
                if (clipboardText.All(Char.IsDigit))
                {
                    // Проверяем, не превысит ли длина текста в TextBox после вставки MaxLength
                    int currentTextLength = textBox.TextLength;
                    int maxLength = textBox.MaxLength;

                    // Если текст, который мы вставляем, плюс текущий текст в TextBox не превышает MaxLength
                    if (currentTextLength + clipboardText.Length <= maxLength || maxLength == 0)
                    {
                        // Вставляем текст в TextBox
                        textBox.SelectedText = clipboardText;
                    }
                    else
                    {
                        // Если длина вставляемого текста превышает максимальную длину, отменяем вставку
                        e.Handled = true;
                    }
                }
                else
                {
                    // Если в буфере обмена есть нецифровой текст, отменяем вставку
                    e.Handled = true;
                }

                // Останавливаем дальнейшую обработку события
                e.SuppressKeyPress = true;
            }
        }


    }
}
