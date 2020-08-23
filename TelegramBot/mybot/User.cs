using System;
using System.Collections.Generic;
using System.Text;

namespace mybot
{
    class User
    {
        public int UserNum { get; set; }
        public int TelegramID { get; set; }
        public string Name { get; set; }
        public string GoName { get; set; }
        public int TotalEggs { get; set; }
        public int ShinyEggs { get; set; }
        public int TotalRaids { get; set; }
        public int CaughtRaids { get; set; }
        public int ShinyRaids { get; set; }

        public User(int id, string n)
        {
            TelegramID = id;
            Name = n;
            GoName = "";
            TotalEggs = 0;
            ShinyEggs = 0;
            TotalRaids = 0;
            CaughtRaids = 0;
            ShinyRaids = 0;
        }

        public User()
        {
            TelegramID = 0;
            Name = "";
            GoName = "";
            TotalEggs = 0;
            ShinyEggs = 0;
            TotalRaids = 0;
            CaughtRaids = 0;
            ShinyRaids = 0;
        }

        public void Combine(User u){
            this.TotalEggs += u.TotalEggs;
            this.ShinyEggs += u.ShinyEggs;
            this.TotalRaids += u.TotalRaids;
            this.CaughtRaids += u.CaughtRaids;
            this.ShinyRaids += u.ShinyRaids;
        }


    }
}
