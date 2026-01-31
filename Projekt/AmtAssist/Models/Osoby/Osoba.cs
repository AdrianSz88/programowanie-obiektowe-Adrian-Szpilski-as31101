namespace AmtAssist.Models.Osoby
{
    public abstract class Osoba
    {
        //właściwości - dane każdej osoby
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        //konstruktor 
        protected Osoba(string imię, string nazwisko, string email, string telefon)
        {
            Imię = imię;
            Nazwisko = nazwisko;
            Email = email;
            Telefon = telefon;
        }

        //metoda abstrakcyjna 
        public abstract void WyświetlDane();

        //zwykła metoda
        public string PełneImię()
        {
            return $"{Imię} {Nazwisko}";
        }
    }
}