using System;
using MongoDB.Bson;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Bson.IO;
using System.IO;

namespace Databases2
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient();  
            var db = client.GetDatabase("ProgManager");//creaza baza de date cu numele ...
            var collection = db.GetCollection<Volunteer>("Volunteers");//Adauga o colectie in baza de date anterior creata
            bool aux = false;
            bool finished = false;
            int choice;
            int idforedit;
            Volunteer.Openfile();

            while (finished != true)
            {
                Console.WriteLine("Press 1 to add a person!");
                Console.WriteLine("Press 2 to Remove a person!");
                Console.WriteLine("Press 3 to Edit a person!");
                Console.WriteLine("Press 4 to Show Everyone!");
                Console.WriteLine("Press 5 to Exit!");

                Console.WriteLine("Please enter your choice!");
                try
                {
                    choice = Convert.ToInt16(Console.ReadLine());
                    {
                        switch (choice)
                        {
                            case 1:
                                // Add
                                Volunteer volunteer = ReadVolunteerFromCmdLine();
                                
                                db.AddVolunteer(volunteer);
                                //int x = Volunteer.Idgetter(collection);             
                                //Volunteer.AddPerson(x,collection);
                                
                                break;

                            case 2:
                                idforedit = Volunteer.ShowallMongoselect(collection);
                                Volunteer.Remove(idforedit,collection);

                                break;

                            case 3:
                                idforedit = Volunteer.ShowallMongoselect(collection);
                                Volunteer.Edit(idforedit, collection);
                             
                                break;

                            case 4:
                                aux = Volunteer.closefile();
                                Volunteer.ShowallMongo(collection);
                                aux = Volunteer.Openfile();
                                break;
                            case 5:
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
            if (aux == false)
            Volunteer.closefile();
        }
    }
}
