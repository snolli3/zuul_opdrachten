using System.Collections.Generic;

class Room
{
	// Private fields
	private string description;
	private Dictionary<string, Room> exits; // stores exits of this room.
	private Dictionary<string, Item> items = new Dictionary<string, Item>();

	// Create a room described "description". Initially, it has no exits.
	// "description" is something like "in a kitchen" or "in a court yard".
	public Room(string desc)
	{
		description = desc;
		exits = new Dictionary<string, Room>();
	}

	// Define an exit for this room.
	public void AddExit(string direction, Room neighbor)
	{
		exits.Add(direction, neighbor);
	}

	// Return the description of the room.
	public string GetShortDescription()
	{
		return description;
	}

	// Return a long description of this room, in the form:
	//     You are in the kitchen.
	//     Exits: north, west
	public string GetLongDescription()
	{
		string str = "You are ";
		str += description;
		str += ".\n";
		str += GetExitString();
		return str;
	}

	// Return the room that is reached if we go from this room in direction
	// "direction". If there is no room in that direction, return null.
	public Room GetExit(string direction)
	{
		if (exits.ContainsKey(direction))
		{
			return exits[direction];
		}
		return null;
	}

	// Return a string describing the room's exits, for example
	// "Exits: north, west".
	public void showItems()
	{
		if (items.Count == 0)
		{
			Console.WriteLine("there are no items here");
			return;
		}
		
		Console.WriteLine("you found a:");
		foreach (var kvp in items)
		{
			Console.WriteLine($"{kvp.Key} - {kvp.Value.Description}");
			Console.WriteLine("take it?");
		}
	}

	private string GetExitString()
	{
		string str = "Exits: ";
		str += String.Join(", ", exits.Keys);

		return str;
	}

	public void AddItem(string name, Item item)
    {
		items[name] = item;
    }



}
