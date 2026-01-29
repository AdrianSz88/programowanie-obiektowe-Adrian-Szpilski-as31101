using AmtAssist.Models.Enums;
using AmtAssist.Models.Osoby;

namespace AmtAssist.Models.Sprawy
{
    public class Zameldowanie : Sprawa
    {
        // Właściwości specyficzne dla zameldowania
        public string MiastoZameldowania { get; set; }
        public string AdresZameldowania { get; set; }
        public DateTime DataPrzeprowadzki { get; set; }
        public bool CzyRodzina { get; set; } // Czy zameldowanie z rodziną

        public Zameldowanie(
            Klient klient,
            string numerSprawy,
            DateTime dataUtworzenia,
            StatusSprawy status,
            string miastoZameldowania,
            string adresZameldowania,
            DateTime dataPrzeprowadzki,
            bool czyRodzina,
            DateTime? termin)
            : base(klient, numerSprawy, dataUtworzenia, status, termin)
        {
            MiastoZameldowania = miastoZameldowania;
            AdresZameldowania = adresZameldowania;
            DataPrzeprowadzki = dataPrzeprowadzki;
            CzyRodzina = czyRodzina;
        }

        // Konstruktor
        public Zameldowanie(
            Klient klient,
            string miastoZameldowania,
            string adresZameldowania,
            DateTime dataPrzeprowadzki,
            bool czyRodzina,
            DateTime? termin = null)
            : base(klient, termin)
        {
            MiastoZameldowania = miastoZameldowania;
            AdresZameldowania = adresZameldowania;
            DataPrzeprowadzki = dataPrzeprowadzki;
            CzyRodzina = czyRodzina;
        }

        public override string PobierzNazwęSprawy()
        {
            return "Zameldowanie (Anmeldung)";
        }

        public override decimal ObliczOpłatę()
        {
            // Opłata bazowa za zameldowanie
            decimal opłataBazowa = 150m;

            // Jeśli zameldowanie z rodziną, dodatkowe 100 zł
            decimal dopłataRodzina = CzyRodzina ? 100m : 0m;

            // Jeśli termin jest bardzo pilny (< 3 dni), dopłata za ekspres
            decimal dopłataEkspres = 0m;
            if (Termin != null && DniDoTerminu() < 3 && DniDoTerminu() >= 0)
            {
                dopłataEkspres = 80m;
            }

            return opłataBazowa + dopłataRodzina + dopłataEkspres;
        }

        public override List<string> PobierzWymaganeDokumenty()
        {
            var dokumenty = new List<string>
            {
                "Paszport lub dowód osobisty",
                "Umowa najmu (Mietvertrag) lub potwierdzenie zamieszkania od właściciela",
                "Formularz Anmeldung (dostępny w urzędzie lub online)"
            };

            if (CzyRodzina)
            {
                dokumenty.Add("Paszporty/dowody wszystkich członków rodziny");
                dokumenty.Add("Akty urodzenia dzieci");
                dokumenty.Add("Akt małżeństwa (jeśli dotyczy)");
            }

            return dokumenty;
        }

        public override bool SprawdźKompletność()
        {
            //sprawdź podstawowe dane
            if (string.IsNullOrWhiteSpace(MiastoZameldowania)) return false;
            if (string.IsNullOrWhiteSpace(AdresZameldowania)) return false;
            if (DataPrzeprowadzki > DateTime.Now) return false; // Data w przyszłości?
            if (Klient == null) return false;

            return true;
        }

        //czy nie przekroczono 14-dniowego limitu
        public bool CzyPrzekroczenoTerminUstawowy()
        {
            int dniOdPrzeprowadzki = (DateTime.Now - DataPrzeprowadzki).Days;
            return dniOdPrzeprowadzki > 14;
        }

        //dodatkowa metoda specyficzna dla zameldowania
        public string PobierzOstrzeżenieOTerminie()
        {
            if (CzyPrzekroczenoTerminUstawowy())
            {
                int dniPóźnienia = (DateTime.Now - DataPrzeprowadzki).Days - 14;
                return $"⚠ UWAGA! Przekroczono ustawowy termin o {dniPóźnienia} dni! Możliwa kara!";
            }

            int dniDoLimitu = 14 - (DateTime.Now - DataPrzeprowadzki).Days;
            if (dniDoLimitu <= 3)
            {
                return $"⚠ Zostały tylko {dniDoLimitu} dni do końca ustawowego terminu!";
            }

            return $"✓ Masz jeszcze {dniDoLimitu} dni na zameldowanie.";
        }

        public override void WyświetlInformacje()
        {
            base.WyświetlInformacje();

            Console.WriteLine($"Miasto: {MiastoZameldowania}");
            Console.WriteLine($"Adres: {AdresZameldowania}");
            Console.WriteLine($"Data przeprowadzki: {DataPrzeprowadzki.ToShortDateString()}");
            Console.WriteLine($"Z rodziną: {(CzyRodzina ? "TAK" : "NIE")}");
            Console.WriteLine($"\n{PobierzOstrzeżenieOTerminie()}");

            Console.WriteLine("\nWymagane dokumenty:");
            int nr = 1;
            foreach (var dok in PobierzWymaganeDokumenty())
            {
                Console.WriteLine($"  {nr}. {dok}");
                nr++;
            }
        }
    }
}