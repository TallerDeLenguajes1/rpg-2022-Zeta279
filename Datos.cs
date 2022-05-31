namespace JuegoRPG{
    class Datos{
        private string tipo;
        private string nombre;
        private string apodo;
        private DateTime fechaNac;
        private int edad;
        private int salud;

        public Datos(){
            string[] tipos = new string[] {"Elfo", "Hechicero", "Barbaro", "Orco", "Arquero"};
            string[] nombres = new string[] {"Ezequiel", "Agustin", "Sergio", "Jorge", "Facundo"};
            string[] apodos = new string[] {"Power", "Xtreme", "Manco", "Master", "Gamer"};
            Random rnd = new Random();

            this.tipo = tipos[rnd.Next(0, 5)];
            this.nombre = nombres[rnd.Next(0, 5)];
            this.apodo = apodos[rnd.Next(0, 5)];
            this.fechaNac = new DateTime(rnd.Next(1985, 2006), rnd.Next(1, 13), rnd.Next(1, 31));
            this.edad = DateTime.Now.Year - fechaNac.Year;
            this.salud = 100;
        }

        public string Tipo { get => tipo; set => tipo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apodo { get => apodo; set => apodo = value; }
        public DateTime FechaNac { get => fechaNac; set => fechaNac = value; }
        public int Edad { get => edad; set => edad = value; }
        public int Salud { get => salud; set => salud = value; }
    }
}