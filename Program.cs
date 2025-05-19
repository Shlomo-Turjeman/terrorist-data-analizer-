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
        static Dictionary<string, double[]> locationMap = new Dictionary<string, double[]>();
        static Dictionary<string, double> distanceMap = new Dictionary<string , double>();

        //דוגמא לדאטה להרצת התוכנית.
        static List<Dictionary<string, object>> peopleData = new List<Dictionary<string, object>>()
    {
        new Dictionary<string, object>()
        {
            { "name", "Sharif Salim" },
            { "weapons", new List<string>{ "M16" } },
            { "age", 50 },
            { "lastLocation", new Dictionary<string, string>
                {
                    { "lat", "-84.79" },
                    { "lon", "42.69" }
                }
            },
            { "affiliation", "Hamas" }
        },
        new Dictionary<string, object>()
        {
            { "name", "Khaled Maqdad" },
            { "weapons", new List<string>{ "Knife", "M16", "Handgun" } },
            { "age", 46 },
            { "lastLocation", new Dictionary<string, string>
                {
                    { "lat", "23.49" },
                    { "lon", "-150.87" }
                }
            },
            { "affiliation", "Hamas" }
        },
        new Dictionary<string, object>()
        {
            { "name", "Mahmoud Shaaban" },
            { "weapons", new List<string>{ "Handgun" } },
            { "age", 25 },
            { "lastLocation", new Dictionary<string, string>
                {
                    { "lat", "10.17" },
                    { "lon", "20.86" }
                }
            },
            { "affiliation", "Islamic Jihad" }
        },
        new Dictionary<string, object>()
        {
            { "name", "Fadi Kamal" },
            { "weapons", new List<string>{ "Knife", "Handgun" } },
            { "age", 30 },
            { "lastLocation", new Dictionary<string, string>
                {
                    { "lat", "75.55" },
                    { "lon", "-90.09" }
                }
            },
            { "affiliation", "Islamic " }
        },
    };


        //יצירת מילון של כמות הנשקים מכל סוג
        static void CreateWeaponsDict(List<Dictionary<string,object>> terrorist)
        {
            foreach(Dictionary<string,object> item in terrorist)
            {
                if (item.TryGetValue("weapons" ,out object currentType) && currentType is List<string> weapons)
                {
                    foreach(string weapon in weapons)
                    {
                        if (weaponsCounter.ContainsKey(weapon))
                        {
                            weaponsCounter[weapon]++;
                        }
                        else
                        {
                            weaponsCounter[weapon] = 1;
                        }
                    }
                }
                
            }
        }


        //יצירת מילון של כמות החברים בכל ארגון טרור
        static void CreateOrgDict(List<Dictionary<string, object>> terrorist)
        {
            foreach (Dictionary<string, object> item in terrorist)
            {
                if (item.TryGetValue("affiliation", out object currentType) && currentType is string org)
                {
                    
                    if (organizationCounter.ContainsKey(org))
                    {
                        organizationCounter[org]++;
                    }
                    else
                    {
                        organizationCounter[org] = 1;
                    }
                    
                }

            }
        }


        //מציאת מקסימום במילון 
        static string FindMaxValue(Dictionary<string, int> dict)
        {
            string result = "";
            int sumCurrent = 0;

            foreach(KeyValuePair<string, int> item in dict)
            {
                if (item.Value > sumCurrent)
                {
                    result = item.Key;
                    sumCurrent = item.Value;
                }
            }
            return result;
        }


        //מציאת מינימום במילון
        static string FindMinValue(Dictionary<string,int> dict)
        {
            string result = "";
            int sumCurrent = int.MaxValue;

            foreach (KeyValuePair<string, int> item in dict)
            {
                if (item.Value < sumCurrent)
                {
                    result = item.Key;
                    sumCurrent = item.Value;
                }
            }
            return result;
        }


        //מציאת מינימום במילון שהערכים הם מספרים מסוג דאבל
        static string FindMinValue(Dictionary<string, double> dict)
        {
            string result = "";
            double sumCurrent = int.MaxValue;

            foreach (KeyValuePair<string, double> item in dict)
            {
                if (item.Value < sumCurrent)
                {
                    result = item.Key;
                    sumCurrent = item.Value;
                }
            }
            return result;
        }


        //חישוב מרחק בין שני מיקומים
        static double Distance(double x1, double y1, double x2, double y2)
        {
            double latSum = x1 - x2;
            double lonSum = y1 - y2;    
            double latPow = Math.Pow(latSum, 2);
            double lonPow = Math.Pow(lonSum, 2);
            return Math.Sqrt(latPow +  lonPow);
        }


        //יצירת מילון מיקומים של כל טרוריסט
        static void CreateLocationMap(List<Dictionary<string,object>> terrorist)
        {
            foreach (Dictionary<string, object> item in terrorist)
            {
                if (item.TryGetValue("name", out object name) &&
                    item.TryGetValue("lastLocation", out object location) &&
                    location is Dictionary<string, string> locationDict)
                {
                    string currName = name.ToString();
                    string lat = locationDict["lat"];
                    string lon = locationDict["lon"];
                    double latNum = double.Parse(lat);
                    double lonNum = double.Parse(lon);
                    double[] locationArr = {latNum, lonNum};
                    locationMap[currName] = locationArr;
                }
            }

        }


        //יצירת מילון של כל זוגות הטרוריסטים שקרובים אחד לשני
        static void CreateDistanceDict(Dictionary<string, double[]> locationMap)
        {
            //string name1 = "";
            //string name2 = "";

            foreach(KeyValuePair<string, double[]> terrorist1 in locationMap)
            {
                string name1 = terrorist1.Key;
                double currDistance = double.MaxValue;
                double tempDistance = double.MaxValue;
                string name2 = "";



                foreach (KeyValuePair<string, double[]> terrorist2 in locationMap)
                {
                    string tempName = terrorist2.Key;

                    if (name1 !=  tempName)
                    {
                        tempDistance = Distance(terrorist1.Value[0], terrorist1.Value[1], terrorist2.Value[0], terrorist2.Value[1]);
                    }
                    if (tempDistance < currDistance)
                    {
                        currDistance = tempDistance;
                        name2 = tempName;
                    }
                }

                distanceMap[$"{name1} - {name2}"] = currDistance;

            }
        }


        //מציאת זוג הטרוריסטים שהכי קרובים
        static void ShowClosestTerrorist()
        {
            CreateLocationMap(peopleData);
            CreateDistanceDict(locationMap);
            string terroristNames = FindMinValue(distanceMap);
            string[] splitNames = terroristNames.Split("-");
            double minDistance = distanceMap[terroristNames];

            Console.WriteLine($"first terrorist: {splitNames[0]} second terrorist: {splitNames[1]} distance: {minDistance}");
        }


        //אימות קלט
        static string ValidateChoice(string choice)
        {
            string[] options = { "a", "b", "c", "d", "e" };

            while ((! options.Contains(choice.ToLower())) && (choice.ToLower() != "exit"))
            {
                Console.WriteLine("Invalid choice please try again");
                choice = Console.ReadLine();
            }

            return choice;
        }


        //הדפסת התפריט
        static void ShowMenue()
        {
            Console.WriteLine("######Menue######\n" +
                "A: Find the most common weapon.\n" +
                "B: Find the least common weapon.\n" +
                "C: Find the organization with the most members.\n" +
                "D: Find the organization with the least members\n" +
                "E: Find the 2 terrorists who are closest to each other\n" +
                "exit: closing program\n" +
                "Enter your choice(A-E or exit): ");
        }


        //ביצוע הפעולה שנבחרה
        static void MakingChoice(string choice)
        {
            switch(choice.ToLower())
            {
                case "a":
                    Console.WriteLine(FindMaxValue(weaponsCounter));
                    break;
                case "b":
                    Console.WriteLine(FindMinValue(weaponsCounter));
                    break;
                case "c":
                    Console.WriteLine(FindMaxValue(organizationCounter));
                    break;
                case "d":
                    Console.WriteLine(FindMinValue(organizationCounter));
                    break;
                case "e":
                    ShowClosestTerrorist();
                    break;
            }
        }


        //הרצת התוכנית בלולאה
        static void RunMenue()
        {
            string choice = "";
            CreateWeaponsDict(peopleData);
            CreateOrgDict(peopleData);


            do
            {
                ShowMenue();
                choice = ValidateChoice(Console.ReadLine());
                MakingChoice(choice);
            }

            while(choice != "exit".ToLower());

            Console.WriteLine("Closing...");

        }



        static void Main(string[] args)

        {
            RunMenue();
        }
    }
}
