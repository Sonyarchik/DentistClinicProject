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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DentistClinicProject.INTERFACES
{
    /// <summary>
    /// Логика взаимодействия для AuthorixForm.xaml
    /// </summary>
    public partial class AuthorixForm : Window
    {
        public string UserFullName { get; private set; }
        private decimal? _userid = null;
        private string _rolename = "Пользователь";
        private decimal? _roleid = null;

        public AuthorixForm()
        {
            InitializeComponent();
        }

        private void Authorization()
        {

            string login = TextBoxLogin.Text;
            decimal password = Convert.ToDecimal(TextBoxPassword.Text);

            try
            {
                using (var db = new DentistClinicContext())
                {
                    var userdata = db.Users.Include(u => u.Doctors).FirstOrDefault(User => User.Login == login);

                    if (userdata == null || userdata.Password != password)
                    {
                        Except();
                        return;
                    }

                    if (userdata != null)
                    {
                        _roleid = userdata.IdRole;
                        _userid = userdata.IdUser;
                    }

                    var roleid = userdata.IdRole;
                    _rolename = db.Roles.First(role => role.IdRole == roleid).RoleName;

                    var doctorFullName = userdata.Doctors.FirstOrDefault()?.FullName;
                    UserFullName = doctorFullName;
                }

                ELLIPSEAprooved1.Visibility = Visibility.Visible;
                ELLIPSEAprooved2.Visibility = Visibility.Visible;

                LabelAprooved1.Visibility = Visibility.Visible;
                LabelAprooved2.Visibility = Visibility.Visible;

                MessageBox.Show($"Здравствуйте, {UserFullName}.\nВход успешно выполнен!\nВы вошли как {_rolename}.");
            }
            catch (Exception ex)
            {
                ELLIPSENotAprooved1.Visibility = Visibility.Hidden;
                ELLIPSENotAprooved2.Visibility = Visibility.Hidden;

                LabelNotAprooved1.Visibility = Visibility.Hidden;
                LabelNotAprooved2.Visibility = Visibility.Hidden;
                MessageBox.Show("Произошла ошибка, попробуйте позже.");
            }

            switch (_rolename)
            {
                case "Администратор":
                    MedicineRegistrator medicineRegistrator = new MedicineRegistrator();
                    medicineRegistrator.Show();
                    break;
                case "Менеджер":
                    Doctor doctor = new Doctor(UserFullName);
                    doctor.Show();
                    break;
            }
        }

        private void Except()
        {
            ELLIPSENotAprooved1.Visibility = Visibility.Visible;
            ELLIPSENotAprooved2.Visibility = Visibility.Visible;

            LabelNotAprooved1.Visibility = Visibility.Visible;
            LabelNotAprooved2.Visibility = Visibility.Visible;

            MessageBoxResult result = MessageBox.Show("Похоже, в ваших данных есть ошибка!");
            if (result == MessageBoxResult.OK)
            {
                ELLIPSENotAprooved1.Visibility = Visibility.Hidden;
                ELLIPSENotAprooved2.Visibility = Visibility.Hidden;

                LabelNotAprooved1.Visibility = Visibility.Hidden;
                LabelNotAprooved2.Visibility = Visibility.Hidden;

                TextBoxLogin.Text = string.Empty;
                TextBoxPassword.Text = string.Empty;
            }
        }

        private async void BUTTONLogin_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxPassword.Text.Length == 11)
            {
                await Task.Delay(5000);

                Authorization();
            }
            else
            {
                await Task.Delay(5000);

                ELLIPSENotAprooved1.Visibility = Visibility.Visible;
                ELLIPSENotAprooved2.Visibility = Visibility.Visible;

                LabelNotAprooved1.Visibility = Visibility.Visible;
                LabelNotAprooved2.Visibility = Visibility.Visible;

                MessageBoxResult result = MessageBox.Show($"Привет, {_rolename}!\nПредоставленных Вами данных не были найдены в нашей системе.\nПопробуйте ещё раз!", 
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    ELLIPSENotAprooved1.Visibility = Visibility.Hidden;
                    ELLIPSENotAprooved2.Visibility = Visibility.Hidden;

                    LabelNotAprooved1.Visibility = Visibility.Hidden;
                    LabelNotAprooved2.Visibility = Visibility.Hidden;

                    TextBoxLogin.Text = string.Empty;
                    TextBoxPassword.Text = string.Empty;
                }
            }
        }
    }
}
