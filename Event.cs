using System;
using System.Collections.Generic;
using System.Text;


namespace SiemensProject
{

        public class Event
        {
            public int Id_Event { get; set; }
            private string tipeOfEvent;
            public string PlaceOfEvent { get; set; }
            public int DateOfEvent { get; set; }
            public int Lasting { get; set; }
            public int NumberOfVolunteersNeeded { get; set; }
            public string TipeOfActyvities { get; set; }
            public string TipeOfEvent { get => tipeOfEvent; set => tipeOfEvent = value; }
    }

    //public string Event()
      //  {
       
    //}
    
}
