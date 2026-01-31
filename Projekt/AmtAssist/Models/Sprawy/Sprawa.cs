using AmtAssist.Models.Osoby;
using AmtAssist.Models.Enums;
using AmtAssist.Interfaces;

namespace AmtAssist.Models.Sprawy
{
    public abstract class Sprawa : ITerminowy, IEksportowalny
    {
        // Statyczne pole do generowania numerów spraw
        private static int _licznikSpraw = 1;
        public static void UstawLicznikSpraw(int wartość)
        {
            _licznikSpraw = wartość;
        }

        // Metoda do pobierania obecnego licznika
        public static int PobierzLicznikSpraw()
        {
            return _licznikSpraw;
        }

        // Właściwości
        public string NumerSprawy { get; private set; }
        public DateTime DataUtworzenia { get; private set; }
        public StatusSprawy Status { get; private set; }
        public Klient Klient { get; private set; }
        public DateTime? Termin { get; protected set; }


        protected Sprawa(Klient klient, string numerSprawy, DateTime dataUtworzenia, StatusSprawy status, DateTime? termin)
        {
            NumerSprawy = numerSprawy;
            DataUtworzenia = dataUtworzenia;
            Status = status;
            Klient = klient;
            Termin = termin;
        }
        // Konstruktor
        protected Sprawa(Klient klient, DateTime? termin = null)
        {
            NumerSprawy = $"SP-{_licznikSpraw:D4}"; // Format: SP-0001, SP-0002, ...
            _licznikSpraw++;

            DataUtworzenia = DateTime.Now;
            Status = StatusSprawy.Nowa;
            Klient = klient;
            Termin = termin;
        }

        // Metody do zarządzania statusem
        public void ZmieńStatus(StatusSprawy nowyStatus)
        {
            Status = nowyStatus;
            Console.WriteLine($"Status sprawy {NumerSprawy} zmieniony na: {nowyStatus}");
        }

        // Metody abstrakcyjne
        public abstract decimal ObliczOpłatę();
        public abstract bool SprawdźKompletność();
        public abstract List<string> PobierzWymaganeDokumenty();
        public abstract string PobierzNazwęSprawy();

        // Implementacja interfejsu ITerminowy
        public DateTime? PobierzTermin()
        {
            return Termin;
        }

        public bool CzyPilne()
        {
            if (Termin == null) return false;

            int dniDoTerminu = (Termin.Value - DateTime.Now).Days;
            return dniDoTerminu <= 7 && dniDoTerminu >= 0;
        }

        public int DniDoTerminu()
        {
            if (Termin == null) return -1;

            return (Termin.Value - DateTime.Now).Days;
        }

        // Implementacja interfejsu IEksportowalny
        public virtual string EksportujDoJSON()
        {
            return 
                $@"{{
                    ""NumerSprawy"": ""{NumerSprawy}"",
                    ""Nazwa"": ""{PobierzNazwęSprawy()}"",
                    ""Status"": ""{Status}"",
                    ""Klient"": ""{Klient.PełneImię()}"",
                    ""DataUtworzenia"": ""{DataUtworzenia:yyyy-MM-dd}"",
                    ""Termin"": ""{(Termin.HasValue ? Termin.Value.ToString("yyyy-MM-dd") : "brak")}"",
                    ""Opłata"": {ObliczOpłatę()}
                 }}";
        }

        // Metoda wyświetlająca podstawowe informacje
        public virtual void WyświetlInformacje()
        {
            Console.WriteLine($"\n--- {PobierzNazwęSprawy()} ---");
            Console.WriteLine($"Numer: {NumerSprawy}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Klient: {Klient.PełneImię()}");
            Console.WriteLine($"Data utworzenia: {DataUtworzenia.ToShortDateString()}");
            Console.WriteLine($"Termin: {(Termin.HasValue ? Termin.Value.ToShortDateString() : "brak")}");
            Console.WriteLine($"Opłata: {ObliczOpłatę()} zł");
            Console.WriteLine($"Kompletna: {(SprawdźKompletność() ? "TAK" : "NIE")}");
        }
    }
}