using System;
using System.Diagnostics;
using System.IO;

namespace WebDynamic
{
    class Mymethods
    {
        public string displayFirstTwoParams(string param1, string param2)
        {
            return "<HTML><BODY> Hello " + param1 + " et " + param2 + "</BODY></HTML>";
        }
        public string displayFirstTwoParamsOnExternalExec(string param1, string param2)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\user\Desktop\Polytech\SI4A 2020-2021\S8\soc-ws\eiin839\TD2\ExecTest\bin\Debug\ExecTest.exe"; // Specify exe name.
            start.Arguments = param1 + " " + param2;
            Console.WriteLine(start.Arguments);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

            String returnValue;
            using (Process process = Process.Start(start))
            {
                //
                // Read in all the text from the process with the StreamReader.
                //
                using (StreamReader reader = process.StandardOutput)
                {
                    returnValue = reader.ReadToEnd();
                }
            }
            return returnValue;
        }
    }
}
