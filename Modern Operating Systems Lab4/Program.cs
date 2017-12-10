using System;
using System.Linq;
using Modern_Operating_Systems_Lab4.Helpers;
using Modern_Operating_Systems_Lab4.Models;

namespace Modern_Operating_Systems_Lab4
{
	internal static class Program
	{
		private static void Main()
		{
			var helper = new DatabaseHelper();

			// Get free Rooms
			var rooms = helper.GetFreeRoomsForSelectedDate(DateTime.Now).ToList();
			Console.WriteLine($"Free rooms for {DateTime.Now}:");
			foreach (var room in rooms)
			{
				Console.WriteLine(room);
			}

			if (rooms.Count == 0)
			{
				Console.WriteLine("No free Rooms, sorry");
				return;
			}

			// Reserve Room
			var donaldTrump = new Client { Id = Guid.Parse("80B7E813-80DC-4427-90F8-2184F3A9F410") };
			var hisFavoriteRoom = new Room { Id = Guid.Parse("D51927E6-E95E-474B-83F5-0D678909719F") };
			helper.ReserveRoom(hisFavoriteRoom, donaldTrump, new DateTime(2017, 11, 20), new DateTime(2017, 12, 1));
			Console.WriteLine("\nRoom reserved successfully");

			helper.RemoveRoomReservation(hisFavoriteRoom, donaldTrump, new DateTime(2017, 11, 20), new DateTime(2017, 12, 1));
			Console.WriteLine("\nRoom reservation removed");
		}
	}
}
