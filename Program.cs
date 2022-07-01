using System.Text.Json;

namespace JuegoRPG{
    class Program{
        static void Main(string[] args){
            int opcion = 0;
            List<Personaje> listaPersonajes = new List<Personaje>();

            while (opcion != 3)
            {

                Console.WriteLine("Ingrese una opción: ");
                Console.WriteLine("1) Jugar");
                Console.WriteLine("2) Registro de batallas");
                Console.WriteLine("3) Salir");

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
                        Console.WriteLine("\nIngrese la cantidad de jugadores (mínimo 2, máximo 20): ");
                        do
                        {
                            cantidad = Int32.Parse(Console.ReadLine());
                        } while (cantidad < 2 || cantidad > 20);

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
            int dmgProvocado1 = 0, dmgProvocado2 = 0, contEnfrentamientos = 1, aux;
            int cantidadJugadores = listaPersonajes.Count;
            Personaje atacante, defensor;
            Random rnd = new Random();

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
                Console.WriteLine($"{player1.Dat.Nombre} {player1.Dat.Apodo} vs {player2.Dat.Nombre} {player2.Dat.Apodo}");

                for (int i = 0; i < 6 && player1.estaVivo() && player2.estaVivo(); i++)
                {
                    if (i % 2 == 0)
                    {
                        aux = Gameplay.Ataque(player1, player2);
                        dmgProvocado1 += aux;
                        Console.WriteLine($"Ataca el jugador {player1.Dat.Nombre} {player1.Dat.Apodo}");
                        Console.WriteLine($"Daño provocado: {aux}\n");
                    }
                    else
                    {
                        aux = Gameplay.Ataque(player2, player1);
                        dmgProvocado2 += aux;
                        Console.WriteLine($"Ataca el jugador {player2.Dat.Nombre} {player2.Dat.Apodo}");
                        Console.WriteLine($"Daño provocado: {aux}\n");
                    }
                    Thread.Sleep(500);
                }

                // Determinar ganador del enfrentamiento
                if (!player1.estaVivo() || player1.Dat.Salud < player2.Dat.Salud)
                {
                    Console.WriteLine($"Perdió el jugador {player1.Dat.Nombre} {player1.Dat.Apodo}");
                    if (listaPersonajes.Count > 0)
                    {
                        player1 = listaPersonajes[rnd.Next(0, listaPersonajes.Count)];
                        listaPersonajes.Remove(player1);
                    }
                    dmgProvocado1 = 0;
                    player2.MejorarPJ();
                }
                else if (!player2.estaVivo() || player2.Dat.Salud < player1.Dat.Salud)
                {
                    Console.WriteLine($"Perdió el jugador {player2.Dat.Nombre} {player2.Dat.Apodo}");
                    if (listaPersonajes.Count > 0)
                    {
                        player2 = listaPersonajes[rnd.Next(0, listaPersonajes.Count)];
                        listaPersonajes.Remove(player2);
                    }
                    dmgProvocado2 = 0;
                    player1.MejorarPJ();
                }
                else
                {
                    Console.WriteLine("Empate");
                }
                Console.Write("Presione una tecla para continuar");
                Console.ReadKey();
                Console.Write("\r ");
            }

            // Mostrar ganador y guardarlo en un archivo .csv
            GuardarCSV(player1, player2, dmgProvocado1, dmgProvocado2);
        }

        static void GuardarCSV(Personaje player1, Personaje player2, int danio1, int danio2)
        {
            if (!File.Exists("ganadores.csv"))
            {
                File.Create("ganadores.csv").Close();
                File.WriteAllText("ganadores.csv", "FECHA;NOMBRE;DAÑO\n");
            }

            if (player1.estaVivo())
            {
                Console.WriteLine($"Ganó el jugador {player1.Dat.Nombre} {player1.Dat.Apodo}\n");
                File.AppendAllText("ganadores.csv", $"{DateTime.Now.ToString("dd/MM/yyyy")};{player1.Dat.Nombre + " " + player1.Dat.Apodo};{danio1}\n");
            }
            else
            {
                Console.WriteLine($"Ganó el jugador {player2.Dat.Nombre} {player2.Dat.Apodo}\n");
                File.AppendAllText("ganadores.csv", $"{DateTime.Now.ToString("dd/MM/yyyy")};{player2.Dat.Nombre + " " + player2.Dat.Apodo};{danio2}\n");
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