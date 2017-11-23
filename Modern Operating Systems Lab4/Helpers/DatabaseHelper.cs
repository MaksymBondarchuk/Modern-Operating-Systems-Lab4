using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Modern_Operating_Systems_Lab4.Models;

namespace Modern_Operating_Systems_Lab4.Helpers
{
	public class DatabaseHelper
	{
		private readonly SqlConnection _connection;

		public DatabaseHelper()
		{
			_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["corporate-hotel-server"].ConnectionString);
			_connection.Open();
		}

		/// <summary>
		/// Отримати список вільних номерів на вибрану дату
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public IEnumerable<Room> GetFreeRoomsForSelectedDate(DateTime date)
		{
			var rooms = new List<Room>();

			const string request = @"SELECT * 
							        FROM   Rooms r 
							        WHERE  NOT EXISTS (SELECT 1 
											           FROM   ClientAccommodations ca 
											           WHERE  ca.RoomId = r.Id 
													          AND ca.[From] <= @date 
													          AND @date <= ca.[To])";

			using (var command = new SqlCommand(request, _connection))
			{
				command.Parameters.Add("@date", SqlDbType.DateTime);
				command.Parameters["@date"].Value = date;

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var id = reader.GetGuid(reader.GetOrdinal("Id"));
						var roomClass = reader.GetInt32(reader.GetOrdinal("Class"));
						var beds = reader.GetInt32(reader.GetOrdinal("Beds"));
						var number = reader.GetString(reader.GetOrdinal("Number"));
						var cost = reader.GetDecimal(reader.GetOrdinal("Cost"));
						var notes = reader.GetValue(reader.GetOrdinal("Notes")) == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("Notes"));
						var floorId = reader.GetGuid(reader.GetOrdinal("FloorId"));

						var room = new Room
						{
							Id = id,
							Class = roomClass,
							Beds = beds,
							Number = number,
							Cost = cost,
							Notes = notes,
							FloorId = floorId,
							Floor = new Floor { Id = floorId }
						};
						rooms.Add(room);
					}
				}
			}
			return rooms;
		}

		/// <summary>
		/// Зарезервувати номер для вибраного клієнта на вибрані дати
		/// </summary>
		public void ReserveRoom(Room room, Client client, DateTime from, DateTime to)
		{
			const string request = @"INSERT INTO ClientAccommodations (IsBooking, [From], [To], ClientId, RoomId)
									VALUES (1, @from, @to, @clientId, @roomId)";
			using (var command = new SqlCommand(request, _connection) { CommandType = CommandType.Text })
			{
				command.Parameters.Add("@from", SqlDbType.DateTime);
				command.Parameters.Add("@to", SqlDbType.DateTime);
				command.Parameters.Add("@clientId", SqlDbType.UniqueIdentifier);
				command.Parameters.Add("@roomId", SqlDbType.UniqueIdentifier);
				command.Parameters["@from"].Value = from;
				command.Parameters["@to"].Value = to;
				command.Parameters["@clientId"].Value = client.Id;
				command.Parameters["@roomId"].Value = room.Id;

				command.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Видалити резервування номерів для вибраного клієнта на вибрані дати
		/// </summary>
		public void RemoveRoomReservation(Room room, Client client, DateTime from, DateTime to)
		{
			const string request = @"DELETE
									FROM ClientAccommodations
									WHERE IsBooking = 1
										AND [From] <= @from
										AND @to <= [To]
										AND ClientId = @clientId
										AND RoomId = @roomId";
			using (var command = new SqlCommand(request, _connection) { CommandType = CommandType.Text })
			{
				command.Parameters.Add("@from", SqlDbType.DateTime);
				command.Parameters.Add("@to", SqlDbType.DateTime);
				command.Parameters.Add("@clientId", SqlDbType.UniqueIdentifier);
				command.Parameters.Add("@roomId", SqlDbType.UniqueIdentifier);
				command.Parameters["@from"].Value = from;
				command.Parameters["@to"].Value = to;
				command.Parameters["@clientId"].Value = client.Id;
				command.Parameters["@roomId"].Value = room.Id;

				command.ExecuteNonQuery();
			}
		}
	}
}
