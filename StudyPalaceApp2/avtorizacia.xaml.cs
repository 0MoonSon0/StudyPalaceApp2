using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;

namespace StudyPalaceApp2
{
    /// <summary>
    /// Логика взаимодействия для avtorizacia.xaml
    /// </summary>
    public partial class avtorizacia : Window
    {
        STUDY_PALACEEntities2 db;

        private readonly string _connectionString = "Server=DESKTOP-OTD56KR;Database=STUDY_PALACE;Integrated Security=True;";

        public avtorizacia()
        {
            InitializeComponent();
        }


        // Обработчик нажатия кнопки регистрации
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            // Хэширование пароля и получение соли
            (string hash, string salt) = PasswordHelper.HashPassword(password);

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new SqlCommand("INSERT INTO users (name, login, hash, salt) VALUES (@name, @login, @hash, @salt)", connection);

                    // Явное указание типа данных для параметров
                    command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 255).Value = username;
                    command.Parameters.Add("@login", System.Data.SqlDbType.NVarChar, 255).Value = username;
                    command.Parameters.Add("@hash", System.Data.SqlDbType.NVarChar, 255).Value = hash;
                    command.Parameters.Add("@salt", System.Data.SqlDbType.NVarChar, 255).Value = salt;

                    command.ExecuteNonQuery();

                    MessageBox.Show("Регистрация прошла успешно!");

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();  // Показываем главное окно
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}");
            }
        }

        // Обработчик нажатия кнопки входа
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Ошибка при открытии соединения: {ex.Message}");
                        foreach (SqlError error in ex.Errors)
                        {
                            Console.WriteLine($"Код ошибки: {error.Number}, Сообщение: {error.Message}");
                        }
                    }

                    var command = new SqlCommand("SELECT hash, salt FROM users WHERE login = @login", connection);
                    command.Parameters.Add("@login", System.Data.SqlDbType.NVarChar, 255).Value = username;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader.GetString(0);
                            string storedSalt = reader.GetString(1);

                            // Проверка пароля
                            bool isPasswordValid = PasswordHelper.VerifyPassword(password, storedHash, storedSalt);

                            if (isPasswordValid)
                            {
                                MessageBox.Show("Вход выполнен успешно!");

                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();  // Показываем главное окно
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Неверный пароль.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе: {ex.Message}");
            }
        }

    }
}