using System;
using System.Reflection;
using System.Text;

namespace Produkt.Model
{
    public class ProduktSpozywczy : ProduktCls
    {
        private DateTime pDataWaznosci;
        private double pWaga;

        public ProduktSpozywczy(DateTime dataWaz, double waga, string nazwa, double cena, DateTime dataZakupu, double stawkaVat, double zysk, byte pJM)
                                : base(nazwa, cena, dataZakupu, stawkaVat, zysk, pJM)
        {
            this.DataWaznosci = dataWaz;
            this.Waga = waga;
        }

        public ProduktSpozywczy(ProduktCls basePro, DateTime dataWaz, double waga)
        {
            foreach (PropertyInfo prop in basePro.GetType().GetProperties())
            {
                PropertyInfo prop2 = basePro.GetType().GetProperty(prop.Name);

                prop2.SetValue(this, prop.GetValue(basePro, null), null);
            }
            this.DataWaznosci = dataWaz;
            this.Waga = waga;
        }

        public DateTime DataWaznosci { get => pDataWaznosci; set => pDataWaznosci = value; }
        public double Waga { get => pWaga; set => pWaga = value; }

        public bool DoSpozycia() => DataWaznosci < DateTime.UtcNow;

        public bool DoSpozycia(DateTime dataToCheck) => DataWaznosci < dataToCheck;

        public override string ProduktOpis()
        {
            var sb = new StringBuilder();
            sb.Append(base.ProduktOpis());
            sb.AppendLine($"Produkt Spozywczy");
            sb.AppendLine($"Data Wazności:{DataWaznosci}---- Waga:{Waga}");
            return sb.ToString();
        }
    }
}