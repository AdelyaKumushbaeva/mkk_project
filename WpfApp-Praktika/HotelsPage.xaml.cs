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
using WpfApp_ИСиТ_2_лаба;

namespace WpfApp_ИСиТ

{
    /// <summary>
    /// Логика взаимодействия для Page1_ИСиТ.xaml
    /// </summary>
    
    public partial class HotelsPage : Page
    {
        public HotelsPage()
        {
            InitializeComponent();
            DGridHotels.ItemsSource = ИСиТ_лабыEntities.GetContext().mountains.ToList();  
        }        


        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as mountain));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var mountainsForRemoving = DGridHotels.SelectedItems.Cast<mountain>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {mountainsForRemoving.Count()} элементов?", "Внимание",
        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ИСиТ_лабыEntities.GetContext().mountains.RemoveRange(mountainsForRemoving);

                    ИСиТ_лабыEntities.GetContext().SaveChanges();

                    MessageBox.Show("Данные удалены!");

                    DGridHotels.ItemsSource = ИСиТ_лабыEntities.GetContext().mountains.ToList();
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
                ИСиТ_лабыEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridHotels.ItemsSource = ИСиТ_лабыEntities.GetContext().mountains.ToList();               
            }
        }
    }
}
