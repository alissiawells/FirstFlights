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
    public class Methods
    {
        public static string GetText()
        {
            while (true)
            {
                Console.WriteLine("1. Загрузить файл \n2. Ввести текст");
                int val = ValidateNumber();
                switch (val)
                {
                    case 1: // чтение из файла
                        string text = UploadFile();
                        if (UseFile(text))
                            return text;
                        else
                            break;
                    case 2: // ввод с консоли
                        Console.WriteLine("Введите текст");
                        return Console.ReadLine();
                    default:
                        Console.WriteLine("Введите 1 / 2");
                        break;
                }
            }
        }


        public static string UploadFile()
        {
            Console.WriteLine("Введите название файла");

            while (true)
            {
                string filename = Console.ReadLine();
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", filename);
                if (!File.Exists(path))
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
                if (!File.Exists(path))
                    Console.WriteLine("Введите название другого файла");
                else
                {
                    if (Path.GetExtension(path) == ".docx")
                        try
                        {
                            using (var extractor = new DocxExtractor(path))
                                return extractor.ReadWordDocument();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка при извлечении текста из файла" + ex.ToString());
                            Console.ReadKey();
                        }
                    else
                    {
                        if (Path.GetExtension(path) != ".txt")
                            Console.WriteLine("Поддерживаемые форматы: txt, docx");
                        else
                        {
                            using (FileStream fstream = File.OpenRead(path))
                            {
                                byte[] array = new byte[fstream.Length];
                                fstream.Read(array, 0, array.Length);
                                string text = System.Text.Encoding.Default.GetString(array);
                                if (UseFile(text))
                                    return text;
                               
                            }
                        }
                    }
                }

            }
        }


        public static bool UseFile(string text)
        {
            while (true)
            {
                Console.WriteLine("Посмотреть текст?\n да / нет");
                string val = Console.ReadLine().Trim().ToLower();
                if (val.Equals("да"))
                {
                    Console.Clear();
                    Console.WriteLine(text);
                    Console.WriteLine("\tИспользовать этот файл?\n да / нет");
                    string opt = Console.ReadLine().Trim().ToLower();
                    while (true)
                    {
                        if (opt.Equals("да"))
                            return true;
                        else
                        {
                            if (opt.Equals("нет"))
                                return false;
                            else
                                Console.WriteLine("Введите да / нет");
                        }
                    }
                }

                if (val.Equals("нет"))
                    return true;
                else
                {
                    Console.WriteLine("Введите да / нет");
                }
            }
        }


        public static void Print(string text)
        {
            Console.WriteLine("1. Записать в файл 2. Вывести на экран");
            int opt = ValidateNumber();
            switch (opt)
            {
                case 1:
                    Console.WriteLine("Введите название файла");
                    string filename = Console.ReadLine();
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", filename);
                    if (File.Exists(path))
                    {
                        Console.WriteLine("Файл существует. \n1. Перезаписать \n2. Дозаписать \n3. Использовать другой файл");
                        int val = ValidateNumber();
                        switch (val)
                        {
                            case 1:
                                WriteFile(path, filename, text, false);
                                break;
                            case 2:
                                WriteFile(path, filename, text, true);
                                break;
                            case 3:
                                break;
                            default:
                                Console.WriteLine("Введите 1 / 2 / 3");
                                break;
                        }
                    }
                    else
                        WriteFile(path, filename, text, false);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine(text);
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Введите 1 / 2");
                    break;
            }
        }
 
        public static void WriteFile(string path, string filename, string text, bool option)
        {
            using (StreamWriter w = new StreamWriter(path, option, System.Text.Encoding.Default))
            {
                w.WriteLine(text);
            }
            Console.WriteLine("Сохранено в " + filename);
        }

        // сдвиг
        public static int Shift()
        {
            Console.WriteLine("Введите значение сдвига");
            int shift = ValidateNumber();
            if (shift > 33)
                shift = shift % 33;
            Console.WriteLine("Введите направление сдвига: влево / вправо");
            string dshift = Console.ReadLine().Trim().ToLower();
            while (true)
            {
                if (dshift.Equals("влево"))
                    return shift = -shift;
                if (dshift.Equals("вправо"))
                    return shift;
                else
                {
                    Console.WriteLine("Неверный формат ввода, введите \"влево\" или \"вправо\"");
                    dshift = Console.ReadLine().Trim().ToLower();
                }
            }
        }


        // валидация int
        public static int ValidateNumber()
        {
            while (true)
            {
                string input = Console.ReadLine().Trim();
                if (!int.TryParse(input, out int opt))
                    Console.WriteLine("Введите целое число");
                else
                    return int.Parse(input);
            }
        }
    }
}