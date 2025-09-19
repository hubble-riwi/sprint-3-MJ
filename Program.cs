

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

        switch (option)
        {
            
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