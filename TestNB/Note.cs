using System;

namespace NotebookApp
{
    public class Note
    {
        public int Id { get; set; }
        public long PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; } 
        public DateTime Birthday { get; set; } 
        public string Country { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public string Additional { get; set; }

        // Форматирование вывода
        public string Brief()
        {
            return String.Format("{0}. {1} {2}: {3}", Id, Name, Surname, PhoneNumber);
        }

        public string Full()
        {
            return String.Format("Id: {0}\nName: {1}\nMiddlename: {2}\nSurname: {3}\nPhone: {4}\nCountry: {5}\nOrganization: {6}\nPosition: {7}\nBirthday: {8}\nAdditional: {9}", Id, Name, MiddleName, Surname, PhoneNumber, Country, Organization, Position, Birthday, Additional);
        }

        public Note(int id, long phoneNumber, string Name, string Surname, string MiddleName=" ", DateTime Birthday=default(DateTime), string Country=" ", string Organization=" ", string Position=" ", string Additional=" ")
        {
            this.Id = id;
            this.PhoneNumber = phoneNumber;
            this.Name = Name;
            this.Surname = Surname;
            this.MiddleName = " ";
            this.Birthday = Birthday;
            this.Country = Country;
            this.Organization = Organization;
            this.Position = Position;
            this.Additional = Additional;
        }
    }
}
