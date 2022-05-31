namespace JuegoRPG{
    class Program{
        static void Main(string[] args){
            int cantidadJugadores;
            List<Personaje> listaPersonajes = new List<Personaje>();

            Console.WriteLine("Ingrese la cantidad de jugadores: ");
            cantidadJugadores = int.Parse(Console.ReadLine());
        }
    }
}