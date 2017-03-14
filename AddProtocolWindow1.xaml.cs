using System;
using System.Data.SqlClient;
using System.Windows;
using Smart_Touch_Protocol_Utility.AddProtocols;

namespace Smart_Touch_Protocol_Utility
{
    public partial class AddProtocolWindow1 : Window
    {
        public AddProtocolWindow1()
        {
            InitializeComponent();
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(uvCodeTextBox.Text) && !string.IsNullOrWhiteSpace(numTreatTextBox.Text)
                && !string.IsNullOrWhiteSpace(uvDescripTextBox.Text))
            {
                var uvCode = uvCodeTextBox.Text;
                var uvDescrip = uvDescripTextBox.Text;
                int numTreat = int.Parse(numTreatTextBox.Text);
                int gpID = GlobalProtocols.gpID();

                try
                {
                    if (uvbRadioButton.IsChecked == true)
                    {
                        GlobalProtocolTreatments.gptUVB(gpID, numTreat);
                        TreatmentType.uvbTreatType(uvCode, uvDescrip);
                        GlobalProtocols.uvbGlobalProtocols(uvCode);
                        TreatmentLimits.treatLim(uvCode);
                        ResourceEdit.resourceUVBEdit(uvCode, uvDescrip);
                    }
                    else
                    {
                        GlobalProtocolTreatments.gptUVA(gpID, numTreat);
                        TreatmentType.uvaTreatType(uvCode, uvDescrip);
                        GlobalProtocols.uvaGlobalProtocols(uvCode);
                        ResourceEdit.resourceUVAEdit(uvCode, uvDescrip);
                    }
                    MessageBox.Show("Protocol has been added.", "Protocol complete");
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
                MessageBox.Show("Please complete all fields.", "Missing Information",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void numTreatTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}