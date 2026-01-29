using AmtAssist.Models.Osoby;

namespace AmtAssist.Services
{
    public class KlientService
    {
        // Lista wszystkich klientów (kolekcja generyczna)
        private List<Klient> _klienci;

        // Konstruktor
        public KlientService()
        {
            _klienci = new List<Klient>();
        }

        // Dodaj klienta do listy
        public void DodajKlienta(Klient klient)
        {
            _klienci.Add(klient);
            KolorKonsoli.Sukces($"\n✓ Klient {klient.PełneImię()} został dodany do systemu.");
        }

        // Pobierz wszystkich klientów
        public List<Klient> PobierzWszystkichKlientów()
        {
            return _klienci;
        }

        // Wyszukaj klienta po PESEL
        //LINQ - wyszukiwanie w kolekcji
        //k => k.PESEL == pesel - wyrażenie lambda(funkcja anonimowa)
        //Czytaj to jako: "dla każdego klienta k, sprawdź czy jego PESEL równa się szukanemu"
        public Klient? ZnajdźKlientaPoPESEL(string pesel)
        {
            // LINQ - wyrażenie lambda
            return _klienci.FirstOrDefault(k => k.PESEL == pesel);
        }

        // Wyszukaj klienta po imieniu i nazwisku
        public List<Klient> ZnajdźKlientówPoImieniu(string szukanyTekst)
        {
            szukanyTekst = szukanyTekst.ToLower();

            // LINQ - filtrowanie
            return _klienci.Where(k =>
                k.Imię.ToLower().Contains(szukanyTekst) ||
                k.Nazwisko.ToLower().Contains(szukanyTekst)
            ).ToList();
        }

        // Wyświetl listę wszystkich klientów
        public void WyświetlListęKlientów()
        {
            if (_klienci.Count == 0)
            {
                Console.WriteLine("\nBrak klientów w systemie.");
                return;
            }

            Console.WriteLine($"\n=== LISTA KLIENTÓW ({_klienci.Count}) ===");
            Console.WriteLine();

            int nr = 1;
            foreach (var klient in _klienci)
            {
                Console.WriteLine($"{nr}. {klient.PełneImię()}");
                Console.WriteLine($"   PESEL: {klient.PESEL}");
                Console.WriteLine($"   Email: {klient.Email}");
                Console.WriteLine($"   Liczba spraw: {klient.LiczbaSpraw()}");
                Console.WriteLine();
                nr++;
            }
        }

        // Wyświetl szczegóły klienta
        public void WyświetlSzczegółyKlienta(string pesel)
        {
            var klient = ZnajdźKlientaPoPESEL(pesel);

            if (klient == null)
            {
                Console.WriteLine($"\n✗ Nie znaleziono klienta o PESEL: {pesel}");
                return;
            }

            klient.WyświetlDane();

            // Wyświetl sprawy klienta
            var sprawy = klient.PobierzSprawy();
            if (sprawy.Count > 0)
            {
                Console.WriteLine("\n--- SPRAWY KLIENTA ---");
                foreach (var sprawa in sprawy)
                {
                    Console.WriteLine($"• {sprawa.NumerSprawy} - {sprawa.PobierzNazwęSprawy()} ({sprawa.Status})");
                }
            }
        }

        // Pobierz liczbę klientów
        public int LiczbaKlientów()
        {
            return _klienci.Count;
        }

        // Pobierz klientów posortowanych alfabetycznie
        public List<Klient> PobierzKlientówPosortowanych()
        {
            // LINQ - sortowanie
            return _klienci.OrderBy(k => k.Nazwisko).ThenBy(k => k.Imię).ToList();
        }

        // Sprawdź czy klient o danym PESEL już istnieje
        public bool CzyKlientIstnieje(string pesel)
        {
            return _klienci.Any(k => k.PESEL == pesel);
        }
    }
}