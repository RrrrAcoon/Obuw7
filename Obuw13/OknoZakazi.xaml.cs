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
using System.Windows.Shapes;
using Obuw13.Modeli;
using System.Data.Entity;

namespace Obuw13
{
    /// <summary>
    /// Логика взаимодействия для OknoZakazi.xaml
    /// </summary>
    public partial class OknoZakazi : Window
    {
        Polzovatel _pol;
        ObuwKontext _db;
        RedaktZakazi _oknoZakaz;
        public OknoZakazi(Polzovatel pol,ObuwKontext db)
        {
            InitializeComponent();
            _pol = pol;
            _db = db;
            if(_pol.RolId==2)
            {
                BthDobavit.Visibility = Visibility.Collapsed;
                BthRedakt.Visibility = Visibility.Collapsed;
                BthUdalit.Visibility = Visibility.Collapsed;
            }
            Zagruzka();
        }

        void Zagruzka()
        {
            LvElement.ItemsSource = _db.Zakazi
                .Include(z => z.PunktVidachi)
                .Include(z => z.StatusZakaza)
                .Include(z => z.Polzovatel)
                .ToList();
        }
        bool OknoUzheOtkrito()
        {
            if (_oknoZakaz != null && _oknoZakaz.IsLoaded)
            {
                MessageBox.Show("Окно уже открыто!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                _oknoZakaz.Focus();
                return true;
            }
            return false;
        }

        void OtkritZakaz(Zakaz zakaz)
        {
            if (OknoUzheOtkrito()) return;
            _oknoZakaz = new RedaktZakazi(zakaz, _db);
            _oknoZakaz.ShowDialog();
            Zagruzka();
        }

        private void Nazad(object sender, RoutedEventArgs e)
        {
            new MainWindow(_pol).Show();
            Close();
        }
        private void Dobavit(object sender, RoutedEventArgs e)
        {
            OtkritZakaz(null);
        }

        private void Redakt(object sender, RoutedEventArgs e)
        {
            var z = LvElement.SelectedItem as Zakaz;
            if (z == null)
            {
                MessageBox.Show("Выбирите заказ!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            OtkritZakaz(z);
        }

        private void Udalit(object sender, RoutedEventArgs e)
        {
            var z = LvElement.SelectedItem as Zakaz;
            if (z == null)
            {
                MessageBox.Show("Выбирите заказ!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_db.ZakaziTovarov.Any(zt => zt.ZakazId == z.Id)) { MessageBox.Show("Товар присутсвует в заказах", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            if (MessageBox.Show("Удалить заказ?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            _db.Zakazi.Remove(z);
            _db.SaveChanges();
            Zagruzka();
        }

        private void ClickEl(object sender, MouseButtonEventArgs e)
        {
            if (_pol?.RolId == 1 && LvElement.SelectedItem is Zakaz)
                OtkritZakaz(LvElement.SelectedItem as Zakaz);
        }
    }
}
