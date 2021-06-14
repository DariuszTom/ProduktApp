using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Produkt.Helper
{
    public static class GeneralMethods
    {
        public static string AppPath() => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);

        public static string SolutionPath() => Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

        public static string RemLetters(string myString)
        {
            string remLettersRet = default;
            string regPattern = "[a-zA-Z]+";

            var regx = new Regex(regPattern);
            remLettersRet = regx.Replace(myString, "");
            remLettersRet = Regex.Replace(remLettersRet, @"\,$", "");
            return remLettersRet;
        }

        public static string FormatPath(string path)
        {
            string slash = @"\";// check if path is ending with slash
            char c = path.Trim().ToString().Last();
            if (c.ToString() != slash) path += slash;
            return path;
        }

        public static List<string[]> VBCSVParser(string path)
        {
            //Parser CSV jeszcze z czasow VB (6.0) nadal najlepsze z wszystkiego co C# moze w tej
            // kwesti zaoferowac -Dariusz Tomczak
            if (File.Exists(path) == false) return null;
            List<string[]> tempList = new List<string[]>();
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { ";" });
                csvParser.HasFieldsEnclosedInQuotes = true;

                csvParser.ReadLine();
                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    tempList.Add(fields);
                }
                tempList = tempList //Linq sort
                        .OrderBy(arr => arr[0])
                        .ThenBy(arr => arr[1])
                        .ToList();
                return tempList;
            }
        }

        public static string RandomeString(int length)
        {
            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return (str_build.ToString());
        }

        public static DateTime PiorWKDay(DateTime today, byte minusDays)
        {
            DateTime prevWorkday = today.AddDays(-minusDays);
            while (prevWorkday.ToString("dddd") == "Saturday" || prevWorkday.ToString("dddd") == "Sunday")
            {
                prevWorkday = prevWorkday.AddDays(-minusDays);
            }
            return prevWorkday;
        }

        public static bool IsDate(Object o)
        {
            if (o is DateTime dt)
            {
                return true;
            }
            else if (DateTime.TryParse(o?.ToString(), out dt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}