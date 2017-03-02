using System.Diagnostics;
using System.IO;

namespace Smart_Touch_Protocol_Utility.AddProtocols
{
    class ResourceEdit
    {
        // Currently not in use due to needing admin privs to 
        // make changes to the files.
        public static void resourceUVAEdit(string uvCode, string uvDescrip)
        {
            string path = @"C:\Program Files (x86)\Daavlin\Smart Touch\Daavlin STUV 4.0\Resources\English.txt";

            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine("uvatreatmenttypes.uvatreatmenttypedescription." + uvCode + " = " + uvDescrip);
            }

            ProcessStartInfo resgen = new ProcessStartInfo();
            resgen.Verb = "runas";
            resgen.FileName = @"C:\Program Files (x86)\Daavlin\Smart Touch\Daavlin STUV 4.0\Resources\Resgen.exe";
            resgen.Arguments = "English.txt";
            Process.Start(resgen);
        }

        // Currently not in use due to needing admin privs to 
        // make changes to the files.
        public static void resourceUVBEdit(string uvCode, string uvDescrip)
        {
            string path = @"C:\Program Files (x86)\Daavlin\Smart Touch\Daavlin STUV 4.0\Resources\English.txt";

            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine("uvbtreatmenttypes.uvbtreatmenttypedescription." + uvCode + " = " + uvDescrip);
            }

            ProcessStartInfo resgen = new ProcessStartInfo();
            resgen.Verb = "runas";
            resgen.FileName = @"C:\Program Files (x86)\Daavlin\Smart Touch\Daavlin STUV 4.0\Resources\Resgen.exe";
            resgen.Arguments = "English.txt";
            Process.Start(resgen);
        }
    }
}
