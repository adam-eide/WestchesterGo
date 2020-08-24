This is my project for tracking local Pokemon Go data. 
See data at westchestergo.com
Enter data at https://t.me/WestchesterGoBot (Telegram bot)
The PokeForm page is not publicly accessible


data schemas:
CREATE TABLE [Users] ( 
	[UserNum] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[TelegramID] INTEGER NOT NULL,
	[Name] TEXT,
	[GoName] TEXT,
	[TotalEggs] INTEGER NOT NULL,
	[ShinyEggs] INTEGER NOT NULL,
	[TotalRaids] INTEGER NOT NULL,
	[CaughtRaids] INTEGER NOT NULL,
	[ShinyRaids] INTEGER NOT NULL 
); 
CREATE TABLE [Raids] ( 
	[raidID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[stars] INTEGER NOT NULL,
	[pokemon] TEXT,
	[eventName] TEXT,
	[total] INTEGER NOT NULL,
	[caught] INTEGER NOT NULL,
	[shiny] INTEGER NOT NULL
); 
CREATE TABLE [Eggs] ( 
	[id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[distance] INTEGER NOT NULL,
	[name] TEXT,
	[eventName] TEXT,
	[hatched] INTEGER NOT NULL,
	[shiny] INTEGER NOT NULL
); 

CREATE TABLE [TempEggs] ( 
	[ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[PlayerID] INTEGER NOT NULL,
	[EggID] INTEGER NOT NULL,
	[Distance] INTEGER NOT NULL,
	[Name] TEXT,
	[Shiny] INTEGER NOT NULL
); 
CREATE TABLE [CurrentEvent] ( 
	[ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[raids] TEXT,
	[eggs2] TEXT,
	[eggs5] TEXT,
	[eggs7] TEXT,
	[eggs10] TEXT
); 
