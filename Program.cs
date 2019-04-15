using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;
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
            string jsontext = File.ReadAllText(@"volunteer.json");
            if (jsontext != "")
            {
                JsonConvert.PopulateObject(jsontext, allvolunteers2);//Daca Jsonul este empty da eroare, insa este necesar pentru a trece datele in array din json
            }

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
                                int x = Db.Idgetter(allvolunteers2);//cere numarul voluntarului care urmeaza sa fie modificat
                                Volunteer vol = Db.AddVolunteerfromCmd(x);
                                Db.AddVolunteer(vol, allvolunteers2);

                                break;

                            case 2:
                                if (allvolunteers2.Count() > 1)//ultimul voluntar nu poate fi sters ca da peste cap totul
                                {
                                    int y = Db.Selector(allvolunteers2);
                                    allvolunteers2.RemoveAt(y);//metoda pre definita
                                    Console.WriteLine("Volunteer Deleted");
                                    Db.SavetoJson(allvolunteers2);//mereu daca modificam ceva in allvollunteers2 trebuie sa facem schimbarea si in json ca data viitoare sa continuam tot de aici
                                }
                                else
                                {
                                    Console.WriteLine("You cannot delete the last volunteer!");
                                }
                                break;

                            case 3:
                                int z = Db.Selector(allvolunteers2);
                                //Trebuie facut o metoda update

                                break;

                            case 4:
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
