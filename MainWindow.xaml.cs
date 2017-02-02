using System;
using System.Data.SqlClient;
using System.Windows;

namespace Smart_Touch_Protocol_Utility
{
    /*
    *  Interaction logic for MainWindow.xaml
    */
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void importButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Export.exportTables();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddProtocolButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}