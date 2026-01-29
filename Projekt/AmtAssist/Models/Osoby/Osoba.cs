namespace AmtAssist.Models.Osoby
{
    public abstract class Osoba
    {
        //właściwości - dane każdej osoby
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        //konstruktor - wywoływany przy tworzeniu nowej osoby
        protected Osoba(string imię, string nazwisko, string email, string telefon)
        {
            Imię = imię;
            Nazwisko = nazwisko;
            Email = email;
            Telefon = telefon;
        }

        //metoda abstrakcyjna - każda klasa pochodna MUSI ją zaimplementować
        public abstract void WyświetlDane();

        //zwykła metoda - dostępna dla wszystkich klas pochodnych
        public string PełneImię()
        {
            return $"{Imię} {Nazwisko}";
        }
    }
}