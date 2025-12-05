string password;

do
{
    Console.Write("Podaj hasło: ");
    password = Console.ReadLine() ?? "";
}
while (password != "admin123");

Console.WriteLine("Zalogowano pomyślnie!");