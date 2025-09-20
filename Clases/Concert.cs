namespace RiwiMusic.Clases;
using Spotify;
public class Concert
{
    public string Name { get; set; }
    public List<string> Artists { get; set; } = new List<string>();
    public DateOnly DateOn { get; set; }
    public TimeOnly TimeOn { get; set; }
    public string Place { get; set;  }
    public decimal BasePrice { get; set; }

    public Concert(string name, List<string> artists, DateOnly dateOn, TimeOnly timeOn, string place,decimal basePrice)
    {
        Name = name;
        Artists = artists;
        DateOn = dateOn;
        TimeOn = timeOn;
        Place = place;
        BasePrice = basePrice;
    } 
}
