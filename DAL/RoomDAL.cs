using DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RoomDAL
    {
        DBContext connect = new DBContext();

        public DataTable GetListType()
        {
            SqlCommand command = new SqlCommand("Select * from category", connect.GetConnection());
            connect.OpenConnect();
            try
            {
                SqlDataReader rd = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(rd);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetListRoom()
        {
            SqlCommand command = new SqlCommand("Select * from rooms", connect.GetConnection());
            connect.OpenConnect();
            try
            {
                SqlDataReader rd = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(rd);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetTotalRooms()
        {
            int totalRooms = 0;
            try
            {
                connect.OpenConnect();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Rooms", connect.GetConnection());
                totalRooms = (int)command.ExecuteScalar();
            } catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return totalRooms;
        }

        public int GetFreeRooms()
        {
            int totalRooms = 0;
            try
            {
                connect.OpenConnect();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Rooms where RoomStatus = 'Free'", connect.GetConnection());
                totalRooms = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return totalRooms;
        }

        public RoomDTO GetRoomById(int roomId)
        {
            try
            {
                connect.OpenConnect();
                using (SqlCommand command = new SqlCommand("SELECT * FROM rooms WHERE RoomId = @RoomId", connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@RoomId", roomId);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        RoomDTO room = new RoomDTO
                        {
                            RoomId = Convert.ToInt32(reader["RoomId"]),
                            RoomNo = Convert.ToInt32(reader["RoomNo"]),
                            RoomType = Convert.ToInt32(reader["RoomType"]),
                            RoomPhone = reader["RoomPhone"].ToString(),
                            RoomStatus = reader["RoomStatus"].ToString(),
                        };

                        return room;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error :" + ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public List<RoomDTO> SearchRoomById(int RoomNo)
        {
            try
            {
                connect.OpenConnect();
                using (SqlCommand command = new SqlCommand("SELECT * FROM rooms WHERE RoomNo LIKE @RoomNo", connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@RoomNo", "%" + RoomNo + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<RoomDTO> rooms = new List<RoomDTO>();

                        while (reader.Read())
                        {
                            RoomDTO room = new RoomDTO();
                            room.RoomId = reader.GetInt32("RoomId");
                            room.RoomNo = reader.GetInt32("RoomNo");
                            room.RoomType = reader.GetInt32("RoomType");
                            room.RoomStatus = reader.GetString("RoomStatus");
                            room.RoomPhone = reader.GetString("RoomPhone");
                            rooms.Add(room);
                        }

                        return rooms;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred" + ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public RoomDTO InsertRoom(RoomDTO roomDTO)
        {
            try
            {
                connect.OpenConnect();
                SqlCommand command = new SqlCommand("insert into rooms (RoomNo, RoomType, RoomPhone, RoomStatus) values (@RoomNo, @RoomType, @RoomPhone, @RoomStatus)", connect.GetConnection());
                command.Parameters.AddWithValue("@RoomNo", roomDTO.RoomNo);
                command.Parameters.AddWithValue("@RoomType", roomDTO.RoomType);
                command.Parameters.AddWithValue("@RoomPhone", roomDTO.RoomPhone);
                command.Parameters.AddWithValue("@RoomStatus", roomDTO.RoomStatus);
                int result = command.ExecuteNonQuery();

                if (result == 1)
                {
                    return roomDTO;
                }
                else
                {
                    throw new Exception("Failed to insert room.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting client." + ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public RoomDTO UpdateRoom(RoomDTO roomDTO)
        {
            try
            {

                connect.OpenConnect();
                SqlCommand command = new SqlCommand("UPDATE rooms SET RoomNo = @RoomNo, RoomType = @RoomType, RoomPhone = @RoomPhone, RoomStatus = @RoomStatus WHERE RoomId = @RoomId", connect.GetConnection());
                command.Parameters.AddWithValue("@RoomId", roomDTO.RoomId);
                command.Parameters.AddWithValue("@RoomNo", roomDTO.RoomNo);
                command.Parameters.AddWithValue("@RoomType", roomDTO.RoomType);
                command.Parameters.AddWithValue("@RoomPhone", roomDTO.RoomPhone);
                command.Parameters.AddWithValue("@RoomStatus", roomDTO.RoomStatus);

                int result = command.ExecuteNonQuery();

                if (result == 1)
                {
                    return roomDTO;
                }
                else
                {
                    throw new Exception("Failed to update Room.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while update Room." + ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public void DeleteRoom(int roomId)
        {
            try
            {
                connect.OpenConnect();
                SqlCommand command = new SqlCommand("delete rooms where RoomId = @RoomId", connect.GetConnection());
                command.Parameters.AddWithValue("RoomId", roomId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
