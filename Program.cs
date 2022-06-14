using System.Text.Json;

namespace JuegoRPG{
    class Program{
        static void Main(string[] args){
            int cantidadJugadores = 0;
            List<Personaje> listaPersonajes = new List<Personaje>();
            int opcion = 0, contEnfrentamientos = 1;
            int dmgProvocado1 = 0, dmgProvocado2 = 0, aux;
            Personaje player1, player2;
            Random rnd = new Random();

            while(opcion != 3)
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
                        int opcion1 = 0;

                        while(opcion1 < 1 || opcion > 3){
                            Console.WriteLine("1) Generar aleatoriamente los jugadores");
                            Console.WriteLine("2) Leer los jugadores de un json");
                            Console.WriteLine("3) Retornar");
                            Console.Write("Opción: ");
                            opcion1 = Int32.Parse(Console.ReadLine());

                            switch(opcion1){
                                case 1:
                                    // Ingresar la cantidad de jugadores
                                    Console.WriteLine("Ingrese la cantidad de jugadores (mínimo 2, máximo 20): ");
                                    do
                                    {
                                        cantidadJugadores = Int32.Parse(Console.ReadLine());
                                    } while (cantidadJugadores < 2 || cantidadJugadores > 20);

                                    // Guardar los jugadores en una lista
                                    for (int i = 0; i < cantidadJugadores; i++)
                                    {
                                        listaPersonajes.Add(new Personaje(new Caracteristicas(), new Datos()));
                                    }

                                    // Guardar en un archivo json los jugadores
                                    string json = JsonSerializer.Serialize(listaPersonajes);
                                    Console.WriteLine(json);
                                    if(!File.Exists("jugadores.json")){
                                        File.Create("jugadores.json").Close();
                                    }
                                    File.WriteAllText("jugadores.json", json);

                                    break;
                                case 2:
                                    // Obtener los jugadores del archivo json
                                    if(!File.Exists("jugadores.json")){
                                        Console.WriteLine("No es posible cargar los jugadores de un archivo json");
                                    }
                                    else{
                                        try{
                                            listaPersonajes = JsonSerializer.Deserialize<List<Personaje>>(File.ReadAllText("jugadores.json"));
                                            cantidadJugadores = listaPersonajes.Count();
                                        }
                                        catch{
                                            Console.WriteLine("No es posible cargar los jugadores de un archivo json");
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
                        
                        

                        // Mostrar los datos de cada jugador
                        Console.WriteLine($"Mostrando los datos de {cantidadJugadores} personajes\n");

                        foreach (Personaje p in listaPersonajes)
                        {
                            Console.WriteLine(p);
                        }

                        // Elegir los personajes
                        player1 = listaPersonajes[0];
                        player2 = listaPersonajes[1];
                        listaPersonajes.Remove(player1);
                        listaPersonajes.Remove(player2);

                        
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
                                    aux = Gameplay.Ataque(player1, player2);
                                    dmgProvocado2 += aux;
                                    Console.WriteLine($"Ataca el jugador {player2.Dat.Nombre} {player2.Dat.Apodo}");
                                    Console.WriteLine($"Daño provocado: {aux}\n");
                                }
                            }

                            // Determinar ganador del enfrentamiento
                            if (!player1.estaVivo() || player1.Dat.Salud < player2.Dat.Salud)
                            {
                                Console.WriteLine($"Perdió el jugador {player1.Dat.Nombre} {player1.Dat.Apodo}");
                                if(listaPersonajes.Count > 0)
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
                                if(listaPersonajes.Count > 0)
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
                        }

                        // Mostrar ganador y guardarlo en un archivo .csv
                        if (!File.Exists("ganadores.csv"))
                        {
                            File.Create("ganadores.csv").Close();
                            File.WriteAllText("ganadores.csv", "FECHA;NOMBRE;DAÑO\n");
                        }

                        if (player1.estaVivo())
                        {
                            Console.WriteLine($"Ganó el jugador {player1.Dat.Nombre} {player1.Dat.Apodo}\n");
                            File.AppendAllText("ganadores.csv", $"{DateTime.Now.ToString("dd/MM/yyy")};{player1.Dat.Nombre + " " + player1.Dat.Apodo};{dmgProvocado1}\n");
                        }
                        else
                        {
                            Console.WriteLine($"Ganó el jugador {player2.Dat.Nombre} {player2.Dat.Apodo}\n");
                            File.AppendAllText("ganadores.csv", $"{DateTime.Now.ToString("dd/MM/yyy")};{player2.Dat.Nombre + " " + player2.Dat.Apodo};{dmgProvocado2}\n");
                        }
                        
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

        static void Registro()
        {
            int opcionRegistro = 0;

            while(opcionRegistro < 1 || opcionRegistro > 2)
            {
                Console.WriteLine("\n1) Ver registro");
                Console.WriteLine("2) Limpiar registro");
                Console.WriteLine("3) Regresar al menú");

                Console.Write("Opción: ");
                opcionRegistro = Int32.Parse(Console.ReadLine());

                switch (opcionRegistro)
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