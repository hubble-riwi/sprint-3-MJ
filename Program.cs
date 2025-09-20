using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
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
            case "5":
                flag = false;
                break;
            default:
            {
                Console.WriteLine("Ingrese una opcion valida!");
                break;
            }
        }
    }
}

void Tickets()
{   
    Console.Clear();
    bool flag = true;

    while (flag)
    {
        Console.Write("-- Tiquetes --\n" +
                      "1. Vender tiquete \n" +
                      "2. Mostrar tiquetes \n" +
                      "3. Editar tiquete \n" +
                      "4. Eliminar tiquete \n" +
                      "5. Salir\n" +
                      ">> ");
        string option =  Console.ReadLine();
        switch (option)
        {
            case "1":
                {
                    Console.Write("Ingrese el documento del cliente: ");
                    int docClient = int.Parse(Console.ReadLine());

                    if (!riwiMusic.TryGetClient(docClient, out Client client))
                    {
                        Console.WriteLine("Cliente no encontrado. Regístrelo primero.");
                        break;
                    }


                    Console.WriteLine("Lista de conciertos disponibles:");
                    int idx = 0;
                    foreach (var concert in riwiMusic.GetConcerts())
                    {
                        Console.WriteLine($"{idx}. {concert.Name} | {concert.Place} | {concert.DateOn} {concert.TimeOn} | Precio: {concert.BasePrice}");
                        idx++;
                    }

                    Console.Write("Ingrese el ID del concierto: ");
                    int idConcert = int.Parse(Console.ReadLine());

                    if (!riwiMusic.TryGetConcert(idConcert, out Concert concertSelected))
                    {
                        Console.WriteLine("Concierto no encontrado.");
                        break;
                    }


                    Console.Write("Ingrese la cantidad de tiquetes: ");
                    int quantity = int.Parse(Console.ReadLine());


                    decimal total = concertSelected.BasePrice * quantity;


                    riwiMusic.CreateTicket(docClient, idConcert, quantity, total, DateOnly.FromDateTime(DateTime.Now));

                    Console.WriteLine($"Compra registrada! Total a pagar: {total}");
                    break;
                        
                }
            case "2":
            {
                if (riwiMusic.GetConcerts().Count == 0)
                {
                    Console.WriteLine("No Hay tiquetes vendidos.");
                }
                else
                {
                    Console.WriteLine("Lista de Tiquetes vendidos:");
                    int i = 0;
                    foreach (var ticket in riwiMusic.GetTickets())
                    {
                        Console.WriteLine($"{i}. Cliente: {ticket.DocumentClient} | ConciertoID: {ticket.IdConcert} | Cantidad: {ticket.QuantityTickets} | Total: {ticket.Total} | Fecha: {ticket.DateBuy}");
                        i++;
                    }
                }
                break;
            }
            case "3":
                {
                    if (riwiMusic.GetConcerts().Count == 0)
                    {
                        Console.WriteLine("No Hay tiquetes vendidos.");
                    }
                    else
                    {
                        Console.Write("Ingrese el ID del tiquete a editar: ");
                        int id = int.Parse(Console.ReadLine());
                        if (riwiMusic.TryGetTicket(id, out Tickets ticket))
                        {
                            Console.Write($"Cantidad anterior: {ticket.QuantityTickets}/ Nueva cantidad: ");
                            int newQuantity = int.Parse(Console.ReadLine());
                            ticket.QuantityTickets = newQuantity;

                            if (riwiMusic.TryGetConcert(ticket.IdConcert, out Concert concert))
                            {
                                ticket.Total = concert.BasePrice * newQuantity;
                                Console.WriteLine("Tiquete actualizado!");
                            }
                            else
                            {
                                Console.WriteLine("Error al actualizar el total. Concierto no encontrado.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Tiquete no encontrado!");
                        }
                    }
                    
                    
                    break;
                }
            case "4":
                {
                    if (riwiMusic.GetTickets().Count == 0)
                    {
                        Console.WriteLine("No hay tiquetes para eliminar.");
                    }
                    else
                    {
                        Console.Write("Ingrese el ID del tiquete a eliminar: ");
                        int id = int.Parse(Console.ReadLine());
                    
                        if (riwiMusic.TryGetTicket(id, out Tickets ticket))
                        {
                            riwiMusic.DeleteTicket(id);
                            Console.WriteLine("Tiquete eliminado!");
                        }
                        else
                        {
                            Console.WriteLine("Tiquete no encontrado!");
                        }
                    }
                    break;
                }
            case "5":
                {
                    flag = false;
                    Console.Clear();
                    break;
                }
            default:
                {
                    Console.WriteLine("Ingrese una opcion valida!");
                    break;
                }
        }
    }
}

void Concerts()
{
    Console.Clear();
    bool flag = true;

    while (flag)
    {
        Console.Write("-- Gestion Conciertos --\n" +
                      "1. Registrar Concierto \n" +
                      "2. Mostrar Conciertos \n" +
                      "3. Editar Concierto \n" +
                      "4. Eliminar Concierto \n" +
                      "5. Salir\n" +
                      ">> ");
        string option =  Console.ReadLine();
        switch (option)
        {
            case "1":
                {
                    Console.Write("Ingrese el nombre del concierto: ");
                    string name;
                    while (true)
                    {
                        name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name))
                            break;
                        Console.Write("El nombre no puede estar vacío. Ingrese nuevamente: ");
                    }

                    Console.Write("Ingrese los artistas (separados por comas): ");
                    List<string> artists;
                    while (true)
                    {
                        string input = Console.ReadLine();
                        artists = input.Split(',')
                            .Select(a => a.Trim())
                            .Where(a => !string.IsNullOrWhiteSpace(a))
                            .ToList();

                        if (artists.Count > 0)
                            break;
                        Console.Write("Debe ingresar al menos un artista. Intente de nuevo: ");
                    }
                    
                    Console.Write("Ingrese la fecha del concierto (YYYY-MM-DD): ");
                    DateOnly dateOn;
                    while (!DateOnly.TryParse(Console.ReadLine(), out dateOn))
                    {
                        Console.Write("Formato inválido. Ingrese la fecha (YYYY-MM-DD): ");
                    }
                    
                    Console.Write("Ingrese la hora del concierto (HH:MM): ");
                    TimeOnly timeOn;
                    while (!TimeOnly.TryParse(Console.ReadLine(), out timeOn))
                    {
                        Console.Write("Formato inválido. Ingrese la hora (HH:MM): ");
                    }
                    
                    Console.Write("Ingrese el lugar del concierto: ");
                    string place;
                    while (true)
                    {
                        place = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(place))
                            break;
                        Console.Write("El lugar no puede estar vacío. Ingrese nuevamente: ");
                    }
                    
                    Console.Write("Ingrese el precio base del concierto: ");
                    decimal basePrice;
                    while (!decimal.TryParse(Console.ReadLine(), out basePrice) || basePrice < 0)
                    {
                        Console.Write("Precio inválido. Ingrese un número mayor o igual a 0: ");
                    }
                    
                    riwiMusic.CreateConcert(name, artists, dateOn, timeOn, place, basePrice);
                    Console.WriteLine("Concierto registrado exitosamente!");
                    break;
                }
            case "2":
            {
                if (riwiMusic.GetConcerts().Count == 0)
                {
                    Console.WriteLine("No Hay conciertos registrados.");
                }
                else
                {
                    Console.WriteLine("Lista de Conciertos:\n");
                    int index = 0;
                    foreach (var concert in riwiMusic.GetConcerts())
                    {
                        string artistas = string.Join(", ", concert.Artists);
                        Console.WriteLine($"ID: {index} - Nombre: {concert.Name} - Artistas: {artistas} - Lugar: {concert.Place} - Fecha: {concert.DateOn} - Hora: {concert.TimeOn}");
                        index++;
                    }
                }
                break;    
            }
            case "3":
            {
                if (riwiMusic.GetConcerts().Count == 0)
                {
                    Console.WriteLine("No Hay conciertos registrados.");
                }
                else
                {
                    Console.Write("Ingrese el ID del concierto a editar: ");
                    int id = int.Parse(Console.ReadLine());

                    if (riwiMusic.TryGetConcert(id, out Concert concert))
                    {
                        Console.WriteLine($"✏ Editando concierto: {concert.Name}");

                        Console.Write($"Nuevo nombre (actual: {concert.Name}): ");
                        string newName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newName))
                            concert.Name = newName;

                        Console.WriteLine($"Artistas actuales: {string.Join(", ", concert.Artists)}");
                        Console.Write("Ingrese los nuevos artistas (separados por comas, deje vacío para no cambiar): ");
                        string newArtists = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newArtists))
                            concert.Artists = newArtists.Split(',').Select(a => a.Trim()).ToList();

                        Console.Write($"Nueva fecha (YYYY-MM-DD) (actual: {concert.DateOn}): ");
                        string newDate = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newDate))
                            concert.DateOn = DateOnly.Parse(newDate);

                        Console.Write($"Nueva hora (HH:MM) (actual: {concert.TimeOn}): ");
                        string newTime = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newTime))
                            concert.TimeOn = TimeOnly.Parse(newTime);

                        Console.Write($"Nuevo lugar (actual: {concert.Place}): ");
                        string newPlace = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newPlace))
                            concert.Place = newPlace;

                        Console.Write($"Nuevo precio base (actual: {concert.BasePrice}): ");
                        string newPrice = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newPrice))
                            concert.BasePrice = decimal.Parse(newPrice);

                        Console.WriteLine("Concierto actualizado correctamente!");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró el concierto con ese ID.");
                    }
                }
                break;
            }

            case "4":
            {
                Console.Write("Ingrese el ID del concierto a eliminar: ");
                int id = int.Parse(Console.ReadLine());

                if (riwiMusic.TryGetConcert(id, out Concert concert))
                {
                    riwiMusic.DeleteConcert(id);
                    Console.WriteLine("Concierto eliminado correctamente!");
                }
                else
                {
                    Console.WriteLine("No se encontró el concierto con ese ID.");
                }
                break;
            }
            case "5":
                {
                    flag = false;
                    Console.Clear();
                    break;
                }
            default:
                {
                    Console.WriteLine("Ingrese una opcion valida!");
                    break;
                }
        }
    }
}

void HistoryBuys()
{
    Console.Write("Ingrese el documento del cliente: ");
    string validation =  Console.ReadLine();

    if (int.TryParse(validation, out int document))
    {
        var tickets = riwiMusic.GetTickets();
        var concerts = riwiMusic.GetConcerts();


        try
        {
            var BuysPerUser = from t in tickets
                join c in concerts on t.IdConcert equals riwiMusic.ReturnIdConcert((c.Name))
                where t.DocumentClient == document
                select new
                {
                    NameConcert = c.Name,
                    DateConcert = c.DateOn,
                };

            Console.WriteLine("Crompras realizadas por el usuario");
            foreach (var buy in BuysPerUser)
            {
                Console.Write($"- Nombre del concierto: {buy.NameConcert} \n" +
                              $"- Fecha del concierto: {buy.DateConcert}");
            }
        }
        catch
        {
            Console.WriteLine("El usuario no ha hecho compras!");
        }
    }
}

void Querys()
{
    Console.Clear();
    bool flag = true;
    string validation;
    var allConcerts = riwiMusic.GetConcerts();

    while (flag)
    {
        Console.Write("-- Consultas avanzadas-- \n" +
                      "1. Consultar conciertos por ciudad\n" +
                      "2. Consultar conciertos por rango de fechas\n" +
                      "3. Consultar el concierto con más tiquetes vendidos\n" +
                      "4. Consultar ingresos totales de un concierto\n" +
                      "5. Consultar cliente con más compras realizadas \n" +
                      "6. Regresar \n" +
                      ">> ");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Console.Write("Ingrese la ciudad: ");
                string city = Console.ReadLine();
                var concerts = riwiMusic.GetConcerts();
                var ConcertsPerCity = from c in concerts where c.Place == city select c;

                if (ConcertsPerCity.Count() == 0)
                {
                    Console.WriteLine("No hay conciertos en esta ciudad");
                }
                else
                {
                    Console.WriteLine($"Conciertos en la ciudad {city}");
                    foreach (var c in ConcertsPerCity)
                    {
                        Console.WriteLine($"- Nombre del concierto: {c.Name} \n" +
                                          $"- Artistas: {string.Join(", ", c.Artists)}\n" +
                                          $"- Fecha y hora del concierto: {c.DateOn} - {c.TimeOn} \n" +
                                          $"- Precio: {c.BasePrice}\n ");
                    }
                }

                break;

            case "2":
                Console.Write("Ingrese la primer fecha (YYYY-MM-DD): ");
                validation = Console.ReadLine();
                if (DateOnly.TryParse(validation, out var FirstDate))
                {
                    Console.Write("Ingres la segunda fecha (YYYY-MM-DD): ");
                    validation = Console.ReadLine();

                    if (DateOnly.TryParse(validation, out var SecondDate))
                    {
                        var concertstwo = riwiMusic.GetConcerts();
                        var concertsDates = from c in concertstwo
                                            where c.DateOn >= FirstDate && c.DateOn <= SecondDate
                                            select c;

                        Console.WriteLine($"Conciertos de {FirstDate} a {SecondDate}");
                        foreach (var concert in concertsDates)
                        {
                            Console.WriteLine($"- Nombre del concierto: {concert.Name} \n" +
                                              $"- Artistas: {string.Join(", ", concert.Artists)}\n" +
                                              $"- Fecha y hora del concierto: {concert.DateOn} - {concert.TimeOn} \n" +
                                              $"- Precio: {concert.BasePrice}\n ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Ingrese un fecha correcta");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Ingrese un fecha correcta");
                }
                break;

            case "3":
                var bestSelling =
                    (from t in riwiMusic.GetTickets()
                     group t by t.IdConcert into groupC
                     orderby groupC.Sum(x => x.QuantityTickets) descending
                     select new
                     {
                         IdConcert = groupC.Key,
                         totalSold = groupC.Sum(x => x.QuantityTickets)
                     }).FirstOrDefault();

                if (bestSelling == null)
                {
                    Console.WriteLine("No hay tiquetes vendidos todavía.");
                    break;
                }


                int id = bestSelling.IdConcert;
                if (id < 0 || id >= allConcerts.Count)
                {
                    Console.WriteLine("Error: el concierto referenciado por las ventas no existe.");
                    break;
                }

                var concertF = allConcerts[id];

                Console.WriteLine("Concierto con más tiquetes vendidos:");
                Console.WriteLine($"- Nombre: {concertF.Name}");
                Console.WriteLine($"- Artistas: {string.Join(", ", concertF.Artists)}");
                Console.WriteLine($"- Lugar: {concertF.Place}");
                Console.WriteLine($"- Fecha y hora: {concertF.DateOn} {concertF.TimeOn}");
                Console.WriteLine($"- Tickets vendidos: {bestSelling.totalSold}");
                break;
            case "4":

                for (int i = 0; i < riwiMusic.GetConcerts().Count; i++)
                {
                    Console.WriteLine($"{i}. {allConcerts[i].Name} | {allConcerts[i].Place}");
                }

                Console.Write("Ingrese el id del concierto para consultar ingresos: ");
                int idConcert = int.Parse(Console.ReadLine()!);
                var incomes = (from t in riwiMusic.GetTickets()
                                where t.IdConcert == idConcert
                                select t.QuantityTickets).Sum()
                                * riwiMusic.GetConcerts()[idConcert].BasePrice;
                Console.WriteLine($"Los ingresos totales son {incomes}");
                break;
            case "5":
                var cMorePurchases = (from t in riwiMusic.GetTickets()
                                      group t by t.DocumentClient into groupD
                                      orderby groupD.Sum(x => x.QuantityTickets) descending
                                      select new
                                      {
                                          documentC = groupD.Key,
                                          totalPurchases = groupD.Sum(x => x.QuantityTickets)
                                      }).FirstOrDefault();
                if (cMorePurchases == null)
                {
                    Console.WriteLine("No hay tiquetes vendidos todavía.");
                    break;
                }

                int doc = cMorePurchases.documentC;
                if (!riwiMusic.TryGetClient(doc, out Client clientMorePurchases))
                {
                    Console.WriteLine("Error: el cliente referenciado por las compras no existe.");
                    break;
                }
                Console.WriteLine("Cliente con más compras realizadas:");
                Console.WriteLine($"- Documento: {clientMorePurchases.ReturnDocument()}");
                Console.WriteLine($"- Nombre: {clientMorePurchases.Name}");
                Console.WriteLine($"- Total de tiquetes comprados: {cMorePurchases.totalPurchases}");
                
                break;
            case "6":
                flag = false;
                break;
        }
    }
}