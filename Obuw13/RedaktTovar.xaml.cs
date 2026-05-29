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
using Microsoft.Win32;
using Obuw13.Modeli;
using System.IO;

namespace Obuw13
{
    /// <summary>
    /// Логика взаимодействия для RedaktTovar.xaml
    /// </summary>
    public partial class RedaktTovar : Window
    {
        private Tovar _tovar = new Tovar();
        private ObuwKontext _db;
        public RedaktTovar(Tovar vibraniyTovar, ObuwKontext db)
        {
            InitializeComponent();
            _db = db;

            ComboEdinica.ItemsSource = _db.EdiniciIzmereniy.ToList();
            ComboPostavchik.ItemsSource = _db.Postavchiki.ToList();
            ComboProizvoditel.ItemsSource = _db.Proizvoditeli.ToList();
            ComboKategoriya.ItemsSource = _db.Kategorii.ToList();

            if (vibraniyTovar == null)
            {
                lbId.Visibility = Visibility.Collapsed;
                txtId.Visibility = Visibility.Collapsed;
            }
            else
            {
                _tovar = vibraniyTovar;
            }
            DataContext = _tovar;


        }

        private void Foto(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "Файлы рисунков (*.bmp, *.jpg)|*.bmp;*.jpg|Все файлы (*.*)|*.*" };
            if (dlg.ShowDialog() != true) return;

            var img = new BitmapImage();
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            img.UriSource = new Uri(dlg.FileName);
            img.EndInit();
            if(img.PixelWidth>300 || img.PixelHeight>200)
            {
                MessageBox.Show("Каотинка не может быть более 300х200 пикселей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string papka = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Kartinki");
            string fileName = Path.GetFileName(dlg.FileName);

            if(!string.IsNullOrEmpty(_tovar.Foto)&&_tovar.Foto!=fileName&&_tovar.Foto!="picture.png")
            {
                string old = Path.Combine(papka, _tovar.Foto);
                if(File.Exists(old))File.Delete(old);
            }
            File.Copy(dlg.FileName, Path.Combine(papka, fileName), true);
            _tovar.Foto = fileName;
            MessageBox.Show("Фото добавлено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Sohranit(object sender, RoutedEventArgs e)
        {
            if (_tovar.EdinicaIzmereniyaId== 0 || _tovar.PostavchikId== 0 || _tovar.Proizvoditellid== 0|| _tovar.KategoriyaId == 0)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(_tovar.Skidka<0||_tovar.Kolichestvo<0)
            {
                MessageBox.Show("Количество и скидка не могут быть менее 0!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_tovar.Id == 0) _db.Tovari.Add(_tovar);
            _db.SaveChanges();
            MessageBox.Show("Сохранено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
