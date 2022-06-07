namespace JuegoRPG{
    class Personaje{
        private Caracteristicas carac;
        private Datos dat;

        public Personaje(Caracteristicas carac, Datos dat){
            this.carac = carac;
            this.dat = dat;
        }

        internal Caracteristicas Carac { get => carac; set => carac = value; }
        internal Datos Dat { get => dat; set => dat = value; }

        public void MejorarPJ ()
        {
            this.dat.SaludInicial += 200;
            this.dat.Salud = this.dat.SaludInicial;
            this.Carac.Armadura += 2;
            this.Carac.Velocidad += 2;
            this.Carac.Fuerza += 2;
            this.Carac.Destreza += 2;
            this.Carac.Nivel += 1;
        }

        public void RecibirDMG(int dmg)
        {
            this.dat.Salud -= dmg;
            if(this.dat.Salud < 0)
            {
                this.dat.Salud = 0;
            }
        }

        public bool estaVivo()
        {
            return this.Dat.Salud > 0;
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

            cadena += "Caracter�sticas:\n";
            cadena += $"Velocidad: {this.carac.Velocidad}\n";
            cadena += $"Destreza: {this.carac.Destreza}\n";
            cadena += $"Fuerza: {this.carac.Fuerza}\n";
            cadena += $"Nivel: {this.carac.Nivel}\n";
            cadena += $"Armadura: {this.carac.Armadura}\n";

            return cadena;
        }
    }
}