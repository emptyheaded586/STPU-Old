using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace Smart_Touch_Protocol_Utility
{
    public partial class GlobalProtocolTreatments : Window
    {
        private int gpID;

        public GlobalProtocolTreatments()
        {
            InitializeComponent();
        }

        public GlobalProtocolTreatments(int gpID)
        {
            InitializeComponent();
            this.gpID = gpID;
        }

        private void numTreatTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void medTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void incTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void gpTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("GlobalProtocolID");
            dt.Columns.Add("TreatmentNumber");
            dt.Columns.Add("PrimaryDosage");
            dt.Columns.Add("SecondaryDosage");
            
        }

        private void finishButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
