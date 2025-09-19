namespace RiwiMusic.Clases;

public class Tickets
{
    public int DocumentClient {get; set;}
    public int IdConcert {get; set;}
    public int QuantityTickets {get; set;}
    public decimal Total {get; set;}
    public DateOnly DateBuy {get; set;}
    
    public Tickets(int documentClient, int idConcert, int quantityTickets, decimal total, DateOnly dateBuy)
    {
        DocumentClient = documentClient;
        IdConcert = idConcert;
        QuantityTickets = quantityTickets;
        Total = total;
        DateBuy = dateBuy;
    }
}