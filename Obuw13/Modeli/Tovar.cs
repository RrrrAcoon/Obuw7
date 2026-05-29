using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Obuw13.Modeli
{
    [Table("Tovari")]
    public class Tovar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Artikul { get; set; }
        public string Naimenovanie { get; set; }
        public int EdinicaIzmereniyaId { get; set; }
        public virtual EdinicaIzmereniya EdinicaIzmereniya{ get; set; }
        public decimal Cena{ get; set; }
        public int PostavchikId { get; set; }
        public virtual Postavchik Postavchik{ get; set; }
        public int Proizvoditellid { get; set; }
        public virtual Proizvoditell Proizvoditell{ get; set; }
        public int KategoriyaId { get; set; }
        public virtual Kategoriya Kategoriya{ get; set; }
        public int Skidka { get; set; }
        public int Kolichestvo{ get; set; }
        public string Opisanie { get; set; }
        public string Foto { get; set; }

        public virtual ICollection<ZakazTovar> ZakaziTovarov { get; set; }
        [NotMapped]
        public decimal TecuchayaCena => Skidka > 0 ? Math.Round(Cena - Cena * (decimal)Skidka / 100m, 2) : Cena;
        [NotMapped]
        public string Shapka => $"{Kategoriya.Nazvanie} | {Naimenovanie}";
        [NotMapped]
        public string CvetFona => Kolichestvo <= 0 ? "" : (Skidka > 15 ? "" : "Transparent");
        [NotMapped]
        public string NormalnayaCena => Skidka > 0 ? "Collapsed" : "Visible";
        [NotMapped]
        public object PolniyPutKFoto
        {
            get
            {
                string papka = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Kartinki");
                string put = Path.Combine(papka, Foto ?? "pivture.png");

                if (!File.Exists(put))
                    put = Path.Combine(papka, "picture.png");

                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                img.UriSource = new Uri(papka,UriKind.Absolute);
                img.EndInit();

                return img;
            }
        }

    }
}
