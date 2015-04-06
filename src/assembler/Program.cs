using System;
using System.Collections.Generic;
using System.IO;

namespace Assembler
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
            string asmFile = null, binFile = null;
            if (args.Length == 0 || args.Length>2)
            {
                PrintUsage();
                return;
            }
            if (args.Length == 1)
            {
                asmFile = args[0].Trim();
                binFile = string.Format("{0}.bin", Path.GetFileNameWithoutExtension(asmFile));
            }
            if (args.Length == 2)
            {
                asmFile = args[0].Trim();
                binFile = args[1].Trim();
            }
            string[] strings = File.ReadAllLines(asmFile);

            var assembler = new Assembler();
            List<ushort> binaryDump;

            try
            {
                assembler.Assemble(strings);
                binaryDump = assembler.Dump;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Houston we have a problem !");
                Console.WriteLine("You may be using a feature that is not implemented,  See readme.txt");
                Console.WriteLine(exception);
                return;
            }

            if (File.Exists(binFile))
            {
                File.Delete(binFile);
            }

            using (var fs = new FileStream(binFile, FileMode.CreateNew))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (ushort u in binaryDump)
                    {
                        writer.Write(u);
                    }
                }
            }

            Console.WriteLine("{0} successfully written to disk",binFile);

        }

        private static void PrintUsage()
        {
            
            Console.WriteLine("assembler.exe <asm-file> <bin-file>");

        }
    }
}
