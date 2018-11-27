using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NotebookApp
{
    public class NoteBook : IEnumerable<Note>
    {
        public List<Note> notes;
        List<Note> list = new List<Note>();
        private int i;

        public NoteBook()
        {
            this.notes = list;
        }

        public void ShowAllNotes()
        {
            if (notes.Count != 0)
            {
                foreach (var d in notes)
                {
                    Console.WriteLine(d.Brief());
                }
            }
            else
            {
               Console.WriteLine("Tabula rasa"); 
            }
        }
        public void AddNewNote()
        {
            Console.WriteLine("Name: ");
            string Name = Console.ReadLine();
            Console.WriteLine("Surname: ");
            string Surname = Console.ReadLine();
            long phoneNumber = PhoneValidation();
            var index = GetIndex(Name, Surname, phoneNumber);
            notes.Add(new Note(index, phoneNumber, Name, Surname)); 
            Console.WriteLine("Anything to add? Type \"yes\" or press any key to exit");
            if (Console.ReadLine().Trim().ToLower().Equals("yes"))
            {
                Editor(index);
            }
        }

        public void DeleteNote()
        {
            Console.WriteLine("Enter id"); 
            int id = int.Parse(Console.ReadLine());
            notes.RemoveAt(id - 1);
            //Правильный порядок индексов после удаления записи
            for (int i = 0; i < notes.Count; i++)
            {
                Note s = notes[i];
                if (s.id != notes.IndexOf(s) + 1)
                {
                    s.id = notes.IndexOf(s) + 1;
                }

            }
        }
        public void EditNote()
        {
            Console.WriteLine("Enter id: ");
            int index = int.Parse(Console.ReadLine());
            Editor(index);
        }

        public void Editor(int index)
        {
             while (true)
            {
                Console.Clear();
                Console.WriteLine("[N]ame \n[S]urname \n[P]hone number \n[M]iddleName \n[B]irthday \n[C]ontry \n[O]rganization \n[Pos]ition \n[A]dditional\ne[X]it");
                string choice = Console.ReadLine().Trim().ToUpper();
                switch (choice)
                {
                    case "N":
                        Console.WriteLine("New name: ");
                        notes[index-1].Name = Console.ReadLine();
                        break;
                    case "S":
                        Console.WriteLine("New Surname: ");
                        notes[index-1].Surname = Console.ReadLine();
                        break;
                    case "M":
                        Console.WriteLine("Enter Middlename: ");
                        notes[index-1].MiddleName = Console.ReadLine();
                        break;
                    case "P":
                        notes[index-1].phoneNumber = PhoneValidation();
                        break;
                    case "B":
                        Console.WriteLine("Enter date of Birthday: ");
                        notes[index-1].Birthday = DateTime.Parse(Console.ReadLine());
                        break;
                    case "C":
                        Console.WriteLine("Enter Country: ");
                        notes[index-1].Country = Console.ReadLine();
                        break;
                    case "O":
                        Console.WriteLine("Enter Organization: ");
                        notes[index-1].Organization = Console.ReadLine();
                        break;
                    case "POS":
                    Console.WriteLine("Enter Position: ");
                        notes[index-1].Position = Console.ReadLine();
                        break;
                    case "A":
                    Console.WriteLine("Enter Additional: ");
                        notes[index-1].Additional = Console.ReadLine();
                        break; 
                    case "X":
                        return ;  
                    default:
                        Console.Clear();
                        Console.WriteLine("Incorrect input\n");
                        break;
                }
            }
        }

        public void ShowANote()
        {
           Console.WriteLine("Enter id:");
           int id = int.Parse(Console.ReadLine());
           Console.WriteLine(notes[id - 1].Full());
           Console.WriteLine("Press any key");
           Console.ReadKey();

        }
        public static long PhoneValidation()
        {
            while (true)
            {
                long phoneNumber;
                Console.WriteLine("Enter phone (more than 5 numbers): ");
                string input = Console.ReadLine().Trim();
                if ((input.Length < 5) || (!long.TryParse(input, out phoneNumber)))
                {
                    Console.WriteLine("Incorrect input");
                }
                else
                {
                    return long.Parse(input);
                }
            }
        }

        private int GetIndex(string Name, string Surname, long phoneNumber)
        {
            if (notes.Count > 0)
            {
                return notes.Count+1;
            }
            else
            {
                return 1;
            }
        }

        public IEnumerator<Note> GetEnumerator() 
        {
            return notes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
