namespace AmtAssist.Services
{
    public class WalidatorPESEL
    {
        // Wagi do obliczania sumy kontrolnej
        private static readonly int[] Wagi = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

        // Główna metoda walidacji
        public static bool CzyPrawidłowy(string pesel)
        {
            // Sprawdź czy PESEL ma 11 cyfr
            if (string.IsNullOrWhiteSpace(pesel))
                return false;

            if (pesel.Length != 11)
                return false;

            // Sprawdź czy wszystkie znaki to cyfry
            foreach (char c in pesel)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            // Oblicz sumę kontrolną
            int suma = 0;
            for (int i = 0; i < 10; i++)
            {
                int cyfra = int.Parse(pesel[i].ToString());
                suma += cyfra * Wagi[i];
            }

            // Oblicz cyfrę kontrolną
            int reszta = suma % 10;
            int cyfraKontrolna = (10 - reszta) % 10;

            // Porównaj z ostatnią cyfrą PESEL
            int ostatniaCyfra = int.Parse(pesel[10].ToString());

            return cyfraKontrolna == ostatniaCyfra;
        }

        // Dodatkowa metoda - wyciągnij datę urodzenia z PESEL
        public static DateTime? PobierzDateUrodzenia(string pesel)
        {
            if (!CzyPrawidłowy(pesel))
                return null;

            try
            {
                int rok = int.Parse(pesel.Substring(0, 2));
                int miesiąc = int.Parse(pesel.Substring(2, 2));
                int dzień = int.Parse(pesel.Substring(4, 2));

                // Obsługa różnych wieków (1900, 2000, etc.)
                if (miesiąc > 80)
                {
                    rok += 1800;
                    miesiąc -= 80;
                }
                else if (miesiąc > 60)
                {
                    rok += 2200;
                    miesiąc -= 60;
                }
                else if (miesiąc > 40)
                {
                    rok += 2100;
                    miesiąc -= 40;
                }
                else if (miesiąc > 20)
                {
                    rok += 2000;
                    miesiąc -= 20;
                }
                else
                {
                    rok += 1900;
                }

                return new DateTime(rok, miesiąc, dzień);
            }
            catch
            {
                return null;
            }
        }

        // Sprawdź płeć z PESEL
        public static string PobierzPłeć(string pesel)
        {
            if (!CzyPrawidłowy(pesel))
                return "Nieznana";

            int cyfraPłci = int.Parse(pesel[9].ToString());

            return cyfraPłci % 2 == 0 ? "Kobieta" : "Mężczyzna";
        }

        // Szczegółowy raport walidacji (do debugowania)
        public static string PobierzRaportWalidacji(string pesel)
        {
            if (string.IsNullOrWhiteSpace(pesel))
                return "❌ PESEL jest pusty";

            if (pesel.Length != 11)
                return $"❌ PESEL ma {pesel.Length} cyfr (wymagane 11)";

            foreach (char c in pesel)
            {
                if (!char.IsDigit(c))
                    return $"❌ PESEL zawiera nieprawidłowy znak: '{c}'";
            }

            // Oblicz sumę kontrolną
            int suma = 0;
            for (int i = 0; i < 10; i++)
            {
                int cyfra = int.Parse(pesel[i].ToString());
                suma += cyfra * Wagi[i];
            }

            int reszta = suma % 10;
            int cyfraKontrolna = (10 - reszta) % 10;
            int ostatniaCyfra = int.Parse(pesel[10].ToString());

            if (cyfraKontrolna != ostatniaCyfra)
            {
                return $"❌ Nieprawidłowa suma kontrolna (oczekiwano {cyfraKontrolna}, jest {ostatniaCyfra})";
            }

            var data = PobierzDateUrodzenia(pesel);
            var płeć = PobierzPłeć(pesel);

            return $"✅ PESEL prawidłowy\n   Data urodzenia: {data?.ToShortDateString()}\n   Płeć: {płeć}";
        }
    }
}
