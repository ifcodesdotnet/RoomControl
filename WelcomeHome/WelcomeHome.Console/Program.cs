using System;
using WemoNet;

namespace WelcomeHome.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo input;
            Wemo wemo = new Wemo();
            do
            {
                input = System.Console.ReadKey();

                if (Char.ToLower(input.KeyChar) == 'h')
                {
                    wemo.TurnOnWemoPlugAsync("http://10.0.0.9").GetAwaiter().GetResult();
                }
                else if (Char.ToLower(input.KeyChar) == 'l')
                {
                    wemo.TurnOffWemoPlugAsync("http://10.0.0.9").GetAwaiter().GetResult();
                }
            }
            while (Char.ToLower(input.KeyChar) != 'e');
        }
    }
}
