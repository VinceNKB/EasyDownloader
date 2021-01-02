namespace EasyDownloader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Diagnostics
    {
        private static Dictionary<DebugLevel, ConsoleColor> DisplayColor = new Dictionary<DebugLevel, ConsoleColor>()
        {
            { DebugLevel.Info, ConsoleColor.White },
            { DebugLevel.Debug, ConsoleColor.Green },
            { DebugLevel.Exception, ConsoleColor.Cyan },
            { DebugLevel.Error, ConsoleColor.Yellow },
            { DebugLevel.Critical, ConsoleColor.Red }
        };

        public static bool EnableDebugTrace { get; set; } = true;

        public static DebugLevel Level { get; set; } = DebugLevel.Critical | DebugLevel.Error | DebugLevel.Exception | DebugLevel.Debug | DebugLevel.Info;


        public static void WriteDebugTrace(string trace, DebugLevel level = DebugLevel.Debug)
        {
            if (EnableDebugTrace && (Level & level) != 0)
            {
                Console.ForegroundColor = DisplayColor[level];
                Console.WriteLine(trace);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        [Flags]
        public enum DebugLevel
        {
            Critical = 0,
            Error = 1,
            Exception = 2,
            Debug = 3,
            Info = 4,
            None = 5
        }

    }
}
