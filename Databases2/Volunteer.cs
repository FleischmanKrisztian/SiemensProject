using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;

namespace Databases2
{
    public class Volunteer
    {//din json:
        public int _id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public Address AddressDetails = new Address();
        public string Occupation { get; set; }
        public string Occupation_description { get; set; }
        public string Field_of_activity { get; set; }
        public string Desired_workplace { get; set; }

        Address adr = new Address();//face un obiect de tipul adresa

        internal static bool Openfile()//sterge } pentru a putea fi editat fisierul
        {

            string filepath = @"Volunteers.json";
            string yas = File.ReadAllText(filepath);
            yas = yas.TrimEnd(new char[] { '}' });
            yas = yas.TrimEnd(new char[] { ']' });
            File.WriteAllText(filepath, yas);
            return false;
        }


        internal static bool closefile()
        {
            File.AppendAllText(@"Volunteers.json", "]}");
            return true;
        }//adauga { pentru a se inchide fisierul

        public static void AddPerson(int x, IMongoCollection<Volunteer> collection)
        {
            Volunteer vol = new Volunteer();
            vol._id = x + 1;
            Console.WriteLine("---You have chosen to create a new volunteer---");
            Console.WriteLine("Please enter the person's:");
            Console.WriteLine("Firstname: ");
            vol.Firstname = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Lastname: ");
            vol.Lastname = Convert.ToString(Console.ReadLine());
            do
            {
                try
                {
                    vol.Age = 0;
                    Console.WriteLine("Age: ");
                    vol.Age = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine();
                    vol.Age = 0;
                }

            } while (vol.Age < 1);

            Console.WriteLine("Gender: ");
            vol.Gender = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Country: ");
            vol.AddressDetails.Country = Convert.ToString(Console.ReadLine());
            Console.WriteLine("City: ");
            vol.AddressDetails.City = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Street: ");
            vol.AddressDetails.Street = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Occupation: ");
            vol.Occupation = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Occuation Description: ");
            vol.Occupation_description = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Field of Activity: ");
            vol.Field_of_activity = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Desired workplace: ");
            vol.Desired_workplace = Convert.ToString(Console.ReadLine());
            Console.WriteLine("-----------Succes------------");
            string entry = JsonConvert.SerializeObject(vol);
            if (x == 0)
            {
                File.AppendAllText(@"Volunteers.json", "{ \"Volunteers\": [");
                File.AppendAllText(@"Volunteers.json", entry);
            }
            else
            {
                File.AppendAllText(@"Volunteers.json", ",");
                File.AppendAllText(@"Volunteers.json", entry);
            }
            collection.InsertOne(vol);// inregisreaza in mongodb noua peroana
            Console.WriteLine("Entered");

        }
        internal static void Edit(int idforedit, IMongoCollection<Volunteer> collection)
        {
            Volunteer vol = new Volunteer();
            Console.WriteLine("---You have chosen to Edit volunteer with id number: " + idforedit);
            Console.WriteLine("Please enter the person's:");
            Console.WriteLine("Firstname: ");
            vol.Firstname = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Lastname: ");
            vol.Lastname = Convert.ToString(Console.ReadLine());
            do
            {
                try
                {
                    vol.Age = 0;
                    Console.WriteLine("Age: ");
                    vol.Age = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine();
                    vol.Age = 0;
                }

            } while (vol.Age < 1);

            Console.WriteLine("Gender: ");
            vol.Gender = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Country: ");
            vol.AddressDetails.Country = Convert.ToString(Console.ReadLine());
            Console.WriteLine("City: ");
            vol.AddressDetails.City = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Street: ");
            vol.AddressDetails.Street = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Occupation: ");
            vol.Occupation = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Occuation Description: ");
            vol.Occupation_description = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Field of Activity: ");
            vol.Field_of_activity = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Desired workplace: ");
            vol.Desired_workplace = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Volunteer succesfully modified!");
            collection.UpdateOne(Builders<Volunteer>.Filter.Eq("_id", idforedit),
                                Builders<Volunteer>.Update
                                    .Set("Firstname", vol.Firstname)
                                    .Set("Lastname", vol.Lastname)
                                    .Set("Age", vol.Age)
                                    .Set("Gender", vol.Gender)
                                    .Set("Occupation", vol.Occupation)
                                    .Set("Occupation_description", vol.Occupation_description)
                                    .Set("Field_of_activity", vol.Field_of_activity)
                                    .Set("Desired_workplace", vol.Desired_workplace)
                                    .Set("AddressDetails.Country",vol.AddressDetails.Country)
                                    .Set("AddressDetails.City", vol.AddressDetails.City)
                                    .Set("AddressDetails.Street", vol.AddressDetails.Street)
                                    );
        }

        internal static void Remove(int idforedit, IMongoCollection<Volunteer> collection)
        {
            collection.DeleteOne(Builders<Volunteer>.Filter.Eq("_id", idforedit));

        }

        public static void ShowallJson()
        {
            Volunteer ble = new Volunteer();
            string strResultJson = JsonConvert.SerializeObject(ble);
            strResultJson = String.Empty;
            strResultJson = File.ReadAllText(@"Volunteers.json");
           
            var dictionary = JsonConvert.DeserializeObject<IDictionary>(strResultJson);
            foreach (DictionaryEntry entry in dictionary)
            {
                Console.WriteLine(entry.Key + ": " + entry.Value);
                //collection.InsertOneAsync(entry);
            }
        }
        public static int Idgetter(IMongoCollection<Volunteer> collection)
        {
            var vols = collection.AsQueryable<Volunteer>().ToList();
            int nr = 0;
            foreach (var volunteer in vols)
            {
                nr = volunteer._id;
            }
            return nr;
        }
        public static int ShowallMongoselect(IMongoCollection<Volunteer> collection)
        {
            var vols = collection.AsQueryable<Volunteer>().ToList();
            int nr=0;
            foreach(var volunteer in vols)
            {
                Console.WriteLine(volunteer._id + ". " + volunteer.Firstname + " " + volunteer.Lastname);
                nr = volunteer._id;
                
            }
            Console.WriteLine("Please enter the number of which Person you want to edit/remove!");
            int x=1;
            int k;
            bool done = false;
            while (done == false)
            {
                k = Convert.ToInt16(Console.ReadLine());
                if (k > nr || k <= 0)
                {
                    Console.WriteLine("Not a valid id number!\n Please enter another id!");
                }
                else
                {
                    x = k;
                    done = true;
                }
            }
            
         
            return x;
        }

        public static void ShowallMongo(IMongoCollection<Volunteer> collection)
        {
            var vols = collection.AsQueryable<Volunteer>().ToList();
            foreach (var volunteer in vols)
            {
                Console.WriteLine("Id: " + volunteer._id);
                Console.WriteLine("Firstname: " + volunteer.Firstname);
                Console.WriteLine("Lastname: " + volunteer.Lastname);
                Console.WriteLine("Age: " + volunteer.Age);
                Console.WriteLine("Gender: " + volunteer.Gender);
                Console.WriteLine("Address:\t" + volunteer.AddressDetails.Country + ", " + volunteer.AddressDetails.City + ", " + volunteer.AddressDetails.Street);
                Console.WriteLine("Occupation: " + volunteer.Occupation);
                Console.WriteLine("Occupation description: " + volunteer.Occupation_description);
                Console.WriteLine("Field of activity: " + volunteer.Field_of_activity);
                Console.WriteLine("Desired workplace: " + volunteer.Desired_workplace);
                Console.WriteLine();
            }
        }
    }
   
    public class Address
    {
        public string Street { get; set; }
        public string City;
        public string Country { get; set; }
    }

    
} 
  