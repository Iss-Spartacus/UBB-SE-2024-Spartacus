using DataAccessLibrary.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccessLibrary.Repository;
using System.IO;
using ISSpartacusWPFApp.Service;
namespace ISSpartacusWPFApp.Views
{
    /// <summary>
    /// Interaction logic for Weapons.xaml
    /// </summary>
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

            public WeaponsViewModel()
            {
                Weapons = new ObservableCollection<Weapon>(GetWeapons());
            }

            private List<Weapon> GetWeapons()
            {
                // Call your repository here and return the list of weapons
                ConfigurationLoader.Configuration config = new ConfigurationLoader.Configuration();
                config.LoadFromJson("ConfigurationFile.json");
                WeaponRepository repository = new WeaponRepository(config);
                WeaponService service = new WeaponService(repository);
                return service.GetAvailableWeaponsService().ToList();
            }
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
