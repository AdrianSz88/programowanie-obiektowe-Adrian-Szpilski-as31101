using System.Text.Json;
using AmtAssist.Models.Osoby;
using AmtAssist.Models.Sprawy;
using AmtAssist.Models.Enums;

namespace AmtAssist.Services
{
    public class JsonService
    {
        private readonly string _ścieżkaDoPliku;

        // Konstruktor - ustawia ścieżkę do pliku JSON
        public JsonService(string nazwaPliku = "klienci.json")
        {
            // Plik będzie zapisywany w folderze Data
            string folderData = Path.Combine(Directory.GetCurrentDirectory(), "Data");

            // Utwórz folder Data jeśli nie istnieje
            if (!Directory.Exists(folderData))
            {
                Directory.CreateDirectory(folderData);
            }

            _ścieżkaDoPliku = Path.Combine(folderData, nazwaPliku);
        }

        // Zapisz listę klientów do pliku JSON
        public void ZapiszKlientów(List<Klient> klienci)
        {
            try
            {
                // Konwertuj klientów do formatu, który można zapisać
                var daneDoZapisu = klienci.Select(k => new
                {
                    Imię = k.Imię,
                    Nazwisko = k.Nazwisko,
                    Email = k.Email,
                    Telefon = k.Telefon,
                    PESEL = k.PESEL,
                    AdresPolska = k.AdresPolska,
                    AdresNiemcy = k.AdresNiemcy,
                    DataRejestracji = k.DataRejestracji,
                    // Dodatkowe pola dla KlientIndywidualny
                    TypKlienta = k.GetType().Name,
                    DataUrodzenia = (k as KlientIndywidualny)?.DataUrodzenia,
                    MiejsceUrodzenia = (k as KlientIndywidualny)?.MiejsceUrodzenia,
                    LiczbaSpraw = k.LiczbaSpraw(),
                    // NOWE: Zapisz sprawy klienta
                    Sprawy = k.PobierzSprawy().Select(s => new
                    {
                        NumerSprawy = s.NumerSprawy,
                        NazwaSprawy = s.PobierzNazwęSprawy(),
                        Status = s.Status.ToString(),
                        DataUtworzenia = s.DataUtworzenia,
                        Termin = s.Termin,
                        Opłata = s.ObliczOpłatę(),
                        TypSprawy = s.GetType().Name,

                        // Dane specyficzne dla WniosekKindergeld
                        LiczbaDzieci = (s as WniosekKindergeld)?.LiczbaDzieci,
                        ImionaDzieci = (s as WniosekKindergeld)?.ImionaDzieci,
                        CzyPierwszyWniosek = (s as WniosekKindergeld)?.CzyPierwszyWniosek,

                        // Dane specyficzne dla Zameldowanie
                        MiastoZameldowania = (s as Zameldowanie)?.MiastoZameldowania,
                        AdresZameldowania = (s as Zameldowanie)?.AdresZameldowania,
                        DataPrzeprowadzki = (s as Zameldowanie)?.DataPrzeprowadzki,
                        CzyRodzina = (s as Zameldowanie)?.CzyRodzina
                    }).ToList()
                });
                // Opcje serializacji - ładne formatowanie
                var opcje = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                // Serializuj do JSON
                string json = JsonSerializer.Serialize(daneDoZapisu, opcje);

                // Zapisz do pliku
                File.WriteAllText(_ścieżkaDoPliku, json);

                Console.WriteLine($"\n✓ Dane zostały zapisane do pliku: {_ścieżkaDoPliku}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Błąd podczas zapisu: {ex.Message}");
            }
        }

        // Wczytaj listę klientów z pliku JSON
        public List<KlientIndywidualny> WczytajKlientów()
        {
            var klienci = new List<KlientIndywidualny>();

            try
            {
                // Sprawdź czy plik istnieje
                if (!File.Exists(_ścieżkaDoPliku))
                {
                    Console.WriteLine("\n⚠ Plik z danymi nie istnieje. Rozpoczynam z pustą bazą.");
                    return klienci;
                }

                // Wczytaj zawartość pliku
                string json = File.ReadAllText(_ścieżkaDoPliku);

                // Deserializuj JSON do obiektów
                using (JsonDocument dokument = JsonDocument.Parse(json))
                {
                    var root = dokument.RootElement;

                    foreach (var element in root.EnumerateArray())
                    {
                        // Odczytaj dane z JSON
                        string imię = element.GetProperty("Imię").GetString() ?? "";
                        string nazwisko = element.GetProperty("Nazwisko").GetString() ?? "";
                        string email = element.GetProperty("Email").GetString() ?? "";
                        string telefon = element.GetProperty("Telefon").GetString() ?? "";
                        string pesel = element.GetProperty("PESEL").GetString() ?? "";
                        string adresPolska = element.GetProperty("AdresPolska").GetString() ?? "";
                        string adresNiemcy = element.GetProperty("AdresNiemcy").GetString() ?? "";

                        // Parsuj daty
                        DateTime dataUrodzenia = DateTime.Parse(
                            element.GetProperty("DataUrodzenia").GetString() ?? DateTime.Now.ToString()
                        );
                        string miejsceUrodzenia = element.GetProperty("MiejsceUrodzenia").GetString() ?? "";

                        // Utwórz klienta
                        var klient = new KlientIndywidualny(
                            imię, nazwisko, email, telefon, pesel,
                            adresPolska, adresNiemcy, dataUrodzenia, miejsceUrodzenia
                        );

                        
                        // NOWE: Wczytaj sprawy klienta
                        int maxNumerSprawy = 0; // Śledź najwyższy numer sprawy

                        if (element.TryGetProperty("Sprawy", out JsonElement sprawyElement))
                        {
                            foreach (var sprawaEl in sprawyElement.EnumerateArray())
                            {
                                // Odczytaj wspólne dane dla wszystkich typów spraw
                                string numerSprawy = sprawaEl.GetProperty("NumerSprawy").GetString() ?? "";
                                DateTime dataUtworzenia = sprawaEl.GetProperty("DataUtworzenia").GetDateTime();
                                string statusStr = sprawaEl.GetProperty("Status").GetString() ?? "Nowa";
                                StatusSprawy status = Enum.Parse<StatusSprawy>(statusStr);
                                string typSprawy = sprawaEl.GetProperty("TypSprawy").GetString() ?? "";

                                // Wyciągnij numer z formatu "SP-0001" -> 1
                                if (numerSprawy.StartsWith("SP-"))
                                {
                                    if (int.TryParse(numerSprawy.Substring(3), out int numer))
                                    {
                                        if (numer > maxNumerSprawy) maxNumerSprawy = numer;
                                    }
                                }

                                DateTime? termin = null;
                                if (sprawaEl.TryGetProperty("Termin", out JsonElement terminEl) &&
                                    terminEl.ValueKind != JsonValueKind.Null)
                                {
                                    termin = terminEl.GetDateTime();
                                }

                                if (typSprawy == "WniosekKindergeld")
                                {
                                    int liczbaDzieci = sprawaEl.GetProperty("LiczbaDzieci").GetInt32();
                                    bool czyPierwszy = sprawaEl.GetProperty("CzyPierwszyWniosek").GetBoolean();

                                    List<string> imionaDzieci = new List<string>();
                                    if (sprawaEl.TryGetProperty("ImionaDzieci", out JsonElement imionaEl))
                                    {
                                        foreach (var imieEl in imionaEl.EnumerateArray())
                                        {
                                            imionaDzieci.Add(imieEl.GetString() ?? "");
                                        }
                                    }

                                    var sprawa = new WniosekKindergeld(
                                        klient, numerSprawy, dataUtworzenia, status,
                                        liczbaDzieci, imionaDzieci, czyPierwszy, termin
                                    );

                                    klient.DodajSprawę(sprawa);
                                }
                                else if (typSprawy == "Zameldowanie")
                                {
                                    string miasto = sprawaEl.GetProperty("MiastoZameldowania").GetString() ?? "";
                                    string adres = sprawaEl.GetProperty("AdresZameldowania").GetString() ?? "";
                                    DateTime dataPrzeprowadzki = sprawaEl.GetProperty("DataPrzeprowadzki").GetDateTime();
                                    bool czyRodzina = sprawaEl.GetProperty("CzyRodzina").GetBoolean();

                                    var sprawa = new Zameldowanie(
                                        klient, numerSprawy, dataUtworzenia, status,
                                        miasto, adres, dataPrzeprowadzki, czyRodzina, termin
                                    );

                                    klient.DodajSprawę(sprawa);
                                }
                            }
                        }

                        // Ustaw licznik spraw na wartość większą niż najwyższy numer
                        if (maxNumerSprawy > 0)
                        {
                            Sprawa.UstawLicznikSpraw(maxNumerSprawy + 1);
                        }

                        
                        klienci.Add(klient);
                    }
                }

                //Console.WriteLine($"✓ Wczytano {klienci.Count} klientów z pliku.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Błąd podczas wczytywania: {ex.Message}");
            }

            return klienci;
        }

        // Sprawdź czy plik istnieje
        public bool CzyPlikIstnieje()
        {
            return File.Exists(_ścieżkaDoPliku);
        }

        // Pobierz ścieżkę do pliku
        public string PobierzŚcieżkę()
        {
            return _ścieżkaDoPliku;
        }
    }
}