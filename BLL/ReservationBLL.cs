using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReservationBLL
    {
        private ReservationDAL _reservationDAL;
        public ReservationBLL()
        {
            _reservationDAL = new ReservationDAL();
        }

        public DataTable GetRoomByType(int roomType)
        {
            try
            {
                return _reservationDAL.GetRoomByType(roomType);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetReserveList()
        {
            try
            {
                var list = _reservationDAL.GetReserveList();
                return list;
            }
            catch 
            {
                throw;
            }
        }

        public int GetTotalReservation()
        {
            try
            {
                return _reservationDAL.GetTotalReservation();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetTypeRoom(int RoomId)
        {
            try
            {
                return _reservationDAL.GetTypeRoom(RoomId);
            }
            catch
            {
                throw;
            }
        }

        public ReservationDTO GetReserveById(int reservaId)
        {
            try
            {
                return _reservationDAL.GetReserveById(reservaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool SetReserveRoom(int roomNo, string roomStatus)
        {
            try
            {
                var list = _reservationDAL.SetReserveRoom(roomNo, roomStatus);
                return list;
            }
            catch
            {
                throw;
            }
        }

        public List<ReservationDTO> SearchReserveByRoom(int roomNo)
        {
            try
            {
                return _reservationDAL.SearchReserveByRoom(roomNo);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public ReservationDTO InsertReserve(ReservationDTO reservationDTO)
        {
            try
            {
                ReservationDTO reservation = new ReservationDTO
                {
                    ClientId = reservationDTO.ClientId,
                    RoomNo = reservationDTO.RoomNo,
                    DateIn = reservationDTO.DateIn,
                    DateOut = reservationDTO.DateOut,
                };
                return _reservationDAL.InsertReserve(reservation);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteReservation(int reservaId)
        {
            try
            {
                return _reservationDAL.DeleteReservation(reservaId);
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi" + ex.Message);
            }
        }

        public ReservationDTO UpdateReservation(ReservationDTO reservationDTO)
        {
            try
            {
                ReservationDTO reservation = new ReservationDTO
                {
                    ReserveId = reservationDTO.ReserveId,
                    ClientId = reservationDTO.ClientId,
                    RoomNo = reservationDTO.RoomNo,
                    DateIn = reservationDTO.DateIn,
                    DateOut = reservationDTO.DateOut,
                };
                return _reservationDAL.UpdateReservation(reservation);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
