namespace Lab2.Models;

public class Bike : Vehicle
{

    public Bike(string marka, string model, int year)
        : base(marka, model, year)
    {

    }

    public override void ShowInfo()
    {
        Console.WriteLine("BIKE");     
        base.ShowInfo();
    }
}