using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ReservationDTO
    {
        public int ReserveId { get; set; }
        public string ClientId { get; set; }
        public int RoomNo { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateOut { get; set; }
    }
}
