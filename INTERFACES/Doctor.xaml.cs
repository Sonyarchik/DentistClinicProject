using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

namespace DentistClinicProject.INTERFACES
{
    /// <summary>
    /// Логика взаимодействия для Doctor.xaml
    /// </summary>
    public partial class Doctor : Window
    {
        private string _userFullName;
        public Doctor(string userFullName)
        {
            InitializeComponent();
            PrintDataGridMedicalBooks();

            _userFullName = userFullName;
        }

        ///<summary>
        ///DataGridView's
        ///</summary>

        private void PrintDataGridMedicalBooks()
        {
            using (var db = new DentistClinicContext())
            {
                MedicalBookGrid.ItemsSource = db.MedicalBooks
                .Include(md => md.IdPatientNavigation)
                .Include(md => md.IdDoctorNavigation)
                .Include(md => md.IdServiceNavigation)
                .Include(md => md.IdDiagnosisNavigation)
                .Include(md => md.IdStatusNavigation)
                .Select(md => new
                {
                    Имя__пациента = md.IdPatientNavigation.FullName,
                    Имя__врача = md.IdDoctorNavigation.FullName,
                    Услуга = md.IdServiceNavigation.NameOfService,
                    Дата__и__время = md.DataAppointment + md.TimeAppointment,
                    Диагноз = md.IdDiagnosisNavigation.DiagnosisName,
                    Рекомендации__по__лечению = md.Treatment
                }).ToList();
            }
        }

        /// <summary>
        /// button's
        /// </summary>
        private void WatchInfoDoctor_Click(object sender, RoutedEventArgs e)
        {
            UpdateData updateData = new UpdateData(_userFullName);
            updateData.ShowDialog();
        }

        private void WatchInfoRefresh_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGridMedicalBooks();
        }
    }
}
