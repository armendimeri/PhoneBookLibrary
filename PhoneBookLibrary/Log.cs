using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookLibrary
{
    public class Log
    {
        /// <summary>
        /// Custom exception logging, logs are saved in docs/Logs.txt
        /// </summary>
        /// <param name="ex">Thrown exception</param>
        /// <param name="description">Description or location of code</param>
        public static void Error(Exception ex, string description)
        {
            //Continue trying when file is locked
            bool written = false;
            while (!written)
            {
                try
                {
                    string filePath = @"Logs.txt";

                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine(description + Environment.NewLine + "Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                           "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                        writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    }
                    written = true;
                }
                catch
                {

                }

            }
        }
    }
}
