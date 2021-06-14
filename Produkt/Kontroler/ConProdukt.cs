using Produkt.Helper;
using Produkt.Model;
using System;
using System.Globalization;
using static Produkt.Model.ProduktCls;
using static System.Console;

namespace Produkt.Kontroler
{
    public class ConProdukt
    {
        public ConProdukt()
        {
        }

        public void DodajProdukt()
        {
            WriteLine("Program do dodawania produktów");

            string inputName = GetFromUserString("Proszę podaj nazwę produktu");
            double inputCena = GetFromUserDouble("Proszę podaj cenę produktu, np 3.2");
            DateTime inputDate = GetFromUserDate("Proszę podaj datę zakupu. Dozwolony format to dd/mm/yyyy");
            double inputVat = GetFromUserDouble("Proszę podaj Vat, format 3.2 dla 3.2%");
            double inputZysk = GetFromUserDouble("Proszę podaj marże z produktu, format 3.2 dla 3.2%");

            WriteLine("Wybierz numer standardowej jednostki miary dla tego produktu");
            for (byte loopB = 0; loopB < 5; loopB++)
            {
                dynamic wd = (JednostkaMiary)loopB;
                WriteLine("\t {0} numer: {1}", wd, loopB);
            }
            byte? met = null;
            do
            {
                if (byte.TryParse(GeneralMethods.RemLetters(ReadLine()), out byte newMet) != true && (met >= 0 & met <= 5))
                {
                    WriteLine(@"Podana wartość nie jest liczba. Kończe zadanie");
                }
                else met = newMet;
            } while (met == null);

            var newProduct = new ProduktCls
                (
                inputName,
                inputCena,
                Convert.ToDateTime(inputDate),
                inputVat,
                inputZysk,
                Convert.ToByte(met)
                );

            WriteLine(newProduct.ProduktOpis());

            KlonPro(newProduct);

            Dziedziczenia(ref newProduct);
        }

        private void KlonPro(ProduktCls clonThis)
        {
            WriteLine(Environment.NewLine + "Klonowanie Produktu");
            dynamic nowyKlon = clonThis.Clone();
            nowyKlon.Nazwa = nowyKlon.Nazwa + " Klon";

            WriteLine(nowyKlon.ProduktOpis());
        }

        private void Dziedziczenia(ref ProduktCls basecls)
        {
            WriteLine("Dziedziczenie z Base klasy do Child");
            var deriwPro = new ProduktSpozywczy(basecls, DateTime.Now, 20);
            WriteLine(deriwPro.ProduktOpis());
        }

        private string GetFromUserString(string inputMsg)
        {
            string tempTxt;
            WriteLine(inputMsg);
            do
            {
                tempTxt = ReadLine().Trim();
                //inputName=
            } while (string.IsNullOrEmpty(tempTxt));
            return tempTxt;
        }

        private double GetFromUserDouble(string inputMsg)
        {
            NumberFormatInfo provider = new NumberFormatInfo();/// Dla poprawnego ustp;;awienia formatu double
            provider.NumberDecimalSeparator = ".";

            WriteLine(inputMsg);
            double tempdouble;
            do
            {
                if (double.TryParse(GeneralMethods.RemLetters(ReadLine().Replace(",", ".")), NumberStyles.Float, provider, out tempdouble) != true)
                    WriteLine(@"Nie poprawna wartość. Sprobój jeszcze raz");
            } while (tempdouble == 0);
            return tempdouble;
        }

        private DateTime GetFromUserDate(string inputMsg)
        {
            WriteLine(inputMsg);
            bool poprawnaData;
            string tempTxt;
            do
            {
                tempTxt = ReadLine().Trim();
                poprawnaData = GeneralMethods.IsDate(tempTxt);
                if (poprawnaData == false)
                    WriteLine(@"Nie poprawna wartość. Sprobój jeszcze raz");
            } while (poprawnaData == false);

            return Convert.ToDateTime(tempTxt);
        }
    }
}