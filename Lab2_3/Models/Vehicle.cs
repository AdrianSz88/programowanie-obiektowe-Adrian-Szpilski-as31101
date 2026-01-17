
namespace Lab2.Models;

public abstract class Vehicle
{
    protected string marka;
    protected string model;
    protected int year;

    public string Model
    {
        get { return model; }
        set { model = value; }
    }

    public int Year
    {
        get { return year; }
        set { year = value; }
    }

    
    public Vehicle(string marka,string model, int year)
    {
        this.marka = marka;      
        this.model = model;
        this.year = year;
    }

    public virtual void ShowInfo()
    {
        Console.WriteLine($"Marka: {marka}");
        Console.WriteLine($"Model: {model}");
        Console.WriteLine($"Year: {year}");
    }

    public virtual void ShowInfo(string header)
    {
        Console.WriteLine(header);
        ShowInfo();
    }
}
