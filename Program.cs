namespace JuegoRPG{
    class Program{
        static void Main(string[] args){
            int cantidadJugadores;
            List<Personaje> listaPersonajes = new List<Personaje>();
            int jugadoresRestantes, player1, player2;
            Random rnd = new Random();

            Console.WriteLine("Ingrese la cantidad de jugadores: ");
            cantidadJugadores = int.Parse(Console.ReadLine());
            jugadoresRestantes = cantidadJugadores;

            for (int i = 0; i < cantidadJugadores; i++)
            {
                listaPersonajes.Add(new Personaje(new Caracteristicas(), new Datos()));
            }

            Console.WriteLine($"Mostrando los datos de {cantidadJugadores} personajes\n");

            foreach(Personaje p in listaPersonajes)
            {
                Console.WriteLine(p);
            }
        }
    }
}