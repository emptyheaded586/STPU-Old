using System;
using System.Data.SqlClient;
using System.Windows;

namespace Smart_Touch_Protocol_Utility
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Import.importTables();
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
            AddProtocolWindow1 win1 = new AddProtocolWindow1();
            win1.ShowDialog();
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            // Not currently in use.
        }
    }
}