using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Test;

namespace Test_ADO.NET
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"];

            PersonService service = new PersonService(connectionString);
            Person person = 
            service.Login(LoginBox.Text, PasswordBox.Password);
            if(person != null)
            {
                NavigationService.Navigate(new Uri("/MainWindow.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/RegistrationPage.xaml", UriKind.Relative));
        }
    }
}
