namespace RiwiMusic.Clases;

public class Client
{
    private int Document { get; set; }
    public string Name { get; set; }
    public int Phone { get; set; }
    public string Email { get; set; }

    public Client(int document, string name, int phone, string email)
    {
        Document = document;
        Name = name;
        Phone = phone;
        Email = email;
    }
}