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
    /// Логика взаимодействия для AddEditPage_ИСиТ.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private mountain _currentMountain = new mountain(); 
        public AddEditPage(mountain selectedMountain)
        {
            InitializeComponent();

            if (selectedMountain != null)
                _currentMountain = selectedMountain;

            DataContext = _currentMountain; 
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentMountain.mountain_name))
                errors.AppendLine("Укажите ФИО клиента");

            if (_currentMountain.height <= 0)
                errors.AppendLine("Сумма займа должна быть больше 0");

            if (string.IsNullOrWhiteSpace(_currentMountain.country))
                errors.AppendLine("Укажите срок займа");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentMountain.mountain_id == 0)
            {
                ИСиТ_лабыEntities.GetContext().mountains.Add(_currentMountain);
            }

            try
            {
                ИСиТ_лабыEntities.GetContext().SaveChanges(); 
                MessageBox.Show("Информация сохранена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); 
            }
        }
    }
}
