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
    /// Логика взаимодействия для RedaktTovar.xaml
    /// </summary>
    public partial class RedaktTovar : Window
    {
        public RedaktTovar(Tovar vibraniyTovar, ObuwKontext db)
        {
            InitializeComponent();
        }
    }
}
