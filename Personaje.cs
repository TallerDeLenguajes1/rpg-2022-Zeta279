namespace JuegoRPG{
    class Personaje{
        private Caracteristicas carac;
        private Datos dat;

        public Personaje(Caracteristicas carac, Datos dat){
            this.carac = carac;
            this.dat = dat;
        }

        public override string ToString()
        {
            string cadena = new string("");

            cadena += "Datos:\n";
            cadena += $"Tipo: {this.dat.Tipo}\n";
            cadena += $"Nombre: {this.dat.Nombre}\n";
            cadena += $"Apodo: {this.dat.Apodo}\n";
            cadena += $"Fecha de nacimiento: {this.dat.FechaNac.ToString("dd/MM/yyyy")}\n";
            cadena += $"Edad: {this.dat.Edad}\n\n";

            cadena += "Características:\n";
            cadena += $"Velocidad: {this.carac.Velocidad}\n";
            cadena += $"Destreza: {this.carac.Destreza}\n";
            cadena += $"Fuerza: {this.carac.Fuerza}\n";
            cadena += $"Nivel: {this.carac.Nivel}\n";
            cadena += $"Armadura: {this.carac.Armadura}\n";

            return cadena;
        }
    }
}