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
using System.Xml.Linq;

namespace HotelManagementWPFApp
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        UserBLL userBLL = new UserBLL();
        EditUserForm editUserForm = new EditUserForm();
        public AccountPage(UserBLL userBLL)
        {
            InitializeComponent();
            editUserForm.DataAdded += EditUserForm_DataAdded;
            GetListUser();
        }

        private void EditUserForm_DataAdded(object? sender, EventArgs e)
        {
            GetListUser();
        }

        private void textBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxEmail.Text) && textBoxEmail.Text.Length > 0)
            {
                TextEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextEmail.Visibility = Visibility.Visible;
            }
        }

        private void textBoxUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxUsername.Text) && textBoxUsername.Text.Length > 0)
            {
                TextUserName.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextUserName.Visibility = Visibility.Visible;
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void textBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPhone.Text) && textBoxPhone.Text.Length > 0)
            {
                TextPhone.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextPhone.Visibility = Visibility.Visible;
            }
        }


        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxEmail.Focus();
        }
        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }
        private void textPhone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxPhone.Focus();
        }
        private void textUsername_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxUsername.Focus();
        }

        private void GetListUser()
        {
            DataTable userList = userBLL.GetUserList();
            lvUsers.ItemsSource = userList.DefaultView;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxUsername.Text == "" || textBoxEmail.Text == "" || passwordBox.Password == "" || textBoxPhone.Text == "")
            {
                MessageBox.Show("Required Field!", "Required Field", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    UserDTO userDTO = new UserDTO();
                    userDTO.UserName = textBoxUsername.Text;
                    userDTO.UserEmail = textBoxEmail.Text;
                    userDTO.UserPassword = passwordBox.Password;
                    userDTO.UserPhone = textBoxPhone.Text;
                    UserDTO result = userBLL.InsertUser(userDTO);
                    if (result != null)
                    {
                        MessageBox.Show("User added successfully!");
                        GetListUser();
                        textBoxUsername.Clear();
                        textBoxEmail.Clear();
                        passwordBox.Clear();
                        textBoxPhone.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add user.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
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
            string searchText = textBox.Text;

            List<UserDTO> searchResults = userBLL.SearchUsersByUsername(searchText);
            lvUsers.ItemsSource = searchResults;

        }

        //private void ListViewUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (lvUsers.SelectedItem != null)
        //    {
        //        DataRowView selectedRow = (DataRowView)lvUsers.SelectedItem;
        //        UserDTO selectedUser = new UserDTO
        //        {
        //            UserId = Convert.ToInt32(selectedRow.Row[0].ToString()),
        //            UserName = selectedRow[1].ToString(),
        //            UserEmail = selectedRow[2].ToString(),
        //            UserPhone = selectedRow[3].ToString(),
        //        };
        //        textBoxUserIdEdit.Text = selectedUser.UserId.ToString();
        //        textBoxUsernameEdit.Text = selectedUser.UserName;
        //        textBoxEmailEdit.Text = selectedUser.UserEmail;
        //        textBoxPhoneEdit.Text = selectedUser.UserPhone;

        //    }
        //}
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you want to remove?", "Remove user", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (MessageBoxResult.Yes == result)
                {
                    Button button = (Button)sender;
                    int userId = (int)button.Tag;
                    userBLL.DeleteUser(userId);
                    MessageBox.Show("Remove user successfully");
                    GetListUser();
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

                int userId = (int)button.Tag;

                UserDTO user = userBLL.GetUserById(userId);
                // Tạo một đối tượng Window tùy chỉnh để hiển thị form chỉnh sửa
                if(user != null)
                {
                    editUserForm.setUserData(user);
                    Window editUserWindow = new Window
                    {
                        Title = "Edit User",
                        Width = 500,
                        Height = 400,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        Content = editUserForm ,// Sử dụng UserControl EditUserForm làm nội dung của Window
                };
                    // Hiển thị form chỉnh sửa thông tin người dùng
                    editUserWindow.ShowDialog();
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
    }

}
