using System;
using System.Data;
using System.Windows;

namespace Smart_Touch_Protocol_Utility
{
    public partial class DosageAmounts : Window
    {
        private int numTreat;
        private static DataTable dt;

        public DosageAmounts()
        {
            InitializeComponent();
        }
        
        public DosageAmounts(int numTreat)
        {
            InitializeComponent();
            this.numTreat = numTreat;
            this.dosageAmts.ItemsSource = this.BindTable().DefaultView;
        }

        private DataTable BindTable()
        {
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("numTreat", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("doseAmt", Type.GetType("System.Double")));

            for (int x = 0; x < numTreat; ++x)
            {
                DataRow row = null;
                row = dt.NewRow();
                row["numTreat"] = "Treatment number # " + (x + 1);
                row["doseAmt"] = 0;
                dt.Rows.Add(row);
            }
            return dt;
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public static DataTable getTable()
        {
            return dt;
        }
    }
}
