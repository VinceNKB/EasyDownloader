using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Diagnostics.WriteDebugTrace("Start");
            StartUp();

            string input = string.Empty;

            do
            {
                input = Console.ReadLine();
            }
            while (!input.Contains("exit"));
        }

        public static void StartUp()
        {
            // listen to Clipboard
            ClipboardNotificationHandler cnh = new ClipboardNotificationHandler();
        }
    }
}
