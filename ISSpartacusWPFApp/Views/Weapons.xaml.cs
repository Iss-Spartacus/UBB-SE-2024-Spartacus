using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using ISSpartacusWPFApp.Service;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ISSpartacusWPFApp.Views
{
    public partial class Weapons : Page
    {
        public Weapons()
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
                //check if the balance is enough
                //
                selectedWeapon.Availability = false;
                repository.zUpdateEntityByName(selectedWeapon.Name,selectedWeapon);

                // Refresh the list of available weapons
                ((WeaponsViewModel)DataContext).Weapons = new ObservableCollection<Weapon>(service.GetAvailableWeaponsService().ToList());
                MessageBox.Show($"The weapon {selectedWeapon.Name} has been bought.", "Weapon Bought", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
