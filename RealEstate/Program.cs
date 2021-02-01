using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RealEstate
{
    class Estate
    {
        public int Id;
        public string Type;
        public string Location;
        public string Description;
        public int Size;
        public int Price;
    }
    class Program
    {
        static void Display(List<Estate> estates)
        {
            foreach (Estate p in estates)
            {
                double pricePerM = (p.Price / p.Size);
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                Console.WriteLine("ID: " + p.Id + " Type: " + p.Type);
                Console.WriteLine("Location: " + p.Location);
                Console.WriteLine("Description: " + p.Description);
                Console.WriteLine("Size : " + p.Size + " m2");
                Console.WriteLine("Price: " + p.Price + " EUR, Price per m2 is: " + pricePerM + " EUR/m2");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            }


        }

      
        static void Logo()
        {
            
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("                             ");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" WELCOME TO REAL ESTATE ");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                         ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
        }

        static void Main(string[] args)
        {
            

            string path = @"C:\Temp\RealEstateData.csv";
            List<Estate> estates = new List<Estate>();
            if (!File.Exists(path))
                File.Create(path).Dispose();

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                Estate p = new Estate
                {
                    Id = int.Parse(values[0]),
                    Type = values[1],
                    Location = values[2],
                    Description = values[3],
                    Size = int.Parse(values[4]),
                    Price = int.Parse(values[5])
                };
                estates.Add(p);
            }


            //meny message with 6 options:
            bool go = true;
            while (go)
            {
                Logo();
              
                Console.WriteLine("Please select one of the options:");
                Console.WriteLine();
                Console.WriteLine("1. Show all properties");
                Console.WriteLine("2. Enter and save new property");
                Console.WriteLine("3. Search properties by size");
                Console.WriteLine("4. Search properties by price");
                Console.WriteLine("5. Delete property");
                Console.WriteLine("6. Exit");

                Console.WriteLine();
                Console.Write("Select option: ");
             
                try
                {
                    int option = int.Parse(Console.ReadLine());

                    if (option == 1)
                    {
                        Console.Clear();
                        //show all properties
                        if (estates.Count == 0)
                            Console.WriteLine("\n" + " There is no real estates to show " + "\n");

                        Display(estates);
                    }
                    else if (option == 2)
                    {
                        Console.Clear();
                        //enter new property
                        Console.WriteLine("Enter a new estate ");

                        // Random rnd = new Random();
                        //  int idnum = rnd.Next(4, 1000);
                        int idnum = estates.Count + 1;

                        Console.WriteLine("Enter type of estate (house,apartmant or land): ");
                        string s1 = Console.ReadLine();
                        Console.WriteLine("Enter the city where estete is located: ");
                        string s2 = Console.ReadLine();
                        Console.WriteLine("Enter desctiption of estate: ");
                        string s3 = Console.ReadLine();
                        Console.WriteLine("Enter the size in m2: ");
                        string s4 = Console.ReadLine();
                        Console.WriteLine("Enter the price in EUR: ");
                        string s5 = Console.ReadLine();


                        Estate e = new Estate
                        {
                            Id = idnum,
                            Type = s1,
                            Location = s2,
                            Description = s3,
                            Size = int.Parse(s4),
                            Price = int.Parse(s5)
                        };
                        estates.Add(e);

                        Console.WriteLine();
                        Console.WriteLine("The property is added. ");
                        Console.WriteLine();
                    }


                    else if (option == 3)
                    {
                        Console.Clear();
                        //search by size
                        Console.WriteLine("Please write minimal size in m2:");
                        int min = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please write maximal size in m2:");
                        int max = int.Parse(Console.ReadLine());

                        Console.WriteLine();
                        Console.WriteLine(" Result of searching: ");
                        Console.WriteLine();


                        List<Estate> searchSize = estates.Where(s => s.Size >= min && s.Size <= max).ToList<Estate>();
                        if (searchSize.Count == 0)
                            Console.WriteLine("\n" + " There is no real estates to show " + "\n");

                        Display(searchSize);
                    }

                    else if (option == 4)
                    {
                        Console.Clear();
                        //search by price
                        Console.WriteLine("Please write minimal price: ");
                        int min = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please write maximal price: ");
                        int max = int.Parse(Console.ReadLine());

                        Console.WriteLine();
                        Console.WriteLine(" Result of searching: ");
                        Console.WriteLine();

                        List<Estate> searchPrice = estates.Where(s => s.Price >= min && s.Price <= max).ToList<Estate>();

                        if (searchPrice.Count == 0)
                            Console.WriteLine("\n" + " There is no real estates to show " + "\n");

                        Display(searchPrice);
                    }

                    else if (option == 5)
                    {
                        Console.Clear();
                        //delete property by id number
                        Console.WriteLine("Please enter property ID number to delete it: ");
                        int iddel = int.Parse(Console.ReadLine());
                        for (int i = 0; i < estates.Count; i++)
                        {
                            if (estates[i].Id == iddel)
                            {
                                estates.Remove(estates[i]);
                                Console.WriteLine("The property is deleted");
                            }
                        }
                        Console.WriteLine();
                       
                        Console.WriteLine();


                    }

                    else if (option == 6)
                    {
                        //save all properties from list before exit (write it to txt file)  exit option + this
                        string all = ("");
                        string linija = ("");

                        foreach (Estate x in estates)
                        {
                            linija = (x.Id + "," + x.Type + "," + x.Location + "," + x.Description + "," + x.Size + "," + x.Price + ",");
                            all = all + (linija + "\n");
                        }
                        File.WriteAllText(path, all);
                        go = false;
                    }

                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please write number between 1 to 6");
                    }


                }
                catch 
                {
                    Console.Clear();
                    Console.WriteLine("You did not wrote number, please try again"); 
                }


            }


        }
    }
}
