using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            _package = package == null ? new Package() : new Package
            {
                Id = package.Id,
                Recipient = package.Recipient,
                Address = package.Address,
                SendDate = package.SendDate,
                PackageWeightKg = package.PackageWeightKg,
                Name = package.Name,
                UnitWeightKg = package.UnitWeightKg,
                Count = package.Count,
                ItemWeightKg = package.ItemWeightKg
            };
            _packageList = PackageList;
            InitializeComponent();
            DataContext = _package;
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
            _originalPackage.PackageWeightKg = _package.PackageWeightKg;
            _originalPackage.Name = _package.Name;
            _originalPackage.UnitWeightKg = _package.UnitWeightKg;
            _originalPackage.Count = _package.Count;
            _originalPackage.ItemWeightKg = _package.ItemWeightKg;
            BackButton_Click(sender, e);
        }
    }
}
