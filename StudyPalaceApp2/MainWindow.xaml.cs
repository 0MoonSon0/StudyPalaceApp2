using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using Microsoft.Win32;
using System.Data;
using System.Collections.ObjectModel;
using System.Net.Mail;

namespace StudyPalaceApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        STUDY_PALACEEntities2 db;
        private ObservableCollection<Client> _client = new ObservableCollection<Client>();
        private ObservableCollection<Consultation> _conseltation = new ObservableCollection<Consultation>();
        private ObservableCollection<Status> _status = new ObservableCollection<Status>();
        private ObservableCollection<Specialist> _specialist = new ObservableCollection<Specialist>();
        private ObservableCollection<Field> _field = new ObservableCollection<Field>();
        private ObservableCollection<Service> _serviece = new ObservableCollection<Service>();
        private ObservableCollection<ServiceList> _servieceList = new ObservableCollection<ServiceList>();
      
        public MainWindow()
        {
            InitializeComponent();
            db = new STUDY_PALACEEntities2();
            dgClient.ItemsSource = db.Client.ToList();
            
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
            LoadClients();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string selectedOption = ((ComboBoxItem)ImpExpBox.SelectedItem)?.Content.ToString();
            switch (selectedOption)
            {
                case "Клиенты":
                    Client newClient = new Client
                    {
                        full_name = "Новое клиент",
                        age = 0,
                        gender = "-",
                        phone_number = "80000000000",
                        email_address = "Адресс почты",
                        registration_date = DateTime.Now
                    };
                    _client.Add(newClient);
                    db.Client.Add(newClient);
                    LoadClients();
                    db.SaveChanges();
                    break;
                case "Консультация":
                    Consultation newConsultation = new Consultation
                    {
                        service_id = 1,
                        specialist_id = 1,
                        client_id = 1,
                        field_id = 1,
                        
                        consultation_date = DateTime.Now,
                        notes = "-"
                    };
                    _conseltation.Add(newConsultation);
                    db.Consultation.Add(newConsultation);
                    db.Consultation.Add(newConsultation);
                    LoadClients();
                    db.SaveChanges();
                    break;

                    case "Статус консультации":
                    Status newStatus = new Status
                    {
                       
                        consultation_id = 1,
                        status_description = "-",

                    };
                    _status.Add(newStatus);
                    db.Status.Add(newStatus);
                    db.Status.Add(newStatus);
                    LoadClients();
                    db.SaveChanges();
                    break;
                case "Специалисты":
                    Specialist newSpecialist = new Specialist
                    {
                       
                        full_name = "Новое клиент",
                        age = 0,
                        gender = "-",
                        email_address = "Адресс почты",
                        field = "",
                        field_id = 1

                    };
                    _specialist.Add(newSpecialist);
                    db.Specialist.Add(newSpecialist);
                    db.Specialist.Add(newSpecialist);
                    db.SaveChanges();
                    break;
                case "Услуги":
                    Service newService = new Service
                    {
                      
                        service_name = "Услуга",
                        service_field = "Направление",
                        price = 0,
                       

                    };
                    _serviece.Add(newService);
                    db.Service.Add(newService);
                    db.Service.Add(newService);
                    db.SaveChanges();
                    break;
                case "Список услуг":
                    ServiceList newServiceList = new ServiceList
                    {
                       
                        service_count = 1,
                        client_id = 1,
                        service_id = 1,

                    };
                    _servieceList.Add(newServiceList);
                    db.ServiceList.Add(newServiceList);
                    db.ServiceList.Add(newServiceList);
                    db.SaveChanges();
                    break;
                default:
                    _client = new ObservableCollection<Client>(db.Client.ToList());
                    dgClient.ItemsSource = _client;
                    db.SaveChanges();
                    break;

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string selectedOption = ((ComboBoxItem)ImpExpBox.SelectedItem)?.Content.ToString();
            switch (selectedOption)
            {
                case "Клиенты":
                    if (dgClient.SelectedItem is Client selectedBeer)
                    {
                        db.Client.Remove(selectedBeer);
                        db.SaveChanges();
                        LoadClients();
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите товар для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "Консультация":
                    _conseltation = new ObservableCollection<Consultation>(db.Consultation.ToList());
                    dgClient.ItemsSource = _conseltation;
                    db.SaveChanges();
                    break;
                case "Статус консультации":
                    _status = new ObservableCollection<Status>(db.Status.ToList());
                    dgClient.ItemsSource = _status;
                    db.SaveChanges();
                    break;
                case "Специалисты":
                    _specialist = new ObservableCollection<Specialist>(db.Specialist.ToList());
                    dgClient.ItemsSource = _specialist;
                    db.SaveChanges();
                    break;
                case "Услуги":
                    _serviece = new ObservableCollection<Service>(db.Service.ToList());
                    dgClient.ItemsSource = _serviece;
                    db.SaveChanges();
                    break;
                case "Список услуг":
                    _servieceList = new ObservableCollection<ServiceList>(db.ServiceList.ToList());
                    dgClient.ItemsSource = _servieceList;
                    db.SaveChanges();
                    break;
                case "LINQ":
                    var query = from con in db.Consultation
                                join cl in db.Client on con.client_id equals cl.id
                                join fi in db.Field on con.field_id equals fi.id
                                join se in db.Service on con.service_id equals se.id
                                select new
                                {
                                    se.service_name,
                                    con.specialist_id,
                                    cl.full_name,
                                    fi.field_name
                                };
                    dgClient.ItemsSource = query.ToList();

                    break;
                default:
                    _client = new ObservableCollection<Client>(db.Client.ToList());
                    dgClient.ItemsSource = _client;
                    db.SaveChanges();
                    break;
            }
        }
          
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.dgClient.SelectAllCells();
            this.dgClient.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader; 
            ApplicationCommands.Copy.Execute(null, this.dgClient);
            this.dgClient.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            try
            {
                StreamWriter sw = new StreamWriter("wpfdaata.csv");
                sw.WriteLine(result);
                sw.Close();
                Process.Start("wpfdaata.csv");
            }
            catch (Exception ex)
            { }
        }

        private void SelectImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            byte[] image_bytes = File.ReadAllBytes(openFileDialog.FileName); 

            string connectionString = "Server=DESKTOP-OTD56KR;Database=STUDY_PALACE;Integrated Security=True;"; // строка подключения
            using (SqlConnection connection = new SqlConnection(connectionString)) // создаем подключение
            {
                connection.Open(); // откроем подключение
                SqlCommand command = new SqlCommand(); // создадим запрос
                command.Connection = connection; // дадим запросу подключение
                command.CommandText = @"INSERT INTO image VALUES (@Image)"; // пропишем запрос
                command.Parameters.Add("@Image", SqlDbType.Image, 1000000);
                command.Parameters["@Image"].Value = image_bytes;// скалярной переменной ImageData присвоем массив байтов
                command.ExecuteNonQuery(); // запустим
            }

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                    string savePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "SelectedImage.png");
                    File.WriteAllBytes(savePath, imageBytes);

                    MessageBox.Show($"Изображение сохранено по пути: {savePath}", "Сохранение успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            DataTable matcher_query = SqlModel.Select("SELECT * from image"); // запрос
            if (matcher_query.Rows.Count > 0)
            {
                // Берем последнюю строку и извлекаем байтовый массив изображения
                byte[] imageBytes = (byte[])matcher_query.Rows[matcher_query.Rows.Count - 1]["image"];

                // Преобразуем байты в изображение с помощью класса ByteImage
                image.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray(imageBytes));
            }
            else
            {
                MessageBox.Show("Нет изображений в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoadClients()
        {
            string selectedOption = ((ComboBoxItem)ImpExpBox.SelectedItem)?.Content.ToString();
            switch (selectedOption)
            {
                case "Клиенты":

                    _client = new ObservableCollection<Client>(db.Client.ToList());
                    dgClient.ItemsSource = _client;
                    db.SaveChanges();
                    break;
                case "Консультация":
                    _conseltation = new ObservableCollection<Consultation>(db.Consultation.ToList());
                    dgClient.ItemsSource = _conseltation;
                    db.SaveChanges();
                    break;
                case "Статус консультации":
                    _status = new ObservableCollection<Status>(db.Status.ToList());
                    dgClient.ItemsSource = _status;
                    db.SaveChanges();
                    break;
                case "Специалисты":
                    _specialist = new ObservableCollection<Specialist>(db.Specialist.ToList());
                    dgClient.ItemsSource = _specialist;
                    db.SaveChanges();
                    break;
                case "Услуги":
                    _serviece = new ObservableCollection<Service>(db.Service.ToList());
                    dgClient.ItemsSource = _serviece;
                    db.SaveChanges();
                    break;
                case "Список услуг":
                    _servieceList = new ObservableCollection<ServiceList>(db.ServiceList.ToList());
                    dgClient.ItemsSource = _servieceList;
                    db.SaveChanges();
                    break;
                case "LINQ":
                    var query = from con in db.Consultation
                                join cl in db.Client on con.client_id equals cl.id
                                join fi in db.Field on con.field_id equals fi.id
                                join se in db.Service on con.service_id equals se.id
                                select new
                                {
                                    se.service_name,
                                    con.specialist_id,
                                    cl.full_name,
                                    fi.field_name
                                };
                    dgClient.ItemsSource = query.ToList();

                    break;
                default:
                    _client = new ObservableCollection<Client>(db.Client.ToList());
                    dgClient.ItemsSource = _client;
                    db.SaveChanges();
                    break;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }
    }
}