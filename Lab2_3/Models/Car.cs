namespace Lab2.Models;

//TOFO: dodaj kolor


public class Car : Vehicle
{

    private string color;
    public string Color
    {
        get { return color; }
        set { color = value; }
    }


    public Car(string marka, string model, int year, string color)
        : base(marka, model, year)  
    {
        this.color = color;
    }
        
    public override void ShowInfo()
    {
      //  Console.WriteLine("CAR");      
        base.ShowInfo();
        Console.WriteLine($"Color: {color}");
    }
}