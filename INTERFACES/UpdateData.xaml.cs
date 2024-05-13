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
    /// Логика взаимодействия для UpdateData.xaml
    /// </summary>
    public partial class UpdateData : Window
    {
        private string _userFullName;
        public UpdateData(string userFullName)
        {
            InitializeComponent();
            COMBOBOXPatientItems();
            COMBOBOXStatusItems();
            COMBOBOXDiagnosesItems();

            _userFullName = userFullName;
        }

        /// <summary>
        /// comboboxes
        /// </summary>
        private void COMBOBOXPatientItems() //Выпадающий список фамилий пациентов
        {
            using (var db = new DentistClinicContext())
            {
                COMBOBOXPatient.ItemsSource = db.Patients.Select(p => p.FullName).ToList();
            }
        }

        private void COMBOBOXStatusItems() //Выпадающий список статусов записи
        {
            using (var db = new DentistClinicContext())
            {
                COMBOBOXStatus.ItemsSource = db.Statuses.Select(s => s.StatusName).ToList();
            }
        }
        
        private void COMBOBOXDiagnosesItems()
        {
            using (var db = new DentistClinicContext())
            {
                COMBOBOXDiagnoses.ItemsSource = db.DiagnosisLists.Select(d => d.DiagnosisName).ToList();
            }
        } //Выпадающий список болезней

        /// <summary>
        /// button's
        /// </summary>
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DentistClinicContext())
                {
                    if (COMBOBOXPatient.SelectedItem != null && COMBOBOXStatus.SelectedItem != null)
                    {
                        string selectedPatient = COMBOBOXPatient.SelectedItem.ToString();
                        string selectedStatus = COMBOBOXStatus.SelectedItem.ToString();
                        string selectedDiagnoses = COMBOBOXDiagnoses.SelectedItem.ToString();
                        string treatmentRecommendation = TextBoxTreatment.Text;

                        var patient = db.MedicalBooks.FirstOrDefault(p => p.IdPatientNavigation.FullName == selectedPatient);
                        var doctor = db.MedicalBooks.FirstOrDefault(d => d.IdDoctorNavigation.FullName == _userFullName);

                        if (patient != null)
                        {
                            MessageBox.Show("Вы уверены, что хотите изменить данные?", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Question);

                            db.SaveChanges();

                            var medicalBook = db.MedicalBooks.Include(mb => mb.IdDoctorNavigation)
                                     .Include(mb => mb.IdStatusNavigation)
                                     .Include(mb => mb.IdDiagnosisNavigation)
                                     .FirstOrDefault(mb => mb.IdPatient == patient.IdPatient);
                            if (medicalBook != null)
                            {
                                doctor.IdDoctorNavigation.FullName = _userFullName;
                                medicalBook.IdStatusNavigation.StatusName = selectedStatus;
                                medicalBook.IdDiagnosisNavigation.DiagnosisName = selectedDiagnoses;
                                medicalBook.Treatment = treatmentRecommendation;

                                db.SaveChanges();
                                MessageBox.Show("Данные успешно сохранены!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Question);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Похоже, в ходе выполнения задачи что-то пошло не так.\nПопробуйте еще раз!:)", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
