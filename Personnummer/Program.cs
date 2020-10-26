using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnummer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Init an array where index+1 is the month and the value is the length in days
            int[] monthLength = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            Console.WriteLine("Skriv in ditt personnummer");
            string userInput = Console.ReadLine();
            //userInput = userInput.Insert(0,"19");
            //if (userInput.Length == 10)
            //{
            //    if (userInput.Substring(9,1) == "-" && userInput.)
            //    {
            //        userInput = userInput.Insert(0, "19");
            //    }
            //    else if (userInput.Substring(9, 1) == "+")
            //    {

            //    }
            //}


            LuhnCheck("8112189870");

            if (userInput.Length != 12)
            {
                Console.WriteLine("Felaktigt personnummer, fel antal");
            }
            else if (UInt64.TryParse(userInput, out _) == false)
            {
                Console.WriteLine("Felaktigt personnummer, ange siffor");
            }
            else  
            {
                //We already checked if the input is an integer so we can safely assign our values
                //scope of variables inside if 
                int year = int.Parse(userInput.Substring(0, 4));
                int month = int.Parse(userInput.Substring(4, 2));
                int day = int.Parse(userInput.Substring(6, 2));
                int birthnumber = int.Parse(userInput.Substring(8, 3));
                int cNumber = int.Parse(userInput.Substring(11, 1));
                
                if (LeapYear(year))
                {
                    //if its a leap year february is 29 days long
                    monthLength[1] = 29;
                }
                
                if (year >= 1753 && year <= 2020 == false)
                {
                    Console.WriteLine("Felaktigt personnummer, ange [rtal mellan 1753-2020");
                }
                else if (!(month >= 1 && month <=12))
                {
                    Console.WriteLine("Felaktigt personnummer, ange manad mellan 1-12");
                }
                //month -1 because arrays begin at 0
                else if (day == 0 || (day > monthLength[month-1]))
                {
                    Console.WriteLine("Felaktigt personnummer, fel antal dagar i manad");
                }

                else
                {
                    Console.WriteLine("Personnummer {0} är korrekt och tillhör en {1}", userInput, CheckGender(birthnumber));
                }



            }


            Console.ReadKey();
        }

        

        static bool LeapYear(int year)
        {
            if (year % 400 == 0)
            {
                return true;
            }
            else if (year % 100 == 0)
            {
                return false;
            }
            else if (year % 4 == 0)
            {
                return true;
            }
            //else
            return false;
        }

        static string CheckGender(int birth)
        {
            if (birth % 2 == 0)
            {
                return "kvinna";
            }

            return "man";
        }

        static bool LuhnCheck(string formatedUserInput)
        {
            int sum;
            string split = "";
            // (var % 2) +1
            for (int i = 1; i < formatedUserInput.Length; i++)
            {
                //% 2 + 1 to alternate between multiplying by 1 and 2
                sum = (int.Parse(formatedUserInput.Substring(i-1, 1)) * ((i % 2) + 1));
                split += (sum.ToString());

            }
            sum = 0;
            for (int i = 0; i < split.Length; i++)
            {
                sum += int.Parse(split.Substring(i, 1));
            }
            return false;
        }
    }
}
