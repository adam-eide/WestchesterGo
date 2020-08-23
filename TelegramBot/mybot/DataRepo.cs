using System;
using Dapper;
using System.Data.SQLite;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace mybot
{

    class DataRepo
    {
        private readonly string connectionstring;

        public DataRepo(string cs)
        {
            this.connectionstring = cs;
        }

        public string[] GetCurrentEvents()
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();
            var q = @"SELECT * FROM CurrentEvent";
            Events events = con.QueryFirstOrDefault<Events>(q);
            return new string[]{ events.raids , events.eggs2, events.eggs5, events.eggs7, events.eggs10 };
        }

        public User GetUser(int id)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();
            var q = @"SELECT * FROM Users WHERE TelegramID = " + id.ToString();
            return con.QuerySingleOrDefault<User>(q);

        }

        public void AddUser(User user)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            var q = @"INSERT INTO Users (TelegramID, Name, GoName, TotalEggs, ShinyEggs, TotalRaids, CaughtRaids, ShinyRaids) VALUES (@TelegramID, @Name, @GoName, 0, 0, 0, 0, 0)";
            con.Execute(q, user);
        }

        public void SetGoName(User user)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            var q = @"UPDATE Users SET GoName = @GoName WHERE TelegramID = @TelegramID";
            con.Execute(q, user);
        }

        public User[] GetAllUsers()
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            var q = @"SELECT * FROM Users";
            return con.Query<User>(q).ToArray();

        }

        //version = 1 if current, becomes 0 when not avalible 
        public Egg[] GetEggs(string eventName, int size)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            var q = @"SELECT * FROM Eggs WHERE distance = " + size + " AND eventName = '" + eventName + "'";
            return con.Query<Egg>(q).ToArray();
        }

        public Raid[] GetRaids(string currentevent, int size)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            var q = @"SELECT * FROM Raids WHERE stars = " + size + " AND  eventName = '" + currentevent + "'";
            return con.Query<Raid>(q).ToArray();
        }

        public void AddRaid(string playerID, string raidID, string caught, string shiny)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            var q = @"UPDATE Users SET TotalRaids = TotalRaids + 1, CaughtRaids = CaughtRaids + " + 
                caught + ", ShinyRaids = ShinyRaids + "+ shiny + " WHERE TelegramID = " + playerID;
            con.Execute(q);
            var q2 = @"UPDATE Raids SET total = total + 1, caught = caught + " +
                caught + ", shiny = shiny + " + shiny + " WHERE raidID = " + raidID;
            con.Execute(q2);
        }

        public string GetEggName(int id)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            var q = @"SELECT * FROM Eggs WHERE id = " + id;
            return con.QueryFirstOrDefault<Egg>(q).name;
        }
        public string GetEggName(string id)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            var q = @"SELECT * FROM Eggs WHERE id = " + id;
            return con.QueryFirstOrDefault<Egg>(q).name;
        }

        //Delete this?
        public int GetEggSize(int id)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            var q = @"SELECT * FROM Eggs WHERE id = " + id;
            return con.QuerySingleOrDefault<Egg>(q).distance;
        }

        public void AddEgg(int eggID, int playerID, Boolean shiny)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();
            string shine = " ";
            if (shiny)
                shine = ", shiny = shiny + 1 ";
            var q = @"UPDATE Eggs SET hatched = hatched + 1" + shine + "WHERE id = " + eggID;
            con.Execute(q);
            shine = " ";
            if (shiny)
                shine = ", ShinyEggs = ShinyEggs + 1 ";
            var q2 = @"UPDATE Users SET TotalEggs = TotalEggs + 1" + shine + "WHERE TelegramID = " + playerID;
            con.Execute(q2);
        }

        public void AddTempEgg(string eggID, string playerID, string size, Boolean shiny)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            var q = @"INSERT INTO TempEggs (PlayerID, EggID, Distance, Name, Shiny) VALUES (" +
                playerID + ", " + eggID + ", " + size + ", '" + GetEggName(eggID) + "', " +
                ((shiny) ? "1" : "0") + ")";
            con.Execute(q);
        }

        public List<TempEgg> GetTempEggs(string playerID)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            var q = @"SELECT * FROM TempEggs WHERE PlayerID = " + playerID;
            return con.Query<TempEgg>(q).ToList();
        }

        public void ConfirmTempEggs(string playerID)
        {
            List<TempEgg> tempEggs = GetTempEggs(playerID);

            if (tempEggs == null)
                return;


            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            foreach (TempEgg e in tempEggs)
            {
                var q = @"UPDATE Eggs SET hatched = hatched + 1, shiny = shiny + " + e.Shiny + " WHERE id = " + e.EggID;
                con.Execute(q);
                var q2 = @"UPDATE Users SET TotalEggs = TotalEggs + 1, ShinyEggs = ShinyEggs + " + e.Shiny + " WHERE TelegramID = " + playerID;
                con.Execute(q2);
            }

            DeleteTempEggs(playerID);
        }

        public void DeleteTempEggs(string playerID)
        {
            using var con = new SQLiteConnection(connectionstring);
            con.Open();

            var q = @"DELETE FROM TempEggs WHERE playerID = " + playerID;
            con.Execute(q);
        }
    }
}
