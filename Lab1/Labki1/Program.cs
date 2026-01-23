using System;

//namespace Zadania
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            //ZADANIE 1

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



//            //ZADANIE 2
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


//            //ZADANIE 3
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


//ZADANIE 4

class Osoba
{

    public string Imie;
    public int Wiek;

    public void PrzedstawSie()
    {
        Console.WriteLine($"Cześć, jestem {Imie} i mam {Wiek} lat.");
    }
}


class Program
{
    static void Main()
    {
        Console.WriteLine("=== TWORZENIE OBIEKTÓW KLASY OSOBA ===\n");


        Osoba osoba1 = new Osoba();
        osoba1.Imie = "Jan";
        osoba1.Wiek = 25;


        Osoba osoba2 = new Osoba();
        osoba2.Imie = "Anna";
        osoba2.Wiek = 30;


        Osoba osoba3 = new Osoba();
        osoba3.Imie = "Piotr";
        osoba3.Wiek = 22;


        osoba1.PrzedstawSie();
        osoba2.PrzedstawSie();
        osoba3.PrzedstawSie();
    }


}

