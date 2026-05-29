using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obuw13.Modeli;

namespace Obuw13
{
    public class ObuwKontext : DbContext
    {
        public ObuwKontext() : base("name=PodklBd")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ObuwKontext>());
        }

        public DbSet<EdinicaIzmereniya> EdiniciIzmereniy { get; set; }
        public DbSet<Kategoriya> Kategorii { get; set; }
        public DbSet<Polzovatel> Polzovateli { get; set; }
        public DbSet<Postavchik> Postavchiki { get; set; }
        public DbSet<Proizvoditell> Proizvoditeli { get; set; }
        public DbSet<PunktVidachi> PunktiVidachi { get; set; }
        public DbSet<Rol> Roli { get; set; }
        public DbSet<StatusZakaza> StatusiZakazov { get; set; }
        public DbSet<Tovar> Tovari { get; set; }
        public DbSet<Zakaz> Zakazi { get; set; }
        public DbSet<ZakazTovar> ZakaziTovarov { get; set; }
       

    }
}
