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
    /// Interaction logic for ReservationPage.xaml
    /// </summary>
    public partial class ReservationPage : Page
    {
        ReservationBLL reservationBLL = new ReservationBLL();
        RoomBLL roomBLL = new RoomBLL();
        ClientBLL clientBLL = new ClientBLL();
        EditReservationForm editReservationForm = new EditReservationForm();
        public ReservationPage(ReservationBLL reservationBLL)
        {
            InitializeComponent();
            editReservationForm.DataAdded += EditRoomForm_DataAdded;

        }

        private void EditRoomForm_DataAdded(object? sender, EventArgs e)
        {
            LoadDataReservation();

        }

        private void Reservation_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable listType = roomBLL.GetListType();
            cboRoomType.ItemsSource = listType.DefaultView;
            cboRoomType.DisplayMemberPath = "Type";
            cboRoomType.SelectedValuePath = "CategoryId";
            cboRoomType.SelectedIndex = 0;

            DataTable listClient = clientBLL.GetClientList();
            cboClientId.ItemsSource = listClient.DefaultView;
            cboClientId.DisplayMemberPath = "ClientId";
            cboClientId.SelectedValuePath = "ClientId";
            cboClientId.SelectedIndex = 0;

            int roomType = Convert.ToInt32(cboRoomType.SelectedValue);
            DataTable dataTable = reservationBLL.GetRoomByType(roomType);
            cboRoomNo.ItemsSource = dataTable.DefaultView;
            cboRoomNo.DisplayMemberPath = "RoomNo";
            cboRoomNo.SelectedValuePath = "RoomNo";
            LoadDataReservation();
        }

        private void LoadDataReservation()
        {
            DataTable reservationList = reservationBLL.GetReserveList();
            dataReservation.ItemsSource = reservationList.DefaultView;
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

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if(cboRoomNo.SelectedValue == "" || dateTime_DateIn.SelectedDate == null || dateTime_DateOut.SelectedDate == null)
            {
                MessageBox.Show("Required Field!", "Required Field", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    ReservationDTO reservation = new ReservationDTO();

                    reservation.ClientId = cboClientId.Text.ToString();
                    reservation.RoomNo = Convert.ToInt32(cboRoomNo.SelectedValue);
                    reservation.DateIn = dateTime_DateIn.SelectedDate;
                    reservation.DateOut = dateTime_DateOut.SelectedDate;
                    if (reservation.DateIn < DateTime.Today)
                    {
                        MessageBox.Show("Reservation Date In must be today or after", "Invalid Date", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (reservation.DateOut < reservation.DateIn)
                    {
                        MessageBox.Show("Reservation Date Out must be Date In or after", "Invalid Date", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        ReservationDTO result = reservationBLL.InsertReserve(reservation);
                        if (result != null)
                        {
                            reservationBLL.SetReserveRoom(reservation.RoomNo, "Busy");
                            MessageBox.Show("Reservation add successfully!", "Add Reservation", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadDataReservation();
                        }
                        else
                        {
                            MessageBox.Show("Reservation add failed!", "Error Reservation", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while add the Room: " + ex.Message);
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you want to remove?", "Remove Reservation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (MessageBoxResult.Yes == result)
                {
                    Button button = (Button)sender;
                    int reserveId = Convert.ToInt32(button.Tag);

                    if (reservationBLL.DeleteReservation(reserveId))
                    {
                        MessageBox.Show("Remove Reservation successfully");
                        LoadDataReservation();
                    }
                    else
                    {
                        MessageBox.Show("Remove Reservation failer");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;

                int reservationId = Convert.ToInt32(button.Tag);

                ReservationDTO reservation = reservationBLL.GetReserveById(reservationId);
                // Tạo một đối tượng Window tùy chỉnh để hiển thị form chỉnh sửa
                if (reservation != null)
                {
                    editReservationForm.SetReservationData(reservation);
                    Window ediReservationWindow = new Window
                    {
                        Title = "Edit Reservation",
                        Width = 500,
                        Height = 400,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        Content = editReservationForm,// Sử dụng UserControl EditUserForm làm nội dung của Window
                    };
                    // Hiển thị form chỉnh sửa thông tin người dùng
                    ediReservationWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("User not found.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SearchTextBox.Text) && SearchTextBox.Text.Length > 0)
            {
                textSearch.Visibility = Visibility.Collapsed;
            }
            else
            {
                textSearch.Visibility = Visibility.Visible;
            }
            TextBox textBox = (TextBox)sender;
            int searchText;
            if (int.TryParse(textBox.Text, out searchText))
            {
                List<ReservationDTO> searchResults = reservationBLL.SearchReserveByRoom(searchText);
                dataReservation.ItemsSource = searchResults;
            }
        }

    }
}
