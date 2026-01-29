using AmtAssist.Services;
using AmtAssist.Models.Osoby;
using AmtAssist.Models.Sprawy;
using AmtAssist.Models.Enums;

namespace AmtAssist
{
    class Program
    {
        //serwis do zarządzania klientami
        static KlientService klientService = new KlientService();
        static JsonService jsonService = new JsonService();

        static void Main(string[] args)
        {
            //ustawienie polskich znaków w konsoli
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("===========================================");
            Console.WriteLine("   Witaj w systemie AmtAssist!");
            Console.WriteLine("===========================================\n");

           
            WczytajDaneZPliku();

            //główna pętla programu
            bool działaj = true;
            while (działaj)
            {
                WyświetlMenu();
                string wybór = Console.ReadLine() ?? "";

                Console.WriteLine();

          
                switch (wybór)
                {
                    case "1":
                        DodajNowegoKlienta();
                        break;
                    case "2":
                        klientService.WyświetlListęKlientów();
                        break;
                    case "3":
                        WyszukajKlienta();
                        break;
                    case "4":
                        EdytujDaneKlienta();
                        break;                                       
                    case "5":
                        DodajSprawęDoKlienta();
                        break;
                    case "6":
                        ZmieńStatusSprawy();
                        break;
                    case "7":
                        ListaWszystkichSpraw();
                        break;
                    case "8":
                        RaportPilnychSpraw();
                        break;
                    case "9":
                        WyszukajSprawęPoNumerze();
                        break;
                    case "10":
                        WyświetlWymaganeDokumenty();
                        break;
                    case "11":
                        WyświetlStatystyki();
                        break;
                    case "12":
                        ZapiszDaneDoPliku();
                        break;
                    case "13":
                        WczytajDaneZPliku();
                        break;
                    case "0":
                        Console.WriteLine("\nCzy chcesz zapisać dane przed wyjściem (tak/nie): ");
                        string odpowiedź = Console.ReadLine()?.ToLower() ?? "";
                        if (odpowiedź == "tak" || odpowiedź == "t")
                        {
                            ZapiszDaneDoPliku();
                        }
                        Console.WriteLine("Do widzenia!");
                        działaj = false;
                        break;
                    default:
                        Console.WriteLine("✗ Nieprawidłowy wybór. Spróbuj ponownie.");
                        break;
                }

                if (działaj)
                {
                    Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
                    Console.ReadKey();
                    Console.Clear(); //czyszczenie EKRANU
                }
            }
        }

        static void WyświetlMenu()
        {
            Console.WriteLine("\n╔══════════════════════════════════════════╗");
            Console.WriteLine("║            MENU GŁÓWNE                   ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║         KLIENCI                          ║");
            Console.WriteLine("║  1. Dodaj nowego klienta                 ║");
            Console.WriteLine("║  2. Lista klientów                       ║");
            Console.WriteLine("║  3. Wyszukaj klienta                     ║");
            Console.WriteLine("║  4. Edytuj dane klienta                  ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║         SPRAWY                           ║");
            Console.WriteLine("║  5. Dodaj sprawę do klienta              ║");
            Console.WriteLine("║  6. Zmień status sprawy                  ║");
            Console.WriteLine("║  7. Lista wszystkich spraw               ║");
            Console.WriteLine("║  8. Pilne sprawy (deadline < 7 dni)      ║");
            Console.WriteLine("║  9. Wyszukaj sprawe po numerze           ║");
            Console.WriteLine("║  10. Wymagane dokumenty dla sprawy       ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║         SYSTEM                           ║");
            Console.WriteLine("║  11. Statystyki                          ║");
            Console.WriteLine("║  12. Zapisz dane do pliku                ║");
            Console.WriteLine("║  13. Wczytaj dane z pliku                ║");
            Console.WriteLine("║  0. Wyjście                              ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write("\nTwój wybór: ");
        }

        static void DodajNowegoKlienta()
        {
            Console.WriteLine("=== DODAWANIE NOWEGO KLIENTA ===\n");

            Console.Write("Imię: ");
            string imię = Console.ReadLine() ?? "";

            Console.Write("Nazwisko: ");
            string nazwisko = Console.ReadLine() ?? "";

            Console.Write("PESEL: ");
            string pesel = Console.ReadLine() ?? "";

            if (!WalidatorPESEL.CzyPrawidłowy(pesel))
            {
                KolorKonsoli.Błąd("\n❌ PESEL NIEPRAWIDŁOWY!");
                Console.WriteLine(WalidatorPESEL.PobierzRaportWalidacji(pesel));
                Console.Write("\nCzy mimo to dodać klienta? (tak/nie): ");
                string odpowiedź = Console.ReadLine()?.ToLower() ?? "";
                if (odpowiedź != "tak" && odpowiedź != "t")
                {
                    Console.WriteLine("Anulowano dodawanie klienta.");
                    return;
                }
            }
            else
            {
                KolorKonsoli.Sukces("✅ PESEL prawidłowy");
            }

            //czy klient już istnieje
            if (klientService.CzyKlientIstnieje(pesel))
            {
                Console.WriteLine("\nKlient o tym numerze PESEL już istnieje w systemie!");
                return;
            }

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Telefon: ");
            string telefon = Console.ReadLine() ?? "";

            Console.Write("Adres w Polsce: ");
            string adresPolska = Console.ReadLine() ?? "";

            Console.Write("Adres w Niemczech: ");
            string adresNiemcy = Console.ReadLine() ?? "";

            Console.Write("Data urodzenia (rrrr-mm-dd): ");
            string dataUrStr = Console.ReadLine() ?? "";
            DateTime dataUrodzenia;
            if (!DateTime.TryParse(dataUrStr, out dataUrodzenia))
            {
                Console.WriteLine("\nNieprawidłowy format daty!");
                return;
            }

            Console.Write("Miejsce urodzenia: ");
            string miejsceUrodzenia = Console.ReadLine() ?? "";

            //utwórz nowego klienta
            var nowyKlient = new KlientIndywidualny(
                imię, nazwisko, email, telefon, pesel,
                adresPolska, adresNiemcy, dataUrodzenia, miejsceUrodzenia
            );

            klientService.DodajKlienta(nowyKlient);
        }

        static void WyszukajKlienta()
        {
            Console.WriteLine("=== WYSZUKIWANIE KLIENTA ===\n");
            Console.WriteLine("Wybierz sposób wyszukiwania:");
            Console.WriteLine("1. Po PESEL");
            Console.WriteLine("2. Po nazwisku");
            Console.Write("\nTwój wybór: ");

            string wybór = Console.ReadLine() ?? "";

            if (wybór == "1")
            {
                //wyszukiwanie po PESEL
                Console.Write("\nPodaj PESEL klienta: ");
                string pesel = Console.ReadLine() ?? "";
                klientService.WyświetlSzczegółyKlienta(pesel);
            }
            else if (wybór == "2")
            {
                //wyszukiwanie po nazwisku
                Console.Write("\nPodaj nazwisko klienta: ");
                string nazwisko = Console.ReadLine() ?? "";

                var znalezieniKlienci = klientService.ZnajdźKlientówPoImieniu(nazwisko);

                if (znalezieniKlienci.Count == 0)
                {
                    Console.WriteLine($"\nNie znaleziono klienta o nazwisku: {nazwisko}");
                    return;
                }

                if (znalezieniKlienci.Count == 1)
                {
                    //jeśli jeden klient - pokaż od razu szczegóły
                    klientService.WyświetlSzczegółyKlienta(znalezieniKlienci[0].PESEL);
                }
                else
                {
                    //jeśli wielu klientów - pokaż listę do wyboru
                    Console.WriteLine($"\nZnaleziono {znalezieniKlienci.Count} klientów:");
                    for (int i = 0; i < znalezieniKlienci.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {znalezieniKlienci[i].PełneImię()} (PESEL: {znalezieniKlienci[i].PESEL})");
                    }

                    Console.Write("\nWybierz numer klienta, aby zobaczyć szczegóły (lub 0 aby anulować): ");
                    string wyborKlienta = Console.ReadLine() ?? "";

                    if (int.TryParse(wyborKlienta, out int numer) && numer > 0 && numer <= znalezieniKlienci.Count)
                    {
                        klientService.WyświetlSzczegółyKlienta(znalezieniKlienci[numer - 1].PESEL);
                    }
                }
            }
            else
            {
                Console.WriteLine("\n✗ Nieprawidłowy wybór.");
            }
        }


        static void DodajSprawęDoKlienta()
        {
            Console.WriteLine("=== DODAWANIE SPRAWY ===\n");
            Console.WriteLine("Wybierz sposób wyszukiwania klienta:");
            Console.WriteLine("1. Po PESEL");
            Console.WriteLine("2. Po nazwisku");
            Console.Write("\nTwój wybór: ");

            string wybór = Console.ReadLine() ?? "";
            Klient? klient = null;

            if (wybór == "1")
            {
                //wyszukiwanie po PESEL
                Console.Write("\nPodaj PESEL klienta: ");
                string pesel = Console.ReadLine() ?? "";
                klient = klientService.ZnajdźKlientaPoPESEL(pesel);

                if (klient == null)
                {
                    Console.WriteLine($"\nNie znaleziono klienta o PESEL: {pesel}");
                    return;
                }
            }
            else if (wybór == "2")
            {
                //wyszukiwanie po nazwisku
                Console.Write("\nPodaj nazwisko klienta: ");
                string nazwisko = Console.ReadLine() ?? "";

                var znalezieniKlienci = klientService.ZnajdźKlientówPoImieniu(nazwisko);

                if (znalezieniKlienci.Count == 0)
                {
                    Console.WriteLine($"\nNie znaleziono klienta o nazwisku: {nazwisko}");
                    return;
                }

                if (znalezieniKlienci.Count == 1)
                {
                    //jeśli jeden klient - wybierz automatycznie
                    klient = znalezieniKlienci[0];
                }
                else
                {
                    //jeśli wielu klientów - pokaż listę do wyboru
                    Console.WriteLine($"\nZnaleziono {znalezieniKlienci.Count} klientów:");
                    for (int i = 0; i < znalezieniKlienci.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {znalezieniKlienci[i].PełneImię()} (PESEL: {znalezieniKlienci[i].PESEL})");
                    }

                    Console.Write("\nWybierz numer klienta (lub 0 aby anulować): ");
                    string wyborKlienta = Console.ReadLine() ?? "";

                    if (int.TryParse(wyborKlienta, out int numer) && numer > 0 && numer <= znalezieniKlienci.Count)
                    {
                        klient = znalezieniKlienci[numer - 1];
                    }
                    else
                    {
                        Console.WriteLine("\n✗ Anulowano dodawanie sprawy.");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("\nNieprawidłowy wybór.");
                return;
            }

            //jeśli klient został znaleziony, dodaj sprawę
            if (klient != null)
            {
                Console.WriteLine($"\nKlient: {klient.PełneImię()}");
                Console.WriteLine("\nDostępne typy spraw:");
                Console.WriteLine("1. Wniosek Kindergeld");
                Console.WriteLine("2. Zameldowanie (Anmeldung)");
                Console.Write("\nWybierz typ sprawy: ");
                string typSprawy = Console.ReadLine() ?? "";

                if (typSprawy == "1")
                {
                    Console.Write("Liczba dzieci: ");
                    int liczbaDzieci = int.Parse(Console.ReadLine() ?? "0");

                    List<string> imionaDzieci = new List<string>();
                    for (int i = 1; i <= liczbaDzieci; i++)
                    {
                        Console.Write($"Imię dziecka {i}: ");
                        imionaDzieci.Add(Console.ReadLine() ?? "");
                    }

                    Console.Write("Czy to pierwszy wniosek? (tak/nie): ");
                    bool czyPierwszy = Console.ReadLine()?.ToLower() == "tak";

                    Console.Write("Deadline od urzędu (rrrr-mm-dd) lub Enter jeśli brak: ");
                    string terminStr = Console.ReadLine() ?? "";
                    DateTime? termin = null;
                    if (!string.IsNullOrWhiteSpace(terminStr))
                    {
                        DateTime terminParsed;
                        if (DateTime.TryParse(terminStr, out terminParsed))
                            termin = terminParsed;
                    }

                    var sprawa = new WniosekKindergeld(klient, liczbaDzieci, imionaDzieci, czyPierwszy, termin);
                    klient.DodajSprawę(sprawa);

                    Console.WriteLine($"\n✓ Sprawa {sprawa.NumerSprawy} została dodana!");
                    Console.WriteLine($"Opłata za usługę: {sprawa.ObliczOpłatę()} zł");
                }
                else if (typSprawy == "2")
                {
                    //zameldowanie
                    Console.Write("Miasto zameldowania: ");
                    string miasto = Console.ReadLine() ?? "";

                    Console.Write("Adres zameldowania: ");
                    string adres = Console.ReadLine() ?? "";

                    Console.Write("Data przeprowadzki (rrrr-mm-dd): ");
                    string dataPrzepStr = Console.ReadLine() ?? "";
                    DateTime dataPrzeprowadzki;
                    if (!DateTime.TryParse(dataPrzepStr, out dataPrzeprowadzki))
                    {
                        Console.WriteLine("\n✗ Nieprawidłowy format daty!");
                        return;
                    }

                    Console.Write("Czy zameldowanie z rodziną? (tak/nie): ");
                    bool czyRodzina = Console.ReadLine()?.ToLower() == "tak";

                    //automatyczne obliczenie deadline - 14 dni od przeprowadzki
                    DateTime termin = dataPrzeprowadzki.AddDays(14);
                    Console.WriteLine($"\n✓ Ustawowy termin zameldowania: {termin.ToShortDateString()} (14 dni od przeprowadzki)");

                    //sprawdź czy klient ma już inne zameldowania
                    var istniejąceZameldowania = klient.PobierzSprawy()
                        .Where(s => s is Zameldowanie)
                        .ToList();

                    //jeśli ma poprzednie zameldowania, oznacz je jako zakończone
                    if (istniejąceZameldowania.Count > 0)
                    {
                        Console.WriteLine($"\n⚠ Znaleziono {istniejąceZameldowania.Count} poprzednie zameldowanie(a).");
                        Console.WriteLine("Oznaczam poprzednie zameldowania jako zakończone...");

                        foreach (var stareZameldowanie in istniejąceZameldowania)
                        {
                            stareZameldowanie.ZmieńStatus(Models.Enums.StatusSprawy.Zakończona);
                            Console.WriteLine($"  • {stareZameldowanie.NumerSprawy} → Zakończona");
                        }
                    }

                    //utwórz nową sprawę zameldowania
                    var sprawa = new Zameldowanie(klient, miasto, adres, dataPrzeprowadzki, czyRodzina, termin);
                    klient.DodajSprawę(sprawa);

                    //aktualizuj adres klienta w Niemczech
                    string pełnyAdres = $"{adres}, {miasto}";
                    klient.AdresNiemcy = pełnyAdres;
                    Console.WriteLine($"\n✓ Zaktualizowano adres klienta: {pełnyAdres}");

                    Console.WriteLine($"\n✓ Sprawa {sprawa.NumerSprawy} została dodana!");
                    Console.WriteLine($"Opłata za usługę: {sprawa.ObliczOpłatę()} zł");
                    Console.WriteLine($"\n{sprawa.PobierzOstrzeżenieOTerminie()}");
                }


            }
        }


        static void WyświetlStatystyki()
        {
            Console.WriteLine("=== STATYSTYKI SYSTEMU ===\n");
            Console.WriteLine($"Liczba klientów w systemie: {klientService.LiczbaKlientów()}");

            var klienci = klientService.PobierzWszystkichKlientów();
            int łącznaLiczbaSpraw = 0;
            decimal łącznaDochód = 0;

            foreach (var klient in klienci)
            {
                var sprawy = klient.PobierzSprawy();
                łącznaLiczbaSpraw += sprawy.Count;

                foreach (var sprawa in sprawy)
                {
                    łącznaDochód += sprawa.ObliczOpłatę();
                }
            }

            Console.WriteLine($"Łączna liczba spraw: {łącznaLiczbaSpraw}");
            Console.WriteLine($"Łączny potencjalny dochód: {łącznaDochód} zł");
        }

        static void ZapiszDaneDoPliku()
        {
            Console.WriteLine("+++ Zapisywanie DANYCH +++");
            var klienci = klientService.PobierzWszystkichKlientów();

            if (klienci.Count == 0)
            {
                Console.WriteLine("!!!Brak klientów do zapisania.!!!");
                return;
            }

            jsonService.ZapiszKlientów(klienci);
            Console.WriteLine($"Zapisano {klienci.Count} klientów do pliku.");
        }

        static void WczytajDaneZPliku()
        {
            Console.WriteLine("+++ Wczytywanie DANYCH +++");

            if (!jsonService.CzyPlikIstnieje())
            {
                Console.WriteLine("!!!Plik z danymi nie istnieje.!!!");
                return;
            }
            var wczytaniKlienci = jsonService.WczytajKlientów();

            if (wczytaniKlienci.Count == 0)
            {
                Console.WriteLine("!!!Nie udało się wczytaj danych!!!");
                return;
            }
            if (klientService.LiczbaKlientów() > 0)
            {
                Console.Write($"\n⚠ W systemie jest już {klientService.LiczbaKlientów()} klientów. Czy zastąpić ich danymi z pliku? (tak/nie): ");
                string odpowiedź = Console.ReadLine()?.ToLower() ?? "";
                if (odpowiedź != "tak" && odpowiedź != "t")
                {
                    Console.WriteLine("Anulowano wczytywanie.");
                    return;
                }
            }

            var wszyscyKlienci = klientService.PobierzWszystkichKlientów();
            wszyscyKlienci.Clear();

            foreach (var klient in wczytaniKlienci)
            {

                wszyscyKlienci.Add(klient);
            }
            Console.WriteLine($"Wczytano {wczytaniKlienci.Count} klientów z pliku.");


        }
        static void ZmieńStatusSprawy()
        {
            Console.WriteLine("=== ZMIANA STATUSU SPRAWY ===\n");

            //krok 1: Znajdź klienta
            Console.WriteLine("Wybierz sposób wyszukiwania klienta:");
            Console.WriteLine("1. Po PESEL");
            Console.WriteLine("2. Po nazwisku");
            Console.Write("\nTwój wybór: ");

            string wybór = Console.ReadLine() ?? "";
            Klient? klient = null;

            if (wybór == "1")
            {
                Console.Write("\nPodaj PESEL klienta: ");
                string pesel = Console.ReadLine() ?? "";
                klient = klientService.ZnajdźKlientaPoPESEL(pesel);

                if (klient == null)
                {
                    Console.WriteLine($"\n✗ Nie znaleziono klienta o PESEL: {pesel}");
                    return;
                }
            }
            else if (wybór == "2")
            {
                Console.Write("\nPodaj nazwisko klienta: ");
                string nazwisko = Console.ReadLine() ?? "";

                var znalezieniKlienci = klientService.ZnajdźKlientówPoImieniu(nazwisko);

                if (znalezieniKlienci.Count == 0)
                {
                    Console.WriteLine($"\n✗ Nie znaleziono klienta o nazwisku: {nazwisko}");
                    return;
                }

                if (znalezieniKlienci.Count == 1)
                {
                    klient = znalezieniKlienci[0];
                }
                else
                {
                    Console.WriteLine($"\nZnaleziono {znalezieniKlienci.Count} klientów:");
                    for (int i = 0; i < znalezieniKlienci.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {znalezieniKlienci[i].PełneImię()} (PESEL: {znalezieniKlienci[i].PESEL})");
                    }

                    Console.Write("\nWybierz numer klienta (lub 0 aby anulować): ");
                    string wyborKlienta = Console.ReadLine() ?? "";

                    if (int.TryParse(wyborKlienta, out int numer) && numer > 0 && numer <= znalezieniKlienci.Count)
                    {
                        klient = znalezieniKlienci[numer - 1];
                    }
                    else
                    {
                        Console.WriteLine("\n✗ Anulowano.");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("\n✗ Nieprawidłowy wybór.");
                return;
            }

            //krok 2: Pokaż sprawy klienta
            if (klient != null)
            {
                var sprawy = klient.PobierzSprawy();

                if (sprawy.Count == 0)
                {
                    Console.WriteLine($"\n⚠ Klient {klient.PełneImię()} nie ma żadnych spraw.");
                    return;
                }

                Console.WriteLine($"\n--- SPRAWY KLIENTA: {klient.PełneImię()} ---");
                for (int i = 0; i < sprawy.Count; i++)
                {
                    var s = sprawy[i];
                    string pilne = s.CzyPilne() ? " ⚠ PILNE!" : "";
                    Console.WriteLine($"{i + 1}. {s.NumerSprawy} - {s.PobierzNazwęSprawy()}");
                    Console.WriteLine($"   Status: {s.Status}{pilne}");
                    if (s.Termin != null)
                    {
                        Console.WriteLine($"   Termin: {s.Termin.Value.ToShortDateString()} (za {s.DniDoTerminu()} dni)");
                    }
                    Console.WriteLine();
                }

                //krok 3: Wybierz sprawę
                Console.Write("Wybierz numer sprawy (lub 0 aby anulować): ");
                string wyborSprawy = Console.ReadLine() ?? "";

                if (!int.TryParse(wyborSprawy, out int numerSprawy) || numerSprawy < 1 || numerSprawy > sprawy.Count)
                {
                    Console.WriteLine("\n✗ Nieprawidłowy wybór.");
                    return;
                }

                var wybranaSprawy = sprawy[numerSprawy - 1];

                //krok 4: Wybierz nowy status
                Console.WriteLine($"\n--- ZMIANA STATUSU: {wybranaSprawy.NumerSprawy} ---");
                Console.WriteLine($"Obecny status: {wybranaSprawy.Status}");
                Console.WriteLine("\nDostępne statusy:");
                Console.WriteLine("1. Nowa");
                Console.WriteLine("2. W trakcie");
                Console.WriteLine("3. Oczekuje na dokumenty");
                Console.WriteLine("4. Wysłana");
                Console.WriteLine("5. Rozpatrywana");
                Console.WriteLine("6. Zakończona");
                Console.WriteLine("7. Odrzucona");

                Console.Write("\nWybierz nowy status: ");
                string wyborStatusu = Console.ReadLine() ?? "";

                StatusSprawy nowyStatus;
                switch (wyborStatusu)
                {
                    case "1": nowyStatus = StatusSprawy.Nowa; break;
                    case "2": nowyStatus = StatusSprawy.WTrakcie; break;
                    case "3": nowyStatus = StatusSprawy.OczekujeNaDokumenty; break;
                    case "4": nowyStatus = StatusSprawy.Wysłana; break;
                    case "5": nowyStatus = StatusSprawy.Rozpatrywana; break;
                    case "6": nowyStatus = StatusSprawy.Zakończona; break;
                    case "7": nowyStatus = StatusSprawy.Odrzucona; break;
                    default:
                        Console.WriteLine("\n✗ Nieprawidłowy wybór.");
                        return;
                }

                
                //zmień status
                wybranaSprawy.ZmieńStatus(nowyStatus);
                KolorKonsoli.Sukces($"\n✓ Status sprawy {wybranaSprawy.NumerSprawy} został zmieniony na: {nowyStatus}");

            }
        }
        static void EdytujDaneKlienta()
        {
            Console.WriteLine("=== EDYCJA DANYCH KLIENTA ===\n");

            //krok 1: Znajdź klienta
            Console.WriteLine("Wybierz sposób wyszukiwania klienta:");
            Console.WriteLine("1. Po PESEL");
            Console.WriteLine("2. Po nazwisku");
            Console.Write("\nTwój wybór: ");

            string wybór = Console.ReadLine() ?? "";
            Klient? klient = null;

            if (wybór == "1")
            {
                Console.Write("\nPodaj PESEL klienta: ");
                string pesel = Console.ReadLine() ?? "";
                klient = klientService.ZnajdźKlientaPoPESEL(pesel);

                if (klient == null)
                {
                    Console.WriteLine($"\n✗ Nie znaleziono klienta o PESEL: {pesel}");
                    return;
                }
            }
            else if (wybór == "2")
            {
                Console.Write("\nPodaj nazwisko klienta: ");
                string nazwisko = Console.ReadLine() ?? "";

                var znalezieniKlienci = klientService.ZnajdźKlientówPoImieniu(nazwisko);

                if (znalezieniKlienci.Count == 0)
                {
                    Console.WriteLine($"\n✗ Nie znaleziono klienta o nazwisku: {nazwisko}");
                    return;
                }

                if (znalezieniKlienci.Count == 1)
                {
                    klient = znalezieniKlienci[0];
                }
                else
                {
                    Console.WriteLine($"\nZnaleziono {znalezieniKlienci.Count} klientów:");
                    for (int i = 0; i < znalezieniKlienci.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {znalezieniKlienci[i].PełneImię()} (PESEL: {znalezieniKlienci[i].PESEL})");
                    }

                    Console.Write("\nWybierz numer klienta (lub 0 aby anulować): ");
                    string wyborKlienta = Console.ReadLine() ?? "";

                    if (int.TryParse(wyborKlienta, out int numer) && numer > 0 && numer <= znalezieniKlienci.Count)
                    {
                        klient = znalezieniKlienci[numer - 1];
                    }
                    else
                    {
                        Console.WriteLine("\n✗ Anulowano.");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("\n✗ Nieprawidłowy wybór.");
                return;
            }

            //krok 2: Wyświetl obecne dane i pozwól edytować
            if (klient != null)
            {
                Console.WriteLine($"\n--- EDYCJA DANYCH: {klient.PełneImię()} ---");
                Console.WriteLine("\nObecne dane:");
                Console.WriteLine($"1. Imię: {klient.Imię}");
                Console.WriteLine($"2. Nazwisko: {klient.Nazwisko}");
                Console.WriteLine($"3. Email: {klient.Email}");
                Console.WriteLine($"4. Telefon: {klient.Telefon}");
                Console.WriteLine($"5. Adres w Polsce: {klient.AdresPolska}");
                Console.WriteLine($"   (PESEL i adres w Niemczech nie można edytować)");

                Console.WriteLine("\nCo chcesz zmienić?");
                Console.WriteLine("1. Imię");
                Console.WriteLine("2. Nazwisko");
                Console.WriteLine("3. Email");
                Console.WriteLine("4. Telefon");
                Console.WriteLine("5. Adres w Polsce");
                Console.WriteLine("6. Wszystko");
                Console.WriteLine("0. Anuluj");

                Console.Write("\nTwój wybór: ");
                string wyborEdycji = Console.ReadLine() ?? "";

                bool zmieniono = false;

                switch (wyborEdycji)
                {
                    case "1":
                        Console.Write($"Nowe imię (obecne: {klient.Imię}): ");
                        string noweImię = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(noweImię))
                        {
                            klient.Imię = noweImię;
                            Console.WriteLine("✓ Imię zaktualizowane.");
                            zmieniono = true;
                        }
                        break;

                    case "2":
                        Console.Write($"Nowe nazwisko (obecne: {klient.Nazwisko}): ");
                        string noweNazwisko = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(noweNazwisko))
                        {
                            klient.Nazwisko = noweNazwisko;
                            Console.WriteLine("✓ Nazwisko zaktualizowane.");
                            zmieniono = true;
                        }
                        break;

                    case "3":
                        Console.Write($"Nowy email (obecny: {klient.Email}): ");
                        string nowyEmail = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(nowyEmail))
                        {
                            klient.Email = nowyEmail;
                            Console.WriteLine("✓ Email zaktualizowany.");
                            zmieniono = true;
                        }
                        break;

                    case "4":
                        Console.Write($"Nowy telefon (obecny: {klient.Telefon}): ");
                        string nowyTelefon = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(nowyTelefon))
                        {
                            klient.Telefon = nowyTelefon;
                            Console.WriteLine("✓ Telefon zaktualizowany.");
                            zmieniono = true;
                        }
                        break;

                    case "5":
                        Console.Write($"Nowy adres w Polsce (obecny: {klient.AdresPolska}): ");
                        string nowyAdres = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(nowyAdres))
                        {
                            klient.AdresPolska = nowyAdres;
                            Console.WriteLine("✓ Adres zaktualizowany.");
                            zmieniono = true;
                        }
                        break;

                    case "6":
                        //edytuj wszystko
                        Console.Write($"Nowe imię (obecne: {klient.Imię}): ");
                        string imię = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(imię)) klient.Imię = imię;

                        Console.Write($"Nowe nazwisko (obecne: {klient.Nazwisko}): ");
                        string nazwisko = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(nazwisko)) klient.Nazwisko = nazwisko;

                        Console.Write($"Nowy email (obecny: {klient.Email}): ");
                        string email = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(email)) klient.Email = email;

                        Console.Write($"Nowy telefon (obecny: {klient.Telefon}): ");
                        string telefon = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(telefon)) klient.Telefon = telefon;

                        Console.Write($"Nowy adres w Polsce (obecny: {klient.AdresPolska}): ");
                        string adres = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(adres)) klient.AdresPolska = adres;

                        Console.WriteLine("\n✓ Wszystkie dane zaktualizowane.");
                        zmieniono = true;
                        break;

                    case "0":
                        Console.WriteLine("\n✗ Anulowano edycję.");
                        return;

                    default:
                        Console.WriteLine("\n✗ Nieprawidłowy wybór.");
                        return;
                }

                if (zmieniono)
                {
                    KolorKonsoli.Sukces($"\n✓ Dane klienta {klient.PełneImię()} zostały zaktualizowane.");
                    KolorKonsoli.Ostrzeżenie("⚠ Pamiętaj o zapisaniu danych (opcja 6)!");
                }
            }
        }

        static void RaportPilnychSpraw()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              RAPORT PILNYCH SPRAW (< 7 DNI)                  ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            var wszyscyKlienci = klientService.PobierzWszystkichKlientów();

            //zbierz wszystkie sprawy ze wszystkich klientów
            var wszystkieSprawy = new List<Sprawa>();
            foreach (var klient in wszyscyKlienci)
            {
                wszystkieSprawy.AddRange(klient.PobierzSprawy());
            }

            //filtruj tylko pilne sprawy (używamy interfejsu ITerminowy)
            var pilneSprawy = wszystkieSprawy
                .Where(s => s.CzyPilne())  // Metoda z interfejsu ITerminowy
                .OrderBy(s => s.DniDoTerminu())  // Sortuj od najbliższych
                .ToList();

            if (pilneSprawy.Count == 0)
            {
                KolorKonsoli.Sukces("✓ Brak pilnych spraw! Wszystko pod kontrolą.");
                return;
            }

            KolorKonsoli.Ostrzeżenie($"\n⚠ Znaleziono {pilneSprawy.Count} pilnych spraw:\n");

            int nr = 1; 
            foreach (var sprawa in pilneSprawy)  
            {
                int dniDoTerminu = sprawa.DniDoTerminu();

                if (dniDoTerminu <= 2)
                {
                    KolorKonsoli.BardzoPilne($"{nr}. 🔴 BARDZO PILNE!");
                }
                else
                {
                    KolorKonsoli.Ostrzeżenie($"{nr}. ⚠ PILNE");
                }

                Console.WriteLine($"   Numer sprawy: {sprawa.NumerSprawy}");
                Console.WriteLine($"   Typ: {sprawa.PobierzNazwęSprawy()}");
                Console.WriteLine($"   Klient: {sprawa.Klient.PełneImię()}");
                Console.WriteLine($"   Status: {sprawa.Status}");
                Console.WriteLine($"   Termin: {sprawa.PobierzTermin()?.ToShortDateString()}");
                Console.WriteLine($"   Zostało dni: {dniDoTerminu}");
                Console.WriteLine($"   Opłata: {sprawa.ObliczOpłatę()} zł");
                Console.WriteLine();
                nr++;
            }

            //podsumowanie
            decimal łącznaWartość = pilneSprawy.Sum(s => s.ObliczOpłatę());
            Console.WriteLine("─────────────────────────────────────────────────────────────");
            Console.WriteLine($"Łączna wartość pilnych spraw: {łącznaWartość} zł");


        }
        static void ListaWszystkichSpraw()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  LISTA WSZYSTKICH SPRAW                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            var wszyscyKlienci = klientService.PobierzWszystkichKlientów();

            //zbierz wszystkie sprawy ze wszystkich klientów
            var wszystkieSprawy = new List<Sprawa>();
            foreach (var klient in wszyscyKlienci)
            {
                wszystkieSprawy.AddRange(klient.PobierzSprawy());
            }

            if (wszystkieSprawy.Count == 0)
            {
                Console.WriteLine("⚠ Brak spraw w systemie.");
                return;
            }

            Console.WriteLine($"Znaleziono {wszystkieSprawy.Count} spraw.\n");

            //opcje filtrowania
            Console.WriteLine("Filtruj sprawy:");
            Console.WriteLine("1. Wszystkie");
            Console.WriteLine("2. Tylko aktywne (Nowa, W trakcie, Oczekuje na dokumenty)");
            Console.WriteLine("3. Tylko zakończone");
            Console.WriteLine("4. Według typu sprawy");
            Console.WriteLine("5. Według statusu");
            Console.Write("\nTwój wybór: ");

            string wybór = Console.ReadLine() ?? "";
            List<Sprawa> sprawDoWyświetlenia = wszystkieSprawy;

            switch (wybór)
            {
                case "1":
                    //wszystkie - już mamy
                    break;

                case "2":
                    //tylko aktywne
                    sprawDoWyświetlenia = wszystkieSprawy
                        .Where(s => s.Status != StatusSprawy.Zakończona && s.Status != StatusSprawy.Odrzucona)
                        .ToList();
                    Console.WriteLine($"\nFiltruję tylko aktywne sprawy...");
                    break;

                case "3":
                    //tylko zakończone
                    sprawDoWyświetlenia = wszystkieSprawy
                        .Where(s => s.Status == StatusSprawy.Zakończona || s.Status == StatusSprawy.Odrzucona)
                        .ToList();
                    Console.WriteLine($"\nFiltruję tylko zakończone sprawy...");
                    break;

                case "4":
                    //według typu
                    Console.WriteLine("\nWybierz typ sprawy:");
                    Console.WriteLine("1. Wniosek Kindergeld");
                    Console.WriteLine("2. Zameldowanie");
                    Console.Write("Twój wybór: ");
                    string typWybór = Console.ReadLine() ?? "";

                    if (typWybór == "1")
                    {
                        sprawDoWyświetlenia = wszystkieSprawy.Where(s => s is WniosekKindergeld).ToList();
                        Console.WriteLine($"\nFiltruję tylko wnioski Kindergeld...");
                    }
                    else if (typWybór == "2")
                    {
                        sprawDoWyświetlenia = wszystkieSprawy.Where(s => s is Zameldowanie).ToList();
                        Console.WriteLine($"\nFiltruję tylko zameldowania...");
                    }
                    break;

                case "5":
                    //według statusu
                    Console.WriteLine("\nWybierz status:");
                    Console.WriteLine("1. Nowa");
                    Console.WriteLine("2. W trakcie");
                    Console.WriteLine("3. Oczekuje na dokumenty");
                    Console.WriteLine("4. Wysłana");
                    Console.WriteLine("5. Rozpatrywana");
                    Console.WriteLine("6. Zakończona");
                    Console.WriteLine("7. Odrzucona");
                    Console.Write("Twój wybór: ");
                    string statusWybór = Console.ReadLine() ?? "";

                    StatusSprawy wybranyStatus = StatusSprawy.Nowa;
                    switch (statusWybór)
                    {
                        case "1": wybranyStatus = StatusSprawy.Nowa; break;
                        case "2": wybranyStatus = StatusSprawy.WTrakcie; break;
                        case "3": wybranyStatus = StatusSprawy.OczekujeNaDokumenty; break;
                        case "4": wybranyStatus = StatusSprawy.Wysłana; break;
                        case "5": wybranyStatus = StatusSprawy.Rozpatrywana; break;
                        case "6": wybranyStatus = StatusSprawy.Zakończona; break;
                        case "7": wybranyStatus = StatusSprawy.Odrzucona; break;
                        default:
                            Console.WriteLine("\n✗ Nieprawidłowy wybór.");
                            return;
                    }

                    sprawDoWyświetlenia = wszystkieSprawy.Where(s => s.Status == wybranyStatus).ToList();
                    Console.WriteLine($"\nFiltruję sprawy ze statusem: {wybranyStatus}...");
                    break;

                default:
                    Console.WriteLine("\n✗ Nieprawidłowy wybór.");
                    return;
            }

            if (sprawDoWyświetlenia.Count == 0)
            {
                Console.WriteLine("\n⚠ Brak spraw pasujących do wybranego filtru.");
                return;
            }

            //wyświetl sprawy
            Console.WriteLine($"\n═══════════════════════════════════════════════════════════════");
            Console.WriteLine($"Znaleziono {sprawDoWyświetlenia.Count} spraw:\n");

            int nr = 1;
            decimal łącznaWartość = 0;

            foreach (var sprawa in sprawDoWyświetlenia)
            {
                if (sprawa.DniDoTerminu() > 0 && sprawa.DniDoTerminu() <= 2)
                {
                    Console.Write($"{nr}. {sprawa.NumerSprawy} - {sprawa.PobierzNazwęSprawy()}");
                    KolorKonsoli.BardzoPilne(" 🔴 BARDZO PILNE!");
                }
                else if (sprawa.CzyPilne())
                {
                    Console.Write($"{nr}. {sprawa.NumerSprawy} - {sprawa.PobierzNazwęSprawy()}");
                    KolorKonsoli.Ostrzeżenie(" ⚠ PILNE!");
                }
                else
                {
                    Console.WriteLine($"{nr}. {sprawa.NumerSprawy} - {sprawa.PobierzNazwęSprawy()}");
                }

                Console.WriteLine($"   Klient: {sprawa.Klient.PełneImię()} (PESEL: {sprawa.Klient.PESEL})");
                Console.WriteLine($"   Status: {sprawa.Status}");
                Console.WriteLine($"   Data utworzenia: {sprawa.DataUtworzenia.ToShortDateString()}");

                if (sprawa.Termin != null)
                {
                    Console.WriteLine($"   Termin: {sprawa.Termin.Value.ToShortDateString()} (za {sprawa.DniDoTerminu()} dni)");
                }
                else
                {
                    Console.WriteLine($"   Termin: brak");
                }

                Console.WriteLine($"   Opłata: {sprawa.ObliczOpłatę()} zł");
                Console.WriteLine();

                łącznaWartość += sprawa.ObliczOpłatę();
                nr++;
            }

            //podsumowanie
            Console.WriteLine("─────────────────────────────────────────────────────────────");
            Console.WriteLine($"Łączna wartość wyświetlonych spraw: {łącznaWartość} zł");

            //statystyki według typu
            var kindergeld = sprawDoWyświetlenia.Count(s => s is WniosekKindergeld);
            var zameldowanie = sprawDoWyświetlenia.Count(s => s is Zameldowanie);

            Console.WriteLine($"\nPodział według typu:");
            Console.WriteLine($"  • Wnioski Kindergeld: {kindergeld}");
            Console.WriteLine($"  • Zameldowania: {zameldowanie}");
        }

        static void WyszukajSprawęPoNumerze()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              WYSZUKIWANIE SPRAWY PO NUMERZE                  ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            Console.Write("Podaj numer sprawy (np. SP-0001): ");
            string numerSprawy = Console.ReadLine()?.ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(numerSprawy))
            {
                KolorKonsoli.Błąd("\n✗ Nie podano numeru sprawy.");
                return;
            }

            //szukaj sprawy we wszystkich klientach
            var wszyscyKlienci = klientService.PobierzWszystkichKlientów();
            Sprawa? znalezionaSprawy = null;
            Klient? właścicielSprawy = null;

            foreach (var klient in wszyscyKlienci)
            {
                var sprawy = klient.PobierzSprawy();
                var sprawa = sprawy.FirstOrDefault(s => s.NumerSprawy.Equals(numerSprawy, StringComparison.OrdinalIgnoreCase));

                if (sprawa != null)
                {
                    znalezionaSprawy = sprawa;
                    właścicielSprawy = klient;
                    break;
                }
            }

            if (znalezionaSprawy == null)
            {
                KolorKonsoli.Błąd($"\n✗ Nie znaleziono sprawy o numerze: {numerSprawy}");
                return;
            }

            //wyświetl szczegóły sprawy
            Console.WriteLine();
            if (znalezionaSprawy.CzyPilne())
            {
                if (znalezionaSprawy.DniDoTerminu() <= 2)
                {
                    KolorKonsoli.BardzoPilne("🔴 SPRAWA BARDZO PILNA!");
                }
                else
                {
                    KolorKonsoli.Ostrzeżenie("⚠ SPRAWA PILNA!");
                }
            }
            else
            {
                KolorKonsoli.Sukces("✓ Sprawa nie jest pilna");
            }

            znalezionaSprawy.WyświetlInformacje();
            
            
            Console.WriteLine($"\n--- KLIENT ---");
            Console.WriteLine($"Imię i nazwisko: {właścicielSprawy?.PełneImię()}");
            Console.WriteLine($"PESEL: {właścicielSprawy?.PESEL}");
            Console.WriteLine($"Email: {właścicielSprawy?.Email}");
            Console.WriteLine($"Telefon: {właścicielSprawy?.Telefon}");
            
        }

        static void WyświetlWymaganeDokumenty()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║            WYMAGANE DOKUMENTY DLA SPRAWY                     ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            Console.Write("Podaj numer sprawy (np. SP-0001): ");
            string numerSprawy = Console.ReadLine()?.ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(numerSprawy))
            {
                KolorKonsoli.Błąd("\n✗ Nie podano numeru sprawy.");
                return;
            }

            //szukaj sprawy we wszystkich klientach
            var wszyscyKlienci = klientService.PobierzWszystkichKlientów();
            Sprawa? znalezionaSprawy = null;

            foreach (var klient in wszyscyKlienci)
            {
                var sprawy = klient.PobierzSprawy();
                var sprawa = sprawy.FirstOrDefault(s => s.NumerSprawy.Equals(numerSprawy, StringComparison.OrdinalIgnoreCase));

                if (sprawa != null)
                {
                    znalezionaSprawy = sprawa;
                    break;
                }
            }

            if (znalezionaSprawy == null)
            {
                KolorKonsoli.Błąd($"\n✗ Nie znaleziono sprawy o numerze: {numerSprawy}");
                return;
            }

            //wyświetl informacje o sprawie
            KolorKonsoli.Info($"\nSprawa: {znalezionaSprawy.NumerSprawy} - {znalezionaSprawy.PobierzNazwęSprawy()}");
            Console.WriteLine($"Klient: {znalezionaSprawy.Klient.PełneImię()}");
            Console.WriteLine($"Status: {znalezionaSprawy.Status}");

            //pobierz wymagane dokumenty
            var wymaganeDokumenty = znalezionaSprawy.PobierzWymaganeDokumenty();

            Console.WriteLine($"\n╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  LISTA WYMAGANYCH DOKUMENTÓW ({wymaganeDokumenty.Count})");
            Console.WriteLine($"╚══════════════════════════════════════════════════════════════╝\n");

            if (wymaganeDokumenty.Count == 0)
            {
                KolorKonsoli.Sukces("✓ Brak wymaganych dokumentów dla tej sprawy.");
                return;
            }

            //wyświetl checklistę
            for (int i = 0; i < wymaganeDokumenty.Count; i++)
            {
                Console.Write($"  [ ] {i + 1}. ");
                Console.WriteLine(wymaganeDokumenty[i]);
            }

            //sprawdź kompletność
            Console.WriteLine();
            if (znalezionaSprawy.SprawdźKompletność())
            {
                KolorKonsoli.Sukces("✅ Sprawa jest kompletna - wszystkie wymagane dane wprowadzone.");
            }
            else
            {
                KolorKonsoli.Ostrzeżenie("⚠ Sprawa NIE jest kompletna - brakuje wymaganych danych!");
            }

            //dodatkowe informacje dla Zameldowania
            if (znalezionaSprawy is Zameldowanie zameldowanie)
            {
                Console.WriteLine();
                var ostrzeżenie = zameldowanie.PobierzOstrzeżenieOTerminie();

                if (zameldowanie.CzyPrzekroczenoTerminUstawowy())
                {
                    KolorKonsoli.Błąd(ostrzeżenie);
                }
                else if (zameldowanie.DniDoTerminu() <= 3)
                {
                    KolorKonsoli.Ostrzeżenie(ostrzeżenie);
                }
                else
                {
                    KolorKonsoli.Sukces(ostrzeżenie);
                }
            }

            //podsumowanie
            Console.WriteLine($"\n─────────────────────────────────────────────────────────────");
            Console.WriteLine($"Opłata za obsługę sprawy: {znalezionaSprawy.ObliczOpłatę()} zł");
        }




    }
}