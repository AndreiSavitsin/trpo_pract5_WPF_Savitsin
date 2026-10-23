using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace trpo_pract5_WPF_Savitsin
{
    /// <summary>
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public ObservableCollection<Package> Packages { get; set; } = new();

        public Package? SelectedPackage { get; set; }

        public ListPage()
        {
            Packages.Add(new Package() { Id = 1, Recipient = "Иван", Address = "Екатеринбург",
                SendDate = DateTime.Now, PackageWeightKg = 2,
                Name = "Название вложения", UnitWeightKg = 1, Count = 2, ItemWeightKg = 2 });

            Packages.Add(new Package() { Id = 2, Recipient = "Андрей", Address = "Москва",
                SendDate = DateTime.Now, PackageWeightKg = 5,
                Name = "Название вложения", UnitWeightKg = 1, Count = 2, ItemWeightKg = 2 });

            Packages.Add(new Package() { Id = 3, Recipient = "Дмитрий", Address = "Калининград",
                SendDate = DateTime.Now, PackageWeightKg = 1,
                Name = "Название вложения", UnitWeightKg = 1, Count = 2, ItemWeightKg = 2 });

            Packages.Add(new Package() { Id = 4, Recipient = "Данил", Address = "Санкт-Петербург",
                SendDate = DateTime.Now, PackageWeightKg = 4,
                Name = "Название вложения", UnitWeightKg = 1, Count = 2, ItemWeightKg = 2});

            Packages.Add(new Package() { Id = 5, Recipient = "Александр", Address = "Новосибирск",
                SendDate = DateTime.Now, PackageWeightKg = 3,
                Name = "Название вложения", UnitWeightKg = 1, Count = 2, ItemWeightKg = 2 });

            InitializeComponent();
            DataContext = this;
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPackage == null)
            {
                MessageBox.Show("Пользователь не выбран", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            bool confirm = MessageBox.Show(
                "Вы действительно хотите удалить запись?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                ) == MessageBoxResult.Yes;

            if (confirm)
            {
                Packages.Remove(SelectedPackage);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PackageFormPage(Packages));
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPackage == null)
            {
                MessageBox.Show("Не выбран элемент списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            NavigationService.Navigate(new PackageFormPage(Packages, SelectedPackage));
        }
        string fileName = "Packages.json";
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string jsonString = JsonSerializer.Serialize(Packages);
            File.WriteAllText(fileName, jsonString);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                Packages = JsonSerializer.Deserialize<ObservableCollection<Package>>(jsonString);
            }
            else
            {
                MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
