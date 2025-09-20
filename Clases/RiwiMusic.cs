using RiwiMusic.Clases;
namespace Spotify;

public class RiwiMusic
{
    private List<Concert> ListConcerts { get; set; } = new List<Concert>();
    private List<Tickets> ListTickets { get; set; } = new List<Tickets>();
    private List<Client> ListClients { get; set; } = new List<Client>();

    public void CreateConcert(string name, List<string> artists, DateOnly dateOn, TimeOnly timeOn, string place,
        decimal basePrice)
    {
        ListConcerts.Add(new(name, artists, dateOn, timeOn, place, basePrice));
    }
    
    public void CreateTicket(int documentClient, int idConcert, int quantityTickets, decimal total, DateOnly dateBuy)
    {
        ListTickets.Add(new(documentClient, idConcert, quantityTickets, total, dateBuy));
    }
    
    public void CreateClient(int document, string name, int phone, string email)
    {
        ListClients.Add(new(document, name, phone, email));
    }
    
    

    public bool TryGetConcert(int idConcert, out Concert concert)
    {
        if (idConcert >= 0 && idConcert < ListConcerts.Count)
        {
            concert = ListConcerts[idConcert];
            return true;
        }

        concert = null;
        return false;
    }

    
    public bool TryGetTicket(int idTicket, out Tickets ticket)
    {
        bool found = false;
        for (int i = 0; i < ListTickets.Count; i++)
        {
            if (idTicket == i)
            {
                ticket = ListTickets[i];
                found = true;
            }
        }

        ticket = null;
        return found;
    }
    
    public bool TryGetClient(int idClient, out Client client)
    {
        bool found = false;
        for (int i = 0; i < ListClients.Count; i++)
        {
            if (idClient == i)
            {
                client = ListClients[i];
                found = true;
            }
        }

        client = null;
        return found;
    }
    
    public List<Concert> GetConcerts()
    {
        return ListConcerts;
    }
    
    public void DeleteConcert(int idConcert)
    {
        if (idConcert >= 0 && idConcert < ListConcerts.Count)
        {
            ListConcerts.RemoveAt(idConcert);
        }
    }

    
    
    
    
}
