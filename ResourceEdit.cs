using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Smart_Touch_Protocol_Utility.AddProtocols
{
    class ResourceEdit
    {
        /// <summary>
        /// Modifies the .txt and .resource files located within STUV to allow user defined  UVA
        /// protocol to appear within STUV with the proper code and description.
        /// </summary>
        /// <param name="uvCode"></param>
        /// <param name="uvDescrip"></param>
        public static void resourceUVAEdit(string uvCode, string uvDescrip)
        {
            string path = @"C:\Program Files (x86)\Daavlin\Smart Touch\Daavlin STUV 4.0\Resources\";

            try
            {
                using (StreamWriter w = File.AppendText(path + "English.txt"))
                {
                    w.WriteLine();
                    w.WriteLine();
                    w.WriteLine("; codes for database strings");
                    w.WriteLine();
                    w.WriteLine("uvatreatmenttypes.uvatreatmenttypedescription." + uvCode + " = " + uvDescrip);
                }
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = Directory.GetCurrentDirectory() + @"\ResGen.bat";
                Process.Start(info);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Modifies the .txt and .resource files located within STUV to allow user defined UVB
        /// protocol to appear within STUV with the proper code and description.
        /// </summary>
        /// <param name="uvCode"></param>
        /// <param name="uvDescrip"></param>
        public static void resourceUVBEdit(string uvCode, string uvDescrip)
        {
            string path = @"C:\Program Files (x86)\Daavlin\Smart Touch\Daavlin STUV 4.0\Resources\";

            try
            {
                using (StreamWriter w = File.AppendText(path + "English.txt"))
                {
                    w.WriteLine();
                    w.WriteLine();
                    w.WriteLine("; codes for database strings");
                    w.WriteLine();
                    w.WriteLine("uvbtreatmenttypes.uvbtreatmenttypedescription." + uvCode + " = " + uvDescrip);
                }
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = Directory.GetCurrentDirectory() + @"\ResGen.bat";
                Process.Start(info);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
