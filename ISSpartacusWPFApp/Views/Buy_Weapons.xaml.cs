using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using ISSpartacusWPFApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ISSpartacusWPFApp.Views
{
    /// <summary>
    /// Interaction logic for Buy_Weapons.xaml
    /// </summary>
    public partial class Buy_Weapons : Window
    {
        public Buy_Weapons()
        {
            InitializeComponent();
            DataContext = new WeaponsViewModel();
        }
        public class WeaponsViewModel
        {
            public ObservableCollection<Weapon> Weapons { get; set; }
            public Weapon? SelectedWeapon { get; set; }

            public WeaponsViewModel()
            {
                Weapons = new ObservableCollection<Weapon>(GetWeapons());
            }

            private List<Weapon> GetWeapons()
            {
                ConfigurationLoader.Configuration config = new ConfigurationLoader.Configuration();
                config.LoadFromJson("ConfigurationFile.json");
                WeaponRepository repository = new WeaponRepository(config);
                WeaponService service = new WeaponService(repository);
                return service.GetAvailableWeaponsService().ToList();
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewWeapons.SelectedItem is Weapon selectedWeapon)
            {
                ((WeaponsViewModel)DataContext).SelectedWeapon = selectedWeapon;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((WeaponsViewModel)DataContext).SelectedWeapon is Weapon selectedWeapon)
            {
                ConfigurationLoader.Configuration config = new ConfigurationLoader.Configuration();
                config.LoadFromJson("ConfigurationFile.json");
                WeaponRepository repository = new WeaponRepository(config);
                WeaponService service = new WeaponService(repository);
                //TODO check if the balance is enough
                //
                selectedWeapon.Availability = false;
                repository.zUpdateEntityByName(selectedWeapon.Name, selectedWeapon);

                // Refresh the list of available weapons
                ((WeaponsViewModel)DataContext).Weapons = new ObservableCollection<Weapon>(service.GetAvailableWeaponsService().ToList());
                MessageBox.Show($"The weapon {selectedWeapon.Name} has been bought.", "Weapon Bought", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    
}
}
