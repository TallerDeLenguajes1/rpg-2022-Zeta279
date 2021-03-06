namespace JuegoRPG{
    class Datos{
        private string tipo;
        private string nombre;
        private string apodo;
        private DateTime fechaNac;
        private int edad;
        private int salud;
        private int saludInicial;

        public Datos(){
            string[] tipos = new string[] {"Elfo", "Hechicero", "Barbaro", "Orco", "Arquero"};
            string[] nombres = new string[] {"Ezequiel", "Agustin", "Sergio", "Jorge", "Facundo"};
            string[] apodos = new string[] {"Power", "Xtreme", "Manco", "Master", "Gamer"};
            Random rnd = new Random();

            // Tratar de conseguir los nombres utilizando una API
            try
            {
                ObtenerNombre().Wait();
            }
            catch
            {
                this.nombre = nombres[rnd.Next(0, 5)];
            }

            this.tipo = tipos[rnd.Next(0, 5)];
            this.apodo = apodos[rnd.Next(0, 5)];
            this.fechaNac = new DateTime(1985, 1, 1);
            this.fechaNac = this.fechaNac.AddMonths(rnd.Next(1, 12));
            this.fechaNac = this.fechaNac.AddDays(rnd.Next(1, 31));
            this.edad = DateTime.Now.Year - fechaNac.Year;
            this.salud = 3000;
            this.saludInicial = 3000;
        }

        private async Task ObtenerNombre()
        {
            Persona? persona = await Persona.ObtenerPersona();
            List<String> lista = persona.Name.Split(" ").ToList();
            while(lista.Count > 2)
            {
                lista.RemoveAt(lista.Count - 1);
            }
            if (lista[0].EndsWith(".")) lista.RemoveAt(0);
            this.Nombre = String.Join(" ", lista);
        }

        public string Tipo { get => tipo; set => tipo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apodo { get => apodo; set => apodo = value; }
        public DateTime FechaNac { get => fechaNac; set => fechaNac = value; }
        public int Edad { get => edad; set => edad = value; }
        public int Salud { get => salud; set => salud = value; }
        public int SaludInicial { get => saludInicial; set => saludInicial = value; }
    }
}