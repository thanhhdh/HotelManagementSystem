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
using System.Windows.Shapes;

namespace HotelManagementWPFApp
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        private UserDTO loggedInUser;
        UserBLL userBLL = new UserBLL();
        ClientBLL clientBLL = new ClientBLL();
        RoomBLL roomBLL = new RoomBLL();
        ReservationBLL reservationBLL = new ReservationBLL();
        public Dashboard(UserDTO user)
        {
            InitializeComponent();
            loggedInUser = user;
            DisplayUserInformation();
            DisplayCurrentDateTime();
        }
        private void DisplayUserInformation()
        {
            txtUserName.Text = loggedInUser.UserName;
            txtEmail.Text = loggedInUser.UserEmail;
        }
        private void DisplayCurrentDateTime()
        {
            // Get the current date and time
            DateTime currentDateTime = DateTime.Now;

            // Display the current date and time in the TextBlock
            txtDateTime.Text = currentDateTime.ToString();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private bool IsMaximized = false;
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                if(IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;
                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage();

            Button button = (Button)sender;
            string buttonText = ((TextBlock)((StackPanel)button.Content).Children[1]).Text;
            txtTitle.Text = buttonText;
        }
        private void Account_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccountPage(userBLL);

            Button button = (Button)sender;
            string buttonText = ((TextBlock)((StackPanel)button.Content).Children[1]).Text;
            txtTitle.Text = buttonText;
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientPage(clientBLL);

            Button button = (Button)sender;
            string buttonText = ((TextBlock)((StackPanel)button.Content).Children[1]).Text;
            txtTitle.Text = buttonText;
        }

        private void Room_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new RoomPage(roomBLL);

            Button button = (Button)sender;
            string buttonText = ((TextBlock)((StackPanel)button.Content).Children[1]).Text;
            txtTitle.Text = buttonText;
        }
        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ReservationPage(reservationBLL);

            Button button = (Button)sender;
            string buttonText = ((TextBlock)((StackPanel)button.Content).Children[1]).Text;
            txtTitle.Text = buttonText;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = ((TextBlock)((StackPanel)button.Content).Children[1]).Text;
            txtTitle.Text = buttonText;
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you want to log out?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {
                this.Close();
            }
        }
       
    }
}
