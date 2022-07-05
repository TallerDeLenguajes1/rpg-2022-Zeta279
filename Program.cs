using System.Text.Json;

namespace JuegoRPG{
    class Program{
        static void Main(string[] args){
            int opcion = 0;
            List<Personaje> listaPersonajes;

            while (opcion != 4)
            {

                Console.WriteLine("Ingrese una opción: ");
                Console.WriteLine("1) Jugar");
                Console.WriteLine("2) Registro de batallas");
                Console.WriteLine("3) Opción de desarrollo");
                Console.WriteLine("4) Salir");

                Console.Write("Opción: ");
                opcion = Int32.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        listaPersonajes = ObtenerJugadores();
                        if(listaPersonajes.Count > 0) Jugar(listaPersonajes);
                        break;
                    case 2:
                        Registro();
                        break;
                    case 3:
                        break;
                    case 4:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción incorrecta");
                        break;
                }
                
            }
        }

        static List<Personaje> ObtenerJugadores()
        {
            int opcion = 0;
            List<Task> crearPersonajes;
            List<Personaje> listaPersonajes = new List<Personaje>();
            int cantidad;

            while (opcion < 1 || opcion > 3)
            {
                Console.WriteLine("1) Generar aleatoriamente los jugadores");
                Console.WriteLine("2) Leer los jugadores de un json");
                Console.WriteLine("3) Retornar");
                Console.Write("Opción: ");
                opcion = Int32.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        // Ingresar la cantidad de jugadores
                        Console.WriteLine("\nIngrese la cantidad de jugadores (mínimo 2, máximo 10): ");
                        do
                        {
                            cantidad = Int32.Parse(Console.ReadLine());
                        } while (cantidad < 2 || cantidad > 10);

                        // Generar asincrónicamente y guardar los jugadores en una lista
                        Console.WriteLine("Creando personajes...");
                        crearPersonajes = new List<Task>();

                        Console.Write("\r");
                        BarraProgreso(0, cantidad);

                        for (int i = 0; i < cantidad; i++)
                        {
                            crearPersonajes.Add(new Task(() => listaPersonajes.Add(new Personaje(new Caracteristicas(), new Datos()))));
                            crearPersonajes[i].Start();
                        }

                        for (int i = 0; i < cantidad; i++)
                        {
                            crearPersonajes[i].Wait();
                            Console.Write("\r");
                            BarraProgreso(i + 1, cantidad);
                        }

                        Console.WriteLine("\nPersonajes creados con éxito!");
                        Thread.Sleep(1000);

                        // Guardar en un archivo json los jugadores
                        string json = JsonSerializer.Serialize(listaPersonajes);
                        if (!File.Exists("jugadores.json"))
                        {
                            File.Create("jugadores.json").Close();
                        }
                        File.WriteAllText("jugadores.json", json);

                        break;
                    case 2:
                        // Obtener los jugadores del archivo json
                        if (!File.Exists("jugadores.json"))
                        {
                            Console.WriteLine("No es posible cargar los jugadores de un archivo json");
                            opcion = 0;
                        }
                        else
                        {
                            try
                            {
                                listaPersonajes = JsonSerializer.Deserialize<List<Personaje>>(File.ReadAllText("jugadores.json"));
                            }
                            catch
                            {
                                Console.WriteLine("No es posible cargar los jugadores de un archivo json");
                                opcion = 0;
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("Retornando");
                        break;
                    default:
                        Console.WriteLine("Opción inválida");
                        break;
                }
            }

            return listaPersonajes;
        }

        static void Jugar(List<Personaje> listaPersonajes)
        {
            int contEnfrentamientos = 1, aux, dmgCausado;
            int cantidadJugadores = listaPersonajes.Count;
            string cadena;
            int delay = 40, salud, restar;
            Random rnd = new Random();
            Personaje atacante, defensor, ganador, perdedor;

            // Mostrar los datos de cada jugador
            Console.WriteLine($"Mostrando los datos de {cantidadJugadores} personajes\n");

            foreach (Personaje p in listaPersonajes)
            {
                Console.WriteLine(p);
            }

            // Elegir los personajes
            Personaje player1 = listaPersonajes[0];
            Personaje player2 = listaPersonajes[1];
            for(int i = 0; i < 2; i++) listaPersonajes.RemoveAt(0);


            // Enfrentamientos entre los jugadores
            while (player1.estaVivo() && player2.estaVivo())
            {
                Console.WriteLine($"\nEnfrentamiento {contEnfrentamientos++}");
                Console.WriteLine($"{player1.NombreYApodo()} vs {player2.NombreYApodo()}");

                for (int i = 0; i < 6 && player1.estaVivo() && player2.estaVivo(); i++)
                {
                    if(i % 2 == 0)
                    {
                        atacante = player1;
                        defensor = player2;
                    }
                    else
                    {
                        atacante = player2;
                        defensor = player1;
                    }

                    salud = defensor.Dat.Salud;
                    dmgCausado = Gameplay.Ataque(atacante, defensor);
                    Console.WriteLine($"\nAtaca el jugador {atacante.NombreYApodo()}!");
                    Thread.Sleep(500);
                    if (dmgCausado == 0)
                    {
                        MostrarDelay("No se provocó ningun daño", delay);
                    }
                    else
                    {
                        MostrarDelay("Daño provocado: ", delay);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(dmgCausado);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Thread.Sleep(500);
                        Console.WriteLine();
                        MostrarDelay($"Salud restante de {defensor.Dat.Nombre}: ", delay);
                        MostrarColor($"{salud}", ConsoleColor.Red);
                        Thread.Sleep(500);

                        if (dmgCausado <= 100) restar = 5;
                        else if (100 < dmgCausado && dmgCausado <= 500) restar = 10;
                        else if (500 < dmgCausado && dmgCausado <= 1000) restar = 20;
                        else if (1000 < dmgCausado && dmgCausado <= 3000) restar = 50;
                        else restar = 100;

                        aux = salud;

                        while (salud > (aux - dmgCausado) && salud > 0)
                        {
                            salud -= restar;
                            if (salud < 0) salud = 0;
                            LimpiarLinea();
                            Console.Write($"\rSalud restante de {defensor.Dat.Nombre}: ");
                            MostrarColor($"{salud}", ConsoleColor.Red);
                            Thread.Sleep(1);
                        }
                        LimpiarLinea();
                        Console.Write($"\rSalud restante de {defensor.Dat.Nombre}: ");
                        MostrarColor($"{defensor.Dat.Salud}", ConsoleColor.Red);
                        Thread.Sleep(500);
                    }

                    Console.WriteLine();
                    
                    Thread.Sleep(500);
                }

                // Determinar ganador del enfrentamiento
                if(player1.Dat.Salud != player2.Dat.Salud)
                {
                    if (!player1.estaVivo() || player1.Dat.Salud < player2.Dat.Salud)
                    {
                        ganador = player2;
                        perdedor = player1;
                    }
                    else
                    {
                        ganador = player1;
                        perdedor = player2;
                    }

                    Console.WriteLine($"\nPerdió el jugador {perdedor.NombreYApodo()}!\n");
                    if (listaPersonajes.Count > 0)
                    {
                        if (ganador == player2) player1 = listaPersonajes[rnd.Next(0, listaPersonajes.Count)];
                        else player2 = listaPersonajes[rnd.Next(0, listaPersonajes.Count)];
                        listaPersonajes.Remove(perdedor);
                    }

                    ganador.MejorarPJ();
                }
                else
                {
                    Console.WriteLine("Empate!");
                }
            }

            // Mostrar ganador y guardarlo en un archivo .csv
            if (player1.estaVivo()) Console.WriteLine($"El GANADOR final de la partida es el jugador {player1.Dat.Nombre} {player1.Dat.Apodo}!!!");
            else Console.WriteLine($"El GANADOR final de la partida es el jugador {player2.Dat.Nombre} {player2.Dat.Apodo}!!!");
            GuardarCSV(player1, player2);
        }

        static void MostrarColor(string cadena, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(cadena);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void MostrarDelay(string cadena, int delay)
        {
            foreach(char c in cadena)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }

        static void LimpiarLinea()
        {
            Console.Write("\r");
            for(int i = 0; i < 100; i++) Console.Write(" ");
            Console.Write("\r");
        }

        static void GuardarCSV(Personaje player1, Personaje player2)
        {
            if (!File.Exists("ganadores.csv"))
            {
                File.Create("ganadores.csv").Close();
                File.WriteAllText("ganadores.csv", "FECHA;NOMBRE;DAÑO\n");
            }

            if (player1.estaVivo())
            {
                File.AppendAllText("ganadores.csv", $"{DateTime.Now.ToString("dd/MM/yyyy")};{player1.NombreYApodo};{player1.DmgTotalCausado}\n");
            }
            else
            {
                File.AppendAllText("ganadores.csv", $"{DateTime.Now.ToString("dd/MM/yyyy")};{player2.NombreYApodo};{player2.DmgTotalCausado}\n");
            }
        }

        static void BarraProgreso(int completo, int restante)
        {
            Console.Write("[");
            for(int i = 0; i < completo; i++)
            {
                Console.Write("█");
            }
            for(int i = completo; i < restante; i++)
            {
                Console.Write(" ");
            }
            Console.Write($"]    {completo} de {restante}");
        }

        static void Registro()
        {
            int opcion = 0;

            while(opcion < 1 || opcion > 2)
            {
                Console.WriteLine("\n1) Ver registro");
                Console.WriteLine("2) Limpiar registro");
                Console.WriteLine("3) Regresar al menú");

                Console.Write("Opción: ");
                opcion = Int32.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        if (File.Exists("ganadores.csv"))
                        {
                            Console.WriteLine("Mostrando el historial de batallas\n");
                            List<string> lista = new List<string>();
                            string[] listado;

                            lista = File.ReadLines("ganadores.csv").ToList();
                            lista.RemoveAt(0);

                            foreach (string registro in lista)
                            {
                                listado = registro.Split(";");
                                Console.WriteLine($"Fecha de batalla: {listado[0]}");
                                Console.WriteLine($"Nombre: {listado[1]}");
                                Console.WriteLine($"Daño total: {listado[2]}\n");

                            }
                        }
                        else
                        {
                            Console.WriteLine("No se registró ninguna batalla");
                        }
                        break;
                    case 2:
                        if (File.Exists("ganadores.csv"))
                        {
                            File.Delete("ganadores.csv");
                        }

                        Console.WriteLine("Registro limpiado");
                        break;
                    case 3:
                        Console.WriteLine("Regresando...");
                        break;
                    default:
                        Console.WriteLine("Opción incorrecta");
                        break;
                }
            }
            
            
        }
    }
}