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
            if (!string.IsNullOrWhiteSpace(uvCodeTextBox.Text))
            {
                try
                {
                    var uvCode = uvCodeTextBox.Text;
                    var uvDescrip = uvDescripTextBox.Text;
                    int numTreat = int.Parse(numTreatTextBox.Text);
                    int gpID = AddProtocol.gpID();

                    if (uvbRadioButton.IsChecked == true)
                    {
                        AddProtocol.uvbTreatType(uvCode, uvDescrip);
                        AddProtocol.uvbGlobalProtocols(uvCode);
                        AddProtocol.gptTable(gpID, numTreat);
                    }
                    else
                    {
                        AddProtocol.uvaTreatType(uvCode, uvDescrip);
                        AddProtocol.uvaGlobalProtocols(uvCode);
                        AddProtocol.gptTable(gpID, numTreat);
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
            else
            {
                MessageBox.Show("Please enter a UV code to continue.");
            }
        }

        private void numTreatTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}