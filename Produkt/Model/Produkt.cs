using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Produkt.Model
{
    public class ProduktCls : ICloneable
    {
        public enum JednostkaMiary
        {
            gram = 0, dekagram, kilogram, mililitr, litr
        }

        [Required]
        private string pNazwa;

        private double pCena;
        private DateTime pDataZakupu;
        private double pStawkaVat;
        private double pZysk;
        private JednostkaMiary pJM;

        public Guid IDpro { get; internal set; }

        public ProduktCls()
        {
        }

        public ProduktCls(string nazwa, double cena, DateTime dataZakupu, double stawkaVat, double zysk, byte pJM)
        {
            Nazwa = nazwa;
            Cena = cena;
            DataZakupu = dataZakupu;
            StawkaVat = stawkaVat;
            Zysk = zysk;
            PJM = pJM;
            IDpro = Guid.NewGuid();
        }

        public string Nazwa { get => pNazwa; set => pNazwa = value; }
        public double Cena { get => pCena; set => pCena = value; }
        public DateTime DataZakupu { get => pDataZakupu; set => pDataZakupu = value; }
        public double StawkaVat { get => pStawkaVat; set => pStawkaVat = value / 100; }
        public double Zysk { get => pZysk; set => pZysk = value / 100; }

        private byte PJM
        {
            set
            {
                try
                {
                    pJM = (JednostkaMiary)value;
                }
                catch { Console.WriteLine("Niepoprawna jednostka miar"); }
            }
        }

        public virtual string ProduktOpis()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nazwa Produktu:{Nazwa}");
            sb.AppendLine($"Cena:{Cena}-------Stawka Vat:{StawkaVat:P} ------- Marża {Zysk:P}");
            sb.AppendLine($"Data zakupu: {DataZakupu:dd/MM/yyyy}");
            sb.AppendLine($"Jednostka miar:{pJM}---- ID produktu:{IDpro}");

            return Environment.NewLine + sb.ToString();
        }

        #region Klonowanie klasy

        public object Clone()
        {
            var clone = this.MemberwiseClone();
            return clone;
        }

        #endregion Klonowanie klasy
    }
}