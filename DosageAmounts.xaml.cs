using System;
using System.Data;
using System.Windows;

namespace Smart_Touch_Protocol_Utility
{
    public partial class DosageAmounts : Window
    {
        private int numTreat;
        public static DataTable userDoses { get; private set; }

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
            userDoses = new DataTable();
            userDoses.Columns.Add(new DataColumn("numTreat", Type.GetType("System.String")));
            userDoses.Columns.Add(new DataColumn("doseAmt", Type.GetType("System.Double")));

            for (int x = 0; x < numTreat; ++x)
            {
                DataRow row = null;
                row = userDoses.NewRow();
                row["numTreat"] = "Treatment number # " + (x + 1);
                row["doseAmt"] = DBNull.Value;
                userDoses.Rows.Add(row);
            }
            return userDoses;
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
