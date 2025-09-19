using RiwiMusic.Clases;
using Spotify;

bool flag = true;
Spotify.RiwiMusic riwiMusic = new Spotify.RiwiMusic();

Dictionary<string, Action> actions = new Dictionary<string, Action>();
actions.Add("1", Concerts);
actions.Add("2", Clients);
actions.Add("3", Tickets);
actions.Add("4", HistoryBuys);
actions.Add("5", Querys);

while (flag)
{
    Console.Write("-- Riwi Music --\n" +
                  "1. Administrar conciertos \n" +
                  "2. Administrar clientes \n" +
                  "3. Administrar tiquetes \n" +
                  "4. Historial de compras de clientes \n" +
                  "5. Consultas avanzadas \n" +
                  "6. Salir\n" +
                  ">> ");
    string option =  Console.ReadLine();

    if (new List<string> { "1", "2", "3", "4", "5" }.Contains(option))
    {
        actions[option]();
    } else if (option == "6")
    {
        flag = false;
    }
    else
    {
        Console.WriteLine("Ingrese una opcion valida!");
    }
}

void Clients()
{
    Console.Clear();
    bool flag = true;

    while (flag)
    {
        Console.Write("-- Clientes-- \n" +
                      "1. Registrar nuevo cliente \n" +
                      "2. Listar clientes\n" +
                      "3. Editar cliente \n" +
                      "4. Eliminar cliente\n" +
                      "5. Regresar\n" +
                      ">> ");
        
        string option = Console.ReadLine();
        string validation;

        switch (option)
        {
            case "1":
                Console.Write("Ingrese el documento del cliente: ");
                validation = Console.ReadLine();

                if (int.TryParse(validation, out int Document))
                {
                    Console.Write("Ingrese el nombre: ");
                    string Name = Console.ReadLine();
                    
                    Console.Write("Ingrese el telefono: ");
                    validation = Console.ReadLine();

                    if (int.TryParse(validation, out int Phone))
                    {
                        Console.Write("Ingrese el email: ");
                        string Email = Console.ReadLine();
                        
                        riwiMusic.CreateClient(Document, Name, Phone, Email);
                        Console.WriteLine("Cliente creado! \n ");
                    }
                    else
                    {
                        Console.WriteLine("Error: Ingreso un campo invalido, intentelo de nuevo");  
                    }
                }
                else
                {
                    Console.WriteLine("Error: Ingreso un campo invalido, intentelo de nuevo");
                }
                break;
            
            case "2":
                var clients = riwiMusic.GetClients();
                
                Console.WriteLine("Listado de clientes");
                foreach (var client in clients)
                {
                    Console.WriteLine($"- Documento: {client.ReturnDocument()} \n" +
                                      $"- Nombre: {client.Name} \n" +
                                      $"- Telefono: {client.Phone} \n" +
                                      $"- Email: {client.Email}\n ");
                }

                break;
            case "3":
                Console.Write("Ingrese el documento del cliente: ");
                validation = Console.ReadLine();

                if (int.TryParse(validation, out int id))
                {
                    if (riwiMusic.TryGetClient(id, out Client client))
                    {
                        Console.Write($"Nombre anterior: {client.Name}/ Nombre: ");
                        string NewName = Console.ReadLine();
                        
                        client.Name =  NewName;
                        
                        Console.Write($"Telefono anterior: {client.Phone}/ Telefono: ");
                        validation = Console.ReadLine();

                        if (int.TryParse(validation, out int Phone))
                        {
                            client.Phone = Phone;
                            
                            Console.Write($"Email anterior: {client.Email}/ Email:");
                            string email = Console.ReadLine();
                            
                            client.Email = email;
                            Console.WriteLine("Cliente actualizado!");
                        }
                        else
                        {
                            Console.WriteLine("Error: Ingreso un campo invalido, intentelo de nuevo");
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Cliente no encontrado!");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Ingreso un campo invalido, intentelo de nuevo");
                }
                break;
            
            case "4":
                Console.Write("Ingrese el documento del cliente: ");
                validation = Console.ReadLine();

                if (int.TryParse(validation, out int document))
                {
                    if (riwiMusic.DeleteClient(document))
                    {
                        Console.WriteLine("Cliente eliminado!");
                    }
                    else
                    {
                        Console.WriteLine("Cliente no encontrado!");
                    }
                }

                break;
        }
    }
}

void Tickets()
{
    
}

void Concerts()
{
    
}

void HistoryBuys()
{
    
}

void Querys()
{
    
}