using System;
using System.IO.Compression;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}

	private void CreateRooms()
    {
        // Create the rooms

        Room outside = new Room("outside the main entrance of the university");

        Room theatre = new Room("in a lecture theatre");

        Room theatreUp = new Room("upstage behind the curtains");

        Room pub = new Room("in the campus pub");
        Room lab = new Room("in a computing lab");
        Room office = new Room("in the computing admin office");
		Room hallway = new Room("in the hallway");



        player.CurrentRoom = outside;

        // Initialise room exits
		outside.AddExit("south", hallway);

        hallway.AddExit("east", theatre);
        hallway.AddExit("south", lab);
        hallway.AddExit("west", pub);

        theatre.AddExit("west", hallway);
        theatre.AddExit("up", theatreUp);

        theatreUp.AddExit("down", theatre);

        pub.AddExit("east", hallway);

        lab.AddExit("north", hallway);
        lab.AddExit("east", office);

        office.AddExit("west", lab);


        // Create your Items here
        Item sword = new Item (2, "metal stick on wooden stick");
		Item pillow = new Item (1, "cotton sleeve with feathers");
		Item wand = new Item (1, "magic wonky wooden stick");
		Item beer = new Item (1, "alcoholistic drink");
		Item key = new Item (1, "shiny key to open a door...");


        // And add them to the Rooms
    	theatre.AddItem("wand", wand);
		pub.AddItem("beer", beer);
		office.AddItem("key", key);
		hallway.AddItem("sword", sword);
		lab.AddItem("pillow", pillow);



    }

    //  Main play routine. Loops until end of play.
    public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				Console.WriteLine(player.CurrentRoom.GetLongDescription());
				player.CurrentRoom.showItems();
				break;
			case "status":
				Console.WriteLine("player health is: "+player.health);
				break;
			case "put":
				Console.WriteLine("you put this item in your inventory:");
				break;
			case "get":
				Console.WriteLine("You grabbed the item");
				break;
			case "die":
				Console.WriteLine(player.health = 0);
				Console.WriteLine("you died.");
				wantToQuit = true;
				break;

		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}
		else
		{
			if (player.bleeding)
			{
				player.Damage(5);
			}
		}

		

		player.CurrentRoom = nextRoom;
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}
}
