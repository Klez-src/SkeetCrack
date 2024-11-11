﻿using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace skeetloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool bSelected;

        public MainWindow()
        {
            bSelected = true;
            InitializeComponent();

            main_grid.Visibility = Visibility.Hidden;
            login_grid.Visibility = Visibility.Visible;


        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Exit_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Load_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (!bSelected)
                return;

            Run r = new Run("Waiting for CS2");
            r.Foreground = new SolidColorBrush(Color.FromRgb(80, 118, 32));


            StatusBox.Document.Blocks.Add(new Paragraph(r));
        }

        private void CS_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            bSelected = !bSelected;

            CS_Name_Label.Foreground = bSelected ? new SolidColorBrush(Color.FromRgb(80, 118, 32)) : Brushes.White;

            CS_BG.Fill = bSelected ? new SolidColorBrush(Color.FromRgb(26, 26, 26)) : new SolidColorBrush(Color.FromRgb(35, 35, 35));

            Launch_Button_BG.Fill = bSelected ? new SolidColorBrush(Color.FromRgb(29, 29, 29)) : new SolidColorBrush(Color.FromRgb(35, 35, 35));
            Launch_Button_BG.IsEnabled = bSelected;
        }

        private void Login_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            Authorization auth = new Authorization(username_box.Text, password_box.Password);
            if(!auth.Login())
            {
                MessageBox.Show("Wrong credentials");
                return;
            }


            StatusBox.Document.Blocks.Add(new Paragraph(new Run("Welcome back, " + auth.GetUsername())));
            StatusBox.Document.Blocks.Add(new Paragraph(new Run("Added Counter-Strike 2 (" + auth.GetSubscription()  + " days remaining)")));

            main_grid.Visibility = Visibility.Visible;
            login_grid.Visibility = Visibility.Hidden;
        }

        private void username_box_preview_text_input(object sender, TextCompositionEventArgs e)
        {
            if(e.Text.Contains(" "))
            {
                e.Handled = true;
            }
            MessageBox.Show(e.Text);
        }

        private void username_box_preview_key_down(object sender, KeyEventArgs e) // Someday should be renamed to text_box_cancel_input used for both username_box and password_box
        {
            // Check if the key pressed is a space or tab
            if (e.Key == Key.Space || e.Key == Key.Tab)
            {
                e.Handled = true; // Prevent the space or tab from being added to the TextBox
            }
        }

        
    }
}