namespace JuegoRPG{
    class Caracteristicas{
        private int velocidad;
        private int destreza;
        private int fuerza;
        private int nivel;
        private int armadura;

        public Caracteristicas(){
            Random random = new Random();

            this.velocidad = random.Next(1, 11);
            this.destreza = random.Next(1, 6);
            this.fuerza = random.Next(1, 11);
            this.nivel = random.Next(1, 11);
            this.armadura = random.Next(1, 11);
        }

        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Destreza { get => destreza; set => destreza = value; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Armadura { get => armadura; set => armadura = value; }
    }
}