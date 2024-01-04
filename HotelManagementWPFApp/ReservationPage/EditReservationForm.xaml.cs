using BLL;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for EditReservationForm.xaml
    /// </summary>
    public partial class EditReservationForm : UserControl
    {
        ReservationBLL reservationBLL = new ReservationBLL();
        RoomBLL roomBLL = new RoomBLL();
        ClientBLL clientBLL = new ClientBLL();

        public event EventHandler? DataAdded;
        public EditReservationForm()
        {
            InitializeComponent();
        }

        public void SetReservationData(ReservationDTO reservation)
        {
            DataTable listClient = clientBLL.GetClientList();
            cboClientId.ItemsSource = listClient.DefaultView;
            cboClientId.DisplayMemberPath = "ClientId";
            cboClientId.SelectedValuePath = "ClientId";
            cboClientId.SelectedValue = reservation.ClientId;

            DataTable listType = roomBLL.GetListType();
            cboRoomType.ItemsSource = listType.DefaultView;
            cboRoomType.DisplayMemberPath = "Type";
            cboRoomType.SelectedValuePath = "CategoryId";
            cboRoomType.SelectedIndex = 0;


            int roomType = Convert.ToInt32(cboRoomType.SelectedValue);
            DataTable dataTable = reservationBLL.GetRoomByType(roomType);
            cboRoomNo.ItemsSource = dataTable.DefaultView;
            cboRoomNo.DisplayMemberPath = "RoomNo";
            cboRoomNo.SelectedValuePath = "RoomNo";
            cboRoomNo.SelectedIndex = Convert.ToInt32(reservation.RoomNo);

            dateTime_DateIn.SelectedDate = reservation.DateIn;
            dateTime_DateOut.SelectedDate = reservation.DateOut;
            txtReserveId.Text = reservation.ReserveId.ToString();
        }
        private void cboRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int roomType = Convert.ToInt32(cboRoomType.SelectedValue);
                DataTable dataTable = reservationBLL.GetRoomByType(roomType);
                cboRoomNo.ItemsSource = dataTable.DefaultView;
                cboRoomNo.DisplayMemberPath = "RoomNo";
                cboRoomNo.SelectedValuePath = "RoomNo";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (cboClientId.Text == "" || cboRoomNo.SelectedValue == null || dateTime_DateIn.SelectedDate == null || dateTime_DateOut.SelectedDate == null)
            {
                MessageBox.Show("Required Field!", "Required Field", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                try
                {
                    ReservationDTO reservationDTO = new ReservationDTO
                    {
                        ReserveId = Convert.ToInt32(txtReserveId.Text),
                        ClientId = cboClientId.Text.ToString(),
                        RoomNo = Convert.ToInt32(cboRoomNo.SelectedValue),
                        DateIn = dateTime_DateIn.SelectedDate,
                        DateOut = dateTime_DateOut.SelectedDate,

                    };
                    ReservationDTO result = reservationBLL.UpdateReservation(reservationDTO);
                    if (result != null)
                    {
                        DataAdded?.Invoke(this, EventArgs.Empty);
                        reservationBLL.SetReserveRoom(reservationDTO.RoomNo, "Busy");
                        MessageBox.Show("Reservation updated successfully!");
                        Window parentWindow = Window.GetWindow(this);
                        if (parentWindow != null)
                        {
                            parentWindow.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }

}
