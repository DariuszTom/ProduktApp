using Produkt.Kontroler;
using System;

namespace Produkt
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var newKon = new ConProdukt();
            newKon.DodajProdukt();
            Console.ReadKey();
        }
    }
}