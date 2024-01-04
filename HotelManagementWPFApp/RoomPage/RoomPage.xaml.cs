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
    /// Interaction logic for RoomPage.xaml
    /// </summary>
    public partial class RoomPage : Page
    {
        RoomBLL roomBLL = new RoomBLL();
        EditRoomForm editRoomForm = new EditRoomForm();
        public RoomPage(RoomBLL roomBLL)
        {
            this.roomBLL = roomBLL;
            InitializeComponent();
            editRoomForm.DataAdded += EditRoomForm_DataAdded;

        }
        private void EditRoomForm_DataAdded(object? sender, EventArgs e)
        {
            LoadDataRoom();
        }
        private void Room_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable listType = roomBLL.GetListType();
            cboRoomType.ItemsSource = listType.DefaultView;
            cboRoomType.DisplayMemberPath = "Type";
            cboRoomType.SelectedValuePath = "CategoryId";
            cboRoomType.SelectedIndex = 0;
            
            LoadDataRoom();
        }        
            
        private void LoadDataRoom()
        {
            DataTable roomList = roomBLL.GetListRoom();
            dataRooms.ItemsSource = roomList.DefaultView;
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you want to remove?", "Remove room", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (MessageBoxResult.Yes == result)
                {
                    Button button = (Button)sender;
                    int roomId = Convert.ToInt32(button.Tag);
                    roomBLL.DeleteRoom(roomId);
                    MessageBox.Show("Remove user successfully");
                    LoadDataRoom();
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

                int roomId = Convert.ToInt32(button.Tag);

                RoomDTO room = roomBLL.GetRoomById(roomId);
                // Tạo một đối tượng Window tùy chỉnh để hiển thị form chỉnh sửa
                if (room != null)
                {
                    editRoomForm.setRoomData(room);
                    Window editClientWindow = new Window
                    {
                        Title = "Edit Room",
                        Width = 600,
                        Height = 600,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        Content = editRoomForm,// Sử dụng UserControl EditUserForm làm nội dung của Window
                    };
                    // Hiển thị form chỉnh sửa thông tin người dùng
                    editClientWindow.ShowDialog();
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

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtRoomNo.Text == "" || txtRoomPhone.Text == "")
            {
                MessageBox.Show("Required Field!", "Required Field", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    RoomDTO roomDTO = new RoomDTO();

                    roomDTO.RoomNo = Convert.ToInt32(txtRoomNo.Text);
                    roomDTO.RoomType = Convert.ToInt32(cboRoomType.SelectedValue);
                    roomDTO.RoomPhone = txtRoomPhone.Text;
                    roomDTO.RoomStatus = rdoFree.IsChecked.HasValue && rdoFree.IsChecked.Value ? "Free" : "Busy";
                    RoomDTO resultInsert = roomBLL.InsertRoom(roomDTO);
                    if (resultInsert != null)
                    {
                        MessageBox.Show("Room add successfully!", "Room add", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Room add failed!", "Room add", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadDataRoom();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while add the Room: " + ex.Message);
                }
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
                List<RoomDTO> searchResults = roomBLL.SearchRoomById(searchText);
                dataRooms.ItemsSource = searchResults;
            }
        }
    }
}
