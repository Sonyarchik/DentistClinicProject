using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DentistClinicProject.INTERFACES
{
    /// <summary>
    /// Логика взаимодействия для MedicineRegistrator.xaml
    /// </summary>
    public partial class MedicineRegistrator : Window
    {
        public MedicineRegistrator()
        {
            InitializeComponent();
            PrintDataGridHistoryBook();
            COMBOBOXPatientItems();
            COMBOBOXDoctorItems();
            COMBOBOXStatusItems();
            COMBOBOXServiceListItems();
            PrintDataGridPatients();
            PrintDataGridDoctor();
            PrintDataGridService();
            COMBOBOXCategoryListItems();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LABELTotalSumm_DataContextChanged();
        }

        /// <summary>
        /// DataGridView's
        /// </summary>

        private void PrintDataGridHistoryBook()
        {
            using (var db = new DentistClinicContext()) //Подключаем базу данных
            {
                MedicineBookGrid.ItemsSource = db.MedicalBooks
                .Include(md => md.IdPatientNavigation)
                .Include(md => md.IdDoctorNavigation)
                .Include(md => md.IdServiceNavigation)
                .Include(md => md.IdDiagnosisNavigation)
                .Include(md => md.IdStatusNavigation)
                .Select(md => new
                {
                    Имя__пациента = md.IdPatientNavigation.FullName, //Создание столбца "Имя пациента"
                    Имя__врача = md.IdDoctorNavigation.FullName, // Создание столбца "Имя врача"
                    Услуга = md.IdServiceNavigation.NameOfService, // Создания столбца "Услуга"
                    Сумма = md.TotalPayment + " руб.", // Создания столбца "Сумма"
                    Дата__и__время = md.DataAppointment + md.TimeAppointment, // Создание столбца "Дата и время"
                    Диагноз = md.IdDiagnosisNavigation.DiagnosisName, // Создание столбца "Диагноз"
                    Рекомендации__по__лечению = md.Treatment, // Создание столбца "Рекомендации по лечению"
                    Статус__обработки = md.IdStatusNavigation.StatusName // Сорздание столбцы "Статус обработки"
                }).ToList();
            }
        } //вывод базы данных на странице "История посещений"

        private void PrintDataGridPatients()
        {
            using (var db = new DentistClinicContext())
            {
                PatientsGrid.ItemsSource = db.Patients.Select(p => new
                {
                    Имя__пациента = p.FullName,
                    Номер__телефона = p.Phone,
                    Почта = p.Email,
                    Паспорт = p.PassportData,
                    Дата__рождения = p.BdayDate,
                    СНИЛС = p.Snils
                }).ToList();
            }
        } //вывод базы данных на странице "Пациенты"

        private void PrintDataGridDoctor()
        {
            using (var db = new DentistClinicContext())
            {
                DoctorGrid.ItemsSource = db.Doctors.Select(d => new
                {
                    Имя__врача = d.FullName,
                    Отделение = d.IdCategoryNavigation.CategoryName,
                    Должность = d.IdJobTitleNavigation.JobTitleName,
                    Номер__телефона = d.Phone,
                    Почта = d.Email,
                }).ToList();
            }
        } //вывод базы данных на странице "Врачи"

        private void PrintDataGridService()
        {
            using (var db = new DentistClinicContext())
            {
                ServicesList.ItemsSource = db.ListOfServices.Select(l => new
                {
                    Отделение = l.IdCategoryNavigation.CategoryName,
                    Услуга = l.NameOfService,
                    Описание__услуги = l.Description,
                    Стоимость = l.Price + " руб."
                }).ToList();
            }
        } //вывод базы данных на страницу "Услуги"

        /// <summary>
        /// Label's
        /// </summary>
        private void LABELTotalSumm_DataContextChanged()
        {
            decimal totalSumm;

            using (var db = new DentistClinicContext())
            {
                var total = db.MedicalBooks.Sum(mb => mb.TotalPayment);
                totalSumm = Convert.ToDecimal(total);

                LABELTotalSumm.Content = "Общая сумма продаж: \n      " + Convert.ToString(totalSumm) + " руб.";
            }
        }  //расчет суммы продаж

        /// <summary>
        /// Combobox's
        /// </summary>

        private void COMBOBOXPatientItems() //вывод ФИО пациентов
        {
            using (var db = new DentistClinicContext())
            {
                COMBOBOXPatient.ItemsSource = db.Patients.Select(p => p.FullName).ToList();
            }
        }

        private void COMBOBOXDoctorItems() //вывод ФИО врачей
        {
            using (var db = new DentistClinicContext())
            {
                COMBOBOXDoctor.ItemsSource = db.Doctors.Select(d => d.FullName).ToList();
            }

            using (var db = new DentistClinicContext())
            {
                COMBOBOXDoctor1.ItemsSource = db.Doctors.Select(d => d.FullName).ToList();
            }
        }

        private void COMBOBOXStatusItems() //вывод статуса записи
        {
            using (var db = new DentistClinicContext())
            {
                COMBOBOXStatus.ItemsSource = db.Statuses.Select(p => p.StatusName).ToList();
            }
        }

        private void COMBOBOXServiceListItems() //вывод списка услуг
        {
            using (var db = new DentistClinicContext())
            {
                COMBOBOXServicesList.ItemsSource = db.ListOfServices.Select(s => s.NameOfService).ToList();
            }
        }

        private void COMBOBOXCategoryListItems()
        {
            using (var db = new DentistClinicContext())
            {
                COMBOBOXCategory.ItemsSource = db.CategoryOfServices.Select(c => c.CategoryName).ToList();
            }
        } //вывод списка отделений

        /// <summary>
        /// Button's
        /// </summary>

        private void BUTTONAdd_Click(object sender, RoutedEventArgs e) //обработчик события кнопки записи пациента
        {
            try
            {
                using (var db = new DentistClinicContext())
                {
                    MedicalBook newRecord = new MedicalBook();

                    newRecord.IdDoctor = db.Doctors.FirstOrDefault(d => d.FullName == COMBOBOXDoctor.Text)?.IdDoctor ?? 0;
                    newRecord.IdPatient = db.Patients.FirstOrDefault(p => p.FullName == COMBOBOXPatient.Text)?.IdPatient ?? 0;
                    newRecord.IdService = 1; // Перманентно выставляем услугу

                    ListOfService service = db.ListOfServices.FirstOrDefault(s => s.NameOfService == COMBOBOXServicesList.Text);
                    if (service != null)
                    {
                        newRecord.IdService = service.IdService;
                        newRecord.TotalPayment = service.Price;
                    }

                    newRecord.DataAppointment = Calendar.SelectedDate ?? DateTime.Now;

                    string time = string.Format("{0}:{1}:{2}", TextBoxHour.Text, TextBoxMinute.Text, "00");
                    newRecord.TimeAppointment = TimeSpan.Parse(time);

                    newRecord.IdDiagnosis = 41; // Перманентно выставляем диагноз "41"
                    newRecord.Treatment = TextBoxTreatment.Text;
                    newRecord.IdStatus = db.Statuses.FirstOrDefault(s => s.StatusName == COMBOBOXStatus.Text)?.IdStatus ?? 0;

                    MessageBoxResult messageBox = MessageBox.Show("Вы точно хотите оформить запись?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBox == MessageBoxResult.Yes)
                    {
                        db.MedicalBooks.Add(newRecord);
                        db.SaveChanges();

                        MessageBox.Show("Данные успешно сохранены!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Прерывание процесса добавления строки по запросу пользователя.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("При введении данных в базу данных произошла ошибка! Попробуйте еще раз.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BUTTONRefresh_Click(object sender, RoutedEventArgs e) //обработчик события кнопки, обновляющая данные бд в datagridview
        {
            PrintDataGridHistoryBook();
        }

        private void BUTTONAddPatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем данные из текстовых полей
                string firstName = TEXTBOXFirstName.Text;
                string secondName = TEXTBOXSecondName.Text;
                string patronymic = TEXTBOXPatronymic.Text;
                string phone = TEXTBOXPhone.Text;
                string email = TEXTBOXEmail.Text;
                string passportSeries = TEXTBOXPassportSeries.Text.Replace(" ", "");
                string passportNumber = TEXTBOXPassportNumber.Text.Replace(" ", "");
                string snils = TEXTBOXSNILS.Text;
                DateTime bdayDate = DatePickerBday.DisplayDate;

                // Проверки данных
                if (phone.Length != 11 || !phone.All(char.IsDigit))
                {
                    MessageBox.Show("Пожалуйста, введите корректный номер телефона (11 цифр).");
                    return;
                }

                if (passportSeries.Length != 4 || !passportSeries.All(char.IsDigit))
                {
                    MessageBox.Show("Пожалуйста, введите корректную серию паспорта (4 цифры).");
                    return;
                }

                if (passportNumber.Length != 6 || !passportNumber.All(char.IsDigit))
                {
                    MessageBox.Show("Пожалуйста, введите корректный номер паспорта (6 цифр).");
                    return;
                }

                if (snils.Length != 11 || !snils.All(char.IsDigit))
                {
                    MessageBox.Show("Пожалуйста, введите корректный СНИЛС (11 цифр).");
                    return;
                }

                // Объединяем серию и номер паспорта
                string passportData = passportSeries + passportNumber;

                using (var db = new DentistClinicContext())
                {
                    // Создаем объект Patient
                    var patient = new Patient
                    {
                        FullName = $"{secondName} {firstName} {patronymic}",
                        Phone = Convert.ToDecimal(phone),
                        Email = email,
                        PassportData = Convert.ToDecimal(passportData),
                        Snils = Convert.ToDecimal(snils),
                        BdayDate = bdayDate
                    };

                    MessageBoxResult messageBox = MessageBox.Show("Вы точно хотите добавить данные этого пациента?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBox == MessageBoxResult.Yes)
                    {
                        // Добавляем пациента в таблицу patients
                        db.Patients.Add(patient);
                        db.SaveChanges();
                        MessageBox.Show("Данные о пациенте успешно добавлены!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                        PrintDataGridPatients();
                    }
                    else
                    {
                        MessageBox.Show("Прерывание процесса добавления строки по запросу пользователя.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("В ходе выполнения операции что-то пошло не так. \n Попробуйте снова позже!:)", "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }  //обработчик события кнопки добавления данных нового пациента

        private void WatchInfoDoctor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DentistClinicContext())
                {
                    string selectedDoctor = COMBOBOXDoctor1.SelectedItem.ToString();
                    DateTime selectedDate = CalendarDoctors.SelectedDate.GetValueOrDefault();

                    var medicalBooks = db.MedicalBooks
                        .Where(mb => mb.IdDoctorNavigation.FullName == selectedDoctor && mb.DataAppointment.Date == selectedDate.Date)
                        .Select(mb => new
                        {
                            Имя__врача = mb.IdDoctorNavigation.FullName,
                            Отделение = mb.IdDoctorNavigation.IdCategoryNavigation.CategoryName,
                            Дата__и__время__записи = mb.DataAppointment + mb.TimeAppointment
                        })
                        .ToList();

                    DoctorGrid.ItemsSource = medicalBooks;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("В ходе выполнения операции что-то пошло не так. \n Попробуйте снова позже!:)", "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } //обработчик события кнопки нахождения записей врачей

        private void BUTTONSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchText = TEXTBOXSearch.Text.Trim().ToLower();
                decimal totalSumm = 0;

                using (var db = new DentistClinicContext()) //Подключение базы данных
                {
                    IQueryable<MedicalBook> query = db.MedicalBooks
                        .Include(md => md.IdPatientNavigation)
                        .Include(md => md.IdDoctorNavigation)
                        .Include(md => md.IdServiceNavigation)
                        .Include(md => md.IdDiagnosisNavigation)
                        .Include(md => md.IdStatusNavigation);

                    if (RadioButtonSearchPatient.IsChecked == true) //Проверка переключателя
                    {
                        query = query.Where(mb => mb.IdPatientNavigation.FullName.ToLower().Contains(searchText));
                    }
                    else if (RadioButtonSearchDoctor.IsChecked == true) //Проверка переключателя
                    {
                        query = query.Where(mb => mb.IdDoctorNavigation.FullName.ToLower().Contains(searchText));
                    }
                    else if (RadioButtonSearchDiagnosis.IsChecked == true) //Проверка переключателя
                    {
                        query = query.Where(mb => mb.IdDiagnosisNavigation.DiagnosisName.ToLower().Contains(searchText));
                    }
                    else if (RadioButtonSearchService.IsChecked == true) //Проверка переключателя
                    {
                        query = query.Where(mb => mb.IdServiceNavigation.NameOfService.ToLower().Contains(searchText));
                    }

                    var searchResults = query.Select(md => new //Вывод базы данных
                    {
                        Имя_пациента = md.IdPatientNavigation.FullName,
                        Имя_врача = md.IdDoctorNavigation.FullName,
                        Услуга = md.IdServiceNavigation.NameOfService,
                        Сумма = md.TotalPayment,
                        Дата_посещения = md.DataAppointment.Date,
                        Диагноз = md.IdDiagnosisNavigation.DiagnosisName,
                        Рекомендации_по_лечению = md.Treatment,
                        Статус_обработки = md.IdStatusNavigation.StatusName
                    }).ToList();

                    MedicineBookGrid.ItemsSource = searchResults;

                    totalSumm = searchResults.Sum(sr => sr.Сумма);
                    LABELTotalSumm.Content = "Общая сумма продаж: \n      " + Convert.ToString(totalSumm) + " руб.";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("В ходе выполнения операции что-то пошло не так. \n Попробуйте снова позже!:)", "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } //обработчик события кнопки поиска

        private void SearchService_Click(object sender, RoutedEventArgs e) //обработчик события кнопки поиска услуг
        {
            try
            {
                string searchText = TEXTBOXSearchService.Text.ToLower();

                using (var db = new DentistClinicContext())
                {
                    var searchResults = db.ListOfServices
                        .Where(s => s.NameOfService.ToLower().Contains(searchText))
                        .Select(s => new
                        {
                            Отделение = s.IdCategoryNavigation.CategoryName,
                            Услуга = s.NameOfService,
                            Описание_услуги = s.Description,
                            Стоимость = s.Price + " руб."
                        })
                        .ToList();

                    ServicesList.ItemsSource = searchResults;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("В ходе выполнения операции что-то пошло не так. \n Попробуйте снова позже!:)", "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonSortServices_Click(object sender, RoutedEventArgs e) //обработчик события кнопки сортировки услуг по критериям
        {
            try
            {
                using (var db = new DentistClinicContext())
                {
                    var services = db.ListOfServices.Select(sv => new
                    {
                        sv.IdCategoryNavigation.CategoryName,
                        sv.NameOfService,
                        sv.Description,
                        sv.Price
                    }).ToList();

                    if (RadioButtonPrice.IsChecked == true)
                    {
                        services = services.OrderBy(s => s.Price).ToList();
                    }
                    else if (RadioButtonAlphabet.IsChecked == true)
                    {
                        services = services.OrderBy(s => s.NameOfService).ToList();
                    }
                    else if (RadioButtonAlphabetInvert.IsChecked == true)
                    {
                        services = services.OrderByDescending(s => s.NameOfService).ToList();
                    }
                    else
                    {
                        MessageBox.Show("Введите критерии сортировки", "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    var sortresults = services.Select(sv => new
                    {
                        Отделение = sv.CategoryName,
                        Услуга = sv.NameOfService,
                        Описание__услуги = sv.Description,
                        Стоимость = sv.Price + " руб."
                    }).ToList();

                    ServicesList.ItemsSource = sortresults;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("В ходе выполнения операции что-то пошло не так. \n Попробуйте снова позже!:)", "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonRefreshServices_Click(object sender, RoutedEventArgs e) //обработчик события обновления данных в datagrid
        {
            PrintDataGridService();
        }

        private void ButtonSortCategories_Click(object sender, RoutedEventArgs e) //обработчик события сортировки категорий по данным combobox'а
        {
            try
            {
                using (var db = new DentistClinicContext())
                {
                    string selectedCategory = COMBOBOXCategory.SelectedItem.ToString();

                    var categories = db.ListOfServices
                           .Where(ct => ct.IdCategoryNavigation.CategoryName == selectedCategory)
                           .Select(ct => new
                           {
                               Отделение = ct.IdCategoryNavigation.CategoryName,
                               Услуга = ct.NameOfService,
                               Описание__услуги = ct.Description,
                               Стоимость = ct.Price + " руб."
                           }).ToList();
                    ServicesList.ItemsSource = categories;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("В ходе выполнения операции что-то пошло не так. \n Попробуйте снова позже!:)", "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
