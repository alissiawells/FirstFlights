using System;

namespace NotebookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var nb = new NoteBook();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine((new string('=', 79)));
                Console.WriteLine("\t\tDeviant profiling");
                Console.WriteLine((new string('=', 79)));
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.White;
                nb.ShowAllNotes();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine((new string('=', 79)));
                Console.WriteLine("Usage: ");
                Console.WriteLine("1 - Add deviant\n2 - Delete a note\n3 - Edit a note\n4 - Show full profile\n5 - Quit");
                Console.WriteLine((new string('=', 79)));
                Console.Write("Enter option: ");
                Console.ForegroundColor = ConsoleColor.White;

                string option = Console.ReadLine().Trim();
                switch (option)
                {
                    case "1":
                        nb.AddNewNote();
                        break;
                    case "2":
                        nb.DeleteNote();
                        break;
                    case "3":
                        nb.EditNote();
                        break;
                    case "4":
                        Console.Clear();
                        nb.ShowANote();
                        break;
                    case "5":
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Incorrect input\n");
                        break;
                }

            }
        }
 
    }
}
