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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"];

            PersonService service = new PersonService(connectionString);
            Person person = new Person();
            person.Name_person = NameBox.Text;
            person.Login_person = LoginBox.Text;
            person.Password_person = PasswordBox.Password;
            service.Create(person);

            NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
        }
    }
}
