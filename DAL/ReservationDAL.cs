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
    public class ReservationDAL
    {
        DBContext connect = new DBContext();
        public DataTable GetRoomByType(int roomType)
        {
            try
            {
                SqlCommand command = new SqlCommand("select RoomNo from rooms where RoomType = @type and RoomStatus = 'Free'", connect.GetConnection());
                connect.OpenConnect();
                command.Parameters.AddWithValue("@type", roomType);
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dataTable = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public int GetTypeRoom(int RoomId)
        {
            try
            {
                SqlCommand command = new SqlCommand("select roomtype from rooms where RoomId = @RoomId", connect.GetConnection());
                connect.OpenConnect();
                command.Parameters.AddWithValue("@RoomId", RoomId);
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dataTable = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);
                // Truy cập vào giá trị của cột đầu tiên, hàng đầu tiên
                return Convert.ToInt32(dataTable.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetTotalReservation()
        {
            int totalReservation = 0;
            try
            {
                connect.OpenConnect();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Reservation", connect.GetConnection());
                totalReservation = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return totalReservation;
        }

        public ReservationDTO GetReserveById(int reserveId)
        {
            try
            {
                connect.OpenConnect();
                using (SqlCommand command = new SqlCommand("SELECT * FROM reservation WHERE ReserveId = @ReserveId", connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@ReserveId", reserveId);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        ReservationDTO reserve = new ReservationDTO
                        {
                            ReserveId = Convert.ToInt32(reader["ReserveId"]),
                            ClientId = reader["ClientId"].ToString(),
                            RoomNo = Convert.ToInt32(reader["RoomNo"]),
                            DateIn = Convert.ToDateTime(reader["DateIn"]),
                            DateOut = Convert.ToDateTime(reader["DateOut"]),
                        };

                        return reserve;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error :", ex);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        //public int GetClient(string clientId)
        //{
        //    try
        //    {
        //        SqlCommand command = new SqlCommand("select ClientName from clients where ClientId = @ClientId", connect.GetConnection());
        //        connect.OpenConnect();
        //        command.Parameters.AddWithValue("@ClientId", clientId);
        //        SqlDataAdapter adapter = new SqlDataAdapter();
        //        DataTable dataTable = new DataTable();
        //        adapter.SelectCommand = command;
        //        adapter.Fill(dataTable);
        //        // Truy cập vào giá trị của cột đầu tiên, hàng đầu tiên
        //        return Convert.ToInt32(dataTable.Rows[0][0].ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public DataTable GetReserveList()
        {
            try
            {
                SqlCommand command = new SqlCommand("Select * from reservation", connect.GetConnection());
                connect.OpenConnect();
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dataTable = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }

        public bool SetReserveRoom(int roomNo, string roomStatus)
        {
            SqlCommand command = new SqlCommand("update rooms set RoomStatus = @roomStatus  where RoomNo = @roomNo", connect.GetConnection());
            connect.OpenConnect();
            command.Parameters.AddWithValue("@roomNo", roomNo);
            command.Parameters.AddWithValue("@roomStatus", roomStatus);
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseConnect();
                return true;
            }
            else
            {
                connect.CloseConnect();
                return false;
            }
        }

        public List<ReservationDTO> SearchReserveByRoom(int RoomNo)
        {
            try
            {
                connect.OpenConnect();
                using (SqlCommand command = new SqlCommand("SELECT * FROM reservation WHERE RoomNo LIKE @RoomNo", connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@RoomNo", "%" + RoomNo + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<ReservationDTO> reservationDTO = new List<ReservationDTO>();

                        while (reader.Read())
                        {
                            ReservationDTO reservation = new ReservationDTO();
                            reservation.ReserveId = reader.GetInt32("ReserveId");
                            reservation.ClientId = reader.GetString("ClientId");
                            reservation.RoomNo = reader.GetInt32("RoomNo");
                            reservation.DateIn = reader.GetDateTime("DateIn");
                            reservation.DateOut = reader.GetDateTime("DateOut");
                            reservationDTO.Add(reservation);
                        }
                        return reservationDTO;
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


        public ReservationDTO InsertReserve(ReservationDTO reservationDTO)
        {
            try
            {
                connect.OpenConnect();
                SqlCommand command = new SqlCommand("insert into reservation (ClientId, RoomNo, DateIn, DateOut) VALUES ( @ClientId, @RoomNo, @DateIn, @DateOut)", connect.GetConnection());
                command.Parameters.AddWithValue("@ClientId", reservationDTO.ClientId);
                command.Parameters.AddWithValue("@RoomNo", reservationDTO.RoomNo);
                command.Parameters.AddWithValue("@DateIn", reservationDTO.DateIn);
                command.Parameters.AddWithValue("@DateOut", reservationDTO.DateOut);
                int result = command.ExecuteNonQuery();

                if (result == 1)
                {
                    return reservationDTO;
                }
                else
                {
                    throw new Exception("Failed to insert reservation.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting reservation." + ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public ReservationDTO UpdateReservation(ReservationDTO reservationDTO)
        {
            try
            {

                connect.OpenConnect();
                SqlCommand command = new SqlCommand("UPDATE reservation SET ClientId = @ClientId, RoomNo = @RoomNo, DateIn = @DateIn, DateOut = @DateOut WHERE ReserveId = @ReserveId", connect.GetConnection());
                command.Parameters.AddWithValue("@ReserveId", reservationDTO.ReserveId);
                command.Parameters.AddWithValue("@ClientId", reservationDTO.ClientId);
                command.Parameters.AddWithValue("@RoomNo", reservationDTO.RoomNo);
                command.Parameters.AddWithValue("@DateIn", reservationDTO.DateIn);
                command.Parameters.AddWithValue("@DateOut", reservationDTO.DateOut);

                int result = command.ExecuteNonQuery();

                if (result == 1)
                {
                    return reservationDTO;
                }
                else
                {
                    throw new Exception("Failed to update Reservation.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while update Reservation." + ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public bool DeleteReservation(int ReserveId)
        {
            try
            {
                connect.OpenConnect();
                SqlCommand command = new SqlCommand("delete reservation where ReserveId = @ReserveId", connect.GetConnection());
                command.Parameters.AddWithValue("ReserveId", ReserveId);
                if (command.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

    }
}
