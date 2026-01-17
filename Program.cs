using Lab2.Models;
using Newtonsoft.Json; 


var path = Path.Combine(Directory.GetCurrentDirectory(), "data.json");
//Console.WriteLine("Ścieżka do pliku: {path}");
//Console.WriteLine("Plik istnieje: {File.Exists(path)}");
var txt = File.ReadAllText(path);
var data = JsonConvert.DeserializeObject<List<Car>>(txt);

var continueApp = true;

do
{
    Console.WriteLine("\n╔═══════════════════════════╗");
    Console.WriteLine("║     ===++ MENU ++===      ║");
    Console.WriteLine("║   ZARZĄDZANIE POJAZDAMI   ║");
    Console.WriteLine("╠═══════════════════════════╣");
    Console.WriteLine("║ 1. Lista pojazdów         ║");
    Console.WriteLine("║ 2. Dodaj pojazd           ║");
    Console.WriteLine("║ 3. Usuń pojazd            ║");
    Console.WriteLine("║ 4. Edytuj kolor           ║");
    Console.WriteLine("║ 0. Wyjście                ║");
    Console.WriteLine("╚═══════════════════════════╝");
   

    var option = Console.ReadKey().KeyChar;
    Console.WriteLine();

    switch (option)
    {
        case '1':
            if (data.Count == 0)
            {
                Console.WriteLine(" Lista jest pusta!");
               
            }
            else
            Console.WriteLine("\n=== LISTA POJAZDÓW ===");
            for (int i = 0; i < data.Count; i++)
            {
                Console.WriteLine($"\nID: {i}");
                data[i].ShowInfo();
            }
            break;
           

        case '2':
            AddCar();
            break;

        case '3':
            RemoveCar();
            break;

        case '4':  
            EditCarColor();
            break;

        case '0':
            continueApp = false;
            Console.WriteLine("Koniec programu.");
            break;

        default:
            Console.WriteLine("Nieznana opcja. Spróbuj ponownie.");
            break;

    }



} while (continueApp);

var json = JsonConvert.SerializeObject(data, Formatting.Indented);
File.WriteAllText(path, json);

Console.WriteLine("Dane zapisane!");

return;

void AddCar()
{
    Console.WriteLine("\n===++ DODAWANIE POJAZDU ++===");

   
    Console.Write("Podaj marke: ");
    var marka = Console.ReadLine();
    
    Console.Write("Podaj model: ");
    var model = Console.ReadLine();

    // Dodawanie koloru
    Console.Write("Podaj kolor: ");
    var color = Console.ReadLine(); 

    Console.Write("Podaj rok: ");
    var success = int.TryParse(Console.ReadLine(), out int year);

    
    if (!success || model == null || marka == null || color == null || string.IsNullOrWhiteSpace(model))                 
    {
        Console.WriteLine("!!! Niepoprawne dane!");
        return;
    }

    
    data.Add(new Car(marka, model, year, color));
    Console.WriteLine($"+++ Dodano:{marka} {model} ({year}) {color}");
}
// TODO: dodaj usuwanie pojazdu po indeksie
void RemoveCar()
{
    Console.WriteLine("\n===++ USUWANIE POJAZDU ++===");

    if (data.Count == 0)
    {
        Console.WriteLine(" Lista jest pusta!");
        return;
    }

    Console.WriteLine("Dostępne pojazdy:");
    for (int i = 0; i < data.Count; i++)
    {
        Console.WriteLine($"{i}: {data[i].Model} ({data[i].Year})");
    }

    Console.Write("\nPodaj numer ID pojazdu do usunięcia: ");
    var success = int.TryParse(Console.ReadLine(), out int index);

    if (!success || index < 0 || index >= data.Count)
    {
        Console.WriteLine("!!! Niepoprawny indeks!");
        return;
    }

    // usuwanie
    var removed = data[index];
    data.RemoveAt(index);
    Console.WriteLine($"--- Usunięto: {removed.Model} ({removed.Year})");
}
// edycja koloru pojazdu    
void EditCarColor()
{
    Console.WriteLine("\n=== EDYCJA KOLORU POJAZDU ===");

    // Sprawdź czy są pojazdy
    if (data.Count == 0)
    {
        Console.WriteLine(" Lista jest pusta!");
        return;
    }

    // Wyświetl listę pojazdów
    Console.WriteLine("\nDostępne pojazdy:");
    for (int i = 0; i < data.Count; i++)
    {
        Console.WriteLine($"{i}: {data[i].Model} ({data[i].Year}) - {data[i].Color}");
    }

    // Pobierz ID pojazdu
    Console.Write("\nPodaj ID pojazdu do edycji: ");
    var success = int.TryParse(Console.ReadLine(), out int index);

    // Walidacja ID
    if (!success || index < 0 || index >= data.Count)
    {
        Console.WriteLine(" Niepoprawny ID!");
        return;
    }

    // Pobierz nowy kolor
    Console.Write($"Aktualny kolor: {data[index].Color}");
    Console.Write("\nPodaj nowy kolor: ");
    var newColor = Console.ReadLine();

    // Walidacja koloru
    if (string.IsNullOrWhiteSpace(newColor))
    {
        Console.WriteLine(" Kolor nie może być pusty!");
        return;
    }

    // Zapisz stary kolor do wyświetlenia
    var oldColor = data[index].Color;

    // Zaktualizuj kolor
    data[index].Color = newColor;

    // Potwierdzenie
    Console.WriteLine($"!!! Zmieniono kolor pojazdu {data[index].Model}:");
    Console.WriteLine($"   {oldColor} ==> {newColor}:");
}



