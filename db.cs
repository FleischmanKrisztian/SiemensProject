
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
using MongoDB.Driver;

namespace SiemensProject
{
    class Db
    {
        public static void AddVolunteer(Volunteer vol,List<Volunteer> allvolunteers2)//aici se appendeaza in json noul voluntar, stiu ca a zis ca trebe sa facem fara [] dar alta cale pur si simplu nu am gasit
        {
            string entry = JsonConvert.SerializeObject(vol);
            allvolunteers2.Add(vol);
            string filepath = @"volunteer.json";
            string yas = File.ReadAllText(filepath);
            yas = yas.TrimEnd(new char[] { ']' });
            File.WriteAllText(filepath, yas);
            if (vol.id == 1)
            {
                File.AppendAllText(@"volunteer.json", "[");
            }
            else
            {
                File.AppendAllText(@"volunteer.json", ",");
            }
            File.AppendAllText(@"volunteer.json", entry);
            File.AppendAllText(@"volunteer.json", "]");
        }

        public static void Showalljson()//Nu Functioneza nu stiu de ce dar oricum nu e nevoie de el daca merge show all volunteers
        {
            Volunteer ble = new Volunteer();
            string strResultJson = JsonConvert.SerializeObject(ble);
            strResultJson = String.Empty;
            strResultJson = File.ReadAllText(@"volunteer.json");
            var dictionary = JsonConvert.DeserializeObject<IDictionary>(strResultJson);
            foreach (DictionaryEntry entry in dictionary)
            {
                Console.WriteLine(entry.Key + ": " + entry.Value);
            }
        }

        public static int Idgetter(List<Volunteer> allvolunteers2)//Returneaza cel mai mare id dintre toate voluntari (este folosit cand creeam un nou voluntar si mai incrementam aceasta valoare)
        {
            var vols = allvolunteers2.AsQueryable<Volunteer>().ToList();
            int nrofvols = 0;
            foreach (var volunteer in vols)
            {
                nrofvols = volunteer.id;
            }
            return nrofvols;
        }

        internal static Volunteer AddVolunteerfromCmd(int x)//Aici se creaza un nou voluntar de la tastatura
        {
            Volunteer vol = new Volunteer();
            vol.id = x + 1;
            Console.WriteLine("You have chosen to add a Volunteer");
            Console.WriteLine("Please enter the persons:");
            Console.WriteLine("Firstname: ");
            vol.Firstname = Console.ReadLine();
            Console.WriteLine("Lastname: ");
            vol.Lastname = Console.ReadLine();
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
                    Console.WriteLine("Please enter a Number");
                    vol.Age = 0;
                }

            } while (vol.Age < 1);// int  pentru age
            /*Console.WriteLine("Country: ");
            vol.Address.Country = Console.ReadLine();
            Console.WriteLine("City: ");
            vol.Address.City = Console.ReadLine();
            Console.WriteLine("Street: ");
            vol.Address.Street = Console.ReadLine();
            Console.WriteLine("Number: ");
            vol.Address.Number = Console.ReadLine();
            Console.WriteLine("Gender: ");
            vol.Gender = Console.ReadLine();*/
            do
            {
                try
                {
                    vol.birthdate.Year = 0;
                    Console.WriteLine("Birth Year: ");
                    vol.birthdate.Year = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a Number");
                    vol.birthdate.Year = 0;
                }

            } while (vol.birthdate.Year < 1);//int pentru brthyear
            do
            {
                try
                {
                    vol.birthdate.Month = 0;
                    Console.WriteLine("Birth Month: ");
                    vol.birthdate.Month = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a Number");
                    vol.birthdate.Month = 0;
                }

            } while (vol.birthdate.Month < 1);//int pentru brthmonth
            do
            {
                try
                {
                    vol.birthdate.Day = 0;
                    Console.WriteLine("Birth Day: ");
                    vol.birthdate.Day = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a Number");
                    vol.birthdate.Day = 0;
                }

            } while (vol.birthdate.Day < 1);//int pentru birthday
           /* Console.WriteLine("Has Driving License? (true/false)"); //trebe facut pentru boolean
            vol.AditionalInfo.HasDrivingLicence = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Has Car? (true/false)");//trebe facut pentru boolean
            vol.AditionalInfo.HasCar = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("EmailAddress: : ");
            vol.ContactInformations.MailAdress = Console.ReadLine();
            Console.WriteLine("PhoneNumber: : ");
            vol.ContactInformations.PhoneNumber = Console.ReadLine();
            Console.WriteLine("HasContract (true/false)");//trebe facut pentru boolean
            vol.Contract.HasContract = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Contract Period in months");
            vol.Contract.ContractPeriod = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Number of Registration: : ");
            vol.Contract.NumberOfRegistration = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Registration Year");
            vol.registrationday.Year = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Registration Month");
            vol.registrationday.Month = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Registration Day");
            vol.registrationday.Day = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Desired Workplace");
            vol.Desired_workplace = Console.ReadLine();
            Console.WriteLine("Field of activity");
            vol.Field_of_activity = Console.ReadLine();
            Console.WriteLine("Hour Count");
            vol.HourCount = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Active? (true/false)");//trebe facut pentru boolean
            vol.InActivity = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Occupation");
            vol.Occupation = Console.ReadLine();
            Console.WriteLine("Occupation Description");
            vol.Occupation_description = Console.ReadLine();
            Console.WriteLine("How many Hours a week");
            vol.WorkSchedule.HoursWeek = Console.ReadLine();
            Console.WriteLine("Which days is he available");
            vol.WorkSchedule.Comments.Days = Console.ReadLine();
            Console.WriteLine("What hours is he available");
            vol.WorkSchedule.Comments.TimeAvailability = Console.ReadLine();*/

            Console.WriteLine("Volunteer successfully added!");

            return vol;
        }

        internal static void ShowVolunteerbd(List<Volunteer> allvolunteers2)
        {
            string todaydate = DateTime.Today.ToString("dd-MM-yyyy");
            string[] dates = todaydate.Split("-");
            int Day = Convert.ToInt16(dates[0]);
            int Month = Convert.ToInt16(dates[1]);
            int Year = Convert.ToInt16(dates[2]);
            Day = (Month-1) * 30 + Day;
            int voldays;
            
            Console.WriteLine("The following Volunteers are going to have birthdays");
            foreach (Volunteer vol in allvolunteers2)
            { 
                    voldays = (vol.birthdate.Month-1) * 30 + vol.birthdate.Day;
                Console.WriteLine("Datadeazi: " + Day + " birthdays of vols: " + voldays);
                if (Day < voldays && Day+7 >voldays)//mai trebe aici caz special cand este decembrie 20 - ianuarie 7
                {
                    Console.WriteLine(vol.Firstname + " " + vol.Lastname + " : " + vol.birthdate.Year + "." + vol.birthdate.Month + "." + vol.birthdate.Day);
                }

            }
        }

        internal static void ShowVolunteercontractexp(List<Volunteer> allvolunteers2)
        {
            string todaydate = DateTime.Today.ToString("dd-MM-yyyy");
            string[] dates = todaydate.Split("-");
            int Day = Convert.ToInt16(dates[0]);
            int Month = Convert.ToInt16(dates[0]);
            int Year = Convert.ToInt16(dates[0]);
            Day = Year*365 + (Month - 1) * 30 + Day;
            int voldays;
            Console.WriteLine("The following Volunteers are going to have their contracts expired in the next 20 days!");
            foreach (Volunteer vol in allvolunteers2)
            {
                voldays = Year*365 + (vol.birthdate.Month - 1 + vol.Contract.ContractPeriod) * 30 + vol.birthdate.Day;
                if (Day < voldays && Day + 20 > voldays)
                {
                    Console.WriteLine(vol.Firstname + " " + vol.Lastname + " : " + vol.birthdate.Year + "." + vol.birthdate.Month + "." + vol.birthdate.Day);
                }

            }
        }

        internal static void Showallvolunteers(List<Volunteer> allvolunteers2)//Aici se afiseaza fiecare voluntar care este stocat in array
        {
            foreach (Volunteer vol in allvolunteers2)
            {
                Console.WriteLine("Id: " + vol.id);
                Console.WriteLine("Firstname: " + vol.Firstname);
                Console.WriteLine("Lastname: " + vol.Lastname);
                Console.WriteLine("Age: " + vol.Age);
                Console.WriteLine("Address: " + vol.id);
                Console.WriteLine("Id: " + vol.id);
                Console.WriteLine("Id: " + vol.id);
                Console.WriteLine("Id: " + vol.id);
                Console.WriteLine("Id: " + vol.id);
                Console.WriteLine("Id: " + vol.id);
                Console.WriteLine("Id: " + vol.id);
                Console.WriteLine("Id: " + vol.id);
                Console.WriteLine("Id: " + vol.id);
                Console.WriteLine("Id: " + vol.id);
                // DE TERMINAT
            }
        }

        internal static void SavetoJson(List<Volunteer> allvolunteers2)//aici trece toate datele din arrayul allvollunteers in fisierul Json
        {
            string tojson = JsonConvert.SerializeObject(allvolunteers2);
            File.WriteAllText("volunteer.json", tojson);
        }

        internal static int Selector(List<Volunteer> allvolunteers2)//aici selectezi care persoana vei modifica mai incolo
        {
            int nr = 0;
            int idselected = 0;
            foreach (Volunteer vol in allvolunteers2)
            {
                nr++;
                Console.WriteLine("ID: " + nr + ", Name:" + vol.Firstname + " " + vol.Lastname);
            }
            Console.WriteLine("Please enter the number of which Person you want to edit/remove!");
            bool done = false;
            while (done == false)
            {
                idselected = Convert.ToInt16(Console.ReadLine());
                if (idselected > nr || idselected <= 0)
                {
                    Console.WriteLine("Not a valid id number!\n Please enter another id!");
                }
                else
                {
                    done = true;
                }
            }
            return idselected-1;
        }
    }
}
