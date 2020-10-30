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
            //Init an array where index is the month(-1) and the value is the length in days
            int[] monthLength = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            Console.WriteLine("Skriv in ditt personnummer");
            string userInput = Console.ReadLine();
            if (userInput.Length == 11)
            {
                if (int.TryParse(userInput.Substring(0, 6), out int yearCheck))
                {
                    if (userInput.Substring(6, 1) == "-" && !CompareToCurrentDate(userInput.Substring(0, 6)))
                    {
                        userInput = userInput.Insert(0, "19");
                    }
                    else if (userInput.Substring(6, 1) == "-")
                    {
                        userInput = userInput.Insert(0, "20");
                    }
                    //People with + are older than 100
                    if (userInput.Substring(6, 1) == "+")
                    {
                        if (CheckIfOverHundredYears(userInput.Substring(0, 6)) && !CompareToCurrentDate(userInput.Substring(0, 6)))
                        {
                            userInput = userInput.Insert(0, "18");
                        }
                        else
                        {
                            userInput = userInput.Insert(0, "19");
                        }
                    }
                }
                //remove the -+ sign so we can reuse our solution for level 1
                userInput = userInput.Remove(8, 1);
            }
            if (userInput.Length != 12)
            {
                Console.WriteLine("Felaktigt personnummer, fel antal");
            }
            //regular int too small
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
                    Console.WriteLine("Felaktigt personnummer, ange årtal mellan 1753-2020");
                }
                else if (!(month >= 1 && month <= 12))
                {
                    Console.WriteLine("Felaktigt personnummer, ange månad mellan 1-12");
                }
                //month -1 because arrays begin at 0
                else if (day == 0 || (day > monthLength[month - 1]))
                {
                    Console.WriteLine("Felaktigt personnummer, fel antal dagar i månad");
                }
                else if (!LuhnCheck(userInput.Substring(2), cNumber))
                {
                    Console.WriteLine("Felaktigt personnummer, fel kontrollsiffra " + userInput);
                }
                else
                {
                    Console.WriteLine("Personnummer {0} är korrekt och tillhör en {1}", userInput, CheckGender(birthnumber));
                }
            }
            //Stop
            Console.ReadKey();
        }
        //returns true if the year is a leapyear
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
            //Even last number is a woman, odd man
            if (birth % 2 == 0)
            {
                return "kvinna";
            }
            return "man";
        }
        static bool LuhnCheck(string formatedUserInput, int cNumber)
        {
            int sum;
            string split = "";
            for (int i = 1; i < formatedUserInput.Length; i++)
            {
                //% 2 + 1 to alternate between multiplying by 1 and 2
                sum = (int.Parse(formatedUserInput.Substring(i - 1, 1)) * ((i % 2) + 1));
                split += (sum.ToString());
            }
            sum = 0;
            for (int i = 0; i < split.Length; i++)
            {
                sum += int.Parse(split.Substring(i, 1));
            }
            //Sum is removed from nearest higher multiple of ten 
            //https://sv.wikipedia.org/wiki/Personnummer_i_Sverige#Kontrollsiffran
            if (10 - (sum % 10) % 10 == cNumber)
            {
                return true;
            }
            return false;
        }
        static bool CompareToCurrentDate(string userInputDate)
        {
            DateTime date = DateTime.Now;
            int userYear = int.Parse(userInputDate.Substring(0, 2));
            int userMonth = int.Parse(userInputDate.Substring(2, 2));
            int userDay = int.Parse(userInputDate.Substring(4, 2));
            //returns true if current time is(without century) is higher than userinputed time
            //this means we should treat the person as born during the 1900s
            if ((date.Year - 2000) > userYear)
            {
                return true;
            }
            else if ((date.Year - 2000) == userYear && (date.Month > userMonth))
            {
                return true;
            }
            else if ((date.Year - 2000) == userYear && (date.Month == userMonth) && date.Day >= userDay)
            {
                return true;
            }
            return false;
        }

        static bool CheckIfOverHundredYears(string userInputDate)
        {
            DateTime date = DateTime.Now;
            if (!CompareToCurrentDate(userInputDate))
            {
                userInputDate = userInputDate.Insert(0, "19");
            }
            else
            {
                userInputDate = userInputDate.Insert(0, "20");
            }
            int userYear = int.Parse(userInputDate.Substring(0, 4));
            int userMonth = int.Parse(userInputDate.Substring(4, 2));
            int userDay = int.Parse(userInputDate.Substring(6, 2));
            if (date.Year - userYear > 0)
            {
                return true;
            }
            else if (date.Year - userYear == 0 && date.Month - userMonth > 0)
            {
                return true;
            }
            else if (date.Year - userYear == 0 && date.Month - userMonth == 0 && date.Day - userDay > 0)
            {
                return true;
            }
            return false;
        }
    }
}
