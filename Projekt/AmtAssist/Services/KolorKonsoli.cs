namespace AmtAssist.Services
{
    public static class KolorKonsoli
    {
        // Sukces - zielony
        public static void Sukces(string tekst)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(tekst);
            Console.ResetColor();
        }

        // Błąd - czerwony
        public static void Błąd(string tekst)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(tekst);
            Console.ResetColor();
        }

        // Ostrzeżenie - żółty
        public static void Ostrzeżenie(string tekst)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(tekst);
            Console.ResetColor();
        }

        // Informacja - niebieski
        public static void Info(string tekst)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(tekst);
            Console.ResetColor();
        }

        // Nagłówek - biały na niebieskim
        public static void Nagłówek(string tekst)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(tekst);
            Console.ResetColor();
        }

        // Pilne - czerwony pogrubiony
        public static void BardzoPilne(string tekst)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(tekst);
            Console.ResetColor();
        }

        // Mniej ważne - szary
        public static void Drugoplanowe(string tekst)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(tekst);
            Console.ResetColor();
        }

        // Custom kolor
        public static void Kolor(string tekst, ConsoleColor kolor)
        {
            Console.ForegroundColor = kolor;
            Console.WriteLine(tekst);
            Console.ResetColor();
        }
    }
}