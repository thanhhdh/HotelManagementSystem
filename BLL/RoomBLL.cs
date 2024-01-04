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
    public class RoomBLL
    {
        private RoomDAL roomDAL;
        public RoomBLL()
        {
            roomDAL = new RoomDAL();
        }

        public DataTable GetListRoom()
        {
            try
            {
                var listRoom = roomDAL.GetListRoom();
                return listRoom;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi:" + ex.Message, ex);
            }
        }

        public int GetTotalRooms()
        {
            try
            {
                return roomDAL.GetTotalRooms();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetFreeRooms()
        {
            try
            {
                return roomDAL.GetFreeRooms();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RoomDTO> SearchRoomById(int roomNo)
        {
            try
            {
                return roomDAL.SearchRoomById(roomNo);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable GetListType()
        {
            try
            {
                var listType = roomDAL.GetListType();
                return listType;
            } catch (Exception ex)
            {
                throw new Exception("Có lỗi:"+ex.Message, ex);
            }
        }

        public RoomDTO GetRoomById(int roomId)
        {
            try
            {
                return roomDAL.GetRoomById(roomId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public RoomDTO InsertRoom(RoomDTO roomDTO)
        {
            try
            {
                RoomDTO room = new RoomDTO
                {
                    RoomNo = roomDTO.RoomNo,
                    RoomType = roomDTO.RoomType,
                    RoomPhone = roomDTO.RoomPhone,
                    RoomStatus = roomDTO.RoomStatus,
                };
                return roomDAL.InsertRoom(room);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public RoomDTO UpdateRoom(RoomDTO roomDTO)
        {
            try
            {
                RoomDTO room = new RoomDTO
                {
                    RoomId = roomDTO.RoomId,
                    RoomNo = roomDTO.RoomNo,
                    RoomType = roomDTO.RoomType,
                    RoomPhone = roomDTO.RoomPhone,
                    RoomStatus = roomDTO.RoomStatus,
                };
                return roomDAL.UpdateRoom(room);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteRoom(int roomId)
        {
            try
            {
                roomDAL.DeleteRoom(roomId);
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi" + ex.Message);
            }
        }
    }
}
