using System;
using Modern_Operating_Systems_Lab4.Helpers;

namespace Modern_Operating_Systems_Lab4
{
	internal static class Program
	{
		private static void Main()
		{
			var helper = new DatabaseHelper();
			helper.GetFreeRoomsForSelectedDate(DateTime.Now);
		}
	}
}
