using System;

namespace ExeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
                Console.WriteLine(args[0]);
            else if (args.Length == 2)
                Console.WriteLine("<HTML><BODY> Hello (from external process) " + args[0] + " et " + args[1] + "</BODY></HTML>");
            else
                Console.WriteLine("ExeTest <string parameter>");
        }
    }
}
