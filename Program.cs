bool flag = true;

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

    switch (option)
    {
        default:
            Console.WriteLine("Invalid option!");
            break;
    }
}