using BLL;
using DAL;
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
using static System.Net.Mime.MediaTypeNames;

namespace HotelManagementWPFApp
{
    /// <summary>
    /// Interaction logic for EditClientForm.xaml
    /// </summary>
    public partial class EditClientForm : UserControl
    {
        ClientBLL clientBLL = new ClientBLL();
        public event EventHandler? DataAdded;
        public EditClientForm()
        {
            InitializeComponent();
        }
        public void setClientData(ClientDTO client)
        {
            txtClientIdEdit.Text = client.ClientId.ToString();
            txtClientNameEdit.Text = client.ClientName;
            txtEmailEdit.Text = client.ClientEmail;
            txtPhoneEdit.Text = client.ClientPhone;
            txtAddressEdit.Text = client.ClientAddress;
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtClientNameEdit.Text == "" || txtEmailEdit.Text == "" || txtPhoneEdit.Text == "" || txtAddressEdit.Text == "")
            {
                MessageBox.Show("Required Field!", "Required Field", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    ClientDTO clientDTO = new ClientDTO
                    {
                        ClientId = txtClientIdEdit.Text,
                        ClientName = txtClientNameEdit.Text,
                        ClientEmail = txtEmailEdit.Text,
                        ClientPhone = txtPhoneEdit.Text,
                        ClientAddress = txtAddressEdit.Text,

                    };
                    ClientDTO result = clientBLL.UpdateClient(clientDTO);
                    if (result != null)
                    {
                        DataAdded?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show("Client updated successfully!");
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
