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
			var donaldTrump = new Client { Id = Guid.Parse("14F6BEA9-AC19-4366-8944-BDE9FDEDE35F") };
			var hisFavoriteRoom = new Room { Id = Guid.Parse("48701E49-43C2-4FE8-8654-158EFCDF82C9") };
			helper.ReserveRoom(hisFavoriteRoom, donaldTrump, new DateTime(2017, 11, 20), new DateTime(2017, 12, 1));
			Console.WriteLine("\nRoom reserved successfully");

			helper.RemoveRoomReservation(hisFavoriteRoom, donaldTrump, new DateTime(2017, 11, 20), new DateTime(2017, 12, 1));
			Console.WriteLine("\nRoom reservation removed");
		}
	}
}
