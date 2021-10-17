using System;
using System.Linq;

namespace PragueParkingv1
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[] parking = new string[101];
            Array.Fill(parking, "EMPTY");
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu(parking);
            }
            Console.ReadKey(true);
        }

        // Metod för switch case menu med alternativ för olika handlingar 
        public static bool MainMenu(string[] parking)
        {

            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Clear();
                Console.WriteLine("Hi! And Welcome to Prague parking assistance!");
                Console.WriteLine("How can i be of service?");
                Console.WriteLine("1) Park a vehicle");
                Console.WriteLine("2) Relocate vehicles");
                Console.WriteLine("3) Search for a vehicle by registration number");
                Console.WriteLine("Q) Quit");
                Console.WriteLine("5) Print the array");
                Console.WriteLine("Make a selection:");

                switch (Console.ReadLine().ToLower())
                {
                    case "1":
                        {
                            ParkVehicle(parking);
                            return true;
                        }
                    case "2":
                        {
                            RelocateVehicle(parking);
                            return true;
                        }
                    case "3":
                        {
                            SearchVehicle(parking);
                            return true;
                        }
                    case "q":
                        {
                            Console.WriteLine("Thanks for using Prague parking assistance");
                            Console.WriteLine("Have a nice day!");
                            Environment.Exit(0);
                            return false;
                        }
                    case "5":
                        {
                            PrintArray(parking);
                            return true;
                        }
                    default:
                        {
                            Console.WriteLine("Error: Invalid Input");
                            return true;
                        }
                }
            }
        }

        // Metod för att leta efter parkerat fordon med hjälp av registeringsnummer
        public static void SearchVehicle(string[] parking)
        {
            // Frågar användaren om det är bil eller mc och läser av input och går till motsvarande case
            Console.WriteLine("Car or MC");
            switch (Console.ReadLine().ToUpper())
            {
                case "CAR":
                    {
                        // Frågar efter reg nummer och ger det värdet till en string och om 
                        // parking innehåller hit så skriver den ut vart den är placerad
                        Console.WriteLine("Type the registration number: ");
                        string hit = "CAR" + "_" + Console.ReadLine().ToUpper() + "%";
                        for (int i = 1; i < parking.Length - 1; i++)
                        {
                            if (hit == parking[i])
                            {
                                Console.WriteLine("{0} is located at {1}", hit, i);
                            }
                        }
                        Console.ReadKey();
                        return;
                    }
                case "MC":
                    {
                        // Frågar efter reg nummer och ger det värdet till en string och om 
                        // parking innehåller hit så skriver den ut vart den är placerad
                        Console.WriteLine("Type the registration number: ");
                        string hit = "MC" + "_" + Console.ReadLine().ToUpper();
                        for (int i = 100; i > 1; i--)
                        {
                            if (hit == parking[i])
                            {
                                continue;
                            }
                            if (parking[i].Contains('#'))
                            {
                                Console.WriteLine("{0} is located at {1}", hit, i);
                                break;
                            }

                        }
                        Console.ReadKey();
                        return;
                    }
                // Om annat än mc eller car skrivs in blir output "Invalid Input!" och går tillbaka till huvudmenyn
                default:
                    {
                        Console.WriteLine("Invalid Input!");
                        return;
                    }


            }

        }

        // Metod för flytta på ett parkerat fordon
        public static void RelocateVehicle(string[] parking)
        {
            // Frågar användaren om det är bil eller mc och läser av input och går till motsvarnade case
            Console.WriteLine("Car or MC");
            switch (Console.ReadLine().ToUpper())
            {
                case "CAR":
                    {
                        Console.WriteLine("Type in the registration number: ");
                        string hit = "CAR" + "_" + Console.ReadLine().ToUpper() + "%";
                        for (int i = 1; i < parking.Length - 1; i++)
                        {

                            if (hit == parking[i])
                            {
                                Console.WriteLine("{0} is located at {1}", hit, i);
                                Console.WriteLine("Do you wish to relocate? (y/n)");
                                string answer = Console.ReadLine().ToUpper();
                                string yes = "Y";
                                string no = "N";
                                if (answer == yes)
                                {
                                    Console.WriteLine("Enter a parkingspot: (1-100)");
                                    string relocate = Console.ReadLine();
                                    int index = int.Parse(relocate);
                                    var buffer = parking[i];
                                    parking[i] = parking[index];
                                    parking[index] = buffer;
                                    Console.WriteLine("Car: {0}, Successfully moved to spot : {1}", hit, index);
                                    Console.ReadKey();
                                    MainMenu(parking);
                                }
                                while (answer == no)
                                {
                                    break;
                                }
                            }

                        }
                        break;
                    }
                case "MC":
                    {
                        Console.WriteLine("Type the registration number: ");
                        string hit = "MC" + "_" + Console.ReadLine().ToUpper();

                        for (int i = 100; i < parking.Length; i--)
                        {
                            if (parking[i].Contains(hit + '#'))
                            {
                                hit = hit + '#';
                            }
                            if (parking[i].Contains(hit + '%'))
                            {
                                hit = hit + '%';

                            }

                            if (parking[i].Contains(hit))
                            {
                                Console.WriteLine("{0} is located at {1}", hit, i);
                                Console.WriteLine("Do you wish to relocate? (y/n)");
                                string answer = Console.ReadLine().ToUpper();
                                string yes = "Y";
                                string no = "N";
                                if (answer == yes)
                                {
                                    Console.WriteLine("Enter a Parkingspot: (1-100)");
                                    string relocate = Console.ReadLine();
                                    int index = int.Parse(relocate);
                                    if (parking[index].Contains('%'))
                                    {
                                        Console.WriteLine("Spot taken, Press any key to continue...");
                                        Console.ReadKey();
                                        break;
                                    }
                                    if (hit.Contains('%'))
                                    {
                                        var IndexRemover = hit.IndexOf('%', 0);
                                        parking[i] = parking[i].Remove(IndexRemover);
                                        parking[i] = parking[i] + '#';
                                    }
                                    else if (hit.Contains('#'))
                                    {
                                        parking[i] = parking[i].Remove(0, hit.Length);
                                        parking[i] = parking[i].Replace('%', '#');
                                    }
                                    if (parking[index].Contains("EMPTY"))
                                    {
                                        parking[index] = String.Empty;
                                        hit = hit.Replace('%', '#');
                                        parking[index] = hit;
                                    }
                                    else if (parking[index].Contains('#'))
                                    {
                                        Console.WriteLine("Do you want to park beside {0}? (y/n)", parking[index]);
                                        answer = Console.ReadLine().ToUpper();

                                        if (answer == yes)
                                        {
                                            hit = hit.Replace('#', '%');
                                            parking[index] += string.Join('#', hit);
                                            parking[i] = "EMPTY";
                                        }
                                        while (answer == no)
                                        {
                                            Console.WriteLine("Too bad.");
                                            break;
                                        }

                                    }
                                    Console.WriteLine("MC: {0}, Successfully moved to spot : {1}", hit, index);
                                    Console.ReadKey();
                                }
                                break;
                            }


                        }
                        break;
                    }


            }
            MainMenu(parking);
        }

        // Metod för att parkera ett fordon 
        public static void ParkVehicle(string[] parking)
        {
            // Ger string car värdet car och string mc värdet mc
            // Frågar användaren om typ av fordon
            string car = "car".ToUpper();
            string mc = "mc".ToUpper();
            Console.WriteLine("What type of vehicle do you want to park?");
            switch (Console.ReadLine().ToLower())   // switch case som skickar användaren beroende på typ av fordon 
            {
                case "car":
                    {
                        ParkingCar(car, parking);
                        DoneParking(parking);
                        break;
                    }
                case "mc":
                    {
                        ParkingMC(mc, parking);
                        DoneParking(parking);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public static void ParkingCar(string car, string[] parking)
        {
            // Ber användaren att skriva reg nummer och ger den värdet till string regNumberCar och 
            // ger invalid input om den överskrider 10 tecken och skickar tillbaka den till input regnummer
            string regNumberCar;
            Console.WriteLine("Enter registration number:");
            regNumberCar = Console.ReadLine().ToUpper();
            while (!regNumberCar.Contains("CAR"))
            {
                if (regNumberCar.Length <= 10)
                {
                    regNumberCar = "CAR" + '_' + regNumberCar;
                }
                else if (regNumberCar.Length > 10)
                {
                    Console.WriteLine("Too many Characters, Please try again (Max 10)");
                    break;
                }

                // Går igenom hela arrayen och ger nuvarande tid som värde till string currentTime
                for (int i = 1; i < 2; i++)
                {

                    if (!parking[i].Contains("EMPTY"))
                    {
                        i++;
                    }
                    parking[i] = regNumberCar;
                }

            }
        }

        // Metod för att registera mc i arrayen
        public static void ParkingMC(string mc, string[] parking)
        {
            // Ber användaren att skriva reg nummer och ger den värdet till string regNumberCar och
            // ger invalid input om den överskrider 10 tecken och skickar tillbaka den till input regnummer
            string regNumberMc;
            Console.WriteLine("Enter registration number:  ");
            regNumberMc = Console.ReadLine().ToUpper();
            while (!regNumberMc.Contains("MC"))
            {
                if (regNumberMc.Length <= 10)
                {
                    regNumberMc = "MC" + '_' + regNumberMc;
                }
                else if (regNumberMc.Length > 10)
                {
                    Console.WriteLine("Too many Characters, Please try again (Max 10)");
                    break;
                }

                for (int i = 100; i > 1; i--)
                {

                    if (parking[i].Contains('%'))
                    {
                        continue;
                    }
                    if (parking[i].Contains('#'))
                    {
                        parking[i] += regNumberMc + '%';
                        break;
                    }
                    if (parking[i].Contains("EMPTY"))
                    {
                        parking[i] = regNumberMc + '#';
                        break;
                    }

                }

            }
        }


        // Metod för att avsluta loop när man har parkerat klart
        public static void DoneParking(string[] parking)
        {
            // Frågar användaren om man gar parkerat klart och 
            // gör output värdet till string "answer" och
            // ger string "yes1" värdet y och "no1" värdet n
            Console.WriteLine("Are you done parking?(y/n)");
            string answer = Console.ReadLine().ToLower();
            string yes1 = "y";
            string no1 = "n";
            if (answer == yes1)
            {
                MainMenu(parking);
            }
            while (answer == no1)
            {
                ParkVehicle(parking);
                break;
            }
        }

        // Metod för att visa alla parkeringsplatser och se om de är upptagna eller inte
        public static void PrintArray(string[] parking)
        {
            // Rensar kosnolfönstret och delar upp arrayen så den är splittad i 5 kolumner och ger platsen färgen gul 
            // och "EMPTY" grön om platsen är ledig annars röd med fordon typen och reg nummer 
            Console.Clear();
            for (int j = 1; j < parking.Length; j++)
            {

                if (parking[j].Contains("EMPTY"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Plats: {0}", j);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" {0}", parking[j]);
                    Console.ResetColor();
                }
                if (!parking[j].Contains("EMPTY"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Plats: {0}", j);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(" {0}", parking[j]);
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Press any Key to Continue..."); Console.ReadKey();
        }
    }
}