using EncryptFig.Cmd.Services;
using System;

namespace EncryptFig.Cmd
{
    internal class Program
    {
        //https://www.codeproject.com/Tips/795135/Encrypt-ConnectionString-in-Web-Config
        private static void Main(string[] args)
        {
            WelcomeMessage();
        }

        private static void WelcomeMessage()
        {
            var service = new ConfigService();
            Console.WriteLine("Welcome to Enrypt, your Encryption and Decryption tool... \n \n ");

            Console.WriteLine("Configuration:");
            Console.WriteLine(service.OutputConfig());
            Console.WriteLine("\n \n");
            Console.WriteLine("Please type either 'encrypt' or 'decrypt' to perform the action \n ('e' or 'd' is also acceptable) \n \n Input:");
            var input = Console.ReadLine();
            if (!ReadInput(input))
            {
                Console.Clear();
                WelcomeMessage();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private static bool ReadInput(string input)
        {
            input = input.ToLower();
            input = input.Trim();
            var myInput = input[0];

            if (myInput == 'e' || myInput == 'd')
            {
                _selectAction(myInput);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void _selectAction(char input)
        {
            var service = new ConfigService();
            if (input == 'e')
            {
                Console.WriteLine(service.EnCrypt());
                _successMessage();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine(service.DeCrypt());
                _successMessage();
                Console.ReadLine();
            }
        }

        private static void _successMessage()
        {
            Console.WriteLine("\n \n Success!!! \n \n press any key to exit....");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}