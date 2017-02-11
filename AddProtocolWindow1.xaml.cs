using System;
using System.Data.SqlClient;
using System.Windows;

namespace Smart_Touch_Protocol_Utility
{
    public partial class AddProtocolWindow1 : Window
    {
        public AddProtocolWindow1()
        {
            InitializeComponent();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var uvCode = uvCodeTextBox.Text;
                var uvDescrip = uvDescripTextBox.Text;

                if (uvbRadioButton.IsChecked == true)
                {
                    AddProtocol.uvbTreatType(uvCode, uvDescrip);
                }
                else
                {
                    AddProtocol.uvaTreatType(uvCode, uvDescrip);
                }
                Close();
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
    }
}
