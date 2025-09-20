using RiwiMusic.Clases;
using Spotify;

Spotify.RiwiMusic riwiMusic = new Spotify.RiwiMusic();
bool flag = true;



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
    
}

void Tickets()
{
    
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
                Console.WriteLine("Lista de Conciertos:\n");
                int index = 0;
                foreach (var concert in riwiMusic.GetConcerts())
                {
                    string artistas = string.Join(", ", concert.Artists);
                    Console.WriteLine($"ID: {index} - Nombre: {concert.Name} - Artistas: {artistas} - Lugar: {concert.Place} - Fecha: {concert.DateOn} - Hora: {concert.TimeOn}");
                    index++;
                }
                break;    
            }
            case "3":
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
        {
            
        }
    }
    
    


}

void HistoryBuys()
{
    
}

void Querys()
{
    
}