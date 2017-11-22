using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            var rooms = new List<Room>();

            const string request = @"SELECT * 
							        FROM   Rooms r 
							        WHERE  NOT EXISTS (SELECT 1 
											           FROM   ClientAccommodations ca 
											           WHERE  ca.RoomId = r.Id 
													          AND ca.[Begin] <= @date 
													          AND ca.[End] <= @date)";

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
    }
}
