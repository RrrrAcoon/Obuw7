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

        void OtkritTovar(Tovar tovar)
        {
            if (OknoUzheOtkrito()) return;
            _oknoZakaz = new RedaktTovar(tovar, _db);
            _oknoZakaz.ShowDialog();
            ObnovlenieDannih();
        }

        private void Nazad(object sender, RoutedEventArgs e)
        {
            new MainWindow(_pol).Show();
            Close();
        }
        private void Dobavit(object sender, RoutedEventArgs e)
        {
            OtkritTovar(null);
        }

        private void Redakt(object sender, RoutedEventArgs e)
        {
            var t = LvElement.SelectedItem as Tovar;
            if (t == null)
            {
                MessageBox.Show("Выбирите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            OtkritTovar(t);
        }

        private void Udalit(object sender, RoutedEventArgs e)
        {
            var t = LvElement.SelectedItem as Tovar;
            if (t == null)
            {
                MessageBox.Show("Выбирите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (db.ZakaziTovarov.Any(zt => zt.TovarId == t.Id)) { MessageBox.Show("Товар присутсвует в заказах", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            if (MessageBox.Show("Удалить товар?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            db.Tovari.Remove(t);
            db.SaveChanges();
            ObnovlenieDannih();
        }

        private void ClickEl(object sender, MouseButtonEventArgs e)
        {
            if (_pol?.RolId == 1 && LvElement.ItemsSource is Tovar)
                OtkritTovar(LvElement.SelectedItem as Tovar);
        }
    }
}
