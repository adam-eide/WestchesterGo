using SQLitePCL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace mybot
{
    class Keyboards
    {
        public InlineKeyboardButton[][] optionbuttons;
        public InlineKeyboardButton[][] eggconfirmmenu;
        public InlineKeyboardButton[][] eggmenu;
        public InlineKeyboardButton[][] eggbuttons2;
        public InlineKeyboardButton[][] eggbuttons5;
        public InlineKeyboardButton[][] eggbuttons7;
        public InlineKeyboardButton[][] eggbuttons10;
        public InlineKeyboardButton[][] raidconfirmmenu;
        public InlineKeyboardButton[][] raidmenu;
        public InlineKeyboardButton[][] raidbuttons1;
        public InlineKeyboardButton[][] raidbuttons2;
        public InlineKeyboardButton[][] raidbuttons3;
        public InlineKeyboardButton[][] raidbuttons4;
        public InlineKeyboardButton[][] raidbuttons5;
        
        
        string[] CURRENT_EVENT;

        public Keyboards(DataRepo data, string[] currentEvent)
        {
            CURRENT_EVENT = currentEvent;
            optionbuttons = new InlineKeyboardButton[][] { new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Stats", "stats") }, new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Enter Eggs", "eggs menu 0") }, new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Enter Raids", "raid menu 0") } };
            
            eggmenu = new InlineKeyboardButton[][] { 
                new InlineKeyboardButton[] { 
                    InlineKeyboardButton.WithCallbackData("2km", "eggs menu 2"),
                    InlineKeyboardButton.WithCallbackData("5km", "eggs menu 5"),
                    InlineKeyboardButton.WithCallbackData("7km", "eggs menu 7"),
                    InlineKeyboardButton.WithCallbackData("10km", "eggs menu 10")
                }, 
            };

            eggconfirmmenu = new InlineKeyboardButton[][] {
                new InlineKeyboardButton[] {
                    InlineKeyboardButton.WithCallbackData("Cancel", "eggs confirm cancel"),
                    InlineKeyboardButton.WithCallbackData("Done", "eggs confirm yes"),
                },
            };

            raidconfirmmenu = new InlineKeyboardButton[][] {
                new InlineKeyboardButton[] {
                    InlineKeyboardButton.WithCallbackData("No", "raid confirm 0"),
                    InlineKeyboardButton.WithCallbackData("Yes!", "raid confirm 1"),
                },
            };

            raidmenu = new InlineKeyboardButton[][] {
                new InlineKeyboardButton[] {
                    InlineKeyboardButton.WithCallbackData("1*", "raid menu 1"),
                    InlineKeyboardButton.WithCallbackData("2*", "raid menu 2"),
                    InlineKeyboardButton.WithCallbackData("3*", "raid menu 3"),
                    InlineKeyboardButton.WithCallbackData("4*", "raid menu 4"),
                    InlineKeyboardButton.WithCallbackData("5*", "raid menu 5")
                },
            };

            SetButtons(data);
            
        }

        private void SetButtons(DataRepo data)
        {
            Egg[] eggs2 = data.GetEggs(CURRENT_EVENT[1], 2);
            eggbuttons2 = SetEggKeyboard(eggs2);
            Egg[] eggs5 = data.GetEggs(CURRENT_EVENT[2], 5);
            eggbuttons5 = SetEggKeyboard(eggs5);
            Egg[] eggs7 = data.GetEggs(CURRENT_EVENT[3], 7);
            eggbuttons7 = SetEggKeyboard(eggs7);
            Egg[] eggs10 = data.GetEggs(CURRENT_EVENT[4], 10);
            eggbuttons10 = SetEggKeyboard(eggs10);
            Raid[] raids1 = data.GetRaids(CURRENT_EVENT[0], 1);
            raidbuttons1 = SetRaidKeyboard(raids1);
            Raid[] raids2 = data.GetRaids(CURRENT_EVENT[0], 2);
            raidbuttons2 = SetRaidKeyboard(raids2);
            Raid[] raids3 = data.GetRaids(CURRENT_EVENT[0], 3);
            raidbuttons3 = SetRaidKeyboard(raids3);
            Raid[] raids4 = data.GetRaids(CURRENT_EVENT[0], 4);
            raidbuttons4 = SetRaidKeyboard(raids4);
            Raid[] raids5 = data.GetRaids(CURRENT_EVENT[0], 5);
            raidbuttons5 = SetRaidKeyboard(raids5);

        }

        private InlineKeyboardButton[][] SetEggKeyboard(Egg[] eggs)
        {

            List<List<InlineKeyboardButton>> eggList = new List<List<InlineKeyboardButton>>();
            int row = 0;
            int col = 1;
            eggList.Add(new List<InlineKeyboardButton>());
            foreach(Egg e in eggs)
            {
                eggList[row].Add(InlineKeyboardButton.WithCallbackData(e.name, "eggs add " + e.distance + " " + e.id));
                if (col++ % 3 == 0)
                {
                    row++;
                    eggList.Add(new List<InlineKeyboardButton>());
                }
            }
            if (eggList[row].Count > 0)
            {
                eggList.Add(new List<InlineKeyboardButton>());
                row++;
            }
            eggList[row].Add(InlineKeyboardButton.WithCallbackData("Add a shiny", "eggs shiny " + ((eggs.Length > 0) ? eggs.ElementAt(0).distance.ToString() : "7")));
            eggList[row].Add(InlineKeyboardButton.WithCallbackData("Done", "eggs confirm done"));

            InlineKeyboardButton[][] array = new InlineKeyboardButton[eggList.Count][];
            row = 0;
            foreach(List<InlineKeyboardButton> l in eggList)
            {
                array[row++] = l.ToArray();
            }
            

            return array;
            
        }

        private InlineKeyboardButton[][] SetRaidKeyboard(Raid[] raids)
        {

            List<List<InlineKeyboardButton>> raidList = new List<List<InlineKeyboardButton>>();
            int row = 0;
            
            foreach (Raid r in raids)
            {
                raidList.Add(new List<InlineKeyboardButton>());
                raidList[row].Add(InlineKeyboardButton.WithCallbackData(r.pokemon, "raid add " + r.raidID + " " + r.pokemon));
                if (r.shiny >= 0)
                    raidList[row].Add(InlineKeyboardButton.WithCallbackData("Shiny " + r.pokemon, "raid add " + r.raidID + " " + r.pokemon + " shiny"));
                row++;
                
            }
            
            raidList.Add(new List<InlineKeyboardButton>());

            raidList[row].Add(InlineKeyboardButton.WithCallbackData("Cancel", "raid cancel"));
            

            InlineKeyboardButton[][] array = new InlineKeyboardButton[raidList.Count][];
            row = 0;
            foreach (List<InlineKeyboardButton> l in raidList)
            {
                array[row++] = l.ToArray();
            }


            return array;

        }



    }
}
