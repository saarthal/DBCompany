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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Trainees _currentTraine = new Trainees();

        public AddEditPage(Trainees selectedTrainees)
        {
            InitializeComponent();

            if (selectedTrainees != null)
                _currentTraine = selectedTrainees;

            DataContext = _currentTraine;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentTraine.FirstName))
                errors.AppendLine("Укажите имя");

            if (string.IsNullOrWhiteSpace(_currentTraine.Surname))
                errors.AppendLine("Укажите фамилию");

            if (string.IsNullOrWhiteSpace(_currentTraine.Patronymic))
                errors.AppendLine("Укажите отчество");

            if (_currentTraine.Age < 18 || _currentTraine.Age > 80)
                errors.AppendLine("Укажите возраст от 18 до 80");

            if (string.IsNullOrWhiteSpace(_currentTraine.Classification))
                errors.AppendLine("Укажите уровень");

            if (string.IsNullOrWhiteSpace(_currentTraine.TheDepartment))
                errors.AppendLine("Укажите отдел");

            if (_currentTraine.Salary < 0 || _currentTraine.Salary > 100000)
                errors.AppendLine("Укажите заплату от 0 до 100000");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentTraine.id == 0)
                CompanyDatabaseEntities.GetContext().Trainees.Add(_currentTraine);


            try
            {
                CompanyDatabaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }
    }
}
