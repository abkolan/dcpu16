using System;
using System.Collections.Generic;
using System.IO;

namespace Emulator3
{
    class Program
    {
        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Houston we have a problem !");
            Console.WriteLine("You may be using a feature that is not implemented, See readme.txt");
            Console.WriteLine(e.ExceptionObject);
        }

        static void Main(string[] args)
        {
            string binFile = null;
            if (args.Length == 0 || args.Length > 1)
            {
                PrintUsage();
                return;
            }
            if (args.Length == 1)
            {
                binFile = args[0];
            }

            InitVideo();

            List<ushort> binaryDump = new List<ushort>();

            using (var fs = new FileStream(binFile, FileMode.Open))
            {
                using (var reader = new BinaryReader(fs))
                {

                    int pos = 0;
                    int length = (int)reader.BaseStream.Length;
                    while (pos < length)
                    {
                        ushort word = reader.ReadUInt16();
                        binaryDump.Add(word);
                        pos += sizeof (ushort);
                    }
                }
            }


            var vm = new DCPUVM();
            vm.RefreshVideo += vm_RefreshVideo;

            vm.Execute(binaryDump);


            Console.ReadKey();
        }

        private static void PrintUsage()
        {
            Console.WriteLine("assembler.exe <bin-file>");

        }

        private static void InitVideo()
        {
            Console.ResetColor();
            Console.Clear();
            Console.Title = "0x10c Emulator";
            Console.SetWindowSize(32,16 );
            Console.CursorVisible = false;

        }

        static void vm_RefreshVideo(object sender, VideoArgs e)
        {
            Display(e.Address,e.Value);

        }

        private static Dictionary<int,ConsoleColor> consolePallete = new Dictionary<int, ConsoleColor>(); 

        static void Display(int address, ushort displayValue)
        {

            // See http://0x10command.com/dcpu-assembly-tutorials/dasm-lesson-8-display-output/

            int adjustedAddress = address - 32768;
            int y = (int) adjustedAddress/32;
            int x = adjustedAddress%32;
            Console.SetCursorPosition(x,y);

            ushort textValue = (ushort) (0xff & displayValue);
            char c = Convert.ToChar(textValue);

            int backColor = (0x0f00 & displayValue)>>8;
            ConsoleColor backConsoleColor = (ConsoleColor) backColor;


            int foreColor = (0xf000 & displayValue)>>12;
            ConsoleColor foreConsoleColor = (ConsoleColor) foreColor;

            Console.BackgroundColor = backConsoleColor;
            Console.ForegroundColor = foreConsoleColor;

            Console.Write(c);

        }
    }
}
