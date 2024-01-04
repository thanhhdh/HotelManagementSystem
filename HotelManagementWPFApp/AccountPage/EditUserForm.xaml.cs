using BLL;
using DTO;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditUserForm.xaml
    /// </summary>
    public partial class EditUserForm : UserControl
    {
        UserBLL userBLL = new UserBLL();
        public event EventHandler? DataAdded;
        public EditUserForm()
        {
            InitializeComponent();
        }
       
        public void setUserData(UserDTO user)
        {
            txtUserIdEdit.Text = user.UserId.ToString();
            txtUsernameEdit.Text = user.UserName;
            txtEmailEdit.Text = user.UserEmail;
            txtPhoneEdit.Text = user.UserPhone;
        }
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserDTO userDTO = new UserDTO
                {
                    UserId = Convert.ToInt32(txtUserIdEdit.Text),
                    UserName = txtUsernameEdit.Text,
                    UserEmail = txtEmailEdit.Text,
                    UserPhone = txtPhoneEdit.Text,
                };
                UserDTO result = userBLL.UpdateUser(userDTO);
                if (result != null)
                {
                    DataAdded?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show("User updated successfully!");
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
