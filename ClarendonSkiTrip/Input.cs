using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarendonSkiTrip
{
    class Input
    {
        public static string GetInput(string _text)
        {
            Console.WriteLine($"{_text}: ");
            return Console.ReadLine();
        }

        public static char GetCharInput(string _text)
        {
            while (true)
            {
                try
                {
                    return Convert.ToChar(GetInput(_text));
                }
                catch
                {
                    Console.WriteLine("Enter a char!");
                }
            }
        }

        public static bool GetYesNo(string _text)
        {
            char confirm;

            do
            {
                confirm = GetCharInput($"{_text} (Y/N)");

                if (confirm == 'n') return false;
            } while (confirm != 'y');

            return true;
        }

        public static int GetInt(string _text)
        {
            try
            {
                return Convert.ToInt16(GetInput(_text));
            }
            catch (Exception)
            {
                Console.Error.Write("Enter a number");
                return 0;
            }
        }

        public static void End()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
        }

        public static string GetPrivateInput(string _text)
        {
            Console.WriteLine($"{_text}: ");

            char key;

            string text = "";

            while ((key = Console.ReadKey(true).KeyChar) == key)
            {
                if (key == ((char)ConsoleKey.Enter))
                {
                    Console.Write("\n");
                    return text;
                }

                if (key == (char)ConsoleKey.Backspace && text.Length > 0)
                {
                    Console.Write("\b \b");
                    text = text.Substring(0, text.Length - 1);
                }
                else
                {
                    Console.Write("*");
                    text += key.ToString();
                }
            }

            return "";
        }
    }
}
