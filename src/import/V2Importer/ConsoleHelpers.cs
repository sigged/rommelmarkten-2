using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2Importer
{
    internal static class ConsoleHelpers
    {
        const int countLength = 30;

        public static void ShowCount(long records, long total, bool jumpback = false)
        {

            if (jumpback)
            {
                Console.SetCursorPosition(Console.CursorLeft - countLength, Console.CursorTop);
            }
            
            string output = $"{records} / {total}";
            output = output.Length > countLength ? output.Substring(0, countLength) : output.PadRight(countLength);
            Console.Write(output);
        }
    }
}
