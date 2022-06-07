namespace JuegoRPG{
    class Program{
        static void Main(string[] args){
            int cantidadJugadores;
            List<Personaje> listaPersonajes = new List<Personaje>();
            int num1, num2, contEnfrentamientos = 0;
            Personaje player1, player2;
            Random rnd = new Random();

            // Ingresar la cantidad de jugadores
            Console.WriteLine("Ingrese la cantidad de jugadores (mínimo 2): ");
            do
            {
                cantidadJugadores = int.Parse(Console.ReadLine());
            } while (cantidadJugadores < 2);

            for (int i = 0; i < cantidadJugadores; i++)
            {
                listaPersonajes.Add(new Personaje(new Caracteristicas(), new Datos()));
            }

            // Mostrar los datos
            Console.WriteLine($"Mostrando los datos de {cantidadJugadores} personajes\n");

            foreach(Personaje p in listaPersonajes)
            {
                Console.WriteLine(p);
            }

            // Elegir los personajes de forma aleatoria
            num1 = rnd.Next(0, listaPersonajes.Count);
            do
            {
                num2 = rnd.Next(0, listaPersonajes.Count);
            } while (num2 == num1);

            player1 = listaPersonajes[num1];
            player2 = listaPersonajes[num2];

            // Enfrentamientos
            while (listaPersonajes.Count > 1)
            {
                contEnfrentamientos++;

                Console.WriteLine($"\nEnfrentamiento {contEnfrentamientos}");
                Console.WriteLine($"{player1.Dat.Nombre} {player1.Dat.Apodo} vs {player2.Dat.Nombre} {player2.Dat.Apodo}");

                for(int i = 0; i < 6 && player1.estaVivo() && player2.estaVivo(); i++)
                {
                    if(i % 2 == 0)
                    {
                        Console.WriteLine($"Ataca el jugador {player1.Dat.Nombre} {player1.Dat.Apodo}");
                        Console.WriteLine($"Daño provocado: {Gameplay.Ataque(player1, player2)}\n");
                    }
                    else
                    {
                        Console.WriteLine($"Ataca el jugador {player2.Dat.Nombre} {player2.Dat.Apodo}");
                        Console.WriteLine($"Daño provocado: {Gameplay.Ataque(player1, player2)}\n");
                    }
                }

                if (!player1.estaVivo() || player1.Dat.Salud < player2.Dat.Salud)
                {
                    Console.WriteLine($"Perdió el jugador {player1.Dat.Nombre} {player1.Dat.Apodo}");
                    listaPersonajes.Remove(player1);
                    do
                    {
                        num1 = rnd.Next(0, listaPersonajes.Count);
                    } while (num1 == num2 && listaPersonajes.Count > 1);
                    player1 = listaPersonajes[num1];
                    player2.MejorarPJ();
                }
                else if(!player2.estaVivo() || player2.Dat.Salud < player1.Dat.Salud)
                {
                    Console.WriteLine($"Perdió el jugador {player2.Dat.Nombre} {player2.Dat.Apodo}");
                    listaPersonajes.Remove(player2);
                    do
                    {
                        num2 = rnd.Next(0, listaPersonajes.Count);
                    } while (num2 == num1 && listaPersonajes.Count > 1);
                    player2 = listaPersonajes[num2];
                    player1.MejorarPJ();
                }
                else
                {
                    Console.WriteLine("Empate");
                }
            }

            if(player1.estaVivo()){
                Console.WriteLine($"Ganó el jugador {player1.Dat.Nombre} {player1.Dat.Apodo}");
            }
            else{
                Console.WriteLine($"Ganó el jugador {player2.Dat.Nombre} {player2.Dat.Apodo}");
            }
        }
    }
}