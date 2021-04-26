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
    /// Логика взаимодействия для Base.xaml
    /// </summary>
    public partial class Base : Page
    {
        public Base()
        {
            InitializeComponent();
            //DGridBase.ItemsSource = CompanyDatabaseEntities.GetContext().Trainees.ToList();
        }

       

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Trainees));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var remove = DGridBase.SelectedItems.Cast<Trainees>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {remove.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CompanyDatabaseEntities.GetContext().Trainees.RemoveRange(remove);
                    CompanyDatabaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridBase.ItemsSource = CompanyDatabaseEntities.GetContext().Trainees.ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
                if (Visibility == Visibility.Visible)
            {
                CompanyDatabaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridBase.ItemsSource = CompanyDatabaseEntities.GetContext().Trainees.ToList();
            }
        }
    }
}
