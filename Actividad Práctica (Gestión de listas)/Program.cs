
// Una tienda de barrio, un menu de opciones 


class Program

{
    static List<string> nombresProductos = new() { "Papaya", "Melon", "Limon", "Pera", "Fresa" };
    static List<decimal> preciosProductos = new() { 450.49m, 500.75m, 700.50m, 900.00m, 2000.00m };
    
    static void Main()
    {

        Console.WriteLine("======== Bienvenidos a la tienda La Bola Rosa ========");
        Console.WriteLine();
        Console.WriteLine();
        // Empieza en -1 para entrar la primera vez; termina cuando la opción es 0
        for (int opcion = -1; opcion != 0;)
        {
            Console.WriteLine("\n--- Productos disponibles ---");
            MostrarProductos();

            Console.Write("\n1.Agregar 2.Mostrar 3.Buscar 4.Modificar 5.Eliminar 6.Estadisticas 7.Ordenar 0.Salir: ");
            if (!int.TryParse(Console.ReadLine(), out opcion)) opcion = -1;

            switch (opcion)
            {
                case 1: AgregarProducto(); break;
                case 2: MostrarProductos(); break;
                case 3: BuscarProducto(); break;
                case 4: ModificarProducto(); break;
                case 5: EliminarProducto(); break;
                case 6: MostrarEstadisticas(); break;
                case 7: OrdenarProductos(); break;
                case 0: Console.WriteLine("No quiero nada"); break;
                default: Console.WriteLine("Agotado"); break;
            }
        }
    }

    // Repite la pregunta hasta que el usuario escriba algo
    static string PedirTexto(string mensaje)
    {
        string texto = "";
        for (; texto == ""; texto = (Console.ReadLine() ?? "").Trim()) Console.Write(mensaje);
        return texto;
    }

    // Repite la pregunta hasta que el usuario escriba un número mayor que 0
    static decimal PedirPrecio()
    {
        decimal precio;
        Console.Write("Precio: ");
        for (; !decimal.TryParse(Console.ReadLine(), out precio) || precio <= 0;) Console.Write("Precio invalido: ");
        return precio;
    }

    // Devuelve la posición del producto en la lista (-1 si no existe)
    static int BuscarPosicion(string nombreBuscado)
    {
        for (int posicion = 0; posicion < nombresProductos.Count; posicion++)
            if (nombresProductos[posicion].Equals(nombreBuscado, StringComparison.OrdinalIgnoreCase)) return posicion;
        return -1;
    }

    // Avisa si no hay productos y devuelve true o false
    static bool HayProductos()
    {
        if (nombresProductos.Count == 0) Console.WriteLine("Inventario vacio.");
        return nombresProductos.Count > 0;
    }

    // Pide un nombre y devuelve su posición (-1 si no hay inventario o no existe)
    static int PedirProductoExistente(string mensaje)
    {
        if (!HayProductos()) return -1;
        int posicion = BuscarPosicion(PedirTexto(mensaje));
        if (posicion == -1) Console.WriteLine("Estimado no existe.");
        return posicion;
    }

    static void AgregarProducto()
    {
        string nombre = PedirTexto("Nombre: ");
        if (BuscarPosicion(nombre) != -1) { Console.WriteLine("Ya existe."); return; }
        nombresProductos.Add(nombre);
        preciosProductos.Add(PedirPrecio());
    }

    static void MostrarProductos()
    {
        if (!HayProductos()) return;
        int numero = 1;
        foreach (string nombre in nombresProductos)
        {
            Console.WriteLine($"{numero}. {nombre} - ${preciosProductos[numero - 1]:F2}");
            numero++;
        }
    }

    static void BuscarProducto()
    {
        int posicion = PedirProductoExistente("Buscar: ");
        if (posicion != -1) Console.WriteLine($"{nombresProductos[posicion]} - ${preciosProductos[posicion]:F2}");
    }

    static void ModificarProducto()
    {
        int posicion = PedirProductoExistente("Producto a modificar: ");
        if (posicion == -1) return;
        nombresProductos[posicion] = PedirTexto("Nuevo nombre: ");
        preciosProductos[posicion] = PedirPrecio();
    }

    static void EliminarProducto()
    {
        int posicion = PedirProductoExistente("Producto a eliminar: ");
        if (posicion == -1) return;
        nombresProductos.RemoveAt(posicion);
        preciosProductos.RemoveAt(posicion);
    }

    static void MostrarEstadisticas()
    {
        if (!HayProductos()) return;
        decimal mayor = preciosProductos[0], menor = preciosProductos[0], suma = 0;
        foreach (decimal precio in preciosProductos)
        {
            suma += precio;
            mayor = Math.Max(mayor, precio);
            menor = Math.Min(menor, precio);
        }
        Console.WriteLine($"Total: {nombresProductos.Count} | Mayor: ${mayor:F2} | Menor: ${menor:F2} | Promedio: ${suma / preciosProductos.Count:F2}");
    }
    static void OrdenarProductos()
    {
        if (!HayProductos()) return;
        Console.Write("Ordenar por 1.Nombre 2.Precio: ");
        string criterio = Console.ReadLine();
        if (criterio != "1" && criterio != "2") { Console.WriteLine("."); return; }

        for (int vuelta = 0; vuelta < nombresProductos.Count - 1; vuelta++)
            for (int actual = 0; actual < nombresProductos.Count - 1 - vuelta; actual++)
            {
                int siguiente = actual + 1;
                bool estaMalOrdenado = criterio == "1"
                    ? string.Compare(nombresProductos[actual], nombresProductos[siguiente], true) > 0
                    : preciosProductos[actual] > preciosProductos[siguiente];

                if (estaMalOrdenado)
                {
                    (nombresProductos[actual], nombresProductos[siguiente]) = (nombresProductos[siguiente], nombresProductos[actual]);
                    (preciosProductos[actual], preciosProductos[siguiente]) = (preciosProductos[siguiente], preciosProductos[actual]);
                }
            }
        MostrarProductos();
    }
}
