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
    
    public bool TryGetClient(int document, out Client client)
    {
        client = ListClients.FirstOrDefault(c => c.ReturnDocument() == document);
        
        return client != null;
    }

    public List<Client> GetClients()
    {
        return ListClients;
    }

    public List<Concert> GetConcerts()
    {
        return ListConcerts;
    }

    public bool DeleteClient(int document)
    {
        for (int i = 0; i < ListClients.Count; i++)
        {
            if (ListClients[i].ReturnDocument() == document)
            {
                ListClients.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
    
    
    public void DeleteConcert(int idConcert)
    {
        if (idConcert >= 0 && idConcert < ListConcerts.Count)
        {
            ListConcerts.RemoveAt(idConcert);
        }
    }
    
    public object ReturnIdConcert(string name)
    {
        for (int i = 0; i < ListConcerts.Count; i++)
        {
            if (ListConcerts[i].Name == name)
            {
                return i;
            }
        }

        return null;
    }
}
