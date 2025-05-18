using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;


namespace Terrorist_data_analizer
{
    internal class Program
    {
        static Dictionary<string,int> weaponsCounter = new Dictionary<string,int>();
        static Dictionary<string,int> organizationCounter = new Dictionary<string,int>();
        static void Main(string[] args)
        {
            //    var terrorists = new List<Dictionary<string, object>>();
            //    string[] firstNames = {
            //    "Ahmad", "Yousef", "Saeed","Khaled","Rami",
            //    "Maher", "Fadi", "Basem", "Nader", "Alaa",
            //    "Hussein", "Ali", "Wasim", "Mahmoud", "Ibrahim",
            //    "Tariq", "Jihad", "Samir", "Ammar", "Sharif"
            //};
            //    string[] lastNames = {
            //    "Khalil", "Mansour", "Al-Ali", "Zeidan","Yousef",
            //    "Salim", "Darwish", "Radi", "Jaber", "Naim",
            //    "Barakat", "Omran", "Al-Zahar", "Hijazi", "Kamal",
            //    "Najm", "Shaaban", "Maqdad", "Abu-Salim", "Qasem"
            //};
            //    string[] weapons = { "Knife", "M16", "Handgun" };
            //    string[] affiliations = { "Hamas", "Islamic Jihad" };
            //    Random rand = new Random();

            //    for (int i = 0; i < 20; i++)
            //    {
            //        var person = new Dictionary<string, object>();
            //        person["name"] = firstNames[rand.Next(firstNames.Length)] + " " +
            //                         lastNames[rand.Next(lastNames.Length)];
            //        person["weapons"] = GetRandomWeapons(rand, weapons);
            //        person["age"] = rand.Next(18, 51);
            //        var location = (
            //            Math.Round(rand.NextDouble() * 180 - 90, 2),
            //            Math.Round(rand.NextDouble() * 360 - 180, 2)
            //        );
            //        person["lastLocation"] = new Dictionary<string, string> {
            //        { "lat", location.Item1.ToString("F2") },
            //        { "lon", location.Item2.ToString("F2") }
            //    };
            //        person["affiliation"] = affiliations[rand.Next(affiliations.Length)];
            //        terrorists.Add(person);
            //    }

            //    var options = new JsonSerializerOptions { WriteIndented = true };
            //    string jsonOutput = JsonSerializer.Serialize(terrorists, options);
            //    File.WriteAllText("terrorists.txt", jsonOutput);

            //    Console.WriteLine("JSON file 'terrorists.txt' created successfully.");
            //}

            //static List<string> GetRandomWeapons(Random rand, string[] weapons)
            //{
            //    var list = new List<string>();
            //    foreach (var weapon in weapons)
            //    {
            //        if (rand.NextDouble() < 0.5)
            //            list.Add(weapon);
            //    }
            //    if (list.Count == 0)
            //        list.Add(weapons[rand.Next(weapons.Length)]);
            //    return list;
            //}
        }
    }
}
