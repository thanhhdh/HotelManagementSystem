using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagementWPFApp
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private RoomBLL roomBLL;
        private ClientBLL clientBLL;
        private ReservationBLL reservationBLL;
        public HomePage()
        {
            InitializeComponent();
            roomBLL = new RoomBLL();
            clientBLL = new ClientBLL();
            reservationBLL = new ReservationBLL();
            GetTotalRoom();
            GetTotalClient();
            GetFreeRoom();
            GetTotalReservation();
        }

        public void GetTotalRoom()
        {
            int totalRooms = roomBLL.GetTotalRooms();
            TotalRooms.Number = totalRooms.ToString();        
        }

        public void GetFreeRoom()
        {
            int freeRooms = roomBLL.GetFreeRooms();
            FreeRoom.Number = freeRooms.ToString();
        }

        public void GetTotalClient()
        {
            int totalClients = clientBLL.GetTotalClients();
            TotalClients.Number = totalClients.ToString();
        }

        public void GetTotalReservation()
        {
            int totalReservation = reservationBLL.GetTotalReservation();
            TotalReserve.Number = totalReservation.ToString();
        }

    }
}
