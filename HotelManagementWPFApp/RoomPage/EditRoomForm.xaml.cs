using BLL;
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
    /// Interaction logic for EditRoomForm.xaml
    /// </summary>
    public partial class EditRoomForm : UserControl
    {
        RoomBLL roomBLL = new RoomBLL();
        public event EventHandler? DataAdded;

        public EditRoomForm()
        {
            InitializeComponent();
        }

        private void Room_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void setRoomData(RoomDTO room)
        {
            DataTable listType = roomBLL.GetListType();
            cboRoomType.ItemsSource = listType.DefaultView;
            cboRoomType.DisplayMemberPath = "Type";
            cboRoomType.SelectedValuePath = "CategoryId";
            cboRoomType.SelectedIndex = Convert.ToInt32(room.RoomType.ToString()) - 1;

            txtRoomId.Text = room.RoomId.ToString();
            txtRoomNo.Text = room.RoomNo.ToString();
            txtRoomPhone.Text = room.RoomPhone;
            if(room.RoomStatus == "Free")
            {
                rdoFree.IsChecked = true;
            } else if(room.RoomStatus == "Busy")
            {
                rdoBusy.IsChecked = true;
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtRoomNo.Text == "" || cboRoomType.SelectedValue == null || txtRoomPhone.Text == "")
            {
                MessageBox.Show("Required Field!", "Required Field", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    RoomDTO roomDTO = new RoomDTO
                    {
                        RoomId = Convert.ToInt32(txtRoomId.Text),
                        RoomNo = Convert.ToInt32(txtRoomNo.Text),
                        RoomType = Convert.ToInt32(cboRoomType.SelectedValue),
                        RoomPhone = txtRoomPhone.Text,
                        RoomStatus = rdoFree.IsChecked.HasValue && rdoFree.IsChecked.Value ? "Free" : "Busy",

                    };
                    RoomDTO result = roomBLL.UpdateRoom(roomDTO);
                    if (result != null)
                    {
                        DataAdded?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show("Room updated successfully!");
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
