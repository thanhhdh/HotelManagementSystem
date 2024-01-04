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
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        ClientBLL clientBLL = new ClientBLL();

        EditClientForm editClientForm = new EditClientForm();
        public ClientPage(ClientBLL clientBLL)
        {
            InitializeComponent();
            editClientForm.DataAdded += EditClientForm_DataAdded;
            LoadDataClient();
        }

        private void EditClientForm_DataAdded(object? sender, EventArgs e)
        {
            LoadDataClient();
        }
        private void LoadDataClient()
        {
            DataTable clientList = clientBLL.GetClientList();
            dataClients.ItemsSource = clientList.DefaultView;
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
           if(textBoxId.Text == "" || textBoxName.Text == "" || textBoxEmail.Text == "" || textBoxPhone.Text == "" || textBoxAddress.Text == "")
           {
                MessageBox.Show("Required Field!", "Required Field", MessageBoxButton.OK, MessageBoxImage.Information);
           }
            else
            {
                try
                {
                    ClientDTO clientDTO = new ClientDTO();
                    clientDTO.ClientId = textBoxId.Text;
                    clientDTO.ClientName = textBoxName.Text;
                    clientDTO.ClientEmail = textBoxEmail.Text;
                    clientDTO.ClientPhone = textBoxPhone.Text;
                    clientDTO.ClientAddress = textBoxAddress.Text;
                    ClientDTO result = clientBLL.InsertClient(clientDTO);
                    if (result != null)
                    {
                        MessageBox.Show("Client added successfully!");
                        LoadDataClient();
                        textBoxName.Clear();
                        textBoxId.Clear();
                        textBoxEmail.Clear();
                        textBoxPhone.Clear();
                        textBoxAddress.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add Client.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you want to remove?", "Remove user", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (MessageBoxResult.Yes == result)
                {
                    Button button = (Button)sender;
                    string clientId = button.Tag.ToString();
                    clientBLL.DeleteClient(clientId);
                    MessageBox.Show("Remove user successfully");
                    LoadDataClient();
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

                string clientId = button.Tag.ToString();

                ClientDTO client = clientBLL.GetClientById(clientId);
                // Tạo một đối tượng Window tùy chỉnh để hiển thị form chỉnh sửa
                if (client != null)
                {
                    editClientForm.setClientData(client);
                    Window editClientWindow = new Window
                    {
                        Title = "Edit User",
                        Width = 500,
                        Height = 400,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        Content = editClientForm,// Sử dụng UserControl EditUserForm làm nội dung của Window
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

        private void TextAddress_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxAddress.Focus();
        }

        private void textBoxAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxAddress.Text) && textBoxAddress.Text.Length > 0)
            {
                TextAddress.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextAddress.Visibility = Visibility.Visible;
            }
        }

        private void TextPhone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxPhone.Focus();
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

            List<ClientDTO> searchResults = clientBLL.SearchClientsByClientName(searchText);
            dataClients.ItemsSource = searchResults;
        }

        private void TextEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxEmail.Focus();
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

        private void TextName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxName.Focus();
        }
        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxName.Text) && textBoxName.Text.Length > 0)
            {
                TextName.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextName.Visibility = Visibility.Visible;
            }
        }
        private void TextId_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxId.Focus();
        }

        private void textBoxId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxId.Text) && textBoxId.Text.Length > 0)
            {
                TextId.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextId.Visibility = Visibility.Visible;
            }
        }

        

    }

}
