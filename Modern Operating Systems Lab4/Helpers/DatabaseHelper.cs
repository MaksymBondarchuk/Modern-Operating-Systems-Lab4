using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modern_Operating_Systems_Lab4.Models;

namespace Modern_Operating_Systems_Lab4.Helpers
{
	public class DatabaseHelper
	{
		private SqlConnection _connection;

		public DatabaseHelper()
		{
			_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["corporate-hotel-server"].ConnectionString);
			_connection.Open();
		}

		// Отримати список вільних номерів на вибрану дату
		public List<Room> GetFreeRoomsForSelectedDate(DateTime date)
		{
			var request = @"SELECT * 
							FROM   Rooms r 
							WHERE  NOT EXISTS (SELECT 1 
											   FROM   ClientAccommodations ca 
											   WHERE  ca.RoomId = r.Id 
													  AND ca.[Begin] <= @date 
													  AND ca.[End] <= @date)";

			using (var command = new SqlCommand(request, _connection))
			{
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var id = reader.GetInt32(0);

						Debugger.Break();
						//var number = reader.GetInt32("number");
						//var name = reader.GetString("name");

					}

				}
			}
			return null;
		}
	}
}
