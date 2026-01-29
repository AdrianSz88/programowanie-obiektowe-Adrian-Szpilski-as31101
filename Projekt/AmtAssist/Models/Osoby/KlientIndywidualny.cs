namespace AmtAssist.Models.Osoby
{
    public class KlientIndywidualny : Klient
    {
        // Dodatkowe właściwości specyficzne dla klienta indywidualnego
        public DateTime DataUrodzenia { get; set; }
        public string MiejsceUrodzenia { get; set; }

        // Konstruktor
        public KlientIndywidualny(
            string imię,
            string nazwisko,
            string email,
            string telefon,
            string pesel,
            string adresPolska,
            string adresNiemcy,
            DateTime dataUrodzenia,
            string miejsceUrodzenia)
            : base(imię, nazwisko, email, telefon, pesel, adresPolska, adresNiemcy)
        {
            DataUrodzenia = dataUrodzenia;
            MiejsceUrodzenia = miejsceUrodzenia;
        }

        // Implementacja metody abstrakcyjnej z klasy Osoba
        public override void WyświetlDane()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    KLIENT INDYWIDUALNY                         ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Imię i nazwisko:        {PełneImię(),-38} ║");
            Console.WriteLine($"║ PESEL:                  {PESEL,-38} ║");
            Console.WriteLine($"║ Data urodzenia:         {DataUrodzenia.ToShortDateString(),-38} ║");
            Console.WriteLine($"║ Miejsce urodzenia:      {MiejsceUrodzenia,-38} ║");
            Console.WriteLine($"║ Email:                  {Email,-38} ║");
            Console.WriteLine($"║ Telefon:                {Telefon,-38} ║");
            Console.WriteLine($"║ Adres w Polsce:         {AdresPolska,-38} ║");
            Console.WriteLine($"║ Adres w Niemczech:      {AdresNiemcy,-38} ║");
            Console.WriteLine($"║ Data rejestracji:       {DataRejestracji.ToShortDateString(),-38} ║");
            Console.WriteLine($"║ Liczba spraw:           {LiczbaSpraw(),-38} ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
        }

        // Metoda sprawdzająca wiek klienta
        public int ObliczWiek()
        {
            int wiek = DateTime.Now.Year - DataUrodzenia.Year;
            if (DateTime.Now.DayOfYear < DataUrodzenia.DayOfYear)
                wiek--;
            return wiek;
        }
    }
}