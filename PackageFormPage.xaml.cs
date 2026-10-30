using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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

namespace trpo_pract5_WPF_Savitsin
{
    /// <summary>
    /// Логика взаимодействия для PackageFormPage.xaml
    /// </summary>
    public partial class PackageFormPage : Page
    {
        private Package _package;
        private ObservableCollection<Package> _packageList;
        private Package? _originalPackage;
        public PackageFormPage(ObservableCollection<Package> PackageList, Package? package = null)            
        {
            _originalPackage = package;
            _packageList = PackageList;

            if (package == null)
            {
                _package = new Package();
                _package.Id = _packageList.Count();
            }
            else
            {
                _package = new Package
                {
                    Id = package.Id,
                    Recipient = package.Recipient,
                    Address = package.Address,
                    SendDate = package.SendDate
                };

                foreach (var i in package.Items)
                {
                    _package.Items.Add(new Attachment
                    {                        
                        Name = i.Name,
                        UnitWeightKg = i.UnitWeightKg,
                        Count = i.Count
                    });
                }
            }

            for (int i = 0; i < _package.Items.Count; i++)
            {
                _package.Items[i].PropertyChanged += Item_PropertyChanged;
            }

            InitializeComponent();
            DataContext = _package;
        }

        private void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ItemWeightKg")
            {
                _package.RefreshWeight();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_originalPackage == null)
            {
                _packageList.Add(_package);
                BackButton_Click(sender, e);
                return;
            }

            _originalPackage.Id = _package.Id;
            _originalPackage.Recipient = _package.Recipient;
            _originalPackage.Address = _package.Address;
            _originalPackage.SendDate = _package.SendDate;

            for (int i = 0; i < _originalPackage.Items.Count; i++)
            {
                _originalPackage.Items[i].PropertyChanged -= Item_PropertyChanged;
            }

            _originalPackage.Items.Clear();
            foreach (var i in _package.Items)
                _originalPackage.Items.Add(i);

            for (int i = 0; i < _originalPackage.Items.Count; i++)
            {
                _originalPackage.Items[i].PropertyChanged += Item_PropertyChanged;
            }

            _originalPackage.RefreshWeight();

            BackButton_Click(sender, e);
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            Attachment a = new Attachment();
            a.PropertyChanged += Item_PropertyChanged;
            _package.Items.Add(a);
            _package.RefreshWeight();
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedIndex == -1)
            {
                MessageBox.Show("Вложение не выбрано", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Attachment a = _package.Items[ItemsList.SelectedIndex];
            a.PropertyChanged -= Item_PropertyChanged;
            _package.Items.RemoveAt(ItemsList.SelectedIndex);
            _package.RefreshWeight();            
        }
    }
}
