using System;

//namespace Zadania
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            //ZADANIE 1 <=================================================================================

//            string poprawneHaslo = "admin123";
//            string haslo = "";

//            while (haslo != poprawneHaslo)
//            {
//                Console.Write("Podaj hasło: ");
//                haslo = Console.ReadLine();

//                if (haslo != poprawneHaslo)
//                {
//                    Console.WriteLine("Niepoprawne hasło! Spróbuj ponownie.\n");
//                }
//            }

//            Console.WriteLine("Zalogowano pomyślnie!\n");



//            //ZADANIE 2 <=================================================================================
//            int liczba;

//            do
//            {
//                Console.Write("Podaj liczbę większą od zera: ");
//                bool sukces = int.TryParse(Console.ReadLine(), out liczba);

//                if (!sukces || liczba <= 0)
//                {
//                    Console.WriteLine(" Liczba musi być większa od zera!\n");
//                }

//            } while (liczba <= 0);

//            Console.WriteLine($"Świetnie! Podałeś liczbę: {liczba}");

//            Console.WriteLine("=== ZADANIE 3: Pętla foreach ===\n");


//            //ZADANIE 3 <=================================================================================
//            // Tablica z 5 nazwami miast
//            string[] miasta = { "Warszawa", "Kraków", "Gdańsk", "Wrocław", "Poznań" };

//            // Pętla foreach - przechodzi przez każde miasto
//            foreach (string miasto in miasta)
//            {
//                Console.WriteLine(miasto);
//            }
//        }
//    }
//}


//ZADANIE 4 <===============================================================================================

//class Osoba
//{

//    public string Imie;
//    public int Wiek;

//    public void PrzedstawSie()
//    {
//        Console.WriteLine($"Cześć, jestem {Imie} i mam {Wiek} lat.");
//    }
//}


//class Program
//{
//    static void Main()
//    {


//        Osoba osoba1 = new Osoba();
//        osoba1.Imie = "Jan";
//        osoba1.Wiek = 25;


//        Osoba osoba2 = new Osoba();
//        osoba2.Imie = "Anna";
//        osoba2.Wiek = 30;


//        Osoba osoba3 = new Osoba();
//        osoba3.Imie = "Piotr";
//        osoba3.Wiek = 22;


//        osoba1.PrzedstawSie();
//        osoba2.PrzedstawSie();
//        osoba3.PrzedstawSie();
//    }


//}

// ZADANIE 5 <===============================================================================================
//class KontoBankowe
//{

//    private double saldo;

//    public void Wplata(double kwota)
//    {
//        if (kwota > 0) 
//        {
//            saldo += kwota;
//            Console.WriteLine($"Wpłacono: {kwota} zł");
//        }
//        else
//        {
//            Console.WriteLine("Kwota musi być dodatnia!");
//        }
//    }


//    public void Wyplata(double kwota)
//    {

//        if (kwota <= 0)
//        {
//            Console.WriteLine("Kwota musi być dodatnia!");
//            return; 
//        }


//        if (kwota > saldo)
//        {
//            Console.WriteLine($"Brak środków! Dostępne: {saldo} zł");
//            return;  
//        }


//        saldo -= kwota;
//        Console.WriteLine($"Wypłacono: {kwota} zł");
//    }


//    public double PobierzSaldo()
//    {
//        return saldo;
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        Console.WriteLine("=== SYSTEM BANKOWY ===\n");


//        KontoBankowe konto = new KontoBankowe();


//        Console.WriteLine($"Saldo początkowe: {konto.PobierzSaldo()} zł\n");


//        konto.Wplata(1000);
//        Console.WriteLine($"Aktualne saldo: {konto.PobierzSaldo()} zł\n");

//        konto.Wyplata(500);
//        Console.WriteLine($"Aktualne saldo: {konto.PobierzSaldo()} zł\n");


//        konto.Wyplata(700);
//        Console.WriteLine($"Aktualne saldo: {konto.PobierzSaldo()} zł\n");


//        konto.Wyplata(-100);
//    }
//}


// ZADANIE 6 <===============================================================================================
//class Zwierze
//{

//    public void Jedz()
//    {
//        Console.WriteLine("Zwierzę je");
//    }
//}

////KLASA POCHODNA 1: PIES 
//class Pies : Zwierze  
//{

//    public void Szczekaj()
//    {
//        Console.WriteLine("Hau hau!");
//    }
//}

////KLASA POCHODNA 2: KOT
//class Kot : Zwierze  
//{

//    public void Miaucz()
//    {
//        Console.WriteLine("Miau!");
//    }
//}


//class Program
//{
//    static void Main()
//    {


//        // Tworzę psa
//        Pies pies = new Pies();
//        Console.WriteLine("--- PIES ---");
//        pies.Jedz();        // Metoda z klasy bazowej Zwierze
//        pies.Szczekaj();    // Metoda z klasy Pies

//        Console.WriteLine();

//        // Tworzę kota
//        Kot kot = new Kot();
//        Console.WriteLine("--- KOT ---");
//        kot.Jedz();         // Metoda z klasy bazowej Zwierze
//        kot.Miaucz();       // Metoda z klasy Kot
//    }
//}

// ZADANIE 7 <===============================================================================================
//class Zwierze
//{
//    public virtual void DajGlos()
//    {
//        Console.WriteLine("Zwierzę wydaje dźwięk");
//    }
//}

//// KLASY POCHODNE
//class Pies : Zwierze
//{
//    public override void DajGlos()
//    {
//        Console.WriteLine("Hau hau!");
//    }
//}

//class Kot : Zwierze
//{
//    public override void DajGlos()
//    {
//        Console.WriteLine("Miau!");
//    }
//}

//class Krowa : Zwierze
//{
//    public override void DajGlos()
//    {
//        Console.WriteLine("Muuu!");
//    }
//}

//class Owca : Zwierze
//{
//    public override void DajGlos()
//    {
//        Console.WriteLine("Beeee!");
//    }
//}


//class Program
//{
//    static void Main()
//    {


//        Zwierze[] zwierzeta = new Zwierze[]
//        {
//            new Pies(),
//            new Kot(),
//            new Krowa(),
//            new Pies(),
//            new Owca()
//        };

//        // Pętla foreach
//        foreach (Zwierze zwierze in zwierzeta)
//        {
//            zwierze.DajGlos();
//        }
//    }
//}

// ZADANIE 8 <===============================================================================================

using System;

class Pojazd
{
    public virtual void Start()
    {
        Console.WriteLine("Pojazd uruchomiony");
    }
}

class Samochod : Pojazd  
{
 
    public void Jedz()
    {
        Console.WriteLine("Samochód jedzie");
    }
}

class ElektrycznySamochod : Samochod  
{
  
    public void Laduj()
    {
        Console.WriteLine("Ładowanie baterii...");
    }
}


class Program
{
    static void Main()
    {
     

        
        //Console.WriteLine("TEST 1: POJAZD);
        //Pojazd pojazd = new Pojazd();
        //pojazd.Start();
        

        //Console.WriteLine();

        
        //Console.WriteLine("TEST 2: SAMOCHOD");
        //Samochod samochod = new Samochod();
        //samochod.Start();   
        //samochod.Jedz();   
                            

        Console.WriteLine();

        
        Console.WriteLine("ELEKTRYCZNY SAMOCHOD");
        ElektrycznySamochod elektryk = new ElektrycznySamochod();
        elektryk.Start();   
        elektryk.Jedz();    
        elektryk.Laduj();   

        Console.WriteLine();

        
      
    }
}