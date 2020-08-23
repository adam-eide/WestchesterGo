using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Dapper;
using System.Data.SQLite;
using System.Linq;
using Telegram.Bot.Requests;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mybot
{
    
    class Program
    {
        static ITelegramBotClient botClient;
        static DataRepo data;
        static Keyboards keyboards;

        static void Main()
        {
            botClient = new TelegramBotClient("");

            
            data = new DataRepo("");
            keyboards = new Keyboards(data, data.GetCurrentEvents());

            Console.WriteLine("Starting...");
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );
            botClient.OnMessage += Bot_OnMessage;
            botClient.OnCallbackQuery += BotClient_OnCallbackQuery;
            botClient.StartReceiving();

            Console.WriteLine("Press any key to exit");
            //Console.ReadKey();
            while (true)
            {
                
            }
            botClient.StopReceiving();
        }

        private static async void BotClient_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            Console.WriteLine("\nMessage id: " + e.CallbackQuery.Message.MessageId + "code: " + e.CallbackQuery.Data);
            string[] code = e.CallbackQuery.Data.Split(" ");
            switch (code[0])
            {
                case "reset":
                    await SendOptionsAsync(e.CallbackQuery.Message.Chat.Id, Messages.OPTIONS);
                    break;
                case "stats":
                    await SendStats(e.CallbackQuery.Message, e.CallbackQuery.From.Id);
                    break;
                case "eggs": 
                    await ProcessEggCode(e.CallbackQuery.Message.Chat.Id, code, (e.CallbackQuery.Message.Text == "Which one was shiny?"));
                    break;
                case "raid":
                    await ProcessRaidCode(e.CallbackQuery.Message.Chat.Id, code);
                    break;
                default:

                    break;
            }
            
            try
            {
                await botClient.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId);
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException ex)
            {
                Console.WriteLine("tried to delete a message twice\n" + ex.ToString());
            }
            

        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            
            if (e.Message.Type != MessageType.Text)
                return;

            User u = data.GetUser(e.Message.From.Id);
            if (u == null)
            {
                u = new User(e.Message.From.Id, e.Message.From.FirstName);
                data.AddUser(u);
            }

            string action = e.Message.Text.Split(' ').First();

            Console.WriteLine("action = " + action);
            switch (action.ToUpper())
            {
                case "/GO":
                    await SetGoNameAsync(e, u);
                    await DeleteInputMessage(e.Message.Chat.Id, e.Message.MessageId, "Deleting /go failed");
                    break;
                case "/START":
                    await DeleteInputMessage(e.Message.Chat.Id, e.Message.MessageId, "Deleting /start failed");
                    int inARow = 0;
                    for (int i = e.Message.MessageId-1; i > 0; i--)
                    {
                        try
                        {
                            await botClient.DeleteMessageAsync(e.Message.Chat.Id, i);
                            inARow = 0;
                        }
                        catch (Telegram.Bot.Exceptions.ApiRequestException ex)
                        {
                            Console.WriteLine("bad delete");
                            ++inARow;
                        }
                        if (inARow >= 5)
                            i = 0;
                    }
                    await SendOptionsAsync(e.Message.Chat.Id, Messages.INTRO);
                    Console.WriteLine("Started... showing options");
                    break;
                case "/HELP":
                    await DeleteInputMessage(e.Message.Chat.Id, e.Message.MessageId, "Deleting /help failed");
                    await SendOptionsAsync(e.Message.Chat.Id, Messages.HELP);
                    Console.WriteLine("Started... showing options");
                    break;
                default:
                    await DeleteInputMessage(e.Message.Chat.Id, e.Message.MessageId, "Deleting text input failed");
                    await SendOptionsAsync(e.Message.Chat.Id, Messages.USAGE);
                    break;
            }
           
            
        }

        private static async System.Threading.Tasks.Task DeleteInputMessage(long c, int m, string errorMsg)
        {
            try
            {
                await botClient.DeleteMessageAsync(c, m);
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException ex)
            {
                Console.WriteLine(errorMsg);
            }
        }

        private static async System.Threading.Tasks.Task ProcessRaidCode(long chat, string[] code)
        {
            switch (code[1])
            {
                case "menu":
                    await ShowRaidMenu(chat, code[2]);
                    break;
                case "add":
                    await ConfirmRaid(chat, code);
                    break;
                case "cancel":
                    await SendOptionsAsync(chat, Messages.OPTIONS);
                    break;
                case "confirm":
                    await SaveRaid(chat, code);
                    break;
                default:
                    Console.WriteLine("\nERROR ProcessRaidCode hit default switch?\n");
                    break;
            }
        }

        private static async System.Threading.Tasks.Task ShowRaidMenu(long c, string level)
        {
            switch (level)
            {
                case "0":
                    await botClient.SendTextMessageAsync(
                        chatId: c,
                        text: "What type of raid did you do?",
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(keyboards.raidmenu)
                        );
                    break;
                case "1":
                    await botClient.SendTextMessageAsync(
                        chatId: c,
                        text: "Which Pokemon was it?",
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(keyboards.raidbuttons1)
                        );
                    break;
                case "2":
                    await botClient.SendTextMessageAsync(
                        chatId: c,
                        text: "Which Pokemon was it?",
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(keyboards.raidbuttons2)
                        );
                    break;
                case "3":
                    await botClient.SendTextMessageAsync(
                        chatId: c,
                        text: "Which Pokemon was it?",
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(keyboards.raidbuttons3)
                        );
                    break;
                case "4":
                    await botClient.SendTextMessageAsync(
                        chatId: c,
                        text: "Which Pokemon was it?",
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(keyboards.raidbuttons4)
                        );
                    break;
                case "5":
                    await botClient.SendTextMessageAsync(
                        chatId: c,
                        text: "Which Pokemon was it?",
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(keyboards.raidbuttons5)
                        );
                    break;
            }
        }

        private static async System.Threading.Tasks.Task ConfirmRaid(long c, string[] code)
        {
            Boolean shiny = code.Length > 4;
            string message = "Did you catch the " + (shiny ? "shiny " : "") + code[3] + "?";
            await botClient.SendTextMessageAsync(
                        chatId: c,
                        text: message,
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton[][] {
                            new InlineKeyboardButton[] {
                                InlineKeyboardButton.WithCallbackData("No", "raid confirm 0 " + code[2] +  (shiny ? " 1" : " 0")),
                                InlineKeyboardButton.WithCallbackData("Yes!", "raid confirm 1 "+ code[2] +  (shiny ? " 1" : " 0")),
                                InlineKeyboardButton.WithCallbackData("Cancel", "raid cancel")
                            },
                        }));

        }

        private static async System.Threading.Tasks.Task SaveRaid(long c, string[] code)
        {
            data.AddRaid(c.ToString(), code[3], code[2], code[4]);
            SendOptionsAsync(c, Messages.OPTIONS);
        }

        private static async System.Threading.Tasks.Task SendOptionsAsync(long c, string message)
        { 
            
            await botClient.SendTextMessageAsync(
                    chatId: c, // or a chat id: 123456789
                    text: message,
                    disableNotification: true,
                    replyMarkup: new InlineKeyboardMarkup(keyboards.optionbuttons)
                    );
        }

        private static async System.Threading.Tasks.Task ProcessEggCode(long chat, string[] code, Boolean shiny)
        {
            switch (code[1])
            {
                case "menu":
                    await ShowEggMenu(chat, code[2], "What did you hatch?");
                    break;
                case "add":
                    await SelectEgg(chat, code, shiny);
                    break;
                case "shiny":
                    await ShowEggMenu(chat, code[2], "Which one was shiny?");
                    break;
                case "confirm":
                    await ConfirmEggs(chat, code);
                    break;
                default:
                    Console.WriteLine("\nERROR ProcessEggCode hit default switch?\n");
                    break;
            }
        }


        private static async System.Threading.Tasks.Task ShowEggMenu(long c, string size, string message)
        {
            switch (size)
            {
                case "0":
                    await botClient.SendTextMessageAsync(
                        chatId: c, 
                        text: message,
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(keyboards.eggmenu)
                        );
                    break;
                case "2":
                    await botClient.SendTextMessageAsync(
                        chatId: c,
                        text: message,
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(keyboards.eggbuttons2)
                        );
                    break;
                case "5":
                    await botClient.SendTextMessageAsync(
                        chatId: c,
                        text: message,
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(keyboards.eggbuttons5)
                        );
                    break;
                case "7":
                    await botClient.SendTextMessageAsync(
                        chatId: c,
                        text: message,
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(keyboards.eggbuttons7)
                        );
                    break;
                case "10":
                    await botClient.SendTextMessageAsync(
                        chatId: c,
                        text: message,
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(keyboards.eggbuttons10)
                        );
                    break;
            }
        }

        private static async System.Threading.Tasks.Task SelectEgg(long c, string[] code, Boolean shiny)
        {
            //add egg to tempEggs
            data.AddTempEgg(code[3], c.ToString(), code[2], shiny);
            //make message
            string msg = "The" +(shiny ? " shiny " : " ") +data.GetEggName(code[3]) + " has been added!";
            //call showeggmenu
            await ShowEggMenu(c, code[2], msg);
        }

        private static async System.Threading.Tasks.Task ConfirmEggs(long c, string[] code)
        {
            switch (code[2])
            {
                case "done":
                    await ShowEggConfirm(c);
                    return;
                case "cancel":
                    data.DeleteTempEggs(c.ToString());
                    break;
                case "yes":
                    data.ConfirmTempEggs(c.ToString());
                    break;
            }
            await SendOptionsAsync(c, Messages.OPTIONS);

        }

        private static async System.Threading.Tasks.Task ShowEggConfirm(long c)
        {
            string msg = "";
            List<TempEgg> eggs = data.GetTempEggs(c.ToString());
            if (eggs == null)
            {
                Console.WriteLine("No eggs were selected");
                msg = "No eggs were selected!";
            }
            else
            {
                msg = "You are about to add:\n";
                foreach(TempEgg e in eggs)
                {
                    msg += (e.Shiny == 1 ? "Shiny " : "") + e.Name + "\n";
                }
            }

            await botClient.SendTextMessageAsync(
                        chatId: c,
                        text: msg,
                        disableNotification: true,
                        replyMarkup: new InlineKeyboardMarkup(keyboards.eggconfirmmenu)
                        );
        }



        private static async System.Threading.Tasks.Task SetGoNameAsync(MessageEventArgs e, User u)
        {
            if (e.Message.Text.Length < 6)
            {
                // usage
            }
            else
            {
                u.GoName = e.Message.Text.Substring(4);
                data.SetGoName(u);
                await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat, // or a chat id: 123456789
                text: "You have set your Pokemon Go username to " + u.GoName,
                disableNotification: true,
                replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("OK", "reset"))
                );
            }
        }

        

        private static async System.Threading.Tasks.Task SendStats(Message m, int id)
        {
            Console.WriteLine("sending stats");
            User u = data.GetUser(id);
            if (u == null)
            {
                Console.WriteLine("User did not exist");
                return;
            }
            
            User[] users = data.GetAllUsers();
            if (users == null)
            {
                Console.WriteLine("users list returned null");
                return;
            }
            User totals = new User();
            foreach(User user in users)
            {
                totals.Combine(user);
            }
            string output = "Out of " + users.Length + " registered players...\nAll Eggs:\n" + totals.TotalEggs + " hatched\n" + totals.ShinyEggs + " were shiny\n" +
                "All Raids:\n" + totals.TotalRaids + " raids\n" + totals.CaughtRaids + " caught\n" + totals.ShinyRaids + " were shiny\n\n" +
                ((u.GoName == "") ? u.Name : u.GoName) + "'s stats:\n" +
                "Your Eggs:\n" + u.TotalEggs + " hatched\n" + u.ShinyEggs + " were shiny\n" +
                "Your Raids:\n" + u.TotalRaids + " raids\n" + u.CaughtRaids + " caught\n" + u.ShinyRaids + " were shiny\n";
            await botClient.SendTextMessageAsync(
                chatId: m.Chat, // or a chat id: 123456789
                text: output,
                disableNotification: true,
                replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("OK", "reset"))
                );

        }
    }
}