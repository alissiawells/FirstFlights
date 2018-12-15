using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

namespace Caesar
{
    class Program
    {
        static void Main(string[] args)
        {
            Cipher c = new Cipher();
            Methods m = new Methods();
            const int k = 1;

            string str = Methods.GetText();

            string dir = AppDomain.CurrentDomain.BaseDirectory;
            string subdir = @"files";
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(subdir);

            Console.WriteLine("1. Зашифровать \n2. Расшифровать \n3. Расшифровка перебором \n4. Угадать сдвиг");
            int opt = Methods.ValidateNumber();

            switch (opt)
            {
                case 1:
                    int shift = Methods.Shift();
                    Methods.Print(Cipher.Encrypt(str, shift, k));
                    break;
                case 2:
                    int dshift = Methods.Shift();
                    Methods.Print(Cipher.Decrypt(str, dshift, -k));
                    break;
                case 3:
                    var text = " ";
                    for (int i = 1; i < 34; i++)
                    {
                        text += "Сдвиг: " + i + " вправо\n" + Cipher.Decrypt(str, i, -k) + "\nСдвиг: " + i + " влево\n" + Cipher.Decrypt(str, -i, -k);
                    }
                    Methods.Print(text);
                    break;
                case 4:
                    Methods.Print(Cipher.GuessShift(str));
                    break;
                default:
                    Console.WriteLine("Incorrect input\n");
                    break;
            }
        }
    }
}