using System;
using System.Collections.Generic;
using System.Text;

namespace mybot
{
    class Messages
    {
        public static string INTRO = "Hello!\nThis bot keeps track of raid and egg statistics.\nUse \"/go username\" to set your Pokemon Go name.\nChoose a button to see statistics, enter your eggs, or enter your raids.\n\nCheck out the data at westchestergo.com";
        public static string OPTIONS = "What do you want to do?";
        public static string HELP = INTRO + "\nIf you have questions, email them to help@westchestergo.com";
        public static string USAGE = "I only respond to:\n/go username\n/start\n/help\nand button presses\n\n" + INTRO;
    }
}
