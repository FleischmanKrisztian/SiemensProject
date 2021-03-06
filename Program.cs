﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;
using System.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;

namespace SiemensProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("SiemensProject");//creaza baza de date cu numele ...
            var volunteercollection = db.GetCollection<Volunteer>("Volunteers");//Adauga o colectie in baza de date anterior creata
            var eventcollection = db.GetCollection<Event>("Events");
            bool finished = false;
            int choice;
            List<Volunteer> allvolunteers2 = new List<Volunteer>();
            //DONE
            //Intrebare daca Nar fi bine sa sterg characterul care cauzeaza problema
            string jsontext = File.ReadAllText(@"volunteer.json");
            bool success = false;
            //while (success = true)
            //{
                try
                {
                    var tmpObj = JsonValue.Parse(jsontext);
                    JsonConvert.PopulateObject(jsontext, allvolunteers2) ; //Daca Jsonul este empty da eroare, insa este necesar pentru a trece datele in array din json
                    success = true;
                }
                catch (FormatException fex)
                {
                    //Invalid json format
                    Console.WriteLine(fex);
                    finished = true;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    finished = true;
                }
            //}
            
               

            while (finished != true)
            {
                Console.WriteLine("Press 1 to add a person!");
                Console.WriteLine("Press 2 to Remove a person!");
                Console.WriteLine("Press 3 to Edit a person!");
                Console.WriteLine("Press 4 to Show Everyone!");
                Console.WriteLine("Press 5 to Show Upcoming birthdays!");
                Console.WriteLine("Press 6 to Show Upcoming contract expirations!");
                Console.WriteLine("Press 7 to Exit!");

                Console.WriteLine("Please enter your choice!");
                try
                {
                    choice = Convert.ToInt16(Console.ReadLine());
                    {
                        switch (choice)
                        {
                            case 1:
                                Volunteer vol = Db.AddVolunteerfromCmd();
                                allvolunteers2.Add(vol);
                                Db.SavetoJson(allvolunteers2);

                                break;

                            case 2:
                                int y = Db.Selector(allvolunteers2);
                                allvolunteers2.RemoveAt(y);//metoda pre definita
                                Console.WriteLine("Volunteer Deleted");
                                Db.SavetoJson(allvolunteers2); //mereu daca modificam ceva in allvollunteers2 trebuie sa facem schimbarea si in json ca data viitoare sa continuam tot de aici
                                break;

                            case 3:
                                int z = Db.Selector(allvolunteers2);
                                Db.Edit(allvolunteers2, z);
                                Db.SavetoJson(allvolunteers2);
                                break;

                            case 4:
                                //DE TERMINAT
                                Db.Showallvolunteers(allvolunteers2);

                                break;

                            case 5:
                                Db.ShowVolunteerbd(allvolunteers2);

                                break;
                            case 6:
                                Db.ShowVolunteercontractexp(allvolunteers2);
                                break;

                            case 7:
                                finished = true;

                                break;

                            default:
                                Console.WriteLine("Not a valid option!");

                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
}
