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
    public class Cipher
    {
        public string text;
        public int shift;
        public int k = 1;
        
        static public string Func(string text, int shift, int k)
        {
            string message = null;
            for (int i = 0; i < text.Length; i++)
            {
                int ASCII = (int)text[i];

                if (ASCII == 1025) // Ё 
                {
                    if ((1046 + k * shift < 1041) || (1046 + k * shift > 1103))
                        message += (char)(1045 + k * (shift - 33));
                    else
                        message += (char)(1045 + k * shift);
                }
                else
                {
                    if (ASCII == 1105) // ё
                    {
                        if ((1078 + k * shift < 1041) || (1078 + k * shift > 1103))
                            message += char.ToLower((char)(1077 + k * (shift - 33)));
                        else
                            message += char.ToLower((char)(1078 + k * shift));
                    }
                    else
                    {
                        // не кириллица 
                        if ((ASCII < 1040) || (ASCII > 1103))
                            message += text[i];
                    }
                }

                if (ASCII == 1045) // Е 
                {
                    if (shift == 1)
                        message += (char)(1025);
                    else
                    {
                        if ((1046 + k * shift < 1041) || (1046 + k * shift > 1103))
                            message += (char)(1045 + k * (shift - 33));
                        else
                            message += (char)(1045 + k * shift);
                    }
                }
                if (ASCII == 1077) // е 
                {
                    if (shift == 1)
                        message += (char)(1105);
                    else
                    {
                        if ((1077 + k * shift < 1041) || (1077 + k * shift > 1103))
                            message += char.ToLower((char)(1076 + k * (shift - 33)));
                        else
                            message += char.ToLower((char)(1077 + k * shift));
                    }
                }

                if (ASCII == 1078) // ж
                {
                    if (k * shift == -1)
                        message += (char)(1105);
                    else
                    {
                        if ((1078 + k * shift < 1041) || (1078 + k * shift > 1072))
                            message += char.ToLower((char)(1078 + k * (shift - 31)));
                        else
                            message += char.ToLower((char)(1078 + k * shift));
                    }
                }

                if (ASCII == 1071) // Я 
                {
                    if (shift == 1)
                        message += (char)(1040);
                    else
                    {
                        if ((1071 + k * shift < 1041) || (1071 + k * shift > 1072))
                            message += (char)(1071 + k * (shift - 33));
                        else
                            message += (char)(1071 + k * shift);
                    }
                }

                if (ASCII == 1103) // я
                {
                    if (shift == 1)
                        message += (char)(1072);
                    else
                    {
                        if ((1103 + k * shift < 1041) || (1103 + k * shift > 1072))
                            message += char.ToLower((char)(1104 + k * (shift - 33)));
                        else
                            message += char.ToLower((char)(1103 + k * shift));
                    }
                }

                // строчная буква
                if ((ASCII > 1071) && (ASCII < 1077)) // a-д
                {
                    // после сдвига буква выходит за пределы алфавита в нижнем регистре
                    if ((ASCII + k * shift < 1041) || (ASCII + k * shift > 1103))
                        message += char.ToLower((char)(ASCII + k * (shift - 33)));
                    // буква может быть сдвинута в пределах алфавита
                    else
                        message += char.ToLower((char)(ASCII + k * shift));
                }
                if ((ASCII > 1078) && (ASCII < 1103)) // ж-ю
                {
                    // после сдвига буква выходит за пределы алфавита
                    if ((ASCII + k * shift < 1041) || (ASCII + k * shift > 1103))
                        message += (char)(ASCII + k * (shift - 33));
                    // буква может быть сдвинута в пределах алфавита
                    else
                        message += (char)(ASCII + k * shift);
                }

                // прописная буква
                if ((ASCII > 1039) && (ASCII < 1046)) //  А-Е
                {
                    //Если буква после сдвига выходит за пределы алфавита
                    if ((ASCII + k * shift < 1041) || (ASCII + k * shift > 1103))
                        message += (char)(ASCII + k * (shift - 33));
                    //Если буква может быть сдвинута в пределах алфавита
                    else
                        message += (char)(ASCII + k * shift);
                }
                if ((ASCII > 1045) && (ASCII < 1071)) // Ж-Ю
                {
                    //Если буква, после сдвига выходит за пределы алфавита
                    if ((ASCII + k * shift < 1041) || (ASCII + k * shift > 1104))
                        message += (char)(ASCII + k * (shift - 31));
                    //Если буква может быть сдвинута в пределах алфавита
                    else
                        message += (char)(ASCII + k * shift);
                }
            }
            return message;
        }

        static public string Encrypt(string text, int shift, int k)
        {
            return Func(text, shift, k);
        }

        static public string Decrypt(string text, int shift, int k)
        {
            return Func(text, shift, -k);
        }

        public static string GuessShift(string text)
        {
            var decrypted="";
            var freq = text.GroupBy(x => x).OrderByDescending(x => x.Count()).ToList();
            string oftenUsed = " оаеинтсрвлкмдпуяыьгзбчйхжшэфъё";
            for (int i = 0; i < freq.Count; i++)
                decrypted = text.Replace(freq[i].Key, oftenUsed[i]);
            return decrypted;
        }
    }
}